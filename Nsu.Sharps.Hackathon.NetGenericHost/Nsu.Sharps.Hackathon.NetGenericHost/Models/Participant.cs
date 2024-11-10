namespace Nsu.Sharps.Hackathon.NetGenericHost.Models;

public abstract class Participant
{
    protected readonly int TotalParticipants;

    protected Participant(int id, string name, int totalParticipants)
    {
        Id = id;
        Name = name;
        Wishlist = new List<int>();
        TotalParticipants = totalParticipants;
    }

    public int Id { get; }
    public string Name { get; }
    public List<int> Wishlist { get; set; }

    public int CalculateHarmonyLevel(int targetId)
    {
        var index = Wishlist.IndexOf(targetId);
        if (index < 0)
            throw new InvalidOperationException($"{targetId} is not in the wishlist of {Name} (Id: {Id})");

        return TotalParticipants - index;
    }
}