using System;
using System.CodeDom.Compiler;
using System.Data;
using System.Diagnostics;
using System.ServiceModel;

namespace ENROLL.Core
{
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[MessageContract(WrapperName="ObtenerUsuarioPorIdResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
	public class CoreGetUserByIdResponse
	{
		[MessageBodyMember(Namespace="http://tempuri.org/", Order=0)]
		public DataTable ObtenerUsuarioPorIdResult;

		[MessageBodyMember(Namespace="http://tempuri.org/", Order=1)]
		public string pMensajebd;

		public CoreGetUserByIdResponse()
		{
		}

		public CoreGetUserByIdResponse(DataTable ObtenerUsuarioPorIdResult, string pMensajebd)
		{
			this.ObtenerUsuarioPorIdResult = ObtenerUsuarioPorIdResult;
			this.pMensajebd = pMensajebd;
		}
	}
}