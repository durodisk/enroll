using System.CodeDom.Compiler;
using System.ServiceModel;
using System.Threading.Tasks;

namespace BIODV.swCentralCore
{
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[ServiceContract(ConfigurationName="swCentralCore.IswArchivos")]
	public interface IswArchivos
	{
		[OperationContract(Action="http://tempuri.org/IswArchivos/ActualizarUsuarioPassword", ReplyAction="http://tempuri.org/IswArchivos/ActualizarUsuarioPasswordResponse")]
		ActualizarUsuarioPasswordResponse ActualizarUsuarioPassword(ActualizarUsuarioPasswordRequest request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ActualizarUsuarioPassword", ReplyAction="http://tempuri.org/IswArchivos/ActualizarUsuarioPasswordResponse")]
		Task<ActualizarUsuarioPasswordResponse> ActualizarUsuarioPasswordAsync(ActualizarUsuarioPasswordRequest request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ArchivosBas", ReplyAction="http://tempuri.org/IswArchivos/ArchivosBasResponse")]
		ArchivosBasResponse ArchivosBas(ArchivosBasRequest request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ArchivosBas64", ReplyAction="http://tempuri.org/IswArchivos/ArchivosBas64Response")]
		ArchivosBas64Response ArchivosBas64(ArchivosBas64Request request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ArchivosBas64", ReplyAction="http://tempuri.org/IswArchivos/ArchivosBas64Response")]
		Task<ArchivosBas64Response> ArchivosBas64Async(ArchivosBas64Request request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ArchivosBas", ReplyAction="http://tempuri.org/IswArchivos/ArchivosBasResponse")]
		Task<ArchivosBasResponse> ArchivosBasAsync(ArchivosBasRequest request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ArchivosBio", ReplyAction="http://tempuri.org/IswArchivos/ArchivosBioResponse")]
		ArchivosBioResponse ArchivosBio(ArchivosBioRequest request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ArchivosBio64", ReplyAction="http://tempuri.org/IswArchivos/ArchivosBio64Response")]
		ArchivosBio64Response ArchivosBio64(ArchivosBio64Request request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ArchivosBio64", ReplyAction="http://tempuri.org/IswArchivos/ArchivosBio64Response")]
		Task<ArchivosBio64Response> ArchivosBio64Async(ArchivosBio64Request request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ArchivosBio", ReplyAction="http://tempuri.org/IswArchivos/ArchivosBioResponse")]
		Task<ArchivosBioResponse> ArchivosBioAsync(ArchivosBioRequest request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ArchivosEpd", ReplyAction="http://tempuri.org/IswArchivos/ArchivosEpdResponse")]
		ArchivosEpdResponse ArchivosEpd(ArchivosEpdRequest request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ArchivosEpd", ReplyAction="http://tempuri.org/IswArchivos/ArchivosEpdResponse")]
		Task<ArchivosEpdResponse> ArchivosEpdAsync(ArchivosEpdRequest request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ArchivosFac", ReplyAction="http://tempuri.org/IswArchivos/ArchivosFacResponse")]
		ArchivosFacResponse ArchivosFac(ArchivosFacRequest request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ArchivosFac64", ReplyAction="http://tempuri.org/IswArchivos/ArchivosFac64Response")]
		ArchivosFac64Response ArchivosFac64(ArchivosFac64Request request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ArchivosFac64", ReplyAction="http://tempuri.org/IswArchivos/ArchivosFac64Response")]
		Task<ArchivosFac64Response> ArchivosFac64Async(ArchivosFac64Request request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ArchivosFac", ReplyAction="http://tempuri.org/IswArchivos/ArchivosFacResponse")]
		Task<ArchivosFacResponse> ArchivosFacAsync(ArchivosFacRequest request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ArchivosHue", ReplyAction="http://tempuri.org/IswArchivos/ArchivosHueResponse")]
		ArchivosHueResponse ArchivosHue(ArchivosHueRequest request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ArchivosHue64", ReplyAction="http://tempuri.org/IswArchivos/ArchivosHue64Response")]
		ArchivosHue64Response ArchivosHue64(ArchivosHue64Request request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ArchivosHue64", ReplyAction="http://tempuri.org/IswArchivos/ArchivosHue64Response")]
		Task<ArchivosHue64Response> ArchivosHue64Async(ArchivosHue64Request request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ArchivosHue", ReplyAction="http://tempuri.org/IswArchivos/ArchivosHueResponse")]
		Task<ArchivosHueResponse> ArchivosHueAsync(ArchivosHueRequest request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/CrearUsuario", ReplyAction="http://tempuri.org/IswArchivos/CrearUsuarioResponse")]
		CrearUsuarioResponse CrearUsuario(CrearUsuarioRequest request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/CrearUsuario", ReplyAction="http://tempuri.org/IswArchivos/CrearUsuarioResponse")]
		Task<CrearUsuarioResponse> CrearUsuarioAsync(CrearUsuarioRequest request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ModificarUsuario", ReplyAction="http://tempuri.org/IswArchivos/ModificarUsuarioResponse")]
		ModificarUsuarioResponse ModificarUsuario(ModificarUsuarioRequest request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ModificarUsuario", ReplyAction="http://tempuri.org/IswArchivos/ModificarUsuarioResponse")]
		Task<ModificarUsuarioResponse> ModificarUsuarioAsync(ModificarUsuarioRequest request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ObtenerUsuarioPorId", ReplyAction="http://tempuri.org/IswArchivos/ObtenerUsuarioPorIdResponse")]
		ObtenerUsuarioPorIdResponse ObtenerUsuarioPorId(ObtenerUsuarioPorIdRequest request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ObtenerUsuarioPorId", ReplyAction="http://tempuri.org/IswArchivos/ObtenerUsuarioPorIdResponse")]
		Task<ObtenerUsuarioPorIdResponse> ObtenerUsuarioPorIdAsync(ObtenerUsuarioPorIdRequest request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ObtenerUsuarios", ReplyAction="http://tempuri.org/IswArchivos/ObtenerUsuariosResponse")]
		ObtenerUsuariosResponse ObtenerUsuarios(ObtenerUsuariosRequest request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ObtenerUsuarios", ReplyAction="http://tempuri.org/IswArchivos/ObtenerUsuariosResponse")]
		Task<ObtenerUsuariosResponse> ObtenerUsuariosAsync(ObtenerUsuariosRequest request);
	}
}