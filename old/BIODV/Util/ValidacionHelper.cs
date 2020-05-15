using System;

namespace BIODV.Util
{
	internal class ValidacionHelper
	{
		public ValidacionHelper()
		{
		}

		public static ResultadoValidacionEntidad ValidarEntidad<T>(T pEntidad)
		where T : class
		{
			return (new EntityValidator<T>()).Validar(pEntidad, false);
		}
	}
}