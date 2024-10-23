namespace Nsu.Sharps.Hackathon.ConsoleApp.Models;

public class TeamLead : Participant
{
    public TeamLead(int id, string name) : base(id, name)
    {
    }

    public int CalculateHarmonyLevel(int juniorId)
    {
        return CalculateHarmonyLevel(juniorId, Constants.AmountOfTeamLeads);
    }
}