using System.CodeDom.Compiler;
using System.ServiceModel;
using System.Threading.Tasks;

namespace ENROLL.Core
{
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[ServiceContract(ConfigurationName="swCentralCore.IswArchivos")]
	public interface CoreFiles
	{
		[OperationContract(Action="http://tempuri.org/IswArchivos/ActualizarUsuarioPassword", ReplyAction="http://tempuri.org/IswArchivos/ActualizarUsuarioPasswordResponse")]
		CorePasswordRequest ActualizarUsuarioPassword(CoreLoginRequest request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ActualizarUsuarioPassword", ReplyAction="http://tempuri.org/IswArchivos/ActualizarUsuarioPasswordResponse")]
		Task<CorePasswordRequest> ActualizarUsuarioPasswordAsync(CoreLoginRequest request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ArchivosBas", ReplyAction="http://tempuri.org/IswArchivos/ArchivosBasResponse")]
		CoreBasResponse ArchivosBas(CoreBasRequest request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ArchivosBas64", ReplyAction="http://tempuri.org/IswArchivos/ArchivosBas64Response")]
		CoreBas64Response ArchivosBas64(CoreBas64Request request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ArchivosBas64", ReplyAction="http://tempuri.org/IswArchivos/ArchivosBas64Response")]
		Task<CoreBas64Response> ArchivosBas64Async(CoreBas64Request request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ArchivosBas", ReplyAction="http://tempuri.org/IswArchivos/ArchivosBasResponse")]
		Task<CoreBasResponse> ArchivosBasAsync(CoreBasRequest request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ArchivosBio", ReplyAction="http://tempuri.org/IswArchivos/ArchivosBioResponse")]
		CoreBioResponse ArchivosBio(CoreBioRequest request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ArchivosBio64", ReplyAction="http://tempuri.org/IswArchivos/ArchivosBio64Response")]
		CoreBio64Response ArchivosBio64(CoreBio64Request request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ArchivosBio64", ReplyAction="http://tempuri.org/IswArchivos/ArchivosBio64Response")]
		Task<CoreBio64Response> ArchivosBio64Async(CoreBio64Request request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ArchivosBio", ReplyAction="http://tempuri.org/IswArchivos/ArchivosBioResponse")]
		Task<CoreBioResponse> ArchivosBioAsync(CoreBioRequest request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ArchivosEpd", ReplyAction="http://tempuri.org/IswArchivos/ArchivosEpdResponse")]
		CoreEpdResponse ArchivosEpd(CoreEpdRequest request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ArchivosEpd", ReplyAction="http://tempuri.org/IswArchivos/ArchivosEpdResponse")]
		Task<CoreEpdResponse> ArchivosEpdAsync(CoreEpdRequest request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ArchivosFac", ReplyAction="http://tempuri.org/IswArchivos/ArchivosFacResponse")]
		CoreFacResponse ArchivosFac(CoreFacRequest request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ArchivosFac64", ReplyAction="http://tempuri.org/IswArchivos/ArchivosFac64Response")]
		CoreFac64Response ArchivosFac64(CoreFac64Request request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ArchivosFac64", ReplyAction="http://tempuri.org/IswArchivos/ArchivosFac64Response")]
		Task<CoreFac64Response> ArchivosFac64Async(CoreFac64Request request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ArchivosFac", ReplyAction="http://tempuri.org/IswArchivos/ArchivosFacResponse")]
		Task<CoreFacResponse> ArchivosFacAsync(CoreFacRequest request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ArchivosHue", ReplyAction="http://tempuri.org/IswArchivos/ArchivosHueResponse")]
		CoreHueResponse ArchivosHue(CoreHueRequest request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ArchivosHue64", ReplyAction="http://tempuri.org/IswArchivos/ArchivosHue64Response")]
		CoreHue64Response ArchivosHue64(CoreHue64Request request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ArchivosHue64", ReplyAction="http://tempuri.org/IswArchivos/ArchivosHue64Response")]
		Task<CoreHue64Response> ArchivosHue64Async(CoreHue64Request request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ArchivosHue", ReplyAction="http://tempuri.org/IswArchivos/ArchivosHueResponse")]
		Task<CoreHueResponse> ArchivosHueAsync(CoreHueRequest request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/CrearUsuario", ReplyAction="http://tempuri.org/IswArchivos/CrearUsuarioResponse")]
		CoreCreateUserResponse CrearUsuario(CoreCreateUserRequest request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/CrearUsuario", ReplyAction="http://tempuri.org/IswArchivos/CrearUsuarioResponse")]
		Task<CoreCreateUserResponse> CrearUsuarioAsync(CoreCreateUserRequest request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ModificarUsuario", ReplyAction="http://tempuri.org/IswArchivos/ModificarUsuarioResponse")]
		CoreEditUserResponse ModificarUsuario(CoreEditUserRequest request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ModificarUsuario", ReplyAction="http://tempuri.org/IswArchivos/ModificarUsuarioResponse")]
		Task<CoreEditUserResponse> ModificarUsuarioAsync(CoreEditUserRequest request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ObtenerUsuarioPorId", ReplyAction="http://tempuri.org/IswArchivos/ObtenerUsuarioPorIdResponse")]
		CoreGetUserByIdResponse ObtenerUsuarioPorId(CoreGetUseryByIdRequest request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ObtenerUsuarioPorId", ReplyAction="http://tempuri.org/IswArchivos/ObtenerUsuarioPorIdResponse")]
		Task<CoreGetUserByIdResponse> ObtenerUsuarioPorIdAsync(CoreGetUseryByIdRequest request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ObtenerUsuarios", ReplyAction="http://tempuri.org/IswArchivos/ObtenerUsuariosResponse")]
		CoreGetUsersResponse ObtenerUsuarios(CoreGetUsersRequest request);

		[OperationContract(Action="http://tempuri.org/IswArchivos/ObtenerUsuarios", ReplyAction="http://tempuri.org/IswArchivos/ObtenerUsuariosResponse")]
		Task<CoreGetUsersResponse> ObtenerUsuariosAsync(CoreGetUsersRequest request);
	}
}