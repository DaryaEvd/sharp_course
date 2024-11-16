using Microsoft.Extensions.Options;
using Nsu.Sharps.Hackathon.NetGenericHost.Options;

namespace Nsu.Sharps.Hackathon.NetGenericHost.Models;

public abstract class Participant
{
    private IOptions<AmountValuesOptions> amountOptions;

    protected Participant(int id, string name)
    {
        Id = id;
        Name = name;
        Wishlist = new List<int>();
    }

    public int Id { get; }
    public string Name { get; }
    public List<int> Wishlist { get; set; }

    public int CalculateHarmonyLevelByParticipant(int preferredId, int totalParticipantsInTeam)
    {
        var preferenceRank = Wishlist.IndexOf(preferredId);
        if (preferenceRank < 0)
            throw new InvalidOperationException(
                $"{preferredId} is not in the wishlist of {Name} (Id: {Id})");

        return totalParticipantsInTeam - preferenceRank;
    }
}