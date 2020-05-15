using System;

namespace ENROLL.Helpers
{
    internal class HelperValidacion
    {
        public HelperValidacion()
        {
        }

        public static HelperValidator ValidarEntidad<T>(T pEntidad)
        where T : class
        {
            return (new EntityValidator<T>()).Validar(pEntidad, false);
        }
    }
}