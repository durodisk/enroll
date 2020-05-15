using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ENROLL.Model
{
	public class DatosMarcas : IValidatableObject
	{
		[Required(ErrorMessage="Este valor no puede estar vacío.")]
		public string cmbAreaCuerpo
		{
			get;
			set;
		}

		[Required(ErrorMessage="Este valor no puede estar vacío.")]
		public string cmbTipoMarca
		{
			get;
			set;
		}

		public DatosMarcas()
		{
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			return new List<ValidationResult>();
		}
	}
}