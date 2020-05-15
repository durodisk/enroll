using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ENROLL.Model
{
	public class DatosLogin : IValidatableObject
	{
		[Required(ErrorMessage="Debe ingresar la contraseña del usuario.")]
		public string txtContrasenia
		{
			get;
			set;
		}

		[Required(ErrorMessage="Debe ingresar el nombre del usuario.")]
		public string txtUsuario
		{
			get;
			set;
		}

		public DatosLogin()
		{
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			return new List<ValidationResult>();
		}
	}
}