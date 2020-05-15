using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;

namespace ENROLL.Core
{
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[MessageContract(WrapperName="ArchivosFac64Response", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
	public class CoreFac64Response
	{
		[MessageBodyMember(Namespace="http://tempuri.org/", Order=0)]
		public string pMensajebd;

		public CoreFac64Response()
		{
		}

		public CoreFac64Response(string pMensajebd)
		{
			this.pMensajebd = pMensajebd;
		}
	}
}