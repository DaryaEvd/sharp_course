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

    public int CalculateSatisfactionLevel(int preferredId)
    {
        var preferenceRank = Wishlist.IndexOf(preferredId);
        return preferenceRank >= 0 ? 20 - preferenceRank : 0;
    }
}