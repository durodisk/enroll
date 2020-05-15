using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;

namespace ENROLL.Core
{
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[MessageContract(WrapperName="ActualizarUsuarioPassword", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
	public class CoreLoginRequest
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

		public CoreLoginRequest()
		{
		}

		public CoreLoginRequest(string pMensajebd, string pUsuario, string pPassword, DateTime pCreated, string pCreatedBy)
		{
			this.pMensajebd = pMensajebd;
			this.pUsuario = pUsuario;
			this.pPassword = pPassword;
			this.pCreated = pCreated;
			this.pCreatedBy = pCreatedBy;
		}
	}
}