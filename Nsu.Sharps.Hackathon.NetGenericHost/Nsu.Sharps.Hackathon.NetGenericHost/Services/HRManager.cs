using Nsu.Sharps.Hackathon.NetGenericHost.Models;

namespace Nsu.Sharps.Hackathon.NetGenericHost.Services;

public class HRManager
{
    private readonly HRDirector _hrDirector;
    private List<Junior> _juniors;

    private List<TeamLead> _teamLeads;

    // private IReader _reader;
    private readonly WishlistGenerator _wishlistGenerator;

    // public HRManager(/*IReader reader*/ WishlistGenerator wishlistGenerator, HRDirector hrDirector)
    public HRManager(List<Junior> juniors, List<TeamLead> teamLeads, WishlistGenerator wishlistGenerator,
        HRDirector hrDirector)

    {
        // _reader = reader;
        _wishlistGenerator = wishlistGenerator;
        _hrDirector = hrDirector;

        _juniors = juniors;
        _teamLeads = teamLeads;
        // _juniors = LoadJuniors();
        // _teamLeads = LoadTeamLeads();
    }

    // private List<Junior> LoadJuniors()
    // {
    //     var participants = _reader.ReadFile(Constants.PathForJuniors);
    //     return participants.OfType<Junior>().ToList();
    // }
    //
    // private List<TeamLead> LoadTeamLeads()
    // {
    //     var participants = _reader.ReadFile(Constants.PathForTeamLeads);
    //     return participants.OfType<TeamLead>().ToList();
    // }

    public void CheckDataLoaded()
    {
        if (_juniors == null || !_juniors.Any() || _teamLeads == null || !_teamLeads.Any())
            throw new InvalidOperationException("Data not loaded");
    }

    public void LoadParticipants(List<Junior> juniors, List<TeamLead> teamLeads)
    {
        _juniors = juniors;
        _teamLeads = teamLeads;
    }

    public double RunHackathons(int numberOfHackathons)
    {
        CheckDataLoaded();

        double totalHarmonyLevel = 0;

        for (var i = 1; i <= numberOfHackathons; i++)
        {
            var hackathon = new Hackathon(_juniors, _teamLeads, _wishlistGenerator, _hrDirector);

            hackathon.RunOneHackathon();
            totalHarmonyLevel += hackathon.HarmonyLevel;

            Console.WriteLine($"Harmony level for hackathon #{i}: {hackathon.HarmonyLevel}");
        }

        return totalHarmonyLevel / numberOfHackathons;
    }
}