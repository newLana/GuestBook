using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuestBook.Models.Validation
{
    public static class RecordValidator
    {
        public static IEnumerable<ValidationResult> ValidateRecordModel(Record record)
        {
            if (String.IsNullOrWhiteSpace(record.Text))
            {
                yield return new ValidationResult("Field Text can't be an empty!");
            }
            if (String.IsNullOrEmpty(record.Author))
            {
                yield return new ValidationResult("Field Author can't be an empty!");
            }
            if(record.Author.Length > 30)
            {
                yield return new ValidationResult("Too long name. Its greater then 30 symbols.");
            }
            if(record.Text.Length > 300)
            {
                yield return new ValidationResult("Too long text. Its greater then 300 symbols.");
            }
        }
    }
}