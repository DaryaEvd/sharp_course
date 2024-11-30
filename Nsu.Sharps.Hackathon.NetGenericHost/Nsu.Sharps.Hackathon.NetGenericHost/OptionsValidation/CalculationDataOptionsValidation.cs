using Microsoft.Extensions.Options;
using Nsu.Sharps.Hackathon.NetGenericHost.Options;

namespace Nsu.Sharps.Hackathon.NetGenericHost.OptionsValidation;

public class CalculationDataOptionsValidation : IValidateOptions<CalculationDataOptions>
{
    public ValidateOptionsResult Validate(string name, CalculationDataOptions options)
    {
        if (options.NumeratorInCountingHarmony < 0)
            return ValidateOptionsResult.Fail("NumeratorInCountingHarmony must be greater than 0");

        if (options.AmountOfParticipantsInNumerator < 0)
            return ValidateOptionsResult.Fail("AmountValuesOptions must be greater than 0");

        return ValidateOptionsResult.Success;
    }
}