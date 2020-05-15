using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;

namespace ENROLL.Core
{
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[MessageContract(WrapperName="ArchivosHue64Response", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
	public class CoreHue64Response
	{
		[MessageBodyMember(Namespace="http://tempuri.org/", Order=0)]
		public string pMensajebd;

		public CoreHue64Response()
		{
		}

		public CoreHue64Response(string pMensajebd)
		{
			this.pMensajebd = pMensajebd;
		}
	}
}