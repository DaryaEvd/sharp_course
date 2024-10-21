namespace Nsu.Sharps.Hackathon.NetGenericHost.Models;

public class Junior : Participant
{
    public Junior(int id, string name) : base(id, name) { }
    
    public int CalculateHarmonyLevel(int teamLeadId)
    {
        var index = Wishlist.IndexOf(teamLeadId);
        if (index < 0)
        {
            throw new InvalidOperationException($"teamlead {teamLeadId} is not in the wishlist of jun {Id}");
        }

        return Constants.AmountOfJuniors - index;
        
    }
}