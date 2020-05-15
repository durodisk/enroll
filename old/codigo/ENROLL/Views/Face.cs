using Datys.Enroll.Core;
using Emgu.CV;
using Emgu.CV.Structure;
using ENROLL.Helpers;
using ENROLL.Model;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ENROLL
{
    public class vFace : Form
    {
        private HelperSerializer ser = new HelperSerializer();

        private CapturedPerson PersonaCapturada = new CapturedPerson();

        private bool _selecting;

        private Image<Bgr, byte> imgInput;

        private Image<Bgr, byte> temp;

        private float escala;

        private float saldoX;

        private float saldoY;

        private Point StartLocation;

        private Point EndLcation;

        private Rectangle rect;

        public bool ForceClose = false;

        public bool nuevofrontal = false;

        private Bitmap BitmapRostroFrontalCropRegla;

        private Bitmap BitmapRostroFrontalCropReglaAux;

        private Bitmap BitmapRostroFrontal;

        private Bitmap BitmapRostroFrontalCrop;

        private int AuxNuevaImagen = 0;


        private Bitmap BitmapPerfilDerechoCropRegla;

        private IContainer components = null;

        private TabPage tabPage0;

        private Label label12;

        private NumericUpDown numEstatura;

        private Label label16;

        private TextBox txtid;

        private Panel panel1;

        private TabPage tabPage1;

        private Button btnCargarFrontal;

        private PictureBox pbCapturaFrontal;

        private TabPage tabPage2;

        private Button btnAyuda;

        private Button btnguardar;

        private Button btnsiguiente;

        private Button btncancelar;

        private TabPage tabPage3;

        private PictureBox pbFrontalRegla;

        private ErrorProvider errorProvider1;

        private Label label7;

        private Label label8;

        private DataGridView dataGridView2;

        private DataGridViewTextBoxColumn Detalle2;

        private DataGridViewTextBoxColumn Valor2;

        private DataGridView dataGridView1;

        private DataGridViewTextBoxColumn Detalle;

        private DataGridViewTextBoxColumn Valor;

        private PictureBox pbImagenRecorte;

        private Button btnFoto;

        private Button btnAnterior;

        private TabControl tabCapturaFacial;

        private TableLayoutPanel tableLayoutPanel2;

        private TableLayoutPanel tableLayoutPanel1;

        private Label label1;

        private TableLayoutPanel tableLayoutPanel11;

        private TableLayoutPanel tableLayoutPanel4;

        private TableLayoutPanel tableLayoutPanel3;

        private Label label13;

        private TableLayoutPanel tableLayoutPanel5;

        private Label label2;

        private TableLayoutPanel tableLayoutPanel12;

        private TableLayoutPanel tableLayoutPanel6;

        private TableLayoutPanel tableLayoutPanel8;

        private TableLayoutPanel tableLayoutPanel7;

        private Label label4;

        private TableLayoutPanel tableLayoutPanel10;

        private TableLayoutPanel tableLayoutPanel9;

        private Label label5;

        private Panel panel2;

        private PictureBox pictureBox1;

        private Panel panel4;

        private MyIface myIface = new MyIface();

        public vFace()
        {
            this.InitializeComponent();
        }

        private void ActualizarRecordDataBodyFace()
        {
            try
            {
                if (this.PersonaCapturada == null)
                {
                    this.PersonaCapturada = new CapturedPerson();
                }
                if (this.PersonaCapturada.BasicData == null)
                {
                    this.PersonaCapturada.BasicData = new LightPersonBasicData();
                }
                if (this.PersonaCapturada.RecordData == null)
                {
                    this.PersonaCapturada.RecordData = new RecordData();
                }
                if (this.PersonaCapturada.RecordData.Body == null)
                {
                    this.PersonaCapturada.RecordData.Body = new FaceData();
                }
                if (this.PersonaCapturada.RecordData.FaceFrontal == null)
                {
                    this.PersonaCapturada.RecordData.FaceFrontal = new FaceData();
                }
                if (this.BitmapRostroFrontal != null)
                {
                    this.PersonaCapturada.RecordData.FaceFrontal.OriginalImageArr = this.ser.ImagenToJPGByteArray((Bitmap)this.ser.CambiarTamanoImagen(this.BitmapRostroFrontal, 1000));
                }
                if (this.BitmapRostroFrontalCrop != null)
                {
                    this.PersonaCapturada.RecordData.FaceFrontal.NormalizedImageArr = this.ser.ImagenToJPGByteArray((Bitmap)this.ser.CambiarTamanoImagen(this.BitmapRostroFrontalCrop, 1500));
                    this.PersonaCapturada.BasicData.PhotoArr = this.ser.RedimensionarByteArray(this.ser.ImageToByteArray((Bitmap)this.ser.CambiarTamanoImagen(this.BitmapRostroFrontalCrop, 500)), 48);
                }
                if (this.BitmapRostroFrontalCropRegla != null)
                {
                    this.PersonaCapturada.RecordData.FaceFrontal.RuledImageArr = this.ser.ImagenToJPGByteArray((Bitmap)this.ser.CambiarTamanoImagen(this.BitmapRostroFrontalCropRegla, 1000));
                }
                vEnroll.PersonaCapturada = this.PersonaCapturada;
                if (this.txtid.Text != this.PersonaCapturada.OfflinePerson.Identities[0].Identification)
                {
                    this.PersonaCapturada.OfflinePerson.Identities[0].Identification = this.txtid.Text;
                }
                if (this.numEstatura.Value != Convert.ToDecimal(this.PersonaCapturada.RecordData.BodySize))
                {
                    this.PersonaCapturada.RecordData.BodySize = Convert.ToInt32(this.numEstatura.Value);
                }
                (new HelperSerializer()).SerializeEpd(this.PersonaCapturada, vContainerMain.RutaEpd);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void ActualizarRecordDataBodyFace2(string newFolderName)
        {
            try
            {
                if (Directory.Exists(newFolderName))
                {
                    if (this.PersonaCapturada == null)
                    {
                        this.PersonaCapturada = new CapturedPerson();
                    }
                    if (this.PersonaCapturada.BasicData == null)
                    {
                        this.PersonaCapturada.BasicData = new LightPersonBasicData();
                    }
                    if (this.PersonaCapturada.RecordData == null)
                    {
                        this.PersonaCapturada.RecordData = new RecordData();
                    }
                    string directoryName = Path.GetDirectoryName(Application.ExecutablePath);
                    string str = Path.DirectorySeparatorChar.ToString();
                    char directorySeparatorChar = Path.DirectorySeparatorChar;
                    string ruta = string.Concat(directoryName, str, "Rostro", directorySeparatorChar.ToString());
                    if (this.PersonaCapturada.RecordData.Body == null)
                    {
                        this.PersonaCapturada.RecordData.Body = new FaceData();
                    }
                    if (File.Exists(string.Concat(ruta, "CuerpoOriginal.bmp")))
                    {
                        this.PersonaCapturada.RecordData.Body.OriginalImageArr = File.ReadAllBytes(string.Concat(ruta, "CuerpoOriginal.bmp"));
                    }
                    if (File.Exists(string.Concat(ruta, "CuerpoNormal.bmp")))
                    {
                        this.PersonaCapturada.RecordData.Body.NormalizedImageArr = File.ReadAllBytes(string.Concat(ruta, "CuerpoNormal.bmp"));
                    }
                    if (File.Exists(string.Concat(ruta, "CuerpoRegla.bmp")))
                    {
                        this.PersonaCapturada.RecordData.Body.RuledImageArr = File.ReadAllBytes(string.Concat(ruta, "CuerpoRegla.bmp"));
                    }
                    if (this.PersonaCapturada.RecordData.FaceFrontal == null)
                    {
                        this.PersonaCapturada.RecordData.FaceFrontal = new FaceData();
                    }
                    if (this.BitmapRostroFrontal != null)
                    {
                        this.PersonaCapturada.RecordData.FaceFrontal.OriginalImageArr = this.ser.ImagenToPNGByteArray(this.BitmapRostroFrontal);
                    }
                    if (this.BitmapRostroFrontalCrop != null)
                    {
                        this.PersonaCapturada.RecordData.FaceFrontal.NormalizedImageArr = this.ser.ImagenToPNGByteArray(this.BitmapRostroFrontalCrop);
                        this.PersonaCapturada.BasicData.PhotoArr = this.ser.RedimensionarByteArray(this.ser.ImageToByteArray(this.BitmapRostroFrontalCrop), 48);
                    }
                    if (this.BitmapRostroFrontalCropRegla != null)
                    {
                        this.PersonaCapturada.RecordData.FaceFrontal.RuledImageArr = this.ser.ImagenToPNGByteArray(this.BitmapRostroFrontalCropRegla);
                    }
                    if (this.PersonaCapturada.RecordData.FaceRightProfile == null)
                    {
                        this.PersonaCapturada.RecordData.FaceRightProfile = new FaceData();
                    }
                    if (File.Exists(string.Concat(ruta, "PerfilDerOriginal.bmp")))
                    {
                        this.PersonaCapturada.RecordData.FaceRightProfile.OriginalImageArr = File.ReadAllBytes(string.Concat(ruta, "PerfilDerOriginal.bmp"));
                    }
                    if (File.Exists(string.Concat(ruta, "PerfilDerNormal.bmp")))
                    {
                        this.PersonaCapturada.RecordData.FaceRightProfile.NormalizedImageArr = File.ReadAllBytes(string.Concat(ruta, "PerfilDerNormal.bmp"));
                    }
                    if (File.Exists(string.Concat(ruta, "PerfilDerRegla.bmp")))
                    {
                        this.PersonaCapturada.RecordData.FaceRightProfile.RuledImageArr = File.ReadAllBytes(string.Concat(ruta, "PerfilDerRegla.bmp"));
                    }
                    if (this.PersonaCapturada.RecordData.FaceLeftProfile == null)
                    {
                        this.PersonaCapturada.RecordData.FaceLeftProfile = new FaceData();
                    }
                    if (File.Exists(string.Concat(ruta, "PerfilIzqOriginal.bmp")))
                    {
                        this.PersonaCapturada.RecordData.FaceLeftProfile.OriginalImageArr = File.ReadAllBytes(string.Concat(ruta, "PerfilIzqOriginal.bmp"));
                    }
                    if (File.Exists(string.Concat(ruta, "PerfilIzqNormal.bmp")))
                    {
                        this.PersonaCapturada.RecordData.FaceLeftProfile.NormalizedImageArr = File.ReadAllBytes(string.Concat(ruta, "PerfilIzqNormal.bmp"));
                    }
                    if (File.Exists(string.Concat(ruta, "PerfilIzqRegla.bmp")))
                    {
                        this.PersonaCapturada.RecordData.FaceLeftProfile.RuledImageArr = File.ReadAllBytes(string.Concat(ruta, "PerfilIzqRegla.bmp"));
                    }
                    vEnroll.PersonaCapturada = this.PersonaCapturada;
                    if (this.txtid.Text != this.PersonaCapturada.OfflinePerson.Identities[0].Identification)
                    {
                        this.PersonaCapturada.OfflinePerson.Identities[0].Identification = this.txtid.Text;
                    }
                    if (this.numEstatura.Value != Convert.ToDecimal(this.PersonaCapturada.RecordData.BodySize))
                    {
                        this.PersonaCapturada.RecordData.BodySize = Convert.ToInt32(this.numEstatura.Value);
                    }
                    (new HelperSerializer()).SerializeEpd(this.PersonaCapturada, vContainerMain.RutaEpd);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
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

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.tabCapturaFacial.SelectedIndex == 1)
                {
                    this.btnAnterior.Enabled = false;
                }
                this.tabCapturaFacial.SelectTab(this.tabCapturaFacial.SelectedIndex - 1);
                this.btnsiguiente.Enabled = true;
                this.btnsiguiente.Text = "Siguiente";
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void btncancelar_Click(object sender, EventArgs e)
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



        private void btnCargarFrontal_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                {
                    this.btnsiguiente.Enabled = false;
                }
                else
                {
                    this.AuxNuevaImagen = 1;
                    this.BitmapRostroFrontal = null;
                    this.BitmapRostroFrontalCrop = null;
                    this.BitmapRostroFrontalCropReglaAux = null;
                    this.BitmapRostroFrontalCropRegla = null;
                    this.pbCapturaFrontal.Image = null;
                    this.pbImagenRecorte.Image = null;
                    this.pbFrontalRegla.Image = null;
                    this.BitmapRostroFrontal = new Bitmap(ofd.FileName);
                    this.pbCapturaFrontal.Image = this.BitmapRostroFrontal;
                    this.pbCapturaFrontal.Enabled = true;
                    bool num = true;
                    bool flag = (bool)num;
                    this.btnsiguiente.Enabled = (bool)num;
                    this.nuevofrontal = flag;
                }
            }
            catch
            {
            }
            System.Windows.Forms.Cursor.Current = Cursors.Default;
        }



        private void btnFoto_Click(object sender, EventArgs e)
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
                    this.AuxNuevaImagen = 1;
                    this.BitmapRostroFrontal = null;
                    this.BitmapRostroFrontalCrop = null;
                    this.BitmapRostroFrontalCropReglaAux = null;
                    this.BitmapRostroFrontalCropRegla = null;
                    this.pbCapturaFrontal.Image = null;
                    this.pbImagenRecorte.Image = null;
                    this.pbFrontalRegla.Image = null;
                    this.BitmapRostroFrontal = vPicture.Captura;
                    this.pbCapturaFrontal.Image = this.BitmapRostroFrontal;
                    bool num = true;
                    bool flag = (bool)num;
                    this.pbCapturaFrontal.Enabled = (bool)num;
                    this.nuevofrontal = flag;
                }
                this.btnsiguiente.Enabled = this.pbCapturaFrontal.Image != null;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
                this.ActualizarRecordDataBodyFace();
                System.Windows.Forms.Cursor.Current = Cursors.Default;
                this.Alerta("Mensaje", "Se guardaron los datos satisfactoriamente", false);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void btnsiguiente_Click(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
                switch (this.tabCapturaFacial.SelectedIndex)
                {
                    case 0:
                        {
                            if (this.ValidaModeloDatosGenerales())
                            {
                                this.btnAnterior.Enabled = true;
                                this.tabCapturaFacial.SelectTab(1);
                            }
                            break;
                        }
                    case 1:
                        {
                            this.CargaPestanaCalidaFrontal();
                            this.tabCapturaFacial.SelectTab(2);
                            break;
                        }
                    case 2:
                        {
                            this.CargaPestanaReglaFrontal();
                            this.btnsiguiente.Enabled = true;
                            this.btnsiguiente.Text = "Finalizar";
                            if (this.BitmapRostroFrontalCropRegla == null)
                            {
                                this.btnsiguiente.Enabled = false;
                            }
                            this.tabCapturaFacial.SelectTab(3);
                            break;
                        }
                    case 3:
                        {
                            this.ActualizarRecordDataBodyFace();
                            this.ForceClose = true;
                            if (this.Alerta("Mensaje", "¿ Esta seguro de Salir ? ", true))
                            {
                                (new vEnroll()
                                {
                                    MdiParent = base.ParentForm
                                }).Show();
                                base.Close();
                            }
                            break;
                        }

                    case 6:
                        {
                            this.tabCapturaFacial.SelectTab(7);
                            //  this.CargaResumen();
                            this.btnsiguiente.Text = "Finalizar";
                            break;
                        }
                    case 7:
                        {
                            this.ActualizarRecordDataBodyFace();
                            this.ForceClose = true;
                            base.Close();
                            break;
                        }
                }
                System.Windows.Forms.Cursor.Current = Cursors.Default;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void CargaPestana(Button btnOriginal, PictureBox pb, string pNombreArchivo, FaceData pFaceData)
        {
            try
            {
                bool num = false;
                bool flag = (bool)num;
                this.btnsiguiente.Enabled = (bool)num;
                btnOriginal.Enabled = flag;
                if (File.Exists(string.Concat("Rostro/", pNombreArchivo, "Regla.bmp")))
                {
                    this.imgInput = new Image<Bgr, byte>(string.Concat("Rostro/", pNombreArchivo, "Regla.bmp"));
                    pb.Image = this.imgInput.Bitmap;
                    bool num1 = true;
                    flag = (bool)num1;
                    this.btnsiguiente.Enabled = (bool)num1;
                    btnOriginal.Enabled = flag;
                }
                else if (File.Exists(string.Concat("Rostro/", pNombreArchivo, "Normal.bmp")))
                {
                    this.imgInput = new Image<Bgr, byte>(string.Concat("Rostro/", pNombreArchivo, "Normal.bmp"));
                    pb.Image = this.imgInput.Bitmap;
                    btnOriginal.Enabled = true;
                }
                else if (File.Exists(string.Concat("Rostro/", pNombreArchivo, "Original.bmp")))
                {
                    this.imgInput = new Image<Bgr, byte>(string.Concat("Rostro/", pNombreArchivo, "Original.bmp"));
                    pb.Image = this.imgInput.Bitmap;
                }
                if ((pb.Image != null ? true : pFaceData == null))
                {
                    if (pb.Image != null)
                    {
                        this.btnsiguiente.Enabled = true;
                    }
                }
                else if (pFaceData.RuledImage != null)
                {
                    this.imgInput = new Image<Bgr, byte>(pFaceData.RuledImage);
                    pb.Image = this.imgInput.Bitmap;
                    bool num2 = true;
                    flag = (bool)num2;
                    this.btnsiguiente.Enabled = (bool)num2;
                    btnOriginal.Enabled = flag;
                }
                else if (pFaceData.NormalizedImage != null)
                {
                    this.imgInput = new Image<Bgr, byte>(pFaceData.NormalizedImage);
                    pb.Image = this.imgInput.Bitmap;
                    btnOriginal.Enabled = true;
                }
                else if (pFaceData.OriginalImage != null)
                {
                    this.imgInput = new Image<Bgr, byte>(pFaceData.OriginalImage);
                    pb.Image = this.imgInput.Bitmap;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void CargaPestanaCalidaFrontal()
        {
            try
            {
                DataTable dtImg = myIface.GetFaceData(this.BitmapRostroFrontal);
                int ImageCalidadPorcentaje = (int)myIface.FaceScore;

                this.dataGridView1.Visible = false;
                this.dataGridView2.Visible = false;
                this.label8.Text = "";
                this.label7.Text = "";
                if (dtImg.Rows.Count <= 0)
                {
                    this.BitmapRostroFrontalCrop = this.BitmapRostroFrontal;
                    this.pbImagenRecorte.Image = this.BitmapRostroFrontalCrop;
                    this.btnsiguiente.Enabled = true;
                    this.label8.Text = "La imagen no cumple con lo requerido.\nSin embargo Ud. puede decidir continuar con el proceso.";
                }
                else if (ImageCalidadPorcentaje < 10)
                {
                    this.BitmapRostroFrontalCrop = this.BitmapRostroFrontal;
                    this.pbImagenRecorte.Image = this.BitmapRostroFrontalCrop;
                    this.btnsiguiente.Enabled = true;
                    this.label8.Text = "La imagen no cumple con lo requerido.\nSin embargo Ud. puede decidir continuar con el proceso.";
                }
                else
                {
                    DataRow[] dr1 = dtImg.Select("Tipo = 'Rasgos'");
                    DataRow[] dr2 = dtImg.Select("Tipo = 'Atributos'");
                    DataTable dt1 = dr1.CopyToDataTable<DataRow>();
                    DataTable dt2 = dr2.CopyToDataTable<DataRow>();
                    string[] TobeDistinct = new string[] { "Detalle", "Valor" };
                    DataTable dtRostro = vFace.GetDistinctRecords(dt1, TobeDistinct);
                    DataTable dtAtribtos = vFace.GetDistinctRecords(dt2, TobeDistinct);
                    this.dataGridView1.DataSource = dtRostro;
                    this.dataGridView2.DataSource = dtAtribtos;
                    this.dataGridView1.Visible = true;
                    this.dataGridView2.Visible = true;
                    this.label8.Text = "RASGOS FACIALES";
                    this.label7.Text = "ATRIBUTOS FACIALES";
                    PictureBox vPictureBox = new PictureBox();
                    this.BitmapRostroFrontalCrop = (Bitmap)myIface.CroppedImg;
                    this.pbImagenRecorte.Image = this.BitmapRostroFrontalCrop;
                    this.btnsiguiente.Enabled = true;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                this.BitmapRostroFrontalCrop = this.BitmapRostroFrontal;
                this.pbImagenRecorte.Image = this.BitmapRostroFrontalCrop;
                this.btnsiguiente.Enabled = true;
                this.label8.Text = "La imagen no cumple con lo requerido.\nSin embargo Ud. puede decidir continuar con el proceso.";
            }
        }



        private void CargaPestanaFotoFrontal()
        {
            try
            {
                if (this.pbCapturaFrontal.Image != null)
                {
                    this.btnsiguiente.Enabled = true;
                }
                else
                {
                    this.btnsiguiente.Enabled = false;
                }
                if (this.PersonaCapturada != null)
                {
                    if (this.PersonaCapturada.RecordData != null)
                    {
                        if (this.PersonaCapturada.RecordData.FaceFrontal != null)
                        {
                            if (this.PersonaCapturada.RecordData.FaceFrontal.OriginalImage != null)
                            {
                                this.BitmapRostroFrontal = this.PersonaCapturada.RecordData.FaceFrontal.OriginalImage;
                                this.pbCapturaFrontal.Image = this.BitmapRostroFrontal;
                                this.btnsiguiente.Enabled = true;
                            }
                        }
                    }
                }
                else if (this.pbCapturaFrontal.Image != null)
                {
                    this.btnsiguiente.Enabled = true;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }


        private void CargaPestanaReglaFrontal()
        {
            try
            {
                if (this.AuxNuevaImagen == 1)
                {
                    if (this.BitmapRostroFrontalCropRegla == null)
                    {
                        this.pbFrontalRegla.Image = this.BitmapRostroFrontalCrop;
                    }
                    else
                    {
                        this.pbFrontalRegla.Image = this.BitmapRostroFrontalCropReglaAux;
                    }
                }
                else if ((this.pbFrontalRegla.Image != null ? false : this.PersonaCapturada.RecordData != null))
                {
                    if (this.PersonaCapturada.RecordData.FaceFrontal != null)
                    {
                        if (this.PersonaCapturada.RecordData.FaceFrontal.RuledImageArr != null)
                        {
                            this.pbFrontalRegla.Image = this.ser.byteArrayToImage(this.PersonaCapturada.RecordData.FaceFrontal.RuledImageArr);
                            this.btnsiguiente.Enabled = true;
                        }
                        else if (this.PersonaCapturada.RecordData.FaceFrontal.NormalizedImageArr != null)
                        {
                            this.pbFrontalRegla.Image = this.ser.byteArrayToImage(this.PersonaCapturada.RecordData.FaceFrontal.NormalizedImageArr);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }


        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dataGridView1.Columns[e.ColumnIndex].Name == "Valor")
                {
                    if (e.Value.ToString() == "0")
                    {
                        e.CellStyle.BackColor = Color.Red;
                        e.Value = "No Detectado";
                    }
                    if (e.Value.ToString() == "1")
                    {
                        e.CellStyle.BackColor = Color.Yellow;
                        e.Value = "Moderado";
                    }
                    if (e.Value.ToString() == "2")
                    {
                        e.CellStyle.BackColor = Color.GreenYellow;
                        e.Value = "Confiable";
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dataGridView2.Columns[e.ColumnIndex].Name == "Valor2")
                {
                    string valorreal = e.Value.ToString();
                    string valor_sub = valorreal.Substring(0, 1);
                    string valor_grilla = valorreal.Substring(1);
                    if (valor_sub == "E")
                    {
                        e.CellStyle.BackColor = Color.Red;
                        e.Value = " Malo";
                    }
                    if (valor_sub == "0")
                    {
                        e.CellStyle.BackColor = Color.Red;
                        e.Value = valor_grilla;
                    }
                    if (valor_sub == "1")
                    {
                        e.CellStyle.BackColor = Color.Yellow;
                        e.Value = valor_grilla;
                    }
                    if (valor_sub == "2")
                    {
                        e.CellStyle.BackColor = Color.GreenYellow;
                        e.Value = valor_grilla;
                    }
                    if (valor_sub == "9")
                    {
                        e.Value = valor_grilla;
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void DibujaRegla(Bitmap bitmap, PictureBox pb, float y)
        {
            decimal value;
            string str;
            string str1;
            string str2;
            try
            {
                float tamanio = (float)bitmap.Height * 0.02f;
                this.escala = (float)bitmap.Height / (float)pb.Height;
                float pDitancia = (float)bitmap.Height / 21.7f;
                float pInicial = y * this.escala - pDitancia / 2f;
                if (this.saldoY <= 0f)
                {
                    this.saldoY = 0f;
                }
                pInicial -= (float)this.saldoY;
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    using (System.Drawing.Font arialFont = new System.Drawing.Font("Arial", tamanio, FontStyle.Regular))
                    {
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial - 4f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial - 3f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial - 2f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial - pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial - 4f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial - 3f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial - 2f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial - pDitancia));
                        Graphics graphic = graphics;
                        if (this.numEstatura.Value > new decimal(99))
                        {
                            value = this.numEstatura.Value;
                            str = string.Concat(value.ToString(), "_____");
                        }
                        else
                        {
                            value = this.numEstatura.Value;
                            str = string.Concat(value.ToString(), "______");
                        }
                        graphic.DrawString(str, arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 2f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 3f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 4f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 2f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 3f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 4f * pDitancia));
                        Graphics graphic1 = graphics;
                        if ((this.numEstatura.Value - new decimal(10)) > new decimal(99))
                        {
                            value = this.numEstatura.Value - new decimal(10);
                            str1 = string.Concat(value.ToString(), "_____");
                        }
                        else
                        {
                            value = this.numEstatura.Value - new decimal(10);
                            str1 = string.Concat(value.ToString(), "______");
                        }
                        graphic1.DrawString(str1, arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 5f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 5f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 6f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 7f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 8f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 9f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 6f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 7f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 8f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 9f * pDitancia));
                        Graphics graphic2 = graphics;
                        if ((this.numEstatura.Value - new decimal(20)) > new decimal(99))
                        {
                            value = this.numEstatura.Value - new decimal(20);
                            str2 = string.Concat(value.ToString(), "_____");
                        }
                        else
                        {
                            value = this.numEstatura.Value - new decimal(20);
                            str2 = string.Concat(value.ToString(), "______");
                        }
                        graphic2.DrawString(str2, arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 10f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 10f * pDitancia));
                        graphics.DrawString(this.txtid.Text, arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), (float)(bitmap.Height / 21 * 20)));
                    }
                }
                if (pb.Name != "pbFrontalRegla")
                {
                    this.BitmapPerfilDerechoCropRegla = bitmap;

                }
                else
                {
                    this.BitmapRostroFrontalCropRegla = bitmap;
                }
                pb.Image = bitmap;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void DibujaReglaCuerpo(Bitmap bitmap, PictureBox pb, float y)
        {
            decimal value;
            string str;
            try
            {
                float tamanio = (float)bitmap.Height * 0.02f;
                this.escala = (float)bitmap.Height / (float)pb.Height;
                float pDitancia = (float)bitmap.Height / 21.7f;
                float pInicial = y * this.escala - pDitancia / 2f;
                if (this.saldoY <= 0f)
                {
                    this.saldoY = 0f;
                }
                pInicial -= (float)this.saldoY;
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    using (System.Drawing.Font arialFont = new System.Drawing.Font("Arial", tamanio, FontStyle.Regular))
                    {
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial - 4f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial - 3f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial - 2f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial - pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 2f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 3f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 4f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 5f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 6f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 7f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 8f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 9f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 10f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial - 4f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial - 3f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial - 2f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial - pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 2f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 3f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 4f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 5f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 6f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 7f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 8f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 9f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 10f * pDitancia));
                        Graphics graphic = graphics;
                        if (this.numEstatura.Value > new decimal(99))
                        {
                            value = this.numEstatura.Value;
                            str = string.Concat(value.ToString(), "_____");
                        }
                        else
                        {
                            value = this.numEstatura.Value;
                            str = string.Concat(value.ToString(), "______");
                        }
                        graphic.DrawString(str, arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial));
                        graphics.DrawString(this.txtid.Text, arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 4), (float)(bitmap.Height / 21 * 20)));
                    }
                }
                pb.Image = bitmap;

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void DibujaReglaDer(Bitmap bitmap, PictureBox pb, float y)
        {
            decimal value;
            string str;
            string str1;
            string str2;
            try
            {
                float tamanio = (float)bitmap.Height * 0.02f;
                this.escala = (float)bitmap.Height / (float)pb.Height;
                float pDitancia = (float)bitmap.Height / 21.7f;
                float pInicial = y * this.escala - pDitancia / 2f;
                if (this.saldoY <= 0f)
                {
                    this.saldoY = 0f;
                }
                pInicial -= (float)this.saldoY;
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    using (System.Drawing.Font arialFont = new System.Drawing.Font("Arial", tamanio, FontStyle.Regular))
                    {
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial - 4f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial - 3f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial - 2f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial - pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial - 4f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial - 3f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial - 2f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial - pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial));
                        Graphics graphic = graphics;
                        if (this.numEstatura.Value > new decimal(99))
                        {
                            value = this.numEstatura.Value;
                            str = string.Concat(value.ToString(), "_____");
                        }
                        else
                        {
                            value = this.numEstatura.Value;
                            str = string.Concat(value.ToString(), "______");
                        }
                        graphic.DrawString(str, arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 2f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 3f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 4f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 2f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 3f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 4f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 5f * pDitancia));
                        Graphics graphic1 = graphics;
                        if ((this.numEstatura.Value - new decimal(10)) > new decimal(99))
                        {
                            value = this.numEstatura.Value - new decimal(10);
                            str1 = string.Concat(value.ToString(), "_____");
                        }
                        else
                        {
                            value = this.numEstatura.Value - new decimal(10);
                            str1 = string.Concat(value.ToString(), "______");
                        }
                        graphic1.DrawString(str1, arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 5f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 6f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 7f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 8f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 9f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 6f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 7f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 8f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 9f * pDitancia));
                        graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 10f * pDitancia));
                        Graphics graphic2 = graphics;
                        if ((this.numEstatura.Value - new decimal(20)) > new decimal(99))
                        {
                            value = this.numEstatura.Value - new decimal(20);
                            str2 = string.Concat(value.ToString(), "_____");
                        }
                        else
                        {
                            value = this.numEstatura.Value - new decimal(20);
                            str2 = string.Concat(value.ToString(), "______");
                        }
                        graphic2.DrawString(str2, arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 10f * pDitancia));
                        graphics.DrawString(this.txtid.Text, arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), (float)(bitmap.Height / 21 * 20)));
                    }
                }
                pb.Image = bitmap;

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if ((!disposing ? false : this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }


        private void fCapturaFacial_Load(object sender, EventArgs e)
        {
            this.AuxNuevaImagen = 0;
            this.pbCapturaFrontal.Image = null;
            this.pbImagenRecorte.Image = null;
            this.pbFrontalRegla.Image = null;
            try
            {
                if (this.PersonaCapturada == null)
                {
                    this.PersonaCapturada = new CapturedPerson();
                }
                if (this.PersonaCapturada.BasicData == null)
                {
                    this.PersonaCapturada.BasicData = new LightPersonBasicData();
                }
                if (this.PersonaCapturada.RecordData == null)
                {
                    this.PersonaCapturada.RecordData = new RecordData();
                }
                this.PersonaCapturada = vEnroll.PersonaCapturada;
                string directoryName = Path.GetDirectoryName(Application.ExecutablePath);
                char directorySeparatorChar = Path.DirectorySeparatorChar;
                string NewFolderName = string.Concat(directoryName, directorySeparatorChar.ToString(), "Rostro");
                if (!Directory.Exists(NewFolderName))
                {
                    Directory.CreateDirectory(NewFolderName);
                }
                if ((this.PersonaCapturada == null ? false : this.PersonaCapturada.OfflinePerson != null))
                {
                    this.txtid.Text = this.PersonaCapturada.OfflinePerson.Identities[0].Identification;
                    this.numEstatura.Value = Convert.ToDecimal(this.PersonaCapturada.RecordData.BodySize);
                }
                this.btnsiguiente.Enabled = this.ValidaModeloDatosGenerales();
                this.CargaPestanaFotoFrontal();
                if ((this.PersonaCapturada.OfflinePerson.Identities[0].Identification == null ? true : Convert.ToDecimal(this.PersonaCapturada.RecordData.BodySize) <= new decimal(49)))
                {
                    this.btnsiguiente.Enabled = false;
                }
                else
                {
                    this.btnsiguiente.Enabled = true;
                }
            }
            catch
            {
            }
        }

        public static DataTable GetDistinctRecords(DataTable dt, string[] Columns)
        {
            DataTable table;
            try
            {
                DataTable dtUniqRecords = new DataTable();
                table = dt.DefaultView.ToTable(true, Columns);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                table = null;
            }
            return table;
        }

        private Rectangle GetRectangle()
        {
            this.rect = new Rectangle()
            {
                X = Math.Min(this.StartLocation.X, this.EndLcation.X),
                Y = Math.Min(this.StartLocation.Y, this.EndLcation.Y),
                Width = Math.Abs(this.StartLocation.X - this.EndLcation.X),
                Height = Math.Abs(this.StartLocation.Y - this.EndLcation.Y)
            };
            return this.rect;
        }

        public void HallaEscalaSaldo(PictureBox pbCaptura)
        {
            try
            {
                this.imgInput = new Image<Bgr, byte>((Bitmap)pbCaptura.Image);
                System.Drawing.Size size = pbCaptura.Size;
                this.escala = (float)size.Height / (float)this.imgInput.Height;
                float nuevamedidax = (float)this.imgInput.Width * this.escala;
                float nuevamediday = (float)this.imgInput.Height * this.escala;
                if (nuevamedidax > (float)pbCaptura.Size.Width)
                {
                    size = pbCaptura.Size;
                    this.escala = (float)size.Width / (float)this.imgInput.Width;
                    nuevamedidax = (float)this.imgInput.Height * this.escala;
                    size = pbCaptura.Size;
                    this.saldoY = ((float)size.Height - nuevamedidax) / 2f;
                    this.saldoX = 0f;
                }
                else
                {
                    size = pbCaptura.Size;
                    this.saldoX = ((float)size.Width - nuevamedidax) / 2f;
                    this.saldoY = 0f;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(vFace));
            this.tabPage0 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel12 = new System.Windows.Forms.TableLayoutPanel();
            this.txtid = new System.Windows.Forms.TextBox();
            this.numEstatura = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAnterior = new System.Windows.Forms.Button();
            this.btnAyuda = new System.Windows.Forms.Button();
            this.btnguardar = new System.Windows.Forms.Button();
            this.btnsiguiente = new System.Windows.Forms.Button();
            this.btncancelar = new System.Windows.Forms.Button();
            this.tabCapturaFacial = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCargarFrontal = new System.Windows.Forms.Button();
            this.btnFoto = new System.Windows.Forms.Button();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.pbCapturaFrontal = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label13 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Detalle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Valor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label7 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Detalle2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Valor2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pbImagenRecorte = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.pbFrontalRegla = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tabPage0.SuspendLayout();
            this.tableLayoutPanel12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEstatura)).BeginInit();
            this.tableLayoutPanel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabCapturaFacial.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCapturaFrontal)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImagenRecorte)).BeginInit();
            this.tableLayoutPanel7.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFrontalRegla)).BeginInit();
            this.tableLayoutPanel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.tableLayoutPanel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage0
            // 
            this.tabPage0.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(63)))), ((int)(((byte)(105)))));
            this.tabPage0.Controls.Add(this.panel2);
            this.tabPage0.Controls.Add(this.tableLayoutPanel12);
            this.tabPage0.Controls.Add(this.tableLayoutPanel5);
            this.tabPage0.Location = new System.Drawing.Point(4, 22);
            this.tabPage0.Name = "tabPage0";
            this.tabPage0.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage0.Size = new System.Drawing.Size(1181, 586);
            this.tabPage0.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Location = new System.Drawing.Point(13, 192);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1183, 25);
            this.panel2.TabIndex = 54;
            // 
            // tableLayoutPanel12
            // 
            this.tableLayoutPanel12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel12.ColumnCount = 4;
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.66721F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.93344F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.8447F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.55465F));
            this.tableLayoutPanel12.Controls.Add(this.txtid, 2, 0);
            this.tableLayoutPanel12.Controls.Add(this.numEstatura, 2, 1);
            this.tableLayoutPanel12.Controls.Add(this.label16, 1, 1);
            this.tableLayoutPanel12.Controls.Add(this.label12, 1, 0);
            this.tableLayoutPanel12.ForeColor = System.Drawing.Color.White;
            this.tableLayoutPanel12.Location = new System.Drawing.Point(231, 66);
            this.tableLayoutPanel12.Name = "tableLayoutPanel12";
            this.tableLayoutPanel12.RowCount = 2;
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel12.Size = new System.Drawing.Size(693, 85);
            this.tableLayoutPanel12.TabIndex = 53;
            // 
            // txtid
            // 
            this.txtid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtid.Location = new System.Drawing.Point(276, 11);
            this.txtid.Name = "txtid";
            this.txtid.Size = new System.Drawing.Size(235, 20);
            this.txtid.TabIndex = 44;
            // 
            // numEstatura
            // 
            this.numEstatura.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numEstatura.Location = new System.Drawing.Point(276, 53);
            this.numEstatura.Maximum = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.numEstatura.Minimum = new decimal(new int[] {
            49,
            0,
            0,
            0});
            this.numEstatura.Name = "numEstatura";
            this.numEstatura.Size = new System.Drawing.Size(235, 20);
            this.numEstatura.TabIndex = 49;
            this.numEstatura.Value = new decimal(new int[] {
            49,
            0,
            0,
            0});
            this.numEstatura.ValueChanged += new System.EventHandler(this.numEstatura_ValueChanged);
            // 
            // label16
            // 
            this.label16.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label16.Location = new System.Drawing.Point(176, 56);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(94, 14);
            this.label16.TabIndex = 48;
            this.label16.Text = "Estatura (cm):";
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label12.Location = new System.Drawing.Point(176, 14);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(94, 14);
            this.label12.TabIndex = 50;
            this.label12.Text = "Identificación:";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 6);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1179, 29);
            this.tableLayoutPanel5.TabIndex = 52;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1173, 18);
            this.label2.TabIndex = 26;
            this.label2.Text = "DATOS GENERALES";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(63)))), ((int)(((byte)(105)))));
            this.panel1.Controls.Add(this.btnAnterior);
            this.panel1.Controls.Add(this.btnAyuda);
            this.panel1.Controls.Add(this.btnguardar);
            this.panel1.Controls.Add(this.btnsiguiente);
            this.panel1.Controls.Add(this.btncancelar);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(339, 68);
            this.panel1.TabIndex = 15;
            // 
            // btnAnterior
            // 
            this.btnAnterior.BackColor = System.Drawing.Color.White;
            this.btnAnterior.Image = ((System.Drawing.Image)(resources.GetObject("btnAnterior.Image")));
            this.btnAnterior.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAnterior.Location = new System.Drawing.Point(13, 3);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new System.Drawing.Size(75, 60);
            this.btnAnterior.TabIndex = 20;
            this.btnAnterior.Text = "Anterior";
            this.btnAnterior.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAnterior.UseVisualStyleBackColor = false;
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click);
            // 
            // btnAyuda
            // 
            this.btnAyuda.BackColor = System.Drawing.Color.White;
            this.btnAyuda.Location = new System.Drawing.Point(853, 17);
            this.btnAyuda.Name = "btnAyuda";
            this.btnAyuda.Size = new System.Drawing.Size(75, 27);
            this.btnAyuda.TabIndex = 19;
            this.btnAyuda.Text = "Ayuda";
            this.btnAyuda.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAyuda.UseVisualStyleBackColor = false;
            this.btnAyuda.Visible = false;
            // 
            // btnguardar
            // 
            this.btnguardar.BackColor = System.Drawing.Color.White;
            this.btnguardar.Image = ((System.Drawing.Image)(resources.GetObject("btnguardar.Image")));
            this.btnguardar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnguardar.Location = new System.Drawing.Point(175, 3);
            this.btnguardar.Name = "btnguardar";
            this.btnguardar.Size = new System.Drawing.Size(75, 60);
            this.btnguardar.TabIndex = 18;
            this.btnguardar.Text = "Guardar";
            this.btnguardar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnguardar.UseVisualStyleBackColor = false;
            this.btnguardar.Click += new System.EventHandler(this.btnguardar_Click);
            // 
            // btnsiguiente
            // 
            this.btnsiguiente.BackColor = System.Drawing.Color.White;
            this.btnsiguiente.Image = ((System.Drawing.Image)(resources.GetObject("btnsiguiente.Image")));
            this.btnsiguiente.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnsiguiente.Location = new System.Drawing.Point(94, 3);
            this.btnsiguiente.Name = "btnsiguiente";
            this.btnsiguiente.Size = new System.Drawing.Size(75, 60);
            this.btnsiguiente.TabIndex = 17;
            this.btnsiguiente.Text = "Siguiente";
            this.btnsiguiente.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnsiguiente.UseVisualStyleBackColor = false;
            this.btnsiguiente.Click += new System.EventHandler(this.btnsiguiente_Click);
            // 
            // btncancelar
            // 
            this.btncancelar.BackColor = System.Drawing.Color.White;
            this.btncancelar.Image = ((System.Drawing.Image)(resources.GetObject("btncancelar.Image")));
            this.btncancelar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btncancelar.Location = new System.Drawing.Point(256, 3);
            this.btncancelar.Name = "btncancelar";
            this.btncancelar.Size = new System.Drawing.Size(75, 60);
            this.btncancelar.TabIndex = 16;
            this.btncancelar.Text = "Salir";
            this.btncancelar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btncancelar.UseVisualStyleBackColor = false;
            this.btncancelar.Click += new System.EventHandler(this.btncancelar_Click);
            // 
            // tabCapturaFacial
            // 
            this.tabCapturaFacial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabCapturaFacial.Controls.Add(this.tabPage0);
            this.tabCapturaFacial.Controls.Add(this.tabPage1);
            this.tabCapturaFacial.Controls.Add(this.tabPage2);
            this.tabCapturaFacial.Controls.Add(this.tabPage3);
            this.tabCapturaFacial.ItemSize = new System.Drawing.Size(89, 0);
            this.tabCapturaFacial.Location = new System.Drawing.Point(0, 0);
            this.tabCapturaFacial.Margin = new System.Windows.Forms.Padding(0);
            this.tabCapturaFacial.Multiline = true;
            this.tabCapturaFacial.Name = "tabCapturaFacial";
            this.tabCapturaFacial.SelectedIndex = 0;
            this.tabCapturaFacial.Size = new System.Drawing.Size(1189, 612);
            this.tabCapturaFacial.TabIndex = 14;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(63)))), ((int)(((byte)(105)))));
            this.tabPage1.Controls.Add(this.tableLayoutPanel6);
            this.tabPage1.Controls.Add(this.tableLayoutPanel4);
            this.tabPage1.Controls.Add(this.tableLayoutPanel3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1181, 586);
            this.tabPage1.TabIndex = 0;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel6.ColumnCount = 4;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel6.Controls.Add(this.btnCargarFrontal, 2, 0);
            this.tableLayoutPanel6.Controls.Add(this.btnFoto, 1, 0);
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 484);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(1171, 58);
            this.tableLayoutPanel6.TabIndex = 56;
            // 
            // btnCargarFrontal
            // 
            this.btnCargarFrontal.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnCargarFrontal.BackColor = System.Drawing.Color.White;
            this.btnCargarFrontal.Image = ((System.Drawing.Image)(resources.GetObject("btnCargarFrontal.Image")));
            this.btnCargarFrontal.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCargarFrontal.Location = new System.Drawing.Point(587, 5);
            this.btnCargarFrontal.Name = "btnCargarFrontal";
            this.btnCargarFrontal.Size = new System.Drawing.Size(89, 48);
            this.btnCargarFrontal.TabIndex = 10;
            this.btnCargarFrontal.Text = "Cargar Imagen";
            this.btnCargarFrontal.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCargarFrontal.UseVisualStyleBackColor = false;
            this.btnCargarFrontal.Click += new System.EventHandler(this.btnCargarFrontal_Click);
            // 
            // btnFoto
            // 
            this.btnFoto.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnFoto.BackColor = System.Drawing.Color.White;
            this.btnFoto.Image = ((System.Drawing.Image)(resources.GetObject("btnFoto.Image")));
            this.btnFoto.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnFoto.Location = new System.Drawing.Point(492, 5);
            this.btnFoto.Name = "btnFoto";
            this.btnFoto.Size = new System.Drawing.Size(89, 48);
            this.btnFoto.TabIndex = 53;
            this.btnFoto.Text = "Tomar Foto";
            this.btnFoto.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnFoto.UseVisualStyleBackColor = false;
            this.btnFoto.Click += new System.EventHandler(this.btnFoto_Click);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel4.Controls.Add(this.pbCapturaFrontal, 1, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(6, 41);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1168, 437);
            this.tableLayoutPanel4.TabIndex = 55;
            // 
            // pbCapturaFrontal
            // 
            this.pbCapturaFrontal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbCapturaFrontal.BackColor = System.Drawing.Color.Gainsboro;
            this.pbCapturaFrontal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbCapturaFrontal.Location = new System.Drawing.Point(181, 6);
            this.pbCapturaFrontal.Margin = new System.Windows.Forms.Padding(6);
            this.pbCapturaFrontal.Name = "pbCapturaFrontal";
            this.pbCapturaFrontal.Size = new System.Drawing.Size(805, 425);
            this.pbCapturaFrontal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbCapturaFrontal.TabIndex = 4;
            this.pbCapturaFrontal.TabStop = false;
            this.pbCapturaFrontal.Paint += new System.Windows.Forms.PaintEventHandler(this.pb_Paint);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.label13, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(6, 6);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1168, 29);
            this.tableLayoutPanel3.TabIndex = 54;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(3, 5);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1162, 18);
            this.label13.TabIndex = 26;
            this.label13.Text = "(FOTO FRONTAL)";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(63)))), ((int)(((byte)(105)))));
            this.tabPage2.Controls.Add(this.tableLayoutPanel8);
            this.tabPage2.Controls.Add(this.tableLayoutPanel7);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(1181, 586);
            this.tabPage2.TabIndex = 5;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel8.ColumnCount = 5;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel8.Controls.Add(this.label8, 2, 0);
            this.tableLayoutPanel8.Controls.Add(this.dataGridView1, 2, 1);
            this.tableLayoutPanel8.Controls.Add(this.label7, 3, 0);
            this.tableLayoutPanel8.Controls.Add(this.dataGridView2, 3, 1);
            this.tableLayoutPanel8.Controls.Add(this.panel4, 1, 1);
            this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 38);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 2;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.984791F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.01521F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(1174, 526);
            this.tableLayoutPanel8.TabIndex = 63;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(354, 14);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(346, 13);
            this.label8.TabIndex = 58;
            this.label8.Text = "RASGOS FACIALES";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ColumnHeadersVisible = false;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Detalle,
            this.Valor});
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView1.Location = new System.Drawing.Point(354, 44);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView1.Size = new System.Drawing.Size(346, 479);
            this.dataGridView1.TabIndex = 56;
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            // 
            // Detalle
            // 
            this.Detalle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Detalle.DataPropertyName = "Detalle";
            this.Detalle.HeaderText = "Detalle";
            this.Detalle.Name = "Detalle";
            this.Detalle.ReadOnly = true;
            this.Detalle.Width = 190;
            // 
            // Valor
            // 
            this.Valor.DataPropertyName = "Valor";
            this.Valor.HeaderText = "Valor";
            this.Valor.Name = "Valor";
            this.Valor.ReadOnly = true;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(706, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(346, 13);
            this.label7.TabIndex = 59;
            this.label7.Text = "ATRIBUTOS FACIALES";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.ColumnHeadersVisible = false;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Detalle2,
            this.Valor2});
            this.dataGridView2.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView2.Location = new System.Drawing.Point(706, 44);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView2.Size = new System.Drawing.Size(346, 479);
            this.dataGridView2.TabIndex = 57;
            this.dataGridView2.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView2_CellFormatting);
            // 
            // Detalle2
            // 
            this.Detalle2.DataPropertyName = "Detalle";
            this.Detalle2.HeaderText = "Detalle2";
            this.Detalle2.Name = "Detalle2";
            this.Detalle2.ReadOnly = true;
            this.Detalle2.Width = 125;
            // 
            // Valor2
            // 
            this.Valor2.DataPropertyName = "Valor";
            this.Valor2.HeaderText = "Valor2";
            this.Valor2.Name = "Valor2";
            this.Valor2.ReadOnly = true;
            this.Valor2.Width = 190;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.pbImagenRecorte);
            this.panel4.Location = new System.Drawing.Point(120, 44);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(207, 365);
            this.panel4.TabIndex = 60;
            // 
            // pbImagenRecorte
            // 
            this.pbImagenRecorte.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbImagenRecorte.BackColor = System.Drawing.Color.Gainsboro;
            this.pbImagenRecorte.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbImagenRecorte.Location = new System.Drawing.Point(-18, 3);
            this.pbImagenRecorte.Name = "pbImagenRecorte";
            this.pbImagenRecorte.Size = new System.Drawing.Size(222, 261);
            this.pbImagenRecorte.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbImagenRecorte.TabIndex = 55;
            this.pbImagenRecorte.TabStop = false;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel7.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(1174, 29);
            this.tableLayoutPanel7.TabIndex = 62;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(3, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1168, 18);
            this.label4.TabIndex = 26;
            this.label4.Text = "CALIDAD DE LA FOTO FRONTAL";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(63)))), ((int)(((byte)(105)))));
            this.tabPage3.Controls.Add(this.tableLayoutPanel10);
            this.tabPage3.Controls.Add(this.tableLayoutPanel9);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1181, 586);
            this.tabPage3.TabIndex = 8;
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel10.ColumnCount = 3;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel10.Controls.Add(this.pbFrontalRegla, 1, 0);
            this.tableLayoutPanel10.Location = new System.Drawing.Point(3, 38);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 1;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(1172, 516);
            this.tableLayoutPanel10.TabIndex = 62;
            // 
            // pbFrontalRegla
            // 
            this.pbFrontalRegla.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbFrontalRegla.BackColor = System.Drawing.Color.Gainsboro;
            this.pbFrontalRegla.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbFrontalRegla.Location = new System.Drawing.Point(240, 6);
            this.pbFrontalRegla.Margin = new System.Windows.Forms.Padding(6);
            this.pbFrontalRegla.Name = "pbFrontalRegla";
            this.pbFrontalRegla.Size = new System.Drawing.Size(691, 500);
            this.pbFrontalRegla.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbFrontalRegla.TabIndex = 60;
            this.pbFrontalRegla.TabStop = false;
            this.pbFrontalRegla.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbFrontalRegla_MouseDown);
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel9.ColumnCount = 1;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel9.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel9.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 1;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(1172, 29);
            this.tableLayoutPanel9.TabIndex = 61;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(3, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1166, 18);
            this.label5.TabIndex = 26;
            this.label5.Text = "COLOCAR ESTATURA";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel11.ColumnCount = 2;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel11.Controls.Add(this.pictureBox1, 1, 0);
            this.tableLayoutPanel11.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel11.Location = new System.Drawing.Point(32, 29);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 1;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(1189, 75);
            this.tableLayoutPanel11.TabIndex = 18;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1106, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(80, 69);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 55;
            this.pictureBox1.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(32, 101);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1189, 25);
            this.tableLayoutPanel1.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1183, 24);
            this.label1.TabIndex = 26;
            this.label1.Text = "INFORMACION FACIAL";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.tabCapturaFacial, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(32, 106);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1189, 612);
            this.tableLayoutPanel2.TabIndex = 20;
            // 
            // vFace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(63)))), ((int)(((byte)(105)))));
            this.ClientSize = new System.Drawing.Size(1248, 732);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.tableLayoutPanel11);
            this.Controls.Add(this.tableLayoutPanel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.HelpButton = true;
            this.MinimizeBox = false;
            this.Name = "vFace";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "fCapturaFacial";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.fCapturaFacial_Load);
            this.tabPage0.ResumeLayout(false);
            this.tableLayoutPanel12.ResumeLayout(false);
            this.tableLayoutPanel12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEstatura)).EndInit();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tabCapturaFacial.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCapturaFrontal)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbImagenRecorte)).EndInit();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tableLayoutPanel10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbFrontalRegla)).EndInit();
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.tableLayoutPanel11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        private void numEstatura_ValueChanged(object sender, EventArgs e)
        {
            this.ValidaModeloDatosGenerales();
        }

        private Bitmap ObtieneImagen(Bitmap bitmap2, FaceData pFaceData)
        {
            Bitmap bitmap1;
            try
            {
                Bitmap bitmap = null;
                if (bitmap2 != null)
                {
                    bitmap = bitmap2;
                }
                else if (pFaceData != null)
                {
                    this.imgInput = new Image<Bgr, byte>(pFaceData.NormalizedImage);
                    bitmap = this.imgInput.Bitmap;
                }
                bitmap1 = bitmap;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                bitmap1 = null;
            }
            return bitmap1;
        }

        private void pb_Paint(object sender, PaintEventArgs e)
        {
            if (this._selecting)
            {
                e.Graphics.DrawRectangle(Pens.Red, this.GetRectangle());
            }
        }

        private void pbFrontalRegla_MouseDown(object sender, MouseEventArgs e)
        {
            FaceData faceFrontal;
            try
            {
                PictureBox pb = (PictureBox)sender;
                Bitmap bitmapRostroFrontalCrop = this.BitmapRostroFrontalCrop;
                if (this.PersonaCapturada == null || this.PersonaCapturada.RecordData == null)
                {
                    faceFrontal = null;
                }
                else
                {
                    faceFrontal = this.PersonaCapturada.RecordData.FaceFrontal;
                }
                Bitmap bitmap = this.ObtieneImagen(bitmapRostroFrontalCrop, faceFrontal);
                this.BitmapRostroFrontalCropReglaAux = (Bitmap)bitmap.Clone();
                this.HallaEscalaSaldo(pb);
                this.DibujaRegla(this.BitmapRostroFrontalCropReglaAux, pb, (float)e.Location.Y);
                this.btnsiguiente.Enabled = true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private bool ValidaModeloDatosGenerales()
        {
            bool enabled;
            try
            {
                DatosGenerales vRegistroPersona = new DatosGenerales()
                {
                    numEstatura = this.numEstatura.Value
                };
                this.btnsiguiente.Enabled = HelperValidatorField.ValidarCampos(vRegistroPersona, this.errorProvider1, this);
                enabled = this.btnsiguiente.Enabled;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                enabled = false;
            }
            return enabled;
        }
    }
}