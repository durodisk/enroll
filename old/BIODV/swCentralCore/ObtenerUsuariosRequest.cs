using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;

namespace BIODV.swCentralCore
{
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[MessageContract(WrapperName="ObtenerUsuarios", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
	public class ObtenerUsuariosRequest
	{
		[MessageBodyMember(Namespace="http://tempuri.org/", Order=0)]
		public string pMensajebd;

		public ObtenerUsuariosRequest()
		{
		}

		public ObtenerUsuariosRequest(string pMensajebd)
		{
			this.pMensajebd = pMensajebd;
		}
	}
}