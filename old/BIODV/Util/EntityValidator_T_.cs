using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BIODV.Util
{
	public class EntityValidator<T>
	where T : class
	{
		public EntityValidator()
		{
		}

		public ResultadoValidacionEntidad Validar(T pEntidad, bool pPropiedadCompleta = false)
		{
			List<ValidationResult> vColValidacion = new List<ValidationResult>();
			ValidationContext vContextoValidacion = new ValidationContext((object)pEntidad, null, null);
			Validator.TryValidateObject(pEntidad, vContextoValidacion, vColValidacion, true);
			return new ResultadoValidacionEntidad(vColValidacion, pPropiedadCompleta);
		}
	}
}