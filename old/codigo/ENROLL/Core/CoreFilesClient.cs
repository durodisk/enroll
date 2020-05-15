using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace ENROLL.Core
{
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	public class CoreFilesClient : ClientBase<CoreFiles>, CoreFiles
	{
		public CoreFilesClient()
		{
		}

		public CoreFilesClient(string endpointConfigurationName) : base(endpointConfigurationName)
		{
		}

		public CoreFilesClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		public CoreFilesClient(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		public CoreFilesClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
		{
		}

		public void ActualizarUsuarioPassword(ref string pMensajebd, string pUsuario, string pPassword, DateTime pCreated, string pCreatedBy)
		{
			CoreLoginRequest inValue = new CoreLoginRequest()
			{
				pMensajebd = pMensajebd,
				pUsuario = pUsuario,
				pPassword = pPassword,
				pCreated = pCreated,
				pCreatedBy = pCreatedBy
			};
			pMensajebd = ((CoreFiles)this).ActualizarUsuarioPassword(inValue).pMensajebd;
		}

		public Task<CorePasswordRequest> ActualizarUsuarioPasswordAsync(CoreLoginRequest request)
		{
			return base.Channel.ActualizarUsuarioPasswordAsync(request);
		}

		public void ArchivosBas(ref string pMensajebd, string pPersonId, string pNombreFile, byte[] pFileBas)
		{
			CoreBasRequest inValue = new CoreBasRequest()
			{
				pMensajebd = pMensajebd,
				pPersonId = pPersonId,
				pNombreFile = pNombreFile,
				pFileBas = pFileBas
			};
			pMensajebd = ((CoreFiles)this).ArchivosBas(inValue).pMensajebd;
		}

		public void ArchivosBas64(ref string pMensajebd, string pNombreFile, string pFileB64)
		{
			CoreBas64Request inValue = new CoreBas64Request()
			{
				pMensajebd = pMensajebd,
				pNombreFile = pNombreFile,
				pFileB64 = pFileB64
			};
			pMensajebd = ((CoreFiles)this).ArchivosBas64(inValue).pMensajebd;
		}

		public Task<CoreBas64Response> ArchivosBas64Async(CoreBas64Request request)
		{
			return base.Channel.ArchivosBas64Async(request);
		}

		public Task<CoreBasResponse> ArchivosBasAsync(CoreBasRequest request)
		{
			return base.Channel.ArchivosBasAsync(request);
		}

		public void ArchivosBio(ref string pMensajebd, string pPersonId, string pNombreFile, byte[] pFileBio)
		{
			CoreBioRequest inValue = new CoreBioRequest()
			{
				pMensajebd = pMensajebd,
				pPersonId = pPersonId,
				pNombreFile = pNombreFile,
				pFileBio = pFileBio
			};
			pMensajebd = ((CoreFiles)this).ArchivosBio(inValue).pMensajebd;
		}

		public void ArchivosBio64(ref string pMensajebd, string pNombreFile, string pFileB64)
		{
			CoreBio64Request inValue = new CoreBio64Request()
			{
				pMensajebd = pMensajebd,
				pNombreFile = pNombreFile,
				pFileB64 = pFileB64
			};
			pMensajebd = ((CoreFiles)this).ArchivosBio64(inValue).pMensajebd;
		}

		public Task<CoreBio64Response> ArchivosBio64Async(CoreBio64Request request)
		{
			return base.Channel.ArchivosBio64Async(request);
		}

		public Task<CoreBioResponse> ArchivosBioAsync(CoreBioRequest request)
		{
			return base.Channel.ArchivosBioAsync(request);
		}

		public void ArchivosEpd(ref string pMensajebd, string pPersonId, string pNombreFile, byte[] pFileEpd)
		{
			CoreEpdRequest inValue = new CoreEpdRequest()
			{
				pMensajebd = pMensajebd,
				pPersonId = pPersonId,
				pNombreFile = pNombreFile,
				pFileEpd = pFileEpd
			};
			pMensajebd = ((CoreFiles)this).ArchivosEpd(inValue).pMensajebd;
		}

		public Task<CoreEpdResponse> ArchivosEpdAsync(CoreEpdRequest request)
		{
			return base.Channel.ArchivosEpdAsync(request);
		}

		public void ArchivosFac(ref string pMensajebd, string pPersonId, string pNombreFile, byte[] pFileBio)
		{
			CoreFacRequest inValue = new CoreFacRequest()
			{
				pMensajebd = pMensajebd,
				pPersonId = pPersonId,
				pNombreFile = pNombreFile,
				pFileBio = pFileBio
			};
			pMensajebd = ((CoreFiles)this).ArchivosFac(inValue).pMensajebd;
		}

		public void ArchivosFac64(ref string pMensajebd, string pNombreFile, string pFileB64)
		{
			CoreFac64Request inValue = new CoreFac64Request()
			{
				pMensajebd = pMensajebd,
				pNombreFile = pNombreFile,
				pFileB64 = pFileB64
			};
			pMensajebd = ((CoreFiles)this).ArchivosFac64(inValue).pMensajebd;
		}

		public Task<CoreFac64Response> ArchivosFac64Async(CoreFac64Request request)
		{
			return base.Channel.ArchivosFac64Async(request);
		}

		public Task<CoreFacResponse> ArchivosFacAsync(CoreFacRequest request)
		{
			return base.Channel.ArchivosFacAsync(request);
		}

		public void ArchivosHue(ref string pMensajebd, string pPersonId, string pNombreFile, byte[] pFileHue)
		{
			CoreHueRequest inValue = new CoreHueRequest()
			{
				pMensajebd = pMensajebd,
				pPersonId = pPersonId,
				pNombreFile = pNombreFile,
				pFileHue = pFileHue
			};
			pMensajebd = ((CoreFiles)this).ArchivosHue(inValue).pMensajebd;
		}

		public void ArchivosHue64(ref string pMensajebd, string pNombreFile, string pFileB64)
		{
			CoreHue64Request inValue = new CoreHue64Request()
			{
				pMensajebd = pMensajebd,
				pNombreFile = pNombreFile,
				pFileB64 = pFileB64
			};
			pMensajebd = ((CoreFiles)this).ArchivosHue64(inValue).pMensajebd;
		}

		public Task<CoreHue64Response> ArchivosHue64Async(CoreHue64Request request)
		{
			return base.Channel.ArchivosHue64Async(request);
		}

		public Task<CoreHueResponse> ArchivosHueAsync(CoreHueRequest request)
		{
			return base.Channel.ArchivosHueAsync(request);
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		CorePasswordRequest ENROLL.Core.CoreFiles.ActualizarUsuarioPassword(CoreLoginRequest request)
		{
			return base.Channel.ActualizarUsuarioPassword(request);
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		CoreBasResponse ENROLL.Core.CoreFiles.ArchivosBas(CoreBasRequest request)
		{
			return base.Channel.ArchivosBas(request);
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		CoreBas64Response ENROLL.Core.CoreFiles.ArchivosBas64(CoreBas64Request request)
		{
			return base.Channel.ArchivosBas64(request);
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		CoreBioResponse ENROLL.Core.CoreFiles.ArchivosBio(CoreBioRequest request)
		{
			return base.Channel.ArchivosBio(request);
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		CoreBio64Response ENROLL.Core.CoreFiles.ArchivosBio64(CoreBio64Request request)
		{
			return base.Channel.ArchivosBio64(request);
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		CoreEpdResponse ENROLL.Core.CoreFiles.ArchivosEpd(CoreEpdRequest request)
		{
			return base.Channel.ArchivosEpd(request);
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		CoreFacResponse ENROLL.Core.CoreFiles.ArchivosFac(CoreFacRequest request)
		{
			return base.Channel.ArchivosFac(request);
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		CoreFac64Response ENROLL.Core.CoreFiles.ArchivosFac64(CoreFac64Request request)
		{
			return base.Channel.ArchivosFac64(request);
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		CoreHueResponse ENROLL.Core.CoreFiles.ArchivosHue(CoreHueRequest request)
		{
			return base.Channel.ArchivosHue(request);
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		CoreHue64Response ENROLL.Core.CoreFiles.ArchivosHue64(CoreHue64Request request)
		{
			return base.Channel.ArchivosHue64(request);
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		CoreCreateUserResponse ENROLL.Core.CoreFiles.CrearUsuario(CoreCreateUserRequest request)
		{
			return base.Channel.CrearUsuario(request);
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		CoreEditUserResponse ENROLL.Core.CoreFiles.ModificarUsuario(CoreEditUserRequest request)
		{
			return base.Channel.ModificarUsuario(request);
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		CoreGetUserByIdResponse ENROLL.Core.CoreFiles.ObtenerUsuarioPorId(CoreGetUseryByIdRequest request)
		{
			return base.Channel.ObtenerUsuarioPorId(request);
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		CoreGetUsersResponse ENROLL.Core.CoreFiles.ObtenerUsuarios(CoreGetUsersRequest request)
		{
			return base.Channel.ObtenerUsuarios(request);
		}

		public void CrearUsuario(ref string pMensajebd, string pNumeroDocumento, string pComplemento, string pPrimerNombre, string pSegundoNombre, string pPrimerApellido, string pSegundoApellido, string pUsuario, string pPassword, string pUnidad, DateTime pCreated, string pCreatedBy)
		{
			CoreCreateUserRequest inValue = new CoreCreateUserRequest()
			{
				pMensajebd = pMensajebd,
				pNumeroDocumento = pNumeroDocumento,
				pComplemento = pComplemento,
				pPrimerNombre = pPrimerNombre,
				pSegundoNombre = pSegundoNombre,
				pPrimerApellido = pPrimerApellido,
				pSegundoApellido = pSegundoApellido,
				pUsuario = pUsuario,
				pPassword = pPassword,
				pUnidad = pUnidad,
				pCreated = pCreated,
				pCreatedBy = pCreatedBy
			};
			pMensajebd = ((CoreFiles)this).CrearUsuario(inValue).pMensajebd;
		}

		public Task<CoreCreateUserResponse> CrearUsuarioAsync(CoreCreateUserRequest request)
		{
			return base.Channel.CrearUsuarioAsync(request);
		}

		public void ModificarUsuario(ref string pMensajebd, int pIdUsuario, string pNumeroDocumento, string pComplemento, string pPrimerNombre, string pSegundoNombre, string pPrimerApellido, string pSegundoApellido, string pUsuario, string pPassword, string pUnidad, DateTime pCreated, string pCreatedBy)
		{
			CoreEditUserRequest inValue = new CoreEditUserRequest()
			{
				pMensajebd = pMensajebd,
				pIdUsuario = pIdUsuario,
				pNumeroDocumento = pNumeroDocumento,
				pComplemento = pComplemento,
				pPrimerNombre = pPrimerNombre,
				pSegundoNombre = pSegundoNombre,
				pPrimerApellido = pPrimerApellido,
				pSegundoApellido = pSegundoApellido,
				pUsuario = pUsuario,
				pPassword = pPassword,
				pUnidad = pUnidad,
				pCreated = pCreated,
				pCreatedBy = pCreatedBy
			};
			pMensajebd = ((CoreFiles)this).ModificarUsuario(inValue).pMensajebd;
		}

		public Task<CoreEditUserResponse> ModificarUsuarioAsync(CoreEditUserRequest request)
		{
			return base.Channel.ModificarUsuarioAsync(request);
		}

		public DataTable ObtenerUsuarioPorId(ref string pMensajebd, int pIdusuario)
		{
			CoreGetUseryByIdRequest inValue = new CoreGetUseryByIdRequest()
			{
				pMensajebd = pMensajebd,
				pIdusuario = pIdusuario
			};
			CoreGetUserByIdResponse retVal = ((CoreFiles)this).ObtenerUsuarioPorId(inValue);
			pMensajebd = retVal.pMensajebd;
			return retVal.ObtenerUsuarioPorIdResult;
		}

		public Task<CoreGetUserByIdResponse> ObtenerUsuarioPorIdAsync(CoreGetUseryByIdRequest request)
		{
			return base.Channel.ObtenerUsuarioPorIdAsync(request);
		}

		public DataTable ObtenerUsuarios(ref string pMensajebd)
		{
			CoreGetUsersRequest inValue = new CoreGetUsersRequest()
			{
				pMensajebd = pMensajebd
			};
			CoreGetUsersResponse retVal = ((CoreFiles)this).ObtenerUsuarios(inValue);
			pMensajebd = retVal.pMensajebd;
			return retVal.ObtenerUsuariosResult;
		}

		public Task<CoreGetUsersResponse> ObtenerUsuariosAsync(CoreGetUsersRequest request)
		{
			return base.Channel.ObtenerUsuariosAsync(request);
		}
	}
}