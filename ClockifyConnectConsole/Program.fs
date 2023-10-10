namespace ClockifyConnect

open System
open RequestyClockify
open Requesty

module Main = 
    let out (x: string) = printfn $"{x}"

    GetWorkspaces()
    |> function
    | Ok x -> 
        "Found workspaces" |> out
        x |> Seq.iteri (fun i x -> $"{i} {x.Name}" |> out)
    | Error x -> HRBError.Stringify x |> out

    
    Console.ReadLine() |> ignore