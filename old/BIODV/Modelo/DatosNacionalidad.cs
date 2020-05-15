using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace BIODV.Modelo
{
	public class DatosNacionalidad : IValidatableObject
	{
		[Required(ErrorMessage="Este valor no puede estar vacío.")]
		public string cmbNacionalidad
		{
			get;
			set;
		}

		public DatosNacionalidad()
		{
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			return new List<ValidationResult>();
		}
	}
}