using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace BIODV.Modelo
{
	public class DatosGenerales : IValidatableObject
	{
		[Range(50, 2147483647, ErrorMessage="La estatura debe ser mayor o igual a 50.")]
		public decimal numEstatura
		{
			get;
			set;
		}

		public DatosGenerales()
		{
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			return new List<ValidationResult>();
		}
	}
}