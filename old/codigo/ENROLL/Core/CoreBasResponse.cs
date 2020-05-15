using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;

namespace ENROLL.Core
{
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[MessageContract(WrapperName="ArchivosBasResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
	public class CoreBasResponse
	{
		[MessageBodyMember(Namespace="http://tempuri.org/", Order=0)]
		public string pMensajebd;

		public CoreBasResponse()
		{
		}

		public CoreBasResponse(string pMensajebd)
		{
			this.pMensajebd = pMensajebd;
		}
	}
}