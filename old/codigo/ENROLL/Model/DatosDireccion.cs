using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ENROLL.Model
{
	public class DatosDireccion : IValidatableObject
	{
		[Required(ErrorMessage="Este valor no puede estar vacío.")]
		public string cmbDireccionDpto
		{
			get;
			set;
		}

		[Required(ErrorMessage="Este valor no puede estar vacío.")]
		public string cmbDireccionProv
		{
			get;
			set;
		}

		[Required(ErrorMessage="Este valor no puede estar vacío.")]
		public string cmbTipoDireccion
		{
			get;
			set;
		}

		[Required(ErrorMessage="Este valor no puede estar vacío.")]
		public string txtCallePrincipal
		{
			get;
			set;
		}

		public DatosDireccion()
		{
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			return new List<ValidationResult>();
		}
	}
}