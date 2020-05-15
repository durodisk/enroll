using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;

namespace BIODV.swCentralCore
{
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[MessageContract(WrapperName="ArchivosEpd", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
	public class ArchivosEpdRequest
	{
		[MessageBodyMember(Namespace="http://tempuri.org/", Order=0)]
		public string pMensajebd;

		[MessageBodyMember(Namespace="http://tempuri.org/", Order=1)]
		public string pPersonId;

		[MessageBodyMember(Namespace="http://tempuri.org/", Order=2)]
		public string pNombreFile;

		[MessageBodyMember(Namespace="http://tempuri.org/", Order=3)]
		public byte[] pFileEpd;

		public ArchivosEpdRequest()
		{
		}

		public ArchivosEpdRequest(string pMensajebd, string pPersonId, string pNombreFile, byte[] pFileEpd)
		{
			this.pMensajebd = pMensajebd;
			this.pPersonId = pPersonId;
			this.pNombreFile = pNombreFile;
			this.pFileEpd = pFileEpd;
		}
	}
}