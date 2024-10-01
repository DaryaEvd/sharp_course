using Nsu.Sharps.Hackathon.ConsoleApp.Models;

namespace Nsu.Sharps.Hackathon.ConsoleApp.Interfaces;

public interface IReader
{
    List<Participant> ReadFile(string pathToFile);
}
