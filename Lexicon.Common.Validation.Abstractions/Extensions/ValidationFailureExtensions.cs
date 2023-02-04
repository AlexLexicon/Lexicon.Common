using FluentValidation.Results;

namespace Lexicon.Common.Validation.Abstractions.Extensions;
public static class ValidationFailureExtensions
{
    //removes information that might be sensitive from the error messages
    public static IReadOnlyList<ValidationFailure> Sanitize(this IEnumerable<ValidationFailure> validationFailures)
    {
        ArgumentNullException.ThrowIfNull(validationFailures);

        var sanitizedFailures = new List<ValidationFailure>();
        foreach (ValidationFailure validationFailure in validationFailures)
        {
            string message = validationFailure.ErrorMessage;

            //remove the word must from the message
            int mustIndex = message.IndexOf("must", StringComparison.OrdinalIgnoreCase);
            if (mustIndex > 0)
            {
                message = message[mustIndex..];
            }

            const string MESSAGE_EMPTY_NAME_START = "'' ";
            if (message.Contains(MESSAGE_EMPTY_NAME_START))
            {
                message = message.Replace(MESSAGE_EMPTY_NAME_START, string.Empty);
            }

            const string MESSAGE_EMPTY_NAME_END = " ''";
            if (message.Contains(MESSAGE_EMPTY_NAME_END))
            {
                message = message.Replace(MESSAGE_EMPTY_NAME_END, string.Empty);
            }

            message = RemoveSpacesBetweenWords(message);

            if (!string.IsNullOrWhiteSpace(message))
            {
                //captialize the first letter of the message
                message = char.ToUpper(message[0]) + message[1..];

                var sanitizedFailure = new ValidationFailure(validationFailure.PropertyName, message);

                //we check for duplicates after parsing the error messages
                //because some messages will become the same after
                //based on the CommonLanguageManager
                bool alreadyExists = sanitizedFailures.Any(vf => vf.PropertyName == sanitizedFailure.PropertyName && vf.ErrorMessage == sanitizedFailure.ErrorMessage);
                if (!alreadyExists)
                {
                    sanitizedFailures.Add(sanitizedFailure);
                }
            }
        }

        return sanitizedFailures;
    }

    public static IReadOnlyList<string> ToFrontEndErrorMessages(this IEnumerable<ValidationFailure> validationFailures)
    {
        ArgumentNullException.ThrowIfNull(validationFailures);

        var frontEndErrorMessages = new List<string>();
        foreach (ValidationFailure validationFailure in validationFailures)
        {
            string errorMessage = validationFailure.ErrorMessage;

            string clearErrorMessage = string.Empty;
            bool isSkip = false;
            foreach (char character in errorMessage)
            {
                if (character == '\'')
                {
                    isSkip = !isSkip;
                }
                else if (!isSkip)
                {
                    clearErrorMessage += character;
                }
            }

            clearErrorMessage = RemoveSpacesBetweenWords(clearErrorMessage);

            frontEndErrorMessages.Add(clearErrorMessage);
        }

        return frontEndErrorMessages;
    }

    private static string RemoveSpacesBetweenWords(string text)
    {
        text = string.Join(" ", text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));

        text = text.Trim();

        return text;
    }
}
