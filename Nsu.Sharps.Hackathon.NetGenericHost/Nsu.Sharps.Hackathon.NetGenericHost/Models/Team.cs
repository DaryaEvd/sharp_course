namespace Nsu.Sharps.Hackathon.NetGenericHost.Models;

public class Team
{
    public Team(TeamLead teamLead, Junior junior)
    {
        TeamLead = teamLead;
        Junior = junior;
    }

    public TeamLead TeamLead { get; }
    public Junior Junior { get; }
}