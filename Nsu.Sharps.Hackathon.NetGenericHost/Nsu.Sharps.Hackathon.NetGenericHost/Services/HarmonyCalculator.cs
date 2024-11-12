using Microsoft.Extensions.Options;
using Nsu.Sharps.Hackathon.NetGenericHost.Models;
using Nsu.Sharps.Hackathon.NetGenericHost.Options;

namespace Nsu.Sharps.Hackathon.NetGenericHost.Services;

public class HarmonyCalculator
{
    private readonly AmountValuesOptions _amountValuesOptions;

    private readonly CalculationDataOptions _calculationDataOptions;

    public HarmonyCalculator(IOptions<CalculationDataOptions> calculationDataOptions,
        IOptions<AmountValuesOptions> amountValuesOptions)
    {
        _calculationDataOptions = calculationDataOptions.Value;
        _amountValuesOptions = amountValuesOptions.Value;
    }

    public int CalculateHarmonyLevel(Participant participant, int targetId, bool isJunior)
    {
        var totalParticipants =
            isJunior ? _amountValuesOptions.AmountOfJuniors : _amountValuesOptions.AmountOfTeamLeads;
        var index = participant.Wishlist.IndexOf(targetId);
        if (index < 0)
            throw new InvalidOperationException(
                $"{targetId} is not in the wishlist of {participant.Name} (Id: {participant.Id})");

        return totalParticipants - index;
    }

    public double CalculateHarmonicMean(int juniorHarmonyLevel, int teamLeadHarmonyLevel)
    {
        return _calculationDataOptions.AmountOfParticipantsInNumerator /
               (_calculationDataOptions.NumeratorInCountingHarmony / juniorHarmonyLevel +
                _calculationDataOptions.NumeratorInCountingHarmony / teamLeadHarmonyLevel);
    }
}