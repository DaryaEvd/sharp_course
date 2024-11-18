using Microsoft.Extensions.Options;
using Nsu.Sharps.Hackathon.NetGenericHost.Options;

namespace Nsu.Sharps.Hackathon.Tests;

public class Fixture
{
    public IOptions<CalculationDataOptions> CreateCalculationDataOptions(int amountOfParticipantsInNumerator,
        int numeratorInCountingHarmony)
    {
        return Options.Create(new CalculationDataOptions
        {
            AmountOfParticipantsInNumerator = amountOfParticipantsInNumerator,
            NumeratorInCountingHarmony = numeratorInCountingHarmony
        });
    }

    public IOptions<AmountValuesOptions> CreateAmountValuesOptions(int amountOfParticipantsFromOneSide)
    {
        return Options.Create(new AmountValuesOptions
        {
            AmountOfParticipantsFromOneSide = amountOfParticipantsFromOneSide
        });
    }
}