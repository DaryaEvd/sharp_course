namespace Nsu.Sharps.Hackathon.NetGenericHost.Models;

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
}