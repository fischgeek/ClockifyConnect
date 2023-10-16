namespace ClockifyConnect

open Requesty.Requesty
open Types
open System
open TypeBuilders

module RequestyClockify = 
    let private getkey() = Environment.GetEnvironmentVariable("ClockifyAPIKey", EnvironmentVariableTarget.Machine)
    let private _url = "https://api.clockify.me/api/v1"

    let private initCall (ep: string) = 
        let _key = getkey()
        HRB.Create()
        |> HRB.Url $"{_url}/{ep}"
        |> HRB.AddHeader "X-Api-Key" _key

    let private run parseFn (url: string) = 
        url
        |> initCall
        |> HRB.StockFns.RunWithTextResponse
        |> function
        | Ok res -> parseFn res |> Seq.toList |> Ok
        | Error x -> Error x

    let GetWorkspaces () = 
        Workspaces 
        |> ClockifyEndpoint.ToString
        |> run ParseWorkspaces
        |> function
        | Ok x -> x |> Ok
        | Error er -> er |> Error
            
    let GetProjects (wid: string) = 
        Projects wid
        |> ClockifyEndpoint.ToUrl
        |> run ParseProjects
        



