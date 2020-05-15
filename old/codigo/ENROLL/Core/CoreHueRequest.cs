using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;

namespace ENROLL.Core
{
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[MessageContract(WrapperName="ArchivosHue", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
	public class CoreHueRequest
	{
		[MessageBodyMember(Namespace="http://tempuri.org/", Order=0)]
		public string pMensajebd;

		[MessageBodyMember(Namespace="http://tempuri.org/", Order=1)]
		public string pPersonId;

		[MessageBodyMember(Namespace="http://tempuri.org/", Order=2)]
		public string pNombreFile;

		[MessageBodyMember(Namespace="http://tempuri.org/", Order=3)]
		public byte[] pFileHue;

		public CoreHueRequest()
		{
		}

		public CoreHueRequest(string pMensajebd, string pPersonId, string pNombreFile, byte[] pFileHue)
		{
			this.pMensajebd = pMensajebd;
			this.pPersonId = pPersonId;
			this.pNombreFile = pNombreFile;
			this.pFileHue = pFileHue;
		}
	}
}