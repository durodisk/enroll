using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;

namespace BIODV.swCentralCore
{
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[MessageContract(WrapperName="ArchivosHue64Response", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
	public class ArchivosHue64Response
	{
		[MessageBodyMember(Namespace="http://tempuri.org/", Order=0)]
		public string pMensajebd;

		public ArchivosHue64Response()
		{
		}

		public ArchivosHue64Response(string pMensajebd)
		{
			this.pMensajebd = pMensajebd;
		}
	}
}