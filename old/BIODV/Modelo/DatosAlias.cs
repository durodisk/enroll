using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace BIODV.Modelo
{
	public class DatosAlias : IValidatableObject
	{
		[Required(ErrorMessage="Este valor no puede estar vacío.")]
		public string txtAlias
		{
			get;
			set;
		}

		public DatosAlias()
		{
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			return new List<ValidationResult>();
		}
	}
}