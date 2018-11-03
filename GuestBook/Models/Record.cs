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
            return Validation.RecordValidator.ValidateRecordModel(this);        
        }
    }
}