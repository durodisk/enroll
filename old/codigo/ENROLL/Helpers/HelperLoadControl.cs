using Datys.SIP.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace ENROLL.Helpers
{
    public static class HelperLoadControl
    {
        public static void Checklist(CheckedListBox pCheckedListBox, List<Coder> pColCoder)
        {
            pCheckedListBox.ValueMember = "Id";
            pCheckedListBox.DisplayMember = "Value";
            List<string> vOpciones = (
                from x in pColCoder
                select x.Value).ToList<string>();
            pCheckedListBox.DataSource = vOpciones;
        }

        internal static void Checklist(BindingList<Coder> pColCoder, CheckedListBox chkPiel)
        {
            if (pColCoder.Any<Coder>())
            {
                for (int i = 0; i < chkPiel.Items.Count; i++)
                {
                    if ((
                        from cust in pColCoder
                        where cust.Value == chkPiel.Items[i].ToString()
                        select cust).FirstOrDefault<Coder>() != null)
                    {
                        chkPiel.SetItemChecked(i, true);
                    }
                }
            }
        }

        public static void Combo(ComboBox pComboBox, List<CoderType> pColItem)
        {
            List<CoderType> vColItems = new List<CoderType>()
            {
                new CoderType()
                {
                    Id = "",
                    Value = "[Seleccionar valor]"
                }
            };
            vColItems.AddRange(pColItem);
            pComboBox.ValueMember = "Id";
            pComboBox.DisplayMember = "Value";
            pComboBox.DataSource = vColItems;
            pComboBox.SelectedItem = 0;
        }

        public static void Combo(ComboBox pComboBox, List<Coder> pColCoder, bool pSeleccionar)
        {
            List<Coder> vColItems = new List<Coder>();
            if (pSeleccionar)
            {
                vColItems.Add(new Coder()
                {
                    Id = "",
                    Value = "[Seleccionar valor]"
                });
            }
            vColItems.AddRange(pColCoder);
            pComboBox.ValueMember = "Id";
            pComboBox.DisplayMember = "Value";
            pComboBox.DataSource = vColItems;
            pComboBox.SelectedItem = 0;
        }

        public static void ComboB(ComboBox pComboBox, BindingList<Coder> pColCoder, bool pSeleccionar)
        {
            List<Coder> vColItems = new List<Coder>();
            if (pSeleccionar)
            {
                vColItems.Add(new Coder()
                {
                    Id = "",
                    Value = "[Seleccionar valor]"
                });
            }
            vColItems.AddRange(pColCoder);
            pComboBox.ValueMember = "Id";
            pComboBox.DisplayMember = "Value";
            pComboBox.DataSource = vColItems;
            pComboBox.SelectedItem = 0;
        }

        internal static void ComboGrid(DataGridViewComboBoxColumn departamento, List<Coder> vCoder)
        {
            List<Coder> vColItems = new List<Coder>()
            {
                new Coder()
                {
                    Id = "",
                    Value = "[Seleccionar valor]"
                }
            };
            vColItems.AddRange(vCoder);
            departamento.ValueMember = "Id";
            departamento.DisplayMember = "Value";
            departamento.DataSource = vColItems;
        }

        public static Coder ObtenerCoder(ComboBox pCmb, string pCoderTypeId)
        {
            Coder vCoder = new Coder();
            if (pCmb.SelectedValue != null)
            {
                vCoder = new Coder()
                {
                    CoderTypeId = pCoderTypeId,
                    Id = pCmb.SelectedValue.ToString(),
                    Value = pCmb.Text
                };
            }
            return vCoder;
        }

        public static List<Coder> ObtenerLista(string pGrupo)
        {
            HelperSerializer ser = new HelperSerializer();
            List<Coder> vColCoder = new List<Coder>();
            string directoryName = Path.GetDirectoryName(Application.ExecutablePath);
            char directorySeparatorChar = Path.DirectorySeparatorChar;
            string path = string.Concat(directoryName, directorySeparatorChar.ToString(), "Coders\\Coders.xml");
            if (File.Exists(path))
            {
                CoderBase vCoderBase = ser.Deserialize<CoderBase>(File.ReadAllText(path));
                vColCoder = (
                    from cust in vCoderBase.CodersList
                    where cust.CoderTypeId == pGrupo
                    select cust).ToList<Coder>();
            }
            return vColCoder;
        }

        public static List<Coder> ObtenerLista(string pGrupo, bool pOrdenar)
        {
            HelperSerializer ser = new HelperSerializer();
            List<Coder> vColCoder = new List<Coder>();
            string directoryName = Path.GetDirectoryName(Application.ExecutablePath);
            char directorySeparatorChar = Path.DirectorySeparatorChar;
            string path = string.Concat(directoryName, directorySeparatorChar.ToString(), "Coders\\Coders.xml");
            if (File.Exists(path))
            {
                CoderBase vCoderBase = ser.Deserialize<CoderBase>(File.ReadAllText(path));
                vColCoder = (
                    from cust in vCoderBase.CodersList
                    where cust.CoderTypeId == pGrupo
                    select cust).ToList<Coder>();
                if (pOrdenar)
                {
                    vColCoder = (
                        from si in vColCoder
                        orderby si.Value
                        select si).ToList<Coder>();
                }
            }
            return vColCoder;
        }

        internal static List<Coder> ObtenerSubLista(string pGrupo, string pSubGrupo)
        {
            List<Coder> coders;
            try
            {
                HelperSerializer ser = new HelperSerializer();
                List<Coder> vColCoder = new List<Coder>();
                string directoryName = Path.GetDirectoryName(Application.ExecutablePath);
                char directorySeparatorChar = Path.DirectorySeparatorChar;
                string path = string.Concat(directoryName, directorySeparatorChar.ToString(), "Coders\\Coders.xml");
                if (File.Exists(path))
                {
                    CoderBase vCoderBase = ser.Deserialize<CoderBase>(File.ReadAllText(path));
                    Coder vCoder = (
                        from cust in vCoderBase.CodersList
                        where (cust.CoderTypeId != pGrupo ? false : cust.Id == pSubGrupo)
                        select cust).FirstOrDefault<Coder>();
                    if (vCoder != null)
                    {
                        vColCoder = vCoder.ListCoder;
                    }
                }
                vColCoder = (
                    from o in vColCoder
                    orderby o.Id
                    select o).ToList<Coder>();
                coders = vColCoder;
            }
            catch
            {
                coders = null;
            }
            return coders;
        }

        internal static List<Coder> ObtenerSubLista(string pGrupo, string pSubGrupo, bool pOrdenar)
        {
            HelperSerializer ser = new HelperSerializer();
            List<Coder> vColCoder = new List<Coder>();
            string directoryName = Path.GetDirectoryName(Application.ExecutablePath);
            char directorySeparatorChar = Path.DirectorySeparatorChar;
            string path = string.Concat(directoryName, directorySeparatorChar.ToString(), "Coders\\Coders.xml");
            if (File.Exists(path))
            {
                CoderBase vCoderBase = ser.Deserialize<CoderBase>(File.ReadAllText(path));
                Coder vCoder = (
                    from cust in vCoderBase.CodersList
                    where (cust.CoderTypeId != pGrupo ? false : cust.Id == pSubGrupo)
                    select cust).FirstOrDefault<Coder>();
                if (vCoder != null)
                {
                    vColCoder = vCoder.ListCoder;
                    if (pOrdenar)
                    {
                        vColCoder = (
                            from si in vColCoder
                            orderby si.Value
                            select si).ToList<Coder>();
                    }
                }
            }
            return vColCoder;
        }
    }
}