using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ENROLL.Helpers
{
    public class HelperValidatorField
    {
        public HelperValidatorField()
        {
        }

        public static bool ValidarCampos(object pModelo, ErrorProvider pErrorProveedor, ContainerControl pContenedor)
        {
            bool vResultado = true;
            HelperValidator vResultadoValidacion = HelperValidacion.ValidarEntidad<object>(pModelo);
            pErrorProveedor.Clear();
            foreach (KeyValuePair<string, string> vError in vResultadoValidacion.Error)
            {
                System.Windows.Forms.Control vControl = pContenedor.Controls.Find(vError.Key, true).SingleOrDefault<System.Windows.Forms.Control>();
                if (vControl != null)
                {
                    pErrorProveedor.SetError(vControl, vError.Value);
                }
                vResultado = false;
            }
            return vResultado;
        }
    }
}