using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;

namespace ENROLL.Core
{
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[MessageContract(WrapperName="ArchivosHueResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
	public class CoreHueResponse
	{
		[MessageBodyMember(Namespace="http://tempuri.org/", Order=0)]
		public string pMensajebd;

		public CoreHueResponse()
		{
		}

		public CoreHueResponse(string pMensajebd)
		{
			this.pMensajebd = pMensajebd;
		}
	}
}