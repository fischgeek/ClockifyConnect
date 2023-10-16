namespace ClockifyConnect

open FSharp.Data
open JFSharp.Pipes

module TypeBuilders = 

    let private buildWorkspaceRate (r: _Workspaces.HourlyRate) =
        {
            Amount = r.Amount
            Currency = r.Currency
        }
    let private buildProjectRate (r: _Projects.HourlyRate) =
        {
            Amount = r.Amount
            Currency = r.Currency
        }
    let private buildEstimate (r: _Projects.Estimate) = 
        {
            Estimate = r.Estimate
            EstimateType = r.Type
        }
    let private buildProjectTimeEstimate (r: _Projects.TimeEstimate) = 
        {
            Estimate = r.Estimate
            EstimateType = r.Type
            Active = r.Active
            IncludeNonBillable = r.IncludeNonBillable
        }
    let private buildWorkspaceMembership (m: _Workspaces.Membership) = 
        {
            UserId = m.UserId
            HourlyRate = m.HourlyRate |> OptionPipe.SomeToFNX buildWorkspaceRate
            CostRate = m.CostRate |> OptionPipe.SomeToFNX buildWorkspaceRate
            TargetId = m.TargetId
            MembershipType = m.MembershipType
            MembershipStatus = m.MembershipStatus
        }
    let private buildProjectMembership (m: _Projects.Membership) = 
        {
            UserId = m.UserId
            HourlyRate = m.HourlyRate |> OptionPipe.SomeToFNX buildProjectRate
            CostRate = m.CostRate |> OptionPipe.SomeToFNX buildProjectRate
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
            HourlyRate = w.HourlyRate |> buildWorkspaceRate
            CostRate = w.CostRate |> buildWorkspaceRate
            Membership = w.Memberships |> Array.map buildWorkspaceMembership |> Array.toList
            WorkspaceSettings = w.WorkspaceSettings |> buildWorkspaceSettings
            ImageUrl = w.ImageUrl
            FeatureSubscriptionType = w.FeatureSubscriptionType
        }
    let private buildProject (p: _Projects.Project) =
        {
            Id = p.Id
            Name = p.Name
            ClientId = p.ClientId
            Color = p.Color
            Billable = p.Billable
            Note = p.Note
            Public = p.Public
            WorkspaceId = p.WorkspaceId
            clientName = p.ClientName
            Duration = p.Duration
            Estimate = p.Estimate |> buildEstimate
            HourlyRate = p.HourlyRate |> buildProjectRate
            Memberships = p.Memberships |> Array.map buildProjectMembership |> Array.toList
            CostRate = p.CostRate |> OptionPipe.SomeToFNX buildProjectRate
            TimeEstimate = p.TimeEstimate |> buildProjectTimeEstimate
        }

    let ParseWorkspace (x: string) = _Workspace.Parse x
    let ParseWorkspaces (x: string) = _Workspaces.Parse x |> Seq.map buildWorkspace
    let ParseProject (x: string) = _Project.Parse x
    let ParseProjects (x: string) = _Projects.Parse x |> Seq.map buildProject