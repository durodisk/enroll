using BIODV.Control;
using BIODV.Util;
using Datys.Enroll.Core;
using Datys.SIP.Common;
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
using System.Windows.Forms;

namespace BIODV
{
	public class fDatosBiograficos : Form
	{
		private string _numeroDoc = string.Empty;

		private string _complemento = string.Empty;

		private IContainer components = null;

		private Button btncancelar;

		private Button btnsiguiente;

		private Button btnguardar;

		private ErrorProvider errorProvedor;

		private Button btnAyuda;

		private Panel panel1;

		private Button btnAnterior;

		private Label label7;

		private TextBox txtNombreMadre;

		private TextBox txtIdentificacion;

		private Label label8;

		private TextBox txtPrimerNombre;

		private TextBox txtSegundoApellido;

		private Label label9;

		private TextBox txtPrimerApellido;

		private Label label12;

		private Label label10;

		private TextBox txtSegundoNombre;

		private TextBox txtNombrePadre;

		private Label label13;

		private ComboBox cmbProvincia;

		private ComboBox cmbDepartamento;

		private ComboBox cmbPais;

		private Label lblProvincia;

		private Label lblDepartamento;

		private Label lblPais;

		private ComboBox cmbComplexion;

		private ComboBox cmbColorPiel;

		private ComboBox cmbSexo;

		private NumericUpDown numPie;

		private NumericUpDown numEstatura;

		private NumericUpDown numPeso;

		private Label label5;

		private Label label14;

		private Label label15;

		private Label label16;

		private Label label17;

		private Label label18;

		private ComboBox cmbTipoPersona;

		private ComboBox cmbCausa;

		private ComboBox cmbMotivo;

		private Label label3;

		private Label label2;

		private Label label1;

		private Direccion direccion1;

		private Button button1;

		private TableLayoutPanel tableLayoutPanel1;

		private Label label6;

		private TableLayoutPanel tableLayoutPanel8;

		private TableLayoutPanel tableLayoutPanel5;

		private TableLayoutPanel tableLayoutPanel4;

		private Label label35;

		private TableLayoutPanel tableLayoutPanel2;

		private Label label42;

		private TableLayoutPanel tableLayoutPanel10;

		private TableLayoutPanel tableLayoutPanel11;

		private Label label4;

		private TableLayoutPanel tableLayoutPanel12;

		private Label label11;

		private MaskedTextBox txtCodigoGenetico;

		private Label lblComplemento;

		private TextBox txtComplemento;

		private PictureBox pictureBox1;

		private TableLayoutPanel tableLayoutPanel9;

		private Label label43;

		private TableLayoutPanel tableLayoutPanel7;

		private Label label41;

		private TableLayoutPanel tableLayoutPanel6;

		private Label label20;

		private TableLayoutPanel tableLayoutPanel3;

		private TabControl tabDatosBiograficos;

		private TabPage tabPage3;

		private GroupBox groupControl2;

		private TabControl tabControl1;

		private TabPage tabPage1;

		private TabPage tabPage2;

		private GroupBox groupControl7;

		private Nacionalidades nacionalidades1;

		private Alias alias1;

		private ComboBox cmbNivelCultural;

		private Label label19;

		private Label label23;

		private GroupBox groupControl1;

		private TabPage tabPage4;

		private Label lbltitulo2;

		private GroupBox groupControl14;

		private ComboBox cmbCabezaForma;

		private Label label36;

		private GroupBox groupControl8;

		private ComboBox cmbPeloColor;

		private ComboBox cmbPeloTipo;

		private Label label22;

		private Label label21;

		private GroupBox groupControl11;

		private ComboBox cmbFacialesSurcoNasal;

		private ComboBox cmbFacialesMejillas;

		private ComboBox cmbFacialesBarbilla;

		private Label label34;

		private Label label28;

		private Label label29;

		private GroupBox groupControl9;

		private ComboBox cmbNarizPunta;

		private ComboBox cmbNarizAncho;

		private Label label24;

		private Label label25;

		private GroupBox groupControl15;

		private ComboBox cmbCuelloAlto;

		private ComboBox cmbCuelloAncho;

		private Label label37;

		private Label label38;

		private GroupBox groupControl13;

		private ComboBox cmbCejasPoblacion;

		private ComboBox cmbCejasForma;

		private Label label32;

		private Label label33;

		private GroupBox groupControl16;

		private ComboBox cmbOjosForma;

		private ComboBox cmbOjosColor;

		private Label label39;

		private Label label40;

		private GroupBox groupControl12;

		private ComboBox cmbBocaTipo;

		private ComboBox cmbBocaDimension;

		private Label label30;

		private Label label31;

		private GroupBox groupControl10;

		private ComboBox cmbRostro;

		private ComboBox cmbRostroFrente;

		private Label label26;

		private Label label27;

		private TabPage tabPage5;

		private Label lbltitulo3;

		private GroupBox groupControl20;

		private CheckedListBox chkPiesPiernas;

		private GroupBox groupControl19;

		private CheckedListBox chkPiel;

		private GroupBox groupControl24;

		private CheckedListBox chkOrejas;

		private GroupBox groupControl18;

		private CheckedListBox chkPecho;

		private GroupBox groupControl17;

		private CheckedListBox chkEspalda;

		private GroupBox groupControl22;

		private CheckedListBox chkDedosManos;

		private GroupBox groupControl25;

		private CheckedListBox chkDientes;

		private GroupBox groupControl21;

		private CheckedListBox chkBrazos;

		private TabPage tabPage6;

		private GroupBox groupControl23;

		private CheckedListBox chkIdiomas;

		private GroupBox groupControl30;

		private CheckedListBox chkAlAndar;

		private Label lblTitulo4;

		private GroupBox groupControl26;

		private CheckedListBox chkDeLaConducta;

		private GroupBox groupControl29;

		private CheckedListBox chkAlHablar;

		private GroupBox groupControl27;

		private CheckedListBox chkHabilidadesEsp;

		private GroupBox groupControl28;

		private CheckedListBox chkPartesAusentes;

		private ToolTip toolTip1;

		private TableLayoutPanel tableLayoutPanel14;

		public fDatosBiograficos()
		{
			try
			{
				this.InitializeComponent();
				DataSet xmlDataSet = new DataSet("XML DataSet");
				List<Coder> vCoder = CargarControl.ObtenerLista("MOTIVEENROLL");
				CargarControl.Combo(this.cmbMotivo, vCoder, true);
				vCoder = CargarControl.ObtenerLista("PERSONTYPE");
				CargarControl.Combo(this.cmbTipoPersona, vCoder, true);
				vCoder = CargarControl.ObtenerLista("COUNTRY", true);
				CargarControl.Combo(this.cmbPais, vCoder, true);
				vCoder = CargarControl.ObtenerLista("SEX");
				CargarControl.Combo(this.cmbSexo, vCoder, true);
				vCoder = CargarControl.ObtenerLista("SKIN");
				CargarControl.Combo(this.cmbColorPiel, vCoder, true);
				vCoder = CargarControl.ObtenerLista("COMPLEXION");
				CargarControl.Combo(this.cmbComplexion, vCoder, true);
				vCoder = CargarControl.ObtenerLista("CULTURALLEVEL");
				CargarControl.Combo(this.cmbNivelCultural, vCoder, true);
				vCoder = CargarControl.ObtenerLista("HAIRTYPE");
				CargarControl.Combo(this.cmbPeloTipo, vCoder, true);
				vCoder = CargarControl.ObtenerLista("HAIRCOLOR");
				CargarControl.Combo(this.cmbPeloColor, vCoder, true);
				vCoder = CargarControl.ObtenerLista("NOSEWIDTH");
				CargarControl.Combo(this.cmbNarizAncho, vCoder, true);
				vCoder = CargarControl.ObtenerLista("NOSETIP");
				CargarControl.Combo(this.cmbNarizPunta, vCoder, true);
				vCoder = CargarControl.ObtenerLista("FOREHEAD");
				CargarControl.Combo(this.cmbRostroFrente, vCoder, true);
				vCoder = CargarControl.ObtenerLista("FACEPROPORTION");
				CargarControl.Combo(this.cmbRostro, vCoder, true);
				vCoder = CargarControl.ObtenerLista("EYEBROWSHAPE");
				CargarControl.Combo(this.cmbCejasForma, vCoder, true);
				vCoder = CargarControl.ObtenerLista("EYEBROWPOP");
				CargarControl.Combo(this.cmbCejasPoblacion, vCoder, true);
				vCoder = CargarControl.ObtenerLista("MOUTH");
				CargarControl.Combo(this.cmbBocaDimension, vCoder, true);
				vCoder = CargarControl.ObtenerLista("MOUTHTYPE");
				CargarControl.Combo(this.cmbBocaTipo, vCoder, true);
				vCoder = CargarControl.ObtenerLista("CHIN");
				CargarControl.Combo(this.cmbFacialesBarbilla, vCoder, true);
				vCoder = CargarControl.ObtenerLista("CHEEKS");
				CargarControl.Combo(this.cmbFacialesMejillas, vCoder, true);
				vCoder = CargarControl.ObtenerLista("NASALGROOVE");
				CargarControl.Combo(this.cmbFacialesSurcoNasal, vCoder, true);
				vCoder = CargarControl.ObtenerLista("EYESCOLOR");
				CargarControl.Combo(this.cmbOjosColor, vCoder, true);
				vCoder = CargarControl.ObtenerLista("EYESSHAPE");
				CargarControl.Combo(this.cmbOjosForma, vCoder, true);
				vCoder = CargarControl.ObtenerLista("NECKWIDTH");
				CargarControl.Combo(this.cmbCuelloAncho, vCoder, true);
				vCoder = CargarControl.ObtenerLista("NECKLONG");
				CargarControl.Combo(this.cmbCuelloAlto, vCoder, true);
				vCoder = CargarControl.ObtenerLista("HEADSHAPE");
				CargarControl.Combo(this.cmbCabezaForma, vCoder, true);
				vCoder = CargarControl.ObtenerLista("SKINCHARACT");
				CargarControl.Checklist(this.chkPiel, vCoder);
				vCoder = CargarControl.ObtenerLista("CHEST");
				CargarControl.Checklist(this.chkPecho, vCoder);
				vCoder = CargarControl.ObtenerLista("BACK");
				CargarControl.Checklist(this.chkEspalda, vCoder);
				vCoder = CargarControl.ObtenerLista("HAND");
				CargarControl.Checklist(this.chkDedosManos, vCoder);
				vCoder = CargarControl.ObtenerLista("ARMS");
				CargarControl.Checklist(this.chkBrazos, vCoder);
				vCoder = CargarControl.ObtenerLista("FEET");
				CargarControl.Checklist(this.chkPiesPiernas, vCoder);
				vCoder = CargarControl.ObtenerLista("TEETH");
				CargarControl.Checklist(this.chkDientes, vCoder);
				vCoder = CargarControl.ObtenerLista("EAR");
				CargarControl.Checklist(this.chkOrejas, vCoder);
				vCoder = CargarControl.ObtenerLista("WALK");
				CargarControl.Checklist(this.chkAlAndar, vCoder);
				vCoder = CargarControl.ObtenerLista("TALK");
				CargarControl.Checklist(this.chkAlHablar, vCoder);
				vCoder = CargarControl.ObtenerLista("BEHAVIOR");
				CargarControl.Checklist(this.chkDeLaConducta, vCoder);
				vCoder = CargarControl.ObtenerLista("ABSENT");
				CargarControl.Checklist(this.chkPartesAusentes, vCoder);
				vCoder = CargarControl.ObtenerLista("SPECIAL");
				CargarControl.Checklist(this.chkHabilidadesEsp, vCoder);
				vCoder = CargarControl.ObtenerLista("LANGUAGE");
				CargarControl.Checklist(this.chkIdiomas, vCoder);
				this.CargarValores(fEnrolar.PersonaCapturada);
				this.ValidaModeloDatosIdentificativos();
			}
			catch (Exception exception)
			{
			}
		}

