namespace Nsu.Sharps.Hackathon.ConsoleApp.Models;

public class Junior : Participant
{
    public Junior(int id, string name) : base(id, name)
    {
    }

    public int CalculateHarmonyLevel(int teamLeadId)
    {
        return CalculateHarmonyLevel(teamLeadId, Constants.AmountOfJuniors);
    }
}