using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace BIODV.Modelo
{
	public class DatosUsuario : IValidatableObject
	{
		public bool chkCambioPass
		{
			get;
			set;
		}

		[Required(ErrorMessage="Este valor no puede estar vacío.")]
		public string cmbUnidadPol
		{
			get;
			set;
		}

		[RegularExpression("(?=^.{8,}$)((?=.*\\d)|(?=.*\\W+))(?![.\\n])(?=.*[A-Z])(?=.*[a-z]).*$", ErrorMessage="Formato de contraseña incorrecto.")]
		[Required(ErrorMessage="Este valor no puede estar vacío.")]
		public string txtPass
		{
			get;
			set;
		}

		public string txtPrimerApellido
		{
			get;
			set;
		}

		[Required(ErrorMessage="Este valor no puede estar vacío.")]
		public string txtPrimerNombre
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

		public string txtSegundoApellido
		{
			get;
			set;
		}

		public string txtSegundoNombre
		{
			get;
			set;
		}

		public string txtUsuario
		{
			get;
			set;
		}

		public DatosUsuario()
		{
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			List<ValidationResult> validationResults = new List<ValidationResult>();
			if ((!string.IsNullOrWhiteSpace(this.txtPrimerApellido) ? false : string.IsNullOrWhiteSpace(this.txtSegundoApellido)))
			{
				validationResults.Add(new ValidationResult("Debe ingresar primer o segundo apellido.", new List<string>()
				{
					"txtPrimerApellido",
					"txtSegundoApellido"
				}));
			}
			if (this.chkCambioPass && this.txtPass.Trim() != this.txtRepitePass.Trim())
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