using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace BIODV.swCentralCore
{
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	public class IswArchivosClient : ClientBase<IswArchivos>, IswArchivos
	{
		public IswArchivosClient()
		{
		}

		public IswArchivosClient(string endpointConfigurationName) : base(endpointConfigurationName)
		{
		}

		public IswArchivosClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		public IswArchivosClient(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		public IswArchivosClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
		{
		}

		public void ActualizarUsuarioPassword(ref string pMensajebd, string pUsuario, string pPassword, DateTime pCreated, string pCreatedBy)
		{
			ActualizarUsuarioPasswordRequest inValue = new ActualizarUsuarioPasswordRequest()
			{
				pMensajebd = pMensajebd,
				pUsuario = pUsuario,
				pPassword = pPassword,
				pCreated = pCreated,
				pCreatedBy = pCreatedBy
			};
			pMensajebd = ((IswArchivos)this).ActualizarUsuarioPassword(inValue).pMensajebd;
		}

		public Task<ActualizarUsuarioPasswordResponse> ActualizarUsuarioPasswordAsync(ActualizarUsuarioPasswordRequest request)
		{
			return base.Channel.ActualizarUsuarioPasswordAsync(request);
		}

		public void ArchivosBas(ref string pMensajebd, string pPersonId, string pNombreFile, byte[] pFileBas)
		{
			ArchivosBasRequest inValue = new ArchivosBasRequest()
			{
				pMensajebd = pMensajebd,
				pPersonId = pPersonId,
				pNombreFile = pNombreFile,
				pFileBas = pFileBas
			};
			pMensajebd = ((IswArchivos)this).ArchivosBas(inValue).pMensajebd;
		}

		public void ArchivosBas64(ref string pMensajebd, string pNombreFile, string pFileB64)
		{
			ArchivosBas64Request inValue = new ArchivosBas64Request()
			{
				pMensajebd = pMensajebd,
				pNombreFile = pNombreFile,
				pFileB64 = pFileB64
			};
			pMensajebd = ((IswArchivos)this).ArchivosBas64(inValue).pMensajebd;
		}

		public Task<ArchivosBas64Response> ArchivosBas64Async(ArchivosBas64Request request)
		{
			return base.Channel.ArchivosBas64Async(request);
		}

		public Task<ArchivosBasResponse> ArchivosBasAsync(ArchivosBasRequest request)
		{
			return base.Channel.ArchivosBasAsync(request);
		}

		public void ArchivosBio(ref string pMensajebd, string pPersonId, string pNombreFile, byte[] pFileBio)
		{
			ArchivosBioRequest inValue = new ArchivosBioRequest()
			{
				pMensajebd = pMensajebd,
				pPersonId = pPersonId,
				pNombreFile = pNombreFile,
				pFileBio = pFileBio
			};
			pMensajebd = ((IswArchivos)this).ArchivosBio(inValue).pMensajebd;
		}

		public void ArchivosBio64(ref string pMensajebd, string pNombreFile, string pFileB64)
		{
			ArchivosBio64Request inValue = new ArchivosBio64Request()
			{
				pMensajebd = pMensajebd,
				pNombreFile = pNombreFile,
				pFileB64 = pFileB64
			};
			pMensajebd = ((IswArchivos)this).ArchivosBio64(inValue).pMensajebd;
		}

		public Task<ArchivosBio64Response> ArchivosBio64Async(ArchivosBio64Request request)
		{
			return base.Channel.ArchivosBio64Async(request);
		}

		public Task<ArchivosBioResponse> ArchivosBioAsync(ArchivosBioRequest request)
		{
			return base.Channel.ArchivosBioAsync(request);
		}

		public void ArchivosEpd(ref string pMensajebd, string pPersonId, string pNombreFile, byte[] pFileEpd)
		{
			ArchivosEpdRequest inValue = new ArchivosEpdRequest()
			{
				pMensajebd = pMensajebd,
				pPersonId = pPersonId,
				pNombreFile = pNombreFile,
				pFileEpd = pFileEpd
			};
			pMensajebd = ((IswArchivos)this).ArchivosEpd(inValue).pMensajebd;
		}

		public Task<ArchivosEpdResponse> ArchivosEpdAsync(ArchivosEpdRequest request)
		{
			return base.Channel.ArchivosEpdAsync(request);
		}

		public void ArchivosFac(ref string pMensajebd, string pPersonId, string pNombreFile, byte[] pFileBio)
		{
			ArchivosFacRequest inValue = new ArchivosFacRequest()
			{
				pMensajebd = pMensajebd,
				pPersonId = pPersonId,
				pNombreFile = pNombreFile,
				pFileBio = pFileBio
			};
			pMensajebd = ((IswArchivos)this).ArchivosFac(inValue).pMensajebd;
		}

		public void ArchivosFac64(ref string pMensajebd, string pNombreFile, string pFileB64)
		{
			ArchivosFac64Request inValue = new ArchivosFac64Request()
			{
				pMensajebd = pMensajebd,
				pNombreFile = pNombreFile,
				pFileB64 = pFileB64
			};
			pMensajebd = ((IswArchivos)this).ArchivosFac64(inValue).pMensajebd;
		}

		public Task<ArchivosFac64Response> ArchivosFac64Async(ArchivosFac64Request request)
		{
			return base.Channel.ArchivosFac64Async(request);
		}

		public Task<ArchivosFacResponse> ArchivosFacAsync(ArchivosFacRequest request)
		{
			return base.Channel.ArchivosFacAsync(request);
		}

		public void ArchivosHue(ref string pMensajebd, string pPersonId, string pNombreFile, byte[] pFileHue)
		{
			ArchivosHueRequest inValue = new ArchivosHueRequest()
			{
				pMensajebd = pMensajebd,
				pPersonId = pPersonId,
				pNombreFile = pNombreFile,
				pFileHue = pFileHue
			};
			pMensajebd = ((IswArchivos)this).ArchivosHue(inValue).pMensajebd;
		}

		public void ArchivosHue64(ref string pMensajebd, string pNombreFile, string pFileB64)
		{
			ArchivosHue64Request inValue = new ArchivosHue64Request()
			{
				pMensajebd = pMensajebd,
				pNombreFile = pNombreFile,
				pFileB64 = pFileB64
			};
			pMensajebd = ((IswArchivos)this).ArchivosHue64(inValue).pMensajebd;
		}

		public Task<ArchivosHue64Response> ArchivosHue64Async(ArchivosHue64Request request)
		{
			return base.Channel.ArchivosHue64Async(request);
		}

		public Task<ArchivosHueResponse> ArchivosHueAsync(ArchivosHueRequest request)
		{
			return base.Channel.ArchivosHueAsync(request);
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		ActualizarUsuarioPasswordResponse BIODV.swCentralCore.IswArchivos.ActualizarUsuarioPassword(ActualizarUsuarioPasswordRequest request)
		{
			return base.Channel.ActualizarUsuarioPassword(request);
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		ArchivosBasResponse BIODV.swCentralCore.IswArchivos.ArchivosBas(ArchivosBasRequest request)
		{
			return base.Channel.ArchivosBas(request);
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		ArchivosBas64Response BIODV.swCentralCore.IswArchivos.ArchivosBas64(ArchivosBas64Request request)
		{
			return base.Channel.ArchivosBas64(request);
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		ArchivosBioResponse BIODV.swCentralCore.IswArchivos.ArchivosBio(ArchivosBioRequest request)
		{
			return base.Channel.ArchivosBio(request);
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		ArchivosBio64Response BIODV.swCentralCore.IswArchivos.ArchivosBio64(ArchivosBio64Request request)
		{
			return base.Channel.ArchivosBio64(request);
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		ArchivosEpdResponse BIODV.swCentralCore.IswArchivos.ArchivosEpd(ArchivosEpdRequest request)
		{
			return base.Channel.ArchivosEpd(request);
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		ArchivosFacResponse BIODV.swCentralCore.IswArchivos.ArchivosFac(ArchivosFacRequest request)
		{
			return base.Channel.ArchivosFac(request);
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		ArchivosFac64Response BIODV.swCentralCore.IswArchivos.ArchivosFac64(ArchivosFac64Request request)
		{
			return base.Channel.ArchivosFac64(request);
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		ArchivosHueResponse BIODV.swCentralCore.IswArchivos.ArchivosHue(ArchivosHueRequest request)
		{
			return base.Channel.ArchivosHue(request);
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		ArchivosHue64Response BIODV.swCentralCore.IswArchivos.ArchivosHue64(ArchivosHue64Request request)
		{
			return base.Channel.ArchivosHue64(request);
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		CrearUsuarioResponse BIODV.swCentralCore.IswArchivos.CrearUsuario(CrearUsuarioRequest request)
		{
			return base.Channel.CrearUsuario(request);
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		ModificarUsuarioResponse BIODV.swCentralCore.IswArchivos.ModificarUsuario(ModificarUsuarioRequest request)
		{
			return base.Channel.ModificarUsuario(request);
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		ObtenerUsuarioPorIdResponse BIODV.swCentralCore.IswArchivos.ObtenerUsuarioPorId(ObtenerUsuarioPorIdRequest request)
		{
			return base.Channel.ObtenerUsuarioPorId(request);
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		ObtenerUsuariosResponse BIODV.swCentralCore.IswArchivos.ObtenerUsuarios(ObtenerUsuariosRequest request)
		{
			return base.Channel.ObtenerUsuarios(request);
		}

		public void CrearUsuario(ref string pMensajebd, string pNumeroDocumento, string pComplemento, string pPrimerNombre, string pSegundoNombre, string pPrimerApellido, string pSegundoApellido, string pUsuario, string pPassword, string pUnidad, DateTime pCreated, string pCreatedBy)
		{
			CrearUsuarioRequest inValue = new CrearUsuarioRequest()
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
			pMensajebd = ((IswArchivos)this).CrearUsuario(inValue).pMensajebd;
		}

		public Task<CrearUsuarioResponse> CrearUsuarioAsync(CrearUsuarioRequest request)
		{
			return base.Channel.CrearUsuarioAsync(request);
		}

		public void ModificarUsuario(ref string pMensajebd, int pIdUsuario, string pNumeroDocumento, string pComplemento, string pPrimerNombre, string pSegundoNombre, string pPrimerApellido, string pSegundoApellido, string pUsuario, string pPassword, string pUnidad, DateTime pCreated, string pCreatedBy)
		{
			ModificarUsuarioRequest inValue = new ModificarUsuarioRequest()
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
			pMensajebd = ((IswArchivos)this).ModificarUsuario(inValue).pMensajebd;
		}

		public Task<ModificarUsuarioResponse> ModificarUsuarioAsync(ModificarUsuarioRequest request)
		{
			return base.Channel.ModificarUsuarioAsync(request);
		}

		public DataTable ObtenerUsuarioPorId(ref string pMensajebd, int pIdusuario)
		{
			ObtenerUsuarioPorIdRequest inValue = new ObtenerUsuarioPorIdRequest()
			{
				pMensajebd = pMensajebd,
				pIdusuario = pIdusuario
			};
			ObtenerUsuarioPorIdResponse retVal = ((IswArchivos)this).ObtenerUsuarioPorId(inValue);
			pMensajebd = retVal.pMensajebd;
			return retVal.ObtenerUsuarioPorIdResult;
		}

		public Task<ObtenerUsuarioPorIdResponse> ObtenerUsuarioPorIdAsync(ObtenerUsuarioPorIdRequest request)
		{
			return base.Channel.ObtenerUsuarioPorIdAsync(request);
		}

		public DataTable ObtenerUsuarios(ref string pMensajebd)
		{
			ObtenerUsuariosRequest inValue = new ObtenerUsuariosRequest()
			{
				pMensajebd = pMensajebd
			};
			ObtenerUsuariosResponse retVal = ((IswArchivos)this).ObtenerUsuarios(inValue);
			pMensajebd = retVal.pMensajebd;
			return retVal.ObtenerUsuariosResult;
		}

		public Task<ObtenerUsuariosResponse> ObtenerUsuariosAsync(ObtenerUsuariosRequest request)
		{
			return base.Channel.ObtenerUsuariosAsync(request);
		}
	}
}