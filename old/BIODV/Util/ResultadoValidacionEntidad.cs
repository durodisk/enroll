using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BIODV.Util
{
	public class ResultadoValidacionEntidad
	{
		public Dictionary<string, string> Error
		{
			get;
		}

		public string ErrorSerializable
		{
			get
			{
				return JsonConvert.SerializeObject(this.Error);
			}
		}

		public string ErrorSerializablePascalCase
		{
			get
			{
				string json = JsonConvert.SerializeObject(this.Error);
				JObject vDatosSalida = JObject.Parse(json);
				JObject vJsonResultado = new JObject(new JProperty("errorDatosSolicitud", vDatosSalida));
				string str = vJsonResultado.ToString(Formatting.None, Array.Empty<JsonConverter>()).CamelCaseJson();
				return str;
			}
		}

		public bool EsValido
		{
			get
			{
				return this.Error.Count <= 0;
			}
		}

		public ResultadoValidacionEntidad(IList<ValidationResult> pError = null, bool pPropiedadCompleta = false)
		{
			Dictionary<string, string> vDiccionario = new Dictionary<string, string>();
			if ((pError == null ? false : pError.Any<ValidationResult>()))
			{
				foreach (ValidationResult item in pError)
				{
					if (item.MemberNames.Count<string>() > 1 & pPropiedadCompleta)
					{
						vDiccionario.Add(JsonConvert.SerializeObject(item.MemberNames), item.ErrorMessage);
					}
					else if (!vDiccionario.ContainsKey(item.MemberNames.ElementAt<string>(0)))
					{
						foreach (string vItem in item.MemberNames)
						{
							vDiccionario.Add(vItem, item.ErrorMessage);
						}
					}
				}
			}
			this.Error = vDiccionario;
		}
	}
}