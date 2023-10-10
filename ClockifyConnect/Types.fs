namespace ClockifyConnect

open FSharp.Data
open JFSharp.Pipes
open JFSharp.Pipes

[<AutoOpen>]
module Types = 
    type _Workspaces = JsonProvider<"C:\dev\PublicRepos\FSharpDataProviderSampleFiles\json\clockify\clockify-workspace.sample.json", RootName="Workspaces">
    type _Workspace = JsonProvider<"C:\dev\PublicRepos\FSharpDataProviderSampleFiles\json\clockify\clockify-workspace.sample.json", RootName="Workspace", SampleIsList=true>

    type Endpoint = 
        | Workspaces
        static member ToString (x: Endpoint) = x.ToString().ToLower()

    type ClockifyHourlyRate = 
        {
            Amount: int
            Currency: string
        }

    type ClockifyMembership = 
        {
            UserId: string
            HourlyRate: ClockifyHourlyRate option
            CostRate: ClockifyHourlyRate option
            TargetId: string
            MembershipType: string
            MembershipStatus: string
        }

    type ClockifyWorkspaceSettings = 
        {
            DefaultBillableProjects: bool
        }

    type ClockifyWorkspace =
        {
            Id: string
            Name: string
            HourlyRate: ClockifyHourlyRate
            CostRate: ClockifyHourlyRate
            Membership: ClockifyMembership list
            WorkspaceSettings: ClockifyWorkspaceSettings
            ImageUrl: string
            FeatureSubscriptionType: string
        }