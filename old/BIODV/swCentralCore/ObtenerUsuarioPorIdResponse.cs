using System;
using System.CodeDom.Compiler;
using System.Data;
using System.Diagnostics;
using System.ServiceModel;

namespace BIODV.swCentralCore
{
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[MessageContract(WrapperName="ObtenerUsuarioPorIdResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
	public class ObtenerUsuarioPorIdResponse
	{
		[MessageBodyMember(Namespace="http://tempuri.org/", Order=0)]
		public DataTable ObtenerUsuarioPorIdResult;

		[MessageBodyMember(Namespace="http://tempuri.org/", Order=1)]
		public string pMensajebd;

		public ObtenerUsuarioPorIdResponse()
		{
		}

		public ObtenerUsuarioPorIdResponse(DataTable ObtenerUsuarioPorIdResult, string pMensajebd)
		{
			this.ObtenerUsuarioPorIdResult = ObtenerUsuarioPorIdResult;
			this.pMensajebd = pMensajebd;
		}
	}
}