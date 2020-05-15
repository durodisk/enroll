using ENROLL.Core;
using ENROLL.Helpers;
using Datys.Enroll.Core;
using Datys.SIP.Common;
using Datys.SIP.Common.Biometric;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows.Forms;

namespace ENROLL
{
	public class vSynchronization : Form
	{
		private Thread hilo;

		private int ItemMargin = 5;

		private CoreFilesClient EnvioDeArchivos = new CoreFilesClient();

		private bool moviendoFormulario;

		private Point posicionActualPuntero;

		private IContainer components = null;

		private TableLayoutPanel tableLayoutPanel11;

		private Label label4;

		private TableLayoutPanel tableLayoutPanel1;

		private Panel panel1;

		private Button btncancelar;

		private Button btnIniciaSincro;

		private ListBox listBox1;

		private TableLayoutPanel tableLayoutPanel2;

		private ProgressBar progressBar1;

		private TableLayoutPanel tableLayoutPanel3;

		public vSynchronization()
		{
			this.InitializeComponent();
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

		private void btncancelar_Click(object sender, EventArgs e)
		{
			if (this.Alerta("Mensaje", "¿ Esta seguro de Salir ?", true))
			{
				base.Close();
			}
		}

		private void btnIniciaSincro_Click(object sender, EventArgs e)
		{
			this.listBox1.Items.Clear();
			this.btnIniciaSincro.Enabled = false;
			this.btnIniciaSincro.Text = "Sincronizando......";
			ListBox.ObjectCollection items = this.listBox1.Items;
			DateTime now = DateTime.Now;
			items.Add(string.Concat(now.ToString(), "   Preparando "));
			this.hilo = new Thread(new ThreadStart(this.procesar1));
			this.hilo.Start();
			this.progressBar1.Visible = true;
		}

		private void btnmaximizar_Click(object sender, EventArgs e)
		{
		}

		private void btnMinimizar_Click(object sender, EventArgs e)
		{
			base.WindowState = FormWindowState.Minimized;
		}

		private void btnnormal_Click(object sender, EventArgs e)
		{
		}

		private void button1_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (this.Alerta("Mensaje", "¿ Esta seguro de Salir ?", true))
			{
				base.Close();
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

		private string Envio(ref string MensajeProceso)
		{
			DateTime now;
			string str;
			try
			{
				string[] vColArchivoEpd = Directory.GetFiles("Enrolls\\", "*.epd");
				string vNameFile = "";
				string vMensajeWs = "";
				string vMensajeRegistro = "";
				string vMensajeResul = "";
				int contador = 0;
				string[] strArrays = vColArchivoEpd;
				for (int i = 0; i < (int)strArrays.Length; i++)
				{
					string vArchivoEpd = strArrays[i];
					HelperSerializer ser = new HelperSerializer();
					CapturedPerson PersonaCapturada = ser.DeserializeEpd(vArchivoEpd);
					if (PersonaCapturada.BasicData.UploadStatus.ToString() == "Pending")
					{
						string idPerson = PersonaCapturada.PersonId;
						File.ReadAllBytes(vArchivoEpd);
						vNameFile = vArchivoEpd.Substring(8, 18);
						string ruta = "C:\\enrolEpd\\";
						if (Directory.Exists(string.Concat(ruta, vNameFile)))
						{
							(new DirectoryInfo(string.Concat(ruta, vNameFile))).Delete(true);
						}
						Directory.CreateDirectory(string.Concat(ruta, vNameFile));
						this.ExtraeDatos(PersonaCapturada.BasicData, string.Concat(ruta, vNameFile, "\\BasicData"), "\\BasicData.txt");
						this.ExtraeDatos(PersonaCapturada.OfflinePerson.Identities[0], string.Concat(ruta, vNameFile, "\\OfflinePerson"), "\\OfflinePerson.txt");
						this.ExtraeDatos(PersonaCapturada.PalmForm, string.Concat(ruta, vNameFile, "\\PalmForm"), "\\PalmForm.txt");
						this.ExtraeDatos(PersonaCapturada.RecordData, string.Concat(ruta, vNameFile, "\\RecordData"), "\\RecordData.txt");
						vSynchronization.MyDelegado MD = new vSynchronization.MyDelegado(this.Mostrar);
						if (vMensajeWs != "1")
						{
							object[] objArray = new object[] { "1", null };
							string[] str1 = new string[5];
							now = DateTime.Now;
							str1[0] = now.ToString();
							str1[1] = "   ";
							str1[2] = vNameFile;
							str1[3] = " No enviado ERROR: ";
							str1[4] = vMensajeRegistro;
							objArray[1] = string.Concat(str1);
							base.Invoke(MD, objArray);
						}
						else
						{
							contador++;
							object[] objArray1 = new object[] { "1", null };
							now = DateTime.Now;
							objArray1[1] = string.Concat(now.ToString(), "   ", vNameFile, "...ok");
							base.Invoke(MD, objArray1);
							PersonaCapturada.BasicData.UploadStatus = UploadStatus.UploadingBiometricData;
							ser.SerializeEpd(PersonaCapturada, vArchivoEpd);
						}
					}
					MensajeProceso = string.Concat("Sincronizacion Concluida, se envio:", contador.ToString(), " Registros");
					vMensajeResul = "1";
				}
				str = vMensajeResul;
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception);
				str = "Error de Envio";
			}
			return str;
		}

		private void ExtraeDatos(object pDatos, string PathEpd, string FileEpd)
		{
			BindingList<string> value;
			BindingList<Address> addresses;
			DataImageCore?[] nullableArray;
			if (pDatos != null)
			{
				if (!Directory.Exists(PathEpd))
				{
					Directory.CreateDirectory(PathEpd);
				}
				string rutaCompleta = string.Concat(PathEpd, FileEpd);
				if (File.Exists(rutaCompleta))
				{
					File.Delete(rutaCompleta);
				}
				using (StreamWriter mylogs = File.AppendText(rutaCompleta))
				{
				}
				using (StreamWriter file = new StreamWriter(rutaCompleta, true))
				{
					PropertyInfo[] properties = pDatos.GetType().GetProperties();
					for (int i = 0; i < (int)properties.Length; i++)
					{
						PropertyInfo property = properties[i];
						if ((property.PropertyType == typeof(string) || property.PropertyType == typeof(bool) || property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(float) || property.PropertyType == typeof(int) || property.PropertyType == typeof(UploadStatus) || property.PropertyType == typeof(IdentityPersonType) || property.PropertyType == typeof(SelectTypesForm?) ? true : property.PropertyType == typeof(List<int>)))
						{
							string x = property.Name;
							string y = (property.GetValue(pDatos) != null ? property.GetValue(pDatos).ToString() : "");
							string z = property.PropertyType.ToString();
							file.WriteLine(string.Format("{0}|{1}|{2}", x, y, z));
						}
						else if (property.PropertyType == typeof(Bitmap))
						{
							string x = property.Name;
							if (property.GetValue(pDatos) != null)
							{
								Bitmap y = (Bitmap)property.GetValue(pDatos);
								y.Save(string.Concat(PathEpd, "\\", x, ".bmp"));
								y.Save(string.Concat(PathEpd, "\\", x, ".jpg"));
								y.Save(string.Concat(PathEpd, "\\", x, ".png"));
							}
						}
						else if (property.PropertyType == typeof(byte[]))
						{
							string x = property.Name;
							if (property.GetValue(pDatos) != null)
							{
								byte[] y = (byte[])property.GetValue(pDatos);
								File.WriteAllBytes(string.Concat(PathEpd, "\\", x, ".bmp"), y);
								File.WriteAllBytes(string.Concat(PathEpd, "\\", x, ".jpg"), y);
								File.WriteAllBytes(string.Concat(PathEpd, "\\", x, ".png"), y);
							}
						}
						else if (property.PropertyType == typeof(Coder))
						{
							string x = property.Name;
							Coder y = (property.GetValue(pDatos) != null ? (Coder)property.GetValue(pDatos) : new Coder());
							string z = property.PropertyType.ToString();
							file.WriteLine(string.Format("{0}|{1}|{2}", x, JsonConvert.SerializeObject(y), z));
						}
						else if (property.PropertyType == typeof(BindingList<Coder>))
						{
							string x = property.Name;
							BindingList<Coder> y = (property.GetValue(pDatos) != null ? (BindingList<Coder>)property.GetValue(pDatos) : new BindingList<Coder>());
							string z = property.PropertyType.ToString();
							file.WriteLine(string.Format("{0}|{1}|{2}", x, JsonConvert.SerializeObject(y), z));
						}
						else if (property.PropertyType == typeof(GuidTuple))
						{
							string x = property.Name;
							GuidTuple y = (property.GetValue(pDatos) != null ? (GuidTuple)property.GetValue(pDatos) : new GuidTuple());
							string z = property.PropertyType.ToString();
							file.WriteLine(string.Format("{0}|{1}|{2}", x, JsonConvert.SerializeObject(y), z));
						}
						else if (property.PropertyType == typeof(List<BIR>))
						{
							string x = property.Name;
							List<BIR> y = (property.GetValue(pDatos) != null ? (List<BIR>)property.GetValue(pDatos) : new List<BIR>());
							if ((y == null ? false : y.Any<BIR>()))
							{
								if (!Directory.Exists(string.Concat(PathEpd, "\\", x)))
								{
									Directory.CreateDirectory(string.Concat(PathEpd, "\\", x));
								}
								foreach (BIR vBIR in y)
								{
									if (vBIR.BDB != null)
									{
										File.WriteAllBytes(string.Concat(new string[] { PathEpd, "\\", x, "\\", vBIR.BDBInfo.Type, "_", vBIR.BDBInfo.Subtype, ".wsq" }), vBIR.BDB);
										HelperSerializer ser = new HelperSerializer();
									}
								}
							}
						}
						else if (property.PropertyType == typeof(BindingList<BaseBiometric>))
						{
							string x = property.Name;
							BindingList<BaseBiometric> y = (property.GetValue(pDatos) != null ? (BindingList<BaseBiometric>)property.GetValue(pDatos) : new BindingList<BaseBiometric>());
							if ((y == null ? false : y.Any<BaseBiometric>()))
							{
								if (!Directory.Exists(string.Concat(PathEpd, "\\", x)))
								{
									Directory.CreateDirectory(string.Concat(PathEpd, "\\", x));
								}
								foreach (BaseBiometric vBaseBiometric in y)
								{
									string[] pathEpd = new string[] { PathEpd, "\\", x, "\\", null, null };
									pathEpd[4] = vBaseBiometric.IndexFinger.ToString();
									pathEpd[5] = ".wsq";
									File.WriteAllBytes(string.Concat(pathEpd), vBaseBiometric.Image);
								}
							}
						}
						else if (property.PropertyType == typeof(DataImageCore?[]))
						{
							string x = property.Name;
							if (property.GetValue(pDatos) != null)
							{
								nullableArray = (DataImageCore?[])property.GetValue(pDatos);
							}
							else
							{
								nullableArray = null;
							}
							DataImageCore?[] y = nullableArray;
							if ((y == null ? false : y.Any<DataImageCore?>()))
							{
								if (!Directory.Exists(string.Concat(PathEpd, "\\", x)))
								{
									Directory.CreateDirectory(string.Concat(PathEpd, "\\", x));
								}
								int c = 1;
								DataImageCore?[] nullableArray1 = y;
								for (int j = 0; j < (int)nullableArray1.Length; j++)
								{
									DataImageCore vDataImageCore = nullableArray1[j].Value;
									File.WriteAllBytes(string.Concat(new string[] { PathEpd, "\\", x, "\\", c.ToString(), ".bmp" }), vDataImageCore.Bir.BDB);
									File.WriteAllBytes(string.Concat(new string[] { PathEpd, "\\", x, "\\", c.ToString(), ".jpg" }), vDataImageCore.Bir.BDB);
									File.WriteAllBytes(string.Concat(new string[] { PathEpd, "\\", x, "\\", c.ToString(), ".png" }), vDataImageCore.Bir.BDB);
									c++;
								}
							}
						}
						else if (property.PropertyType == typeof(BindingList<Address>))
						{
							string x = property.Name;
							if (property.GetValue(pDatos) != null)
							{
								addresses = (BindingList<Address>)property.GetValue(pDatos);
							}
							else
							{
								addresses = null;
							}
							BindingList<Address> y = addresses;
							string z = property.PropertyType.ToString();
							file.WriteLine(string.Format("{0}|{1}|{2}", x, JsonConvert.SerializeObject(y), z));
						}
						else if (property.PropertyType == typeof(BindingList<string>))
						{
							string x = property.Name;
							if (property.GetValue(pDatos) != null)
							{
								value = (BindingList<string>)property.GetValue(pDatos);
							}
							else
							{
								value = null;
							}
							BindingList<string> y = value;
							string z = property.PropertyType.ToString();
							file.WriteLine(string.Format("{0}|{1}|{2}", x, JsonConvert.SerializeObject(y), z));
						}
						else if (property.PropertyType == typeof(BindingList<BiometricData>))
						{
							string x = property.Name;
							BindingList<BiometricData> y = (property.GetValue(pDatos) != null ? (BindingList<BiometricData>)property.GetValue(pDatos) : new BindingList<BiometricData>());
							if ((y == null ? false : y.Any<BiometricData>()))
							{
								if (!Directory.Exists(string.Concat(PathEpd, "\\", x)))
								{
									Directory.CreateDirectory(string.Concat(PathEpd, "\\", x));
								}
								foreach (BiometricData vBiometricData in y)
								{
									File.WriteAllBytes(string.Concat(new string[] { PathEpd, "\\", x, "\\", vBiometricData.Sample.BDBInfo.Type, "_", vBiometricData.Sample.BDBInfo.Subtype, ".bmp" }), vBiometricData.Sample.BDB);
									File.WriteAllBytes(string.Concat(new string[] { PathEpd, "\\", x, "\\", vBiometricData.Sample.BDBInfo.Type, "_", vBiometricData.Sample.BDBInfo.Subtype, ".jpg" }), vBiometricData.Sample.BDB);
								}
							}
						}
						else if (property.PropertyType == typeof(FaceData))
						{
							string x = property.Name;
							FaceData y = (property.GetValue(pDatos) != null ? (FaceData)property.GetValue(pDatos) : new FaceData());
							if (!Directory.Exists(string.Concat(PathEpd, "\\", x)))
							{
								Directory.CreateDirectory(string.Concat(PathEpd, "\\", x));
							}
							if (y.ModifyImageArr != null)
							{
								File.WriteAllBytes(string.Concat(PathEpd, "\\", x, "\\ModifyImageArr.bmp"), y.ModifyImageArr);
								File.WriteAllBytes(string.Concat(PathEpd, "\\", x, "\\ModifyImageArr.jpg"), y.ModifyImageArr);
								File.WriteAllBytes(string.Concat(PathEpd, "\\", x, "\\ModifyImageArr.png"), y.ModifyImageArr);
							}
							if (y.NormalizedImageArr != null)
							{
								File.WriteAllBytes(string.Concat(PathEpd, "\\", x, "\\NormalizedImageArr.bmp"), y.NormalizedImageArr);
								File.WriteAllBytes(string.Concat(PathEpd, "\\", x, "\\NormalizedImageArr.jpg"), y.NormalizedImageArr);
								File.WriteAllBytes(string.Concat(PathEpd, "\\", x, "\\NormalizedImageArr.png"), y.NormalizedImageArr);
							}
							if (y.OriginalImageArr != null)
							{
								File.WriteAllBytes(string.Concat(PathEpd, "\\", x, "\\OriginalImageArr.bmp"), y.OriginalImageArr);
								File.WriteAllBytes(string.Concat(PathEpd, "\\", x, "\\OriginalImageArr.jpg"), y.OriginalImageArr);
								File.WriteAllBytes(string.Concat(PathEpd, "\\", x, "\\OriginalImageArr.png"), y.OriginalImageArr);
							}
							if (y.RuledImageArr != null)
							{
								File.WriteAllBytes(string.Concat(PathEpd, "\\", x, "\\RuledImageArr.bmp"), y.RuledImageArr);
								File.WriteAllBytes(string.Concat(PathEpd, "\\", x, "\\RuledImageArr.jpg"), y.RuledImageArr);
								File.WriteAllBytes(string.Concat(PathEpd, "\\", x, "\\RuledImageArr.png"), y.RuledImageArr);
							}
						}
						else if (property.PropertyType == typeof(CharacteristicData))
						{
							string x = property.Name;
							CharacteristicData y = (property.GetValue(pDatos) != null ? (CharacteristicData)property.GetValue(pDatos) : new CharacteristicData());
							string z = property.PropertyType.ToString();
							file.WriteLine(string.Format("{0}|{1}|{2}", x, JsonConvert.SerializeObject(y), z));
						}
						else if (property.PropertyType == typeof(DescriptiveData))
						{
							string x = property.Name;
							DescriptiveData y = (property.GetValue(pDatos) != null ? (DescriptiveData)property.GetValue(pDatos) : new DescriptiveData());
							string z = property.PropertyType.ToString();
							file.WriteLine(string.Format("{0}|{1}|{2}", x, JsonConvert.SerializeObject(y), z));
						}
						else if (property.PropertyType == typeof(FeatureData))
						{
							string x = property.Name;
							FeatureData y = (property.GetValue(pDatos) != null ? (FeatureData)property.GetValue(pDatos) : new FeatureData());
							string z = property.PropertyType.ToString();
							file.WriteLine(string.Format("{0}|{1}|{2}", x, JsonConvert.SerializeObject(y), z));
						}
						else if (property.PropertyType == typeof(BindingList<MarkData>))
						{
							string x = property.Name;
							BindingList<MarkData> y = (property.GetValue(pDatos) != null ? (BindingList<MarkData>)property.GetValue(pDatos) : new BindingList<MarkData>());
							if ((y == null ? false : y.Any<MarkData>()))
							{
								if (!Directory.Exists(string.Concat(PathEpd, "\\", x)))
								{
									Directory.CreateDirectory(string.Concat(PathEpd, "\\", x));
								}
								int c = 1;
								foreach (MarkData vMarkData in y)
								{
									int cc = 1;
									foreach (ImageData vImageData in vMarkData.ListImages)
									{
										File.WriteAllBytes(string.Concat(new string[] { PathEpd, "\\", x, "\\", cc.ToString(), "_", c.ToString(), ".bmp" }), vImageData.NormalizedImageArr);
										File.WriteAllBytes(string.Concat(new string[] { PathEpd, "\\", x, "\\", cc.ToString(), "_", c.ToString(), ".jpg" }), vImageData.NormalizedImageArr);
										File.WriteAllBytes(string.Concat(new string[] { PathEpd, "\\", x, "\\", cc.ToString(), "_", c.ToString(), ".png" }), vImageData.NormalizedImageArr);
										cc++;
									}
									c++;
								}
							}
						}
						else if (property.PropertyType != typeof(BindingList<TattooData>))
						{
							string x = property.Name;
							property.PropertyType.ToString();
						}
						else
						{
							string x = property.Name;
							BindingList<TattooData> y = (property.GetValue(pDatos) != null ? (BindingList<TattooData>)property.GetValue(pDatos) : new BindingList<TattooData>());
							if ((y == null ? false : y.Any<TattooData>()))
							{
								if (!Directory.Exists(string.Concat(PathEpd, "\\", x)))
								{
									Directory.CreateDirectory(string.Concat(PathEpd, "\\", x));
								}
								int c = 1;
								foreach (TattooData vTattooData in y)
								{
									int cc = 1;
									foreach (ImageData vImageData in vTattooData.ListImages)
									{
										File.WriteAllBytes(string.Concat(new string[] { PathEpd, "\\", x, "\\", cc.ToString(), "_", c.ToString(), ".bmp" }), vImageData.NormalizedImageArr);
										File.WriteAllBytes(string.Concat(new string[] { PathEpd, "\\", x, "\\", cc.ToString(), "_", c.ToString(), ".jpg" }), vImageData.NormalizedImageArr);
										cc++;
									}
									c++;
								}
							}
						}
					}
					file.Close();
				}
			}
		}

		private void fPaquetes_Load(object sender, EventArgs e)
		{
			base.WindowState = FormWindowState.Normal;
			this.listBox1.DrawMode = DrawMode.OwnerDrawVariable;
			this.progressBar1.Visible = false;
		}

		private void InitializeComponent()
		{
			ComponentResourceManager resources = new ComponentResourceManager(typeof(vSynchronization));
			this.tableLayoutPanel11 = new TableLayoutPanel();
			this.label4 = new Label();
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.panel1 = new Panel();
			this.btnIniciaSincro = new Button();
			this.btncancelar = new Button();
			this.listBox1 = new ListBox();
			this.tableLayoutPanel2 = new TableLayoutPanel();
			this.progressBar1 = new ProgressBar();
			this.tableLayoutPanel3 = new TableLayoutPanel();
			this.tableLayoutPanel11.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			base.SuspendLayout();
			this.tableLayoutPanel11.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.tableLayoutPanel11.BackColor = Color.FromArgb(48, 63, 105);
			this.tableLayoutPanel11.ColumnCount = 1;
			this.tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20f));
			this.tableLayoutPanel11.Controls.Add(this.label4, 0, 0);
			this.tableLayoutPanel11.Location = new Point(3, 28);
			this.tableLayoutPanel11.Name = "tableLayoutPanel11";
			this.tableLayoutPanel11.RowCount = 1;
			this.tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Absolute, 29f));
			this.tableLayoutPanel11.Size = new System.Drawing.Size(624, 29);
			this.tableLayoutPanel11.TabIndex = 26;
			this.label4.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.label4.AutoSize = true;
			this.label4.BackColor = Color.FromArgb(48, 63, 105);
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
			this.label4.ForeColor = Color.White;
			this.label4.Location = new Point(3, 2);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(618, 24);
			this.label4.TabIndex = 26;
			this.label4.Text = "SINCRONIZACION DE REGISTROS";
			this.label4.TextAlign = ContentAlignment.MiddleCenter;
			this.tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.tableLayoutPanel1.BackColor = Color.FromArgb(48, 63, 105);
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20f));
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
			this.tableLayoutPanel1.Location = new Point(3, 63);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 29f));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(624, 72);
			this.tableLayoutPanel1.TabIndex = 27;
			this.panel1.Controls.Add(this.btncancelar);
			this.panel1.Controls.Add(this.btnIniciaSincro);
			this.panel1.Location = new Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(451, 66);
			this.panel1.TabIndex = 0;
			this.btnIniciaSincro.BackColor = Color.FromArgb(16, 103, 242);
			this.btnIniciaSincro.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.btnIniciaSincro.ForeColor = Color.White;
			this.btnIniciaSincro.Location = new Point(3, 3);
			this.btnIniciaSincro.Name = "btnIniciaSincro";
			this.btnIniciaSincro.Size = new System.Drawing.Size(174, 55);
			this.btnIniciaSincro.TabIndex = 7;
			this.btnIniciaSincro.Text = "Iniciar Sincronizacion";
			this.btnIniciaSincro.UseVisualStyleBackColor = false;
			this.btnIniciaSincro.Click += new EventHandler(this.btnIniciaSincro_Click);
			this.btncancelar.BackColor = Color.White;
			this.btncancelar.Image = (Image)resources.GetObject("btncancelar.Image");
			this.btncancelar.ImageAlign = ContentAlignment.TopCenter;
			this.btncancelar.Location = new Point(219, 4);
			this.btncancelar.Name = "btncancelar";
			this.btncancelar.Size = new System.Drawing.Size(75, 54);
			this.btncancelar.TabIndex = 27;
			this.btncancelar.Text = "Salir";
			this.btncancelar.TextAlign = ContentAlignment.BottomCenter;
			this.btncancelar.UseVisualStyleBackColor = false;
			this.btncancelar.Click += new EventHandler(this.btncancelar_Click);
			this.listBox1.Dock = DockStyle.Fill;
			this.listBox1.DrawMode = DrawMode.OwnerDrawVariable;
			this.listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.listBox1.FormattingEnabled = true;
			this.listBox1.HorizontalScrollbar = true;
			this.listBox1.ItemHeight = 15;
			this.listBox1.Location = new Point(3, 3);
			this.listBox1.Name = "listBox1";
			this.listBox1.ScrollAlwaysVisible = true;
			this.listBox1.SelectionMode = SelectionMode.MultiExtended;
			this.listBox1.Size = new System.Drawing.Size(618, 301);
			this.listBox1.TabIndex = 3;
			this.listBox1.DrawItem += new DrawItemEventHandler(this.listBox1_DrawItem);
			this.listBox1.MeasureItem += new MeasureItemEventHandler(this.listBox1_MeasureItem);
			this.tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.tableLayoutPanel2.BackColor = Color.FromArgb(48, 63, 105);
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20f));
			this.tableLayoutPanel2.Controls.Add(this.progressBar1, 0, 0);
			this.tableLayoutPanel2.Location = new Point(6, 138);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 29f));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(624, 40);
			this.tableLayoutPanel2.TabIndex = 28;
			this.progressBar1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.progressBar1.ForeColor = Color.Aqua;
			this.progressBar1.Location = new Point(3, 3);
			this.progressBar1.MarqueeAnimationSpeed = 30;
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(618, 29);
			this.progressBar1.Style = ProgressBarStyle.Marquee;
			this.progressBar1.TabIndex = 4;
			this.tableLayoutPanel3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.tableLayoutPanel3.BackColor = Color.FromArgb(48, 63, 105);
			this.tableLayoutPanel3.ColumnCount = 1;
			this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20f));
			this.tableLayoutPanel3.Controls.Add(this.listBox1, 0, 0);
			this.tableLayoutPanel3.Location = new Point(6, 184);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 1;
			this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 29f));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(624, 307);
			this.tableLayoutPanel3.TabIndex = 29;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = Color.FromArgb(48, 63, 105);
			base.ClientSize = new System.Drawing.Size(639, 503);
			base.Controls.Add(this.tableLayoutPanel3);
			base.Controls.Add(this.tableLayoutPanel2);
			base.Controls.Add(this.tableLayoutPanel1);
			base.Controls.Add(this.tableLayoutPanel11);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Name = "fPaquetes";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Sincronizacion de Registros";
			base.Load += new EventHandler(this.fPaquetes_Load);
			this.tableLayoutPanel11.ResumeLayout(false);
			this.tableLayoutPanel11.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
		{
			Rectangle bounds;
			try
			{
				string txt = (string)(sender as ListBox).Items[e.Index];
				e.DrawBackground();
				if ((e.State & DrawItemState.Selected) != DrawItemState.Selected)
				{
					using (SolidBrush br = new SolidBrush(e.ForeColor))
					{
						Graphics graphics = e.Graphics;
						System.Drawing.Font font = this.Font;
						float left = (float)e.Bounds.Left;
						bounds = e.Bounds;
						graphics.DrawString(txt, font, br, left, (float)(bounds.Top + this.ItemMargin));
					}
				}
				else
				{
					Graphics graphic = e.Graphics;
					System.Drawing.Font font1 = this.Font;
					Brush highlightText = SystemBrushes.HighlightText;
					float single = (float)e.Bounds.Left;
					bounds = e.Bounds;
					graphic.DrawString(txt, font1, highlightText, single, (float)(bounds.Top + this.ItemMargin));
				}
				e.DrawFocusRectangle();
			}
			catch
			{
			}
		}

		private void listBox1_MeasureItem(object sender, MeasureItemEventArgs e)
		{
			string txt = (string)(sender as ListBox).Items[e.Index];
			SizeF txt_size = e.Graphics.MeasureString(txt, this.Font);
			e.ItemHeight = (int)txt_size.Height + 2 * this.ItemMargin;
			e.ItemWidth = (int)txt_size.Width;
		}

		public void Mostrar(string respuesta, string mensaje_proceso)
		{
			this.listBox1.Items.Add(mensaje_proceso);
			if (respuesta == "2")
			{
				this.btnIniciaSincro.Enabled = true;
				this.progressBar1.Visible = false;
				this.btnIniciaSincro.Text = "Iniciar Sincronizacion";
			}
		}

		private void panel2_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button != System.Windows.Forms.MouseButtons.Left)
			{
				this.moviendoFormulario = false;
			}
			else
			{
				this.moviendoFormulario = true;
				this.posicionActualPuntero = new Point(e.X, e.Y);
			}
		}

		private void panel2_MouseMove(object sender, MouseEventArgs e)
		{
			if (this.moviendoFormulario)
			{
				Point nuevoPunto = base.PointToScreen(new Point(e.X, e.Y));
				nuevoPunto.Offset(-this.posicionActualPuntero.X, -this.posicionActualPuntero.Y);
				base.Location = nuevoPunto;
			}
		}

		private void panel2_MouseUp(object sender, MouseEventArgs e)
		{
			this.moviendoFormulario = false;
		}

		private void panel2_Paint(object sender, PaintEventArgs e)
		{
		}

		public void procesar1()
		{
			string respuesta = "";
			string mensaje_proceso = "";
			respuesta = "1";
			vSynchronization.MyDelegado MD = new vSynchronization.MyDelegado(this.Mostrar);
			object[] objArray = new object[] { respuesta, null };
			DateTime now = DateTime.Now;
			objArray[1] = string.Concat(now.ToString(), "   ", mensaje_proceso);
			base.Invoke(MD, objArray);
			if (respuesta != "1")
			{
				MD = new vSynchronization.MyDelegado(this.Mostrar);
				object[] objArray1 = new object[] { "2", null };
				now = DateTime.Now;
				objArray1[1] = string.Concat(now.ToString(), "   Proceso Terminado");
				base.Invoke(MD, objArray1);
			}
			else
			{
				respuesta = this.Envio(ref mensaje_proceso);
				MD = new vSynchronization.MyDelegado(this.Mostrar);
				object[] objArray2 = new object[] { "2", null };
				now = DateTime.Now;
				objArray2[1] = string.Concat(now.ToString(), "   ", mensaje_proceso);
				base.Invoke(MD, objArray2);
			}
		}

		private delegate void MyDelegado(string codigo, string text);
	}
}