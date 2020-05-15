using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ENROLL.Helpers
{
    public class EntityValidator<T>
    where T : class
    {
        public EntityValidator()
        {
        }

        public HelperValidator Validar(T pEntidad, bool pPropiedadCompleta = false)
        {
            List<ValidationResult> vColValidacion = new List<ValidationResult>();
            ValidationContext vContextoValidacion = new ValidationContext((object)pEntidad, null, null);
            Validator.TryValidateObject(pEntidad, vContextoValidacion, vColValidacion, true);
            return new HelperValidator(vColValidacion, pPropiedadCompleta);
        }
    }
}