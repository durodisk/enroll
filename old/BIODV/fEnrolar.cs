using BIODV.Util;
using Datys.Enroll.Core;
using Datys.SIP.Common;
using Datys.SIP.Common.Biometric;
using GBMSDemo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Forms.Classes.Collections;

namespace BIODV
{
	public class fEnrolar : Form
	{
		private Serializer ser = new Serializer();

		private IContainer components = null;

		private Ribbon ribbon1;

		private RibbonTab ribbonTab1;

		private RibbonPanel ribbonPanel1;

		private RibbonButton ribtnGuardarCerrar;

		private RibbonButton ribtnGuardar;

		private DataGridView dataGridView1;

		private RibbonPanel ribbonPanel2;

		private Label lblNacionalidad;

		private Label lblNombrePadre;

		private Label lblNombreMadre;

		private Label lblNombreCompleto;

		private Button btnFormulario;

		private Label lblnac;

		private Label lblnompadre;

		private Label lblnommadre;

		private Label lblnomcompleto;

		private PictureBox pbFrontal;

		private Button btnMarcasTatu;

		private Button btnRostro;

		private Button btnHuellasDactilares;

		private Button btnDatosBiograficos;

		private Button btnConcluirEnrol;

		private RibbonButton ribbonButton1;

		private RibbonButtonList ribbonButtonList1;

		private RibbonPanel ribbonPanel3;

		private RibbonButton ribbonButton2;

		private RibbonPanel ribbonPanel4;

		private TableLayoutPanel tableLayoutPanel11;

		private Label label4;

		private TableLayoutPanel tableLayoutPanel1;

		private TableLayoutPanel tableLayoutPanel2;

		private TableLayoutPanel tableLayoutPanel3;
		private RibbonTab ribbonTab2;
		private PictureBox pictureBox1;

		public PalmForm FormularioPalma
		{
			get;
			set;
		}

		public static CapturedPerson PersonaCapturada
		{
			get;
			set;
		}

