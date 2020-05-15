using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;

namespace BIODV.swCentralCore
{
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[MessageContract(WrapperName="ModificarUsuario", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
	public class ModificarUsuarioRequest
	{
		[MessageBodyMember(Namespace="http://tempuri.org/", Order=0)]
		public string pMensajebd;

		[MessageBodyMember(Namespace="http://tempuri.org/", Order=1)]
		public int pIdUsuario;

		[MessageBodyMember(Namespace="http://tempuri.org/", Order=2)]
		public string pNumeroDocumento;

		[MessageBodyMember(Namespace="http://tempuri.org/", Order=3)]
		public string pComplemento;

		[MessageBodyMember(Namespace="http://tempuri.org/", Order=4)]
		public string pPrimerNombre;

		[MessageBodyMember(Namespace="http://tempuri.org/", Order=5)]
		public string pSegundoNombre;

		[MessageBodyMember(Namespace="http://tempuri.org/", Order=6)]
		public string pPrimerApellido;

		[MessageBodyMember(Namespace="http://tempuri.org/", Order=7)]
		public string pSegundoApellido;

		[MessageBodyMember(Namespace="http://tempuri.org/", Order=8)]
		public string pUsuario;

		[MessageBodyMember(Namespace="http://tempuri.org/", Order=9)]
		public string pPassword;

		[MessageBodyMember(Namespace="http://tempuri.org/", Order=10)]
		public string pUnidad;

		[MessageBodyMember(Namespace="http://tempuri.org/", Order=11)]
		public DateTime pCreated;

		[MessageBodyMember(Namespace="http://tempuri.org/", Order=12)]
		public string pCreatedBy;

		public ModificarUsuarioRequest()
		{
		}

		public ModificarUsuarioRequest(string pMensajebd, int pIdUsuario, string pNumeroDocumento, string pComplemento, string pPrimerNombre, string pSegundoNombre, string pPrimerApellido, string pSegundoApellido, string pUsuario, string pPassword, string pUnidad, DateTime pCreated, string pCreatedBy)
		{
			this.pMensajebd = pMensajebd;
			this.pIdUsuario = pIdUsuario;
			this.pNumeroDocumento = pNumeroDocumento;
			this.pComplemento = pComplemento;
			this.pPrimerNombre = pPrimerNombre;
			this.pSegundoNombre = pSegundoNombre;
			this.pPrimerApellido = pPrimerApellido;
			this.pSegundoApellido = pSegundoApellido;
			this.pUsuario = pUsuario;
			this.pPassword = pPassword;
			this.pUnidad = pUnidad;
			this.pCreated = pCreated;
			this.pCreatedBy = pCreatedBy;
		}
	}
}