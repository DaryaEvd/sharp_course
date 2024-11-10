using Nsu.Sharps.Hackathon.NetGenericHost.Interfaces;
using Nsu.Sharps.Hackathon.NetGenericHost.Models;

namespace Nsu.Sharps.Hackathon.NetGenericHost.Services;

public class ReadCsvFile : IReader
{
    private readonly ParticipantFactory _participantFactory;

    public ReadCsvFile(ParticipantFactory participantFactory)
    {
        _participantFactory = participantFactory;
    }

    public List<Participant> ReadFile(string pathToFile)
    {
        var participants = new List<Participant>();
        var fileName = Path.GetFileName(pathToFile).ToLower();

        using var reader = new StreamReader(pathToFile);
        var line = reader.ReadLine();
        if (line == null) throw new Exception($"File {pathToFile} doesn't exist.");

        while ((line = reader.ReadLine()) != null)
        {
            var columns = line.Split(';');
            if (columns.Length < 2) continue;
            if (!int.TryParse(columns[0], out var id)) continue;
            var name = columns[1];
            if (fileName.StartsWith("junior"))
                participants.Add(_participantFactory.CreateParticipant(id, name, "junior"));
            else if (fileName.StartsWith("teamlead"))
                participants.Add(_participantFactory.CreateParticipant(id, name, "teamlead"));
        }

        return participants;
    }
}