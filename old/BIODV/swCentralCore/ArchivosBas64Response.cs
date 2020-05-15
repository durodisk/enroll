using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;

namespace BIODV.swCentralCore
{
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[MessageContract(WrapperName="ArchivosBas64Response", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
	public class ArchivosBas64Response
	{
		[MessageBodyMember(Namespace="http://tempuri.org/", Order=0)]
		public string pMensajebd;

		public ArchivosBas64Response()
		{
		}

		public ArchivosBas64Response(string pMensajebd)
		{
			this.pMensajebd = pMensajebd;
		}
	}
}