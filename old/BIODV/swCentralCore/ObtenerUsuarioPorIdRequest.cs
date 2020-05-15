using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;

namespace BIODV.swCentralCore
{
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[MessageContract(WrapperName="ObtenerUsuarioPorId", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
	public class ObtenerUsuarioPorIdRequest
	{
		[MessageBodyMember(Namespace="http://tempuri.org/", Order=0)]
		public string pMensajebd;

		[MessageBodyMember(Namespace="http://tempuri.org/", Order=1)]
		public int pIdusuario;

		public ObtenerUsuarioPorIdRequest()
		{
		}

		public ObtenerUsuarioPorIdRequest(string pMensajebd, int pIdusuario)
		{
			this.pMensajebd = pMensajebd;
			this.pIdusuario = pIdusuario;
		}
	}
}