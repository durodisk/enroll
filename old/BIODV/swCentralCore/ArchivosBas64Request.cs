using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;

namespace BIODV.swCentralCore
{
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[MessageContract(WrapperName="ArchivosBas64", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
	public class ArchivosBas64Request
	{
		[MessageBodyMember(Namespace="http://tempuri.org/", Order=0)]
		public string pMensajebd;

		[MessageBodyMember(Namespace="http://tempuri.org/", Order=1)]
		public string pNombreFile;

		[MessageBodyMember(Namespace="http://tempuri.org/", Order=2)]
		public string pFileB64;

		public ArchivosBas64Request()
		{
		}

		public ArchivosBas64Request(string pMensajebd, string pNombreFile, string pFileB64)
		{
			this.pMensajebd = pMensajebd;
			this.pNombreFile = pNombreFile;
			this.pFileB64 = pFileB64;
		}
	}
}