using ENROLL.Model;
using ENROLL.Helpers;
using Datys.Enroll.Core;
using Datys.SIP.Common;
using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace ENROLL
{
    public class vTattoos : Form
    {
        private HelperSerializer ser = new HelperSerializer();

        private Image<Bgr, byte> imgInput;

        private Rectangle rect;

        private Point StartLocation;

        private Point EndLcation;

        private int rowIndex = 0;

        private int cantidad = 0;

        private string _nombrePB;

        private DataTable dt;

        private bool _seleccionado = false;

        private bool _selecting;

        private float escala;

        private float saldoX;

        private float saldoY;

        private Image<Bgr, byte> temp;

        private IContainer components = null;

        private TabControl tabControl1;

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;

        private ToolStripMenuItem eliminarToolStripMenuItem;

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;

        private ToolStripMenuItem eliminarToolStripMenuItem1;

        private ErrorProvider errorProvider1;

        private Panel panel3;

        private Button btnAnterior;

        private Button btnguardar;

        private Button btncancelar;

        private TabPage tabPage1;

        private Panel panel1;

        private Label label9;

        private Button btnañadirM;

        private Label label3;

        private Label label2;

        private ComboBox cmbAreaCuerpo;

        private ComboBox cmbTipoMarca;

        private DataGridView dataGridView1;

        private Button btnCargarMarcas;

        private PictureBox pbImagenMarca;

        private ToolStripMenuItem eliminarToolStripMenuItem3;

        private ToolStripMenuItem eliminarToolStripMenuItem2;

        private Button btnfotoM;

        private ToolStripMenuItem editarToolStripMenuItem;

        private TableLayoutPanel tableLayoutPanel2;

        private TableLayoutPanel tableLayoutPanel1;

        private Label label13;

        private TableLayoutPanel tableLayoutPanel11;

        private PictureBox pictureBox1;

        private Button button1;

        public BindingList<MarkData> Marcas
        {
            get;
            set;
        }

        public BindingList<TattooData> Tatuajes
        {
            get;
            set;
        }

        public vTattoos()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// this funcion takes the actual data and add a row with the selected values of the combo boxes
        /// </summary>
        private void AddRowToTable()
        {
            this.dt = (DataTable)this.dataGridView1.DataSource;
            if (this.dt == null)
            {
                this.dt = new DataTable();
            }
            this.dt.Merge(this.GetRowToBeAdded());
            this.dataGridView1.DataSource = this.dt;
            this.dataGridView1.Columns["IdTipoMarca"].Visible = false;
            this.dataGridView1.Columns["IdAreaCuerpo"].Visible = false;
            this.dataGridView1.Columns["TipoMarca"].Width = 170;
            this.dataGridView1.Columns["AreaCuerpo"].Width = 170;
            this.dataGridView1.Columns["Cantidad"].Width = 120;
        }
       

        private bool Alerta(string titulo, string mensaje, bool confirmacion)
        {
            bool accion = false;
            if (!confirmacion)
            {
                MessageBox.Show(mensaje, titulo);
            }
            else
            {
                accion = (MessageBox.Show(mensaje, titulo, MessageBoxButtons.YesNo).ToString() != "Yes" ? false : true);
            }
            return accion;
        }

        private void btnanterior_Click(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex == 1)
            {
                this.btnAnterior.Enabled = false;
            }
            this.tabControl1.SelectTab(this.tabControl1.SelectedIndex - 1);
            //   this.btnsiguiente.Enabled = true;
            //   this.btnsiguiente.Text = "Siguiente";
        }

        /*  botono que anade una fila cuando hace click   */
        private void BtnAddRow(object sender, EventArgs e)
        {
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

            if (this.ValidaModeloDatosMarcas())
            {
                this.AddRowToTable();
                this.LlenaMarcas();
                this.cmbAreaCuerpo.SelectedIndex = 0;
                this.cmbTipoMarca.SelectedIndex = 0;
                //      this.btnsiguiente.Enabled = this.ValidaGrilla(this.dt);
                this.rowIndex = this.dt.Rows.Count - 1;
                this.CargaImagenesMarcas();
                this._seleccionado = true;
                this.cantidad = 0;
            }
            else
            {
                MessageBox.Show("Por favor seleccione Tipo Adjunto y el Area del cuerpo para añadir un registro ", "Error");
            }

            System.Windows.Forms.Cursor.Current = Cursors.Default;
        }

        //private void btnañadirT_Click(object sender, EventArgs e)
        //{
        //    if (this.ValidaModeloDatosTatuajes())
        //    {
        //        this.AgregaFilaGrillaTatuajes();
        //        this.LlenaTatuajes();
        //        this.txtNombreT.Text = "";
        //        this.cmbMotivo.SelectedIndex = 0;
        //        this.cmbAreaCuerpoT.SelectedIndex = 0;
        //        this.cmbColor.SelectedIndex = 0;
        //        //   this.btnsiguiente.Enabled = this.ValidaGrilla(this.dt);
        //        this.rowIndex = this.dt.Rows.Count - 1;
        //        this.CargaImagenesTatuajes();
        //    }
        //}

        private void btncancelar_Click(object sender, EventArgs e)
        {
            if (this.Alerta("Mensaje", "Se guardaran los datos y se saldra del formulario,\nEsta seguro de realizar esta operacion ? ", true))
            {
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
                vEnroll.PersonaCapturada.RecordData.MarkDatas = this.Marcas;
                vEnroll.PersonaCapturada.RecordData.TattooDatas = this.Tatuajes;
                (new HelperSerializer()).SerializeEpd(vEnroll.PersonaCapturada, vContainerMain.RutaEpd);
                System.Windows.Forms.Cursor.Current = Cursors.Default;
                (new vEnroll()
                {
                    MdiParent = base.ParentForm
                }).Show();
                base.Close();
            }
        }

        private void btnCargarMarcas_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            try
            {
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.imgInput = new Image<Bgr, byte>(ofd.FileName);
                    this.pbImagenMarca.Image = this.imgInput.Bitmap;
                    this.pbImagenMarca.Enabled = true;
                    this.CargaCarril();
                }else
                {
                    this.Alerta("Mensaje", "Por favor seleccione una imagen", false);
                }
            }
            catch
            {
                this.Alerta("Mensaje", "Por favor seleccione una imagen", false);
            }
        }

        //private void btnCargarTatuajes_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        OpenFileDialog ofd = new OpenFileDialog();
        //        if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //        {
        //            this.imgInput = new Image<Bgr, byte>(ofd.FileName);
        //            this.pbImagenTatuaje.Image = this.imgInput.Bitmap;
        //            this.pbImagenTatuaje.Enabled = true;
        //        }
        //    }
        //    catch
        //    {
        //    }
        //}

        //private void btnFoto2_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        vPicture frm = new vPicture();
        //        if (frm.ShowDialog(this) != System.Windows.Forms.DialogResult.OK)
        //        {
        //        }
        //        frm.Dispose();
        //        if (vPicture.Captura != null)
        //        {
        //            this.imgInput = new Image<Bgr, byte>(vPicture.Captura);
        //            this.pbImagenTatuaje.Image = this.imgInput.Bitmap;
        //            this.pbImagenTatuaje.Enabled = true;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        Console.WriteLine(exception);
        //    }
        //}

        private void btnfotoM_Click(object sender, EventArgs e)
        {
            try
            {
                vPicture frm = new vPicture();
                if (frm.ShowDialog(this) != System.Windows.Forms.DialogResult.OK)
                {
                }
                frm.Dispose();
                if (vPicture.Captura != null)
                {
                    this.imgInput = new Image<Bgr, byte>(vPicture.Captura);
                    this.pbImagenMarca.Image = this.imgInput.Bitmap;
                    this.pbImagenMarca.Enabled = true;
                    this.CargaCarril();
                }
            }
            catch (Exception exception)
            {
                this.Alerta("Mensaje", "No se detecto una camara conectada", false);
            //    Console.WriteLine(exception);
            }
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
                vEnroll.PersonaCapturada.RecordData.MarkDatas = this.Marcas;
                vEnroll.PersonaCapturada.RecordData.TattooDatas = this.Tatuajes;
                (new HelperSerializer()).SerializeEpd(vEnroll.PersonaCapturada, vContainerMain.RutaEpd);
                System.Windows.Forms.Cursor.Current = Cursors.Default;
                this.Alerta("Mensaje", "Se guardaron los datos satisfactoriamente", false);
            }
            catch
            {
            }
        }

        //private void btnsiguiente_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int selectedIndex = this.tabControl1.SelectedIndex;
        //        if (selectedIndex == 0)
        //        {
        //            this.btnAnterior.Enabled = true;
        //            this.tabControl1.SelectTab(1);
        //            this.CargaPestanaTatuajes();
        //            this.btnsiguiente.Text = "Finalizar";
        //            this.btnsiguiente.Enabled = this.ValidaGrilla((DataTable)this.dataGridView2.DataSource);
        //        }
        //        else if (selectedIndex == 1)
        //        {
        //            vEnroll.PersonaCapturada.RecordData.MarkDatas = this.Marcas;
        //            vEnroll.PersonaCapturada.RecordData.TattooDatas = this.Tatuajes;
        //            base.Close();
        //        }
        //        this.ser.SerializeEpd(vEnroll.PersonaCapturada, vContainerMain.RutaEpd);
        //        System.Windows.Forms.Cursor.Current = Cursors.Default;
        //    }
        //    catch
        //    {
        //    }
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.Alerta("Mensaje", "¿ Esta seguro de Salir ? , puede que exista datos sin ser guardados", true))
            {
                (new vEnroll()
                {
                    MdiParent = base.ParentForm
                }).Show();
                base.Close();
            }
        }

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    base.Close();
        //}

        public void CargaAreaCuerpo()
        {
            List<Coder> vCoderp = HelperLoadControl.ObtenerLista("BODYAREA");
            HelperLoadControl.Combo(this.cmbAreaCuerpo, vCoderp, true);
            //  HelperLoadControl.Combo(this.cmbAreaCuerpoT, vCoderp, true);
        }

        private void CargaCarril()
        {
            try
            {
                PictureBox p = new PictureBox()
                {
                    Name = string.Concat("pb", this.cantidad.ToString()),
                    Image = this.imgInput.Bitmap,
                    Location = new Point(20 + this.cantidad * 100, 20),
                    Size = new System.Drawing.Size(90, 90),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BorderStyle = BorderStyle.FixedSingle
                };
                p.MouseClick += new MouseEventHandler(this.Pb_MouseClick);
                this.dt = (DataTable)this.dataGridView1.DataSource;
               
                /* */


                //if (this.dt == null)
                //{
                //    this.panel1.Controls.Add(p);
                //    this.Pb_MouseClick(this.panel1.Controls.OfType<PictureBox>().ElementAt<PictureBox>(this.cantidad), new MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 1, 0, 0, 0));
                //    this.cantidad++;
                ////    this.chkanadir.Checked = true;
                //}              
                //else 
                //if (true)
                //{

                //cantidad de imagenes de la fila x
                    this.cantidad = Convert.ToInt32(this.dt.Rows[this.rowIndex]["Cantidad"]);
                //anade la imagen abajo
                    this.panel1.Controls.Add(p);
                /*controladora de click izquierdo click derecho*/
                    this.Pb_MouseClick(this.panel1.Controls.OfType<PictureBox>().ElementAt<PictureBox>(this.cantidad), new MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 1, 0, 0, 0));
                /* anado la cantidad de imagenes */ 
                    
                    this.cantidad++;
                    this.dt = (DataTable)this.dataGridView1.DataSource;
                    this.dt.Rows[this.rowIndex]["Cantidad"] = this.cantidad;
                    this.dataGridView1.DataSource = this.dt;
                    //   this.btnsiguiente.Enabled = this.ValidaGrilla(this.dt);
                 //   if (this._seleccionado)
                  //  {
                        this.Marcas[this.rowIndex].ListImages.Add(new ImageData()
                        {
                            NormalizedImageArr = this.ser.ImagenToJPGByteArray((Bitmap)this.ser.CambiarTamanoImagen(this.imgInput.Bitmap, 800))
                        });
                  //  }
                    this.pbImagenMarca.Enabled = false;
                    this.CargaImagenesMarcas();
                //}
                /*
              else if (!this.chkNuevo.Checked)
              {
                  this.panel1.Controls.Add(p);
                  this.Pb_MouseClick(this.panel1.Controls.OfType<PictureBox>().ElementAt<PictureBox>(this.cantidad), new MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 1, 0, 0, 0));
                  this.cantidad++;
                  this.dt = (DataTable)this.dataGridView1.DataSource;
                  this.dt.Rows[this.rowIndex]["Cantidad"] = this.cantidad;
                  this.dataGridView1.DataSource = this.dt;
                  //   this.btnsiguiente.Enabled = this.ValidaGrilla(this.dt);
                  if (this._seleccionado)
                  {
                      this.Marcas[this.rowIndex].ListImages.Add(new ImageData()
                      {
                          NormalizedImageArr = this.ser.ImagenToJPGByteArray((Bitmap)this.ser.CambiarTamanoImagen(this.imgInput.Bitmap, 1500))
                      });
                  }
                  this.pbImagenMarca.Enabled = false;
              }*/
                //else
                //{
                //    if (this.cantidad == 0)
                //    {
                //        this.dataGridView1.ClearSelection();
                //        this.panel1.Controls.Clear();
                //        this.pbImagenMarca.Image = null;
                //        this.cantidad = 0;
                //        this._seleccionado = false;
                //    }
                //    this._seleccionado = false;
                //    this.panel1.Controls.Add(p);
                //    this.Pb_MouseClick(this.panel1.Controls.OfType<PictureBox>().ElementAt<PictureBox>(this.cantidad), new MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 1, 0, 0, 0));
                //    this.cantidad++;
                //}

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public void CargaColor()
        {
            List<Coder> vCoderp = HelperLoadControl.ObtenerLista("TATOOCOLOR");
            //     HelperLoadControl.Combo(this.cmbColor, vCoderp, true);
        }

        private void CargaImagenesMarcas()
        {
            this.cantidad = 0;
            this.pbImagenMarca.Image = null;
            this.panel1.Controls.Clear();
            if ((this.Marcas == null ? false : this.Marcas.Any<MarkData>()))
            {
                MarkData vMarca = this.Marcas[this.rowIndex];
                if ((vMarca.ListImages == null ? false : vMarca.ListImages.Any<ImageData>()))
                {
                    foreach (ImageData img in vMarca.ListImages)
                    {
                        PictureBox p = new PictureBox()
                        {
                            Name = string.Concat("pb", this.cantidad.ToString()),
                            Image = this.ser.byteArrayToImage(img.NormalizedImageArr),
                            Location = new Point(20 + this.cantidad * 100, 20),
                            Size = new System.Drawing.Size(90, 90),
                            SizeMode = PictureBoxSizeMode.Zoom,
                            BorderStyle = BorderStyle.FixedSingle
                        };
                        p.MouseClick += new MouseEventHandler(this.Pb_MouseClick);
                        this.panel1.Controls.Add(p);
                        this.cantidad++;
                    }
                    this.Pb_MouseClick(this.panel1.Controls.OfType<PictureBox>().ElementAt<PictureBox>(this.cantidad - 1), new MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 1, 0, 0, 0));
                }
                int c = 0;
                foreach (DataGridViewRow row in (IEnumerable)this.dataGridView1.Rows)
                {
                    row.Selected = c == this.rowIndex;
                    c++;
                }
            }
        }

        //private void CargaImagenesTatuajes()
        //{
        //    this.cantidad = 0;
        //    this.pbImagenTatuaje.Image = null;
        //    this.panel2.Controls.Clear();
        //    if ((this.Tatuajes == null ? true : !this.Tatuajes.Any<TattooData>()))
        //    {
        //        this.btnCargarTatuajes.Enabled = false;
        //    }
        //    else
        //    {
        //        TattooData vTatuajes = this.Tatuajes[this.rowIndex];
        //        if ((vTatuajes.ListImages == null ? false : vTatuajes.ListImages.Any<ImageData>()))
        //        {
        //            foreach (ImageData img in vTatuajes.ListImages)
        //            {
        //                PictureBox p = new PictureBox()
        //                {
        //                    Name = string.Concat("pbt", this.cantidad.ToString()),
        //                    Image = this.ser.byteArrayToImage(img.NormalizedImageArr),
        //                    Location = new Point(20 + this.cantidad * 100, 20),
        //                    Size = new System.Drawing.Size(90, 90),
        //                    SizeMode = PictureBoxSizeMode.Zoom,
        //                    BorderStyle = BorderStyle.FixedSingle
        //                };
        //                p.MouseClick += new MouseEventHandler(this.Pbt_MouseClick);
        //                this.panel2.Controls.Add(p);
        //                this.cantidad++;
        //            }
        //            this.Pbt_MouseClick(this.panel2.Controls.OfType<PictureBox>().ElementAt<PictureBox>(this.cantidad - 1), new MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 1, 0, 0, 0));
        //        }
        //        int c = 0;
        //        //foreach (DataGridViewRow row in (IEnumerable)this.dataGridView2.Rows)
        //        //{
        //        //    row.Selected = c == this.rowIndex;
        //        //    c++;
        //        //}
        //        this.btnCargarTatuajes.Enabled = true;
        //    }
        //}

        public void CargaMotivo()
        {
            List<Coder> vCoderp = HelperLoadControl.ObtenerLista("TATOOMOTIVE");
            //      HelperLoadControl.Combo(this.cmbMotivo, vCoderp, true);
        }

        private void CargaPestanaMarcas()
        {
            this.pbImagenMarca.Enabled = false;
            this.cmbTipoMarca.SelectedIndex = 0;
            this.cmbAreaCuerpo.SelectedIndex = 0;
            DataTable dt = this.ObtenerDataTableMarcas();
            //  this.btnsiguiente.Enabled = this.ValidaGrilla(dt);
            if (dt.Rows.Count > 0)
            {
                this.dataGridView1.DataSource = dt;
                this.dataGridView1.Columns["IdTipoMarca"].Visible = false;
                this.dataGridView1.Columns["IdAreaCuerpo"].Visible = false;
                this.dataGridView1.Columns["TipoMarca"].Width = 170;
                this.dataGridView1.Columns["AreaCuerpo"].Width = 170;
                this.dataGridView1.Columns["Cantidad"].Width = 120;
                this.rowIndex = dt.Rows.Count - 1;
                this.CargaImagenesMarcas();
                this._seleccionado = true;
            }
        }

        //private void CargaPestanaTatuajes()
        //{
        //    this.btnCargarTatuajes.Enabled = false;
        //    this.pbImagenTatuaje.Enabled = false;
        //    this.txtNombreT.Text = "";
        //    this.cmbMotivo.SelectedIndex = 0;
        //    this.cmbAreaCuerpoT.SelectedIndex = 0;
        //    this.cmbColor.SelectedIndex = 0;
        //    DataTable dt = this.ObtenerDataTableTatuajes();
        //    if (dt.Rows.Count > 0)
        //    {
        //        this.dataGridView2.DataSource = dt;
        //        this.dataGridView2.Columns["IdMotivo"].Visible = false;
        //        this.dataGridView2.Columns["IdAreaCuerpo"].Visible = false;
        //        this.dataGridView2.Columns["IdColor"].Visible = false;
        //        this.dataGridView2.Columns["Nombre"].Width = 145;
        //        this.dataGridView2.Columns["Motivo"].Width = 145;
        //        this.dataGridView2.Columns["AreaCuerpo"].Width = 145;
        //        this.dataGridView2.Columns["Color"].Width = 145;
        //        this.dataGridView2.Columns["Cantidad"].Width = 110;
        //        this.rowIndex = dt.Rows.Count - 1;
        //        this.CargaImagenesTatuajes();
        //    }
        //}

        public void CargaTipoMarca()
        {
            List<Coder> vCoderp = HelperLoadControl.ObtenerLista("MARKTYPE");
            HelperLoadControl.Combo(this.cmbTipoMarca, vCoderp, true);
        }

        //private void chkanadir_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (!this.chkanadir.Checked)
        //    {
        //        this.rowIndex = -1;
        //        this.chkNuevo.Checked = true;
        //    }
        //    else
        //    {
        //        this.chkNuevo.Checked = false;
        //    }
        //}

        private void chkNuevo_CheckedChanged(object sender, EventArgs e)
        {
            //if (!this.chkNuevo.Checked)
            //{
            //    this.chkanadir.Checked = true;
            //}
            //else
            //{
            //    this.rowIndex = -1;
            //    this.chkanadir.Checked = false;
            //    this.dataGridView1.ClearSelection();
            //    this.panel1.Controls.Clear();
            //    this.pbImagenMarca.Image = null;
            //    this.cantidad = 0;
            //    this._seleccionado = false;
            //}
        }

        private void cmbAreaCuerpo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ValidaModeloDatosMarcas();
            if (this._seleccionado)
            {
                this.dataGridView1.ClearSelection();
                this.panel1.Controls.Clear();
                this.pbImagenMarca.Image = null;
                this.cantidad = 0;
                this._seleccionado = false;
            }
        }

        //private void cmbAreaCuerpoT_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.ValidaModeloDatosMarcas();
        //}

        //private void cmbColor_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.ValidaModeloDatosMarcas();
        //}

        //private void cmbMotivo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.ValidaModeloDatosMarcas();
        //}

        private void cmbTipoMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ValidaModeloDatosMarcas();
            if (this._seleccionado)
            {
                this.dataGridView1.ClearSelection();
                this.panel1.Controls.Clear();
                this.pbImagenMarca.Image = null;
                this.cantidad = 0;
                this._seleccionado = false;
            }
            Coder vCoder = (Coder)this.cmbTipoMarca.SelectedItem;
            if ((vCoder.Id == "BODYMARKTYPE" || vCoder.Id == "LEFTPROFILEMARKTYPE" ? false : vCoder.Id != "RIGHTPROFILEMARKTYPE"))
            {
                this.cmbAreaCuerpo.Enabled = true;
            }
            else
            {
                if (vCoder.Id == "BODYMARKTYPE")
                {
                    this.cmbAreaCuerpo.SelectedValue = "BODYAREA";
                }
                if ((vCoder.Id == "LEFTPROFILEMARKTYPE" ? true : vCoder.Id == "RIGHTPROFILEMARKTYPE"))
                {
                    this.cmbAreaCuerpo.SelectedValue = "FACEAREA";
                }
                this.cmbAreaCuerpo.Enabled = false;
            }
        }

        private void contextMenuStrip2_Click(object sender, EventArgs e)
        {
            try
            {
                this.cantidad = 0;
                int itemeliminado = 0;
                foreach (PictureBox item in this.panel1.Controls.OfType<PictureBox>())
                {
                    if (item.Name != this._nombrePB)
                    {
                        this.cantidad++;
                    }
                    else
                    {
                        this.panel1.Controls.Remove(item);
                        itemeliminado = this.cantidad;
                        break;
                    }
                }
                this.pbImagenMarca.Image = null;
                int contador = 0;
                foreach (PictureBox item in this.panel1.Controls.OfType<PictureBox>())
                {
                    if (contador >= itemeliminado)
                    {
                        Point location = item.Location;
                        int x = location.X - 100;
                        location = item.Location;
                        item.Location = new Point(x, location.Y);
                        item.Name = string.Concat("pb", contador.ToString());
                    }
                    contador++;
                }
                if (this.rowIndex != -1)
                {
                    this.Marcas[this.rowIndex].ListImages.RemoveAt(this.cantidad);
                    DataTable dt = (DataTable)this.dataGridView1.DataSource;
                    dt.Rows[this.rowIndex]["Cantidad"] = contador;
                    this.dataGridView1.DataSource = dt;
                    //       this.btnsiguiente.Enabled = this.ValidaGrilla(dt);
                }
                this.cantidad = contador;
                if (this.cantidad > itemeliminado)
                {
                    this.Pb_MouseClick(this.panel1.Controls.OfType<PictureBox>().ElementAt<PictureBox>(itemeliminado), new MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 1, 0, 0, 0));
                }
                else if (itemeliminado - 1 >= 0)
                {
                    this.Pb_MouseClick(this.panel1.Controls.OfType<PictureBox>().ElementAt<PictureBox>(itemeliminado - 1), new MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 1, 0, 0, 0));
                }
            }
            catch
            {
            }
        }

        //private void contextMenuStrip3_Click(object sender, EventArgs e)
        //{
        //    if (!this.dataGridView2.Rows[this.rowIndex].IsNewRow)
        //    {
        //        this.dataGridView2.Rows.RemoveAt(this.rowIndex);
        //        this.Tatuajes.RemoveAt(this.rowIndex);
        //        this.rowIndex = this.Tatuajes.Count - 1;
        //        this.CargaImagenesTatuajes();
        //    }
        //}

        //private void contextMenuStrip4_Click(object sender, EventArgs e)
        //{
        //    this.cantidad = 0;
        //    int itemeliminado = 0;
        //    foreach (PictureBox item in this.panel2.Controls.OfType<PictureBox>())
        //    {
        //        if (item.Name != this._nombrePB)
        //        {
        //            this.cantidad++;
        //        }
        //        else
        //        {
        //            this.panel2.Controls.Remove(item);
        //            itemeliminado = this.cantidad;
        //            break;
        //        }
        //    }
        //    this.pbImagenTatuaje.Image = null;
        //    int contador = 0;
        //    foreach (PictureBox item in this.panel2.Controls.OfType<PictureBox>())
        //    {
        //        if (contador >= itemeliminado)
        //        {
        //            Point location = item.Location;
        //            int x = location.X - 100;
        //            location = item.Location;
        //            item.Location = new Point(x, location.Y);
        //            item.Name = string.Concat("pbt", contador.ToString());
        //        }
        //        contador++;
        //    }
        //    this.Tatuajes[this.rowIndex].ListImages.RemoveAt(this.cantidad);
        //    this.cantidad = contador;
        //    DataTable dt = (DataTable)this.dataGridView2.DataSource;
        //    dt.Rows[this.rowIndex]["Cantidad"] = this.cantidad;
        //    this.dataGridView2.DataSource = dt;
        //    this.btnsiguiente.Enabled = this.ValidaGrilla(dt);
        //    if (this.cantidad > itemeliminado)
        //    {
        //        this.Pbt_MouseClick(this.panel2.Controls.OfType<PictureBox>().ElementAt<PictureBox>(itemeliminado), new MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 1, 0, 0, 0));
        //    }
        //    else if (itemeliminado - 1 >= 0)
        //    {
        //        this.Pbt_MouseClick(this.panel2.Controls.OfType<PictureBox>().ElementAt<PictureBox>(itemeliminado - 1), new MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 1, 0, 0, 0));
        //    }
        //}

        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.rowIndex = e.RowIndex;
            this.cmbAreaCuerpo.SelectedIndex = 0;
            this.cmbTipoMarca.SelectedIndex = 0;
            this._seleccionado = true;
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.dataGridView1.Rows[e.RowIndex].Selected = true;
                this.dataGridView1.CurrentCell = this.dataGridView1.Rows[e.RowIndex].Cells[0];
                this.contextMenuStrip1.Show(this.dataGridView1, e.Location);
                this.contextMenuStrip1.Show(System.Windows.Forms.Cursor.Position);
            }
            this.CargaImagenesMarcas();
         //   this.chkanadir.Checked = true;
        }

        //private void dataGridView2_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        //{
        //    this.rowIndex = e.RowIndex;
        //    if (e.Button == System.Windows.Forms.MouseButtons.Right)
        //    {
        //        this.dataGridView2.Rows[e.RowIndex].Selected = true;
        //        this.dataGridView2.CurrentCell = this.dataGridView2.Rows[e.RowIndex].Cells[0];
        //        this.contextMenuStrip3.Show(this.dataGridView2, e.Location);
        //        this.contextMenuStrip3.Show(System.Windows.Forms.Cursor.Position);
        //    }
        //    this.CargaImagenesTatuajes();
        //}

        protected override void Dispose(bool disposing)
        {
            if ((!disposing ? false : this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.dataGridView1.Rows[this.rowIndex].IsNewRow)
                {
                    this._seleccionado = false;
                    MarkData vMarca = this.Marcas[this.rowIndex];
                    this.cmbTipoMarca.SelectedValue = vMarca.Type.Id;
                    this.cmbAreaCuerpo.SelectedValue = vMarca.BodyArea.Id;
                    this.dataGridView1.Rows.RemoveAt(this.rowIndex);
                    this.Marcas.RemoveAt(this.rowIndex);
                    this.rowIndex = -1;
                    this.dataGridView1.ClearSelection();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.dataGridView1.Rows[this.rowIndex].IsNewRow)
                {
                    this.dataGridView1.Rows.RemoveAt(this.rowIndex);
                    this.Marcas.RemoveAt(this.rowIndex);
                    this.rowIndex = this.Marcas.Count - 1;
                    this.CargaImagenesMarcas();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void fMarcasTatuaje_Load(object sender, EventArgs e)
        {
            this.rowIndex = -1;
            if (vEnroll.PersonaCapturada == null)
            {
                vEnroll.PersonaCapturada = new CapturedPerson();
            }
            if (vEnroll.PersonaCapturada.RecordData == null)
            {
                vEnroll.PersonaCapturada.RecordData = new RecordData();
            }
            this.Marcas = vEnroll.PersonaCapturada.RecordData.MarkDatas;
            this.Tatuajes = vEnroll.PersonaCapturada.RecordData.TattooDatas;
            this.CargaTipoMarca();
            this.CargaAreaCuerpo();
            this.CargaMotivo();
            this.CargaColor();
            this.CargaPestanaMarcas();
         //   this.chkNuevo_CheckedChanged(sender, e);
        }

        //private Rectangle GetRectangle()
        //{
        //    this.rect = new Rectangle()
        //    {
        //        X = Math.Min(this.StartLocation.X, this.EndLcation.X),
        //        Y = Math.Min(this.StartLocation.Y, this.EndLcation.Y),
        //        Width = Math.Abs(this.StartLocation.X - this.EndLcation.X),
        //        Height = Math.Abs(this.StartLocation.Y - this.EndLcation.Y)
        //    };
        //    return this.rect;
        //}

        //public void HallaEscalaSaldo(PictureBox pbCaptura)
        //{
        //    this.imgInput = new Image<Bgr, byte>((Bitmap)pbCaptura.Image);
        //    System.Drawing.Size size = pbCaptura.Size;
        //    this.escala = (float)size.Height / (float)this.imgInput.Height;
        //    float nuevamedidax = (float)this.imgInput.Width * this.escala;
        //    float nuevamediday = (float)this.imgInput.Height * this.escala;
        //    if (nuevamedidax > (float)pbCaptura.Size.Width)
        //    {
        //        size = pbCaptura.Size;
        //        this.escala = (float)size.Width / (float)this.imgInput.Width;
        //        nuevamedidax = (float)this.imgInput.Height * this.escala;
        //        size = pbCaptura.Size;
        //        this.saldoY = ((float)size.Height - nuevamedidax) / 2f;
        //        this.saldoX = 0f;
        //    }
        //    else
        //    {
        //        size = pbCaptura.Size;
        //        this.saldoX = ((float)size.Width - nuevamedidax) / 2f;
        //        this.saldoY = 0f;
        //    }
        //}

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(vTattoos));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnfotoM = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.btnañadirM = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbAreaCuerpo = new System.Windows.Forms.ComboBox();
            this.cmbTipoMarca = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnCargarMarcas = new System.Windows.Forms.Button();
            this.pbImagenMarca = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          //  this.editarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.eliminarToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.btnAnterior = new System.Windows.Forms.Button();
            this.btnguardar = new System.Windows.Forms.Button();
            this.btncancelar = new System.Windows.Forms.Button();
            this.eliminarToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label13 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbImagenMarca)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.ItemSize = new System.Drawing.Size(47, 10);
            this.tabControl1.Location = new System.Drawing.Point(123, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(988, 579);
            this.tabControl1.TabIndex = 12;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(63)))), ((int)(((byte)(105)))));
            this.tabPage1.Controls.Add(this.btnfotoM);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.btnañadirM);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.cmbAreaCuerpo);
            this.tabPage1.Controls.Add(this.cmbTipoMarca);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Controls.Add(this.btnCargarMarcas);
            this.tabPage1.Controls.Add(this.pbImagenMarca);
            this.tabPage1.Location = new System.Drawing.Point(4, 14);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(980, 561);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Marcas";
            // 
            // btnfotoM
            // 
            this.btnfotoM.BackColor = System.Drawing.Color.White;
            this.btnfotoM.Image = ((System.Drawing.Image)(resources.GetObject("btnfotoM.Image")));
            this.btnfotoM.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnfotoM.Location = new System.Drawing.Point(171, 358);
            this.btnfotoM.Name = "btnfotoM";
            this.btnfotoM.Size = new System.Drawing.Size(91, 53);
            this.btnfotoM.TabIndex = 63;
            this.btnfotoM.Text = "Tomar Foto";
            this.btnfotoM.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnfotoM.UseVisualStyleBackColor = false;
            this.btnfotoM.Click += new System.EventHandler(this.btnfotoM_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(28, 417);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(933, 134);
            this.panel1.TabIndex = 62;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(870, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 13);
            this.label9.TabIndex = 61;
            this.label9.Text = "Añadir";
            // 
            // btnañadirM
            // 
            this.btnañadirM.BackColor = System.Drawing.Color.White;
            this.btnañadirM.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnañadirM.Location = new System.Drawing.Point(833, 24);
            this.btnañadirM.Name = "btnañadirM";
            this.btnañadirM.Size = new System.Drawing.Size(31, 23);
            this.btnañadirM.TabIndex = 60;
            this.btnañadirM.Text = "+";
            this.btnañadirM.UseVisualStyleBackColor = false;
            this.btnañadirM.Click += new System.EventHandler(this.BtnAddRow);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.Location = new System.Drawing.Point(610, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 59;
            this.label3.Text = "Area del Cuerpo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Location = new System.Drawing.Point(368, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 58;
            this.label2.Text = "Tipo de Adjunto";
            // 
            // cmbAreaCuerpo
            // 
            this.cmbAreaCuerpo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbAreaCuerpo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbAreaCuerpo.FormattingEnabled = true;
            this.cmbAreaCuerpo.Location = new System.Drawing.Point(613, 24);
            this.cmbAreaCuerpo.Name = "cmbAreaCuerpo";
            this.cmbAreaCuerpo.Size = new System.Drawing.Size(214, 21);
            this.cmbAreaCuerpo.TabIndex = 56;
            this.cmbAreaCuerpo.SelectedIndexChanged += new System.EventHandler(this.cmbAreaCuerpo_SelectedIndexChanged);
            // 
            // cmbTipoMarca
            // 
            this.cmbTipoMarca.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbTipoMarca.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbTipoMarca.FormattingEnabled = true;
            this.cmbTipoMarca.Location = new System.Drawing.Point(370, 24);
            this.cmbTipoMarca.Name = "cmbTipoMarca";
            this.cmbTipoMarca.Size = new System.Drawing.Size(238, 21);
            this.cmbTipoMarca.TabIndex = 55;
            this.cmbTipoMarca.SelectedIndexChanged += new System.EventHandler(this.cmbTipoMarca_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ColumnHeadersVisible = false;
            this.dataGridView1.Location = new System.Drawing.Point(369, 53);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(593, 358);
            this.dataGridView1.TabIndex = 53;
      //      this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseUp);
            // 
            // btnCargarMarcas
            // 
            this.btnCargarMarcas.BackColor = System.Drawing.Color.White;
            this.btnCargarMarcas.Image = ((System.Drawing.Image)(resources.GetObject("btnCargarMarcas.Image")));
            this.btnCargarMarcas.Location = new System.Drawing.Point(268, 358);
            this.btnCargarMarcas.Name = "btnCargarMarcas";
            this.btnCargarMarcas.Size = new System.Drawing.Size(91, 53);
            this.btnCargarMarcas.TabIndex = 10;
            this.btnCargarMarcas.Text = "Cargar Imagen";
            this.btnCargarMarcas.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCargarMarcas.UseVisualStyleBackColor = false;
            this.btnCargarMarcas.Click += new System.EventHandler(this.btnCargarMarcas_Click);
            // 
            // pbImagenMarca
            // 
            this.pbImagenMarca.BackColor = System.Drawing.Color.White;
            this.pbImagenMarca.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbImagenMarca.Location = new System.Drawing.Point(28, 8);
            this.pbImagenMarca.Margin = new System.Windows.Forms.Padding(6);
            this.pbImagenMarca.Name = "pbImagenMarca";
            this.pbImagenMarca.Size = new System.Drawing.Size(331, 346);
            this.pbImagenMarca.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbImagenMarca.TabIndex = 4;
            this.pbImagenMarca.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eliminarToolStripMenuItem,
            //this.editarToolStripMenuItem
            });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(118, 48);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // editarToolStripMenuItem
            // 
            //this.editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            //this.editarToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            //this.editarToolStripMenuItem.Text = "Editar";
            //this.editarToolStripMenuItem.Click += new System.EventHandler(this.editarToolStripMenuItem_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eliminarToolStripMenuItem1});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(118, 26);
            this.contextMenuStrip2.Click += new System.EventHandler(this.contextMenuStrip2_Click);
            // 
            // eliminarToolStripMenuItem1
            // 
            this.eliminarToolStripMenuItem1.Name = "eliminarToolStripMenuItem1";
            this.eliminarToolStripMenuItem1.Size = new System.Drawing.Size(117, 22);
            this.eliminarToolStripMenuItem1.Text = "Eliminar";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(63)))), ((int)(((byte)(105)))));
            this.panel3.Controls.Add(this.button1);
            this.panel3.Controls.Add(this.btnAnterior);
            this.panel3.Controls.Add(this.btnguardar);
            this.panel3.Controls.Add(this.btncancelar);
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(294, 75);
            this.panel3.TabIndex = 25;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.Location = new System.Drawing.Point(203, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 60);
            this.button1.TabIndex = 33;
            this.button1.Text = "Salir";
            this.button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnAnterior
            // 
            this.btnAnterior.BackColor = System.Drawing.Color.White;
            this.btnAnterior.Enabled = false;
            this.btnAnterior.Image = ((System.Drawing.Image)(resources.GetObject("btnAnterior.Image")));
            this.btnAnterior.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAnterior.Location = new System.Drawing.Point(347, 11);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new System.Drawing.Size(75, 42);
            this.btnAnterior.TabIndex = 1;
            this.btnAnterior.Text = "Anterior";
            this.btnAnterior.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAnterior.UseVisualStyleBackColor = false;
            this.btnAnterior.Visible = false;
            this.btnAnterior.Click += new System.EventHandler(this.btnanterior_Click);
            // 
            // btnguardar
            // 
            this.btnguardar.BackColor = System.Drawing.Color.White;
            this.btnguardar.Image = ((System.Drawing.Image)(resources.GetObject("btnguardar.Image")));
            this.btnguardar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnguardar.Location = new System.Drawing.Point(10, 11);
            this.btnguardar.Name = "btnguardar";
            this.btnguardar.Size = new System.Drawing.Size(80, 60);
            this.btnguardar.TabIndex = 28;
            this.btnguardar.Text = "Guardar";
            this.btnguardar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnguardar.UseVisualStyleBackColor = false;
            this.btnguardar.Click += new System.EventHandler(this.btnguardar_Click);
            // 
            // btncancelar
            // 
            this.btncancelar.BackColor = System.Drawing.Color.White;
            this.btncancelar.Image = ((System.Drawing.Image)(resources.GetObject("btncancelar.Image")));
            this.btncancelar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btncancelar.Location = new System.Drawing.Point(105, 11);
            this.btncancelar.Name = "btncancelar";
            this.btncancelar.Size = new System.Drawing.Size(92, 60);
            this.btncancelar.TabIndex = 26;
            this.btncancelar.Text = "Guardar y Salir";
            this.btncancelar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btncancelar.UseVisualStyleBackColor = false;
            this.btncancelar.Click += new System.EventHandler(this.btncancelar_Click);
            // 
            // eliminarToolStripMenuItem3
            // 
            this.eliminarToolStripMenuItem3.Name = "eliminarToolStripMenuItem3";
            this.eliminarToolStripMenuItem3.Size = new System.Drawing.Size(117, 22);
            this.eliminarToolStripMenuItem3.Text = "Eliminar";
            // 
            // eliminarToolStripMenuItem2
            // 
            this.eliminarToolStripMenuItem2.Name = "eliminarToolStripMenuItem2";
            this.eliminarToolStripMenuItem2.Size = new System.Drawing.Size(117, 22);
            this.eliminarToolStripMenuItem2.Text = "Eliminar";
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(63)))), ((int)(((byte)(105)))));
            this.tableLayoutPanel11.ColumnCount = 2;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel11.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel11.Controls.Add(this.pictureBox1, 1, 0);
            this.tableLayoutPanel11.Location = new System.Drawing.Point(-1, 25);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 1;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(1228, 92);
            this.tableLayoutPanel11.TabIndex = 18;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1125, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 86);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 26;
            this.pictureBox1.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(63)))), ((int)(((byte)(105)))));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.label13, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(-1, 98);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1236, 36);
            this.tableLayoutPanel1.TabIndex = 19;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(3, 6);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1230, 24);
            this.label13.TabIndex = 26;
            this.label13.Text = "ARCHIVOS ADJUNTOS";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(63)))), ((int)(((byte)(105)))));
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.Controls.Add(this.tabControl1, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(-1, 121);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1236, 585);
            this.tableLayoutPanel2.TabIndex = 20;
            // 
            // vTattoos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(63)))), ((int)(((byte)(105)))));
            this.ClientSize = new System.Drawing.Size(1227, 718);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel11);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimizeBox = false;
            this.Name = "vTattoos";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registro de Marcas y/o Tatuajes";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.fMarcasTatuaje_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbImagenMarca)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void LlenaMarcas()
        {
            Coder vTipoMarca = HelperLoadControl.ObtenerCoder(this.cmbTipoMarca, "MARKTYPE");
            Coder vAreaCuerpo = HelperLoadControl.ObtenerCoder(this.cmbAreaCuerpo, "BODYAREA");
            if (this.Marcas == null)
            {
                this.Marcas = new BindingList<MarkData>();
            }
            this.Marcas.Add(new MarkData()
            {
                BodyArea = vAreaCuerpo,
                Type = vTipoMarca,
                Name = string.Concat("Marca - ", vAreaCuerpo.Value, " - ", vTipoMarca.Value),
                ListImages = new BindingList<ImageData>()
            });
            foreach (PictureBox pb in this.panel1.Controls.OfType<PictureBox>())
            {
                this.Marcas[this.Marcas.Count - 1].ListImages.Add(new ImageData()
                {
                    NormalizedImageArr = this.ser.ImageToByteArray(pb.Image)
                });
                Thread.Sleep(500);
            }
        }

        //private void LlenaTatuajes()
        //{
        //    Coder vMotivo = HelperLoadControl.ObtenerCoder(this.cmbMotivo, "TATOOMOTIVE");
        //    Coder vAreaCuerpo = HelperLoadControl.ObtenerCoder(this.cmbAreaCuerpoT, "BODYAREA");
        //    Coder vColor = HelperLoadControl.ObtenerCoder(this.cmbColor, "TATOOCOLOR");
        //    if (this.Tatuajes == null)
        //    {
        //        this.Tatuajes = new BindingList<TattooData>();
        //    }
        //    this.Tatuajes.Add(new TattooData()
        //    {
        //        Motive = vMotivo,
        //        BodyArea = vAreaCuerpo,
        //        Color = vColor,
        //        Name = this.txtNombreT.Text.Trim(),
        //        ListImages = new BindingList<ImageData>()
        //    });
        //}

        private DataTable ObtenerDataTableMarcas()
        {
            DataTable table = new DataTable();
            table.Columns.Add("TipoMarca");
            table.Columns.Add("IdTipoMarca");
            table.Columns.Add("AreaCuerpo");
            table.Columns.Add("IdAreaCuerpo");
            table.Columns.Add("Cantidad");
            foreach (MarkData vItem in this.Marcas)
            {
                DataRow dr = table.NewRow();
                dr["TipoMarca"] = vItem.Type.Value;
                dr["IdTipoMarca"] = vItem.Type.Id;
                dr["AreaCuerpo"] = vItem.BodyArea.Value;
                dr["IdAreaCuerpo"] = vItem.BodyArea.Id;
                dr["Cantidad"] = vItem.ListImages.Count;
                table.Rows.Add(dr);
            }
            return table;
        }

        private DataTable ObtenerDataTableTatuajes()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Nombre");
            table.Columns.Add("Motivo");
            table.Columns.Add("IdMotivo");
            table.Columns.Add("AreaCuerpo");
            table.Columns.Add("IdAreaCuerpo");
            table.Columns.Add("Color");
            table.Columns.Add("IdColor");
            table.Columns.Add("Cantidad");
            foreach (TattooData vItem in this.Tatuajes)
            {
                DataRow dr = table.NewRow();
                dr["Nombre"] = vItem.Name;
                dr["Motivo"] = vItem.Motive.Value;
                dr["IdMotivo"] = vItem.Motive.Id;
                dr["AreaCuerpo"] = vItem.BodyArea.Value;
                dr["IdAreaCuerpo"] = vItem.BodyArea.Id;
                dr["Color"] = vItem.Color.Value;
                dr["IdColor"] = vItem.Color.Id;
                dr["Cantidad"] = vItem.ListImages.Count;
                table.Rows.Add(dr);
            }
            return table;
        }

        /// <summary>
        /// TipoMarca IdTipoMarca    AreaCuerpo  IdAreaCuerpo    Cantidad 
        /// Lunar	  MOLESMARKTYPE	 Abdomen	 BELLYAREA	     0

        /// creates a table with the selected elements 
        /// </summary>
        /// <returns> returns a new table with the selected elements, this table will be merged to the main table  </returns>
        public DataTable GetRowToBeAdded()
        {
            DataTable table = new DataTable();
            table.Columns.Add("TipoMarca");
            table.Columns.Add("IdTipoMarca");
            table.Columns.Add("AreaCuerpo");
            table.Columns.Add("IdAreaCuerpo");
            table.Columns.Add("Cantidad");
            DataRow dr = table.NewRow();
            dr["TipoMarca"] = this.cmbTipoMarca.SelectedItem.ToString();
            dr["IdTipoMarca"] = this.cmbTipoMarca.SelectedValue.ToString();
            dr["AreaCuerpo"] = this.cmbAreaCuerpo.SelectedItem.ToString();
            dr["IdAreaCuerpo"] = this.cmbAreaCuerpo.SelectedValue.ToString();
            dr["Cantidad"] = this.panel1.Controls.OfType<PictureBox>().Count<PictureBox>();
            table.Rows.Add(dr);
            //if (Convert.ToInt32(table.Rows[0]["Cantidad"]) == 0)
            //{
            //    this.chkanadir.Checked = true;
            //}
            return table;
        }

        //public DataTable ObtenerTablaTatuajes()
        //{
        //    DataTable table = new DataTable();
        //    table.Columns.Add("Nombre");
        //    table.Columns.Add("Motivo");
        //    table.Columns.Add("IdMotivo");
        //    table.Columns.Add("AreaCuerpo");
        //    table.Columns.Add("IdAreaCuerpo");
        //    table.Columns.Add("Color");
        //    table.Columns.Add("IdColor");
        //    table.Columns.Add("Cantidad");
        //    DataRow dr = table.NewRow();
        //    dr["Nombre"] = this.txtNombreT.Text;
        //    dr["Motivo"] = this.cmbMotivo.SelectedItem.ToString();
        //    dr["IdMotivo"] = this.cmbMotivo.SelectedValue.ToString();
        //    dr["AreaCuerpo"] = this.cmbAreaCuerpoT.SelectedItem.ToString();
        //    dr["IdAreaCuerpo"] = this.cmbAreaCuerpoT.SelectedValue.ToString();
        //    dr["Color"] = this.cmbColor.SelectedItem.ToString();
        //    dr["IdColor"] = this.cmbColor.SelectedValue.ToString();
        //    dr["Cantidad"] = 0;
        //    table.Rows.Add(dr);
        //    return table;
        //}

        private void Pb_MouseClick(object sender, MouseEventArgs e)
        {
            PictureBox vPictureBox = (PictureBox)sender;
            this._nombrePB = vPictureBox.Name;
            try
            {
                foreach (PictureBox item in this.panel1.Controls.OfType<PictureBox>())
                {
                    item.BackColor = Color.Transparent;
                }
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    if (vPictureBox.Image != null)
                    {
                        this.pbImagenMarca.Image = vPictureBox.Image;
                        this.contextMenuStrip2.Show(System.Windows.Forms.Cursor.Position);
                        this.pbImagenMarca.Enabled = false;
                        vPictureBox.BackColor = Color.Red;
                    }
                }
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    this.pbImagenMarca.Image = vPictureBox.Image;
                    this.pbImagenMarca.Enabled = false;
                    vPictureBox.BackColor = Color.Red;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        //private void pb_MouseDown(object sender, MouseEventArgs e)
        //{
        //    this._selecting = true;
        //    this.StartLocation = e.Location;
        //}

        //private void pb_MouseMove(object sender, MouseEventArgs e)
        //{
        //    if (this._selecting)
        //    {
        //        this.EndLcation = e.Location;
        //        ((PictureBox)sender).Invalidate();
        //    }
        //}

        //private void pb_MouseUp(object sender, MouseEventArgs e)
        //{
        //    try
        //    {
        //        if ((e.Button != System.Windows.Forms.MouseButtons.Left ? false : this._selecting))
        //        {
        //            this.EndLcation = e.Location;
        //            this._selecting = false;
        //            Rectangle rectangle = this.rect;
        //            //if (false)
        //            //{
        //            //	return;
        //            //}else

        //            if ((this.rect.Width <= 0 ? false : this.rect.Height > 0))
        //            {
        //                PictureBox vPictureBox = (PictureBox)sender;
        //                this.HallaEscalaSaldo(vPictureBox);
        //                float nuevoX = (float)this.rect.X - this.saldoX;
        //                float nuevoY = (float)this.rect.Y - this.saldoY;
        //                nuevoX /= this.escala;
        //                nuevoY /= this.escala;
        //                this.rect.X = Convert.ToInt32(Math.Round((double)nuevoX, 0));
        //                this.rect.Y = Convert.ToInt32(Math.Round((double)nuevoY, 0));
        //                this.rect.Width = Convert.ToInt32(Math.Round((double)((float)this.rect.Width / this.escala), 0));
        //                this.rect.Height = Convert.ToInt32(Math.Round((double)((float)this.rect.Height / this.escala), 0));
        //                this.imgInput.ROI = this.rect;
        //                this.temp = this.imgInput.Clone();
        //                vPictureBox.Image = this.temp.Bitmap;
        //                PictureBox p = new PictureBox();
        //                DataTable dt = new DataTable();
        //                string name = vPictureBox.Name;
        //                if (name != null)
        //                {
        //                    if (name == "pbImagenMarca")
        //                    {
        //                        p.Name = string.Concat("pb", this.cantidad.ToString());
        //                        p.Image = this.imgInput.Bitmap;
        //                        p.Location = new Point(20 + this.cantidad * 100, 20);
        //                        p.Size = new System.Drawing.Size(90, 90);
        //                        p.SizeMode = PictureBoxSizeMode.Zoom;
        //                        p.BorderStyle = BorderStyle.FixedSingle;
        //                        p.MouseClick += new MouseEventHandler(this.Pb_MouseClick);
        //                        this.panel1.Controls.Add(p);
        //                        this.Pb_MouseClick(this.panel1.Controls.OfType<PictureBox>().ElementAt<PictureBox>(this.cantidad), new MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 1, 0, 0, 0));
        //                        this.cantidad++;
        //                        dt = (DataTable)this.dataGridView1.DataSource;
        //                        dt.Rows[this.rowIndex]["Cantidad"] = this.cantidad;
        //                        this.dataGridView1.DataSource = dt;
        //                        // this.btnsiguiente.Enabled = this.ValidaGrilla(dt);
        //                        this.Marcas[this.rowIndex].ListImages.Add(new ImageData()
        //                        {
        //                            NormalizedImageArr = this.ser.ImageToByteArray(this.imgInput.Bitmap)
        //                        });
        //                        this.pbImagenMarca.Enabled = false;
        //                    }
        //                    else if (name == "pbImagenTatuaje")
        //                    {
        //                        p.Name = string.Concat("pbt", this.cantidad.ToString());
        //                        p.Image = this.imgInput.Bitmap;
        //                        p.Location = new Point(20 + this.cantidad * 100, 20);
        //                        p.Size = new System.Drawing.Size(90, 90);
        //                        p.SizeMode = PictureBoxSizeMode.Zoom;
        //                        p.BorderStyle = BorderStyle.FixedSingle;
        //                        p.MouseClick += new MouseEventHandler(this.Pbt_MouseClick);
        //                        this.panel2.Controls.Add(p);
        //                        this.Pbt_MouseClick(this.panel2.Controls.OfType<PictureBox>().ElementAt<PictureBox>(this.cantidad), new MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 1, 0, 0, 0));
        //                        this.cantidad++;
        //                        //  dt = (DataTable)this.dataGridView2.DataSource;
        //                        dt.Rows[this.rowIndex]["Cantidad"] = this.cantidad;
        //                        //  this.dataGridView2.DataSource = dt;
        //                        //     this.btnsiguiente.Enabled = this.ValidaGrilla(dt);
        //                        this.Tatuajes[this.rowIndex].ListImages.Add(new ImageData()
        //                        {
        //                            NormalizedImageArr = this.ser.ImageToByteArray(this.imgInput.Bitmap)
        //                        });
        //                        this.pbImagenTatuaje.Enabled = false;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                return;
        //            }
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        Console.WriteLine($"Error: {exception}");
        //    }
        //}

        //private void pbImagenMarca_Paint(object sender, PaintEventArgs e)
        //{
        //    if (this._selecting)
        //    {
        //        e.Graphics.DrawRectangle(Pens.Red, this.GetRectangle());
        //    }
        //}

        //private void pbImagenTatuaje_Paint(object sender, PaintEventArgs e)
        //{
        //    if (this._selecting)
        //    {
        //        e.Graphics.DrawRectangle(Pens.Red, this.GetRectangle());
        //    }
        //}

        //private void Pbt_MouseClick(object sender, MouseEventArgs e)
        //{
        //    PictureBox vPictureBox = (PictureBox)sender;
        //    this._nombrePB = vPictureBox.Name;
        //    try
        //    {
        //        foreach (PictureBox item in this.panel2.Controls.OfType<PictureBox>())
        //        {
        //            item.BackColor = Color.Transparent;
        //        }
        //        if (e.Button == System.Windows.Forms.MouseButtons.Right)
        //        {
        //            if (vPictureBox.Image != null)
        //            {
        //                this.pbImagenTatuaje.Image = vPictureBox.Image;
        //                //      this.contextMenuStrip4.Show(System.Windows.Forms.Cursor.Position);
        //                this.pbImagenTatuaje.Enabled = false;
        //                vPictureBox.BackColor = Color.Red;
        //            }
        //        }
        //        if (e.Button == System.Windows.Forms.MouseButtons.Left)
        //        {
        //            this.pbImagenTatuaje.Image = vPictureBox.Image;
        //            this.pbImagenTatuaje.Enabled = false;
        //            vPictureBox.BackColor = Color.Red;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        MessageBox.Show(exception.Message);
        //    }
        //}

        //private void txtNombre_TextChanged(object sender, EventArgs e)
        //{
        //    this.ValidaModeloDatosMarcas();
        //}

        //private void TxtNombreT_TextChanged(object sender, EventArgs e)
        //{
        //    this.ValidaModeloDatosMarcas();
        //}

        //public bool ValidaGrilla(DataTable pDT)
        //{
        //    bool flag;
        //    bool vEsValido = true;
        //    foreach (DataRow vFila in pDT.Rows)
        //    {
        //        if (Convert.ToInt32(vFila["Cantidad"]) == 0)
        //        {
        //            flag = false;
        //            return flag;
        //        }
        //    }
        //    flag = vEsValido;
        //    return flag;
        //}

        private bool ValidaModeloDatosMarcas()
        {
            return HelperValidatorField.ValidarCampos(new DatosMarcas()
            {
                cmbTipoMarca = (this.cmbTipoMarca.SelectedValue == null ? "" : this.cmbTipoMarca.SelectedValue.ToString()),
                cmbAreaCuerpo = (this.cmbAreaCuerpo.SelectedValue == null ? "" : this.cmbAreaCuerpo.SelectedValue.ToString())
            }, this.errorProvider1, this);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //private bool ValidaModeloDatosTatuajes()
        //{
        //    return HelperValidatorField.ValidarCampos(new DatosTatuajes()
        //    {
        //        txtNombreT = this.txtNombreT.Text,
        //        cmbMotivo = (this.cmbMotivo.SelectedValue == null ? "" : this.cmbMotivo.SelectedValue.ToString()),
        //        cmbAreaCuerpo = (this.cmbAreaCuerpoT.SelectedValue == null ? "" : this.cmbAreaCuerpoT.SelectedValue.ToString()),
        //        cmbColor = (this.cmbColor.SelectedValue == null ? "" : this.cmbColor.SelectedValue.ToString())
        //    }, this.errorProvider1, this);
        //}
    }
}