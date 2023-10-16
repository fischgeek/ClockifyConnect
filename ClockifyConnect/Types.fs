namespace ClockifyConnect

open FSharp.Data
open JFSharp.Pipes
open JFSharp.Pipes

[<AutoOpen>]
module Types = 
    type _Workspaces = JsonProvider<"C:\dev\PublicRepos\FSharpDataProviderSampleFiles\json\clockify\workspace.sample.json", RootName="Workspaces">
    type _Workspace = JsonProvider<"C:\dev\PublicRepos\FSharpDataProviderSampleFiles\json\clockify\workspace.sample.json", RootName="Workspace", SampleIsList=true>
    type _Projects = JsonProvider<"C:\dev\PublicRepos\FSharpDataProviderSampleFiles\json\clockify\project.sample.json", RootName="Projects">
    type _Project = JsonProvider<"C:\dev\PublicRepos\FSharpDataProviderSampleFiles\json\clockify\project.sample.json", RootName="Project", SampleIsList=true>

    type ClockifyEndpoint = 
        | Workspaces 
        | Projects of string
        static member ToString (x: ClockifyEndpoint) = x.ToString().ToLower()
        static member ToUrl (x: ClockifyEndpoint) = 
            match x with
            | Projects z -> $"/workspaces/{z}/projects"
            | Workspaces -> ""

    type ClockifyHourlyRate = 
        {
            Amount: int
            Currency: string
        }

    type ClockifyEstimate = 
        {
            Estimate: string
            EstimateType: string
        }

    type ClockifyTimeEstimate = 
        {
            Estimate: string
            EstimateType: string
            Active: bool
            IncludeNonBillable: bool
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

    type ClockifyProject = 
        {
            Id: string
            Name: string
            ClientId: string
            Color: string
            Billable: bool
            Note: string
            Public: bool
            WorkspaceId: string
            clientName: string
            Duration: string
            Estimate: ClockifyEstimate
            HourlyRate: ClockifyHourlyRate
            Memberships: ClockifyMembership list
            CostRate: ClockifyHourlyRate option
            TimeEstimate: ClockifyTimeEstimate
        }