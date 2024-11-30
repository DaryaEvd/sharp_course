using Microsoft.Extensions.Options;
using Nsu.Sharps.Hackathon.NetGenericHost.Options;

namespace Nsu.Sharps.Hackathon.NetGenericHost.OptionsValidation;

public class AmountValuesOptionsValidation : IValidateOptions<AmountValuesOptions>
{
    public ValidateOptionsResult Validate(string name, AmountValuesOptions options)
    {
        if (options.AmountOfParticipantsFromOneSide < 0)
            return ValidateOptionsResult.Fail("Amount of Participants (teamleads/juniors) can't be negative");

        if (options.AmountOfHackathons < 0)
            return ValidateOptionsResult.Fail("Amount of Hackathons can't be negative");

        return ValidateOptionsResult.Success;
    }
}