using System;

namespace ENROLL.Helpers
{
    public static class HelperRegex
    {
        public const string cNumerosEnteros = "^[0-9 ]{0,9}$";

        public const string cComplementoPersona = "^[A-Z0-9]{2}$";

        public const string cNumerosEnterosMenosCero = "^[-1-9]*$";

        public const string cAlfabeticosBasicosConTildesConEspacios = "^[a-zA-Zñ áéúíóÁÉÚÍÓÑ]*$";

        public const string cAlfanumericoBasicoConTildesConEspacios = "^[a-zA-Z0-9ñ áéúíóÁÉÚÍÓÑ]*$";

        public const string cAlfanumericosCaracteresEspeciales = "^[a-zA-Z0-9._+{}()=&¿?#$%&=¨¬:;,°\"\n\r´'*~<>^¡!ñ áéúíóÁÉÚÍÓÑ@-|-/]*$";

        public const string cAlfanumericosCaracteresEspecialesNoComillas = "^[a-zA-Z0-9._+{}()=&¿?#$%&=¨¬:;,°\n\r´'*~<>^¡!ñ áéúíóÁÉÚÍÓÑ@-|-/]*$";

        public const string cAlfanumericoBasicoGuionMedioSinTildesSinEspacios = "^([a-zñ]){2}-([A-ZÑ]){2}$";

        public const string cAlfabeticoSoloMayusculasGuion = "^[A-Z_]*$";

        public const string cAlfanumericoMayusculaGuionBajoPuntoSinTildesSinEspacios = "^[A-Z0-9Ñ._]*$";

        public const string cAlfabeticoBasicoPuntoSinEspacios = "^[a-z.]*$";

        public const string cAlfanumericosBasicos = "^[a-zA-Z0-9._+-/]{0,9}$";

        public const string cAlfanumericosBasicosRuta = "^[a-zA-Z0-9._+-/]{0,50}$";

        public const string cAlfabeticosBasicosSinEspacios = "^[a-zA-Z]*$";

        public const string cAlfanumericoBasicoSinEspacios = "^[a-zA-Z0-9]*$";

        public const string cAlfabeticoMondedaSoloMayusculas = "^[A-Z]*$";

        public const string cAlfanumericoMayusculaPuntoSinEspacios = "^[A-Z0-9Ñ.]*$";

        public const string cAlfanumericoPuntoSinEspacios = "^[a-zA-Z0-9ñÑ.]*$";

        public const string cAlfanumericoBasicoPuntoSinEspacios = "^[a-zA-Z0-9.]*$";

        public const string cAlfanumericoBasicoGuionPuntoSinEspacios = "^[a-zA-Z0-9._]*$";

        public const string cAlfanumericoBasicoGuionBajoSinEspacios = "^[a-zA-Z0-9_]*$";

        public const string cAlfanumericoBasicoGuionBajoPuntoSinTildesSinEspacios = "^[a-zA-Z0-9ñÑ._]*$";

        public const string cAlfanumericoBasicoGuionBajoGuionMedioSinTildesSinEspacios = "^[a-zA-Z0-9ñÑ_-]*$";

        public const string cNumericoCaracteresEspecialesSinEspacios = "^[0-9\\]\\[!\"#$%&'\\(\\)*+,-./:;<=>?@^_`\\{|\\}~¡©®°¿‘’‚“”„™÷€\\^]*$";

        public const string cContrasenia = "(?=^.{8,}$)((?=.*\\d)|(?=.*\\W+))(?![.\\n])(?=.*[A-Z])(?=.*[a-z]).*$";

        public const string cCorreoElectronico = "^([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$";
    }
}