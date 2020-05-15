using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace BIODV.Modelo
{
	public class DatosIdentificativos : IValidatableObject
	{
		[Required(ErrorMessage="Este valor no puede estar vacío.")]
		public string cmbCausa
		{
			get;
			set;
		}

		[Required(ErrorMessage="Este valor no puede estar vacío.")]
		public string cmbComplexion
		{
			get;
			set;
		}

		[Required(ErrorMessage="Este valor no puede estar vacío.")]
		public string cmbMotivo
		{
			get;
			set;
		}

		[Required(ErrorMessage="Este valor no puede estar vacío.")]
		public string cmbNivelCultural
		{
			get;
			set;
		}

		[Required(ErrorMessage="Este valor no puede estar vacío.")]
		public string cmbPais
		{
			get;
			set;
		}

		[Required(ErrorMessage="Este valor no puede estar vacío.")]
		public string cmbSexo
		{
			get;
			set;
		}

		[Required(ErrorMessage="Este valor no puede estar vacío.")]
		public string cmbTipoPersona
		{
			get;
			set;
		}

		[Range(50, 2147483647, ErrorMessage="La estatura debe ser mayor o igual a 50.")]
		public decimal numEstatura
		{
			get;
			set;
		}

		[Range(40, 2147483647, ErrorMessage="El peso debe ser mayor o igual a 40.")]
		public decimal numPeso
		{
			get;
			set;
		}

		[Range(10, 2147483647, ErrorMessage="La dimensión del pie debe ser mayor o igual a 10.")]
		public decimal numPie
		{
			get;
			set;
		}

		[RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage="Formato incorrecto del documento.")]
		public string txtIdentificacion
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

		public DatosIdentificativos()
		{
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			return new List<ValidationResult>();
		}
	}
}