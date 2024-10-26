using Nsu.Sharps.Hackathon.NetGenericHost.Models;

namespace Nsu.Sharps.Hackathon.NetGenericHost.Interfaces;

public interface IReader
{
    List<Participant> ReadFile(string pathToFile);
}