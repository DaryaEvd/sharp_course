namespace Nsu.Sharps.Hackathon.ConsoleApp.Models;

public abstract class Participant
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public List<int> Wishlist { get; set; }

    protected Participant(int id, string name)
    {
        Id = id;
        Name = name;
        Wishlist = new List<int>();
    }
}