namespace Nsu.Sharps.Hackathon.NetGenericHost.Models;

public class Team
{
    public Team(TeamLead teamLead, Junior junior)
    {
        TeamLead = teamLead;
        Junior = junior;
    }

    public TeamLead TeamLead { get; set; }
    public Junior Junior { get; set; }
}