		private void ActualizarCapturedPerson()
		{
			Coder coder;
			string str;
			string str1;
			string str2;
			string str3;
			string str4;
			string str5;
			string str6;
			string str7;
			string str8;
			string str9;
			string str10;
			string str11;
			string str12;
			string str13;
			string str14;
			string str15;
			string str16;
			string str17;
			string str18;
			string str19;
			try
			{
				CapturedPerson vPersonaCapturada = fEnrolar.PersonaCapturada ?? new CapturedPerson();
				if (vPersonaCapturada.BasicData == null)
				{
					vPersonaCapturada.BasicData = new LightPersonBasicData();
				}
				if (vPersonaCapturada.RecordData == null)
				{
					vPersonaCapturada.RecordData = new RecordData();
				}
				if (vPersonaCapturada.OfflinePerson == null)
				{
					vPersonaCapturada.OfflinePerson = new OfflinePerson();
				}
				string vNombreCompleto = this.txtPrimerNombre.Text.Trim();
				string vSegundoNombre = (string.IsNullOrWhiteSpace(this.txtSegundoNombre.Text) ? "" : string.Concat(" ", this.txtSegundoNombre.Text.Trim()));
				string vPrimerApellido = (string.IsNullOrWhiteSpace(this.txtPrimerApellido.Text) ? "" : string.Concat(" ", this.txtPrimerApellido.Text.Trim()));
				vNombreCompleto = string.Concat(vNombreCompleto, vSegundoNombre, vPrimerApellido, (string.IsNullOrWhiteSpace(this.txtSegundoApellido.Text) ? "" : string.Concat(" ", this.txtSegundoApellido.Text.Trim())));
				if (!string.IsNullOrWhiteSpace(vPersonaCapturada.BasicData.FullName))
				{
					vPersonaCapturada.BasicData.FullName = vNombreCompleto;
					vPersonaCapturada.BasicData.Modified = DateTime.Now;
					vPersonaCapturada.BasicData.ModifiedBy = fLogin.usuario;
				}
				else
				{
					vPersonaCapturada.BasicData.FullName = vNombreCompleto;
					vPersonaCapturada.BasicData.Created = DateTime.Now;
					vPersonaCapturada.BasicData.CreatedBy = fLogin.usuario;
					vPersonaCapturada.BasicData.Modified = DateTime.Now;
					vPersonaCapturada.BasicData.ModifiedBy = fLogin.usuario;
					vPersonaCapturada.BasicData.Label = fLogin.unidad;
				}
				Coder vCoderTipoPersona = this.ObtenerCoder(this.cmbTipoPersona, "PERSONTYPE");
				Coder vCoderPais = this.ObtenerCoder(this.cmbPais, "COUNTRY");
				Coder vCoderDpto = this.ObtenerCoder(this.cmbDepartamento, "PROV");
				Coder vCoderMun = this.ObtenerCoder(this.cmbProvincia, "MUN");
				Coder vCoderSexo = this.ObtenerCoder(this.cmbSexo, "SEX");
				Coder vCoderColorPiel = this.ObtenerCoder(this.cmbColorPiel, "SKIN");
				string vComplemento = (string.IsNullOrWhiteSpace(this.txtComplemento.Text.Trim()) ? "" : string.Concat("|", this.txtComplemento.Text.Trim()));
				vPersonaCapturada.OfflinePerson.Identities = new BindingList<Identity>()
				{
					new Identity()
					{
						Identification = string.Concat(this.txtIdentificacion.Text.Trim(), vComplemento),
						FirstName = this.txtPrimerNombre.Text.Trim(),
						SecondName = this.txtSegundoNombre.Text.Trim(),
						FirstLastName = this.txtPrimerApellido.Text.Trim(),
						SecondLastName = this.txtSegundoApellido.Text.Trim(),
						FatherName = this.txtNombrePadre.Text.Trim(),
						MotherName = this.txtNombreMadre.Text.Trim(),
						GeneticCode = this.txtCodigoGenetico.Text.Trim(),
						PersonType = vCoderTipoPersona,
						Country = vCoderPais,
						State = vCoderDpto,
						District = vCoderMun,
						Sex = vCoderSexo,
						Skin = vCoderColorPiel,
						Nationalities = this.nacionalidades1.Paises
					}
				};
				Coder vCoderMotivo = this.ObtenerCoder(this.cmbMotivo, "MOTIVEENROLL");
				vPersonaCapturada.RecordData.Motive = vCoderMotivo;
				if (this.cmbMotivo.SelectedValue != null)
				{
					coder = this.ObtenerCoder(this.cmbCausa, this.cmbMotivo.SelectedValue.ToString());
				}
				else
				{
					coder = null;
				}
				vPersonaCapturada.RecordData.Crime = coder;
				RecordData recordData = vPersonaCapturada.RecordData;
				if (this.cmbComplexion.SelectedValue != null)
				{
					str = this.cmbComplexion.SelectedValue.ToString();
				}
				else
				{
					str = null;
				}
				recordData.Complexion = str;
				vPersonaCapturada.RecordData.Weigth = Convert.ToInt32(this.numPeso.Value);
				vPersonaCapturada.RecordData.BodySize = Convert.ToInt32(this.numEstatura.Value);
				vPersonaCapturada.RecordData.Foot = Convert.ToInt32(this.numPie.Value);
				RecordData recordDatum = vPersonaCapturada.RecordData;
				if (this.cmbNivelCultural.SelectedValue != null)
				{
					str1 = this.cmbNivelCultural.SelectedValue.ToString();
				}
				else
				{
					str1 = null;
				}
				recordDatum.CulturalLevel = str1;
				RecordData recordData1 = vPersonaCapturada.RecordData;
				DescriptiveData descriptiveDatum = new DescriptiveData();
				Hair hair = new Hair();
				if (this.cmbPeloTipo.SelectedValue != null)
				{
					str2 = this.cmbPeloTipo.SelectedValue.ToString();
				}
				else
				{
					str2 = null;
				}
				hair.Type = str2;
				if (this.cmbPeloColor.SelectedValue != null)
				{
					str3 = this.cmbPeloColor.SelectedValue.ToString();
				}
				else
				{
					str3 = null;
				}
				hair.Color = str3;
				descriptiveDatum.Hair = hair;
				Nose nose = new Nose();
				if (this.cmbNarizAncho.SelectedValue != null)
				{
					str4 = this.cmbNarizAncho.SelectedValue.ToString();
				}
				else
				{
					str4 = null;
				}
				nose.Width = str4;
				if (this.cmbNarizPunta.SelectedValue != null)
				{
					str5 = this.cmbNarizPunta.SelectedValue.ToString();
				}
				else
				{
					str5 = null;
				}
				nose.Tip = str5;
				descriptiveDatum.Nose = nose;
				Face face = new Face();
				if (this.cmbRostroFrente.SelectedValue != null)
				{
					str6 = this.cmbRostroFrente.SelectedValue.ToString();
				}
				else
				{
					str6 = null;
				}
				face.BrowDimension = str6;
				if (this.cmbRostro.SelectedValue != null)
				{
					str7 = this.cmbRostro.SelectedValue.ToString();
				}
				else
				{
					str7 = null;
				}
				face.Proportion = str7;
				descriptiveDatum.Face = face;
				Eyebrow eyebrow = new Eyebrow();
				if (this.cmbCejasForma.SelectedValue != null)
				{
					str8 = this.cmbCejasForma.SelectedValue.ToString();
				}
				else
				{
					str8 = null;
				}
				eyebrow.Shape = str8;
				if (this.cmbCejasPoblacion.SelectedValue != null)
				{
					str9 = this.cmbCejasPoblacion.SelectedValue.ToString();
				}
				else
				{
					str9 = null;
				}
				eyebrow.Population = str9;
				descriptiveDatum.Eyebrow = eyebrow;
				Mouth mouth = new Mouth();
				if (this.cmbBocaDimension.SelectedValue != null)
				{
					str10 = this.cmbBocaDimension.SelectedValue.ToString();
				}
				else
				{
					str10 = null;
				}
				mouth.Dimension = str10;
				if (this.cmbBocaTipo.SelectedValue != null)
				{
					str11 = this.cmbBocaTipo.SelectedValue.ToString();
				}
				else
				{
					str11 = null;
				}
				mouth.Type = str11;
				descriptiveDatum.Mouth = mouth;
				Facial facial = new Facial();
				if (this.cmbFacialesBarbilla.SelectedValue != null)
				{
					str12 = this.cmbFacialesBarbilla.SelectedValue.ToString();
				}
				else
				{
					str12 = null;
				}
				facial.Cheeks = str12;
				if (this.cmbFacialesMejillas.SelectedValue != null)
				{
					str13 = this.cmbFacialesMejillas.SelectedValue.ToString();
				}
				else
				{
					str13 = null;
				}
				facial.Chin = str13;
				if (this.cmbFacialesSurcoNasal.SelectedValue != null)
				{
					str14 = this.cmbFacialesSurcoNasal.SelectedValue.ToString();
				}
				else
				{
					str14 = null;
				}
				facial.NasalGroove = str14;
				descriptiveDatum.Facial = facial;
				Eyes eye = new Eyes();
				if (this.cmbOjosForma.SelectedValue != null)
				{
					str15 = this.cmbOjosForma.SelectedValue.ToString();
				}
				else
				{
					str15 = null;
				}
				eye.Shape = str15;
				if (this.cmbOjosColor.SelectedValue != null)
				{
					str16 = this.cmbOjosColor.SelectedValue.ToString();
				}
				else
				{
					str16 = null;
				}
				eye.Color = str16;
				descriptiveDatum.Eyes = eye;
				Neck neck = new Neck();
				if (this.cmbCuelloAncho.SelectedValue != null)
				{
					str17 = this.cmbCuelloAncho.SelectedValue.ToString();
				}
				else
				{
					str17 = null;
				}
				neck.Width = str17;
				if (this.cmbCuelloAlto.SelectedValue != null)
				{
					str18 = this.cmbCuelloAlto.SelectedValue.ToString();
				}
				else
				{
					str18 = null;
				}
				neck.Long = str18;
				descriptiveDatum.Neck = neck;
				if (this.cmbCabezaForma.SelectedValue != null)
				{
					str19 = this.cmbCabezaForma.SelectedValue.ToString();
				}
				else
				{
					str19 = null;
				}
				descriptiveDatum.HeadShape = str19;
				recordData1.DescriptiveData = descriptiveDatum;
				vPersonaCapturada.RecordData.CharacteristicData = new CharacteristicData()
				{
					Skin = this.ObtenerListaCoder(this.chkPiel, "SKINCHARACT"),
					Chest = this.ObtenerListaCoder(this.chkPecho, "CHEST"),
					Back = this.ObtenerListaCoder(this.chkEspalda, "BACK"),
					FingerHand = this.ObtenerListaCoder(this.chkDedosManos, "HAND"),
					Arm = this.ObtenerListaCoder(this.chkBrazos, "ARMS"),
					FootLeg = this.ObtenerListaCoder(this.chkPiesPiernas, "FEET"),
					Tooth = this.ObtenerListaCoder(this.chkDientes, "TEETH"),
					Ear = this.ObtenerListaCoder(this.chkOrejas, "EAR")
				};
				vPersonaCapturada.RecordData.FeatureData = new FeatureData()
				{
					Walk = this.ObtenerListaCoder(this.chkAlAndar, "WALK"),
					Talk = this.ObtenerListaCoder(this.chkAlHablar, "TALK"),
					Behavior = this.ObtenerListaCoder(this.chkDeLaConducta, "BEHAVIOR"),
					Absent = this.ObtenerListaCoder(this.chkPartesAusentes, "ABSENT"),
					Special = this.ObtenerListaCoder(this.chkHabilidadesEsp, "SPECIAL"),
					Language = this.ObtenerListaCoder(this.chkIdiomas, "LANGUAGE")
				};
				vPersonaCapturada.RecordData.Addresses = this.direccion1.Direcciones;
				vPersonaCapturada.RecordData.Alias = this.alias1.Nombres;
				fEnrolar.PersonaCapturada = vPersonaCapturada;
             //   vPersonaCapturada.RecordData.
                (new Serializer()).SerializeEpd(vPersonaCapturada, fPrincipal.RutaEpd);
				this.Alerta("Mensaje", "Se guardaron correctamente los registros", false);
			}
			catch
			{
				this.Alerta("Mensaje", "Problemas al guardar, comuniquese con el administrador del sistema", false);
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
				if (this.tabDatosBiograficos.SelectedIndex == 1)
				{
					this.btnAnterior.Enabled = false;
				}
				this.tabDatosBiograficos.SelectTab(this.tabDatosBiograficos.SelectedIndex - 1);
				this.btnsiguiente.Enabled = true;
				this.btnsiguiente.Text = "Siguiente";
			}
			catch (Exception exception)
			{
			}
		}

		private void btncancelar_Click(object sender, EventArgs e)
		{
			if (this.Alerta("Mensaje", "¿ Esta seguro de Salir ? , puede que exista datos sin ser guardados", true))
			{
				(new fEnrolar()
				{
					MdiParent = base.ParentForm
				}).Show();
				base.Close();
			}
		}

		private void btnguardar_Click(object sender, EventArgs e)
		{
			try
			{
				System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
				this.ActualizarCapturedPerson();
				System.Windows.Forms.Cursor.Current = Cursors.Default;
				this.Alerta("Mensaje", "Se guardaron los datos satisfactoriamente", false);
			}
			catch
			{
			}
		}

