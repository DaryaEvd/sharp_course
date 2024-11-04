namespace Nsu.Sharps.Hackathon.ConsoleApp.Models;

public abstract class Participant
{
    protected Participant(int id, string name)
    {
        Id = id;
        Name = name;
        Wishlist = new List<int>();
    }

    public int Id { get; }
    public string Name { get; }
    public List<int> Wishlist { get; set; }

    public int CalculateHarmonyLevel(int targetId, int totalParticipants)
    {
        var index = Wishlist.IndexOf(targetId);
        if (index < 0) throw new InvalidOperationException($"{targetId} is not in the wishlist of {Name} (Id: {Id})");

        return totalParticipants - index;
    }
}