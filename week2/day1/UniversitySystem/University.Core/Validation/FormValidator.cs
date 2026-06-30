namespace University.Core.Validation;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using University.Core.Exceptions;

public static class FormValidator
{
    public static void Validate(object form)
    {
        var context = new ValidationContext(form);
        var results = new List<ValidationResult>();

        if (!Validator.TryValidateObject(form, context, results, true))
        {
            var errorsDictionary = new Dictionary<string, List<string>>();

            foreach (var item in results)
            {
                var message = item.ErrorMessage ?? "Invalid field.";

                foreach (var member in item.MemberNames)
                {
                    if (!errorsDictionary.ContainsKey(member))
                    {
                        errorsDictionary.Add(member, new List<string>());
                    }
                    errorsDictionary[member].Add(message);
                }
            }

            throw new BusinessException(errorsDictionary);
        }
    }
}