		private void btnsiguiente_Click(object sender, EventArgs e)
		{
			try
			{
				switch (this.tabDatosBiograficos.SelectedIndex)
				{
					case 0:
					{
						if (this.ValidaModeloDatosIdentificativos())
						{
							this.btnAnterior.Enabled = true;
							this.btnsiguiente.Enabled = this.ValidaModeloDatosDescriptivos();
						}
						break;
					}
					case 1:
					{
						if (this.ValidaModeloDatosDescriptivos())
						{
							this.tabDatosBiograficos.SelectTab(2);
							this.btnAnterior.Enabled = true;
						}
						break;
					}
					case 2:
					{
						this.tabDatosBiograficos.SelectTab(3);
						this.btnAnterior.Enabled = true;
						this.btnsiguiente.Text = "Finalizar";
						break;
					}
					case 3:
					{
						this.ActualizarCapturedPerson();
						base.Close();
						break;
					}
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (this.Alerta("Mensaje", "Se guardaran los datos y se saldra del formulario,\nEsta seguro de realizar esta operacion ? ", true))
			{
				System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
				this.ActualizarCapturedPerson();
				System.Windows.Forms.Cursor.Current = Cursors.Default;
				(new fEnrolar()
				{
					MdiParent = base.ParentForm
				}).Show();
				base.Close();
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void CargarValores(CapturedPerson personaCapturada)
		{
			try
			{
				if (personaCapturada != null)
				{
					if ((personaCapturada.OfflinePerson == null ? false : personaCapturada.OfflinePerson.Identities.Any<Identity>()))
					{
						if ((personaCapturada.OfflinePerson.Identities[0].PersonType == null ? false : personaCapturada.OfflinePerson.Identities[0].PersonType.Id != null))
						{
							this.cmbTipoPersona.SelectedValue = personaCapturada.OfflinePerson.Identities[0].PersonType.Id;
						}
						if (!string.IsNullOrWhiteSpace(personaCapturada.OfflinePerson.Identities[0].Identification))
						{
							string[] vColId = personaCapturada.OfflinePerson.Identities[0].Identification.Split(new char[] { '|' });
							this.txtIdentificacion.Text = vColId[0];
							if (vColId.Count<string>() != 2)
							{
								this.txtComplemento.Text = "";
							}
							else
							{
								this.txtComplemento.Text = vColId[1];
							}
						}
						this.txtPrimerNombre.Text = personaCapturada.OfflinePerson.Identities[0].FirstName;
						this.txtSegundoNombre.Text = personaCapturada.OfflinePerson.Identities[0].SecondName;
						this.txtPrimerApellido.Text = personaCapturada.OfflinePerson.Identities[0].FirstLastName;
						this.txtSegundoApellido.Text = personaCapturada.OfflinePerson.Identities[0].SecondLastName;
						this.txtNombrePadre.Text = personaCapturada.OfflinePerson.Identities[0].FatherName;
						this.txtNombreMadre.Text = personaCapturada.OfflinePerson.Identities[0].MotherName;
						this.txtCodigoGenetico.Text = personaCapturada.OfflinePerson.Identities[0].GeneticCode;
					//	this.nacionalidades1.Paises = personaCapturada.OfflinePerson.Identities[0].Nationalities;
						if ((personaCapturada.OfflinePerson.Identities[0].Country == null ? false : personaCapturada.OfflinePerson.Identities[0].Country.Id != null))
						{
							this.cmbPais.SelectedValue = personaCapturada.OfflinePerson.Identities[0].Country.Id;
						}
						if ((personaCapturada.OfflinePerson.Identities[0].State == null ? false : personaCapturada.OfflinePerson.Identities[0].State.Id != null))
						{
							this.cmbDepartamento.SelectedValue = personaCapturada.OfflinePerson.Identities[0].State.Id;
						}
						if ((personaCapturada.OfflinePerson.Identities[0].District == null ? false : personaCapturada.OfflinePerson.Identities[0].District.Id != null))
						{
							this.cmbProvincia.SelectedValue = personaCapturada.OfflinePerson.Identities[0].District.Id;
						}
						if ((personaCapturada.OfflinePerson.Identities[0].Sex == null ? false : personaCapturada.OfflinePerson.Identities[0].Sex.Id != null))
						{
							this.cmbSexo.SelectedValue = personaCapturada.OfflinePerson.Identities[0].Sex.Id;
						}
						if ((personaCapturada.OfflinePerson.Identities[0].Skin == null ? false : personaCapturada.OfflinePerson.Identities[0].Skin.Id != null))
						{
							this.cmbColorPiel.SelectedValue = personaCapturada.OfflinePerson.Identities[0].Skin.Id;
						}
					}
					if (personaCapturada.RecordData != null)
					{
						if ((personaCapturada.RecordData.Crime == null ? false : personaCapturada.RecordData.Crime.CoderTypeId != null))
						{
							this.cmbMotivo.SelectedValue = personaCapturada.RecordData.Crime.CoderTypeId;
							this.cmbCausa.SelectedValue = personaCapturada.RecordData.Crime.Id;
						}
						if (personaCapturada.RecordData.Complexion != null)
						{
							this.cmbComplexion.SelectedValue = personaCapturada.RecordData.Complexion;
						}
						this.numPeso.Value = Convert.ToDecimal(personaCapturada.RecordData.Weigth);
						this.numEstatura.Value = Convert.ToDecimal(personaCapturada.RecordData.BodySize);
						this.numPie.Value = Convert.ToDecimal(personaCapturada.RecordData.Foot);
						if (personaCapturada.RecordData.CulturalLevel != null)
						{
							this.cmbNivelCultural.SelectedValue = personaCapturada.RecordData.CulturalLevel;
						}
					//	this.direccion1.Direcciones = personaCapturada.RecordData.Addresses;
						this.alias1.Nombres = personaCapturada.RecordData.Alias;
						if (personaCapturada.RecordData.DescriptiveData != null)
						{
							if (personaCapturada.RecordData.DescriptiveData.Hair != null)
							{
								if (personaCapturada.RecordData.DescriptiveData.Hair.Type != null)
								{
									this.cmbPeloTipo.SelectedValue = personaCapturada.RecordData.DescriptiveData.Hair.Type;
								}
								if (personaCapturada.RecordData.DescriptiveData.Hair.Color != null)
								{
									this.cmbPeloColor.SelectedValue = personaCapturada.RecordData.DescriptiveData.Hair.Color;
								}
							}
							if (personaCapturada.RecordData.DescriptiveData.Nose != null)
							{
								if (personaCapturada.RecordData.DescriptiveData.Nose.Width != null)
								{
									this.cmbNarizAncho.SelectedValue = personaCapturada.RecordData.DescriptiveData.Nose.Width;
								}
								if (personaCapturada.RecordData.DescriptiveData.Nose.Tip != null)
								{
									this.cmbNarizPunta.SelectedValue = personaCapturada.RecordData.DescriptiveData.Nose.Tip;
								}
							}
							if (personaCapturada.RecordData.DescriptiveData.Face != null)
							{
								if (personaCapturada.RecordData.DescriptiveData.Face.BrowDimension != null)
								{
									this.cmbRostroFrente.SelectedValue = personaCapturada.RecordData.DescriptiveData.Face.BrowDimension;
								}
								if (personaCapturada.RecordData.DescriptiveData.Face.Proportion != null)
								{
									this.cmbRostro.SelectedValue = personaCapturada.RecordData.DescriptiveData.Face.Proportion;
								}
							}
							if (personaCapturada.RecordData.DescriptiveData.Eyebrow != null)
							{
								if (personaCapturada.RecordData.DescriptiveData.Eyebrow.Shape != null)
								{
									this.cmbCejasForma.SelectedValue = personaCapturada.RecordData.DescriptiveData.Eyebrow.Shape;
								}
								if (personaCapturada.RecordData.DescriptiveData.Eyebrow.Population != null)
								{
									this.cmbCejasPoblacion.SelectedValue = personaCapturada.RecordData.DescriptiveData.Eyebrow.Population;
								}
							}
							if (personaCapturada.RecordData.DescriptiveData.Mouth != null)
							{
								if (personaCapturada.RecordData.DescriptiveData.Mouth.Type != null)
								{
									this.cmbBocaTipo.SelectedValue = personaCapturada.RecordData.DescriptiveData.Mouth.Type;
								}
								if (personaCapturada.RecordData.DescriptiveData.Mouth.Dimension != null)
								{
									this.cmbBocaDimension.SelectedValue = personaCapturada.RecordData.DescriptiveData.Mouth.Dimension;
								}
							}
							if (personaCapturada.RecordData.DescriptiveData.Facial != null)
							{
								if (personaCapturada.RecordData.DescriptiveData.Facial.Cheeks != null)
								{
									this.cmbFacialesBarbilla.SelectedValue = personaCapturada.RecordData.DescriptiveData.Facial.Cheeks;
								}
								if (personaCapturada.RecordData.DescriptiveData.Facial.Chin != null)
								{
									this.cmbFacialesMejillas.SelectedValue = personaCapturada.RecordData.DescriptiveData.Facial.Chin;
								}
								if (personaCapturada.RecordData.DescriptiveData.Facial.NasalGroove != null)
								{
									this.cmbFacialesSurcoNasal.SelectedValue = personaCapturada.RecordData.DescriptiveData.Facial.NasalGroove;
								}
							}
							if (personaCapturada.RecordData.DescriptiveData.Eyes != null)
							{
								if (personaCapturada.RecordData.DescriptiveData.Eyes.Shape != null)
								{
									this.cmbOjosForma.SelectedValue = personaCapturada.RecordData.DescriptiveData.Eyes.Shape;
								}
								if (personaCapturada.RecordData.DescriptiveData.Eyes.Color != null)
								{
									this.cmbOjosColor.SelectedValue = personaCapturada.RecordData.DescriptiveData.Eyes.Color;
								}
							}
							if (personaCapturada.RecordData.DescriptiveData.Neck != null)
							{
								if (personaCapturada.RecordData.DescriptiveData.Neck.Long != null)
								{
									this.cmbCuelloAlto.SelectedValue = personaCapturada.RecordData.DescriptiveData.Neck.Long;
								}
								if (personaCapturada.RecordData.DescriptiveData.Neck.Width != null)
								{
									this.cmbCuelloAncho.SelectedValue = personaCapturada.RecordData.DescriptiveData.Neck.Width;
								}
							}
							if (personaCapturada.RecordData.DescriptiveData.HeadShape != null)
							{
								this.cmbCabezaForma.SelectedValue = personaCapturada.RecordData.DescriptiveData.HeadShape;
							}
						}
						if (personaCapturada.RecordData.CharacteristicData != null)
						{
							CargarControl.Checklist(personaCapturada.RecordData.CharacteristicData.Skin, this.chkPiel);
							CargarControl.Checklist(personaCapturada.RecordData.CharacteristicData.Chest, this.chkPecho);
							CargarControl.Checklist(personaCapturada.RecordData.CharacteristicData.Back, this.chkEspalda);
							CargarControl.Checklist(personaCapturada.RecordData.CharacteristicData.FingerHand, this.chkDedosManos);
							CargarControl.Checklist(personaCapturada.RecordData.CharacteristicData.Arm, this.chkBrazos);
							CargarControl.Checklist(personaCapturada.RecordData.CharacteristicData.FootLeg, this.chkPiesPiernas);
							CargarControl.Checklist(personaCapturada.RecordData.CharacteristicData.Tooth, this.chkDientes);
							CargarControl.Checklist(personaCapturada.RecordData.CharacteristicData.Ear, this.chkOrejas);
						}
						if (personaCapturada.RecordData.FeatureData != null)
						{
							CargarControl.Checklist(personaCapturada.RecordData.FeatureData.Walk, this.chkAlAndar);
							CargarControl.Checklist(personaCapturada.RecordData.FeatureData.Talk, this.chkAlHablar);
							CargarControl.Checklist(personaCapturada.RecordData.FeatureData.Behavior, this.chkDeLaConducta);
							CargarControl.Checklist(personaCapturada.RecordData.FeatureData.Absent, this.chkPartesAusentes);
							CargarControl.Checklist(personaCapturada.RecordData.FeatureData.Special, this.chkHabilidadesEsp);
							CargarControl.Checklist(personaCapturada.RecordData.FeatureData.Language, this.chkIdiomas);
						}
					}
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void cmbBocaDimension_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosDescriptivos();
		}

		private void cmbBocaTipo_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosDescriptivos();
		}

		private void cmbCabezaForma_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosDescriptivos();
		}

		private void cmbCausa_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosIdentificativos();
		}

		private void cmbCejasForma_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosDescriptivos();
		}

		private void cmbCejasPoblacion_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosDescriptivos();
		}

		private void cmbComplexion_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosIdentificativos();
		}

		private void cmbCuelloAlto_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosDescriptivos();
		}

