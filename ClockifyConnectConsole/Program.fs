namespace ClockifyConnect

open System
open RequestyClockify
open Requesty

module Main = 
    let out (x: string) = printfn $"{x}"

    let workspaces = 
        GetWorkspaces()
        |> function
        | Ok x -> 
            "Found workspaces" |> out
            x |> Seq.iteri (fun i x -> $"{x.Id} {x.Name}" |> out)
        | Error x -> HRBError.Stringify x |> out

    GetProjects "6166df91d99e8032c1f99bd0"
    |> function
    | Ok x -> 
        "Found projects" |> out
        x |> Seq.iteri (fun i x -> $"{i} {x.Name}" |> out)
    | Error x -> HRBError.Stringify x |> out

    Console.ReadLine() |> ignore