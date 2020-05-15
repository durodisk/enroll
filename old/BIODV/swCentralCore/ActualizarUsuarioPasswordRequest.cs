using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;

namespace BIODV.swCentralCore
{
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[MessageContract(WrapperName="ActualizarUsuarioPassword", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
	public class ActualizarUsuarioPasswordRequest
	{
		[MessageBodyMember(Namespace="http://tempuri.org/", Order=0)]
		public string pMensajebd;

		[MessageBodyMember(Namespace="http://tempuri.org/", Order=1)]
		public string pUsuario;

		[MessageBodyMember(Namespace="http://tempuri.org/", Order=2)]
		public string pPassword;

		[MessageBodyMember(Namespace="http://tempuri.org/", Order=3)]
		public DateTime pCreated;

		[MessageBodyMember(Namespace="http://tempuri.org/", Order=4)]
		public string pCreatedBy;

		public ActualizarUsuarioPasswordRequest()
		{
		}

		public ActualizarUsuarioPasswordRequest(string pMensajebd, string pUsuario, string pPassword, DateTime pCreated, string pCreatedBy)
		{
			this.pMensajebd = pMensajebd;
			this.pUsuario = pUsuario;
			this.pPassword = pPassword;
			this.pCreated = pCreated;
			this.pCreatedBy = pCreatedBy;
		}
	}
}