		private void cmbCuelloAncho_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosDescriptivos();
		}

		private void cmbDepartamento_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				string vDepartamento = (string)((ComboBox)sender).SelectedValue;
				List<Coder> items = new List<Coder>();
				if (!string.IsNullOrWhiteSpace(vDepartamento))
				{
					items = CargarControl.ObtenerSubLista("PROV", vDepartamento);
				}
				CargarControl.Combo(this.cmbProvincia, items, true);
			}
			catch (Exception exception)
			{
			}
		}

		private void cmbFacialesBarbilla_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosDescriptivos();
		}

		private void cmbFacialesMejillas_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosDescriptivos();
		}

		private void cmbFacialesSurcoNasal_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosDescriptivos();
		}

		private void cmbMotivo_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				string vMotivo = (string)((ComboBox)sender).SelectedValue;
				if (string.IsNullOrWhiteSpace(vMotivo))
				{
					this.label2.Visible = false;
					this.cmbCausa.Visible = false;
				}
				else
				{
					this.label2.Visible = true;
					this.cmbCausa.Visible = true;
					List<Coder> items = CargarControl.ObtenerSubLista("MOTIVEENROLL", vMotivo, true);
					CargarControl.Combo(this.cmbCausa, items, true);
				}
				this.ValidaModeloDatosIdentificativos();
			}
			catch (Exception exception)
			{
			}
		}

		private void cmbNarizAncho_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosDescriptivos();
		}

		private void cmbNarizPunta_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosDescriptivos();
		}

		private void cmbNivelCultural_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosIdentificativos();
		}

		private void cmbOjosColor_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosDescriptivos();
		}

		private void cmbOjosForma_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosDescriptivos();
		}

		private void cmbPais_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				string vPais = (string)((ComboBox)sender).SelectedValue;
				List<Coder> items = new List<Coder>();
				if (vPais == "BOL")
				{
					items = CargarControl.ObtenerLista("PROV");
				}
				CargarControl.Combo(this.cmbDepartamento, items, true);
				this.ValidaModeloDatosIdentificativos();
			}
			catch (Exception exception)
			{
			}
		}

		private void cmbPeloColor_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosDescriptivos();
		}

		private void cmbPeloTipo_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosDescriptivos();
		}

		private void cmbRostro_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosDescriptivos();
		}

		private void cmbRostroFrente_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosDescriptivos();
		}

		private void cmbSexo_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosIdentificativos();
		}

		private void cmbTipoPersona_SelectedIndexChanged(object sender, EventArgs e)
		{
			bool flag;
			try
			{
				string selectedValue = (string)((ComboBox)sender).SelectedValue;
				if (selectedValue == null)
				{
					goto Label0;
				}
				else if (selectedValue == "NARE")
				{
					this.lblDepartamento.Visible = true;
					this.lblProvincia.Visible = true;
					this.cmbDepartamento.Visible = true;
					this.cmbProvincia.Visible = true;
					this.cmbPais.SelectedValue = "BOL";
					if (this.txtIdentificacion.Text == "INDOCUMENTADO")
					{
						this.txtIdentificacion.Text = this._numeroDoc;
						this.txtComplemento.Text = this._complemento;
					}
					TextBox textBox = this.txtComplemento;
					bool num = true;
					flag = (bool)num;
					this.txtIdentificacion.Enabled = (bool)num;
					textBox.Enabled = flag;
				}
				else if (selectedValue == "NANORE")
				{
					this.lblDepartamento.Visible = true;
					this.lblProvincia.Visible = true;
					this.cmbDepartamento.Visible = true;
					this.cmbProvincia.Visible = true;
					this.cmbPais.SelectedValue = "BOL";
					if (this.txtIdentificacion.Text == "INDOCUMENTADO")
					{
						this.txtIdentificacion.Text = this._numeroDoc;
						this.txtComplemento.Text = this._complemento;
					}
					TextBox textBox1 = this.txtComplemento;
					bool num1 = true;
					flag = (bool)num1;
					this.txtIdentificacion.Enabled = (bool)num1;
					textBox1.Enabled = flag;
				}
				else if (selectedValue == "INDOC")
				{
					this.lblDepartamento.Visible = false;
					this.lblProvincia.Visible = false;
					this.cmbDepartamento.Visible = false;
					this.cmbProvincia.Visible = false;
					this.cmbPais.SelectedValue = "NOINF";
					this._numeroDoc = this.txtIdentificacion.Text.Trim();
					this._complemento = this.txtComplemento.Text.Trim();
					this.txtIdentificacion.Text = "INDOCUMENTADO";
					this.txtComplemento.Text = "";
					TextBox textBox2 = this.txtComplemento;
					bool num2 = false;
					flag = (bool)num2;
					this.txtIdentificacion.Enabled = (bool)num2;
					textBox2.Enabled = flag;
				}
				else
				{
					if (selectedValue != "EXRE")
					{
						goto Label0;
					}
					this.lblDepartamento.Visible = false;
					this.lblProvincia.Visible = false;
					this.cmbDepartamento.Visible = false;
					this.cmbProvincia.Visible = false;
					this.cmbPais.SelectedValue = "";
					if (this.txtIdentificacion.Text == "INDOCUMENTADO")
					{
						this.txtIdentificacion.Text = this._numeroDoc;
						this.txtComplemento.Text = this._complemento;
					}
					TextBox textBox3 = this.txtComplemento;
					bool num3 = true;
					flag = (bool)num3;
					this.txtIdentificacion.Enabled = (bool)num3;
					textBox3.Enabled = flag;
				}
			Label2:
				this.ValidaModeloDatosIdentificativos();
			}
			catch (Exception exception)
			{
			}
			return;
		Label0:
			this.lblDepartamento.Visible = false;
			this.lblProvincia.Visible = false;
			this.cmbDepartamento.Visible = false;
			this.cmbProvincia.Visible = false;
			this.cmbPais.SelectedValue = "";
			if (this.txtIdentificacion.Text == "INDOCUMENTADO")
			{
				this.txtIdentificacion.Text = this._numeroDoc;
				this.txtComplemento.Text = this._complemento;
			}
			TextBox textBox4 = this.txtComplemento;
			bool num4 = true;
			flag = (bool)num4;
			this.txtIdentificacion.Enabled = (bool)num4;
			textBox4.Enabled = flag;
		//	goto Label2;
		}

		protected override void Dispose(bool disposing)
		{
			if ((!disposing ? false : this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void fDatosBiograficos_DoubleClick(object sender, EventArgs e)
		{
			base.WindowState = FormWindowState.Maximized;
		}

		private void fDatosBiograficos_Load(object sender, EventArgs e)
		{
			base.ActiveControl = this.cmbMotivo;
		}

		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fDatosBiograficos));
            this.btncancelar = new System.Windows.Forms.Button();
            this.btnsiguiente = new System.Windows.Forms.Button();
            this.btnguardar = new System.Windows.Forms.Button();
            this.errorProvedor = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnAyuda = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.btnAnterior = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNombreMadre = new System.Windows.Forms.TextBox();
            this.txtIdentificacion = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPrimerNombre = new System.Windows.Forms.TextBox();
            this.txtSegundoApellido = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPrimerApellido = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtSegundoNombre = new System.Windows.Forms.TextBox();
            this.txtNombrePadre = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbProvincia = new System.Windows.Forms.ComboBox();
            this.cmbDepartamento = new System.Windows.Forms.ComboBox();
            this.cmbPais = new System.Windows.Forms.ComboBox();
            this.lblProvincia = new System.Windows.Forms.Label();
            this.lblDepartamento = new System.Windows.Forms.Label();
            this.lblPais = new System.Windows.Forms.Label();
            this.cmbComplexion = new System.Windows.Forms.ComboBox();
            this.cmbColorPiel = new System.Windows.Forms.ComboBox();
            this.cmbSexo = new System.Windows.Forms.ComboBox();
            this.numPie = new System.Windows.Forms.NumericUpDown();
            this.numEstatura = new System.Windows.Forms.NumericUpDown();
            this.numPeso = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.cmbTipoPersona = new System.Windows.Forms.ComboBox();
            this.cmbCausa = new System.Windows.Forms.ComboBox();
            this.cmbMotivo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCodigoGenetico = new System.Windows.Forms.MaskedTextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label35 = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.label11 = new System.Windows.Forms.Label();
            this.lblComplemento = new System.Windows.Forms.Label();
            this.txtComplemento = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tabDatosBiograficos = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupControl2 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupControl7 = new System.Windows.Forms.GroupBox();
            this.cmbNivelCultural = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.groupControl1 = new System.Windows.Forms.GroupBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.lbltitulo2 = new System.Windows.Forms.Label();
            this.groupControl14 = new System.Windows.Forms.GroupBox();
            this.cmbCabezaForma = new System.Windows.Forms.ComboBox();
            this.label36 = new System.Windows.Forms.Label();
            this.groupControl8 = new System.Windows.Forms.GroupBox();
            this.cmbPeloColor = new System.Windows.Forms.ComboBox();
            this.cmbPeloTipo = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.groupControl11 = new System.Windows.Forms.GroupBox();
            this.cmbFacialesSurcoNasal = new System.Windows.Forms.ComboBox();
            this.cmbFacialesMejillas = new System.Windows.Forms.ComboBox();
            this.cmbFacialesBarbilla = new System.Windows.Forms.ComboBox();
            this.label34 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.groupControl9 = new System.Windows.Forms.GroupBox();
            this.cmbNarizPunta = new System.Windows.Forms.ComboBox();
            this.cmbNarizAncho = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.groupControl15 = new System.Windows.Forms.GroupBox();
            this.cmbCuelloAlto = new System.Windows.Forms.ComboBox();
            this.cmbCuelloAncho = new System.Windows.Forms.ComboBox();
            this.label37 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.groupControl13 = new System.Windows.Forms.GroupBox();
            this.cmbCejasPoblacion = new System.Windows.Forms.ComboBox();
            this.cmbCejasForma = new System.Windows.Forms.ComboBox();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.groupControl16 = new System.Windows.Forms.GroupBox();
            this.cmbOjosForma = new System.Windows.Forms.ComboBox();
            this.cmbOjosColor = new System.Windows.Forms.ComboBox();
            this.label39 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.groupControl12 = new System.Windows.Forms.GroupBox();
            this.cmbBocaTipo = new System.Windows.Forms.ComboBox();
            this.cmbBocaDimension = new System.Windows.Forms.ComboBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.groupControl10 = new System.Windows.Forms.GroupBox();
            this.cmbRostro = new System.Windows.Forms.ComboBox();
            this.cmbRostroFrente = new System.Windows.Forms.ComboBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.lbltitulo3 = new System.Windows.Forms.Label();
            this.groupControl20 = new System.Windows.Forms.GroupBox();
            this.chkPiesPiernas = new System.Windows.Forms.CheckedListBox();
            this.groupControl19 = new System.Windows.Forms.GroupBox();
            this.chkPiel = new System.Windows.Forms.CheckedListBox();
            this.groupControl24 = new System.Windows.Forms.GroupBox();
            this.chkOrejas = new System.Windows.Forms.CheckedListBox();
            this.groupControl18 = new System.Windows.Forms.GroupBox();
            this.chkPecho = new System.Windows.Forms.CheckedListBox();
            this.groupControl17 = new System.Windows.Forms.GroupBox();
            this.chkEspalda = new System.Windows.Forms.CheckedListBox();
            this.groupControl22 = new System.Windows.Forms.GroupBox();
            this.chkDedosManos = new System.Windows.Forms.CheckedListBox();
            this.groupControl25 = new System.Windows.Forms.GroupBox();
            this.chkDientes = new System.Windows.Forms.CheckedListBox();
            this.groupControl21 = new System.Windows.Forms.GroupBox();
            this.chkBrazos = new System.Windows.Forms.CheckedListBox();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.groupControl23 = new System.Windows.Forms.GroupBox();
            this.chkIdiomas = new System.Windows.Forms.CheckedListBox();
            this.groupControl30 = new System.Windows.Forms.GroupBox();
            this.chkAlAndar = new System.Windows.Forms.CheckedListBox();
            this.lblTitulo4 = new System.Windows.Forms.Label();
            this.groupControl26 = new System.Windows.Forms.GroupBox();
            this.chkDeLaConducta = new System.Windows.Forms.CheckedListBox();
            this.groupControl29 = new System.Windows.Forms.GroupBox();
            this.chkAlHablar = new System.Windows.Forms.CheckedListBox();
            this.groupControl27 = new System.Windows.Forms.GroupBox();
            this.chkHabilidadesEsp = new System.Windows.Forms.CheckedListBox();
            this.groupControl28 = new System.Windows.Forms.GroupBox();
            this.chkPartesAusentes = new System.Windows.Forms.CheckedListBox();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.tableLayoutPanel12 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.label20 = new System.Windows.Forms.Label();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.label41 = new System.Windows.Forms.Label();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.label43 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel14 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvedor)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPie)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEstatura)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPeso)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tabDatosBiograficos.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupControl2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupControl7.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupControl14.SuspendLayout();
            this.groupControl8.SuspendLayout();
            this.groupControl11.SuspendLayout();
            this.groupControl9.SuspendLayout();
            this.groupControl15.SuspendLayout();
            this.groupControl13.SuspendLayout();
            this.groupControl16.SuspendLayout();
            this.groupControl12.SuspendLayout();
            this.groupControl10.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.groupControl20.SuspendLayout();
            this.groupControl19.SuspendLayout();
            this.groupControl24.SuspendLayout();
            this.groupControl18.SuspendLayout();
            this.groupControl17.SuspendLayout();
            this.groupControl22.SuspendLayout();
            this.groupControl25.SuspendLayout();
            this.groupControl21.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.groupControl23.SuspendLayout();
            this.groupControl30.SuspendLayout();
            this.groupControl26.SuspendLayout();
            this.groupControl29.SuspendLayout();
            this.groupControl27.SuspendLayout();
            this.groupControl28.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            this.tableLayoutPanel12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.tableLayoutPanel14.SuspendLayout();
            this.SuspendLayout();
            // 
            // btncancelar
            // 
            this.btncancelar.BackColor = System.Drawing.Color.White;
            this.btncancelar.Image = ((System.Drawing.Image)(resources.GetObject("btncancelar.Image")));
            this.btncancelar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btncancelar.Location = new System.Drawing.Point(174, 16);
            this.btncancelar.Name = "btncancelar";
            this.btncancelar.Size = new System.Drawing.Size(75, 54);
            this.btncancelar.TabIndex = 1;
            this.btncancelar.Text = "Salir";
            this.btncancelar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btncancelar.UseVisualStyleBackColor = false;
            this.btncancelar.Click += new System.EventHandler(this.btncancelar_Click);
            // 
            // btnsiguiente
            // 
            this.btnsiguiente.BackColor = System.Drawing.Color.White;
            this.btnsiguiente.Image = ((System.Drawing.Image)(resources.GetObject("btnsiguiente.Image")));
            this.btnsiguiente.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnsiguiente.Location = new System.Drawing.Point(336, 16);
            this.btnsiguiente.Name = "btnsiguiente";
            this.btnsiguiente.Size = new System.Drawing.Size(75, 54);
            this.btnsiguiente.TabIndex = 2;
            this.btnsiguiente.Text = "Siguiente";
            this.btnsiguiente.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnsiguiente.UseVisualStyleBackColor = false;
            this.btnsiguiente.Visible = false;
            this.btnsiguiente.Click += new System.EventHandler(this.btnsiguiente_Click);
            // 
            // btnguardar
            // 
            this.btnguardar.BackColor = System.Drawing.Color.White;
            this.btnguardar.Image = ((System.Drawing.Image)(resources.GetObject("btnguardar.Image")));
            this.btnguardar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnguardar.Location = new System.Drawing.Point(3, 16);
            this.btnguardar.Name = "btnguardar";
            this.btnguardar.Size = new System.Drawing.Size(75, 54);
            this.btnguardar.TabIndex = 3;
            this.btnguardar.Text = "Guardar";
            this.btnguardar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnguardar.UseVisualStyleBackColor = false;
            this.btnguardar.Click += new System.EventHandler(this.btnguardar_Click);
            // 
            // errorProvedor
            // 
            this.errorProvedor.ContainerControl = this;
            // 
            // btnAyuda
            // 
            this.btnAyuda.BackColor = System.Drawing.Color.White;
            this.btnAyuda.Location = new System.Drawing.Point(450, 26);
            this.btnAyuda.Name = "btnAyuda";
            this.btnAyuda.Size = new System.Drawing.Size(75, 43);
            this.btnAyuda.TabIndex = 4;
            this.btnAyuda.Text = "Ayuda";
            this.btnAyuda.UseVisualStyleBackColor = false;
            this.btnAyuda.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(63)))), ((int)(((byte)(105)))));
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btnAnterior);
            this.panel1.Controls.Add(this.btnguardar);
            this.panel1.Controls.Add(this.btnsiguiente);
            this.panel1.Controls.Add(this.btncancelar);
            this.panel1.Controls.Add(this.btnAyuda);
            this.panel1.Location = new System.Drawing.Point(3, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(255, 72);
            this.panel1.TabIndex = 23;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.Location = new System.Drawing.Point(84, 15);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 54);
            this.button1.TabIndex = 7;
            this.button1.Text = "Guardar y Salir";
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
            this.btnAnterior.Location = new System.Drawing.Point(255, 16);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new System.Drawing.Size(75, 54);
            this.btnAnterior.TabIndex = 1;
            this.btnAnterior.Text = "Anterior";
            this.btnAnterior.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAnterior.UseVisualStyleBackColor = false;
            this.btnAnterior.Visible = false;
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(115, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(138, 15);
            this.label7.TabIndex = 0;
            this.label7.Text = "Identificacion:";
            // 
            // txtNombreMadre
            // 
            this.txtNombreMadre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNombreMadre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombreMadre.Location = new System.Drawing.Point(635, 86);
            this.txtNombreMadre.Name = "txtNombreMadre";
            this.txtNombreMadre.Size = new System.Drawing.Size(218, 20);
            this.txtNombreMadre.TabIndex = 25;
            this.txtNombreMadre.TextChanged += new System.EventHandler(this.txtNombreMadre_TextChanged);
            this.txtNombreMadre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombreMadre_KeyPress);
            // 
            // txtIdentificacion
            // 
            this.txtIdentificacion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIdentificacion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtIdentificacion.Location = new System.Drawing.Point(259, 3);
            this.txtIdentificacion.Name = "txtIdentificacion";
            this.txtIdentificacion.Size = new System.Drawing.Size(219, 20);
            this.txtIdentificacion.TabIndex = 18;
            this.txtIdentificacion.TextChanged += new System.EventHandler(this.txtIdentificacion_TextChanged);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(115, 33);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(138, 15);
            this.label8.TabIndex = 2;
            this.label8.Text = "Primer Nombre:";
            // 
            // txtPrimerNombre
            // 
            this.txtPrimerNombre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPrimerNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPrimerNombre.Location = new System.Drawing.Point(259, 30);
            this.txtPrimerNombre.Name = "txtPrimerNombre";
            this.txtPrimerNombre.Size = new System.Drawing.Size(219, 20);
            this.txtPrimerNombre.TabIndex = 20;
            this.txtPrimerNombre.TextChanged += new System.EventHandler(this.txtPrimerNombre_TextChanged);
            this.txtPrimerNombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrimerNombre_KeyPress);
            // 
            // txtSegundoApellido
            // 
            this.txtSegundoApellido.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSegundoApellido.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSegundoApellido.Location = new System.Drawing.Point(635, 57);
            this.txtSegundoApellido.Name = "txtSegundoApellido";
            this.txtSegundoApellido.Size = new System.Drawing.Size(218, 20);
            this.txtSegundoApellido.TabIndex = 23;
            this.txtSegundoApellido.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSegundoApellido_KeyPress);
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(115, 60);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(138, 15);
            this.label9.TabIndex = 4;
            this.label9.Text = "Primer Apellido:";
            // 
            // txtPrimerApellido
            // 
            this.txtPrimerApellido.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPrimerApellido.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPrimerApellido.Location = new System.Drawing.Point(259, 57);
            this.txtPrimerApellido.Name = "txtPrimerApellido";
            this.txtPrimerApellido.Size = new System.Drawing.Size(219, 20);
            this.txtPrimerApellido.TabIndex = 22;
            this.txtPrimerApellido.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrimerApellido_KeyPress);
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(484, 60);
            this.label12.Name = "label12";
            this.label12.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.label12.Size = new System.Drawing.Size(145, 15);
            this.label12.TabIndex = 10;
            this.label12.Text = "Segundo Apellido:";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(115, 88);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(138, 15);
            this.label10.TabIndex = 6;
            this.label10.Text = "Nombre del Padre:";
            // 
            // txtSegundoNombre
            // 
            this.txtSegundoNombre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSegundoNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSegundoNombre.Location = new System.Drawing.Point(635, 30);
            this.txtSegundoNombre.Name = "txtSegundoNombre";
            this.txtSegundoNombre.Size = new System.Drawing.Size(218, 20);
            this.txtSegundoNombre.TabIndex = 21;
            this.txtSegundoNombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSegundoNombre_KeyPress);
            // 
            // txtNombrePadre
            // 
            this.txtNombrePadre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNombrePadre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombrePadre.Location = new System.Drawing.Point(259, 86);
            this.txtNombrePadre.Name = "txtNombrePadre";
            this.txtNombrePadre.Size = new System.Drawing.Size(219, 20);
            this.txtNombrePadre.TabIndex = 24;
            this.txtNombrePadre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombrePadre_KeyPress);
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(484, 33);
            this.label13.Name = "label13";
            this.label13.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.label13.Size = new System.Drawing.Size(145, 15);
            this.label13.TabIndex = 8;
            this.label13.Text = "Segundo Nombre:";
            // 
            // cmbProvincia
            // 
            this.cmbProvincia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbProvincia.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbProvincia.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbProvincia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvincia.FormattingEnabled = true;
            this.cmbProvincia.Location = new System.Drawing.Point(635, 32);
            this.cmbProvincia.Name = "cmbProvincia";
            this.cmbProvincia.Size = new System.Drawing.Size(217, 21);
            this.cmbProvincia.TabIndex = 29;
            // 
            // cmbDepartamento
            // 
            this.cmbDepartamento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDepartamento.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbDepartamento.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbDepartamento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepartamento.FormattingEnabled = true;
            this.cmbDepartamento.Location = new System.Drawing.Point(257, 32);
            this.cmbDepartamento.Name = "cmbDepartamento";
            this.cmbDepartamento.Size = new System.Drawing.Size(222, 21);
            this.cmbDepartamento.TabIndex = 28;
            this.cmbDepartamento.SelectedIndexChanged += new System.EventHandler(this.cmbDepartamento_SelectedIndexChanged);
            // 
            // cmbPais
            // 
            this.cmbPais.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPais.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbPais.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbPais.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPais.FormattingEnabled = true;
            this.cmbPais.Location = new System.Drawing.Point(635, 3);
            this.cmbPais.Name = "cmbPais";
            this.cmbPais.Size = new System.Drawing.Size(217, 21);
            this.cmbPais.TabIndex = 27;
            this.cmbPais.SelectedIndexChanged += new System.EventHandler(this.cmbPais_SelectedIndexChanged);
            // 
            // lblProvincia
            // 
            this.lblProvincia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProvincia.AutoSize = true;
            this.lblProvincia.BackColor = System.Drawing.Color.Transparent;
            this.lblProvincia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProvincia.ForeColor = System.Drawing.Color.White;
            this.lblProvincia.Location = new System.Drawing.Point(485, 35);
            this.lblProvincia.Name = "lblProvincia";
            this.lblProvincia.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.lblProvincia.Size = new System.Drawing.Size(144, 15);
            this.lblProvincia.TabIndex = 5;
            this.lblProvincia.Text = "Provincia:";
            // 
            // lblDepartamento
            // 
            this.lblDepartamento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDepartamento.AutoSize = true;
            this.lblDepartamento.BackColor = System.Drawing.Color.Transparent;
            this.lblDepartamento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepartamento.ForeColor = System.Drawing.Color.White;
            this.lblDepartamento.Location = new System.Drawing.Point(115, 35);
            this.lblDepartamento.Name = "lblDepartamento";
            this.lblDepartamento.Size = new System.Drawing.Size(136, 15);
            this.lblDepartamento.TabIndex = 3;
            this.lblDepartamento.Text = "Departamento:";
            // 
            // lblPais
            // 
            this.lblPais.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPais.AutoSize = true;
            this.lblPais.BackColor = System.Drawing.Color.Transparent;
            this.lblPais.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPais.ForeColor = System.Drawing.Color.White;
            this.lblPais.Location = new System.Drawing.Point(485, 6);
            this.lblPais.Name = "lblPais";
            this.lblPais.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.lblPais.Size = new System.Drawing.Size(144, 15);
            this.lblPais.TabIndex = 1;
            this.lblPais.Text = "Pais:";
            // 
            // cmbComplexion
            // 
            this.cmbComplexion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbComplexion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbComplexion.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbComplexion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbComplexion.FormattingEnabled = true;
            this.cmbComplexion.Location = new System.Drawing.Point(260, 55);
            this.cmbComplexion.Name = "cmbComplexion";
            this.cmbComplexion.Size = new System.Drawing.Size(219, 21);
            this.cmbComplexion.TabIndex = 33;
            this.cmbComplexion.SelectedIndexChanged += new System.EventHandler(this.cmbComplexion_SelectedIndexChanged);
            // 
            // cmbColorPiel
            // 
            this.cmbColorPiel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbColorPiel.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbColorPiel.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbColorPiel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbColorPiel.FormattingEnabled = true;
            this.cmbColorPiel.Location = new System.Drawing.Point(260, 29);
            this.cmbColorPiel.Name = "cmbColorPiel";
            this.cmbColorPiel.Size = new System.Drawing.Size(219, 21);
            this.cmbColorPiel.TabIndex = 32;
            // 
            // cmbSexo
            // 
            this.cmbSexo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSexo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbSexo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSexo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSexo.FormattingEnabled = true;
            this.cmbSexo.Location = new System.Drawing.Point(260, 3);
            this.cmbSexo.Name = "cmbSexo";
            this.cmbSexo.Size = new System.Drawing.Size(219, 21);
            this.cmbSexo.TabIndex = 31;
            this.cmbSexo.SelectedIndexChanged += new System.EventHandler(this.cmbSexo_SelectedIndexChanged);
            // 
            // numPie
            // 
            this.numPie.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numPie.Location = new System.Drawing.Point(632, 55);
            this.numPie.Maximum = new decimal(new int[] {
            45,
            0,
            0,
            0});
            this.numPie.Minimum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.numPie.Name = "numPie";
            this.numPie.Size = new System.Drawing.Size(222, 20);
            this.numPie.TabIndex = 36;
            this.numPie.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numPie.Value = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.numPie.Visible = false;
            this.numPie.ValueChanged += new System.EventHandler(this.numPie_ValueChanged);
            // 
            // numEstatura
            // 
            this.numEstatura.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numEstatura.Location = new System.Drawing.Point(632, 29);
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
            this.numEstatura.Size = new System.Drawing.Size(222, 20);
            this.numEstatura.TabIndex = 35;
            this.numEstatura.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numEstatura.Value = new decimal(new int[] {
            49,
            0,
            0,
            0});
            this.numEstatura.ValueChanged += new System.EventHandler(this.numEstatura_ValueChanged);
            // 
            // numPeso
            // 
            this.numPeso.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numPeso.Location = new System.Drawing.Point(632, 3);
            this.numPeso.Maximum = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.numPeso.Minimum = new decimal(new int[] {
            39,
            0,
            0,
            0});
            this.numPeso.Name = "numPeso";
            this.numPeso.Size = new System.Drawing.Size(222, 20);
            this.numPeso.TabIndex = 34;
            this.numPeso.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numPeso.Value = new decimal(new int[] {
            39,
            0,
            0,
            0});
            this.numPeso.ValueChanged += new System.EventHandler(this.numPeso_ValueChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(485, 52);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.label5.Size = new System.Drawing.Size(141, 26);
            this.label5.TabIndex = 12;
            this.label5.Text = "Dimension del Pie(cm):";
            this.label5.Visible = false;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(115, 5);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(139, 15);
            this.label14.TabIndex = 2;
            this.label14.Text = "Sexo:";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(115, 31);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(139, 15);
            this.label15.TabIndex = 4;
            this.label15.Text = "Color de Piel:";
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(485, 31);
            this.label16.Name = "label16";
            this.label16.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.label16.Size = new System.Drawing.Size(141, 15);
            this.label16.TabIndex = 10;
            this.label16.Text = "Estatura (cm):";
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(115, 57);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(139, 15);
            this.label17.TabIndex = 6;
            this.label17.Text = "Complexion:";
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(485, 5);
            this.label18.Name = "label18";
            this.label18.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.label18.Size = new System.Drawing.Size(141, 15);
            this.label18.TabIndex = 8;
            this.label18.Text = "Peso (kg):";
            // 
            // cmbTipoPersona
            // 
            this.cmbTipoPersona.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTipoPersona.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbTipoPersona.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbTipoPersona.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoPersona.FormattingEnabled = true;
            this.cmbTipoPersona.Location = new System.Drawing.Point(259, 32);
            this.cmbTipoPersona.Name = "cmbTipoPersona";
            this.cmbTipoPersona.Size = new System.Drawing.Size(219, 21);
            this.cmbTipoPersona.TabIndex = 17;
            this.cmbTipoPersona.SelectedIndexChanged += new System.EventHandler(this.cmbTipoPersona_SelectedIndexChanged);
            // 
            // cmbCausa
            // 
            this.cmbCausa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCausa.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbCausa.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCausa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCausa.FormattingEnabled = true;
            this.cmbCausa.Location = new System.Drawing.Point(635, 3);
            this.cmbCausa.Name = "cmbCausa";
            this.cmbCausa.Size = new System.Drawing.Size(220, 21);
            this.cmbCausa.TabIndex = 16;
            this.cmbCausa.Visible = false;
            this.cmbCausa.SelectedIndexChanged += new System.EventHandler(this.cmbCausa_SelectedIndexChanged);
            // 
            // cmbMotivo
            // 
            this.cmbMotivo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbMotivo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbMotivo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbMotivo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMotivo.FormattingEnabled = true;
            this.cmbMotivo.Location = new System.Drawing.Point(259, 3);
            this.cmbMotivo.Name = "cmbMotivo";
            this.cmbMotivo.Size = new System.Drawing.Size(219, 21);
            this.cmbMotivo.TabIndex = 15;
            this.cmbMotivo.SelectedIndexChanged += new System.EventHandler(this.cmbMotivo_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(115, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Tipo de Persona:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(484, 6);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.label2.Size = new System.Drawing.Size(145, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Causa:";
            this.label2.Visible = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(115, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Motivo:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.28525F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.43488F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.14639F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.89666F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.61141F));
            this.tableLayoutPanel1.Controls.Add(this.cmbProvincia, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblProvincia, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmbDepartamento, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblDepartamento, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmbPais, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblPais, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label6, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtCodigoGenetico, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 389);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(937, 57);
            this.tableLayoutPanel1.TabIndex = 20;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(115, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(136, 28);
            this.label6.TabIndex = 24;
            this.label6.Text = "Fecha de Nacimiento:";
            // 
            // txtCodigoGenetico
            // 
            this.txtCodigoGenetico.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCodigoGenetico.Location = new System.Drawing.Point(257, 4);
            this.txtCodigoGenetico.Mask = "00/00/0000";
            this.txtCodigoGenetico.Name = "txtCodigoGenetico";
            this.txtCodigoGenetico.Size = new System.Drawing.Size(222, 20);
            this.txtCodigoGenetico.TabIndex = 26;
            this.txtCodigoGenetico.ValidatingType = typeof(System.DateTime);
            this.txtCodigoGenetico.TypeValidationCompleted += new System.Windows.Forms.TypeValidationEventHandler(this.txtCodigoGenetico_TypeValidationCompleted);
            this.txtCodigoGenetico.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCodigoGenetico_KeyDown);
            // 
            // label42
            // 
            this.label42.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label42.AutoSize = true;
            this.label42.BackColor = System.Drawing.Color.Transparent;
            this.label42.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label42.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.ForeColor = System.Drawing.Color.White;
            this.label42.Location = new System.Drawing.Point(115, 4);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(740, 20);
            this.label42.TabIndex = 26;
            this.label42.Text = "ENROLAMIENTO";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 79.76318F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.288483F));
            this.tableLayoutPanel2.Controls.Add(this.label42, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(12, 136);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(937, 29);
            this.tableLayoutPanel2.TabIndex = 8;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 79.76318F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.288483F));
            this.tableLayoutPanel4.Controls.Add(this.label35, 1, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(12, 225);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(937, 26);
            this.tableLayoutPanel4.TabIndex = 10;
            // 
            // label35
            // 
            this.label35.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label35.AutoSize = true;
            this.label35.BackColor = System.Drawing.Color.Transparent;
            this.label35.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label35.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.ForeColor = System.Drawing.Color.White;
            this.label35.Location = new System.Drawing.Point(115, 3);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(740, 20);
            this.label35.TabIndex = 26;
            this.label35.Text = "DATOS BASICOS";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel5.ColumnCount = 6;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.50054F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.11195F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.25404F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.0043F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.503768F));
            this.tableLayoutPanel5.Controls.Add(this.label7, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.label8, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.label9, 1, 2);
            this.tableLayoutPanel5.Controls.Add(this.txtNombrePadre, 2, 3);
            this.tableLayoutPanel5.Controls.Add(this.txtPrimerApellido, 2, 2);
            this.tableLayoutPanel5.Controls.Add(this.txtPrimerNombre, 2, 1);
            this.tableLayoutPanel5.Controls.Add(this.label10, 1, 3);
            this.tableLayoutPanel5.Controls.Add(this.txtIdentificacion, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.label11, 3, 3);
            this.tableLayoutPanel5.Controls.Add(this.txtNombreMadre, 4, 3);
            this.tableLayoutPanel5.Controls.Add(this.txtSegundoApellido, 4, 2);
            this.tableLayoutPanel5.Controls.Add(this.label12, 3, 2);
            this.tableLayoutPanel5.Controls.Add(this.label13, 3, 1);
            this.tableLayoutPanel5.Controls.Add(this.txtSegundoNombre, 4, 1);
            this.tableLayoutPanel5.Controls.Add(this.lblComplemento, 3, 0);
            this.tableLayoutPanel5.Controls.Add(this.txtComplemento, 4, 0);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(12, 250);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 4;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(937, 111);
            this.tableLayoutPanel5.TabIndex = 19;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(484, 86);
            this.label11.Name = "label11";
            this.label11.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.label11.Size = new System.Drawing.Size(145, 20);
            this.label11.TabIndex = 12;
            this.label11.Text = "Nombre de la Madre:";
            // 
            // lblComplemento
            // 
            this.lblComplemento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblComplemento.AutoSize = true;
            this.lblComplemento.BackColor = System.Drawing.Color.Transparent;
            this.lblComplemento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComplemento.ForeColor = System.Drawing.Color.White;
            this.lblComplemento.Location = new System.Drawing.Point(484, 6);
            this.lblComplemento.Name = "lblComplemento";
            this.lblComplemento.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.lblComplemento.Size = new System.Drawing.Size(145, 15);
            this.lblComplemento.TabIndex = 18;
            this.lblComplemento.Text = "Complemento:";
            // 
            // txtComplemento
            // 
            this.txtComplemento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtComplemento.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtComplemento.Location = new System.Drawing.Point(635, 3);
            this.txtComplemento.Name = "txtComplemento";
            this.txtComplemento.Size = new System.Drawing.Size(218, 20);
            this.txtComplemento.TabIndex = 19;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel8.ColumnCount = 3;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.33374F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 79.08102F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.503768F));
            this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel8.Location = new System.Drawing.Point(12, 475);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(937, 148);
            this.tableLayoutPanel8.TabIndex = 21;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.tabDatosBiograficos, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 57);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(46, 34);
            this.tableLayoutPanel3.TabIndex = 19;
            this.tableLayoutPanel3.Visible = false;
            // 
            // tabDatosBiograficos
            // 
            this.tabDatosBiograficos.Controls.Add(this.tabPage3);
            this.tabDatosBiograficos.Controls.Add(this.tabPage4);
            this.tabDatosBiograficos.Controls.Add(this.tabPage5);
            this.tabDatosBiograficos.Controls.Add(this.tabPage6);
            this.tabDatosBiograficos.ItemSize = new System.Drawing.Size(95, 20);
            this.tabDatosBiograficos.Location = new System.Drawing.Point(3, 3);
            this.tabDatosBiograficos.Name = "tabDatosBiograficos";
            this.tabDatosBiograficos.SelectedIndex = 0;
            this.tabDatosBiograficos.Size = new System.Drawing.Size(23, 28);
            this.tabDatosBiograficos.TabIndex = 6;
            this.tabDatosBiograficos.Visible = false;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupControl2);
            this.tabPage3.Controls.Add(this.groupControl1);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(15, 0);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Datos Biograficos";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.tabControl1);
            this.groupControl2.Location = new System.Drawing.Point(141, 23);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(835, 481);
            this.groupControl2.TabIndex = 6;
            this.groupControl2.TabStop = false;
            this.groupControl2.Text = "Datos Personales";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(26, 19);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(813, 441);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(805, 415);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Datos Identificativos";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupControl7);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(805, 415);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Datos Adicionales";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupControl7
            // 
            this.groupControl7.Controls.Add(this.cmbNivelCultural);
            this.groupControl7.Controls.Add(this.label19);
            this.groupControl7.Controls.Add(this.label23);
            this.groupControl7.Location = new System.Drawing.Point(26, 22);
            this.groupControl7.Name = "groupControl7";
            this.groupControl7.Size = new System.Drawing.Size(783, 274);
            this.groupControl7.TabIndex = 15;
            this.groupControl7.TabStop = false;
            this.groupControl7.Text = "Otros Datos";
            // 
            // cmbNivelCultural
            // 
            this.cmbNivelCultural.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbNivelCultural.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbNivelCultural.FormattingEnabled = true;
            this.cmbNivelCultural.Location = new System.Drawing.Point(134, 27);
            this.cmbNivelCultural.Name = "cmbNivelCultural";
            this.cmbNivelCultural.Size = new System.Drawing.Size(223, 21);
            this.cmbNivelCultural.TabIndex = 41;
            this.cmbNivelCultural.SelectedIndexChanged += new System.EventHandler(this.cmbNivelCultural_SelectedIndexChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label19.Location = new System.Drawing.Point(39, 34);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(89, 14);
            this.label19.TabIndex = 2;
            this.label19.Text = "Nivel cultural:";
            this.label19.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label23.Location = new System.Drawing.Point(425, 34);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(111, 14);
            this.label23.TabIndex = 8;
            this.label23.Text = "Codigo genetico:";
            // 
            // groupControl1
            // 
            this.groupControl1.Location = new System.Drawing.Point(33, 51);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(835, 29);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.TabStop = false;
            this.groupControl1.Text = "Causas del Fichaje";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.lbltitulo2);
            this.tabPage4.Controls.Add(this.groupControl14);
            this.tabPage4.Controls.Add(this.groupControl8);
            this.tabPage4.Controls.Add(this.groupControl11);
            this.tabPage4.Controls.Add(this.groupControl9);
            this.tabPage4.Controls.Add(this.groupControl15);
            this.tabPage4.Controls.Add(this.groupControl13);
            this.tabPage4.Controls.Add(this.groupControl16);
            this.tabPage4.Controls.Add(this.groupControl12);
            this.tabPage4.Controls.Add(this.groupControl10);
            this.tabPage4.Location = new System.Drawing.Point(4, 24);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(15, 0);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Datos Descriptivos";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // lbltitulo2
            // 
            this.lbltitulo2.AutoSize = true;
            this.lbltitulo2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltitulo2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbltitulo2.Location = new System.Drawing.Point(29, 17);
            this.lbltitulo2.Name = "lbltitulo2";
            this.lbltitulo2.Size = new System.Drawing.Size(160, 20);
            this.lbltitulo2.TabIndex = 12;
            this.lbltitulo2.Text = "Datos Descriptivos";
            // 
            // groupControl14
            // 
            this.groupControl14.Controls.Add(this.cmbCabezaForma);
            this.groupControl14.Controls.Add(this.label36);
            this.groupControl14.Location = new System.Drawing.Point(592, 350);
            this.groupControl14.Name = "groupControl14";
            this.groupControl14.Size = new System.Drawing.Size(314, 138);
            this.groupControl14.TabIndex = 11;
            this.groupControl14.TabStop = false;
            this.groupControl14.Text = "Cabeza";
            // 
            // cmbCabezaForma
            // 
            this.cmbCabezaForma.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCabezaForma.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCabezaForma.FormattingEnabled = true;
            this.cmbCabezaForma.Location = new System.Drawing.Point(69, 30);
            this.cmbCabezaForma.Name = "cmbCabezaForma";
            this.cmbCabezaForma.Size = new System.Drawing.Size(238, 21);
            this.cmbCabezaForma.TabIndex = 21;
            this.cmbCabezaForma.SelectedIndexChanged += new System.EventHandler(this.cmbCabezaForma_SelectedIndexChanged);
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label36.Location = new System.Drawing.Point(15, 32);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(48, 14);
            this.label36.TabIndex = 1;
            this.label36.Text = "Forma:";
            // 
            // groupControl8
            // 
            this.groupControl8.Controls.Add(this.cmbPeloColor);
            this.groupControl8.Controls.Add(this.cmbPeloTipo);
            this.groupControl8.Controls.Add(this.label22);
            this.groupControl8.Controls.Add(this.label21);
            this.groupControl8.Location = new System.Drawing.Point(33, 50);
            this.groupControl8.Name = "groupControl8";
            this.groupControl8.Size = new System.Drawing.Size(259, 138);
            this.groupControl8.TabIndex = 7;
            this.groupControl8.TabStop = false;
            this.groupControl8.Text = "Pelo";
            // 
            // cmbPeloColor
            // 
            this.cmbPeloColor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbPeloColor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbPeloColor.FormattingEnabled = true;
            this.cmbPeloColor.Location = new System.Drawing.Point(64, 59);
            this.cmbPeloColor.Name = "cmbPeloColor";
            this.cmbPeloColor.Size = new System.Drawing.Size(182, 21);
            this.cmbPeloColor.TabIndex = 5;
            this.cmbPeloColor.SelectedIndexChanged += new System.EventHandler(this.cmbPeloColor_SelectedIndexChanged);
            // 
            // cmbPeloTipo
            // 
            this.cmbPeloTipo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbPeloTipo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbPeloTipo.FormattingEnabled = true;
            this.cmbPeloTipo.Location = new System.Drawing.Point(64, 30);
            this.cmbPeloTipo.Name = "cmbPeloTipo";
            this.cmbPeloTipo.Size = new System.Drawing.Size(182, 21);
            this.cmbPeloTipo.TabIndex = 4;
            this.cmbPeloTipo.SelectedIndexChanged += new System.EventHandler(this.cmbPeloTipo_SelectedIndexChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label22.Location = new System.Drawing.Point(15, 57);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(43, 14);
            this.label22.TabIndex = 3;
            this.label22.Text = "Color:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label21.Location = new System.Drawing.Point(21, 32);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(37, 14);
            this.label21.TabIndex = 1;
            this.label21.Text = "Tipo:";
            // 
            // groupControl11
            // 
            this.groupControl11.Controls.Add(this.cmbFacialesSurcoNasal);
            this.groupControl11.Controls.Add(this.cmbFacialesMejillas);
            this.groupControl11.Controls.Add(this.cmbFacialesBarbilla);
            this.groupControl11.Controls.Add(this.label34);
            this.groupControl11.Controls.Add(this.label28);
            this.groupControl11.Controls.Add(this.label29);
            this.groupControl11.Location = new System.Drawing.Point(592, 200);
            this.groupControl11.Name = "groupControl11";
            this.groupControl11.Size = new System.Drawing.Size(314, 138);
            this.groupControl11.TabIndex = 11;
            this.groupControl11.TabStop = false;
            this.groupControl11.Text = "Faciales";
            // 
            // cmbFacialesSurcoNasal
            // 
            this.cmbFacialesSurcoNasal.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbFacialesSurcoNasal.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbFacialesSurcoNasal.FormattingEnabled = true;
            this.cmbFacialesSurcoNasal.Location = new System.Drawing.Point(92, 82);
            this.cmbFacialesSurcoNasal.Name = "cmbFacialesSurcoNasal";
            this.cmbFacialesSurcoNasal.Size = new System.Drawing.Size(215, 21);
            this.cmbFacialesSurcoNasal.TabIndex = 16;
            this.cmbFacialesSurcoNasal.SelectedIndexChanged += new System.EventHandler(this.cmbFacialesSurcoNasal_SelectedIndexChanged);
            // 
            // cmbFacialesMejillas
            // 
            this.cmbFacialesMejillas.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbFacialesMejillas.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbFacialesMejillas.FormattingEnabled = true;
            this.cmbFacialesMejillas.Location = new System.Drawing.Point(92, 52);
            this.cmbFacialesMejillas.Name = "cmbFacialesMejillas";
            this.cmbFacialesMejillas.Size = new System.Drawing.Size(215, 21);
            this.cmbFacialesMejillas.TabIndex = 15;
            this.cmbFacialesMejillas.SelectedIndexChanged += new System.EventHandler(this.cmbFacialesMejillas_SelectedIndexChanged);
            // 
            // cmbFacialesBarbilla
            // 
            this.cmbFacialesBarbilla.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbFacialesBarbilla.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbFacialesBarbilla.FormattingEnabled = true;
            this.cmbFacialesBarbilla.Location = new System.Drawing.Point(92, 25);
            this.cmbFacialesBarbilla.Name = "cmbFacialesBarbilla";
            this.cmbFacialesBarbilla.Size = new System.Drawing.Size(215, 21);
            this.cmbFacialesBarbilla.TabIndex = 14;
            this.cmbFacialesBarbilla.SelectedIndexChanged += new System.EventHandler(this.cmbFacialesBarbilla_SelectedIndexChanged);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label34.Location = new System.Drawing.Point(5, 84);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(81, 14);
            this.label34.TabIndex = 3;
            this.label34.Text = "Surco Nasal:";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label28.Location = new System.Drawing.Point(31, 57);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(55, 14);
            this.label28.TabIndex = 3;
            this.label28.Text = "Mejillas:";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label29.Location = new System.Drawing.Point(31, 32);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(55, 14);
            this.label29.TabIndex = 1;
            this.label29.Text = "Barbilla:";
            // 
            // groupControl9
            // 
            this.groupControl9.Controls.Add(this.cmbNarizPunta);
            this.groupControl9.Controls.Add(this.cmbNarizAncho);
            this.groupControl9.Controls.Add(this.label24);
            this.groupControl9.Controls.Add(this.label25);
            this.groupControl9.Location = new System.Drawing.Point(316, 50);
            this.groupControl9.Name = "groupControl9";
            this.groupControl9.Size = new System.Drawing.Size(259, 138);
            this.groupControl9.TabIndex = 8;
            this.groupControl9.TabStop = false;
            this.groupControl9.Text = "Nariz";
            // 
            // cmbNarizPunta
            // 
            this.cmbNarizPunta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbNarizPunta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbNarizPunta.FormattingEnabled = true;
            this.cmbNarizPunta.Location = new System.Drawing.Point(69, 57);
            this.cmbNarizPunta.Name = "cmbNarizPunta";
            this.cmbNarizPunta.Size = new System.Drawing.Size(182, 21);
            this.cmbNarizPunta.TabIndex = 7;
            this.cmbNarizPunta.SelectedIndexChanged += new System.EventHandler(this.cmbNarizPunta_SelectedIndexChanged);
            // 
            // cmbNarizAncho
            // 
            this.cmbNarizAncho.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbNarizAncho.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbNarizAncho.FormattingEnabled = true;
            this.cmbNarizAncho.Location = new System.Drawing.Point(69, 30);
            this.cmbNarizAncho.Name = "cmbNarizAncho";
            this.cmbNarizAncho.Size = new System.Drawing.Size(182, 21);
            this.cmbNarizAncho.TabIndex = 6;
            this.cmbNarizAncho.SelectedIndexChanged += new System.EventHandler(this.cmbNarizAncho_SelectedIndexChanged);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label24.Location = new System.Drawing.Point(15, 57);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(48, 14);
            this.label24.TabIndex = 3;
            this.label24.Text = "Punta:";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label25.Location = new System.Drawing.Point(13, 32);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(50, 14);
            this.label25.TabIndex = 1;
            this.label25.Text = "Ancho:";
            // 
            // groupControl15
            // 
            this.groupControl15.Controls.Add(this.cmbCuelloAlto);
            this.groupControl15.Controls.Add(this.cmbCuelloAncho);
            this.groupControl15.Controls.Add(this.label37);
            this.groupControl15.Controls.Add(this.label38);
            this.groupControl15.Location = new System.Drawing.Point(316, 350);
            this.groupControl15.Name = "groupControl15";
            this.groupControl15.Size = new System.Drawing.Size(259, 138);
            this.groupControl15.TabIndex = 10;
            this.groupControl15.TabStop = false;
            this.groupControl15.Text = "Cuello";
            // 
            // cmbCuelloAlto
            // 
            this.cmbCuelloAlto.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCuelloAlto.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCuelloAlto.FormattingEnabled = true;
            this.cmbCuelloAlto.Location = new System.Drawing.Point(55, 55);
            this.cmbCuelloAlto.Name = "cmbCuelloAlto";
            this.cmbCuelloAlto.Size = new System.Drawing.Size(196, 21);
            this.cmbCuelloAlto.TabIndex = 20;
            this.cmbCuelloAlto.SelectedIndexChanged += new System.EventHandler(this.cmbCuelloAlto_SelectedIndexChanged);
            // 
            // cmbCuelloAncho
            // 
            this.cmbCuelloAncho.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCuelloAncho.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCuelloAncho.FormattingEnabled = true;
            this.cmbCuelloAncho.Location = new System.Drawing.Point(55, 25);
            this.cmbCuelloAncho.Name = "cmbCuelloAncho";
            this.cmbCuelloAncho.Size = new System.Drawing.Size(196, 21);
            this.cmbCuelloAncho.TabIndex = 19;
            this.cmbCuelloAncho.SelectedIndexChanged += new System.EventHandler(this.cmbCuelloAncho_SelectedIndexChanged);
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label37.Location = new System.Drawing.Point(15, 57);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(37, 14);
            this.label37.TabIndex = 3;
            this.label37.Text = "Alto:";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label38.Location = new System.Drawing.Point(5, 32);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(50, 14);
            this.label38.TabIndex = 1;
            this.label38.Text = "Ancho:";
            // 
            // groupControl13
            // 
            this.groupControl13.Controls.Add(this.cmbCejasPoblacion);
            this.groupControl13.Controls.Add(this.cmbCejasForma);
            this.groupControl13.Controls.Add(this.label32);
            this.groupControl13.Controls.Add(this.label33);
            this.groupControl13.Location = new System.Drawing.Point(33, 200);
            this.groupControl13.Name = "groupControl13";
            this.groupControl13.Size = new System.Drawing.Size(259, 138);
            this.groupControl13.TabIndex = 9;
            this.groupControl13.TabStop = false;
            this.groupControl13.Text = "Cejas";
            // 
            // cmbCejasPoblacion
            // 
            this.cmbCejasPoblacion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCejasPoblacion.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCejasPoblacion.FormattingEnabled = true;
            this.cmbCejasPoblacion.Location = new System.Drawing.Point(85, 57);
            this.cmbCejasPoblacion.Name = "cmbCejasPoblacion";
            this.cmbCejasPoblacion.Size = new System.Drawing.Size(169, 21);
            this.cmbCejasPoblacion.TabIndex = 11;
            this.cmbCejasPoblacion.SelectedIndexChanged += new System.EventHandler(this.cmbCejasPoblacion_SelectedIndexChanged);
            // 
            // cmbCejasForma
            // 
            this.cmbCejasForma.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCejasForma.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCejasForma.FormattingEnabled = true;
            this.cmbCejasForma.Location = new System.Drawing.Point(85, 30);
            this.cmbCejasForma.Name = "cmbCejasForma";
            this.cmbCejasForma.Size = new System.Drawing.Size(169, 21);
            this.cmbCejasForma.TabIndex = 10;
            this.cmbCejasForma.SelectedIndexChanged += new System.EventHandler(this.cmbCejasForma_SelectedIndexChanged);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label32.Location = new System.Drawing.Point(15, 57);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(70, 14);
            this.label32.TabIndex = 3;
            this.label32.Text = "Poblacion:";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label33.Location = new System.Drawing.Point(37, 32);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(48, 14);
            this.label33.TabIndex = 1;
            this.label33.Text = "Forma:";
            // 
            // groupControl16
            // 
            this.groupControl16.Controls.Add(this.cmbOjosForma);
            this.groupControl16.Controls.Add(this.cmbOjosColor);
            this.groupControl16.Controls.Add(this.label39);
            this.groupControl16.Controls.Add(this.label40);
            this.groupControl16.Location = new System.Drawing.Point(33, 350);
            this.groupControl16.Name = "groupControl16";
            this.groupControl16.Size = new System.Drawing.Size(259, 138);
            this.groupControl16.TabIndex = 9;
            this.groupControl16.TabStop = false;
            this.groupControl16.Text = "Ojos";
            // 
            // cmbOjosForma
            // 
            this.cmbOjosForma.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbOjosForma.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbOjosForma.FormattingEnabled = true;
            this.cmbOjosForma.Location = new System.Drawing.Point(64, 55);
            this.cmbOjosForma.Name = "cmbOjosForma";
            this.cmbOjosForma.Size = new System.Drawing.Size(190, 21);
            this.cmbOjosForma.TabIndex = 18;
            this.cmbOjosForma.SelectedIndexChanged += new System.EventHandler(this.cmbOjosForma_SelectedIndexChanged);
            // 
            // cmbOjosColor
            // 
            this.cmbOjosColor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbOjosColor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbOjosColor.FormattingEnabled = true;
            this.cmbOjosColor.Location = new System.Drawing.Point(64, 25);
            this.cmbOjosColor.Name = "cmbOjosColor";
            this.cmbOjosColor.Size = new System.Drawing.Size(190, 21);
            this.cmbOjosColor.TabIndex = 17;
            this.cmbOjosColor.SelectedIndexChanged += new System.EventHandler(this.cmbOjosColor_SelectedIndexChanged);
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label39.Location = new System.Drawing.Point(15, 57);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(48, 14);
            this.label39.TabIndex = 3;
            this.label39.Text = "Forma:";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label40.Location = new System.Drawing.Point(21, 32);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(43, 14);
            this.label40.TabIndex = 1;
            this.label40.Text = "Color:";
            // 
            // groupControl12
            // 
            this.groupControl12.Controls.Add(this.cmbBocaTipo);
            this.groupControl12.Controls.Add(this.cmbBocaDimension);
            this.groupControl12.Controls.Add(this.label30);
            this.groupControl12.Controls.Add(this.label31);
            this.groupControl12.Location = new System.Drawing.Point(316, 200);
            this.groupControl12.Name = "groupControl12";
            this.groupControl12.Size = new System.Drawing.Size(259, 138);
            this.groupControl12.TabIndex = 10;
            this.groupControl12.TabStop = false;
            this.groupControl12.Text = "Boca";
            // 
            // cmbBocaTipo
            // 
            this.cmbBocaTipo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbBocaTipo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbBocaTipo.FormattingEnabled = true;
            this.cmbBocaTipo.Location = new System.Drawing.Point(90, 57);
            this.cmbBocaTipo.Name = "cmbBocaTipo";
            this.cmbBocaTipo.Size = new System.Drawing.Size(161, 21);
            this.cmbBocaTipo.TabIndex = 13;
            this.cmbBocaTipo.SelectedIndexChanged += new System.EventHandler(this.cmbBocaTipo_SelectedIndexChanged);
            // 
            // cmbBocaDimension
            // 
            this.cmbBocaDimension.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbBocaDimension.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbBocaDimension.FormattingEnabled = true;
            this.cmbBocaDimension.Location = new System.Drawing.Point(90, 30);
            this.cmbBocaDimension.Name = "cmbBocaDimension";
            this.cmbBocaDimension.Size = new System.Drawing.Size(161, 21);
            this.cmbBocaDimension.TabIndex = 12;
            this.cmbBocaDimension.SelectedIndexChanged += new System.EventHandler(this.cmbBocaDimension_SelectedIndexChanged);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label30.Location = new System.Drawing.Point(52, 57);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(37, 14);
            this.label30.TabIndex = 3;
            this.label30.Text = "Tipo:";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label31.Location = new System.Drawing.Point(15, 32);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(74, 14);
            this.label31.TabIndex = 1;
            this.label31.Text = "Dimension:";
            // 
            // groupControl10
            // 
            this.groupControl10.Controls.Add(this.cmbRostro);
            this.groupControl10.Controls.Add(this.cmbRostroFrente);
            this.groupControl10.Controls.Add(this.label26);
            this.groupControl10.Controls.Add(this.label27);
            this.groupControl10.Location = new System.Drawing.Point(592, 50);
            this.groupControl10.Name = "groupControl10";
            this.groupControl10.Size = new System.Drawing.Size(314, 138);
            this.groupControl10.TabIndex = 8;
            this.groupControl10.TabStop = false;
            this.groupControl10.Text = "Rostro";
            // 
            // cmbRostro
            // 
            this.cmbRostro.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbRostro.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbRostro.FormattingEnabled = true;
            this.cmbRostro.Location = new System.Drawing.Point(156, 61);
            this.cmbRostro.Name = "cmbRostro";
            this.cmbRostro.Size = new System.Drawing.Size(151, 21);
            this.cmbRostro.TabIndex = 9;
            this.cmbRostro.SelectedIndexChanged += new System.EventHandler(this.cmbRostro_SelectedIndexChanged);
            // 
            // cmbRostroFrente
            // 
            this.cmbRostroFrente.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbRostroFrente.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbRostroFrente.FormattingEnabled = true;
            this.cmbRostroFrente.Location = new System.Drawing.Point(156, 30);
            this.cmbRostroFrente.Name = "cmbRostroFrente";
            this.cmbRostroFrente.Size = new System.Drawing.Size(151, 21);
            this.cmbRostroFrente.TabIndex = 8;
            this.cmbRostroFrente.SelectedIndexChanged += new System.EventHandler(this.cmbRostroFrente_SelectedIndexChanged);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label26.Location = new System.Drawing.Point(99, 61);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(53, 14);
            this.label26.TabIndex = 3;
            this.label26.Text = "Rostro:";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label27.Location = new System.Drawing.Point(3, 32);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(149, 14);
            this.label27.TabIndex = 1;
            this.label27.Text = "Dimension de la frente:";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.lbltitulo3);
            this.tabPage5.Controls.Add(this.groupControl20);
            this.tabPage5.Controls.Add(this.groupControl19);
            this.tabPage5.Controls.Add(this.groupControl24);
            this.tabPage5.Controls.Add(this.groupControl18);
            this.tabPage5.Controls.Add(this.groupControl17);
            this.tabPage5.Controls.Add(this.groupControl22);
            this.tabPage5.Controls.Add(this.groupControl25);
            this.tabPage5.Controls.Add(this.groupControl21);
            this.tabPage5.Location = new System.Drawing.Point(4, 24);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(15, 0);
            this.tabPage5.TabIndex = 2;
            this.tabPage5.Text = "Datos Caracteristicos";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // lbltitulo3
            // 
            this.lbltitulo3.AutoSize = true;
            this.lbltitulo3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltitulo3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbltitulo3.Location = new System.Drawing.Point(29, 16);
            this.lbltitulo3.Name = "lbltitulo3";
            this.lbltitulo3.Size = new System.Drawing.Size(182, 20);
            this.lbltitulo3.TabIndex = 15;
            this.lbltitulo3.Text = "Datos Característicos";
            // 
            // groupControl20
            // 
            this.groupControl20.Controls.Add(this.chkPiesPiernas);
            this.groupControl20.Location = new System.Drawing.Point(585, 197);
            this.groupControl20.Name = "groupControl20";
            this.groupControl20.Size = new System.Drawing.Size(308, 140);
            this.groupControl20.TabIndex = 14;
            this.groupControl20.TabStop = false;
            this.groupControl20.Text = "Pies/Piernas";
            // 
            // chkPiesPiernas
            // 
            this.chkPiesPiernas.FormattingEnabled = true;
            this.chkPiesPiernas.Location = new System.Drawing.Point(15, 25);
            this.chkPiesPiernas.Name = "chkPiesPiernas";
            this.chkPiesPiernas.Size = new System.Drawing.Size(278, 94);
            this.chkPiesPiernas.TabIndex = 1;
            // 
            // groupControl19
            // 
            this.groupControl19.Controls.Add(this.chkPiel);
            this.groupControl19.Location = new System.Drawing.Point(33, 49);
            this.groupControl19.Name = "groupControl19";
            this.groupControl19.Size = new System.Drawing.Size(259, 126);
            this.groupControl19.TabIndex = 9;
            this.groupControl19.TabStop = false;
            this.groupControl19.Text = "Piel";
            // 
            // chkPiel
            // 
            this.chkPiel.FormattingEnabled = true;
            this.chkPiel.Location = new System.Drawing.Point(17, 24);
            this.chkPiel.Name = "chkPiel";
            this.chkPiel.Size = new System.Drawing.Size(225, 79);
            this.chkPiel.TabIndex = 0;
            // 
            // groupControl24
            // 
            this.groupControl24.Controls.Add(this.chkOrejas);
            this.groupControl24.Location = new System.Drawing.Point(309, 356);
            this.groupControl24.Name = "groupControl24";
            this.groupControl24.Size = new System.Drawing.Size(259, 158);
            this.groupControl24.TabIndex = 13;
            this.groupControl24.TabStop = false;
            this.groupControl24.Text = "Orejas";
            // 
            // chkOrejas
            // 
            this.chkOrejas.FormattingEnabled = true;
            this.chkOrejas.Location = new System.Drawing.Point(15, 24);
            this.chkOrejas.Name = "chkOrejas";
            this.chkOrejas.Size = new System.Drawing.Size(227, 109);
            this.chkOrejas.TabIndex = 1;
            // 
            // groupControl18
            // 
            this.groupControl18.Controls.Add(this.chkPecho);
            this.groupControl18.Location = new System.Drawing.Point(309, 49);
            this.groupControl18.Name = "groupControl18";
            this.groupControl18.Size = new System.Drawing.Size(259, 126);
            this.groupControl18.TabIndex = 10;
            this.groupControl18.TabStop = false;
            this.groupControl18.Text = "Pecho";
            // 
            // chkPecho
            // 
            this.chkPecho.FormattingEnabled = true;
            this.chkPecho.Location = new System.Drawing.Point(15, 24);
            this.chkPecho.Name = "chkPecho";
            this.chkPecho.Size = new System.Drawing.Size(227, 79);
            this.chkPecho.TabIndex = 1;
            // 
            // groupControl17
            // 
            this.groupControl17.Controls.Add(this.chkEspalda);
            this.groupControl17.Location = new System.Drawing.Point(585, 49);
            this.groupControl17.Name = "groupControl17";
            this.groupControl17.Size = new System.Drawing.Size(308, 126);
            this.groupControl17.TabIndex = 11;
            this.groupControl17.TabStop = false;
            this.groupControl17.Text = "Espalda";
            // 
            // chkEspalda
            // 
            this.chkEspalda.FormattingEnabled = true;
            this.chkEspalda.Location = new System.Drawing.Point(15, 24);
            this.chkEspalda.Name = "chkEspalda";
            this.chkEspalda.Size = new System.Drawing.Size(278, 79);
            this.chkEspalda.TabIndex = 1;
            // 
            // groupControl22
            // 
            this.groupControl22.Controls.Add(this.chkDedosManos);
            this.groupControl22.Location = new System.Drawing.Point(33, 197);
            this.groupControl22.Name = "groupControl22";
            this.groupControl22.Size = new System.Drawing.Size(259, 140);
            this.groupControl22.TabIndex = 12;
            this.groupControl22.TabStop = false;
            this.groupControl22.Text = "Dedos/Manos";
            // 
            // chkDedosManos
            // 
            this.chkDedosManos.FormattingEnabled = true;
            this.chkDedosManos.Location = new System.Drawing.Point(17, 25);
            this.chkDedosManos.Name = "chkDedosManos";
            this.chkDedosManos.Size = new System.Drawing.Size(225, 94);
            this.chkDedosManos.TabIndex = 0;
            // 
            // groupControl25
            // 
            this.groupControl25.Controls.Add(this.chkDientes);
            this.groupControl25.Location = new System.Drawing.Point(33, 356);
            this.groupControl25.Name = "groupControl25";
            this.groupControl25.Size = new System.Drawing.Size(259, 158);
            this.groupControl25.TabIndex = 12;
            this.groupControl25.TabStop = false;
            this.groupControl25.Text = "Dientes";
            // 
            // chkDientes
            // 
            this.chkDientes.FormattingEnabled = true;
            this.chkDientes.Location = new System.Drawing.Point(17, 24);
            this.chkDientes.Name = "chkDientes";
            this.chkDientes.Size = new System.Drawing.Size(225, 109);
            this.chkDientes.TabIndex = 0;
            // 
            // groupControl21
            // 
            this.groupControl21.Controls.Add(this.chkBrazos);
            this.groupControl21.Location = new System.Drawing.Point(309, 197);
            this.groupControl21.Name = "groupControl21";
            this.groupControl21.Size = new System.Drawing.Size(259, 140);
            this.groupControl21.TabIndex = 13;
            this.groupControl21.TabStop = false;
            this.groupControl21.Text = "Brazos";
            // 
            // chkBrazos
            // 
            this.chkBrazos.FormattingEnabled = true;
            this.chkBrazos.Location = new System.Drawing.Point(15, 25);
            this.chkBrazos.Name = "chkBrazos";
            this.chkBrazos.Size = new System.Drawing.Size(227, 94);
            this.chkBrazos.TabIndex = 1;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.groupControl23);
            this.tabPage6.Controls.Add(this.groupControl30);
            this.tabPage6.Controls.Add(this.lblTitulo4);
            this.tabPage6.Controls.Add(this.groupControl26);
            this.tabPage6.Controls.Add(this.groupControl29);
            this.tabPage6.Controls.Add(this.groupControl27);
            this.tabPage6.Controls.Add(this.groupControl28);
            this.tabPage6.Location = new System.Drawing.Point(4, 24);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(15, 0);
            this.tabPage6.TabIndex = 3;
            this.tabPage6.Text = "Rasgos de la Conducta";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // groupControl23
            // 
            this.groupControl23.Controls.Add(this.chkIdiomas);
            this.groupControl23.Location = new System.Drawing.Point(585, 232);
            this.groupControl23.Name = "groupControl23";
            this.groupControl23.Size = new System.Drawing.Size(308, 175);
            this.groupControl23.TabIndex = 20;
            this.groupControl23.TabStop = false;
            this.groupControl23.Text = "Idiomas";
            // 
            // chkIdiomas
            // 
            this.chkIdiomas.FormattingEnabled = true;
            this.chkIdiomas.Location = new System.Drawing.Point(15, 24);
            this.chkIdiomas.Name = "chkIdiomas";
            this.chkIdiomas.Size = new System.Drawing.Size(269, 124);
            this.chkIdiomas.TabIndex = 1;
            // 
            // groupControl30
            // 
            this.groupControl30.Controls.Add(this.chkAlAndar);
            this.groupControl30.Location = new System.Drawing.Point(33, 51);
            this.groupControl30.Name = "groupControl30";
            this.groupControl30.Size = new System.Drawing.Size(259, 159);
            this.groupControl30.TabIndex = 15;
            this.groupControl30.TabStop = false;
            this.groupControl30.Text = "Al Andar";
            // 
            // chkAlAndar
            // 
            this.chkAlAndar.FormattingEnabled = true;
            this.chkAlAndar.Location = new System.Drawing.Point(17, 24);
            this.chkAlAndar.Name = "chkAlAndar";
            this.chkAlAndar.Size = new System.Drawing.Size(217, 109);
            this.chkAlAndar.TabIndex = 0;
            // 
            // lblTitulo4
            // 
            this.lblTitulo4.AutoSize = true;
            this.lblTitulo4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitulo4.Location = new System.Drawing.Point(29, 18);
            this.lblTitulo4.Name = "lblTitulo4";
            this.lblTitulo4.Size = new System.Drawing.Size(196, 20);
            this.lblTitulo4.TabIndex = 0;
            this.lblTitulo4.Text = "Rasgos de la Conducta";
            // 
            // groupControl26
            // 
            this.groupControl26.Controls.Add(this.chkDeLaConducta);
            this.groupControl26.Location = new System.Drawing.Point(585, 51);
            this.groupControl26.Name = "groupControl26";
            this.groupControl26.Size = new System.Drawing.Size(308, 159);
            this.groupControl26.TabIndex = 17;
            this.groupControl26.TabStop = false;
            this.groupControl26.Text = "De la Conducta";
            // 
            // chkDeLaConducta
            // 
            this.chkDeLaConducta.FormattingEnabled = true;
            this.chkDeLaConducta.Location = new System.Drawing.Point(15, 24);
            this.chkDeLaConducta.Name = "chkDeLaConducta";
            this.chkDeLaConducta.Size = new System.Drawing.Size(269, 109);
            this.chkDeLaConducta.TabIndex = 1;
            // 
            // groupControl29
            // 
            this.groupControl29.Controls.Add(this.chkAlHablar);
            this.groupControl29.Location = new System.Drawing.Point(309, 51);
            this.groupControl29.Name = "groupControl29";
            this.groupControl29.Size = new System.Drawing.Size(259, 159);
            this.groupControl29.TabIndex = 16;
            this.groupControl29.TabStop = false;
            this.groupControl29.Text = "Al Hablar";
            // 
            // chkAlHablar
            // 
            this.chkAlHablar.FormattingEnabled = true;
            this.chkAlHablar.Location = new System.Drawing.Point(15, 24);
            this.chkAlHablar.Name = "chkAlHablar";
            this.chkAlHablar.Size = new System.Drawing.Size(217, 109);
            this.chkAlHablar.TabIndex = 1;
            // 
            // groupControl27
            // 
            this.groupControl27.Controls.Add(this.chkHabilidadesEsp);
            this.groupControl27.Location = new System.Drawing.Point(309, 232);
            this.groupControl27.Name = "groupControl27";
            this.groupControl27.Size = new System.Drawing.Size(259, 175);
            this.groupControl27.TabIndex = 19;
            this.groupControl27.TabStop = false;
            this.groupControl27.Text = "Habilidades Especiales";
            // 
            // chkHabilidadesEsp
            // 
            this.chkHabilidadesEsp.FormattingEnabled = true;
            this.chkHabilidadesEsp.Location = new System.Drawing.Point(15, 24);
            this.chkHabilidadesEsp.Name = "chkHabilidadesEsp";
            this.chkHabilidadesEsp.Size = new System.Drawing.Size(217, 124);
            this.chkHabilidadesEsp.TabIndex = 1;
            // 
            // groupControl28
            // 
            this.groupControl28.Controls.Add(this.chkPartesAusentes);
            this.groupControl28.Location = new System.Drawing.Point(33, 232);
            this.groupControl28.Name = "groupControl28";
            this.groupControl28.Size = new System.Drawing.Size(259, 175);
            this.groupControl28.TabIndex = 18;
            this.groupControl28.TabStop = false;
            this.groupControl28.Text = "Partes Ausentes";
            // 
            // chkPartesAusentes
            // 
            this.chkPartesAusentes.FormattingEnabled = true;
            this.chkPartesAusentes.Location = new System.Drawing.Point(17, 24);
            this.chkPartesAusentes.Name = "chkPartesAusentes";
            this.chkPartesAusentes.Size = new System.Drawing.Size(217, 124);
            this.chkPartesAusentes.TabIndex = 0;
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel10.ColumnCount = 6;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.50054F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.0043F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.71582F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.32723F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.396125F));
            this.tableLayoutPanel10.Controls.Add(this.label5, 3, 2);
            this.tableLayoutPanel10.Controls.Add(this.numPie, 4, 2);
            this.tableLayoutPanel10.Controls.Add(this.label16, 3, 1);
            this.tableLayoutPanel10.Controls.Add(this.cmbComplexion, 2, 2);
            this.tableLayoutPanel10.Controls.Add(this.label18, 3, 0);
            this.tableLayoutPanel10.Controls.Add(this.numEstatura, 4, 1);
            this.tableLayoutPanel10.Controls.Add(this.label14, 1, 0);
            this.tableLayoutPanel10.Controls.Add(this.numPeso, 4, 0);
            this.tableLayoutPanel10.Controls.Add(this.cmbColorPiel, 2, 1);
            this.tableLayoutPanel10.Controls.Add(this.label15, 1, 1);
            this.tableLayoutPanel10.Controls.Add(this.cmbSexo, 2, 0);
            this.tableLayoutPanel10.Controls.Add(this.label17, 1, 2);
            this.tableLayoutPanel10.Location = new System.Drawing.Point(12, 652);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 3;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(937, 78);
            this.tableLayoutPanel10.TabIndex = 22;
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel11.ColumnCount = 1;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel11.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel11.Location = new System.Drawing.Point(12, 105);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 1;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(940, 29);
            this.tableLayoutPanel11.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(3, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(934, 24);
            this.label4.TabIndex = 26;
            this.label4.Text = "DATOS BIOGRAFICOS";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel12
            // 
            this.tableLayoutPanel12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel12.ColumnCount = 6;
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.50054F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.11195F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.25404F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.21959F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.288483F));
            this.tableLayoutPanel12.Controls.Add(this.cmbTipoPersona, 2, 1);
            this.tableLayoutPanel12.Controls.Add(this.cmbMotivo, 2, 0);
            this.tableLayoutPanel12.Controls.Add(this.cmbCausa, 4, 0);
            this.tableLayoutPanel12.Controls.Add(this.label2, 3, 0);
            this.tableLayoutPanel12.Controls.Add(this.label3, 1, 1);
            this.tableLayoutPanel12.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel12.Location = new System.Drawing.Point(12, 164);
            this.tableLayoutPanel12.Name = "tableLayoutPanel12";
            this.tableLayoutPanel12.RowCount = 2;
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel12.Size = new System.Drawing.Size(937, 57);
            this.tableLayoutPanel12.TabIndex = 18;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(857, 21);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(80, 75);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 26;
            this.pictureBox1.TabStop = false;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel6.ColumnCount = 3;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 79.44026F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.61141F));
            this.tableLayoutPanel6.Controls.Add(this.label20, 1, 0);
            this.tableLayoutPanel6.Location = new System.Drawing.Point(12, 364);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(937, 26);
            this.tableLayoutPanel6.TabIndex = 26;
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.White;
            this.label20.Location = new System.Drawing.Point(115, 3);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(737, 20);
            this.label20.TabIndex = 26;
            this.label20.Text = "LUGAR Y FECHA DE NACIMIENTO";
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel7.ColumnCount = 3;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 79.44026F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.61141F));
            this.tableLayoutPanel7.Controls.Add(this.label41, 1, 0);
            this.tableLayoutPanel7.Location = new System.Drawing.Point(12, 450);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(937, 26);
            this.tableLayoutPanel7.TabIndex = 27;
            // 
            // label41
            // 
            this.label41.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label41.AutoSize = true;
            this.label41.BackColor = System.Drawing.Color.Transparent;
            this.label41.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label41.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.ForeColor = System.Drawing.Color.White;
            this.label41.Location = new System.Drawing.Point(115, 3);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(737, 20);
            this.label41.TabIndex = 26;
            this.label41.Text = "DIRECCIONES";
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel9.ColumnCount = 3;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 79.5479F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.503768F));
            this.tableLayoutPanel9.Controls.Add(this.label43, 1, 0);
            this.tableLayoutPanel9.Location = new System.Drawing.Point(12, 627);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 1;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(937, 26);
            this.tableLayoutPanel9.TabIndex = 28;
            // 
            // label43
            // 
            this.label43.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label43.AutoSize = true;
            this.label43.BackColor = System.Drawing.Color.Transparent;
            this.label43.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label43.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.ForeColor = System.Drawing.Color.White;
            this.label43.Location = new System.Drawing.Point(115, 3);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(738, 20);
            this.label43.TabIndex = 26;
            this.label43.Text = "DATOS ADICIONALES";
            // 
            // tableLayoutPanel14
            // 
            this.tableLayoutPanel14.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel14.ColumnCount = 2;
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel14.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel14.Controls.Add(this.pictureBox1, 1, 0);
            this.tableLayoutPanel14.Location = new System.Drawing.Point(12, 5);
            this.tableLayoutPanel14.Name = "tableLayoutPanel14";
            this.tableLayoutPanel14.RowCount = 1;
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 99F));
            this.tableLayoutPanel14.Size = new System.Drawing.Size(940, 99);
            this.tableLayoutPanel14.TabIndex = 29;
            // 
            // fDatosBiograficos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(63)))), ((int)(((byte)(105)))));
            this.ClientSize = new System.Drawing.Size(961, 748);
            this.Controls.Add(this.tableLayoutPanel14);
            this.Controls.Add(this.tableLayoutPanel9);
            this.Controls.Add(this.tableLayoutPanel7);
            this.Controls.Add(this.tableLayoutPanel6);
            this.Controls.Add(this.tableLayoutPanel12);
            this.Controls.Add(this.tableLayoutPanel11);
            this.Controls.Add(this.tableLayoutPanel10);
            this.Controls.Add(this.tableLayoutPanel8);
            this.Controls.Add(this.tableLayoutPanel5);
            this.Controls.Add(this.tableLayoutPanel4);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "fDatosBiograficos";
            this.Text = "Registro de Datos Biograficos";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.fDatosBiograficos_Load);
            this.DoubleClick += new System.EventHandler(this.fDatosBiograficos_DoubleClick);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvedor)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numPie)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEstatura)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPeso)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tabDatosBiograficos.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.groupControl2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupControl7.ResumeLayout(false);
            this.groupControl7.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.groupControl14.ResumeLayout(false);
            this.groupControl14.PerformLayout();
            this.groupControl8.ResumeLayout(false);
            this.groupControl8.PerformLayout();
            this.groupControl11.ResumeLayout(false);
            this.groupControl11.PerformLayout();
            this.groupControl9.ResumeLayout(false);
            this.groupControl9.PerformLayout();
            this.groupControl15.ResumeLayout(false);
            this.groupControl15.PerformLayout();
            this.groupControl13.ResumeLayout(false);
            this.groupControl13.PerformLayout();
            this.groupControl16.ResumeLayout(false);
            this.groupControl16.PerformLayout();
            this.groupControl12.ResumeLayout(false);
            this.groupControl12.PerformLayout();
            this.groupControl10.ResumeLayout(false);
            this.groupControl10.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.groupControl20.ResumeLayout(false);
            this.groupControl19.ResumeLayout(false);
            this.groupControl24.ResumeLayout(false);
            this.groupControl18.ResumeLayout(false);
            this.groupControl17.ResumeLayout(false);
            this.groupControl22.ResumeLayout(false);
            this.groupControl25.ResumeLayout(false);
            this.groupControl21.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.groupControl23.ResumeLayout(false);
            this.groupControl30.ResumeLayout(false);
            this.groupControl26.ResumeLayout(false);
            this.groupControl29.ResumeLayout(false);
            this.groupControl27.ResumeLayout(false);
            this.groupControl28.ResumeLayout(false);
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tableLayoutPanel10.PerformLayout();
            this.tableLayoutPanel11.ResumeLayout(false);
            this.tableLayoutPanel11.PerformLayout();
            this.tableLayoutPanel12.ResumeLayout(false);
            this.tableLayoutPanel12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            this.tableLayoutPanel14.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		private void numEstatura_ValueChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosIdentificativos();
		}

		private void numPeso_ValueChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosIdentificativos();
		}

		private void numPie_ValueChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosIdentificativos();
		}

		private Coder ObtenerCoder(ComboBox pCmb, string pCoderTypeId)
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

		private BindingList<Coder> ObtenerListaCoder(CheckedListBox pChkListBox, string pGrupo)
		{
			BindingList<Coder> vColCoderSalida = new BindingList<Coder>();
			Coder vCoderItem = new Coder();
			List<Coder> vCoderCompleto = CargarControl.ObtenerLista(pGrupo);
			foreach (object checkedItem in pChkListBox.CheckedItems)
			{
				vCoderItem = (
					from cust in vCoderCompleto
					where cust.Value == checkedItem.ToString()
					select cust).FirstOrDefault<Coder>();
				if (vCoderItem != null)
				{
					vColCoderSalida.Add(new Coder()
					{
						CoderTypeId = vCoderItem.CoderTypeId,
						Id = vCoderItem.Id,
						Value = vCoderItem.Value
					});
				}
			}
			return vColCoderSalida;
		}

		private void panel2_Paint(object sender, PaintEventArgs e)
		{
		}

		private void txtCodigoGenetico_KeyDown(object sender, KeyEventArgs e)
		{
			this.toolTip1.Hide(this.txtCodigoGenetico);
		}

		private void txtCodigoGenetico_TextChanged(object sender, EventArgs e)
		{
		}

		private void txtCodigoGenetico_TypeValidationCompleted(object sender, TypeValidationEventArgs e)
		{
			if (e.IsValidInput)
			{
				DateTime userDate = (DateTime)e.ReturnValue;
				if (userDate > DateTime.Now)
				{
					this.toolTip1.ToolTipTitle = "Fecha invalida";
					this.toolTip1.Show("La fecha no puede ser mayor a la fecha actual.", this.txtCodigoGenetico, this.txtCodigoGenetico.Width, -40, 5000);
					this.txtCodigoGenetico.Text = "";
					base.ActiveControl = this.txtCodigoGenetico;
					e.Cancel = true;
				}
				if (userDate < DateTime.Now.AddYears(-100))
				{
					this.toolTip1.ToolTipTitle = "Fecha invalida";
					ToolTip toolTip = this.toolTip1;
					DateTime dateTime = DateTime.Now.AddYears(-100);
					toolTip.Show(string.Concat("La fecha no puede ser menor a: ", dateTime.ToString("dd/mm/yyyy")), this.txtCodigoGenetico, this.txtCodigoGenetico.Width, -40, 5000);
					this.txtCodigoGenetico.Text = "";
					base.ActiveControl = this.txtCodigoGenetico;
					e.Cancel = true;
				}
			}
			else
			{
				this.toolTip1.ToolTipTitle = "Fecha invalida";
				this.toolTip1.Show("El formato de la fecha no es válido dd/mm/yyyy.", this.txtCodigoGenetico, this.txtCodigoGenetico.Width, -40, 5000);
				this.txtCodigoGenetico.Text = "";
				base.ActiveControl = this.txtCodigoGenetico;
			}
		}

		private void txtIdentificacion_TextChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosIdentificativos();
		}

		private void txtNombreMadre_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((!char.IsNumber(e.KeyChar) ? false : e.KeyChar != '\b'))
			{
				MessageBox.Show("No se permiten Numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				e.Handled = true;
			}
		}

		private void txtNombreMadre_TextChanged(object sender, EventArgs e)
		{
		}

		private void txtNombrePadre_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((!char.IsNumber(e.KeyChar) ? false : e.KeyChar != '\b'))
			{
				MessageBox.Show("No se permiten Numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				e.Handled = true;
			}
		}

		private void txtPrimerApellido_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((!char.IsNumber(e.KeyChar) ? false : e.KeyChar != '\b'))
			{
				MessageBox.Show("No se permiten Numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				e.Handled = true;
			}
		}

		private void txtPrimerNombre_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((!char.IsNumber(e.KeyChar) ? false : e.KeyChar != '\b'))
			{
				MessageBox.Show("No se permiten Numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				e.Handled = true;
			}
		}

		private void txtPrimerNombre_TextChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosIdentificativos();
		}

		private void txtSegundoApellido_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((!char.IsNumber(e.KeyChar) ? false : e.KeyChar != '\b'))
			{
				MessageBox.Show("No se permiten Numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				e.Handled = true;
			}
		}

		private void txtSegundoNombre_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((!char.IsNumber(e.KeyChar) ? false : e.KeyChar != '\b'))
			{
				MessageBox.Show("No se permiten Numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				e.Handled = true;
			}
		}

		private bool ValidaModeloDatosDescriptivos()
		{
			bool enabled;
			try
			{
				enabled = this.btnsiguiente.Enabled;
			}
			catch
			{
				enabled = false;
			}
			return enabled;
		}

		private bool ValidaModeloDatosIdentificativos()
		{
			bool enabled;
			try
			{
				enabled = this.btnsiguiente.Enabled;
			}
			catch
			{
				enabled = false;
			}
			return enabled;
		}
	}
}