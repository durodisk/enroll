using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;

namespace BIODV.swCentralCore
{
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[MessageContract(WrapperName="CrearUsuarioResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
	public class CrearUsuarioResponse
	{
		[MessageBodyMember(Namespace="http://tempuri.org/", Order=0)]
		public string pMensajebd;

		public CrearUsuarioResponse()
		{
		}

		public CrearUsuarioResponse(string pMensajebd)
		{
			this.pMensajebd = pMensajebd;
		}
	}
}