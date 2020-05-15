using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ENROLL.Model
{
    public class ModelAlias : IValidatableObject
    {
        [Required(ErrorMessage = "Este valor no puede estar vacío.")]
        public string txtAlias
        {
            get;
            set;
        }

        public ModelAlias()
        {
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}