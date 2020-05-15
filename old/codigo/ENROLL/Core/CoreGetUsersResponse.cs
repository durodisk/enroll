using System;
using System.CodeDom.Compiler;
using System.Data;
using System.Diagnostics;
using System.ServiceModel;

namespace ENROLL.Core
{
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[MessageContract(WrapperName="ObtenerUsuariosResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
	public class CoreGetUsersResponse
	{
		[MessageBodyMember(Namespace="http://tempuri.org/", Order=0)]
		public DataTable ObtenerUsuariosResult;

		[MessageBodyMember(Namespace="http://tempuri.org/", Order=1)]
		public string pMensajebd;

		public CoreGetUsersResponse()
		{
		}

		public CoreGetUsersResponse(DataTable ObtenerUsuariosResult, string pMensajebd)
		{
			this.ObtenerUsuariosResult = ObtenerUsuariosResult;
			this.pMensajebd = pMensajebd;
		}
	}
}