using ENROLL.Control;
using ENROLL.Helpers;
using Datys.Enroll.Core;
using Datys.SIP.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace ENROLL
{
    public class vBiography : Form
    {
        private string _numeroDoc = string.Empty;
        private string _complemento = string.Empty;
        private IContainer components = (IContainer)null;
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

        public vBiography()
        {
            try
            {
                this.InitializeComponent();
                DataSet dataSet = new DataSet("XML DataSet");
                HelperLoadControl.Combo(this.cmbMotivo, HelperLoadControl.ObtenerLista("MOTIVEENROLL"), true);
                HelperLoadControl.Combo(this.cmbTipoPersona, HelperLoadControl.ObtenerLista("PERSONTYPE"), true);
                HelperLoadControl.Combo(this.cmbPais, HelperLoadControl.ObtenerLista("COUNTRY", true), true);
                HelperLoadControl.Combo(this.cmbSexo, HelperLoadControl.ObtenerLista("SEX"), true);
                HelperLoadControl.Combo(this.cmbColorPiel, HelperLoadControl.ObtenerLista("SKIN"), true);
                HelperLoadControl.Combo(this.cmbComplexion, HelperLoadControl.ObtenerLista("COMPLEXION"), true);
                HelperLoadControl.Combo(this.cmbNivelCultural, HelperLoadControl.ObtenerLista("CULTURALLEVEL"), true);
                HelperLoadControl.Combo(this.cmbPeloTipo, HelperLoadControl.ObtenerLista("HAIRTYPE"), true);
                HelperLoadControl.Combo(this.cmbPeloColor, HelperLoadControl.ObtenerLista("HAIRCOLOR"), true);
                HelperLoadControl.Combo(this.cmbNarizAncho, HelperLoadControl.ObtenerLista("NOSEWIDTH"), true);
                HelperLoadControl.Combo(this.cmbNarizPunta, HelperLoadControl.ObtenerLista("NOSETIP"), true);
                HelperLoadControl.Combo(this.cmbRostroFrente, HelperLoadControl.ObtenerLista("FOREHEAD"), true);
                HelperLoadControl.Combo(this.cmbRostro, HelperLoadControl.ObtenerLista("FACEPROPORTION"), true);
                HelperLoadControl.Combo(this.cmbCejasForma, HelperLoadControl.ObtenerLista("EYEBROWSHAPE"), true);
                HelperLoadControl.Combo(this.cmbCejasPoblacion, HelperLoadControl.ObtenerLista("EYEBROWPOP"), true);
                HelperLoadControl.Combo(this.cmbBocaDimension, HelperLoadControl.ObtenerLista("MOUTH"), true);
                HelperLoadControl.Combo(this.cmbBocaTipo, HelperLoadControl.ObtenerLista("MOUTHTYPE"), true);
                HelperLoadControl.Combo(this.cmbFacialesBarbilla, HelperLoadControl.ObtenerLista("CHIN"), true);
                HelperLoadControl.Combo(this.cmbFacialesMejillas, HelperLoadControl.ObtenerLista("CHEEKS"), true);
                HelperLoadControl.Combo(this.cmbFacialesSurcoNasal, HelperLoadControl.ObtenerLista("NASALGROOVE"), true);
                HelperLoadControl.Combo(this.cmbOjosColor, HelperLoadControl.ObtenerLista("EYESCOLOR"), true);
                HelperLoadControl.Combo(this.cmbOjosForma, HelperLoadControl.ObtenerLista("EYESSHAPE"), true);
                HelperLoadControl.Combo(this.cmbCuelloAncho, HelperLoadControl.ObtenerLista("NECKWIDTH"), true);
                HelperLoadControl.Combo(this.cmbCuelloAlto, HelperLoadControl.ObtenerLista("NECKLONG"), true);
                HelperLoadControl.Combo(this.cmbCabezaForma, HelperLoadControl.ObtenerLista("HEADSHAPE"), true);
                HelperLoadControl.Checklist(this.chkPiel, HelperLoadControl.ObtenerLista("SKINCHARACT"));
                HelperLoadControl.Checklist(this.chkPecho, HelperLoadControl.ObtenerLista("CHEST"));
                HelperLoadControl.Checklist(this.chkEspalda, HelperLoadControl.ObtenerLista("BACK"));
                HelperLoadControl.Checklist(this.chkDedosManos, HelperLoadControl.ObtenerLista("HAND"));
                HelperLoadControl.Checklist(this.chkBrazos, HelperLoadControl.ObtenerLista("ARMS"));
                HelperLoadControl.Checklist(this.chkPiesPiernas, HelperLoadControl.ObtenerLista("FEET"));
                HelperLoadControl.Checklist(this.chkDientes, HelperLoadControl.ObtenerLista("TEETH"));
                HelperLoadControl.Checklist(this.chkOrejas, HelperLoadControl.ObtenerLista("EAR"));
                HelperLoadControl.Checklist(this.chkAlAndar, HelperLoadControl.ObtenerLista("WALK"));
                HelperLoadControl.Checklist(this.chkAlHablar, HelperLoadControl.ObtenerLista("TALK"));
                HelperLoadControl.Checklist(this.chkDeLaConducta, HelperLoadControl.ObtenerLista("BEHAVIOR"));
                HelperLoadControl.Checklist(this.chkPartesAusentes, HelperLoadControl.ObtenerLista("ABSENT"));
                HelperLoadControl.Checklist(this.chkHabilidadesEsp, HelperLoadControl.ObtenerLista("SPECIAL"));
                HelperLoadControl.Checklist(this.chkIdiomas, HelperLoadControl.ObtenerLista("LANGUAGE"));
                this.CargarValores(vEnroll.PersonaCapturada);
                this.ValidaModeloDatosIdentificativos();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void CargarValores(CapturedPerson personaCapturada)
        {
            try
            {
                if (personaCapturada == null)
                    return;
                if (personaCapturada.OfflinePerson != null && personaCapturada.OfflinePerson.Identities.Any<Identity>())
                {
                    if (personaCapturada.OfflinePerson.Identities[0].PersonType != null && personaCapturada.OfflinePerson.Identities[0].PersonType.Id != null)
                        this.cmbTipoPersona.SelectedValue = (object)personaCapturada.OfflinePerson.Identities[0].PersonType.Id;
                    if (!string.IsNullOrWhiteSpace(personaCapturada.OfflinePerson.Identities[0].Identification))
                    {
                        string[] strArray = personaCapturada.OfflinePerson.Identities[0].Identification.Split('|');
                        this.txtIdentificacion.Text = strArray[0];
                        if (((IEnumerable<string>)strArray).Count<string>() == 2)
                            this.txtComplemento.Text = strArray[1];
                        else
                            this.txtComplemento.Text = "";
                    }
                    this.txtPrimerNombre.Text = personaCapturada.OfflinePerson.Identities[0].FirstName;
                    this.txtSegundoNombre.Text = personaCapturada.OfflinePerson.Identities[0].SecondName;
                    this.txtPrimerApellido.Text = personaCapturada.OfflinePerson.Identities[0].FirstLastName;
                    this.txtSegundoApellido.Text = personaCapturada.OfflinePerson.Identities[0].SecondLastName;
                    this.txtNombrePadre.Text = personaCapturada.OfflinePerson.Identities[0].FatherName;
                    this.txtNombreMadre.Text = personaCapturada.OfflinePerson.Identities[0].MotherName;
                    this.txtCodigoGenetico.Text = personaCapturada.OfflinePerson.Identities[0].GeneticCode;
                    this.nacionalidades1.Paises = personaCapturada.OfflinePerson.Identities[0].Nationalities;
                    if (personaCapturada.OfflinePerson.Identities[0].Country != null && personaCapturada.OfflinePerson.Identities[0].Country.Id != null)
                        this.cmbPais.SelectedValue = (object)personaCapturada.OfflinePerson.Identities[0].Country.Id;
                    if (personaCapturada.OfflinePerson.Identities[0].State != null && personaCapturada.OfflinePerson.Identities[0].State.Id != null)
                        this.cmbDepartamento.SelectedValue = (object)personaCapturada.OfflinePerson.Identities[0].State.Id;
                    if (personaCapturada.OfflinePerson.Identities[0].District != null && personaCapturada.OfflinePerson.Identities[0].District.Id != null)
                        this.cmbProvincia.SelectedValue = (object)personaCapturada.OfflinePerson.Identities[0].District.Id;
                    if (personaCapturada.OfflinePerson.Identities[0].Sex != null && personaCapturada.OfflinePerson.Identities[0].Sex.Id != null)
                        this.cmbSexo.SelectedValue = (object)personaCapturada.OfflinePerson.Identities[0].Sex.Id;
                    if (personaCapturada.OfflinePerson.Identities[0].Skin != null && personaCapturada.OfflinePerson.Identities[0].Skin.Id != null)
                        this.cmbColorPiel.SelectedValue = (object)personaCapturada.OfflinePerson.Identities[0].Skin.Id;
                }
                if (personaCapturada.RecordData != null)
                {
                    if (personaCapturada.RecordData.Crime != null && personaCapturada.RecordData.Crime.CoderTypeId != null)
                    {
                        this.cmbMotivo.SelectedValue = (object)personaCapturada.RecordData.Crime.CoderTypeId;
                        this.cmbCausa.SelectedValue = (object)personaCapturada.RecordData.Crime.Id;
                    }
                    if (personaCapturada.RecordData.Complexion != null)
                        this.cmbComplexion.SelectedValue = (object)personaCapturada.RecordData.Complexion;
                    this.numPeso.Value = Convert.ToDecimal(personaCapturada.RecordData.Weigth);
                    this.numEstatura.Value = Convert.ToDecimal(personaCapturada.RecordData.BodySize);
                    this.numPie.Value = Convert.ToDecimal(personaCapturada.RecordData.Foot);
                    if (personaCapturada.RecordData.CulturalLevel != null)
                        this.cmbNivelCultural.SelectedValue = (object)personaCapturada.RecordData.CulturalLevel;
                    this.direccion1.Direcciones = personaCapturada.RecordData.Addresses;
                    this.alias1.Nombres = personaCapturada.RecordData.Alias;
                    if (personaCapturada.RecordData.DescriptiveData != null)
                    {
                        if (personaCapturada.RecordData.DescriptiveData.Hair != null)
                        {
                            if (personaCapturada.RecordData.DescriptiveData.Hair.Type != null)
                                this.cmbPeloTipo.SelectedValue = (object)personaCapturada.RecordData.DescriptiveData.Hair.Type;
                            if (personaCapturada.RecordData.DescriptiveData.Hair.Color != null)
                                this.cmbPeloColor.SelectedValue = (object)personaCapturada.RecordData.DescriptiveData.Hair.Color;
                        }
                        if (personaCapturada.RecordData.DescriptiveData.Nose != null)
                        {
                            if (personaCapturada.RecordData.DescriptiveData.Nose.Width != null)
                                this.cmbNarizAncho.SelectedValue = (object)personaCapturada.RecordData.DescriptiveData.Nose.Width;
                            if (personaCapturada.RecordData.DescriptiveData.Nose.Tip != null)
                                this.cmbNarizPunta.SelectedValue = (object)personaCapturada.RecordData.DescriptiveData.Nose.Tip;
                        }
                        if (personaCapturada.RecordData.DescriptiveData.Face != null)
                        {
                            if (personaCapturada.RecordData.DescriptiveData.Face.BrowDimension != null)
                                this.cmbRostroFrente.SelectedValue = (object)personaCapturada.RecordData.DescriptiveData.Face.BrowDimension;
                            if (personaCapturada.RecordData.DescriptiveData.Face.Proportion != null)
                                this.cmbRostro.SelectedValue = (object)personaCapturada.RecordData.DescriptiveData.Face.Proportion;
                        }
                        if (personaCapturada.RecordData.DescriptiveData.Eyebrow != null)
                        {
                            if (personaCapturada.RecordData.DescriptiveData.Eyebrow.Shape != null)
                                this.cmbCejasForma.SelectedValue = (object)personaCapturada.RecordData.DescriptiveData.Eyebrow.Shape;
                            if (personaCapturada.RecordData.DescriptiveData.Eyebrow.Population != null)
                                this.cmbCejasPoblacion.SelectedValue = (object)personaCapturada.RecordData.DescriptiveData.Eyebrow.Population;
                        }
                        if (personaCapturada.RecordData.DescriptiveData.Mouth != null)
                        {
                            if (personaCapturada.RecordData.DescriptiveData.Mouth.Type != null)
                                this.cmbBocaTipo.SelectedValue = (object)personaCapturada.RecordData.DescriptiveData.Mouth.Type;
                            if (personaCapturada.RecordData.DescriptiveData.Mouth.Dimension != null)
                                this.cmbBocaDimension.SelectedValue = (object)personaCapturada.RecordData.DescriptiveData.Mouth.Dimension;
                        }
                        if (personaCapturada.RecordData.DescriptiveData.Facial != null)
                        {
                            if (personaCapturada.RecordData.DescriptiveData.Facial.Cheeks != null)
                                this.cmbFacialesBarbilla.SelectedValue = (object)personaCapturada.RecordData.DescriptiveData.Facial.Cheeks;
                            if (personaCapturada.RecordData.DescriptiveData.Facial.Chin != null)
                                this.cmbFacialesMejillas.SelectedValue = (object)personaCapturada.RecordData.DescriptiveData.Facial.Chin;
                            if (personaCapturada.RecordData.DescriptiveData.Facial.NasalGroove != null)
                                this.cmbFacialesSurcoNasal.SelectedValue = (object)personaCapturada.RecordData.DescriptiveData.Facial.NasalGroove;
                        }
                        if (personaCapturada.RecordData.DescriptiveData.Eyes != null)
                        {
                            if (personaCapturada.RecordData.DescriptiveData.Eyes.Shape != null)
                                this.cmbOjosForma.SelectedValue = (object)personaCapturada.RecordData.DescriptiveData.Eyes.Shape;
                            if (personaCapturada.RecordData.DescriptiveData.Eyes.Color != null)
                                this.cmbOjosColor.SelectedValue = (object)personaCapturada.RecordData.DescriptiveData.Eyes.Color;
                        }
                        if (personaCapturada.RecordData.DescriptiveData.Neck != null)
                        {
                            if (personaCapturada.RecordData.DescriptiveData.Neck.Long != null)
                                this.cmbCuelloAlto.SelectedValue = (object)personaCapturada.RecordData.DescriptiveData.Neck.Long;
                            if (personaCapturada.RecordData.DescriptiveData.Neck.Width != null)
                                this.cmbCuelloAncho.SelectedValue = (object)personaCapturada.RecordData.DescriptiveData.Neck.Width;
                        }
                        if (personaCapturada.RecordData.DescriptiveData.HeadShape != null)
                            this.cmbCabezaForma.SelectedValue = (object)personaCapturada.RecordData.DescriptiveData.HeadShape;
                    }
                    if (personaCapturada.RecordData.CharacteristicData != null)
                    {
                        HelperLoadControl.Checklist(personaCapturada.RecordData.CharacteristicData.Skin, this.chkPiel);
                        HelperLoadControl.Checklist(personaCapturada.RecordData.CharacteristicData.Chest, this.chkPecho);
                        HelperLoadControl.Checklist(personaCapturada.RecordData.CharacteristicData.Back, this.chkEspalda);
                        HelperLoadControl.Checklist(personaCapturada.RecordData.CharacteristicData.FingerHand, this.chkDedosManos);
                        HelperLoadControl.Checklist(personaCapturada.RecordData.CharacteristicData.Arm, this.chkBrazos);
                        HelperLoadControl.Checklist(personaCapturada.RecordData.CharacteristicData.FootLeg, this.chkPiesPiernas);
                        HelperLoadControl.Checklist(personaCapturada.RecordData.CharacteristicData.Tooth, this.chkDientes);
                        HelperLoadControl.Checklist(personaCapturada.RecordData.CharacteristicData.Ear, this.chkOrejas);
                    }
                    if (personaCapturada.RecordData.FeatureData != null)
                    {
                        HelperLoadControl.Checklist(personaCapturada.RecordData.FeatureData.Walk, this.chkAlAndar);
                        HelperLoadControl.Checklist(personaCapturada.RecordData.FeatureData.Talk, this.chkAlHablar);
                        HelperLoadControl.Checklist(personaCapturada.RecordData.FeatureData.Behavior, this.chkDeLaConducta);
                        HelperLoadControl.Checklist(personaCapturada.RecordData.FeatureData.Absent, this.chkPartesAusentes);
                        HelperLoadControl.Checklist(personaCapturada.RecordData.FeatureData.Special, this.chkHabilidadesEsp);
                        HelperLoadControl.Checklist(personaCapturada.RecordData.FeatureData.Language, this.chkIdiomas);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void ActualizarCapturedPerson()
        {
            try
            {
                CapturedPerson pCapturedPerson = vEnroll.PersonaCapturada ?? new CapturedPerson();
                if (pCapturedPerson.BasicData == null)
                    pCapturedPerson.BasicData = new LightPersonBasicData();
                if (pCapturedPerson.RecordData == null)
                    pCapturedPerson.RecordData = new RecordData();
                if (pCapturedPerson.OfflinePerson == null)
                    pCapturedPerson.OfflinePerson = new OfflinePerson();
                string str1 = this.txtPrimerNombre.Text.Trim() + (string.IsNullOrWhiteSpace(this.txtSegundoNombre.Text) ? "" : " " + this.txtSegundoNombre.Text.Trim()) + (string.IsNullOrWhiteSpace(this.txtPrimerApellido.Text) ? "" : " " + this.txtPrimerApellido.Text.Trim()) + (string.IsNullOrWhiteSpace(this.txtSegundoApellido.Text) ? "" : " " + this.txtSegundoApellido.Text.Trim());
                if (string.IsNullOrWhiteSpace(pCapturedPerson.BasicData.FullName))
                {
                    pCapturedPerson.BasicData.FullName = str1;
                    pCapturedPerson.BasicData.Created = DateTime.Now;
                    pCapturedPerson.BasicData.CreatedBy = vLogin.usuario;
                    pCapturedPerson.BasicData.Modified = DateTime.Now;
                    pCapturedPerson.BasicData.ModifiedBy = vLogin.usuario;
                    pCapturedPerson.BasicData.Label = vLogin.unidad;
                }
                else
                {
                    pCapturedPerson.BasicData.FullName = str1;
                    pCapturedPerson.BasicData.Modified = DateTime.Now;
                    pCapturedPerson.BasicData.ModifiedBy = vLogin.usuario;
                }
                Coder coder1 = this.ObtenerCoder(this.cmbTipoPersona, "PERSONTYPE");
                Coder coder2 = this.ObtenerCoder(this.cmbPais, "COUNTRY");
                Coder coder3 = this.ObtenerCoder(this.cmbDepartamento, "PROV");
                Coder coder4 = this.ObtenerCoder(this.cmbProvincia, "MUN");
                Coder coder5 = this.ObtenerCoder(this.cmbSexo, "SEX");
                Coder coder6 = this.ObtenerCoder(this.cmbColorPiel, "SKIN");
                string str2 = string.IsNullOrWhiteSpace(this.txtComplemento.Text.Trim()) ? "" : "|" + this.txtComplemento.Text.Trim();
                OfflinePerson offlinePerson = pCapturedPerson.OfflinePerson;
                BindingList<Identity> bindingList = new BindingList<Identity>();
                bindingList.Add(new Identity()
                {
                    Identification = this.txtIdentificacion.Text.Trim() + str2,
                    FirstName = this.txtPrimerNombre.Text.Trim(),
                    SecondName = this.txtSegundoNombre.Text.Trim(),
                    FirstLastName = this.txtPrimerApellido.Text.Trim(),
                    SecondLastName = this.txtSegundoApellido.Text.Trim(),
                    FatherName = this.txtNombrePadre.Text.Trim(),
                    MotherName = this.txtNombreMadre.Text.Trim(),
                    GeneticCode = this.txtCodigoGenetico.Text.Trim(),
                    PersonType = coder1,
                    Country = coder2,
                    State = coder3,
                    District = coder4,
                    Sex = coder5,
                    Skin = coder6,
                    Nationalities = this.nacionalidades1.Paises
                });
                offlinePerson.Identities = bindingList;
                Coder coder7 = this.ObtenerCoder(this.cmbMotivo, "MOTIVEENROLL");
                pCapturedPerson.RecordData.Motive = coder7;
                Coder coder8 = this.cmbMotivo.SelectedValue != null ? this.ObtenerCoder(this.cmbCausa, this.cmbMotivo.SelectedValue.ToString()) : (Coder)null;
                pCapturedPerson.RecordData.Crime = coder8;
                pCapturedPerson.RecordData.Complexion = this.cmbComplexion.SelectedValue != null ? this.cmbComplexion.SelectedValue.ToString() : (string)null;
                pCapturedPerson.RecordData.Weigth = Convert.ToInt32(this.numPeso.Value);
                pCapturedPerson.RecordData.BodySize = Convert.ToInt32(this.numEstatura.Value);
                pCapturedPerson.RecordData.Foot = Convert.ToInt32(this.numPie.Value);
                pCapturedPerson.RecordData.CulturalLevel = this.cmbNivelCultural.SelectedValue != null ? this.cmbNivelCultural.SelectedValue.ToString() : (string)null;
                pCapturedPerson.RecordData.DescriptiveData = new DescriptiveData()
                {
                    Hair = new Hair()
                    {
                        Type = this.cmbPeloTipo.SelectedValue != null ? this.cmbPeloTipo.SelectedValue.ToString() : (string)null,
                        Color = this.cmbPeloColor.SelectedValue != null ? this.cmbPeloColor.SelectedValue.ToString() : (string)null
                    },
                    Nose = new Nose()
                    {
                        Width = this.cmbNarizAncho.SelectedValue != null ? this.cmbNarizAncho.SelectedValue.ToString() : (string)null,
                        Tip = this.cmbNarizPunta.SelectedValue != null ? this.cmbNarizPunta.SelectedValue.ToString() : (string)null
                    },
                    Face = new Face()
                    {
                        BrowDimension = this.cmbRostroFrente.SelectedValue != null ? this.cmbRostroFrente.SelectedValue.ToString() : (string)null,
                        Proportion = this.cmbRostro.SelectedValue != null ? this.cmbRostro.SelectedValue.ToString() : (string)null
                    },
                    Eyebrow = new Eyebrow()
                    {
                        Shape = this.cmbCejasForma.SelectedValue != null ? this.cmbCejasForma.SelectedValue.ToString() : (string)null,
                        Population = this.cmbCejasPoblacion.SelectedValue != null ? this.cmbCejasPoblacion.SelectedValue.ToString() : (string)null
                    },
                    Mouth = new Mouth()
                    {
                        Dimension = this.cmbBocaDimension.SelectedValue != null ? this.cmbBocaDimension.SelectedValue.ToString() : (string)null,
                        Type = this.cmbBocaTipo.SelectedValue != null ? this.cmbBocaTipo.SelectedValue.ToString() : (string)null
                    },
                    Facial = new Facial()
                    {
                        Cheeks = this.cmbFacialesBarbilla.SelectedValue != null ? this.cmbFacialesBarbilla.SelectedValue.ToString() : (string)null,
                        Chin = this.cmbFacialesMejillas.SelectedValue != null ? this.cmbFacialesMejillas.SelectedValue.ToString() : (string)null,
                        NasalGroove = this.cmbFacialesSurcoNasal.SelectedValue != null ? this.cmbFacialesSurcoNasal.SelectedValue.ToString() : (string)null
                    },
                    Eyes = new Eyes()
                    {
                        Shape = this.cmbOjosForma.SelectedValue != null ? this.cmbOjosForma.SelectedValue.ToString() : (string)null,
                        Color = this.cmbOjosColor.SelectedValue != null ? this.cmbOjosColor.SelectedValue.ToString() : (string)null
                    },
                    Neck = new Neck()
                    {
                        Width = this.cmbCuelloAncho.SelectedValue != null ? this.cmbCuelloAncho.SelectedValue.ToString() : (string)null,
                        Long = this.cmbCuelloAlto.SelectedValue != null ? this.cmbCuelloAlto.SelectedValue.ToString() : (string)null
                    },
                    HeadShape = this.cmbCabezaForma.SelectedValue != null ? this.cmbCabezaForma.SelectedValue.ToString() : (string)null
                };
                pCapturedPerson.RecordData.CharacteristicData = new CharacteristicData()
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
                pCapturedPerson.RecordData.FeatureData = new FeatureData()
                {
                    Walk = this.ObtenerListaCoder(this.chkAlAndar, "WALK"),
                    Talk = this.ObtenerListaCoder(this.chkAlHablar, "TALK"),
                    Behavior = this.ObtenerListaCoder(this.chkDeLaConducta, "BEHAVIOR"),
                    Absent = this.ObtenerListaCoder(this.chkPartesAusentes, "ABSENT"),
                    Special = this.ObtenerListaCoder(this.chkHabilidadesEsp, "SPECIAL"),
                    Language = this.ObtenerListaCoder(this.chkIdiomas, "LANGUAGE")
                };
                pCapturedPerson.RecordData.Addresses = this.direccion1.Direcciones;
                pCapturedPerson.RecordData.Alias = this.alias1.Nombres;
                vEnroll.PersonaCapturada = pCapturedPerson;
                new HelperSerializer().SerializeEpd(pCapturedPerson, vContainerMain.RutaEpd);
                this.Alerta("Mensaje", "Se guardaron correctamente los registros", false);
            }
            catch
            {
                this.Alerta("Mensaje", "Problemas al guardar, comuniquese con el administrador del sistema", false);
            }
        }

        private BindingList<Coder> ObtenerListaCoder(
          CheckedListBox pChkListBox,
          string pGrupo)
        {
            BindingList<Coder> bindingList = new BindingList<Coder>();
            Coder coder1 = new Coder();
            List<Coder> source = HelperLoadControl.ObtenerLista(pGrupo);
            foreach (object checkedItem in pChkListBox.CheckedItems)
            {
                object item = checkedItem;
                Coder coder2 = source.Where<Coder>((Func<Coder, bool>)(cust => cust.Value == item.ToString())).FirstOrDefault<Coder>();
                if (coder2 != null)
                    bindingList.Add(new Coder()
                    {
                        CoderTypeId = coder2.CoderTypeId,
                        Id = coder2.Id,
                        Value = coder2.Value
                    });
            }
            return bindingList;
        }

        private Coder ObtenerCoder(ComboBox pCmb, string pCoderTypeId)
        {
            Coder coder = new Coder();
            if (pCmb.SelectedValue != null)
                coder = new Coder()
                {
                    CoderTypeId = pCoderTypeId,
                    Id = pCmb.SelectedValue.ToString(),
                    Value = pCmb.Text
                };
            return coder;
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.tabDatosBiograficos.SelectedIndex == 1)
                    this.btnAnterior.Enabled = false;
                this.tabDatosBiograficos.SelectTab(this.tabDatosBiograficos.SelectedIndex - 1);
                this.btnsiguiente.Enabled = true;
                this.btnsiguiente.Text = "Siguiente";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void btnsiguiente_Click(object sender, EventArgs e)
        {
            try
            {
                switch (this.tabDatosBiograficos.SelectedIndex)
                {
                    case 0:
                        if (!this.ValidaModeloDatosIdentificativos())
                            break;
                        this.btnAnterior.Enabled = true;
                        this.btnsiguiente.Enabled = this.ValidaModeloDatosDescriptivos();
                        break;
                    case 1:
                        if (!this.ValidaModeloDatosDescriptivos())
                            break;
                        this.tabDatosBiograficos.SelectTab(2);
                        this.btnAnterior.Enabled = true;
                        break;
                    case 2:
                        this.tabDatosBiograficos.SelectTab(3);
                        this.btnAnterior.Enabled = true;
                        this.btnsiguiente.Text = "Finalizar";
                        break;
                    case 3:
                        this.ActualizarCapturedPerson();
                        this.Close();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void txtIdentificacion_TextChanged(object sender, EventArgs e)
        {
            this.ValidaModeloDatosIdentificativos();
        }

        private void cmbMotivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string selectedValue = (string)((ListControl)sender).SelectedValue;
                if (!string.IsNullOrWhiteSpace(selectedValue))
                {
                    this.label2.Visible = true;
                    this.cmbCausa.Visible = true;
                    HelperLoadControl.Combo(this.cmbCausa, HelperLoadControl.ObtenerSubLista("MOTIVEENROLL", selectedValue, true), true);
                }
                else
                {
                    this.label2.Visible = false;
                    this.cmbCausa.Visible = false;
                }
                this.ValidaModeloDatosIdentificativos();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void cmbCausa_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ValidaModeloDatosIdentificativos();
        }

        private void cmbTipoPersona_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch ((string)((ListControl)sender).SelectedValue)
                {
                    case "NARE":
                        this.lblDepartamento.Visible = true;
                        this.lblProvincia.Visible = true;
                        this.cmbDepartamento.Visible = true;
                        this.cmbProvincia.Visible = true;
                        this.cmbPais.SelectedValue = (object)"BOL";
                        if (this.txtIdentificacion.Text == "INDOCUMENTADO")
                        {
                            this.txtIdentificacion.Text = this._numeroDoc;
                            this.txtComplemento.Text = this._complemento;
                        }
                        this.txtComplemento.Enabled = this.txtIdentificacion.Enabled = true;
                        break;
                    case "NANORE":
                        this.lblDepartamento.Visible = true;
                        this.lblProvincia.Visible = true;
                        this.cmbDepartamento.Visible = true;
                        this.cmbProvincia.Visible = true;
                        this.cmbPais.SelectedValue = (object)"BOL";
                        if (this.txtIdentificacion.Text == "INDOCUMENTADO")
                        {
                            this.txtIdentificacion.Text = this._numeroDoc;
                            this.txtComplemento.Text = this._complemento;
                        }
                        this.txtComplemento.Enabled = this.txtIdentificacion.Enabled = true;
                        break;
                    case "INDOC":
                        this.lblDepartamento.Visible = false;
                        this.lblProvincia.Visible = false;
                        this.cmbDepartamento.Visible = false;
                        this.cmbProvincia.Visible = false;
                        this.cmbPais.SelectedValue = (object)"NOINF";
                        this._numeroDoc = this.txtIdentificacion.Text.Trim();
                        this._complemento = this.txtComplemento.Text.Trim();
                        this.txtIdentificacion.Text = "INDOCUMENTADO";
                        this.txtComplemento.Text = "";
                        this.txtComplemento.Enabled = this.txtIdentificacion.Enabled = false;
                        break;
                    case "EXRE":
                        this.lblDepartamento.Visible = false;
                        this.lblProvincia.Visible = false;
                        this.cmbDepartamento.Visible = false;
                        this.cmbProvincia.Visible = false;
                        this.cmbPais.SelectedValue = (object)"";
                        if (this.txtIdentificacion.Text == "INDOCUMENTADO")
                        {
                            this.txtIdentificacion.Text = this._numeroDoc;
                            this.txtComplemento.Text = this._complemento;
                        }
                        this.txtComplemento.Enabled = this.txtIdentificacion.Enabled = true;
                        break;
                    default:
                        this.lblDepartamento.Visible = false;
                        this.lblProvincia.Visible = false;
                        this.cmbDepartamento.Visible = false;
                        this.cmbProvincia.Visible = false;
                        this.cmbPais.SelectedValue = (object)"";
                        if (this.txtIdentificacion.Text == "INDOCUMENTADO")
                        {
                            this.txtIdentificacion.Text = this._numeroDoc;
                            this.txtComplemento.Text = this._complemento;
                        }
                        this.txtComplemento.Enabled = this.txtIdentificacion.Enabled = true;
                        break;
                }
                this.ValidaModeloDatosIdentificativos();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void cmbPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string selectedValue = (string)((ListControl)sender).SelectedValue;
                List<Coder> pColCoder = new List<Coder>();
                if (selectedValue == "BOL")
                    pColCoder = HelperLoadControl.ObtenerLista("PROV");
                HelperLoadControl.Combo(this.cmbDepartamento, pColCoder, true);
                this.ValidaModeloDatosIdentificativos();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void cmbDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string selectedValue = (string)((ListControl)sender).SelectedValue;
                List<Coder> pColCoder = new List<Coder>();
                if (!string.IsNullOrWhiteSpace(selectedValue))
                    pColCoder = HelperLoadControl.ObtenerSubLista("PROV", selectedValue);
                HelperLoadControl.Combo(this.cmbProvincia, pColCoder, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void txtPrimerNombre_TextChanged(object sender, EventArgs e)
        {
            this.ValidaModeloDatosIdentificativos();
        }

        private void cmbSexo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ValidaModeloDatosIdentificativos();
        }

        private void cmbComplexion_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ValidaModeloDatosIdentificativos();
        }

        private void numPeso_ValueChanged(object sender, EventArgs e)
        {
            this.ValidaModeloDatosIdentificativos();
        }

        private void numEstatura_ValueChanged(object sender, EventArgs e)
        {
            this.ValidaModeloDatosIdentificativos();
        }

        private void numPie_ValueChanged(object sender, EventArgs e)
        {
            this.ValidaModeloDatosIdentificativos();
        }

        private void cmbNivelCultural_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ValidaModeloDatosIdentificativos();
        }

        private void cmbPeloTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ValidaModeloDatosDescriptivos();
        }

        private void cmbPeloColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ValidaModeloDatosDescriptivos();
        }

        private void cmbNarizAncho_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ValidaModeloDatosDescriptivos();
        }

        private void cmbNarizPunta_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ValidaModeloDatosDescriptivos();
        }

        private void cmbRostroFrente_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ValidaModeloDatosDescriptivos();
        }

        private void cmbRostro_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ValidaModeloDatosDescriptivos();
        }

        private void cmbCejasForma_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ValidaModeloDatosDescriptivos();
        }

        private void cmbCejasPoblacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ValidaModeloDatosDescriptivos();
        }

        private void cmbBocaDimension_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ValidaModeloDatosDescriptivos();
        }

        private void cmbBocaTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ValidaModeloDatosDescriptivos();
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

        private void cmbOjosColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ValidaModeloDatosDescriptivos();
        }

        private void cmbOjosForma_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ValidaModeloDatosDescriptivos();
        }

        private void cmbCuelloAncho_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ValidaModeloDatosDescriptivos();
        }

        private void cmbCuelloAlto_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ValidaModeloDatosDescriptivos();
        }

        private void cmbCabezaForma_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ValidaModeloDatosDescriptivos();
        }

        private bool ValidaModeloDatosIdentificativos()
        {
            try
            {
                return this.btnsiguiente.Enabled;
            }
            catch
            {
                return false;
            }
        }

        private bool ValidaModeloDatosDescriptivos()
        {
            try
            {
                return this.btnsiguiente.Enabled;
            }
            catch
            {
                return false;
            }
        }

        private void txtNombreMadre_TextChanged(object sender, EventArgs e)
        {
        }

        private void vBiography_DoubleClick(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ActualizarCapturedPerson();
                Cursor.Current = Cursors.Default;
                this.Alerta("Mensaje", "Se guardaron los datos satisfactoriamente", false);
            }
            catch
            {
            }
        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            if (!this.Alerta("Mensaje", "¿ Esta seguro de Salir ? , puede que exista datos sin ser guardados", true))
                return;
            vEnroll vEnroll = new vEnroll();
            vEnroll.MdiParent = this.ParentForm;
            vEnroll.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!this.Alerta("Mensaje", "Se guardaran los datos y se saldra del formulario,\nEsta seguro de realizar esta operacion ? ", true))
                return;
            Cursor.Current = Cursors.WaitCursor;
            this.ActualizarCapturedPerson();
            Cursor.Current = Cursors.Default;
            vEnroll vEnroll = new vEnroll();
            vEnroll.MdiParent = this.ParentForm;
            vEnroll.Show();
            this.Close();
        }

        private bool Alerta(string titulo, string mensaje, bool confirmacion)
        {
            bool flag = false;
            if (confirmacion)
            {
                flag = MessageBox.Show(mensaje, titulo, MessageBoxButtons.YesNo).ToString() == "Yes";
            }
            else
            {
                int num = (int)MessageBox.Show(mensaje, titulo);
            }
            return flag;
        }

        private void vBiography_Load(object sender, EventArgs e)
        {
            this.ActiveControl = (System.Windows.Forms.Control)this.cmbMotivo;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void txtCodigoGenetico_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtCodigoGenetico_TypeValidationCompleted(object sender, TypeValidationEventArgs e)
        {
            if (!e.IsValidInput)
            {
                this.toolTip1.ToolTipTitle = "Fecha invalida";
                this.toolTip1.Show("El formato de la fecha no es válido dd/mm/yyyy.", (IWin32Window)this.txtCodigoGenetico, this.txtCodigoGenetico.Width, -40, 5000);
                this.txtCodigoGenetico.Text = "";
                this.ActiveControl = (System.Windows.Forms.Control)this.txtCodigoGenetico;
            }
            else
            {
                DateTime returnValue = (DateTime)e.ReturnValue;
                if (returnValue > DateTime.Now)
                {
                    this.toolTip1.ToolTipTitle = "Fecha invalida";
                    this.toolTip1.Show("La fecha no puede ser mayor a la fecha actual.", (IWin32Window)this.txtCodigoGenetico, this.txtCodigoGenetico.Width, -40, 5000);
                    this.txtCodigoGenetico.Text = "";
                    this.ActiveControl = (System.Windows.Forms.Control)this.txtCodigoGenetico;
                    e.Cancel = true;
                }
                if (returnValue < DateTime.Now.AddYears(-100))
                {
                    this.toolTip1.ToolTipTitle = "Fecha invalida";
                    this.toolTip1.Show("La fecha no puede ser menor a: " + DateTime.Now.AddYears(-100).ToString("dd/mm/yyyy"), (IWin32Window)this.txtCodigoGenetico, this.txtCodigoGenetico.Width, -40, 5000);
                    this.txtCodigoGenetico.Text = "";
                    this.ActiveControl = (System.Windows.Forms.Control)this.txtCodigoGenetico;
                    e.Cancel = true;
                }
            }
        }

        private void txtCodigoGenetico_KeyDown(object sender, KeyEventArgs e)
        {
            this.toolTip1.Hide((IWin32Window)this.txtCodigoGenetico);
        }

        private void txtPrimerNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) || e.KeyChar == '\b')
                return;
            int num = (int)MessageBox.Show("No se permiten Numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            e.Handled = true;
        }

        private void txtPrimerApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) || e.KeyChar == '\b')
                return;
            int num = (int)MessageBox.Show("No se permiten Numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            e.Handled = true;
        }

        private void txtSegundoNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) || e.KeyChar == '\b')
                return;
            int num = (int)MessageBox.Show("No se permiten Numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            e.Handled = true;
        }

        private void txtSegundoApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) || e.KeyChar == '\b')
                return;
            int num = (int)MessageBox.Show("No se permiten Numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            e.Handled = true;
        }

        private void txtNombrePadre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) || e.KeyChar == '\b')
                return;
            int num = (int)MessageBox.Show("No se permiten Numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            e.Handled = true;
        }

        private void txtNombreMadre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) || e.KeyChar == '\b')
                return;
            int num = (int)MessageBox.Show("No se permiten Numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            e.Handled = true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = (IContainer)new Container();
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(vBiography));
            this.btncancelar = new Button();
            this.btnsiguiente = new Button();
            this.btnguardar = new Button();
            this.errorProvedor = new ErrorProvider(this.components);
            this.btnAyuda = new Button();
            this.panel1 = new Panel();
            this.button1 = new Button();
            this.btnAnterior = new Button();
            this.label7 = new Label();
            this.txtNombreMadre = new TextBox();
            this.txtIdentificacion = new TextBox();
            this.label8 = new Label();
            this.txtPrimerNombre = new TextBox();
            this.txtSegundoApellido = new TextBox();
            this.label9 = new Label();
            this.txtPrimerApellido = new TextBox();
            this.label12 = new Label();
            this.label10 = new Label();
            this.txtSegundoNombre = new TextBox();
            this.txtNombrePadre = new TextBox();
            this.label13 = new Label();
            this.cmbProvincia = new ComboBox();
            this.cmbDepartamento = new ComboBox();
            this.cmbPais = new ComboBox();
            this.lblProvincia = new Label();
            this.lblDepartamento = new Label();
            this.lblPais = new Label();
            this.cmbComplexion = new ComboBox();
            this.cmbColorPiel = new ComboBox();
            this.cmbSexo = new ComboBox();
            this.numPie = new NumericUpDown();
            this.numEstatura = new NumericUpDown();
            this.numPeso = new NumericUpDown();
            this.label5 = new Label();
            this.label14 = new Label();
            this.label15 = new Label();
            this.label16 = new Label();
            this.label17 = new Label();
            this.label18 = new Label();
            this.cmbTipoPersona = new ComboBox();
            this.cmbCausa = new ComboBox();
            this.cmbMotivo = new ComboBox();
            this.label3 = new Label();
            this.label2 = new Label();
            this.label1 = new Label();
            this.tableLayoutPanel1 = new TableLayoutPanel();
            this.label6 = new Label();
            this.txtCodigoGenetico = new MaskedTextBox();
            this.label42 = new Label();
            this.tableLayoutPanel2 = new TableLayoutPanel();
            this.tableLayoutPanel4 = new TableLayoutPanel();
            this.label35 = new Label();
            this.tableLayoutPanel5 = new TableLayoutPanel();
            this.label11 = new Label();
            this.lblComplemento = new Label();
            this.txtComplemento = new TextBox();
            this.tableLayoutPanel8 = new TableLayoutPanel();
            this.direccion1 = new Direccion();
            this.tableLayoutPanel3 = new TableLayoutPanel();
            this.tabDatosBiograficos = new TabControl();
            this.tabPage3 = new TabPage();
            this.groupControl2 = new GroupBox();
            this.tabControl1 = new TabControl();
            this.tabPage1 = new TabPage();
            this.tabPage2 = new TabPage();
            this.groupControl7 = new GroupBox();
            this.nacionalidades1 = new Nacionalidades();
            this.alias1 = new Alias();
            this.cmbNivelCultural = new ComboBox();
            this.label19 = new Label();
            this.label23 = new Label();
            this.groupControl1 = new GroupBox();
            this.tabPage4 = new TabPage();
            this.lbltitulo2 = new Label();
            this.groupControl14 = new GroupBox();
            this.cmbCabezaForma = new ComboBox();
            this.label36 = new Label();
            this.groupControl8 = new GroupBox();
            this.cmbPeloColor = new ComboBox();
            this.cmbPeloTipo = new ComboBox();
            this.label22 = new Label();
            this.label21 = new Label();
            this.groupControl11 = new GroupBox();
            this.cmbFacialesSurcoNasal = new ComboBox();
            this.cmbFacialesMejillas = new ComboBox();
            this.cmbFacialesBarbilla = new ComboBox();
            this.label34 = new Label();
            this.label28 = new Label();
            this.label29 = new Label();
            this.groupControl9 = new GroupBox();
            this.cmbNarizPunta = new ComboBox();
            this.cmbNarizAncho = new ComboBox();
            this.label24 = new Label();
            this.label25 = new Label();
            this.groupControl15 = new GroupBox();
            this.cmbCuelloAlto = new ComboBox();
            this.cmbCuelloAncho = new ComboBox();
            this.label37 = new Label();
            this.label38 = new Label();
            this.groupControl13 = new GroupBox();
            this.cmbCejasPoblacion = new ComboBox();
            this.cmbCejasForma = new ComboBox();
            this.label32 = new Label();
            this.label33 = new Label();
            this.groupControl16 = new GroupBox();
            this.cmbOjosForma = new ComboBox();
            this.cmbOjosColor = new ComboBox();
            this.label39 = new Label();
            this.label40 = new Label();
            this.groupControl12 = new GroupBox();
            this.cmbBocaTipo = new ComboBox();
            this.cmbBocaDimension = new ComboBox();
            this.label30 = new Label();
            this.label31 = new Label();
            this.groupControl10 = new GroupBox();
            this.cmbRostro = new ComboBox();
            this.cmbRostroFrente = new ComboBox();
            this.label26 = new Label();
            this.label27 = new Label();
            this.tabPage5 = new TabPage();
            this.lbltitulo3 = new Label();
            this.groupControl20 = new GroupBox();
            this.chkPiesPiernas = new CheckedListBox();
            this.groupControl19 = new GroupBox();
            this.chkPiel = new CheckedListBox();
            this.groupControl24 = new GroupBox();
            this.chkOrejas = new CheckedListBox();
            this.groupControl18 = new GroupBox();
            this.chkPecho = new CheckedListBox();
            this.groupControl17 = new GroupBox();
            this.chkEspalda = new CheckedListBox();
            this.groupControl22 = new GroupBox();
            this.chkDedosManos = new CheckedListBox();
            this.groupControl25 = new GroupBox();
            this.chkDientes = new CheckedListBox();
            this.groupControl21 = new GroupBox();
            this.chkBrazos = new CheckedListBox();
            this.tabPage6 = new TabPage();
            this.groupControl23 = new GroupBox();
            this.chkIdiomas = new CheckedListBox();
            this.groupControl30 = new GroupBox();
            this.chkAlAndar = new CheckedListBox();
            this.lblTitulo4 = new Label();
            this.groupControl26 = new GroupBox();
            this.chkDeLaConducta = new CheckedListBox();
            this.groupControl29 = new GroupBox();
            this.chkAlHablar = new CheckedListBox();
            this.groupControl27 = new GroupBox();
            this.chkHabilidadesEsp = new CheckedListBox();
            this.groupControl28 = new GroupBox();
            this.chkPartesAusentes = new CheckedListBox();
            this.tableLayoutPanel10 = new TableLayoutPanel();
            this.tableLayoutPanel11 = new TableLayoutPanel();
            this.label4 = new Label();
            this.tableLayoutPanel12 = new TableLayoutPanel();
            this.pictureBox1 = new PictureBox();
            this.tableLayoutPanel6 = new TableLayoutPanel();
            this.label20 = new Label();
            this.tableLayoutPanel7 = new TableLayoutPanel();
            this.label41 = new Label();
            this.tableLayoutPanel9 = new TableLayoutPanel();
            this.label43 = new Label();
            this.toolTip1 = new ToolTip(this.components);
            this.tableLayoutPanel14 = new TableLayoutPanel();
            ((ISupportInitialize)this.errorProvedor).BeginInit();
            this.panel1.SuspendLayout();
            this.numPie.BeginInit();
            this.numEstatura.BeginInit();
            this.numPeso.BeginInit();
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
            ((ISupportInitialize)this.pictureBox1).BeginInit();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.tableLayoutPanel14.SuspendLayout();
            this.SuspendLayout();
            this.btncancelar.BackColor = Color.White;
            this.btncancelar.Image = (Image)componentResourceManager.GetObject("btncancelar.Image");
            this.btncancelar.ImageAlign = ContentAlignment.TopCenter;
            this.btncancelar.Location = new Point(174, 16);
            this.btncancelar.Name = "btncancelar";
            this.btncancelar.Size = new Size(75, 54);
            this.btncancelar.TabIndex = 1;
            this.btncancelar.Text = "Salir";
            this.btncancelar.TextAlign = ContentAlignment.BottomCenter;
            this.btncancelar.UseVisualStyleBackColor = false;
            this.btncancelar.Click += new EventHandler(this.btncancelar_Click);
            this.btnsiguiente.BackColor = Color.White;
            this.btnsiguiente.Image = (Image)componentResourceManager.GetObject("btnsiguiente.Image");
            this.btnsiguiente.ImageAlign = ContentAlignment.TopCenter;
            this.btnsiguiente.Location = new Point(336, 16);
            this.btnsiguiente.Name = "btnsiguiente";
            this.btnsiguiente.Size = new Size(75, 54);
            this.btnsiguiente.TabIndex = 2;
            this.btnsiguiente.Text = "Siguiente";
            this.btnsiguiente.TextAlign = ContentAlignment.BottomCenter;
            this.btnsiguiente.UseVisualStyleBackColor = false;
            this.btnsiguiente.Visible = false;
            this.btnsiguiente.Click += new EventHandler(this.btnsiguiente_Click);
            this.btnguardar.BackColor = Color.White;
            this.btnguardar.Image = (Image)componentResourceManager.GetObject("btnguardar.Image");
            this.btnguardar.ImageAlign = ContentAlignment.TopCenter;
            this.btnguardar.Location = new Point(3, 16);
            this.btnguardar.Name = "btnguardar";
            this.btnguardar.Size = new Size(75, 54);
            this.btnguardar.TabIndex = 3;
            this.btnguardar.Text = "Guardar";
            this.btnguardar.TextAlign = ContentAlignment.BottomCenter;
            this.btnguardar.UseVisualStyleBackColor = false;
            this.btnguardar.Click += new EventHandler(this.btnguardar_Click);
            this.errorProvedor.ContainerControl = (ContainerControl)this;
            this.btnAyuda.BackColor = Color.White;
            this.btnAyuda.Location = new Point(450, 26);
            this.btnAyuda.Name = "btnAyuda";
            this.btnAyuda.Size = new Size(75, 43);
            this.btnAyuda.TabIndex = 4;
            this.btnAyuda.Text = "Ayuda";
            this.btnAyuda.UseVisualStyleBackColor = false;
            this.btnAyuda.Visible = false;
            this.panel1.Anchor = AnchorStyles.Left;
            this.panel1.BackColor = Color.FromArgb(48, 63, 105);
            this.panel1.Controls.Add((System.Windows.Forms.Control)this.button1);
            this.panel1.Controls.Add((System.Windows.Forms.Control)this.btnAnterior);
            this.panel1.Controls.Add((System.Windows.Forms.Control)this.btnguardar);
            this.panel1.Controls.Add((System.Windows.Forms.Control)this.btnsiguiente);
            this.panel1.Controls.Add((System.Windows.Forms.Control)this.btncancelar);
            this.panel1.Controls.Add((System.Windows.Forms.Control)this.btnAyuda);
            this.panel1.Location = new Point(3, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size((int)byte.MaxValue, 72);
            this.panel1.TabIndex = 23;
            this.button1.BackColor = Color.White;
            this.button1.Image = (Image)componentResourceManager.GetObject("button1.Image");
            this.button1.ImageAlign = ContentAlignment.TopCenter;
            this.button1.Location = new Point(84, 15);
            this.button1.Name = "button1";
            this.button1.Size = new Size(84, 54);
            this.button1.TabIndex = 7;
            this.button1.Text = "Guardar y Salir";
            this.button1.TextAlign = ContentAlignment.BottomCenter;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.btnAnterior.BackColor = Color.White;
            this.btnAnterior.Enabled = false;
            this.btnAnterior.Image = (Image)componentResourceManager.GetObject("btnAnterior.Image");
            this.btnAnterior.ImageAlign = ContentAlignment.TopCenter;
            this.btnAnterior.Location = new Point((int)byte.MaxValue, 16);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new Size(75, 54);
            this.btnAnterior.TabIndex = 1;
            this.btnAnterior.Text = "Anterior";
            this.btnAnterior.TextAlign = ContentAlignment.BottomCenter;
            this.btnAnterior.UseVisualStyleBackColor = false;
            this.btnAnterior.Visible = false;
            this.btnAnterior.Click += new EventHandler(this.btnAnterior_Click);
            this.label7.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.BackColor = Color.Transparent;
            this.label7.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.label7.ForeColor = Color.White;
            this.label7.Location = new Point(115, 6);
            this.label7.Name = "label7";
            this.label7.Size = new Size(138, 15);
            this.label7.TabIndex = 0;
            this.label7.Text = "Identificacion:";
            this.txtNombreMadre.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.txtNombreMadre.CharacterCasing = CharacterCasing.Upper;
            this.txtNombreMadre.Location = new Point(635, 86);
            this.txtNombreMadre.Name = "txtNombreMadre";
            this.txtNombreMadre.Size = new Size(218, 20);
            this.txtNombreMadre.TabIndex = 25;
            this.txtNombreMadre.TextChanged += new EventHandler(this.txtNombreMadre_TextChanged);
            this.txtNombreMadre.KeyPress += new KeyPressEventHandler(this.txtNombreMadre_KeyPress);
            this.txtIdentificacion.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.txtIdentificacion.CharacterCasing = CharacterCasing.Upper;
            this.txtIdentificacion.Location = new Point(259, 3);
            this.txtIdentificacion.Name = "txtIdentificacion";
            this.txtIdentificacion.Size = new Size(219, 20);
            this.txtIdentificacion.TabIndex = 18;
            this.txtIdentificacion.TextChanged += new EventHandler(this.txtIdentificacion_TextChanged);
            this.label8.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.label8.AutoSize = true;
            this.label8.BackColor = Color.Transparent;
            this.label8.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.label8.ForeColor = Color.White;
            this.label8.Location = new Point(115, 33);
            this.label8.Name = "label8";
            this.label8.Size = new Size(138, 15);
            this.label8.TabIndex = 2;
            this.label8.Text = "Primer Nombre:";
            this.txtPrimerNombre.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.txtPrimerNombre.CharacterCasing = CharacterCasing.Upper;
            this.txtPrimerNombre.Location = new Point(259, 30);
            this.txtPrimerNombre.Name = "txtPrimerNombre";
            this.txtPrimerNombre.Size = new Size(219, 20);
            this.txtPrimerNombre.TabIndex = 20;
            this.txtPrimerNombre.TextChanged += new EventHandler(this.txtPrimerNombre_TextChanged);
            this.txtPrimerNombre.KeyPress += new KeyPressEventHandler(this.txtPrimerNombre_KeyPress);
            this.txtSegundoApellido.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.txtSegundoApellido.CharacterCasing = CharacterCasing.Upper;
            this.txtSegundoApellido.Location = new Point(635, 57);
            this.txtSegundoApellido.Name = "txtSegundoApellido";
            this.txtSegundoApellido.Size = new Size(218, 20);
            this.txtSegundoApellido.TabIndex = 23;
            this.txtSegundoApellido.KeyPress += new KeyPressEventHandler(this.txtSegundoApellido_KeyPress);
            this.label9.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.label9.AutoSize = true;
            this.label9.BackColor = Color.Transparent;
            this.label9.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.label9.ForeColor = Color.White;
            this.label9.Location = new Point(115, 60);
            this.label9.Name = "label9";
            this.label9.Size = new Size(138, 15);
            this.label9.TabIndex = 4;
            this.label9.Text = "Primer Apellido:";
            this.txtPrimerApellido.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.txtPrimerApellido.CharacterCasing = CharacterCasing.Upper;
            this.txtPrimerApellido.Location = new Point(259, 57);
            this.txtPrimerApellido.Name = "txtPrimerApellido";
            this.txtPrimerApellido.Size = new Size(219, 20);
            this.txtPrimerApellido.TabIndex = 22;
            this.txtPrimerApellido.KeyPress += new KeyPressEventHandler(this.txtPrimerApellido_KeyPress);
            this.label12.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.label12.AutoSize = true;
            this.label12.BackColor = Color.Transparent;
            this.label12.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.label12.ForeColor = Color.White;
            this.label12.Location = new Point(484, 60);
            this.label12.Name = "label12";
            this.label12.Padding = new Padding(20, 0, 0, 0);
            this.label12.Size = new Size(145, 15);
            this.label12.TabIndex = 10;
            this.label12.Text = "Segundo Apellido:";
            this.label10.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.label10.AutoSize = true;
            this.label10.BackColor = Color.Transparent;
            this.label10.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.label10.ForeColor = Color.White;
            this.label10.Location = new Point(115, 88);
            this.label10.Name = "label10";
            this.label10.Size = new Size(138, 15);
            this.label10.TabIndex = 6;
            this.label10.Text = "Nombre del Padre:";
            this.txtSegundoNombre.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.txtSegundoNombre.CharacterCasing = CharacterCasing.Upper;
            this.txtSegundoNombre.Location = new Point(635, 30);
            this.txtSegundoNombre.Name = "txtSegundoNombre";
            this.txtSegundoNombre.Size = new Size(218, 20);
            this.txtSegundoNombre.TabIndex = 21;
            this.txtSegundoNombre.KeyPress += new KeyPressEventHandler(this.txtSegundoNombre_KeyPress);
            this.txtNombrePadre.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.txtNombrePadre.CharacterCasing = CharacterCasing.Upper;
            this.txtNombrePadre.Location = new Point(259, 86);
            this.txtNombrePadre.Name = "txtNombrePadre";
            this.txtNombrePadre.Size = new Size(219, 20);
            this.txtNombrePadre.TabIndex = 24;
            this.txtNombrePadre.KeyPress += new KeyPressEventHandler(this.txtNombrePadre_KeyPress);
            this.label13.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.label13.AutoSize = true;
            this.label13.BackColor = Color.Transparent;
            this.label13.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.label13.ForeColor = Color.White;
            this.label13.Location = new Point(484, 33);
            this.label13.Name = "label13";
            this.label13.Padding = new Padding(20, 0, 0, 0);
            this.label13.Size = new Size(145, 15);
            this.label13.TabIndex = 8;
            this.label13.Text = "Segundo Nombre:";
            this.cmbProvincia.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.cmbProvincia.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cmbProvincia.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.cmbProvincia.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbProvincia.FormattingEnabled = true;
            this.cmbProvincia.Location = new Point(635, 32);
            this.cmbProvincia.Name = "cmbProvincia";
            this.cmbProvincia.Size = new Size(217, 21);
            this.cmbProvincia.TabIndex = 29;
            this.cmbDepartamento.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.cmbDepartamento.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cmbDepartamento.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.cmbDepartamento.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbDepartamento.FormattingEnabled = true;
            this.cmbDepartamento.Location = new Point(257, 32);
            this.cmbDepartamento.Name = "cmbDepartamento";
            this.cmbDepartamento.Size = new Size(222, 21);
            this.cmbDepartamento.TabIndex = 28;
            this.cmbDepartamento.SelectedIndexChanged += new EventHandler(this.cmbDepartamento_SelectedIndexChanged);
            this.cmbPais.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.cmbPais.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cmbPais.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.cmbPais.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbPais.FormattingEnabled = true;
            this.cmbPais.Location = new Point(635, 3);
            this.cmbPais.Name = "cmbPais";
            this.cmbPais.Size = new Size(217, 21);
            this.cmbPais.TabIndex = 27;
            this.cmbPais.SelectedIndexChanged += new EventHandler(this.cmbPais_SelectedIndexChanged);
            this.lblProvincia.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.lblProvincia.AutoSize = true;
            this.lblProvincia.BackColor = Color.Transparent;
            this.lblProvincia.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.lblProvincia.ForeColor = Color.White;
            this.lblProvincia.Location = new Point(485, 35);
            this.lblProvincia.Name = "lblProvincia";
            this.lblProvincia.Padding = new Padding(20, 0, 0, 0);
            this.lblProvincia.Size = new Size(144, 15);
            this.lblProvincia.TabIndex = 5;
            this.lblProvincia.Text = "Provincia:";
            this.lblDepartamento.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.lblDepartamento.AutoSize = true;
            this.lblDepartamento.BackColor = Color.Transparent;
            this.lblDepartamento.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.lblDepartamento.ForeColor = Color.White;
            this.lblDepartamento.Location = new Point(115, 35);
            this.lblDepartamento.Name = "lblDepartamento";
            this.lblDepartamento.Size = new Size(136, 15);
            this.lblDepartamento.TabIndex = 3;
            this.lblDepartamento.Text = "Departamento:";
            this.lblPais.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.lblPais.AutoSize = true;
            this.lblPais.BackColor = Color.Transparent;
            this.lblPais.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.lblPais.ForeColor = Color.White;
            this.lblPais.Location = new Point(485, 6);
            this.lblPais.Name = "lblPais";
            this.lblPais.Padding = new Padding(20, 0, 0, 0);
            this.lblPais.Size = new Size(144, 15);
            this.lblPais.TabIndex = 1;
            this.lblPais.Text = "Pais:";
            this.cmbComplexion.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.cmbComplexion.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cmbComplexion.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.cmbComplexion.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbComplexion.FormattingEnabled = true;
            this.cmbComplexion.Location = new Point(260, 55);
            this.cmbComplexion.Name = "cmbComplexion";
            this.cmbComplexion.Size = new Size(219, 21);
            this.cmbComplexion.TabIndex = 33;
            this.cmbComplexion.SelectedIndexChanged += new EventHandler(this.cmbComplexion_SelectedIndexChanged);
            this.cmbColorPiel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.cmbColorPiel.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cmbColorPiel.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.cmbColorPiel.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbColorPiel.FormattingEnabled = true;
            this.cmbColorPiel.Location = new Point(260, 29);
            this.cmbColorPiel.Name = "cmbColorPiel";
            this.cmbColorPiel.Size = new Size(219, 21);
            this.cmbColorPiel.TabIndex = 32;
            this.cmbSexo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.cmbSexo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cmbSexo.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.cmbSexo.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbSexo.FormattingEnabled = true;
            this.cmbSexo.Location = new Point(260, 3);
            this.cmbSexo.Name = "cmbSexo";
            this.cmbSexo.Size = new Size(219, 21);
            this.cmbSexo.TabIndex = 31;
            this.cmbSexo.SelectedIndexChanged += new EventHandler(this.cmbSexo_SelectedIndexChanged);
            this.numPie.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.numPie.Location = new Point(632, 55);
            this.numPie.Maximum = new Decimal(new int[4]
            {
        45,
        0,
        0,
        0
            });
            this.numPie.Minimum = new Decimal(new int[4]
            {
        9,
        0,
        0,
        0
            });
            this.numPie.Name = "numPie";
            this.numPie.Size = new Size(222, 20);
            this.numPie.TabIndex = 36;
            this.numPie.TextAlign = HorizontalAlignment.Right;
            this.numPie.Value = new Decimal(new int[4]
            {
        9,
        0,
        0,
        0
            });
            this.numPie.Visible = false;
            this.numPie.ValueChanged += new EventHandler(this.numPie_ValueChanged);
            this.numEstatura.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.numEstatura.Location = new Point(632, 29);
            this.numEstatura.Maximum = new Decimal(new int[4]
            {
        250,
        0,
        0,
        0
            });
            this.numEstatura.Minimum = new Decimal(new int[4]
            {
        49,
        0,
        0,
        0
            });
            this.numEstatura.Name = "numEstatura";
            this.numEstatura.Size = new Size(222, 20);
            this.numEstatura.TabIndex = 35;
            this.numEstatura.TextAlign = HorizontalAlignment.Right;
            this.numEstatura.Value = new Decimal(new int[4]
            {
        49,
        0,
        0,
        0
            });
            this.numEstatura.ValueChanged += new EventHandler(this.numEstatura_ValueChanged);
            this.numPeso.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.numPeso.Location = new Point(632, 3);
            this.numPeso.Maximum = new Decimal(new int[4]
            {
        250,
        0,
        0,
        0
            });
            this.numPeso.Minimum = new Decimal(new int[4]
            {
        39,
        0,
        0,
        0
            });
            this.numPeso.Name = "numPeso";
            this.numPeso.Size = new Size(222, 20);
            this.numPeso.TabIndex = 34;
            this.numPeso.TextAlign = HorizontalAlignment.Right;
            this.numPeso.Value = new Decimal(new int[4]
            {
        39,
        0,
        0,
        0
            });
            this.numPeso.ValueChanged += new EventHandler(this.numPeso_ValueChanged);
            this.label5.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.BackColor = Color.Transparent;
            this.label5.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.label5.ForeColor = Color.White;
            this.label5.Location = new Point(485, 52);
            this.label5.Name = "label5";
            this.label5.Padding = new Padding(20, 0, 0, 0);
            this.label5.Size = new Size(141, 26);
            this.label5.TabIndex = 12;
            this.label5.Text = "Dimension del Pie(cm):";
            this.label5.Visible = false;
            this.label14.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.label14.AutoSize = true;
            this.label14.BackColor = Color.Transparent;
            this.label14.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.label14.ForeColor = Color.White;
            this.label14.Location = new Point(115, 5);
            this.label14.Name = "label14";
            this.label14.Size = new Size(139, 15);
            this.label14.TabIndex = 2;
            this.label14.Text = "Sexo:";
            this.label15.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.label15.AutoSize = true;
            this.label15.BackColor = Color.Transparent;
            this.label15.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.label15.ForeColor = Color.White;
            this.label15.Location = new Point(115, 31);
            this.label15.Name = "label15";
            this.label15.Size = new Size(139, 15);
            this.label15.TabIndex = 4;
            this.label15.Text = "Color de Piel:";
            this.label16.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.label16.AutoSize = true;
            this.label16.BackColor = Color.Transparent;
            this.label16.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.label16.ForeColor = Color.White;
            this.label16.Location = new Point(485, 31);
            this.label16.Name = "label16";
            this.label16.Padding = new Padding(20, 0, 0, 0);
            this.label16.Size = new Size(141, 15);
            this.label16.TabIndex = 10;
            this.label16.Text = "Estatura (cm):";
            this.label17.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.label17.AutoSize = true;
            this.label17.BackColor = Color.Transparent;
            this.label17.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.label17.ForeColor = Color.White;
            this.label17.Location = new Point(115, 57);
            this.label17.Name = "label17";
            this.label17.Size = new Size(139, 15);
            this.label17.TabIndex = 6;
            this.label17.Text = "Complexion:";
            this.label18.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.label18.AutoSize = true;
            this.label18.BackColor = Color.Transparent;
            this.label18.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.label18.ForeColor = Color.White;
            this.label18.Location = new Point(485, 5);
            this.label18.Name = "label18";
            this.label18.Padding = new Padding(20, 0, 0, 0);
            this.label18.Size = new Size(141, 15);
            this.label18.TabIndex = 8;
            this.label18.Text = "Peso (kg):";
            this.cmbTipoPersona.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.cmbTipoPersona.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cmbTipoPersona.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.cmbTipoPersona.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbTipoPersona.FormattingEnabled = true;
            this.cmbTipoPersona.Location = new Point(259, 32);
            this.cmbTipoPersona.Name = "cmbTipoPersona";
            this.cmbTipoPersona.Size = new Size(219, 21);
            this.cmbTipoPersona.TabIndex = 17;
            this.cmbTipoPersona.SelectedIndexChanged += new EventHandler(this.cmbTipoPersona_SelectedIndexChanged);
            this.cmbCausa.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.cmbCausa.AutoCompleteMode = AutoCompleteMode.Suggest;
            this.cmbCausa.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.cmbCausa.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbCausa.FormattingEnabled = true;
            this.cmbCausa.Location = new Point(635, 3);
            this.cmbCausa.Name = "cmbCausa";
            this.cmbCausa.Size = new Size(220, 21);
            this.cmbCausa.TabIndex = 16;
            this.cmbCausa.Visible = false;
            this.cmbCausa.SelectedIndexChanged += new EventHandler(this.cmbCausa_SelectedIndexChanged);
            this.cmbMotivo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.cmbMotivo.AutoCompleteMode = AutoCompleteMode.Suggest;
            this.cmbMotivo.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.cmbMotivo.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbMotivo.FormattingEnabled = true;
            this.cmbMotivo.Location = new Point(259, 3);
            this.cmbMotivo.Name = "cmbMotivo";
            this.cmbMotivo.Size = new Size(219, 21);
            this.cmbMotivo.TabIndex = 15;
            this.cmbMotivo.SelectedIndexChanged += new EventHandler(this.cmbMotivo_SelectedIndexChanged);
            this.label3.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.BackColor = Color.Transparent;
            this.label3.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.label3.ForeColor = Color.White;
            this.label3.Location = new Point(115, 35);
            this.label3.Name = "label3";
            this.label3.Size = new Size(138, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Tipo de Persona:";
            this.label2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.BackColor = Color.Transparent;
            this.label2.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.label2.ForeColor = Color.White;
            this.label2.Location = new Point(484, 6);
            this.label2.Name = "label2";
            this.label2.Padding = new Padding(20, 0, 0, 0);
            this.label2.Size = new Size(145, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Causa:";
            this.label2.Visible = false;
            this.label1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.BackColor = Color.Transparent;
            this.label1.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.label1.ForeColor = Color.White;
            this.label1.Location = new Point(115, 6);
            this.label1.Name = "label1";
            this.label1.Size = new Size(138, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Motivo:";
            this.tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12f));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15.28525f));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24.43488f));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.14639f));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23.89666f));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8.61141f));
            this.tableLayoutPanel1.Controls.Add((System.Windows.Forms.Control)this.cmbProvincia, 4, 1);
            this.tableLayoutPanel1.Controls.Add((System.Windows.Forms.Control)this.lblProvincia, 3, 1);
            this.tableLayoutPanel1.Controls.Add((System.Windows.Forms.Control)this.cmbDepartamento, 2, 1);
            this.tableLayoutPanel1.Controls.Add((System.Windows.Forms.Control)this.lblDepartamento, 1, 1);
            this.tableLayoutPanel1.Controls.Add((System.Windows.Forms.Control)this.cmbPais, 4, 0);
            this.tableLayoutPanel1.Controls.Add((System.Windows.Forms.Control)this.lblPais, 3, 0);
            this.tableLayoutPanel1.Controls.Add((System.Windows.Forms.Control)this.label6, 1, 0);
            this.tableLayoutPanel1.Controls.Add((System.Windows.Forms.Control)this.txtCodigoGenetico, 2, 0);
            this.tableLayoutPanel1.Location = new Point(12, 389);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
            this.tableLayoutPanel1.Size = new Size(937, 57);
            this.tableLayoutPanel1.TabIndex = 20;
            this.label6.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.BackColor = Color.Transparent;
            this.label6.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.label6.ForeColor = Color.White;
            this.label6.Location = new Point(115, 0);
            this.label6.Name = "label6";
            this.label6.Size = new Size(136, 28);
            this.label6.TabIndex = 24;
            this.label6.Text = "Fecha de Nacimiento:";
            this.txtCodigoGenetico.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.txtCodigoGenetico.Location = new Point(257, 4);
            this.txtCodigoGenetico.Mask = "00/00/0000";
            this.txtCodigoGenetico.Name = "txtCodigoGenetico";
            this.txtCodigoGenetico.Size = new Size(222, 20);
            this.txtCodigoGenetico.TabIndex = 26;
            this.txtCodigoGenetico.ValidatingType = typeof(DateTime);
            this.txtCodigoGenetico.TypeValidationCompleted += new TypeValidationEventHandler(this.txtCodigoGenetico_TypeValidationCompleted);
            this.txtCodigoGenetico.KeyDown += new KeyEventHandler(this.txtCodigoGenetico_KeyDown);
            this.label42.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.label42.AutoSize = true;
            this.label42.BackColor = Color.Transparent;
            this.label42.BorderStyle = BorderStyle.Fixed3D;
            this.label42.Font = new Font("Microsoft Sans Serif", 11.25f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, (byte)0);
            this.label42.ForeColor = Color.White;
            this.label42.Location = new Point(115, 4);
            this.label42.Name = "label42";
            this.label42.Size = new Size(740, 20);
            this.label42.TabIndex = 26;
            this.label42.Text = "ENROLAMIENTO";
            this.tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12f));
            this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 79.76318f));
            this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8.288483f));
            this.tableLayoutPanel2.Controls.Add((System.Windows.Forms.Control)this.label42, 1, 0);
            this.tableLayoutPanel2.Location = new Point(12, 136);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel2.Size = new Size(937, 29);
            this.tableLayoutPanel2.TabIndex = 8;
            this.tableLayoutPanel4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12f));
            this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 79.76318f));
            this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8.288483f));
            this.tableLayoutPanel4.Controls.Add((System.Windows.Forms.Control)this.label35, 1, 0);
            this.tableLayoutPanel4.Location = new Point(12, 225);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel4.Size = new Size(937, 26);
            this.tableLayoutPanel4.TabIndex = 10;
            this.label35.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.label35.AutoSize = true;
            this.label35.BackColor = Color.Transparent;
            this.label35.BorderStyle = BorderStyle.Fixed3D;
            this.label35.Font = new Font("Microsoft Sans Serif", 11.25f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, (byte)0);
            this.label35.ForeColor = Color.White;
            this.label35.Location = new Point(115, 3);
            this.label35.Name = "label35";
            this.label35.Size = new Size(740, 20);
            this.label35.TabIndex = 26;
            this.label35.Text = "DATOS BASICOS";
            this.tableLayoutPanel5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.tableLayoutPanel5.ColumnCount = 6;
            this.tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12f));
            this.tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15.50054f));
            this.tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24.11195f));
            this.tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.25404f));
            this.tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24.0043f));
            this.tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8.503768f));
            this.tableLayoutPanel5.Controls.Add((System.Windows.Forms.Control)this.label7, 1, 0);
            this.tableLayoutPanel5.Controls.Add((System.Windows.Forms.Control)this.label8, 1, 1);
            this.tableLayoutPanel5.Controls.Add((System.Windows.Forms.Control)this.label9, 1, 2);
            this.tableLayoutPanel5.Controls.Add((System.Windows.Forms.Control)this.txtNombrePadre, 2, 3);
            this.tableLayoutPanel5.Controls.Add((System.Windows.Forms.Control)this.txtPrimerApellido, 2, 2);
            this.tableLayoutPanel5.Controls.Add((System.Windows.Forms.Control)this.txtPrimerNombre, 2, 1);
            this.tableLayoutPanel5.Controls.Add((System.Windows.Forms.Control)this.label10, 1, 3);
            this.tableLayoutPanel5.Controls.Add((System.Windows.Forms.Control)this.txtIdentificacion, 2, 0);
            this.tableLayoutPanel5.Controls.Add((System.Windows.Forms.Control)this.label11, 3, 3);
            this.tableLayoutPanel5.Controls.Add((System.Windows.Forms.Control)this.txtNombreMadre, 4, 3);
            this.tableLayoutPanel5.Controls.Add((System.Windows.Forms.Control)this.txtSegundoApellido, 4, 2);
            this.tableLayoutPanel5.Controls.Add((System.Windows.Forms.Control)this.label12, 3, 2);
            this.tableLayoutPanel5.Controls.Add((System.Windows.Forms.Control)this.label13, 3, 1);
            this.tableLayoutPanel5.Controls.Add((System.Windows.Forms.Control)this.txtSegundoNombre, 4, 1);
            this.tableLayoutPanel5.Controls.Add((System.Windows.Forms.Control)this.lblComplemento, 3, 0);
            this.tableLayoutPanel5.Controls.Add((System.Windows.Forms.Control)this.txtComplemento, 4, 0);
            this.tableLayoutPanel5.Location = new Point(12, 250);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 4;
            this.tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 25f));
            this.tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 25f));
            this.tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 25f));
            this.tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 25f));
            this.tableLayoutPanel5.Size = new Size(937, 111);
            this.tableLayoutPanel5.TabIndex = 19;
            this.label11.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.label11.BackColor = Color.Transparent;
            this.label11.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.label11.ForeColor = Color.White;
            this.label11.Location = new Point(484, 86);
            this.label11.Name = "label11";
            this.label11.Padding = new Padding(20, 0, 0, 0);
            this.label11.Size = new Size(145, 20);
            this.label11.TabIndex = 12;
            this.label11.Text = "Nombre de la Madre:";
            this.lblComplemento.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.lblComplemento.AutoSize = true;
            this.lblComplemento.BackColor = Color.Transparent;
            this.lblComplemento.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.lblComplemento.ForeColor = Color.White;
            this.lblComplemento.Location = new Point(484, 6);
            this.lblComplemento.Name = "lblComplemento";
            this.lblComplemento.Padding = new Padding(20, 0, 0, 0);
            this.lblComplemento.Size = new Size(145, 15);
            this.lblComplemento.TabIndex = 18;
            this.lblComplemento.Text = "Complemento:";
            this.txtComplemento.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.txtComplemento.CharacterCasing = CharacterCasing.Upper;
            this.txtComplemento.Location = new Point(635, 3);
            this.txtComplemento.Name = "txtComplemento";
            this.txtComplemento.Size = new Size(218, 20);
            this.txtComplemento.TabIndex = 19;
            this.tableLayoutPanel8.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.tableLayoutPanel8.ColumnCount = 3;
            this.tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.33374f));
            this.tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 79.08102f));
            this.tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8.503768f));
            this.tableLayoutPanel8.Controls.Add((System.Windows.Forms.Control)this.direccion1, 1, 0);
            this.tableLayoutPanel8.Controls.Add((System.Windows.Forms.Control)this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel8.Location = new Point(12, 475);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel8.Size = new Size(937, 148);
            this.tableLayoutPanel8.TabIndex = 21;
            this.direccion1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.direccion1.BackColor = Color.FromArgb(48, 63, 105);
            this.direccion1.Direcciones = (BindingList<Address>)componentResourceManager.GetObject("direccion1.Direcciones");
            this.direccion1.Location = new Point(118, 3);
            this.direccion1.Name = "direccion1";
            this.direccion1.Size = new Size(735, 142);
            this.direccion1.TabIndex = 30;
            this.tableLayoutPanel3.Anchor = AnchorStyles.Left;
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20f));
            this.tableLayoutPanel3.Controls.Add((System.Windows.Forms.Control)this.tabDatosBiograficos, 0, 0);
            this.tableLayoutPanel3.Location = new Point(3, 57);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel3.Size = new Size(46, 34);
            this.tableLayoutPanel3.TabIndex = 19;
            this.tableLayoutPanel3.Visible = false;
            this.tabDatosBiograficos.Controls.Add((System.Windows.Forms.Control)this.tabPage3);
            this.tabDatosBiograficos.Controls.Add((System.Windows.Forms.Control)this.tabPage4);
            this.tabDatosBiograficos.Controls.Add((System.Windows.Forms.Control)this.tabPage5);
            this.tabDatosBiograficos.Controls.Add((System.Windows.Forms.Control)this.tabPage6);
            this.tabDatosBiograficos.ItemSize = new Size(95, 20);
            this.tabDatosBiograficos.Location = new Point(3, 3);
            this.tabDatosBiograficos.Name = "tabDatosBiograficos";
            this.tabDatosBiograficos.SelectedIndex = 0;
            this.tabDatosBiograficos.Size = new Size(23, 28);
            this.tabDatosBiograficos.TabIndex = 6;
            this.tabDatosBiograficos.Visible = false;
            this.tabPage3.Controls.Add((System.Windows.Forms.Control)this.groupControl2);
            this.tabPage3.Controls.Add((System.Windows.Forms.Control)this.groupControl1);
            this.tabPage3.Location = new Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new Padding(3);
            this.tabPage3.Size = new Size(15, 0);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Datos Biograficos";
            this.tabPage3.UseVisualStyleBackColor = true;
            this.groupControl2.Controls.Add((System.Windows.Forms.Control)this.tabControl1);
            this.groupControl2.Location = new Point(141, 23);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new Size(835, 481);
            this.groupControl2.TabIndex = 6;
            this.groupControl2.TabStop = false;
            this.groupControl2.Text = "Datos Personales";
            this.tabControl1.Controls.Add((System.Windows.Forms.Control)this.tabPage1);
            this.tabControl1.Controls.Add((System.Windows.Forms.Control)this.tabPage2);
            this.tabControl1.Location = new Point(26, 19);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(813, 441);
            this.tabControl1.TabIndex = 6;
            this.tabPage1.Location = new Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new Padding(3);
            this.tabPage1.Size = new Size(805, 415);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Datos Identificativos";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage2.Controls.Add((System.Windows.Forms.Control)this.groupControl7);
            this.tabPage2.Location = new Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new Padding(3);
            this.tabPage2.Size = new Size(805, 415);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Datos Adicionales";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.groupControl7.Controls.Add((System.Windows.Forms.Control)this.nacionalidades1);
            this.groupControl7.Controls.Add((System.Windows.Forms.Control)this.alias1);
            this.groupControl7.Controls.Add((System.Windows.Forms.Control)this.cmbNivelCultural);
            this.groupControl7.Controls.Add((System.Windows.Forms.Control)this.label19);
            this.groupControl7.Controls.Add((System.Windows.Forms.Control)this.label23);
            this.groupControl7.Location = new Point(26, 22);
            this.groupControl7.Name = "groupControl7";
            this.groupControl7.Size = new Size(783, 274);
            this.groupControl7.TabIndex = 15;
            this.groupControl7.TabStop = false;
            this.groupControl7.Text = "Otros Datos";
            this.nacionalidades1.Location = new Point(405, 69);
            this.nacionalidades1.Name = "nacionalidades1";
            this.nacionalidades1.Paises = (BindingList<Coder>)componentResourceManager.GetObject("nacionalidades1.Paises");
            this.nacionalidades1.Size = new Size(358, 185);
            this.nacionalidades1.TabIndex = 44;
            this.alias1.Location = new Point(5, 69);
            this.alias1.Name = "alias1";
            this.alias1.Nombres = (BindingList<string>)componentResourceManager.GetObject("alias1.Nombres");
            this.alias1.Size = new Size(354, 185);
            this.alias1.TabIndex = 43;
            this.cmbNivelCultural.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cmbNivelCultural.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.cmbNivelCultural.FormattingEnabled = true;
            this.cmbNivelCultural.Location = new Point(134, 27);
            this.cmbNivelCultural.Name = "cmbNivelCultural";
            this.cmbNivelCultural.Size = new Size(223, 21);
            this.cmbNivelCultural.TabIndex = 41;
            this.cmbNivelCultural.SelectedIndexChanged += new EventHandler(this.cmbNivelCultural_SelectedIndexChanged);
            this.label19.AutoSize = true;
            this.label19.Font = new Font("Tahoma", 9f, FontStyle.Bold);
            this.label19.Location = new Point(39, 34);
            this.label19.Name = "label19";
            this.label19.Size = new Size(89, 14);
            this.label19.TabIndex = 2;
            this.label19.Text = "Nivel cultural:";
            this.label19.TextAlign = ContentAlignment.TopRight;
            this.label23.AutoSize = true;
            this.label23.Font = new Font("Tahoma", 9f, FontStyle.Bold);
            this.label23.Location = new Point(425, 34);
            this.label23.Name = "label23";
            this.label23.Size = new Size(111, 14);
            this.label23.TabIndex = 8;
            this.label23.Text = "Codigo genetico:";
            this.groupControl1.Location = new Point(33, 51);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new Size(835, 29);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.TabStop = false;
            this.groupControl1.Text = "Causas del Fichaje";
            this.tabPage4.Controls.Add((System.Windows.Forms.Control)this.lbltitulo2);
            this.tabPage4.Controls.Add((System.Windows.Forms.Control)this.groupControl14);
            this.tabPage4.Controls.Add((System.Windows.Forms.Control)this.groupControl8);
            this.tabPage4.Controls.Add((System.Windows.Forms.Control)this.groupControl11);
            this.tabPage4.Controls.Add((System.Windows.Forms.Control)this.groupControl9);
            this.tabPage4.Controls.Add((System.Windows.Forms.Control)this.groupControl15);
            this.tabPage4.Controls.Add((System.Windows.Forms.Control)this.groupControl13);
            this.tabPage4.Controls.Add((System.Windows.Forms.Control)this.groupControl16);
            this.tabPage4.Controls.Add((System.Windows.Forms.Control)this.groupControl12);
            this.tabPage4.Controls.Add((System.Windows.Forms.Control)this.groupControl10);
            this.tabPage4.Location = new Point(4, 24);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new Padding(3);
            this.tabPage4.Size = new Size(15, 0);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Datos Descriptivos";
            this.tabPage4.UseVisualStyleBackColor = true;
            this.lbltitulo2.AutoSize = true;
            this.lbltitulo2.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.lbltitulo2.ForeColor = Color.FromArgb(0, 64, 64);
            this.lbltitulo2.Location = new Point(29, 17);
            this.lbltitulo2.Name = "lbltitulo2";
            this.lbltitulo2.Size = new Size(160, 20);
            this.lbltitulo2.TabIndex = 12;
            this.lbltitulo2.Text = "Datos Descriptivos";
            this.groupControl14.Controls.Add((System.Windows.Forms.Control)this.cmbCabezaForma);
            this.groupControl14.Controls.Add((System.Windows.Forms.Control)this.label36);
            this.groupControl14.Location = new Point(592, 350);
            this.groupControl14.Name = "groupControl14";
            this.groupControl14.Size = new Size(314, 138);
            this.groupControl14.TabIndex = 11;
            this.groupControl14.TabStop = false;
            this.groupControl14.Text = "Cabeza";
            this.cmbCabezaForma.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cmbCabezaForma.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.cmbCabezaForma.FormattingEnabled = true;
            this.cmbCabezaForma.Location = new Point(69, 30);
            this.cmbCabezaForma.Name = "cmbCabezaForma";
            this.cmbCabezaForma.Size = new Size(238, 21);
            this.cmbCabezaForma.TabIndex = 21;
            this.cmbCabezaForma.SelectedIndexChanged += new EventHandler(this.cmbCabezaForma_SelectedIndexChanged);
            this.label36.AutoSize = true;
            this.label36.Font = new Font("Tahoma", 9f, FontStyle.Bold);
            this.label36.Location = new Point(15, 32);
            this.label36.Name = "label36";
            this.label36.Size = new Size(48, 14);
            this.label36.TabIndex = 1;
            this.label36.Text = "Forma:";
            this.groupControl8.Controls.Add((System.Windows.Forms.Control)this.cmbPeloColor);
            this.groupControl8.Controls.Add((System.Windows.Forms.Control)this.cmbPeloTipo);
            this.groupControl8.Controls.Add((System.Windows.Forms.Control)this.label22);
            this.groupControl8.Controls.Add((System.Windows.Forms.Control)this.label21);
            this.groupControl8.Location = new Point(33, 50);
            this.groupControl8.Name = "groupControl8";
            this.groupControl8.Size = new Size(259, 138);
            this.groupControl8.TabIndex = 7;
            this.groupControl8.TabStop = false;
            this.groupControl8.Text = "Pelo";
            this.cmbPeloColor.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cmbPeloColor.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.cmbPeloColor.FormattingEnabled = true;
            this.cmbPeloColor.Location = new Point(64, 59);
            this.cmbPeloColor.Name = "cmbPeloColor";
            this.cmbPeloColor.Size = new Size(182, 21);
            this.cmbPeloColor.TabIndex = 5;
            this.cmbPeloColor.SelectedIndexChanged += new EventHandler(this.cmbPeloColor_SelectedIndexChanged);
            this.cmbPeloTipo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cmbPeloTipo.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.cmbPeloTipo.FormattingEnabled = true;
            this.cmbPeloTipo.Location = new Point(64, 30);
            this.cmbPeloTipo.Name = "cmbPeloTipo";
            this.cmbPeloTipo.Size = new Size(182, 21);
            this.cmbPeloTipo.TabIndex = 4;
            this.cmbPeloTipo.SelectedIndexChanged += new EventHandler(this.cmbPeloTipo_SelectedIndexChanged);
            this.label22.AutoSize = true;
            this.label22.Font = new Font("Tahoma", 9f, FontStyle.Bold);
            this.label22.Location = new Point(15, 57);
            this.label22.Name = "label22";
            this.label22.Size = new Size(43, 14);
            this.label22.TabIndex = 3;
            this.label22.Text = "Color:";
            this.label21.AutoSize = true;
            this.label21.Font = new Font("Tahoma", 9f, FontStyle.Bold);
            this.label21.Location = new Point(21, 32);
            this.label21.Name = "label21";
            this.label21.Size = new Size(37, 14);
            this.label21.TabIndex = 1;
            this.label21.Text = "Tipo:";
            this.groupControl11.Controls.Add((System.Windows.Forms.Control)this.cmbFacialesSurcoNasal);
            this.groupControl11.Controls.Add((System.Windows.Forms.Control)this.cmbFacialesMejillas);
            this.groupControl11.Controls.Add((System.Windows.Forms.Control)this.cmbFacialesBarbilla);
            this.groupControl11.Controls.Add((System.Windows.Forms.Control)this.label34);
            this.groupControl11.Controls.Add((System.Windows.Forms.Control)this.label28);
            this.groupControl11.Controls.Add((System.Windows.Forms.Control)this.label29);
            this.groupControl11.Location = new Point(592, 200);
            this.groupControl11.Name = "groupControl11";
            this.groupControl11.Size = new Size(314, 138);
            this.groupControl11.TabIndex = 11;
            this.groupControl11.TabStop = false;
            this.groupControl11.Text = "Faciales";
            this.cmbFacialesSurcoNasal.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cmbFacialesSurcoNasal.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.cmbFacialesSurcoNasal.FormattingEnabled = true;
            this.cmbFacialesSurcoNasal.Location = new Point(92, 82);
            this.cmbFacialesSurcoNasal.Name = "cmbFacialesSurcoNasal";
            this.cmbFacialesSurcoNasal.Size = new Size(215, 21);
            this.cmbFacialesSurcoNasal.TabIndex = 16;
            this.cmbFacialesSurcoNasal.SelectedIndexChanged += new EventHandler(this.cmbFacialesSurcoNasal_SelectedIndexChanged);
            this.cmbFacialesMejillas.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cmbFacialesMejillas.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.cmbFacialesMejillas.FormattingEnabled = true;
            this.cmbFacialesMejillas.Location = new Point(92, 52);
            this.cmbFacialesMejillas.Name = "cmbFacialesMejillas";
            this.cmbFacialesMejillas.Size = new Size(215, 21);
            this.cmbFacialesMejillas.TabIndex = 15;
            this.cmbFacialesMejillas.SelectedIndexChanged += new EventHandler(this.cmbFacialesMejillas_SelectedIndexChanged);
            this.cmbFacialesBarbilla.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cmbFacialesBarbilla.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.cmbFacialesBarbilla.FormattingEnabled = true;
            this.cmbFacialesBarbilla.Location = new Point(92, 25);
            this.cmbFacialesBarbilla.Name = "cmbFacialesBarbilla";
            this.cmbFacialesBarbilla.Size = new Size(215, 21);
            this.cmbFacialesBarbilla.TabIndex = 14;
            this.cmbFacialesBarbilla.SelectedIndexChanged += new EventHandler(this.cmbFacialesBarbilla_SelectedIndexChanged);
            this.label34.AutoSize = true;
            this.label34.Font = new Font("Tahoma", 9f, FontStyle.Bold);
            this.label34.Location = new Point(5, 84);
            this.label34.Name = "label34";
            this.label34.Size = new Size(81, 14);
            this.label34.TabIndex = 3;
            this.label34.Text = "Surco Nasal:";
            this.label28.AutoSize = true;
            this.label28.Font = new Font("Tahoma", 9f, FontStyle.Bold);
            this.label28.Location = new Point(31, 57);
            this.label28.Name = "label28";
            this.label28.Size = new Size(55, 14);
            this.label28.TabIndex = 3;
            this.label28.Text = "Mejillas:";
            this.label29.AutoSize = true;
            this.label29.Font = new Font("Tahoma", 9f, FontStyle.Bold);
            this.label29.Location = new Point(31, 32);
            this.label29.Name = "label29";
            this.label29.Size = new Size(55, 14);
            this.label29.TabIndex = 1;
            this.label29.Text = "Barbilla:";
            this.groupControl9.Controls.Add((System.Windows.Forms.Control)this.cmbNarizPunta);
            this.groupControl9.Controls.Add((System.Windows.Forms.Control)this.cmbNarizAncho);
            this.groupControl9.Controls.Add((System.Windows.Forms.Control)this.label24);
            this.groupControl9.Controls.Add((System.Windows.Forms.Control)this.label25);
            this.groupControl9.Location = new Point(316, 50);
            this.groupControl9.Name = "groupControl9";
            this.groupControl9.Size = new Size(259, 138);
            this.groupControl9.TabIndex = 8;
            this.groupControl9.TabStop = false;
            this.groupControl9.Text = "Nariz";
            this.cmbNarizPunta.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cmbNarizPunta.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.cmbNarizPunta.FormattingEnabled = true;
            this.cmbNarizPunta.Location = new Point(69, 57);
            this.cmbNarizPunta.Name = "cmbNarizPunta";
            this.cmbNarizPunta.Size = new Size(182, 21);
            this.cmbNarizPunta.TabIndex = 7;
            this.cmbNarizPunta.SelectedIndexChanged += new EventHandler(this.cmbNarizPunta_SelectedIndexChanged);
            this.cmbNarizAncho.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cmbNarizAncho.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.cmbNarizAncho.FormattingEnabled = true;
            this.cmbNarizAncho.Location = new Point(69, 30);
            this.cmbNarizAncho.Name = "cmbNarizAncho";
            this.cmbNarizAncho.Size = new Size(182, 21);
            this.cmbNarizAncho.TabIndex = 6;
            this.cmbNarizAncho.SelectedIndexChanged += new EventHandler(this.cmbNarizAncho_SelectedIndexChanged);
            this.label24.AutoSize = true;
            this.label24.Font = new Font("Tahoma", 9f, FontStyle.Bold);
            this.label24.Location = new Point(15, 57);
            this.label24.Name = "label24";
            this.label24.Size = new Size(48, 14);
            this.label24.TabIndex = 3;
            this.label24.Text = "Punta:";
            this.label25.AutoSize = true;
            this.label25.Font = new Font("Tahoma", 9f, FontStyle.Bold);
            this.label25.Location = new Point(13, 32);
            this.label25.Name = "label25";
            this.label25.Size = new Size(50, 14);
            this.label25.TabIndex = 1;
            this.label25.Text = "Ancho:";
            this.groupControl15.Controls.Add((System.Windows.Forms.Control)this.cmbCuelloAlto);
            this.groupControl15.Controls.Add((System.Windows.Forms.Control)this.cmbCuelloAncho);
            this.groupControl15.Controls.Add((System.Windows.Forms.Control)this.label37);
            this.groupControl15.Controls.Add((System.Windows.Forms.Control)this.label38);
            this.groupControl15.Location = new Point(316, 350);
            this.groupControl15.Name = "groupControl15";
            this.groupControl15.Size = new Size(259, 138);
            this.groupControl15.TabIndex = 10;
            this.groupControl15.TabStop = false;
            this.groupControl15.Text = "Cuello";
            this.cmbCuelloAlto.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cmbCuelloAlto.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.cmbCuelloAlto.FormattingEnabled = true;
            this.cmbCuelloAlto.Location = new Point(55, 55);
            this.cmbCuelloAlto.Name = "cmbCuelloAlto";
            this.cmbCuelloAlto.Size = new Size(196, 21);
            this.cmbCuelloAlto.TabIndex = 20;
            this.cmbCuelloAlto.SelectedIndexChanged += new EventHandler(this.cmbCuelloAlto_SelectedIndexChanged);
            this.cmbCuelloAncho.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cmbCuelloAncho.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.cmbCuelloAncho.FormattingEnabled = true;
            this.cmbCuelloAncho.Location = new Point(55, 25);
            this.cmbCuelloAncho.Name = "cmbCuelloAncho";
            this.cmbCuelloAncho.Size = new Size(196, 21);
            this.cmbCuelloAncho.TabIndex = 19;
            this.cmbCuelloAncho.SelectedIndexChanged += new EventHandler(this.cmbCuelloAncho_SelectedIndexChanged);
            this.label37.AutoSize = true;
            this.label37.Font = new Font("Tahoma", 9f, FontStyle.Bold);
            this.label37.Location = new Point(15, 57);
            this.label37.Name = "label37";
            this.label37.Size = new Size(37, 14);
            this.label37.TabIndex = 3;
            this.label37.Text = "Alto:";
            this.label38.AutoSize = true;
            this.label38.Font = new Font("Tahoma", 9f, FontStyle.Bold);
            this.label38.Location = new Point(5, 32);
            this.label38.Name = "label38";
            this.label38.Size = new Size(50, 14);
            this.label38.TabIndex = 1;
            this.label38.Text = "Ancho:";
            this.groupControl13.Controls.Add((System.Windows.Forms.Control)this.cmbCejasPoblacion);
            this.groupControl13.Controls.Add((System.Windows.Forms.Control)this.cmbCejasForma);
            this.groupControl13.Controls.Add((System.Windows.Forms.Control)this.label32);
            this.groupControl13.Controls.Add((System.Windows.Forms.Control)this.label33);
            this.groupControl13.Location = new Point(33, 200);
            this.groupControl13.Name = "groupControl13";
            this.groupControl13.Size = new Size(259, 138);
            this.groupControl13.TabIndex = 9;
            this.groupControl13.TabStop = false;
            this.groupControl13.Text = "Cejas";
            this.cmbCejasPoblacion.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cmbCejasPoblacion.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.cmbCejasPoblacion.FormattingEnabled = true;
            this.cmbCejasPoblacion.Location = new Point(85, 57);
            this.cmbCejasPoblacion.Name = "cmbCejasPoblacion";
            this.cmbCejasPoblacion.Size = new Size(169, 21);
            this.cmbCejasPoblacion.TabIndex = 11;
            this.cmbCejasPoblacion.SelectedIndexChanged += new EventHandler(this.cmbCejasPoblacion_SelectedIndexChanged);
            this.cmbCejasForma.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cmbCejasForma.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.cmbCejasForma.FormattingEnabled = true;
            this.cmbCejasForma.Location = new Point(85, 30);
            this.cmbCejasForma.Name = "cmbCejasForma";
            this.cmbCejasForma.Size = new Size(169, 21);
            this.cmbCejasForma.TabIndex = 10;
            this.cmbCejasForma.SelectedIndexChanged += new EventHandler(this.cmbCejasForma_SelectedIndexChanged);
            this.label32.AutoSize = true;
            this.label32.Font = new Font("Tahoma", 9f, FontStyle.Bold);
            this.label32.Location = new Point(15, 57);
            this.label32.Name = "label32";
            this.label32.Size = new Size(70, 14);
            this.label32.TabIndex = 3;
            this.label32.Text = "Poblacion:";
            this.label33.AutoSize = true;
            this.label33.Font = new Font("Tahoma", 9f, FontStyle.Bold);
            this.label33.Location = new Point(37, 32);
            this.label33.Name = "label33";
            this.label33.Size = new Size(48, 14);
            this.label33.TabIndex = 1;
            this.label33.Text = "Forma:";
            this.groupControl16.Controls.Add((System.Windows.Forms.Control)this.cmbOjosForma);
            this.groupControl16.Controls.Add((System.Windows.Forms.Control)this.cmbOjosColor);
            this.groupControl16.Controls.Add((System.Windows.Forms.Control)this.label39);
            this.groupControl16.Controls.Add((System.Windows.Forms.Control)this.label40);
            this.groupControl16.Location = new Point(33, 350);
            this.groupControl16.Name = "groupControl16";
            this.groupControl16.Size = new Size(259, 138);
            this.groupControl16.TabIndex = 9;
            this.groupControl16.TabStop = false;
            this.groupControl16.Text = "Ojos";
            this.cmbOjosForma.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cmbOjosForma.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.cmbOjosForma.FormattingEnabled = true;
            this.cmbOjosForma.Location = new Point(64, 55);
            this.cmbOjosForma.Name = "cmbOjosForma";
            this.cmbOjosForma.Size = new Size(190, 21);
            this.cmbOjosForma.TabIndex = 18;
            this.cmbOjosForma.SelectedIndexChanged += new EventHandler(this.cmbOjosForma_SelectedIndexChanged);
            this.cmbOjosColor.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cmbOjosColor.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.cmbOjosColor.FormattingEnabled = true;
            this.cmbOjosColor.Location = new Point(64, 25);
            this.cmbOjosColor.Name = "cmbOjosColor";
            this.cmbOjosColor.Size = new Size(190, 21);
            this.cmbOjosColor.TabIndex = 17;
            this.cmbOjosColor.SelectedIndexChanged += new EventHandler(this.cmbOjosColor_SelectedIndexChanged);
            this.label39.AutoSize = true;
            this.label39.Font = new Font("Tahoma", 9f, FontStyle.Bold);
            this.label39.Location = new Point(15, 57);
            this.label39.Name = "label39";
            this.label39.Size = new Size(48, 14);
            this.label39.TabIndex = 3;
            this.label39.Text = "Forma:";
            this.label40.AutoSize = true;
            this.label40.Font = new Font("Tahoma", 9f, FontStyle.Bold);
            this.label40.Location = new Point(21, 32);
            this.label40.Name = "label40";
            this.label40.Size = new Size(43, 14);
            this.label40.TabIndex = 1;
            this.label40.Text = "Color:";
            this.groupControl12.Controls.Add((System.Windows.Forms.Control)this.cmbBocaTipo);
            this.groupControl12.Controls.Add((System.Windows.Forms.Control)this.cmbBocaDimension);
            this.groupControl12.Controls.Add((System.Windows.Forms.Control)this.label30);
            this.groupControl12.Controls.Add((System.Windows.Forms.Control)this.label31);
            this.groupControl12.Location = new Point(316, 200);
            this.groupControl12.Name = "groupControl12";
            this.groupControl12.Size = new Size(259, 138);
            this.groupControl12.TabIndex = 10;
            this.groupControl12.TabStop = false;
            this.groupControl12.Text = "Boca";
            this.cmbBocaTipo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cmbBocaTipo.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.cmbBocaTipo.FormattingEnabled = true;
            this.cmbBocaTipo.Location = new Point(90, 57);
            this.cmbBocaTipo.Name = "cmbBocaTipo";
            this.cmbBocaTipo.Size = new Size(161, 21);
            this.cmbBocaTipo.TabIndex = 13;
            this.cmbBocaTipo.SelectedIndexChanged += new EventHandler(this.cmbBocaTipo_SelectedIndexChanged);
            this.cmbBocaDimension.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cmbBocaDimension.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.cmbBocaDimension.FormattingEnabled = true;
            this.cmbBocaDimension.Location = new Point(90, 30);
            this.cmbBocaDimension.Name = "cmbBocaDimension";
            this.cmbBocaDimension.Size = new Size(161, 21);
            this.cmbBocaDimension.TabIndex = 12;
            this.cmbBocaDimension.SelectedIndexChanged += new EventHandler(this.cmbBocaDimension_SelectedIndexChanged);
            this.label30.AutoSize = true;
            this.label30.Font = new Font("Tahoma", 9f, FontStyle.Bold);
            this.label30.Location = new Point(52, 57);
            this.label30.Name = "label30";
            this.label30.Size = new Size(37, 14);
            this.label30.TabIndex = 3;
            this.label30.Text = "Tipo:";
            this.label31.AutoSize = true;
            this.label31.Font = new Font("Tahoma", 9f, FontStyle.Bold);
            this.label31.Location = new Point(15, 32);
            this.label31.Name = "label31";
            this.label31.Size = new Size(74, 14);
            this.label31.TabIndex = 1;
            this.label31.Text = "Dimension:";
            this.groupControl10.Controls.Add((System.Windows.Forms.Control)this.cmbRostro);
            this.groupControl10.Controls.Add((System.Windows.Forms.Control)this.cmbRostroFrente);
            this.groupControl10.Controls.Add((System.Windows.Forms.Control)this.label26);
            this.groupControl10.Controls.Add((System.Windows.Forms.Control)this.label27);
            this.groupControl10.Location = new Point(592, 50);
            this.groupControl10.Name = "groupControl10";
            this.groupControl10.Size = new Size(314, 138);
            this.groupControl10.TabIndex = 8;
            this.groupControl10.TabStop = false;
            this.groupControl10.Text = "Rostro";
            this.cmbRostro.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cmbRostro.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.cmbRostro.FormattingEnabled = true;
            this.cmbRostro.Location = new Point(156, 61);
            this.cmbRostro.Name = "cmbRostro";
            this.cmbRostro.Size = new Size(151, 21);
            this.cmbRostro.TabIndex = 9;
            this.cmbRostro.SelectedIndexChanged += new EventHandler(this.cmbRostro_SelectedIndexChanged);
            this.cmbRostroFrente.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cmbRostroFrente.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.cmbRostroFrente.FormattingEnabled = true;
            this.cmbRostroFrente.Location = new Point(156, 30);
            this.cmbRostroFrente.Name = "cmbRostroFrente";
            this.cmbRostroFrente.Size = new Size(151, 21);
            this.cmbRostroFrente.TabIndex = 8;
            this.cmbRostroFrente.SelectedIndexChanged += new EventHandler(this.cmbRostroFrente_SelectedIndexChanged);
            this.label26.AutoSize = true;
            this.label26.Font = new Font("Tahoma", 9f, FontStyle.Bold);
            this.label26.Location = new Point(99, 61);
            this.label26.Name = "label26";
            this.label26.Size = new Size(53, 14);
            this.label26.TabIndex = 3;
            this.label26.Text = "Rostro:";
            this.label27.AutoSize = true;
            this.label27.Font = new Font("Tahoma", 9f, FontStyle.Bold);
            this.label27.Location = new Point(3, 32);
            this.label27.Name = "label27";
            this.label27.Size = new Size(149, 14);
            this.label27.TabIndex = 1;
            this.label27.Text = "Dimension de la frente:";
            this.tabPage5.Controls.Add((System.Windows.Forms.Control)this.lbltitulo3);
            this.tabPage5.Controls.Add((System.Windows.Forms.Control)this.groupControl20);
            this.tabPage5.Controls.Add((System.Windows.Forms.Control)this.groupControl19);
            this.tabPage5.Controls.Add((System.Windows.Forms.Control)this.groupControl24);
            this.tabPage5.Controls.Add((System.Windows.Forms.Control)this.groupControl18);
            this.tabPage5.Controls.Add((System.Windows.Forms.Control)this.groupControl17);
            this.tabPage5.Controls.Add((System.Windows.Forms.Control)this.groupControl22);
            this.tabPage5.Controls.Add((System.Windows.Forms.Control)this.groupControl25);
            this.tabPage5.Controls.Add((System.Windows.Forms.Control)this.groupControl21);
            this.tabPage5.Location = new Point(4, 24);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new Size(15, 0);
            this.tabPage5.TabIndex = 2;
            this.tabPage5.Text = "Datos Caracteristicos";
            this.tabPage5.UseVisualStyleBackColor = true;
            this.lbltitulo3.AutoSize = true;
            this.lbltitulo3.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.lbltitulo3.ForeColor = Color.FromArgb(0, 64, 64);
            this.lbltitulo3.Location = new Point(29, 16);
            this.lbltitulo3.Name = "lbltitulo3";
            this.lbltitulo3.Size = new Size(182, 20);
            this.lbltitulo3.TabIndex = 15;
            this.lbltitulo3.Text = "Datos Característicos";
            this.groupControl20.Controls.Add((System.Windows.Forms.Control)this.chkPiesPiernas);
            this.groupControl20.Location = new Point(585, 197);
            this.groupControl20.Name = "groupControl20";
            this.groupControl20.Size = new Size(308, 140);
            this.groupControl20.TabIndex = 14;
            this.groupControl20.TabStop = false;
            this.groupControl20.Text = "Pies/Piernas";
            this.chkPiesPiernas.FormattingEnabled = true;
            this.chkPiesPiernas.Location = new Point(15, 25);
            this.chkPiesPiernas.Name = "chkPiesPiernas";
            this.chkPiesPiernas.Size = new Size(278, 94);
            this.chkPiesPiernas.TabIndex = 1;
            this.groupControl19.Controls.Add((System.Windows.Forms.Control)this.chkPiel);
            this.groupControl19.Location = new Point(33, 49);
            this.groupControl19.Name = "groupControl19";
            this.groupControl19.Size = new Size(259, 126);
            this.groupControl19.TabIndex = 9;
            this.groupControl19.TabStop = false;
            this.groupControl19.Text = "Piel";
            this.chkPiel.FormattingEnabled = true;
            this.chkPiel.Location = new Point(17, 24);
            this.chkPiel.Name = "chkPiel";
            this.chkPiel.Size = new Size(225, 79);
            this.chkPiel.TabIndex = 0;
            this.groupControl24.Controls.Add((System.Windows.Forms.Control)this.chkOrejas);
            this.groupControl24.Location = new Point(309, 356);
            this.groupControl24.Name = "groupControl24";
            this.groupControl24.Size = new Size(259, 158);
            this.groupControl24.TabIndex = 13;
            this.groupControl24.TabStop = false;
            this.groupControl24.Text = "Orejas";
            this.chkOrejas.FormattingEnabled = true;
            this.chkOrejas.Location = new Point(15, 24);
            this.chkOrejas.Name = "chkOrejas";
            this.chkOrejas.Size = new Size(227, 109);
            this.chkOrejas.TabIndex = 1;
            this.groupControl18.Controls.Add((System.Windows.Forms.Control)this.chkPecho);
            this.groupControl18.Location = new Point(309, 49);
            this.groupControl18.Name = "groupControl18";
            this.groupControl18.Size = new Size(259, 126);
            this.groupControl18.TabIndex = 10;
            this.groupControl18.TabStop = false;
            this.groupControl18.Text = "Pecho";
            this.chkPecho.FormattingEnabled = true;
            this.chkPecho.Location = new Point(15, 24);
            this.chkPecho.Name = "chkPecho";
            this.chkPecho.Size = new Size(227, 79);
            this.chkPecho.TabIndex = 1;
            this.groupControl17.Controls.Add((System.Windows.Forms.Control)this.chkEspalda);
            this.groupControl17.Location = new Point(585, 49);
            this.groupControl17.Name = "groupControl17";
            this.groupControl17.Size = new Size(308, 126);
            this.groupControl17.TabIndex = 11;
            this.groupControl17.TabStop = false;
            this.groupControl17.Text = "Espalda";
            this.chkEspalda.FormattingEnabled = true;
            this.chkEspalda.Location = new Point(15, 24);
            this.chkEspalda.Name = "chkEspalda";
            this.chkEspalda.Size = new Size(278, 79);
            this.chkEspalda.TabIndex = 1;
            this.groupControl22.Controls.Add((System.Windows.Forms.Control)this.chkDedosManos);
            this.groupControl22.Location = new Point(33, 197);
            this.groupControl22.Name = "groupControl22";
            this.groupControl22.Size = new Size(259, 140);
            this.groupControl22.TabIndex = 12;
            this.groupControl22.TabStop = false;
            this.groupControl22.Text = "Dedos/Manos";
            this.chkDedosManos.FormattingEnabled = true;
            this.chkDedosManos.Location = new Point(17, 25);
            this.chkDedosManos.Name = "chkDedosManos";
            this.chkDedosManos.Size = new Size(225, 94);
            this.chkDedosManos.TabIndex = 0;
            this.groupControl25.Controls.Add((System.Windows.Forms.Control)this.chkDientes);
            this.groupControl25.Location = new Point(33, 356);
            this.groupControl25.Name = "groupControl25";
            this.groupControl25.Size = new Size(259, 158);
            this.groupControl25.TabIndex = 12;
            this.groupControl25.TabStop = false;
            this.groupControl25.Text = "Dientes";
            this.chkDientes.FormattingEnabled = true;
            this.chkDientes.Location = new Point(17, 24);
            this.chkDientes.Name = "chkDientes";
            this.chkDientes.Size = new Size(225, 109);
            this.chkDientes.TabIndex = 0;
            this.groupControl21.Controls.Add((System.Windows.Forms.Control)this.chkBrazos);
            this.groupControl21.Location = new Point(309, 197);
            this.groupControl21.Name = "groupControl21";
            this.groupControl21.Size = new Size(259, 140);
            this.groupControl21.TabIndex = 13;
            this.groupControl21.TabStop = false;
            this.groupControl21.Text = "Brazos";
            this.chkBrazos.FormattingEnabled = true;
            this.chkBrazos.Location = new Point(15, 25);
            this.chkBrazos.Name = "chkBrazos";
            this.chkBrazos.Size = new Size(227, 94);
            this.chkBrazos.TabIndex = 1;
            this.tabPage6.Controls.Add((System.Windows.Forms.Control)this.groupControl23);
            this.tabPage6.Controls.Add((System.Windows.Forms.Control)this.groupControl30);
            this.tabPage6.Controls.Add((System.Windows.Forms.Control)this.lblTitulo4);
            this.tabPage6.Controls.Add((System.Windows.Forms.Control)this.groupControl26);
            this.tabPage6.Controls.Add((System.Windows.Forms.Control)this.groupControl29);
            this.tabPage6.Controls.Add((System.Windows.Forms.Control)this.groupControl27);
            this.tabPage6.Controls.Add((System.Windows.Forms.Control)this.groupControl28);
            this.tabPage6.Location = new Point(4, 24);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new Size(15, 0);
            this.tabPage6.TabIndex = 3;
            this.tabPage6.Text = "Rasgos de la Conducta";
            this.tabPage6.UseVisualStyleBackColor = true;
            this.groupControl23.Controls.Add((System.Windows.Forms.Control)this.chkIdiomas);
            this.groupControl23.Location = new Point(585, 232);
            this.groupControl23.Name = "groupControl23";
            this.groupControl23.Size = new Size(308, 175);
            this.groupControl23.TabIndex = 20;
            this.groupControl23.TabStop = false;
            this.groupControl23.Text = "Idiomas";
            this.chkIdiomas.FormattingEnabled = true;
            this.chkIdiomas.Location = new Point(15, 24);
            this.chkIdiomas.Name = "chkIdiomas";
            this.chkIdiomas.Size = new Size(269, 124);
            this.chkIdiomas.TabIndex = 1;
            this.groupControl30.Controls.Add((System.Windows.Forms.Control)this.chkAlAndar);
            this.groupControl30.Location = new Point(33, 51);
            this.groupControl30.Name = "groupControl30";
            this.groupControl30.Size = new Size(259, 159);
            this.groupControl30.TabIndex = 15;
            this.groupControl30.TabStop = false;
            this.groupControl30.Text = "Al Andar";
            this.chkAlAndar.FormattingEnabled = true;
            this.chkAlAndar.Location = new Point(17, 24);
            this.chkAlAndar.Name = "chkAlAndar";
            this.chkAlAndar.Size = new Size(217, 109);
            this.chkAlAndar.TabIndex = 0;
            this.lblTitulo4.AutoSize = true;
            this.lblTitulo4.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.lblTitulo4.ForeColor = Color.FromArgb(0, 64, 64);
            this.lblTitulo4.Location = new Point(29, 18);
            this.lblTitulo4.Name = "lblTitulo4";
            this.lblTitulo4.Size = new Size(196, 20);
            this.lblTitulo4.TabIndex = 0;
            this.lblTitulo4.Text = "Rasgos de la Conducta";
            this.groupControl26.Controls.Add((System.Windows.Forms.Control)this.chkDeLaConducta);
            this.groupControl26.Location = new Point(585, 51);
            this.groupControl26.Name = "groupControl26";
            this.groupControl26.Size = new Size(308, 159);
            this.groupControl26.TabIndex = 17;
            this.groupControl26.TabStop = false;
            this.groupControl26.Text = "De la Conducta";
            this.chkDeLaConducta.FormattingEnabled = true;
            this.chkDeLaConducta.Location = new Point(15, 24);
            this.chkDeLaConducta.Name = "chkDeLaConducta";
            this.chkDeLaConducta.Size = new Size(269, 109);
            this.chkDeLaConducta.TabIndex = 1;
            this.groupControl29.Controls.Add((System.Windows.Forms.Control)this.chkAlHablar);
            this.groupControl29.Location = new Point(309, 51);
            this.groupControl29.Name = "groupControl29";
            this.groupControl29.Size = new Size(259, 159);
            this.groupControl29.TabIndex = 16;
            this.groupControl29.TabStop = false;
            this.groupControl29.Text = "Al Hablar";
            this.chkAlHablar.FormattingEnabled = true;
            this.chkAlHablar.Location = new Point(15, 24);
            this.chkAlHablar.Name = "chkAlHablar";
            this.chkAlHablar.Size = new Size(217, 109);
            this.chkAlHablar.TabIndex = 1;
            this.groupControl27.Controls.Add((System.Windows.Forms.Control)this.chkHabilidadesEsp);
            this.groupControl27.Location = new Point(309, 232);
            this.groupControl27.Name = "groupControl27";
            this.groupControl27.Size = new Size(259, 175);
            this.groupControl27.TabIndex = 19;
            this.groupControl27.TabStop = false;
            this.groupControl27.Text = "Habilidades Especiales";
            this.chkHabilidadesEsp.FormattingEnabled = true;
            this.chkHabilidadesEsp.Location = new Point(15, 24);
            this.chkHabilidadesEsp.Name = "chkHabilidadesEsp";
            this.chkHabilidadesEsp.Size = new Size(217, 124);
            this.chkHabilidadesEsp.TabIndex = 1;
            this.groupControl28.Controls.Add((System.Windows.Forms.Control)this.chkPartesAusentes);
            this.groupControl28.Location = new Point(33, 232);
            this.groupControl28.Name = "groupControl28";
            this.groupControl28.Size = new Size(259, 175);
            this.groupControl28.TabIndex = 18;
            this.groupControl28.TabStop = false;
            this.groupControl28.Text = "Partes Ausentes";
            this.chkPartesAusentes.FormattingEnabled = true;
            this.chkPartesAusentes.Location = new Point(17, 24);
            this.chkPartesAusentes.Name = "chkPartesAusentes";
            this.chkPartesAusentes.Size = new Size(217, 124);
            this.chkPartesAusentes.TabIndex = 0;
            this.tableLayoutPanel10.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.tableLayoutPanel10.ColumnCount = 6;
            this.tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12f));
            this.tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15.50054f));
            this.tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24.0043f));
            this.tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15.71582f));
            this.tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24.32723f));
            this.tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8.396125f));
            this.tableLayoutPanel10.Controls.Add((System.Windows.Forms.Control)this.label5, 3, 2);
            this.tableLayoutPanel10.Controls.Add((System.Windows.Forms.Control)this.numPie, 4, 2);
            this.tableLayoutPanel10.Controls.Add((System.Windows.Forms.Control)this.label16, 3, 1);
            this.tableLayoutPanel10.Controls.Add((System.Windows.Forms.Control)this.cmbComplexion, 2, 2);
            this.tableLayoutPanel10.Controls.Add((System.Windows.Forms.Control)this.label18, 3, 0);
            this.tableLayoutPanel10.Controls.Add((System.Windows.Forms.Control)this.numEstatura, 4, 1);
            this.tableLayoutPanel10.Controls.Add((System.Windows.Forms.Control)this.label14, 1, 0);
            this.tableLayoutPanel10.Controls.Add((System.Windows.Forms.Control)this.numPeso, 4, 0);
            this.tableLayoutPanel10.Controls.Add((System.Windows.Forms.Control)this.cmbColorPiel, 2, 1);
            this.tableLayoutPanel10.Controls.Add((System.Windows.Forms.Control)this.label15, 1, 1);
            this.tableLayoutPanel10.Controls.Add((System.Windows.Forms.Control)this.cmbSexo, 2, 0);
            this.tableLayoutPanel10.Controls.Add((System.Windows.Forms.Control)this.label17, 1, 2);
            this.tableLayoutPanel10.Location = new Point(12, 652);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 3;
            this.tableLayoutPanel10.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333f));
            this.tableLayoutPanel10.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333f));
            this.tableLayoutPanel10.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333f));
            this.tableLayoutPanel10.Size = new Size(937, 78);
            this.tableLayoutPanel10.TabIndex = 22;
            this.tableLayoutPanel11.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.tableLayoutPanel11.ColumnCount = 1;
            this.tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20f));
            this.tableLayoutPanel11.Controls.Add((System.Windows.Forms.Control)this.label4, 0, 0);
            this.tableLayoutPanel11.Location = new Point(12, 105);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 1;
            this.tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Absolute, 29f));
            this.tableLayoutPanel11.Size = new Size(940, 29);
            this.tableLayoutPanel11.TabIndex = 17;
            this.label4.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.BackColor = Color.Transparent;
            this.label4.Font = new Font("Microsoft Sans Serif", 14.25f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, (byte)0);
            this.label4.ForeColor = Color.White;
            this.label4.Location = new Point(3, 2);
            this.label4.Name = "label4";
            this.label4.Size = new Size(934, 24);
            this.label4.TabIndex = 26;
            this.label4.Text = "DATOS BIOGRAFICOS";
            this.label4.TextAlign = ContentAlignment.MiddleCenter;
            this.tableLayoutPanel12.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.tableLayoutPanel12.ColumnCount = 6;
            this.tableLayoutPanel12.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12f));
            this.tableLayoutPanel12.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15.50054f));
            this.tableLayoutPanel12.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24.11195f));
            this.tableLayoutPanel12.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.25404f));
            this.tableLayoutPanel12.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24.21959f));
            this.tableLayoutPanel12.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8.288483f));
            this.tableLayoutPanel12.Controls.Add((System.Windows.Forms.Control)this.cmbTipoPersona, 2, 1);
            this.tableLayoutPanel12.Controls.Add((System.Windows.Forms.Control)this.cmbMotivo, 2, 0);
            this.tableLayoutPanel12.Controls.Add((System.Windows.Forms.Control)this.cmbCausa, 4, 0);
            this.tableLayoutPanel12.Controls.Add((System.Windows.Forms.Control)this.label2, 3, 0);
            this.tableLayoutPanel12.Controls.Add((System.Windows.Forms.Control)this.label3, 1, 1);
            this.tableLayoutPanel12.Controls.Add((System.Windows.Forms.Control)this.label1, 1, 0);
            this.tableLayoutPanel12.Location = new Point(12, 164);
            this.tableLayoutPanel12.Name = "tableLayoutPanel12";
            this.tableLayoutPanel12.RowCount = 2;
            this.tableLayoutPanel12.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
            this.tableLayoutPanel12.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
            this.tableLayoutPanel12.Size = new Size(937, 57);
            this.tableLayoutPanel12.TabIndex = 18;
            this.pictureBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.pictureBox1.Image = (Image)componentResourceManager.GetObject("pictureBox1.Image");
            this.pictureBox1.Location = new Point(857, 21);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(80, 75);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 26;
            this.pictureBox1.TabStop = false;
            this.tableLayoutPanel6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.tableLayoutPanel6.ColumnCount = 3;
            this.tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12f));
            this.tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 79.44026f));
            this.tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8.61141f));
            this.tableLayoutPanel6.Controls.Add((System.Windows.Forms.Control)this.label20, 1, 0);
            this.tableLayoutPanel6.Location = new Point(12, 364);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel6.Size = new Size(937, 26);
            this.tableLayoutPanel6.TabIndex = 26;
            this.label20.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.label20.AutoSize = true;
            this.label20.BackColor = Color.Transparent;
            this.label20.BorderStyle = BorderStyle.Fixed3D;
            this.label20.Font = new Font("Microsoft Sans Serif", 11.25f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, (byte)0);
            this.label20.ForeColor = Color.White;
            this.label20.Location = new Point(115, 3);
            this.label20.Name = "label20";
            this.label20.Size = new Size(737, 20);
            this.label20.TabIndex = 26;
            this.label20.Text = "LUGAR Y FECHA DE NACIMIENTO";
            this.tableLayoutPanel7.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.tableLayoutPanel7.ColumnCount = 3;
            this.tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12f));
            this.tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 79.44026f));
            this.tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8.61141f));
            this.tableLayoutPanel7.Controls.Add((System.Windows.Forms.Control)this.label41, 1, 0);
            this.tableLayoutPanel7.Location = new Point(12, 450);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel7.Size = new Size(937, 26);
            this.tableLayoutPanel7.TabIndex = 27;
            this.label41.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.label41.AutoSize = true;
            this.label41.BackColor = Color.Transparent;
            this.label41.BorderStyle = BorderStyle.Fixed3D;
            this.label41.Font = new Font("Microsoft Sans Serif", 11.25f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, (byte)0);
            this.label41.ForeColor = Color.White;
            this.label41.Location = new Point(115, 3);
            this.label41.Name = "label41";
            this.label41.Size = new Size(737, 20);
            this.label41.TabIndex = 26;
            this.label41.Text = "DIRECCIONES";
            this.tableLayoutPanel9.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.tableLayoutPanel9.ColumnCount = 3;
            this.tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12f));
            this.tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 79.5479f));
            this.tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8.503768f));
            this.tableLayoutPanel9.Controls.Add((System.Windows.Forms.Control)this.label43, 1, 0);
            this.tableLayoutPanel9.Location = new Point(12, 627);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 1;
            this.tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel9.Size = new Size(937, 26);
            this.tableLayoutPanel9.TabIndex = 28;
            this.label43.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.label43.AutoSize = true;
            this.label43.BackColor = Color.Transparent;
            this.label43.BorderStyle = BorderStyle.Fixed3D;
            this.label43.Font = new Font("Microsoft Sans Serif", 11.25f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, (byte)0);
            this.label43.ForeColor = Color.White;
            this.label43.Location = new Point(115, 3);
            this.label43.Name = "label43";
            this.label43.Size = new Size(738, 20);
            this.label43.TabIndex = 26;
            this.label43.Text = "DATOS ADICIONALES";
            this.tableLayoutPanel14.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.tableLayoutPanel14.ColumnCount = 2;
            this.tableLayoutPanel14.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80f));
            this.tableLayoutPanel14.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
            this.tableLayoutPanel14.Controls.Add((System.Windows.Forms.Control)this.panel1, 0, 0);
            this.tableLayoutPanel14.Controls.Add((System.Windows.Forms.Control)this.pictureBox1, 1, 0);
            this.tableLayoutPanel14.Location = new Point(12, 5);
            this.tableLayoutPanel14.Name = "tableLayoutPanel14";
            this.tableLayoutPanel14.RowCount = 1;
            this.tableLayoutPanel14.RowStyles.Add(new RowStyle(SizeType.Absolute, 99f));
            this.tableLayoutPanel14.Size = new Size(940, 99);
            this.tableLayoutPanel14.TabIndex = 29;
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = Color.FromArgb(48, 63, 105);
            this.ClientSize = new Size(961, 748);
            this.Controls.Add((System.Windows.Forms.Control)this.tableLayoutPanel14);
            this.Controls.Add((System.Windows.Forms.Control)this.tableLayoutPanel9);
            this.Controls.Add((System.Windows.Forms.Control)this.tableLayoutPanel7);
            this.Controls.Add((System.Windows.Forms.Control)this.tableLayoutPanel6);
            this.Controls.Add((System.Windows.Forms.Control)this.tableLayoutPanel12);
            this.Controls.Add((System.Windows.Forms.Control)this.tableLayoutPanel11);
            this.Controls.Add((System.Windows.Forms.Control)this.tableLayoutPanel10);
            this.Controls.Add((System.Windows.Forms.Control)this.tableLayoutPanel8);
            this.Controls.Add((System.Windows.Forms.Control)this.tableLayoutPanel5);
            this.Controls.Add((System.Windows.Forms.Control)this.tableLayoutPanel4);
            this.Controls.Add((System.Windows.Forms.Control)this.tableLayoutPanel2);
            this.Controls.Add((System.Windows.Forms.Control)this.tableLayoutPanel1);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Name = nameof(vBiography);
            this.Text = "Registro de Datos Biograficos";
            this.WindowState = FormWindowState.Maximized;
            this.Load += new EventHandler(this.vBiography_Load);
            this.DoubleClick += new EventHandler(this.vBiography_DoubleClick);
            ((ISupportInitialize)this.errorProvedor).EndInit();
            this.panel1.ResumeLayout(false);
            this.numPie.EndInit();
            this.numEstatura.EndInit();
            this.numPeso.EndInit();
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
            ((ISupportInitialize)this.pictureBox1).EndInit();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            this.tableLayoutPanel14.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
