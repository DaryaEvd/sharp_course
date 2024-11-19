using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;
using Nsu.Sharps.Hackathon.NetGenericHost.Options;

namespace Nsu.Sharps.Hackathon.NetGenericHost.OptionsValidation;

public class DataPathOptionsValidation : IValidateOptions<DataPathOptions>
{
    private static readonly Regex FilePathRegex = new(@"^[\w\s\-\\/]+\.[\w\d]+$", RegexOptions.Compiled);

    public ValidateOptionsResult Validate(string name, DataPathOptions options)
    {
        if (!IsPathValid(options.PathForJuniors))
            return ValidateOptionsResult.Fail($"Invalid file path for juniors: '{options.PathForJuniors}'");

        if (!IsPathValid(options.PathForTeamLeads))
            return ValidateOptionsResult.Fail($"Invalid file path for team leads: '{options.PathForTeamLeads}'");

        return ValidateOptionsResult.Success;
    }

    private bool IsPathValid(string path)
    {
        return !string.IsNullOrWhiteSpace(path) &&
               path.IndexOfAny(Path.GetInvalidPathChars()) == -1 &&
               FilePathRegex.IsMatch(path);
    }
}