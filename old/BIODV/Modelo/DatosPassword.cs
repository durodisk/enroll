using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace BIODV.Modelo
{
	public class DatosPassword : IValidatableObject
	{
		[RegularExpression("(?=^.{8,}$)((?=.*\\d)|(?=.*\\W+))(?![.\\n])(?=.*[A-Z])(?=.*[a-z]).*$", ErrorMessage="Formato de contraseña incorrecto.")]
		[Required(ErrorMessage="Este valor no puede estar vacío.")]
		public string txtPass
		{
			get;
			set;
		}

		[Required(ErrorMessage="Este valor no puede estar vacío.")]
		public string txtPassOld
		{
			get;
			set;
		}

		[Required(ErrorMessage="Este valor no puede estar vacío.")]
		public string txtRepitePass
		{
			get;
			set;
		}

		public DatosPassword()
		{
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			List<ValidationResult> validationResults = new List<ValidationResult>();
			if (this.txtPass.Trim() != this.txtRepitePass.Trim())
			{
				validationResults.Add(new ValidationResult("No coinciden las contraseñas.", new List<string>()
				{
					"txtPass",
					"txtRepitePass"
				}));
			}
			return validationResults;
		}
	}
}