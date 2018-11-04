using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GuestBook.Models.Validation
{
    public class RecordValidator
    {
        ICollection<ValidationResult> validationResults = new HashSet<ValidationResult>();

        private Record _record;

        private bool IsTextExists()
        {
            if (String.IsNullOrWhiteSpace(_record.Text))
            {
                validationResults.Add(new ValidationResult("Field Text can't be an empty!"));
                return false;
            }
            return true;
        }

        private bool IsAuthorExists()
        {
            if (String.IsNullOrWhiteSpace(_record.Author))
            {
                validationResults.Add(new ValidationResult("Field Author can't be an empty!"));
                return false;
            }
            return true;
        }

        private void IsAuthorLengthValid()
        {
            if (_record.Author.Length > 30)
            {
               validationResults.Add(new ValidationResult("Too long name. Its greater then 30 symbols."));
            }
        }

        private void IsTextLengthValid()
        {
            if (_record.Text.Length > 300)
            {
               validationResults.Add(new ValidationResult("Too long text. Its greater then 300 symbols."));
            }
        }

        public IEnumerable<ValidationResult> ValidateRecordModel(Record record)
        {
            _record = record;                        
            if (IsAuthorExists())
            {
                IsAuthorLengthValid();
            }
            if (IsTextExists())
            {
                IsTextLengthValid();
            }
            return validationResults;
        }
    }
}