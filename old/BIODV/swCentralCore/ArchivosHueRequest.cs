using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;

namespace BIODV.swCentralCore
{
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[MessageContract(WrapperName="ArchivosHue", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
	public class ArchivosHueRequest
	{
		[MessageBodyMember(Namespace="http://tempuri.org/", Order=0)]
		public string pMensajebd;

		[MessageBodyMember(Namespace="http://tempuri.org/", Order=1)]
		public string pPersonId;

		[MessageBodyMember(Namespace="http://tempuri.org/", Order=2)]
		public string pNombreFile;

		[MessageBodyMember(Namespace="http://tempuri.org/", Order=3)]
		public byte[] pFileHue;

		public ArchivosHueRequest()
		{
		}

		public ArchivosHueRequest(string pMensajebd, string pPersonId, string pNombreFile, byte[] pFileHue)
		{
			this.pMensajebd = pMensajebd;
			this.pPersonId = pPersonId;
			this.pNombreFile = pNombreFile;
			this.pFileHue = pFileHue;
		}
	}
}