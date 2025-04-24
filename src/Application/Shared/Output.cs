using FluentValidation.Results;

namespace Application.Shared
{
    public class Output
    {
        private List<string> errorMessages = [];
        public IReadOnlyList<string> ErrorMessages => errorMessages;

        public object Result { get; private set; } = null;

        public bool IsValid => errorMessages.Count == 0;

        public bool IsInvalid => errorMessages.Count > 0;

        public bool IsEmptyResult => Result == null;

        public void AddMessageErrors(params string[] errors) => errorMessages.AddRange(errors);

        public void AddMessageErrors(ValidationResult validationResult)
        {
            foreach (var result in validationResult.Errors)
                errorMessages.Add(result.ErrorMessage);
        }

        public void AddResult(object result) => Result = result;
    }
}
