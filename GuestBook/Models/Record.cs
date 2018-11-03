using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GuestBook.Models
{
    public class Record:IValidatableObject
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string Author { get; set; }

        public DateTime RecordDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(String.IsNullOrWhiteSpace(Text))
            {
                yield return new ValidationResult("Field Text can't be an empty!");
            }
            if(String.IsNullOrEmpty(Author))
            {
                yield return new ValidationResult("Field Author can't be an empty!");
            }
        }
    }
}