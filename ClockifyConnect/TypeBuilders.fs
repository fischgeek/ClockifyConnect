namespace ClockifyConnect

open FSharp.Data
open JFSharp.Pipes

module TypeBuilders = 

    let private buildRate (r: _Workspaces.HourlyRate) =
        {
            Amount = r.Amount
            Currency = r.Currency
        }
    let private buildMembership (m: _Workspaces.Membership) = 
        {
            UserId = m.UserId
            HourlyRate = m.HourlyRate |> OptionPipe.SomeToFNX buildRate
            CostRate = m.CostRate |> OptionPipe.SomeToFNX buildRate
            TargetId = m.TargetId
            MembershipType = m.MembershipType
            MembershipStatus = m.MembershipStatus
        }
    let private buildWorkspaceSettings (w: _Workspaces.WorkspaceSettings) = 
        {
            DefaultBillableProjects = w.DefaultBillableProjects
        }
    let private buildWorkspace (w: _Workspaces.Workspacis) =
        {
            Id = w.Id
            Name = w.Name
            HourlyRate = w.HourlyRate |> buildRate
            CostRate = w.CostRate |> buildRate
            Membership = w.Memberships |> Array.map buildMembership |> Array.toList
            WorkspaceSettings = w.WorkspaceSettings |> buildWorkspaceSettings
            ImageUrl = w.ImageUrl
            FeatureSubscriptionType = w.FeatureSubscriptionType
        }
    let ParseWorkspace (x: string) = _Workspace.Parse x
    let ParseWorkspaces (x: string) = _Workspaces.Parse x |> Seq.map buildWorkspace