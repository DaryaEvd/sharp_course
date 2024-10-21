namespace Nsu.Sharps.Hackathon.NetGenericHost.Models;

public class TeamLead : Participant
{
    public TeamLead(int id, string name) : base(id, name) { }
    
    public int CalculateHarmonyLevel(int juniorId)
    {
        var index = Wishlist.IndexOf(juniorId);
        if (index < 0)
        {
            throw new InvalidOperationException($"junior {juniorId} is not in the wishlist of teamlead {Id}");
        }
        return Constants.AmountOfTeamLeads - index;
    }
}