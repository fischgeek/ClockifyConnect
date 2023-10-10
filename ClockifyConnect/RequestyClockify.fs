namespace ClockifyConnect

open Requesty.Requesty
open Types
open System
open TypeBuilders

module RequestyClockify = 
    let private getkey() = Environment.GetEnvironmentVariable("ClockifyAPIKey", EnvironmentVariableTarget.Machine)
    let private _url = "https://api.clockify.me/api/v1"

    let private initCall (ep: Endpoint) = 
        let _key = getkey()
        HRB.Create()
        |> HRB.Url $"{_url}/{ep |> Endpoint.ToString}"
        |> HRB.AddHeader "X-Api-Key" _key

    let GetWorkspaces () = 
        initCall Workspaces
        |> HRB.StockFns.RunWithTextResponse
        |> function
        | Ok x -> ParseWorkspaces x |> Seq.toList |> Ok
        | Error er -> er |> Error
            