		public fEnrolar()
		{
			bool flag;
			bool flag1;
			bool flag2;
			byte[] normalizedImageArr;
			this.InitializeComponent();
			try
			{
				if (string.IsNullOrWhiteSpace(fPrincipal.RutaEpd))
				{
					string vDirectorioEpd = "Enrolls\\";
					fPrincipal.RutaEpd = string.Concat(vDirectorioEpd, this.ser.RandomString(18), ".epd");
					fEnrolar.PersonaCapturada = new CapturedPerson();
					fEnrolar.PersonaCapturada.PersonId = Guid.NewGuid().ToString();
					fEnrolar.PersonaCapturada.BasicData.UploadStatus = UploadStatus.InProgress;
					fEnrolar.PersonaCapturada.OfflinePerson = new OfflinePerson();
					Button button = this.btnHuellasDactilares;
					Button button1 = this.btnRostro;
					Button button2 = this.btnMarcasTatu;
					bool num = false;
					//	flag2 = (bool)num;
					flag2 = num;
					this.btnFormulario.Enabled = (bool)num;
					bool flag3 = flag2;
					flag1 = flag3;
					button2.Enabled = flag3;
					bool flag4 = flag1;
					flag = flag4;
					button1.Enabled = flag4;
					button.Enabled = flag;
				}
				else
				{
					fEnrolar.PersonaCapturada = this.ser.DeserializeEpd(fPrincipal.RutaEpd);
					Button button3 = this.btnHuellasDactilares;
					Button button4 = this.btnRostro;
					Button button5 = this.btnMarcasTatu;
					bool flag5 = !string.IsNullOrEmpty(fEnrolar.PersonaCapturada.OfflinePerson.Identities[0].FirstName);
					flag2 = flag5;
					this.btnFormulario.Enabled = flag5;
					bool flag6 = flag2;
					flag1 = flag6;
					button5.Enabled = flag6;
					bool flag7 = flag1;
					flag = flag7;
					button4.Enabled = flag7;
					button3.Enabled = flag;
					this.lblNombreCompleto.Text = fEnrolar.PersonaCapturada.OfflinePerson.Identities[0].FullName;
					this.lblNombreMadre.Text = fEnrolar.PersonaCapturada.OfflinePerson.Identities[0].MotherName;
					this.lblNombrePadre.Text = fEnrolar.PersonaCapturada.OfflinePerson.Identities[0].FatherName;
					this.lblNacionalidad.Text = (!string.IsNullOrWhiteSpace(fEnrolar.PersonaCapturada.OfflinePerson.Identities[0].Country.Id) ? fEnrolar.PersonaCapturada.OfflinePerson.Identities[0].Country.Value : "");
					PictureBox image = this.pbFrontal;
					Serializer serializer = this.ser;
					if (fEnrolar.PersonaCapturada == null || fEnrolar.PersonaCapturada.RecordData == null || fEnrolar.PersonaCapturada.RecordData.FaceFrontal == null)
					{
						normalizedImageArr = null;
					}
					else
					{
						normalizedImageArr = fEnrolar.PersonaCapturada.RecordData.FaceFrontal.NormalizedImageArr;
					}
					image.Image = serializer.byteArrayToImage(normalizedImageArr);
				}
			}
			catch
			{
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

		private void btnConcluirEnrol_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.Alerta("Alerta", "¿Esta seguro de concluir el enrolamiento?, recuerde que no es reversible este proceso", true))
				{
					System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
					string vMensaje = "";
					if ((!this.ValidaFace(ref vMensaje) ? false : this.ValidaBiografia(ref vMensaje)))
					{
						this.FormateaArchivos();
						if (fEnrolar.PersonaCapturada.BasicData.UploadStatus.ToString() == "ManualVerification")
						{
							fEnrolar.PersonaCapturada.BasicData.UploadStatus = UploadStatus.Done;
						}
						if (fEnrolar.PersonaCapturada.BasicData.UploadStatus.ToString() == "InProgress")
						{
							fEnrolar.PersonaCapturada.BasicData.UploadStatus = UploadStatus.Pending;
						}
						(new Serializer()).SerializeEpd(fEnrolar.PersonaCapturada, fPrincipal.RutaEpd);
						(new fPrincipal()
						{
							MdiParent = base.ParentForm
						}).Show();
						base.Close();
					}
					else if (this.Alerta("Mensaje", vMensaje, false))
					{
						base.Close();
					}
					System.Windows.Forms.Cursor.Current = Cursors.Default;
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void btnDatosBiograficos_Click(object sender, EventArgs e)
		{
			(new fDatosBiograficos()
			{
				MdiParent = base.ParentForm
			}).Show();
			base.Close();
		}

		private void btnFormulario_Click(object sender, EventArgs e)
		{
			try
			{
				(new fCropB()
				{
					MdiParent = base.ParentForm
				}).Show();
				base.Close();
			}
			catch
			{
			}
		}

		private void btnHuellasDactilares_Click(object sender, EventArgs e)
		{
			System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
			DemoForm form = new DemoForm();
			if (fEnrolar.PersonaCapturada == null)
			{
				fEnrolar.PersonaCapturada = new CapturedPerson();
			}
			if (fEnrolar.PersonaCapturada.PalmForm == null)
			{
				fEnrolar.PersonaCapturada.PalmForm = new PalmForm();
			}
			DemoForm.FormularioPalma = fEnrolar.PersonaCapturada.PalmForm;
			string directoryName = Path.GetDirectoryName(Application.ExecutablePath);
			char directorySeparatorChar = Path.DirectorySeparatorChar;
			string NewFolderName = string.Concat(directoryName, directorySeparatorChar.ToString(), "CapturaDactilar");
			if (Directory.Exists(NewFolderName))
			{
				(new DirectoryInfo(NewFolderName)).Delete(true);
			}
			System.Windows.Forms.Cursor.Current = Cursors.Default;
			if (form.ShowDialog(this) != System.Windows.Forms.DialogResult.OK)
			{
				fEnrolar.PersonaCapturada.PalmForm.ListLiveFingers = DemoForm.FormularioPalma.ListLiveFingers;
				fEnrolar.PersonaCapturada.PalmForm.AmputeeLiveFingers = DemoForm.FormularioPalma.AmputeeLiveFingers;
				fEnrolar.PersonaCapturada.PalmForm.DiscardLiveFingers = DemoForm.FormularioPalma.DiscardLiveFingers;
				fEnrolar.PersonaCapturada.PalmForm.ListLivePalms = DemoForm.FormularioPalma.ListLivePalms;
				fEnrolar.PersonaCapturada.PalmForm.AmputeeLivePalms = DemoForm.FormularioPalma.AmputeeLivePalms;
				fEnrolar.PersonaCapturada.PalmForm.DiscardLivePalms = DemoForm.FormularioPalma.DiscardLivePalms;
			}
			this.btnFormulario.Enabled = true;
			System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
			(new Serializer()).SerializeEpd(fEnrolar.PersonaCapturada, fPrincipal.RutaEpd);
			this.porcentajes();
			System.Windows.Forms.Cursor.Current = Cursors.Default;
			form.Dispose();
		}

		private void btnMarcasTatu_Click(object sender, EventArgs e)
		{
			(new fMarcasTatuaje()
			{
				MdiParent = base.ParentForm
			}).Show();
			base.Close();
		}

		private void btnRostro_Click(object sender, EventArgs e)
		{
			try
			{
				(new fCapturaFacial()
				{
					MdiParent = base.ParentForm
				}).Show();
				base.Close();
			}
			catch (Exception exception)
			{
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void CambiarTamañoyFormatoImagenes()
		{
			DataImageCore dataImageCore;
			try
			{
				if (this.FormularioPalma != null)
				{
					if (this.FormularioPalma.Form != null)
					{
						this.FormularioPalma.Form = this.ser.ImagenToJPGByteArray((Bitmap)this.ser.CambiarTamanoImagen(this.ser.byteArrayToImage(this.FormularioPalma.Form), 800));
					}
					if (this.FormularioPalma.BackPage != null)
					{
						this.FormularioPalma.BackPage = this.ser.ImagenToJPGByteArray((Bitmap)this.ser.CambiarTamanoImagen(this.ser.byteArrayToImage(this.FormularioPalma.BackPage), 800));
					}
					if (this.FormularioPalma.ListModels != null)
					{
						this.FormularioPalma.ListModels = this.ser.ImagenToJPGByteArray((Bitmap)this.ser.CambiarTamanoImagen(this.ser.byteArrayToImage(this.FormularioPalma.ListModels), 800));
					}
					if (this.FormularioPalma.RolledPrints != null)
					{
						if (this.FormularioPalma.RolledPrints.Any<DataImageCore?>())
						{
							for (int i = 0; i < this.FormularioPalma.RolledPrints.Count<DataImageCore?>(); i++)
							{
								if (this.FormularioPalma.RolledPrints[i].HasValue)
								{
									DataImageCore VDataImageCore = this.FormularioPalma.RolledPrints[i].Value;
									string subtype = VDataImageCore.Bir.BDBInfo.Subtype;
									if (subtype != null)
									{
										if (subtype == "RolledPrintsRight")
										{
											DataImageCore?[] rolledPrints = this.FormularioPalma.RolledPrints;
											dataImageCore = new DataImageCore()
											{
												Bir = new BIR()
												{
													BDB = this.ser.ImagenToJPGByteArray((Bitmap)this.ser.CambiarTamanoImagen(this.ser.byteArrayToImage(VDataImageCore.Bir.BDB), 800)),
													BDBInfo = new BIRBDBInfo()
													{
														Subtype = "RolledPrintsRight"
													}
												},
												RegionType = RegionType.Segmentado
											};
											rolledPrints[0] = new DataImageCore?(dataImageCore);
										}
										else if (subtype == "RolledPrintsLeft")
										{
											DataImageCore?[] nullable = this.FormularioPalma.RolledPrints;
											dataImageCore = new DataImageCore()
											{
												Bir = new BIR()
												{
													BDB = this.ser.ImagenToJPGByteArray((Bitmap)this.ser.CambiarTamanoImagen(this.ser.byteArrayToImage(VDataImageCore.Bir.BDB), 800)),
													BDBInfo = new BIRBDBInfo()
													{
														Subtype = "RolledPrintsLeft"
													}
												},
												RegionType = RegionType.Segmentado
											};
											nullable[1] = new DataImageCore?(dataImageCore);
										}
									}
								}
							}
						}
					}
					if (this.FormularioPalma.SimultaneousPrints != null)
					{
						if (this.FormularioPalma.SimultaneousPrints.Any<DataImageCore?>())
						{
							for (int i = 0; i < this.FormularioPalma.SimultaneousPrints.Count<DataImageCore?>(); i++)
							{
								if (this.FormularioPalma.SimultaneousPrints[i].HasValue)
								{
									DataImageCore VDataImageCore = this.FormularioPalma.SimultaneousPrints[i].Value;
									string str = VDataImageCore.Bir.BDBInfo.Subtype;
									if (str != null)
									{
										if (str == "SimultaneousPrintsLeft")
										{
											DataImageCore?[] simultaneousPrints = this.FormularioPalma.SimultaneousPrints;
											dataImageCore = new DataImageCore()
											{
												Bir = new BIR()
												{
													BDB = this.ser.ImagenToJPGByteArray((Bitmap)this.ser.CambiarTamanoImagen(this.ser.byteArrayToImage(VDataImageCore.Bir.BDB), 500)),
													BDBInfo = new BIRBDBInfo()
													{
														Subtype = "SimultaneousPrintsLeft"
													}
												},
												RegionType = RegionType.Segmentado
											};
											simultaneousPrints[0] = new DataImageCore?(dataImageCore);
										}
										else if (str == "SimultaneousPrintsThumb")
										{
											DataImageCore?[] nullableArray = this.FormularioPalma.SimultaneousPrints;
											dataImageCore = new DataImageCore()
											{
												Bir = new BIR()
												{
													BDB = this.ser.ImagenToJPGByteArray((Bitmap)this.ser.CambiarTamanoImagen(this.ser.byteArrayToImage(VDataImageCore.Bir.BDB), 500)),
													BDBInfo = new BIRBDBInfo()
													{
														Subtype = "SimultaneousPrintsThumb"
													}
												},
												RegionType = RegionType.Segmentado
											};
											nullableArray[1] = new DataImageCore?(dataImageCore);
										}
										else if (str == "SimultaneousPrintsRight")
										{
											DataImageCore?[] simultaneousPrints1 = this.FormularioPalma.SimultaneousPrints;
											dataImageCore = new DataImageCore()
											{
												Bir = new BIR()
												{
													BDB = this.ser.ImagenToJPGByteArray((Bitmap)this.ser.CambiarTamanoImagen(this.ser.byteArrayToImage(VDataImageCore.Bir.BDB), 500)),
													BDBInfo = new BIRBDBInfo()
													{
														Subtype = "SimultaneousPrintsRight"
													}
												},
												RegionType = RegionType.Segmentado
											};
											simultaneousPrints1[2] = new DataImageCore?(dataImageCore);
										}
									}
								}
							}
						}
					}
					if (this.FormularioPalma.PalmsPrints != null)
					{
						if (this.FormularioPalma.PalmsPrints.Any<DataImageCore?>())
						{
							for (int i = 0; i < this.FormularioPalma.PalmsPrints.Count<DataImageCore?>(); i++)
							{
								if (this.FormularioPalma.PalmsPrints[i].HasValue)
								{
									DataImageCore VDataImageCore = this.FormularioPalma.PalmsPrints[i].Value;
									string subtype1 = VDataImageCore.Bir.BDBInfo.Subtype;
									if (subtype1 != null)
									{
										if (subtype1 == "PalmsPrintsRight")
										{
											DataImageCore?[] palmsPrints = this.FormularioPalma.PalmsPrints;
											dataImageCore = new DataImageCore()
											{
												Bir = new BIR()
												{
													BDB = this.ser.ImagenToBMPByteArray((Bitmap)this.ser.byteArrayToImage(VDataImageCore.Bir.BDB)),
													BDBInfo = new BIRBDBInfo()
													{
														Subtype = "PalmsPrintsRight"
													}
												},
												RegionType = RegionType.Segmentado
											};
											palmsPrints[0] = new DataImageCore?(dataImageCore);
										}
										else if (subtype1 == "PalmsPrintsLeft")
										{
											DataImageCore?[] palmsPrints1 = this.FormularioPalma.PalmsPrints;
											dataImageCore = new DataImageCore()
											{
												Bir = new BIR()
												{
													BDB = this.ser.ImagenToBMPByteArray((Bitmap)this.ser.byteArrayToImage(VDataImageCore.Bir.BDB)),
													BDBInfo = new BIRBDBInfo()
													{
														Subtype = "PalmsPrintsLeft"
													}
												},
												RegionType = RegionType.Segmentado
											};
											palmsPrints1[1] = new DataImageCore?(dataImageCore);
										}
									}
								}
							}
						}
					}
					if (this.FormularioPalma.RolledPrintsBIR != null)
					{
						if (this.FormularioPalma.RolledPrintsBIR.Any<BIR>())
						{
							foreach (BIR vBir in this.FormularioPalma.RolledPrintsBIR)
							{
								if (vBir.BDB != null)
								{
									string str1 = vBir.BDBInfo.Subtype;
									if (str1 != null)
									{
										switch (str1)
										{
											case "Right Thumb":
											{
												this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Right Thumb").BDB = vBir.BDB;
												break;
											}
											case "Right IndexFinger":
											{
												this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Right IndexFinger").BDB = vBir.BDB;
												break;
											}
											case "Right MiddleFinger":
											{
												this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Right MiddleFinger").BDB = vBir.BDB;
												break;
											}
											case "Right RingFinger":
											{
												this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Right RingFinger").BDB = vBir.BDB;
												break;
											}
											case "Right LittleFinger":
											{
												this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Right LittleFinger").BDB = vBir.BDB;
												break;
											}
											case "Left Thumb":
											{
												this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Left Thumb").BDB = vBir.BDB;
												break;
											}
											case "Left IndexFinger":
											{
												this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Left IndexFinger").BDB = vBir.BDB;
												break;
											}
											case "Left MiddleFinger":
											{
												this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Left MiddleFinger").BDB = vBir.BDB;
												break;
											}
											case "Left RingFinger":
											{
												this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Left RingFinger").BDB = vBir.BDB;
												break;
											}
											case "Left LittleFinger":
											{
												this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Left LittleFinger").BDB = vBir.BDB;
												break;
											}
										}
									}
								}
							}
						}
					}
					if (fEnrolar.PersonaCapturada.PalmForm.ListLivePalms != null)
					{
						if (fEnrolar.PersonaCapturada.PalmForm.ListLivePalms.Any<BaseBiometric>())
						{
							foreach (BaseBiometric bd in this.FormularioPalma.ListLivePalms)
							{
								int indexFinger = bd.IndexFinger;
								if (indexFinger == 21)
								{
									this.FormularioPalma.ListLivePalms.First<BaseBiometric>((BaseBiometric x) => x.IndexFinger == 21).Image = bd.Image;
								}
								else if (indexFinger == 23)
								{
									this.FormularioPalma.ListLivePalms.First<BaseBiometric>((BaseBiometric x) => x.IndexFinger == 23).Image = bd.Image;
								}
							}
						}
					}
				}
			}
			catch
			{
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

		private void fEnrolar_Load(object sender, EventArgs e)
		{
			this.porcentajes();
		}

		private void FormateaArchivos()
		{
			try
			{
				if (fEnrolar.PersonaCapturada == null)
				{
					fEnrolar.PersonaCapturada = new CapturedPerson();
				}
				if (fEnrolar.PersonaCapturada.PalmForm != null)
				{
					this.FormularioPalma = fEnrolar.PersonaCapturada.PalmForm;
					this.CambiarTamañoyFormatoImagenes();
					fEnrolar.PersonaCapturada.PalmForm = this.FormularioPalma;
					(new Serializer()).SerializeEpd(fEnrolar.PersonaCapturada, fPrincipal.RutaEpd);
				}
			}
			catch (Exception exception)
			{
			}
		}

		private float HallaPorcentaje()
		{
			DateTime dDate;
			Identity x = fEnrolar.PersonaCapturada.OfflinePerson.Identities[0];
			RecordData y = fEnrolar.PersonaCapturada.RecordData;
			int camposLleno = 0;
			int camposTotal = 18;
			if ((y.Motive == null ? false : !string.IsNullOrWhiteSpace(y.Motive.Id)))
			{
				camposLleno++;
			}
			if ((y.Crime == null ? false : !string.IsNullOrWhiteSpace(y.Crime.Id)))
			{
				camposLleno++;
			}
			if ((x.PersonType == null ? false : !string.IsNullOrWhiteSpace(x.PersonType.Id)))
			{
				camposLleno++;
			}
			if (!string.IsNullOrWhiteSpace(x.Identification))
			{
				camposLleno++;
			}
			if (!string.IsNullOrWhiteSpace(x.FirstLastName))
			{
				camposLleno++;
			}
			if (!string.IsNullOrWhiteSpace(x.SecondName))
			{
				camposLleno++;
			}
			if (!string.IsNullOrWhiteSpace(x.FirstLastName))
			{
				camposLleno++;
			}
			if (!string.IsNullOrWhiteSpace(x.SecondLastName))
			{
				camposLleno++;
			}
			if (!string.IsNullOrWhiteSpace(x.FatherName))
			{
				camposLleno++;
			}
			if (!string.IsNullOrWhiteSpace(x.MotherName))
			{
				camposLleno++;
			}
			if ((x.Country == null ? false : !string.IsNullOrWhiteSpace(x.Country.Id)))
			{
				camposLleno++;
			}
			if ((y.Addresses == null ? false : y.Addresses.Any<Address>()))
			{
				camposLleno++;
			}
			if ((x.Sex == null ? false : !string.IsNullOrWhiteSpace(x.Sex.Id)))
			{
				camposLleno++;
			}
			if ((x.Skin == null ? false : !string.IsNullOrWhiteSpace(x.Skin.Id)))
			{
				camposLleno++;
			}
			if (!string.IsNullOrWhiteSpace(y.Complexion))
			{
				camposLleno++;
			}
			if (y.Weigth > 39)
			{
				camposLleno++;
			}
			if (y.BodySize > 49)
			{
				camposLleno++;
			}
			if (!string.IsNullOrWhiteSpace(x.GeneticCode))
			{
				if (DateTime.TryParse(x.GeneticCode, out dDate))
				{
					camposLleno++;
				}
			}
			return (float)camposLleno / (float)camposTotal;
		}

		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fEnrolar));
            this.ribbon1 = new System.Windows.Forms.Ribbon();
            this.ribbonTab1 = new System.Windows.Forms.RibbonTab();
            this.ribbonPanel1 = new System.Windows.Forms.RibbonPanel();
            this.ribtnGuardarCerrar = new System.Windows.Forms.RibbonButton();
            this.ribtnGuardar = new System.Windows.Forms.RibbonButton();
            this.ribbonButton1 = new System.Windows.Forms.RibbonButton();
            this.ribbonButtonList1 = new System.Windows.Forms.RibbonButtonList();
            this.ribbonButton2 = new System.Windows.Forms.RibbonButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lblNacionalidad = new System.Windows.Forms.Label();
            this.lblNombrePadre = new System.Windows.Forms.Label();
            this.lblNombreMadre = new System.Windows.Forms.Label();
            this.lblNombreCompleto = new System.Windows.Forms.Label();
            this.btnFormulario = new System.Windows.Forms.Button();
            this.lblnac = new System.Windows.Forms.Label();
            this.lblnompadre = new System.Windows.Forms.Label();
            this.lblnommadre = new System.Windows.Forms.Label();
            this.lblnomcompleto = new System.Windows.Forms.Label();
            this.pbFrontal = new System.Windows.Forms.PictureBox();
            this.btnMarcasTatu = new System.Windows.Forms.Button();
            this.btnRostro = new System.Windows.Forms.Button();
            this.btnHuellasDactilares = new System.Windows.Forms.Button();
            this.btnDatosBiograficos = new System.Windows.Forms.Button();
            this.btnConcluirEnrol = new System.Windows.Forms.Button();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ribbonTab2 = new System.Windows.Forms.RibbonTab();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFrontal)).BeginInit();
            this.tableLayoutPanel11.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon1
            // 
            this.ribbon1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ribbon1.Location = new System.Drawing.Point(0, 0);
            this.ribbon1.Minimized = false;
            this.ribbon1.Name = "ribbon1";
            // 
            // 
            // 
            this.ribbon1.OrbDropDown.BorderRoundness = 8;
            this.ribbon1.OrbDropDown.Location = new System.Drawing.Point(0, 0);
            this.ribbon1.OrbDropDown.Name = "";
            this.ribbon1.OrbDropDown.Size = new System.Drawing.Size(527, 447);
            this.ribbon1.OrbDropDown.TabIndex = 0;
            this.ribbon1.OrbStyle = System.Windows.Forms.RibbonOrbStyle.Office_2013;
            this.ribbon1.OrbVisible = false;
            // 
            // 
            // 
            this.ribbon1.QuickAccessToolbar.Visible = false;
            this.ribbon1.RibbonTabFont = new System.Drawing.Font("Trebuchet MS", 9F);
            this.ribbon1.Size = new System.Drawing.Size(1258, 152);
            this.ribbon1.TabIndex = 2;
            this.ribbon1.Tabs.Add(this.ribbonTab1);
            this.ribbon1.Tabs.Add(this.ribbonTab2);
            this.ribbon1.TabSpacing = 4;
            this.ribbon1.Text = "ribbon1";
            this.ribbon1.ThemeColor = System.Windows.Forms.RibbonTheme.Blue;
            // 
            // ribbonTab1
            // 
            this.ribbonTab1.Name = "ribbonTab1";
            this.ribbonTab1.Panels.Add(this.ribbonPanel1);
            this.ribbonTab1.Text = "Acciones";
            // 
            // ribbonPanel1
            // 
            this.ribbonPanel1.Items.Add(this.ribtnGuardarCerrar);
            this.ribbonPanel1.Items.Add(this.ribtnGuardar);
            this.ribbonPanel1.Items.Add(this.ribbonButton2);
            this.ribbonPanel1.Name = "ribbonPanel1";
            this.ribbonPanel1.Text = "";
            // 
            // ribtnGuardarCerrar
            // 
            this.ribtnGuardarCerrar.Image = ((System.Drawing.Image)(resources.GetObject("ribtnGuardarCerrar.Image")));
            this.ribtnGuardarCerrar.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribtnGuardarCerrar.LargeImage")));
            this.ribtnGuardarCerrar.Name = "ribtnGuardarCerrar";
            this.ribtnGuardarCerrar.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribtnGuardarCerrar.SmallImage")));
            this.ribtnGuardarCerrar.Text = "Guardar y cerrar";
            this.ribtnGuardarCerrar.Click += new System.EventHandler(this.ribtnGuardarCerrar_Click);
            // 
            // ribtnGuardar
            // 
            this.ribtnGuardar.DropDownItems.Add(this.ribbonButton1);
            this.ribtnGuardar.DropDownItems.Add(this.ribbonButtonList1);
            this.ribtnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("ribtnGuardar.Image")));
            this.ribtnGuardar.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribtnGuardar.LargeImage")));
            this.ribtnGuardar.Name = "ribtnGuardar";
            this.ribtnGuardar.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribtnGuardar.SmallImage")));
            this.ribtnGuardar.Text = "Guardar";
            this.ribtnGuardar.Click += new System.EventHandler(this.ribtnGuardar_Click);
            // 
            // ribbonButton1
            // 
            this.ribbonButton1.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButton1.Image")));
            this.ribbonButton1.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton1.LargeImage")));
            this.ribbonButton1.Name = "ribbonButton1";
            this.ribbonButton1.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton1.SmallImage")));
            this.ribbonButton1.Text = "ribbonButton1";
            // 
            // ribbonButtonList1
            // 
            this.ribbonButtonList1.ButtonsSizeMode = System.Windows.Forms.RibbonElementSizeMode.Large;
            this.ribbonButtonList1.FlowToBottom = false;
            this.ribbonButtonList1.ItemsSizeInDropwDownMode = new System.Drawing.Size(7, 5);
            this.ribbonButtonList1.Name = "ribbonButtonList1";
            this.ribbonButtonList1.Text = "ribbonButtonList1";
            // 
            // ribbonButton2
            // 
            this.ribbonButton2.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButton2.Image")));
            this.ribbonButton2.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton2.LargeImage")));
            this.ribbonButton2.Name = "ribbonButton2";
            this.ribbonButton2.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton2.SmallImage")));
            this.ribbonButton2.Text = "Cerrar";
            this.ribbonButton2.Click += new System.EventHandler(this.ribtnCerrar_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(797, 174);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(215, 75);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.Visible = false;
            // 
            // lblNacionalidad
            // 
            this.lblNacionalidad.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblNacionalidad.AutoSize = true;
            this.lblNacionalidad.BackColor = System.Drawing.Color.Transparent;
            this.lblNacionalidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNacionalidad.ForeColor = System.Drawing.Color.White;
            this.lblNacionalidad.Location = new System.Drawing.Point(150, 110);
            this.lblNacionalidad.Name = "lblNacionalidad";
            this.lblNacionalidad.Size = new System.Drawing.Size(0, 17);
            this.lblNacionalidad.TabIndex = 59;
            // 
            // lblNombrePadre
            // 
            this.lblNombrePadre.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblNombrePadre.AutoSize = true;
            this.lblNombrePadre.BackColor = System.Drawing.Color.Transparent;
            this.lblNombrePadre.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombrePadre.ForeColor = System.Drawing.Color.White;
            this.lblNombrePadre.Location = new System.Drawing.Point(150, 76);
            this.lblNombrePadre.Name = "lblNombrePadre";
            this.lblNombrePadre.Size = new System.Drawing.Size(0, 17);
            this.lblNombrePadre.TabIndex = 58;
            // 
            // lblNombreMadre
            // 
            this.lblNombreMadre.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblNombreMadre.AutoSize = true;
            this.lblNombreMadre.BackColor = System.Drawing.Color.Transparent;
            this.lblNombreMadre.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreMadre.ForeColor = System.Drawing.Color.White;
            this.lblNombreMadre.Location = new System.Drawing.Point(150, 42);
            this.lblNombreMadre.Name = "lblNombreMadre";
            this.lblNombreMadre.Size = new System.Drawing.Size(0, 17);
            this.lblNombreMadre.TabIndex = 57;
            // 
            // lblNombreCompleto
            // 
            this.lblNombreCompleto.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblNombreCompleto.AutoSize = true;
            this.lblNombreCompleto.BackColor = System.Drawing.Color.Transparent;
            this.lblNombreCompleto.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreCompleto.ForeColor = System.Drawing.Color.White;
            this.lblNombreCompleto.Location = new System.Drawing.Point(150, 8);
            this.lblNombreCompleto.Name = "lblNombreCompleto";
            this.lblNombreCompleto.Size = new System.Drawing.Size(0, 17);
            this.lblNombreCompleto.TabIndex = 56;
            // 
            // btnFormulario
            // 
            this.btnFormulario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFormulario.BackColor = System.Drawing.Color.LightCyan;
            this.btnFormulario.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFormulario.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(63)))), ((int)(((byte)(105)))));
            this.btnFormulario.FlatAppearance.BorderSize = 3;
            this.btnFormulario.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSteelBlue;
            this.btnFormulario.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnFormulario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFormulario.Font = new System.Drawing.Font("Microsoft Sans Serif", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFormulario.Image = ((System.Drawing.Image)(resources.GetObject("btnFormulario.Image")));
            this.btnFormulario.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnFormulario.Location = new System.Drawing.Point(462, 174);
            this.btnFormulario.Name = "btnFormulario";
            this.btnFormulario.Size = new System.Drawing.Size(329, 165);
            this.btnFormulario.TabIndex = 55;
            this.btnFormulario.Text = "TARJETA DE FILIACION";
            this.btnFormulario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFormulario.UseVisualStyleBackColor = false;
            this.btnFormulario.Click += new System.EventHandler(this.btnFormulario_Click);
            // 
            // lblnac
            // 
            this.lblnac.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblnac.AutoSize = true;
            this.lblnac.BackColor = System.Drawing.Color.Transparent;
            this.lblnac.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblnac.ForeColor = System.Drawing.Color.White;
            this.lblnac.Location = new System.Drawing.Point(50, 110);
            this.lblnac.Name = "lblnac";
            this.lblnac.Size = new System.Drawing.Size(94, 17);
            this.lblnac.TabIndex = 54;
            this.lblnac.Text = "Nacionalidad:";
            // 
            // lblnompadre
            // 
            this.lblnompadre.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblnompadre.AutoSize = true;
            this.lblnompadre.BackColor = System.Drawing.Color.Transparent;
            this.lblnompadre.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblnompadre.ForeColor = System.Drawing.Color.White;
            this.lblnompadre.Location = new System.Drawing.Point(18, 76);
            this.lblnompadre.Name = "lblnompadre";
            this.lblnompadre.Size = new System.Drawing.Size(126, 17);
            this.lblnompadre.TabIndex = 53;
            this.lblnompadre.Text = "Nombre del padre:";
            // 
            // lblnommadre
            // 
            this.lblnommadre.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblnommadre.AutoSize = true;
            this.lblnommadre.BackColor = System.Drawing.Color.Transparent;
            this.lblnommadre.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblnommadre.ForeColor = System.Drawing.Color.White;
            this.lblnommadre.Location = new System.Drawing.Point(3, 42);
            this.lblnommadre.Name = "lblnommadre";
            this.lblnommadre.Size = new System.Drawing.Size(141, 17);
            this.lblnommadre.TabIndex = 52;
            this.lblnommadre.Text = "Nombre de la madre:";
            // 
            // lblnomcompleto
            // 
            this.lblnomcompleto.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblnomcompleto.AutoSize = true;
            this.lblnomcompleto.BackColor = System.Drawing.Color.Transparent;
            this.lblnomcompleto.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblnomcompleto.ForeColor = System.Drawing.Color.White;
            this.lblnomcompleto.Location = new System.Drawing.Point(21, 8);
            this.lblnomcompleto.Name = "lblnomcompleto";
            this.lblnomcompleto.Size = new System.Drawing.Size(123, 17);
            this.lblnomcompleto.TabIndex = 51;
            this.lblnomcompleto.Text = "Nombre completo:";
            // 
            // pbFrontal
            // 
            this.pbFrontal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbFrontal.BackColor = System.Drawing.Color.White;
            this.pbFrontal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbFrontal.Location = new System.Drawing.Point(128, 3);
            this.pbFrontal.Name = "pbFrontal";
            this.pbFrontal.Size = new System.Drawing.Size(105, 137);
            this.pbFrontal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbFrontal.TabIndex = 50;
            this.pbFrontal.TabStop = false;
            // 
            // btnMarcasTatu
            // 
            this.btnMarcasTatu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMarcasTatu.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnMarcasTatu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMarcasTatu.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(63)))), ((int)(((byte)(105)))));
            this.btnMarcasTatu.FlatAppearance.BorderSize = 3;
            this.btnMarcasTatu.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSteelBlue;
            this.btnMarcasTatu.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnMarcasTatu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMarcasTatu.Font = new System.Drawing.Font("Microsoft Sans Serif", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMarcasTatu.Image = ((System.Drawing.Image)(resources.GetObject("btnMarcasTatu.Image")));
            this.btnMarcasTatu.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnMarcasTatu.Location = new System.Drawing.Point(797, 3);
            this.btnMarcasTatu.Name = "btnMarcasTatu";
            this.btnMarcasTatu.Size = new System.Drawing.Size(329, 165);
            this.btnMarcasTatu.TabIndex = 49;
            this.btnMarcasTatu.Text = "ARCHIVOS ADJUNTOS";
            this.btnMarcasTatu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMarcasTatu.UseVisualStyleBackColor = false;
            this.btnMarcasTatu.Click += new System.EventHandler(this.btnMarcasTatu_Click);
            // 
            // btnRostro
            // 
            this.btnRostro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRostro.BackColor = System.Drawing.Color.LightCyan;
            this.btnRostro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRostro.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(63)))), ((int)(((byte)(105)))));
            this.btnRostro.FlatAppearance.BorderSize = 3;
            this.btnRostro.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSteelBlue;
            this.btnRostro.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnRostro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRostro.Font = new System.Drawing.Font("Microsoft Sans Serif", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRostro.Image = ((System.Drawing.Image)(resources.GetObject("btnRostro.Image")));
            this.btnRostro.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnRostro.Location = new System.Drawing.Point(127, 174);
            this.btnRostro.Name = "btnRostro";
            this.btnRostro.Size = new System.Drawing.Size(329, 165);
            this.btnRostro.TabIndex = 48;
            this.btnRostro.Text = "CAPTURA\r\nFACIAL";
            this.btnRostro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRostro.UseVisualStyleBackColor = false;
            this.btnRostro.Click += new System.EventHandler(this.btnRostro_Click);
            // 
            // btnHuellasDactilares
            // 
            this.btnHuellasDactilares.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHuellasDactilares.BackColor = System.Drawing.Color.SkyBlue;
            this.btnHuellasDactilares.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHuellasDactilares.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(63)))), ((int)(((byte)(105)))));
            this.btnHuellasDactilares.FlatAppearance.BorderSize = 3;
            this.btnHuellasDactilares.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSteelBlue;
            this.btnHuellasDactilares.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnHuellasDactilares.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuellasDactilares.Font = new System.Drawing.Font("Microsoft Sans Serif", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHuellasDactilares.Image = ((System.Drawing.Image)(resources.GetObject("btnHuellasDactilares.Image")));
            this.btnHuellasDactilares.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnHuellasDactilares.Location = new System.Drawing.Point(462, 3);
            this.btnHuellasDactilares.Name = "btnHuellasDactilares";
            this.btnHuellasDactilares.Size = new System.Drawing.Size(329, 165);
            this.btnHuellasDactilares.TabIndex = 47;
            this.btnHuellasDactilares.Text = "HUELLAS DACTILARES";
            this.btnHuellasDactilares.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHuellasDactilares.UseVisualStyleBackColor = false;
            this.btnHuellasDactilares.Click += new System.EventHandler(this.btnHuellasDactilares_Click);
            // 
            // btnDatosBiograficos
            // 
            this.btnDatosBiograficos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDatosBiograficos.BackColor = System.Drawing.Color.GhostWhite;
            this.btnDatosBiograficos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDatosBiograficos.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(63)))), ((int)(((byte)(105)))));
            this.btnDatosBiograficos.FlatAppearance.BorderSize = 3;
            this.btnDatosBiograficos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSteelBlue;
            this.btnDatosBiograficos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnDatosBiograficos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDatosBiograficos.Font = new System.Drawing.Font("Microsoft Sans Serif", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDatosBiograficos.Image = ((System.Drawing.Image)(resources.GetObject("btnDatosBiograficos.Image")));
            this.btnDatosBiograficos.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnDatosBiograficos.Location = new System.Drawing.Point(127, 3);
            this.btnDatosBiograficos.Name = "btnDatosBiograficos";
            this.btnDatosBiograficos.Size = new System.Drawing.Size(329, 165);
            this.btnDatosBiograficos.TabIndex = 46;
            this.btnDatosBiograficos.Text = "DATOS BIOGRAFICOS";
            this.btnDatosBiograficos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDatosBiograficos.UseVisualStyleBackColor = false;
            this.btnDatosBiograficos.Click += new System.EventHandler(this.btnDatosBiograficos_Click);
            // 
            // btnConcluirEnrol
            // 
            this.btnConcluirEnrol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnConcluirEnrol.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(103)))), ((int)(((byte)(242)))));
            this.btnConcluirEnrol.FlatAppearance.BorderColor = System.Drawing.Color.Teal;
            this.btnConcluirEnrol.FlatAppearance.BorderSize = 2;
            this.btnConcluirEnrol.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MediumAquamarine;
            this.btnConcluirEnrol.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConcluirEnrol.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConcluirEnrol.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnConcluirEnrol.Location = new System.Drawing.Point(796, 93);
            this.btnConcluirEnrol.Name = "btnConcluirEnrol";
            this.btnConcluirEnrol.Size = new System.Drawing.Size(148, 47);
            this.btnConcluirEnrol.TabIndex = 60;
            this.btnConcluirEnrol.Text = "CONCLUIR ENROLAMIENTO";
            this.btnConcluirEnrol.UseVisualStyleBackColor = false;
            this.btnConcluirEnrol.Click += new System.EventHandler(this.btnConcluirEnrol_Click);
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel11.ColumnCount = 1;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel11.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel11.Location = new System.Drawing.Point(0, 134);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 1;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(1258, 23);
            this.tableLayoutPanel11.TabIndex = 61;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(3, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1252, 18);
            this.label4.TabIndex = 26;
            this.label4.Text = "DETALLE DE ENROLAMIENTO";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.90099F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.73267F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.73267F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.73267F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.90099F));
            this.tableLayoutPanel1.Controls.Add(this.btnDatosBiograficos, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnHuellasDactilares, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnMarcasTatu, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnRostro, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnFormulario, 2, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 329);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1255, 342);
            this.tableLayoutPanel1.TabIndex = 62;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.96443F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.860106F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.27663F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.11129F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.70763F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.pbFrontal, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnConcluirEnrol, 3, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 158);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 86.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.33333F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1258, 165);
            this.tableLayoutPanel2.TabIndex = 63;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.lblnommadre, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblnompadre, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.lblnac, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.lblNombreCompleto, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblNombrePadre, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.lblNombreMadre, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblNacionalidad, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.lblnomcompleto, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(239, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(551, 136);
            this.tableLayoutPanel3.TabIndex = 63;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1155, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 105);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 65;
            this.pictureBox1.TabStop = false;
            // 
            // ribbonTab2
            // 
            this.ribbonTab2.Name = "ribbonTab2";
            this.ribbonTab2.Text = "ribbonTab2";
            // 
            // fEnrolar
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(63)))), ((int)(((byte)(105)))));
            this.ClientSize = new System.Drawing.Size(1258, 691);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel11);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.ribbon1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "fEnrolar";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.fEnrolar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFrontal)).EndInit();
            this.tableLayoutPanel11.ResumeLayout(false);
            this.tableLayoutPanel11.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

		}

		private void lblPorMarcatatu_Click(object sender, EventArgs e)
		{
		}

		private void porcentajes()
		{
			try
			{
				if ((fEnrolar.PersonaCapturada == null ? false : fEnrolar.PersonaCapturada.RecordData != null))
				{
					fEnrolar.PersonaCapturada.BasicData.BiographicCompleted = this.HallaPorcentaje();
				}
				if (fEnrolar.PersonaCapturada.BasicData.BiographicCompleted >= 1f)
				{
					fEnrolar.PersonaCapturada.BasicData.BiographicCompleted = 1f;
				}
				Button button = this.btnDatosBiograficos;
				double num = Math.Round((double)(fEnrolar.PersonaCapturada.BasicData.BiographicCompleted * 100f), 1);
				button.Text = string.Concat("DATOS\nBIOGRAFICOS\n", num.ToString(), "%");
				if (fEnrolar.PersonaCapturada.BasicData.BiographicCompleted == 0f)
				{
					this.btnDatosBiograficos.BackColor = Color.White;
				}
				if ((fEnrolar.PersonaCapturada.BasicData.BiographicCompleted <= 0f ? false : fEnrolar.PersonaCapturada.BasicData.BiographicCompleted < 1f))
				{
					this.btnDatosBiograficos.BackColor = Color.SkyBlue;
				}
				if (fEnrolar.PersonaCapturada.BasicData.BiographicCompleted == 1f)
				{
					this.btnDatosBiograficos.BackColor = Color.DodgerBlue;
				}
				int c = 0;
				if ((fEnrolar.PersonaCapturada == null ? false : fEnrolar.PersonaCapturada.RecordData != null))
				{
					if (fEnrolar.PersonaCapturada.RecordData.FaceFrontal != null)
					{
						if (fEnrolar.PersonaCapturada.RecordData.FaceFrontal.OriginalImageArr != null)
						{
							c++;
						}
						if (fEnrolar.PersonaCapturada.RecordData.FaceFrontal.NormalizedImageArr != null)
						{
							c++;
						}
						if (fEnrolar.PersonaCapturada.RecordData.FaceFrontal.RuledImageArr != null)
						{
							c++;
						}
					}
				}
				fEnrolar.PersonaCapturada.BasicData.FaceCompleted = (float)c / 3f;
				if (fEnrolar.PersonaCapturada.BasicData.FaceCompleted == 0f)
				{
					this.btnRostro.BackColor = Color.White;
				}
				if ((fEnrolar.PersonaCapturada.BasicData.FaceCompleted <= 0f ? false : fEnrolar.PersonaCapturada.BasicData.FaceCompleted < 1f))
				{
					this.btnRostro.BackColor = Color.SkyBlue;
				}
				if (fEnrolar.PersonaCapturada.BasicData.FaceCompleted == 1f)
				{
					this.btnRostro.BackColor = Color.DodgerBlue;
				}
				Button button1 = this.btnRostro;
				num = Math.Round((double)(fEnrolar.PersonaCapturada.BasicData.FaceCompleted * 100f), 1);
				button1.Text = string.Concat("CAPTURA\nFACIAL\n", num.ToString(), "%");
				this.btnRostro.Refresh();
				int mt = 0;
				if ((fEnrolar.PersonaCapturada == null ? false : fEnrolar.PersonaCapturada.RecordData != null))
				{
					if (fEnrolar.PersonaCapturada.RecordData.MarkDatas != null && fEnrolar.PersonaCapturada.RecordData.MarkDatas.Count > 0)
					{
						mt++;
					}
					if (fEnrolar.PersonaCapturada.RecordData.TattooDatas != null && fEnrolar.PersonaCapturada.RecordData.TattooDatas.Count > 0)
					{
						mt++;
					}
				}
				fEnrolar.PersonaCapturada.BasicData.TattooMarkCompleted = (float)mt / 1f;
				if (fEnrolar.PersonaCapturada.BasicData.TattooMarkCompleted == 0f)
				{
					this.btnMarcasTatu.BackColor = Color.White;
				}
				if ((fEnrolar.PersonaCapturada.BasicData.TattooMarkCompleted <= 0f ? false : fEnrolar.PersonaCapturada.BasicData.TattooMarkCompleted < 1f))
				{
					this.btnMarcasTatu.BackColor = Color.SkyBlue;
				}
				if (fEnrolar.PersonaCapturada.BasicData.TattooMarkCompleted == 1f)
				{
					this.btnMarcasTatu.BackColor = Color.DodgerBlue;
				}
				Button button2 = this.btnMarcasTatu;
				num = Math.Round((double)(fEnrolar.PersonaCapturada.BasicData.TattooMarkCompleted * 100f), 1);
				button2.Text = string.Concat("ARCHIVOS\nADJUNTOS\n", num.ToString(), "%");
				this.btnMarcasTatu.Refresh();
				int h = 0;
				if ((fEnrolar.PersonaCapturada == null ? false : fEnrolar.PersonaCapturada.RecordData != null))
				{
					if (fEnrolar.PersonaCapturada.PalmForm != null)
					{
						if (fEnrolar.PersonaCapturada.PalmForm.ListLiveFingers != null)
						{
							h = Convert.ToInt32(fEnrolar.PersonaCapturada.PalmForm.ListLiveFingers.Count);
						}
						if (fEnrolar.PersonaCapturada.PalmForm.ListLivePalms != null)
						{
							h += Convert.ToInt32(fEnrolar.PersonaCapturada.PalmForm.ListLivePalms.Count);
						}
					}
				}
				fEnrolar.PersonaCapturada.BasicData.FingerPalmCompleted = (float)h / 12f;
				float val = fEnrolar.PersonaCapturada.BasicData.FingerPalmCompleted * 100f;
				if (fEnrolar.PersonaCapturada.BasicData.FingerPalmCompleted == 0f)
				{
					this.btnHuellasDactilares.BackColor = Color.White;
				}
				if ((fEnrolar.PersonaCapturada.BasicData.FingerPalmCompleted <= 0f ? false : fEnrolar.PersonaCapturada.BasicData.FingerPalmCompleted < 1f))
				{
					this.btnHuellasDactilares.BackColor = Color.SkyBlue;
				}
				if (fEnrolar.PersonaCapturada.BasicData.FingerPalmCompleted == 1f)
				{
					this.btnHuellasDactilares.BackColor = Color.DodgerBlue;
				}
				Button button3 = this.btnHuellasDactilares;
				int num1 = Convert.ToInt32(val);
				button3.Text = string.Concat("HUELLAS\nDACTILARES\n", num1.ToString(), "%");
				this.btnHuellasDactilares.Refresh();
				int f = 0;
				if ((fEnrolar.PersonaCapturada == null ? false : fEnrolar.PersonaCapturada.RecordData != null))
				{
					if (fEnrolar.PersonaCapturada.PalmForm != null)
					{
						if ((fEnrolar.PersonaCapturada.PalmForm.RolledPrints == null || !fEnrolar.PersonaCapturada.PalmForm.RolledPrints.Any<DataImageCore?>() || !fEnrolar.PersonaCapturada.PalmForm.RolledPrints[0].HasValue ? false : fEnrolar.PersonaCapturada.PalmForm.RolledPrints[1].HasValue))
						{
							f = Convert.ToInt32((int)fEnrolar.PersonaCapturada.PalmForm.RolledPrints.Length);
						}
						if ((fEnrolar.PersonaCapturada.PalmForm.SimultaneousPrints == null || !fEnrolar.PersonaCapturada.PalmForm.SimultaneousPrints.Any<DataImageCore?>() || !fEnrolar.PersonaCapturada.PalmForm.SimultaneousPrints[0].HasValue || !fEnrolar.PersonaCapturada.PalmForm.SimultaneousPrints[1].HasValue ? false : fEnrolar.PersonaCapturada.PalmForm.SimultaneousPrints[2].HasValue))
						{
							f += Convert.ToInt32((int)fEnrolar.PersonaCapturada.PalmForm.SimultaneousPrints.Length);
						}
						if ((fEnrolar.PersonaCapturada.PalmForm.PalmsPrints == null || !fEnrolar.PersonaCapturada.PalmForm.PalmsPrints.Any<DataImageCore?>() || !fEnrolar.PersonaCapturada.PalmForm.PalmsPrints[0].HasValue ? false : fEnrolar.PersonaCapturada.PalmForm.PalmsPrints[1].HasValue))
						{
							DataImageCore di1 = fEnrolar.PersonaCapturada.PalmForm.PalmsPrints[0].Value;
							DataImageCore di2 = fEnrolar.PersonaCapturada.PalmForm.PalmsPrints[1].Value;
							if ((di1.Bir.BDB == null ? false : di2.Bir.BDB != null))
							{
								f += Convert.ToInt32((int)fEnrolar.PersonaCapturada.PalmForm.PalmsPrints.Length);
							}
						}
						if (fEnrolar.PersonaCapturada.PalmForm.Form != null)
						{
							f++;
						}
						if (fEnrolar.PersonaCapturada.PalmForm.BackPage != null)
						{
							f++;
						}
						if (fEnrolar.PersonaCapturada.PalmForm.ListModels != null)
						{
							f++;
						}
						if (fEnrolar.PersonaCapturada.PalmForm.RolledPrintsBIR.Any<BIR>())
						{
							foreach (BIR vBir in fEnrolar.PersonaCapturada.PalmForm.RolledPrintsBIR)
							{
								if (vBir.BDB != null)
								{
									string subtype = vBir.BDBInfo.Subtype;
									if (subtype != null)
									{
										switch (subtype)
										{
											case "Right Thumb":
											{
												f++;
												break;
											}
											case "Right IndexFinger":
											{
												f++;
												break;
											}
											case "Right MiddleFinger":
											{
												f++;
												break;
											}
											case "Right RingFinger":
											{
												f++;
												break;
											}
											case "Right LittleFinger":
											{
												f++;
												break;
											}
											case "Left Thumb":
											{
												f++;
												break;
											}
											case "Left IndexFinger":
											{
												f++;
												break;
											}
											case "Left MiddleFinger":
											{
												f++;
												break;
											}
											case "Left RingFinger":
											{
												f++;
												break;
											}
											case "Left LittleFinger":
											{
												f++;
												break;
											}
										}
									}
								}
							}
						}
					}
				}
				float valform = (float)f / 20f;
				if (valform == 0f)
				{
					this.btnFormulario.BackColor = Color.White;
				}
				if ((valform <= 0f ? false : valform < 1f))
				{
					this.btnFormulario.BackColor = Color.SkyBlue;
				}
				if (valform == 1f)
				{
					this.btnFormulario.BackColor = Color.DodgerBlue;
				}
				valform *= 100f;
				Button button4 = this.btnFormulario;
				num1 = Convert.ToInt32(valform);
				button4.Text = string.Concat("TARJETA DE\nFILIACION\n", num1.ToString(), "%");
				this.btnFormulario.Refresh();
			}
			catch
			{
			}
		}

		private void ribtnCerrar_Click(object sender, EventArgs e)
		{
			if (this.Alerta("Mensaje", "Esta seguro de salir del detalle de enrolamiento ? ", true))
			{
				(new fPrincipal()
				{
					MdiParent = base.ParentForm
				}).Show();
				base.Close();
			}
		}

		private void ribtnGuardar_Click(object sender, EventArgs e)
		{
			System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
			(new Serializer()).SerializeEpd(fEnrolar.PersonaCapturada, fPrincipal.RutaEpd);
			this.porcentajes();
			System.Windows.Forms.Cursor.Current = Cursors.Default;
			this.Alerta("Mensaje", "Se guardaron los datos satisfactoriamente", false);
		}

		private void ribtnGuardarCerrar_Click(object sender, EventArgs e)
		{
			if (this.Alerta("Mensaje", "Se guardaran los datos y se saldra del detalle de enrolamiento,\nEsta seguro de realizar esta operacion ? ", true))
			{
				(new Serializer()).SerializeEpd(fEnrolar.PersonaCapturada, fPrincipal.RutaEpd);
				(new fPrincipal()
				{
					MdiParent = base.ParentForm
				}).Show();
				base.Close();
			}
		}

		private bool ValidaBiografia(ref string mensaje)
		{
			bool flag;
			bool respuesta = true;
			mensaje = "Debe completar: ";
			if ((fEnrolar.PersonaCapturada == null || fEnrolar.PersonaCapturada.OfflinePerson == null || fEnrolar.PersonaCapturada.OfflinePerson.Identities[0] == null ? true : fEnrolar.PersonaCapturada.RecordData == null))
			{
				respuesta = false;
				mensaje = " datos Biograficos,";
			}
			if ((fEnrolar.PersonaCapturada == null || fEnrolar.PersonaCapturada.OfflinePerson == null || fEnrolar.PersonaCapturada.OfflinePerson.Identities[0] == null || fEnrolar.PersonaCapturada.OfflinePerson.Identities[0].PersonType == null ? true : string.IsNullOrWhiteSpace(fEnrolar.PersonaCapturada.OfflinePerson.Identities[0].PersonType.Id)))
			{
				respuesta = false;
				mensaje = string.Concat(mensaje, " tipo de persona,");
			}
			if ((fEnrolar.PersonaCapturada == null || fEnrolar.PersonaCapturada.OfflinePerson == null || fEnrolar.PersonaCapturada.OfflinePerson.Identities[0] == null || fEnrolar.PersonaCapturada.OfflinePerson.Identities[0].Country == null ? true : string.IsNullOrWhiteSpace(fEnrolar.PersonaCapturada.OfflinePerson.Identities[0].Country.Id)))
			{
				respuesta = false;
				mensaje = string.Concat(mensaje, " lugar de nacimiento,");
			}
			if ((fEnrolar.PersonaCapturada == null || fEnrolar.PersonaCapturada.OfflinePerson == null || fEnrolar.PersonaCapturada.OfflinePerson.Identities[0] == null || fEnrolar.PersonaCapturada.OfflinePerson.Identities[0].GeneticCode == null ? true : string.IsNullOrWhiteSpace(fEnrolar.PersonaCapturada.OfflinePerson.Identities[0].GeneticCode)))
			{
				respuesta = false;
				mensaje = string.Concat(mensaje, " fecha de nacimiento,");
			}
			if ((fEnrolar.PersonaCapturada == null || fEnrolar.PersonaCapturada.OfflinePerson == null || fEnrolar.PersonaCapturada.OfflinePerson.Identities[0] == null ? true : string.IsNullOrWhiteSpace(fEnrolar.PersonaCapturada.OfflinePerson.Identities[0].Identification)))
			{
				respuesta = false;
				mensaje = string.Concat(mensaje, " identificacion(numero de documento),");
			}
			if ((fEnrolar.PersonaCapturada == null || fEnrolar.PersonaCapturada.OfflinePerson == null || fEnrolar.PersonaCapturada.OfflinePerson.Identities[0] == null ? true : string.IsNullOrWhiteSpace(fEnrolar.PersonaCapturada.OfflinePerson.Identities[0].FirstName)))
			{
				respuesta = false;
				mensaje = string.Concat(mensaje, " primer nombre,");
			}
			if (fEnrolar.PersonaCapturada == null || fEnrolar.PersonaCapturada.OfflinePerson == null || fEnrolar.PersonaCapturada.OfflinePerson.Identities[0] == null)
			{
				flag = true;
			}
			else
			{
				flag = (!string.IsNullOrWhiteSpace(fEnrolar.PersonaCapturada.OfflinePerson.Identities[0].FirstLastName) ? false : string.IsNullOrWhiteSpace(fEnrolar.PersonaCapturada.OfflinePerson.Identities[0].SecondLastName));
			}
			if (flag)
			{
				respuesta = false;
				mensaje = string.Concat(mensaje, " primer o segundo apellido,");
			}
			if ((fEnrolar.PersonaCapturada == null || fEnrolar.PersonaCapturada.OfflinePerson == null || fEnrolar.PersonaCapturada.OfflinePerson.Identities[0] == null || fEnrolar.PersonaCapturada.OfflinePerson.Identities[0].Sex == null ? true : string.IsNullOrWhiteSpace(fEnrolar.PersonaCapturada.OfflinePerson.Identities[0].Sex.Id)))
			{
				respuesta = false;
				mensaje = string.Concat(mensaje, " sexo,");
			}
			if ((fEnrolar.PersonaCapturada == null || fEnrolar.PersonaCapturada.OfflinePerson == null || fEnrolar.PersonaCapturada.OfflinePerson.Identities[0] == null || fEnrolar.PersonaCapturada.OfflinePerson.Identities[0].Skin == null ? true : string.IsNullOrWhiteSpace(fEnrolar.PersonaCapturada.OfflinePerson.Identities[0].Skin.Id)))
			{
				respuesta = false;
				mensaje = string.Concat(mensaje, " color de piel,");
			}
			if ((fEnrolar.PersonaCapturada.RecordData == null || fEnrolar.PersonaCapturada.RecordData.Motive == null ? true : string.IsNullOrWhiteSpace(fEnrolar.PersonaCapturada.RecordData.Motive.Id)))
			{
				respuesta = false;
				mensaje = string.Concat(mensaje, " motivo,");
			}
			if ((fEnrolar.PersonaCapturada.RecordData == null || fEnrolar.PersonaCapturada.RecordData.Crime == null ? true : string.IsNullOrWhiteSpace(fEnrolar.PersonaCapturada.RecordData.Crime.Id)))
			{
				respuesta = false;
				mensaje = string.Concat(mensaje, " causa,");
			}
			if ((fEnrolar.PersonaCapturada.RecordData == null ? true : string.IsNullOrWhiteSpace(fEnrolar.PersonaCapturada.RecordData.Complexion)))
			{
				respuesta = false;
				mensaje = string.Concat(mensaje, " complexión,");
			}
			if (fEnrolar.PersonaCapturada.RecordData == null)
			{
				respuesta = false;
				mensaje = string.Concat(mensaje, " ,");
			}
			if ((fEnrolar.PersonaCapturada.RecordData == null ? true : fEnrolar.PersonaCapturada.RecordData.Weigth <= 39))
			{
				respuesta = false;
				mensaje = string.Concat(mensaje, " peso mayor a 39 Kg,");
			}
			if ((fEnrolar.PersonaCapturada.RecordData == null ? true : fEnrolar.PersonaCapturada.RecordData.BodySize <= 49))
			{
				respuesta = false;
				mensaje = string.Concat(mensaje, " estatura mayor a 49 cm,");
			}
			mensaje = string.Concat(mensaje.Substring(0, mensaje.Length - 1), " en los datos biograficos");
			return respuesta;
		}

		private bool ValidaFace(ref string mensaje)
		{
			bool flag;
			if ((fEnrolar.PersonaCapturada == null || fEnrolar.PersonaCapturada.RecordData == null || fEnrolar.PersonaCapturada.RecordData.FaceFrontal == null ? false : fEnrolar.PersonaCapturada.RecordData.FaceFrontal.RuledImageArr != null))
			{
				flag = true;
			}
			else
			{
				mensaje = "Debe completar la captura facial.";
				flag = false;
			}
			return flag;
		}
	}
}