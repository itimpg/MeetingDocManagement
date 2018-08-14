using System.Collections.Generic;

namespace MeetingDoc.Api.Validators
{
    public class ValidateResult
    {
        public ValidateResult()
        {
            ErrorMessages = new List<string>();
        }
        public IList<string> ErrorMessages { get; set; }
        public bool IsValid { get { return ErrorMessages.Count > 0; } }
    }
}