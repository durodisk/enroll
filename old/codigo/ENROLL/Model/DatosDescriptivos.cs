using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ENROLL.Model
{
	public class DatosDescriptivos : IValidatableObject
	{
		[Required(ErrorMessage="Este valor no puede estar vacío.")]
		public string cmbBocaDimension
		{
			get;
			set;
		}

		[Required(ErrorMessage="Este valor no puede estar vacío.")]
		public string cmbBocaTipo
		{
			get;
			set;
		}

		[Required(ErrorMessage="Este valor no puede estar vacío.")]
		public string cmbCabezaForma
		{
			get;
			set;
		}

		[Required(ErrorMessage="Este valor no puede estar vacío.")]
		public string cmbCejasForma
		{
			get;
			set;
		}

		[Required(ErrorMessage="Este valor no puede estar vacío.")]
		public string cmbCejasPoblacion
		{
			get;
			set;
		}

		[Required(ErrorMessage="Este valor no puede estar vacío.")]
		public string cmbCuelloAlto
		{
			get;
			set;
		}

		[Required(ErrorMessage="Este valor no puede estar vacío.")]
		public string cmbCuelloAncho
		{
			get;
			set;
		}

		[Required(ErrorMessage="Este valor no puede estar vacío.")]
		public string cmbFacialesBarbilla
		{
			get;
			set;
		}

		[Required(ErrorMessage="Este valor no puede estar vacío.")]
		public string cmbFacialesMejillas
		{
			get;
			set;
		}

		[Required(ErrorMessage="Este valor no puede estar vacío.")]
		public string cmbFacialesSurcoNasal
		{
			get;
			set;
		}

		[Required(ErrorMessage="Este valor no puede estar vacío.")]
		public string cmbNarizAncho
		{
			get;
			set;
		}

		[Required(ErrorMessage="Este valor no puede estar vacío.")]
		public string cmbNarizPunta
		{
			get;
			set;
		}

		[Required(ErrorMessage="Este valor no puede estar vacío.")]
		public string cmbOjosColor
		{
			get;
			set;
		}

		[Required(ErrorMessage="Este valor no puede estar vacío.")]
		public string cmbOjosForma
		{
			get;
			set;
		}

		[Required(ErrorMessage="Este valor no puede estar vacío.")]
		public string cmbPeloColor
		{
			get;
			set;
		}

		[Required(ErrorMessage="Este valor no puede estar vacío.")]
		public string cmbPeloTipo
		{
			get;
			set;
		}

		[Required(ErrorMessage="Este valor no puede estar vacío.")]
		public string cmbRostro
		{
			get;
			set;
		}

		[Required(ErrorMessage="Este valor no puede estar vacío.")]
		public string cmbRostroFrente
		{
			get;
			set;
		}

		public DatosDescriptivos()
		{
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			return new List<ValidationResult>();
		}
	}
}