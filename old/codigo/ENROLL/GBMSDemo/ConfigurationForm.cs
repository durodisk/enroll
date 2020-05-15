using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using GBMSGUI_NET;
using GBMSAPI_NET.GBMSAPI_NET_Defines.GBMSAPI_NET_DeviceCharacteristicsDefines;
using GBMSAPI_NET.GBMSAPI_NET_LibraryFunctions;
using GBMSAPI_NET.GBMSAPI_NET_Defines.GBMSAPI_NET_AcquisitionProcessDefines;
using GBMSAPI_NET.GBMSAPI_NET_Defines.GBMSAPI_NET_ErrorCodesDefines;

namespace GBMSDemo
{
    public partial class ConfigurationForm : Form
    {
        private DemoForm DemoFormRef;   // reference to main form

        public ConfigurationForm(DemoForm DemoFormRef)
        {
            // save reference to main form
            this.DemoFormRef = DemoFormRef;

            InitializeComponent();
        }

        // Standard used  for image sizes
        public class ScanModes
        {
            public const int FullHiRes = 0;
            public const int FullLowRes = 1;
            public const int RollIQS = 2;
            public const int RollGA = 3;
            public const int RollJoint = 4;
            public const int RollThenar = 5;
        }

        // Item for Diagnostic list
        private class DiagnosticListItem
        {
            public DiagnosticListItem(uint Mask)
            {
                this.Mask = Mask;
            }
            public uint Mask;
            public override String ToString()
            {
                switch (Mask)
                {
                    case GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_SCANNER_SURFACE_NOT_NORMA:
                        return "Scanner surface";
                    case GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_EXT_LIGHT_TOO_STRONG:
                        return "External light";
                    case GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_FLAT_FINGER_SLIDING:
                        return "Finger sliding";
                    case GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_FLAT_FINGER_DISPLACED_DOWN:
                        return "Finger displaced down";
                    case GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_FLAT_FINGER_OUT_OF_REGION_LEFT:
                        return "Finger displaced left";
                    case GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_FLAT_FINGER_OUT_OF_REGION_RIGHT:
                        return "Finger displaced right";
                    case GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_FLAT_FINGER_OUT_OF_REGION_TOP:
                        return "Finger displaced top";
                    case GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_DRY_FINGER:
                        return "Dry finger";
                    case GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_WET_FINGER:
                        return "Wet finger";
                    case GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_COMPOSITION_SLOW:
                        return "Composition slow";
                    case GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_TOO_SHORT_VERTICAL_ROLL:
                        return "Too short vertical roll";
                    case GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_VSROLL_DISPLACED_DOWN:
                        return "Roll displaced down";
                    case GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_VSROLL_IMPROPER_ROLL:
                        return "Improper roll";
                    case GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_VSROLL_OUTSIDE_BORDER_LEFT:
                        return "Roll displaced left";
                    case GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_VSROLL_OUTSIDE_BORDER_RIGHT:
                        return "Roll displaced right";
                    case GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_VSROLL_OUTSIDE_BORDER_TOP:
                        return "Roll displaced top";
                    case GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_VSROLL_TOO_FAST_ROLL:
                        return "Too fast roll";
                    case GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_VSROLL_TOO_NARROW_ROLL:
                        return "Too narrow roll";
                }
                return "";
            }
        }

        // 2.3.0.0
        private struct tHeaterData
        {
            public float MinTemp;
            public float MaxTemp;
            public float DefaultTemp;
            public float CurrentTemp;
        }
        private tHeaterData HeaterData;

        private void ConfigurationForm_Load(object sender, EventArgs e)
        {
            // image size
            // fill Standard's combobox
            cmbStandard.Items.Insert(DemoForm.ImageSizeStandards.Custom, "Custom");
            cmbStandard.Items.Insert(DemoForm.ImageSizeStandards.ANSI_NIST_ITL_1_2000, "ANSI/NIST-ITL 1-2000");
            cmbStandard.Items.Insert(DemoForm.ImageSizeStandards.ANSI_NIST_ITL_1_2000_INTERPOL, "ANSI/NIST-ITL 1-2000 INTERPOL");
            cmbStandard.Items.Insert(DemoForm.ImageSizeStandards.ISO_IEC_FCD_19794_4, "ISO-IEC FCD 19794-4");
            cmbStandard.Items.Insert(DemoForm.ImageSizeStandards.ANSI_NIST_ITL_1_2007, "ANSI/NIST-ITL 1-2007/2011");
            cmbStandard.Items.Insert(DemoForm.ImageSizeStandards.GA_CHINA, "GA (China)");

            // IAFIS quality algorithm
            // fill combobox
            // subtract 1 to use ID as index...
            cmbQualityAlgorithm.Items.Insert(GBMSGUI.QualityAlgorithms.NFIQAlgorithm-1, "NIST Fingerprint Image Quality (NFIQ)");
            cmbQualityAlgorithm.Items.Insert(GBMSGUI.QualityAlgorithms.GBAlgorithm - 1, "Green Bit proprietary");
            cmbQualityAlgorithm.Items.Insert(GBMSGUI.QualityAlgorithms.NFIQ2Algorithm - 1, "NIST Fingerprint Image Quality 2 (NFIQ2)");

            // fill devices list for frame rate
            lstDevice.Items.Add(DemoForm.GetDeviceName(GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_DS26));
            lstDevice.Items.Add(DemoForm.GetDeviceName(GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_DS32));
            lstDevice.Items.Add(DemoForm.GetDeviceName(GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_DS40I));
            lstDevice.Items.Add(DemoForm.GetDeviceName(GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_DS84));
            lstDevice.Items.Add(DemoForm.GetDeviceName(GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_DS84C));
            lstDevice.Items.Add(DemoForm.GetDeviceName(GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_MC517));
            lstDevice.Items.Add(DemoForm.GetDeviceName(GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_MS527));
            lstDevice.Items.Add(DemoForm.GetDeviceName(GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_DS84t));
            lstDevice.Items.Add(DemoForm.GetDeviceName(GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_DSID20));
            lstDevice.Items.Add(DemoForm.GetDeviceName(GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_MS527t));
            lstDevice.SelectedIndex = 0;

            // Language
            cmbLanguage.Items.Insert(DemoForm.GUILanguages.English, "English");
            cmbLanguage.Items.Insert(DemoForm.GUILanguages.Italian, "Italian");
            cmbLanguage.Items.Insert(DemoForm.GUILanguages.Chinese, "Chinese");

            // Finger contact evaluation mode
            // subtract 1 to use ID as index...
            cmbFingerContactEvaluationMode.Items.Insert(GBMSGUI.FingerContactEvaluationMode.DryWetWarning-1, "Dry/Wet warning");
            cmbFingerContactEvaluationMode.Items.Insert(GBMSGUI.FingerContactEvaluationMode.Contrast-1, "Contrast");

            // 2.0.0.0
            DisplaySettings();

            // 2.3.0.0
            uint DeviceFeatures;
            GBMSAPI_NET_DeviceCharacteristicsRoutines.GBMSAPI_NET_GetDeviceFeatures(out DeviceFeatures);
            if (GBMSGUI.CheckMask(DeviceFeatures, GBMSAPI_NET_DeviceFeatures.GBMSAPI_NET_DF_HEATER))
            {
                grpHeater.Enabled = true;
                ReadHeaterSettings();
            }
            else
                grpHeater.Enabled = false;
        }

        private void ShowImageSizeValues()
        {
            // disable all (except flat finger)
            txtUpperPalmWidth.Enabled = false;
            txtUpperPalmHeight.Enabled = false;
            txtLowerPalmWidth.Enabled = false;
            txtLowerPalmHeight.Enabled = false;
            txtWritersPalmWidth.Enabled = false;
            txtWritersPalmHeight.Enabled = false;
            txtFourFingersWidth.Enabled = false;
            txtFourFingersHeight.Enabled = false;
            txtTwoThumbsWidth.Enabled = false;
            txtTwoThumbsHeight.Enabled = false;
            txtFlatThumbWidth.Enabled = false;
            txtFlatThumbHeight.Enabled = false;
            txtRolledThumbWidth.Enabled = false;
            txtRolledThumbHeight.Enabled = false;
            txtRolledIndexWidth.Enabled = false;
            txtRolledIndexHeight.Enabled = false;
            txtRolledMiddleWidth.Enabled = false;
            txtRolledMiddleHeight.Enabled = false;
            txtRolledRingWidth.Enabled = false;
            txtRolledRingHeight.Enabled = false;
            txtRolledLittleWidth.Enabled = false;
            txtRolledLittleHeight.Enabled = false;

            txtUpperPalmWidth.Text = DemoFormRef.DemoConfig.ImageSize.UpperPalmWidth.ToString();
            txtUpperPalmHeight.Text = DemoFormRef.DemoConfig.ImageSize.UpperPalmHeight.ToString();
            txtLowerPalmWidth.Text = DemoFormRef.DemoConfig.ImageSize.LowerPalmWidth.ToString();
            txtLowerPalmHeight.Text = DemoFormRef.DemoConfig.ImageSize.LowerPalmHeight.ToString();
            txtWritersPalmWidth.Text = DemoFormRef.DemoConfig.ImageSize.WritersPalmWidth.ToString();
            txtWritersPalmHeight.Text = DemoFormRef.DemoConfig.ImageSize.WritersPalmHeight.ToString();
            txtFourFingersWidth.Text = DemoFormRef.DemoConfig.ImageSize.FourFingersWidth.ToString();
            txtFourFingersHeight.Text = DemoFormRef.DemoConfig.ImageSize.FourFingersHeight.ToString();
            txtTwoThumbsWidth.Text = DemoFormRef.DemoConfig.ImageSize.TwoThumbsWidth.ToString();
            txtTwoThumbsHeight.Text = DemoFormRef.DemoConfig.ImageSize.TwoThumbsHeight.ToString();
            txtFlatThumbWidth.Text = DemoFormRef.DemoConfig.ImageSize.FlatThumbWidth.ToString();
            txtFlatThumbHeight.Text = DemoFormRef.DemoConfig.ImageSize.FlatThumbHeight.ToString();
            txtFlatFingerWidth.Text = DemoFormRef.DemoConfig.ImageSize.FlatFingerWidth.ToString();
            txtFlatFingerHeight.Text = DemoFormRef.DemoConfig.ImageSize.FlatFingerHeight.ToString();
            txtRolledThumbWidth.Text = DemoFormRef.DemoConfig.ImageSize.RolledThumbWidth.ToString();
            txtRolledThumbHeight.Text = DemoFormRef.DemoConfig.ImageSize.RolledThumbHeight.ToString();
            txtRolledIndexWidth.Text = DemoFormRef.DemoConfig.ImageSize.RolledIndexWidth.ToString();
            txtRolledIndexHeight.Text = DemoFormRef.DemoConfig.ImageSize.RolledIndexHeight.ToString();
            txtRolledMiddleWidth.Text = DemoFormRef.DemoConfig.ImageSize.RolledMiddleWidth.ToString();
            txtRolledMiddleHeight.Text = DemoFormRef.DemoConfig.ImageSize.RolledMiddleHeight.ToString();
            txtRolledRingWidth.Text = DemoFormRef.DemoConfig.ImageSize.RolledRingWidth.ToString();
            txtRolledRingHeight.Text = DemoFormRef.DemoConfig.ImageSize.RolledRingHeight.ToString();
            txtRolledLittleWidth.Text = DemoFormRef.DemoConfig.ImageSize.RolledLittleWidth.ToString();
            txtRolledLittleHeight.Text = DemoFormRef.DemoConfig.ImageSize.RolledLittleHeight.ToString();
            //V1.10
            txtRolledTipWidth.Text = DemoFormRef.DemoConfig.ImageSize.RolledTipWidth.ToString();
            txtRolledTipHeight.Text = DemoFormRef.DemoConfig.ImageSize.RolledTipHeight.ToString();
            txtRolledJointWidth.Text = DemoFormRef.DemoConfig.ImageSize.RolledJointWidth.ToString();
            txtRolledJointHeight.Text = DemoFormRef.DemoConfig.ImageSize.RolledJointHeight.ToString();
            txtFlatJointWidth.Text = DemoFormRef.DemoConfig.ImageSize.FlatJointWidth.ToString();
            txtFlatJointHeight.Text = DemoFormRef.DemoConfig.ImageSize.FlatJointHeight.ToString();
            txtRolledThenarWidth.Text = DemoFormRef.DemoConfig.ImageSize.RolledThenarWidth.ToString();
            txtRolledThenarHeight.Text = DemoFormRef.DemoConfig.ImageSize.RolledThenarHeight.ToString();
            // 1.15.0.0
            txtRolledHypothenarWidth.Text = DemoFormRef.DemoConfig.ImageSize.RolledHypothenarWidth.ToString();
            txtRolledHypothenarHeight.Text = DemoFormRef.DemoConfig.ImageSize.RolledHypothenarHeight.ToString();
            // 2.4.0.0
            txtRolledUpWidth.Text = DemoFormRef.DemoConfig.ImageSize.RolledUpWidth.ToString();
            txtRolledUpHeight.Text = DemoFormRef.DemoConfig.ImageSize.RolledUpHeight.ToString();

            // enable only free values
            if ((DemoFormRef.DemoConfig.ImageSizeStandard == DemoForm.ImageSizeStandards.ANSI_NIST_ITL_1_2000)
                || (DemoFormRef.DemoConfig.ImageSizeStandard == DemoForm.ImageSizeStandards.ANSI_NIST_ITL_1_2000_INTERPOL))
            {
                txtTwoThumbsWidth.Enabled = true;
                txtTwoThumbsHeight.Enabled = true;
            }
            else if (DemoFormRef.DemoConfig.ImageSizeStandard == DemoForm.ImageSizeStandards.ISO_IEC_FCD_19794_4)
            {
                txtFlatThumbWidth.Enabled = true;
                txtFlatThumbHeight.Enabled = true;
            }
            else if (DemoFormRef.DemoConfig.ImageSizeStandard == DemoForm.ImageSizeStandards.Custom)
            {
                // enable all
                txtUpperPalmWidth.Enabled = true;
                txtUpperPalmHeight.Enabled = true;
                txtLowerPalmWidth.Enabled = true;
                txtLowerPalmHeight.Enabled = true;
                txtWritersPalmWidth.Enabled = true;
                txtWritersPalmHeight.Enabled = true;
                txtFourFingersWidth.Enabled = true;
                txtFourFingersHeight.Enabled = true;
                txtTwoThumbsWidth.Enabled = true;
                txtTwoThumbsHeight.Enabled = true;
                txtFlatThumbWidth.Enabled = true;
                txtFlatThumbHeight.Enabled = true;
                txtRolledThumbWidth.Enabled = true;
                txtRolledThumbHeight.Enabled = true;
                txtRolledIndexWidth.Enabled = true;
                txtRolledIndexHeight.Enabled = true;
                txtRolledMiddleWidth.Enabled = true;
                txtRolledMiddleHeight.Enabled = true;
                txtRolledRingWidth.Enabled = true;
                txtRolledRingHeight.Enabled = true;
                txtRolledLittleWidth.Enabled = true;
                txtRolledLittleHeight.Enabled = true;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (chkAutocapture.Checked)
                DemoFormRef.DemoConfig.AcquisitionOptions |= GBMSGUI.AcquisitionOption.Autocapture;
            else
                DemoFormRef.DemoConfig.AcquisitionOptions &= ~GBMSGUI.AcquisitionOption.Autocapture;

            if (chkBlockAutocapture.Checked)
                DemoFormRef.DemoConfig.AcquisitionOptions |= GBMSGUI.AcquisitionOption.BlockAutocapture;
            else
                DemoFormRef.DemoConfig.AcquisitionOptions &= ~GBMSGUI.AcquisitionOption.BlockAutocapture;

            if (chkFullResPreview.Checked)
                DemoFormRef.DemoConfig.AcquisitionOptions |= GBMSGUI.AcquisitionOption.FullResPreview;
            else
                DemoFormRef.DemoConfig.AcquisitionOptions &= ~GBMSGUI.AcquisitionOption.FullResPreview;

            if (chkRotateFinger.Checked)
                DemoFormRef.DemoConfig.AcquisitionOptions |= GBMSGUI.AcquisitionOption.RotateFinger;
            else
                DemoFormRef.DemoConfig.AcquisitionOptions &= ~GBMSGUI.AcquisitionOption.RotateFinger;

            if (chkPalmPrintQualityCalculation.Checked)
                DemoFormRef.DemoConfig.AcquisitionOptions |= GBMSGUI.AcquisitionOption.PalmPrintQualityCalculation;
            else
                DemoFormRef.DemoConfig.AcquisitionOptions &= ~GBMSGUI.AcquisitionOption.PalmPrintQualityCalculation;

            if (optNoRollPreview.Checked)
                DemoFormRef.DemoConfig.AcquisitionOptions |= GBMSGUI.AcquisitionOption.NoRollPreview;
            else
                DemoFormRef.DemoConfig.AcquisitionOptions &= ~GBMSGUI.AcquisitionOption.NoRollPreview;

            if (optRollPreviewManual.Checked)
                DemoFormRef.DemoConfig.AcquisitionOptions |= GBMSGUI.AcquisitionOption.RollPreviewManualStop;
            else
                DemoFormRef.DemoConfig.AcquisitionOptions &= ~GBMSGUI.AcquisitionOption.RollPreviewManualStop;

            if (optRollPreviewCenter.Checked)
                DemoFormRef.DemoConfig.AcquisitionOptions |= GBMSGUI.AcquisitionOption.RollPreviewType;
            else
                DemoFormRef.DemoConfig.AcquisitionOptions &= ~GBMSGUI.AcquisitionOption.RollPreviewType;

            if (chkSound.Checked)
                DemoFormRef.DemoConfig.AcquisitionOptions |= GBMSGUI.AcquisitionOption.Sound;
            else
                DemoFormRef.DemoConfig.AcquisitionOptions &= ~GBMSGUI.AcquisitionOption.Sound;

            // 2.4.0.0
            if (chkSoundOnRollPreviewEnd.Checked)
                DemoFormRef.DemoConfig.AcquisitionOptions |= GBMSGUI.AcquisitionOption.SoundOnRollPreviewEnd;
            else
                DemoFormRef.DemoConfig.AcquisitionOptions &= ~GBMSGUI.AcquisitionOption.SoundOnRollPreviewEnd;

            // 2.4.0.0 - removed
            /*
            if (chkHighSpeedPreview.Checked)
                DemoFormRef.DemoConfig.AcquisitionOptions |= GBMSGUI.AcquisitionOption.HighSpeedPreview;
            else
                DemoFormRef.DemoConfig.AcquisitionOptions &= ~GBMSGUI.AcquisitionOption.HighSpeedPreview;
            */

            // 2.0.1.0
            if (chkNoArtefactsDisplay.Checked)
                DemoFormRef.DemoConfig.AcquisitionOptions |= GBMSGUI.AcquisitionOption.NoArtefactsDisplay;
            else
                DemoFormRef.DemoConfig.AcquisitionOptions &= ~GBMSGUI.AcquisitionOption.NoArtefactsDisplay;

            if (chkSequenceCheck.Checked)
                DemoFormRef.DemoConfig.SessionOptions |= GBMSGUI.SessionOption.SequenceCheck;
            else
                DemoFormRef.DemoConfig.SessionOptions &= ~GBMSGUI.SessionOption.SequenceCheck;
            
            if (chkAskUnavailabilityReason.Checked)
                DemoFormRef.DemoConfig.SessionOptions |= GBMSGUI.SessionOption.AskUnavailabilityReason;
            else
                DemoFormRef.DemoConfig.SessionOptions &= ~GBMSGUI.SessionOption.AskUnavailabilityReason;

            if (chkDetectInvalidPattern.Checked)
                DemoFormRef.DemoConfig.AcquisitionOptions |= GBMSGUI.AcquisitionOption.DetectInvalidPattern;
            else
                DemoFormRef.DemoConfig.AcquisitionOptions &= ~GBMSGUI.AcquisitionOption.DetectInvalidPattern;

            if (chkDetectIncompletePattern.Checked)
                DemoFormRef.DemoConfig.AcquisitionOptions |= GBMSGUI.AcquisitionOption.DetectIncompletePattern;
            else
                DemoFormRef.DemoConfig.AcquisitionOptions &= ~GBMSGUI.AcquisitionOption.DetectIncompletePattern;

            if (chkDetectInclination.Checked)
                DemoFormRef.DemoConfig.AcquisitionOptions |= GBMSGUI.AcquisitionOption.DetectInclination;
            else
                DemoFormRef.DemoConfig.AcquisitionOptions &= ~GBMSGUI.AcquisitionOption.DetectInclination;

            if (chkAutoClearOutsideRoll.Checked)
                DemoFormRef.DemoConfig.AcquisitionOptions |= GBMSGUI.AcquisitionOption.AutoClearOutsideRoll;
            else
                DemoFormRef.DemoConfig.AcquisitionOptions &= ~GBMSGUI.AcquisitionOption.AutoClearOutsideRoll;

            if (chkSegmentsQualityEval.Checked)
                DemoFormRef.DemoConfig.AcquisitionOptions |= GBMSGUI.AcquisitionOption.LiveSegmentsEval;
            else
                DemoFormRef.DemoConfig.AcquisitionOptions &= ~GBMSGUI.AcquisitionOption.LiveSegmentsEval;

            if (chkAutoIncompleteSlapSegm.Checked)
                DemoFormRef.DemoConfig.AcquisitionOptions |= GBMSGUI.AcquisitionOption.AutoIncompleteSlapSegm;
            else
                DemoFormRef.DemoConfig.AcquisitionOptions &= ~GBMSGUI.AcquisitionOption.AutoIncompleteSlapSegm;

            if (chkAdaptRollAreaPosition.Checked)
                DemoFormRef.DemoConfig.AcquisitionOptions |= GBMSGUI.AcquisitionOption.AdaptRollAreaPosition;
            else
                DemoFormRef.DemoConfig.AcquisitionOptions &= ~GBMSGUI.AcquisitionOption.AdaptRollAreaPosition;

            // StripeAcquisition not yet certified
            /*
            if (chkEnableRollStripeAcquisition.Checked)
                DemoFormRef.DemoConfig.AcquisitionOptions |= GBMSGUI.AcquisitionOption.EnableRollStripeAcquisition;
            else
                DemoFormRef.DemoConfig.AcquisitionOptions &= ~GBMSGUI.AcquisitionOption.EnableRollStripeAcquisition;

            if (chkEnableRollStripeFeedback.Checked)
                DemoFormRef.DemoConfig.AcquisitionOptions |= GBMSGUI.AcquisitionOption.EnableRollStripeFeedback;
            else
                DemoFormRef.DemoConfig.AcquisitionOptions &= ~GBMSGUI.AcquisitionOption.EnableRollStripeFeedback;
            */

            if (chkAutoAccept.Checked)
                DemoFormRef.DemoConfig.AcquisitionOptions |= GBMSGUI.AcquisitionOption.Autoaccept;
            else
                DemoFormRef.DemoConfig.AcquisitionOptions &= ~GBMSGUI.AcquisitionOption.Autoaccept;
            // 1.15.0.0
            if (chkDeletePalmFingerSegments.Checked)
                DemoFormRef.DemoConfig.AcquisitionOptions |= GBMSGUI.AcquisitionOption.DeletePalmFingerSegments;
            else
                DemoFormRef.DemoConfig.AcquisitionOptions &= ~GBMSGUI.AcquisitionOption.DeletePalmFingerSegments;

            // 2.4.0.0
            if (chkRemoveHalo.Checked)
                DemoFormRef.DemoConfig.AcquisitionOptions |= GBMSGUI.AcquisitionOption.RemoveHaloLatent;
            else
                DemoFormRef.DemoConfig.AcquisitionOptions &= ~GBMSGUI.AcquisitionOption.RemoveHaloLatent;

            if (chkDryFingerImageEnhancement.Checked)
                DemoFormRef.DemoConfig.SessionOptions |= GBMSGUI.SessionOption.DryFingerImageEnhancement;
            else
                DemoFormRef.DemoConfig.SessionOptions &= ~GBMSGUI.SessionOption.DryFingerImageEnhancement;

            // 2.0.0.0
            if (chkFakeFingerDetection.Checked)
                DemoFormRef.DemoConfig.SessionOptions |= GBMSGUI.SessionOption.SWFakeFingerDetection;
            else
                DemoFormRef.DemoConfig.SessionOptions &= ~GBMSGUI.SessionOption.SWFakeFingerDetection;

            // 2.0.1.0
            if (chkFFDBlocking.Checked)
                DemoFormRef.DemoConfig.SessionOptions |= GBMSGUI.SessionOption.FakeFingerDetectionBlocking;
            else
                DemoFormRef.DemoConfig.SessionOptions &= ~GBMSGUI.SessionOption.FakeFingerDetectionBlocking;

            // 2.1.0.0
            if (chkAutoCaptureBlockForDetectedFakes.Checked)
                DemoFormRef.DemoConfig.SessionOptions |= GBMSGUI.SessionOption.AutocaptureBlockForDetectedFakes;
            else
                DemoFormRef.DemoConfig.SessionOptions &= ~GBMSGUI.SessionOption.AutocaptureBlockForDetectedFakes;

            // 2.4.0.0
            if (chkDuplicationCheck.Checked)
                DemoFormRef.DemoConfig.SessionOptions |= GBMSGUI.SessionOption.DuplicationCheck;
            else
                DemoFormRef.DemoConfig.SessionOptions &= ~GBMSGUI.SessionOption.DuplicationCheck;
            
            if (chkCheckFingertipPhalange.Checked)
                DemoFormRef.DemoConfig.SessionOptions |= GBMSGUI.SessionOption.CheckFingertipPhalange;
            else
                DemoFormRef.DemoConfig.SessionOptions &= ~GBMSGUI.SessionOption.CheckFingertipPhalange;

            if (chkCheckFingerUpsideDown.Checked)
                DemoFormRef.DemoConfig.SessionOptions |= GBMSGUI.SessionOption.CheckFingerUpsideDown;
            else
                DemoFormRef.DemoConfig.SessionOptions &= ~GBMSGUI.SessionOption.CheckFingerUpsideDown;

            if (chkCheckFingertipPhalangeBlocking.Checked)
                DemoFormRef.DemoConfig.SessionOptions |= GBMSGUI.SessionOption.CheckFingertipPhalangeBlocking;
            else
                DemoFormRef.DemoConfig.SessionOptions &= ~GBMSGUI.SessionOption.CheckFingertipPhalangeBlocking;

            if (chkCheckFingerUpsideDownBlocking.Checked)
                DemoFormRef.DemoConfig.SessionOptions |= GBMSGUI.SessionOption.CheckFingerUpsideDownBlocking;
            else
                DemoFormRef.DemoConfig.SessionOptions &= ~GBMSGUI.SessionOption.CheckFingerUpsideDownBlocking;

            if (chkSequenceCheckBlocking.Checked)
                DemoFormRef.DemoConfig.SessionOptions |= GBMSGUI.SessionOption.SequenceCheckBlocking;
            else
                DemoFormRef.DemoConfig.SessionOptions &= ~GBMSGUI.SessionOption.SequenceCheckBlocking;

            if (chkDuplicationCheckBlocking.Checked)
                DemoFormRef.DemoConfig.SessionOptions |= GBMSGUI.SessionOption.DuplicationCheckBlocking;
            else
                DemoFormRef.DemoConfig.SessionOptions &= ~GBMSGUI.SessionOption.DuplicationCheckBlocking;
            
            if (chkNoAdvancedView.Checked)
                DemoFormRef.DemoConfig.SessionOptions |= GBMSGUI.SessionOption.NoAdvancedView;
            else
                DemoFormRef.DemoConfig.SessionOptions &= ~GBMSGUI.SessionOption.NoAdvancedView;

            // settings
            DemoFormRef.DemoConfig.BlockAutocaptureContrast = chkBlockAutocaptureContrast.Checked;

            // image size
            if (!SaveImageSizeValues())
                return;

            // IAFIS quality
            DemoFormRef.DemoConfig.IAFIsQualityAlgorithm = cmbQualityAlgorithm.SelectedIndex + 1;

            // Image compression
            // 500 dpi
            if (optWSQCompression500.Checked)
                DemoFormRef.DemoConfig.ImageCompression500 = DemoForm.ImageCompressions.WSQ;
            else
                DemoFormRef.DemoConfig.ImageCompression500 = DemoForm.ImageCompressions.JPEG2000;
            // 1000 dpi
            if (optWSQCompression1000.Checked)
                DemoFormRef.DemoConfig.ImageCompression1000 = DemoForm.ImageCompressions.WSQ;
            else
                DemoFormRef.DemoConfig.ImageCompression1000 = DemoForm.ImageCompressions.JPEG2000;

            // compression rate
            // 500 dpi
            DemoFormRef.DemoConfig.WQSBitRate500 = Convert.ToDouble(txtWSQBitRate500.Text);
            DemoFormRef.DemoConfig.JPEGRate500 = Convert.ToInt32(txtJPEGRate500.Text);
            // 1000 dpi
            DemoFormRef.DemoConfig.WQSBitRate1000 = Convert.ToDouble(txtWSQBitRate1000.Text);
            DemoFormRef.DemoConfig.JPEGRate1000 = Convert.ToInt32(txtJPEGRate1000.Text);

            // roll area size
            if (optRollAreaIQS.Checked)
                DemoFormRef.DemoConfig.RollAreaSize = GBMSAPI_NET_DeviceFeatures.GBMSAPI_NET_DF_ROLL_AREA_IQS;
            else
                DemoFormRef.DemoConfig.RollAreaSize = GBMSAPI_NET_DeviceFeatures.GBMSAPI_NET_DF_ROLL_AREA_GA;

            // Language
            DemoFormRef.DemoConfig.GUILanguage = cmbLanguage.SelectedIndex;

            // finger contact evaluation mode
            DemoFormRef.DemoConfig.FingerContactEvaluationMode = cmbFingerContactEvaluationMode.SelectedIndex + 1;

            // joint segmentation
            DemoFormRef.DemoConfig.JointSegmentation = chkJointSegmentation.Checked;

            // roll direction
            if (optRollToLeft.Checked)
                DemoFormRef.DemoConfig.RollDirection = GBMSGUI.AdaptiveRollDirection.ToLeft;
            else if (optRollToRight.Checked)
                DemoFormRef.DemoConfig.RollDirection = GBMSGUI.AdaptiveRollDirection.ToRight;
            else
                DemoFormRef.DemoConfig.RollDirection = GBMSGUI.AdaptiveRollDirection.ToCenter;

            // BlockAutocapture Mask
            DemoFormRef.DemoConfig.BlockAutocaptureMask = 0;
            foreach (DiagnosticListItem Item in lstBlockAutocaptureDiag.CheckedItems)
                DemoFormRef.DemoConfig.BlockAutocaptureMask |= Item.Mask;

            // IgnoreDiagnostic Mask
            DemoFormRef.DemoConfig.IgnoredDiagnosticMask = 0;
            foreach (DiagnosticListItem Item in lstIgnoredDiag.CheckedItems)
                DemoFormRef.DemoConfig.IgnoredDiagnosticMask |= Item.Mask;

            DemoFormRef.DemoConfig.EnableBlockComposition = chkEnableBlockComposition.Checked;
            DemoFormRef.DemoConfig.EnableBlockAutocaptureLedColorFeedback = chkEnableBlockAutocaptureLedColorFeedback.Checked;
            // 1.15.0.0
            DemoFormRef.DemoConfig.LiveSegmEvalTimeout = Convert.ToInt32(txtLiveSegmEvalTimeout.Text);
            // 2.0.0.0
            DemoFormRef.DemoConfig.HWFakeFingerDetectionThreshold = Convert.ToInt32(txtHWFakeFingerDetectionThreshold.Text);
            DemoFormRef.DemoConfig.SWFakeFingerDetectionThreshold = Convert.ToInt32(txtSWFakeFingerDetectionThreshold.Text);

            // 2.3.2.0
            DemoFormRef.DemoConfig.MaxAllowedFakesInSlap4 = Convert.ToInt32(txtMaxAllowedFakesInSlap4.Text);
            DemoFormRef.DemoConfig.MaxAllowedFakesInSlap2 = Convert.ToInt32(txtMaxAllowedFakesInSlap2.Text);

            // 2.4.0.0
            DemoFormRef.DemoConfig.FlatFingerOnRollArea = chkFlatFingerOnRollArea.Checked;

            // save configuration
            DemoForm.Configuration.Serialize(Path.ChangeExtension(Application.ExecutablePath, ".cfg"), DemoFormRef.DemoConfig);

            DialogResult = DialogResult.OK;
        }

        private void chkBlockAutocapture_CheckedChanged(object sender, EventArgs e)
        {
            chkBlockAutocaptureContrast.Enabled = chkBlockAutocapture.Checked;
        }

        private void cmbStandard_SelectedIndexChanged(object sender, EventArgs e)
        {
            DemoFormRef.DemoConfig.ImageSizeStandard = cmbStandard.SelectedIndex;
            // show values
            ShowImageSizeValues();

            if (cmbStandard.SelectedIndex == DemoForm.ImageSizeStandards.GA_CHINA)
                optRollAreaGA.Checked = true;
            else
                optRollAreaIQS.Checked = true;
        }

        private bool SaveImageSizeValues()
        {
            DemoForm.ImageSizes ImageSize = DemoFormRef.DemoConfig.ImageSize;

            // change only enabled fields
            if (txtUpperPalmWidth.Enabled)
                ImageSize.UpperPalmWidth = Convert.ToDouble(txtUpperPalmWidth.Text);
            if (txtUpperPalmHeight.Enabled)
                ImageSize.UpperPalmHeight = Convert.ToDouble(txtUpperPalmHeight.Text);
            if (txtLowerPalmWidth.Enabled)
                ImageSize.LowerPalmWidth = Convert.ToDouble(txtLowerPalmWidth.Text);
            if (txtLowerPalmHeight.Enabled)
                ImageSize.LowerPalmHeight = Convert.ToDouble(txtLowerPalmHeight.Text);
            if (txtWritersPalmWidth.Enabled)
                ImageSize.WritersPalmWidth = Convert.ToDouble(txtWritersPalmWidth.Text);
            if (txtWritersPalmHeight.Enabled)
                ImageSize.WritersPalmHeight = Convert.ToDouble(txtWritersPalmHeight.Text);
            if (txtFourFingersWidth.Enabled)
                ImageSize.FourFingersWidth = Convert.ToDouble(txtFourFingersWidth.Text);
            if (txtFourFingersHeight.Enabled)
                ImageSize.FourFingersHeight = Convert.ToDouble(txtFourFingersHeight.Text);
            if (txtTwoThumbsWidth.Enabled)
                ImageSize.TwoThumbsWidth = Convert.ToDouble(txtTwoThumbsWidth.Text);
            if (txtTwoThumbsHeight.Enabled)
                ImageSize.TwoThumbsHeight = Convert.ToDouble(txtTwoThumbsHeight.Text);
            if (txtFlatThumbWidth.Enabled)
                ImageSize.FlatThumbWidth = Convert.ToDouble(txtFlatThumbWidth.Text);
            if (txtFlatThumbHeight.Enabled)
                ImageSize.FlatThumbHeight = Convert.ToDouble(txtFlatThumbHeight.Text);
            if (txtFlatFingerWidth.Enabled)
                ImageSize.FlatFingerWidth = Convert.ToDouble(txtFlatFingerWidth.Text);
            if (txtFlatFingerHeight.Enabled)
                ImageSize.FlatFingerHeight = Convert.ToDouble(txtFlatFingerHeight.Text);
            if (txtRolledThumbWidth.Enabled)
                ImageSize.RolledThumbWidth = Convert.ToDouble(txtRolledThumbWidth.Text);
            if (txtRolledThumbHeight.Enabled)
                ImageSize.RolledThumbHeight = Convert.ToDouble(txtRolledThumbHeight.Text);
            if (txtRolledIndexWidth.Enabled)
                ImageSize.RolledIndexWidth = Convert.ToDouble(txtRolledIndexWidth.Text);
            if (txtRolledIndexHeight.Enabled)
                ImageSize.RolledIndexHeight = Convert.ToDouble(txtRolledIndexHeight.Text);
            if (txtRolledMiddleWidth.Enabled)
                ImageSize.RolledMiddleWidth = Convert.ToDouble(txtRolledMiddleWidth.Text);
            if (txtRolledMiddleHeight.Enabled)
                ImageSize.RolledMiddleHeight = Convert.ToDouble(txtRolledMiddleHeight.Text);
            if (txtRolledRingWidth.Enabled)
                ImageSize.RolledRingWidth = Convert.ToDouble(txtRolledRingWidth.Text);
            if (txtRolledRingHeight.Enabled)
                ImageSize.RolledRingHeight = Convert.ToDouble(txtRolledRingHeight.Text);
            if (txtRolledLittleWidth.Enabled)
                ImageSize.RolledLittleWidth = Convert.ToDouble(txtRolledLittleWidth.Text);
            if (txtRolledLittleHeight.Enabled)
                ImageSize.RolledLittleHeight = Convert.ToDouble(txtRolledLittleHeight.Text);
            //V1.10
            if (txtRolledTipWidth.Enabled)
                ImageSize.RolledTipWidth = Convert.ToDouble(txtRolledTipWidth.Text);
            if (txtRolledTipHeight.Enabled)
                ImageSize.RolledTipHeight = Convert.ToDouble(txtRolledTipHeight.Text);
            if (txtRolledJointWidth.Enabled)
                ImageSize.RolledJointWidth = Convert.ToDouble(txtRolledJointWidth.Text);
            if (txtRolledJointHeight.Enabled)
                ImageSize.RolledJointHeight = Convert.ToDouble(txtRolledJointHeight.Text);
            if (txtFlatJointWidth.Enabled)
                ImageSize.FlatJointWidth = Convert.ToDouble(txtFlatJointWidth.Text);
            if (txtFlatJointHeight.Enabled)
                ImageSize.FlatJointHeight = Convert.ToDouble(txtFlatJointHeight.Text);
            if (txtRolledThenarWidth.Enabled)
                ImageSize.RolledThenarWidth = Convert.ToDouble(txtRolledThenarWidth.Text);
            if (txtRolledThenarHeight.Enabled)
                ImageSize.RolledThenarHeight = Convert.ToDouble(txtRolledThenarHeight.Text);
            // 1.15.0.0
            if (txtRolledHypothenarWidth.Enabled)
                ImageSize.RolledHypothenarWidth = Convert.ToDouble(txtRolledHypothenarWidth.Text);
            if (txtRolledHypothenarHeight.Enabled)
                ImageSize.RolledHypothenarHeight = Convert.ToDouble(txtRolledHypothenarHeight.Text);
            // 2.4.0.0
            if (txtRolledUpWidth.Enabled)
                ImageSize.RolledUpWidth = Convert.ToDouble(txtRolledUpWidth.Text);
            if (txtRolledUpHeight.Enabled)
                ImageSize.RolledUpHeight = Convert.ToDouble(txtRolledUpHeight.Text);

            DemoFormRef.DemoConfig.ImageSize =  ImageSize;

            return true;
        }

        private void SizeValidate(object sender, CancelEventArgs e)
        {
            // check only that value is ranged between 0.4 and 5
            if (!CheckDoubleField((TextBox)sender, 0.4, 5))
            {
                e.Cancel = true;
                return;
            }

            e.Cancel = false;
        }

        private void WSQBitRateValidate(object sender, CancelEventArgs e)
        {
            // check that value is ranged between 0 and 1
            if (!CheckDoubleField((TextBox)sender, 0, 1))
            {
                e.Cancel = true;
                return;
            }
            e.Cancel = false;
        }

        private void JPEGRateValidate(object sender, CancelEventArgs e)
        {
            // check only that value is ranged between 1 and 100
            if (!CheckIntField((TextBox)sender, 1, 100))
            {
                e.Cancel = true;
                return;
            }

            e.Cancel = false;
        }

        private bool CheckDoubleField(TextBox txtBox, double MinVal, double MaxVal)
        {
            double Value = 0;

            try
            {
                Value = Convert.ToDouble(txtBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBox.Focus();
                return false;
            }

            // check only that value is ranged between MinVal and MaxVal
            if ((Value < MinVal) || (Value > MaxVal))
            {
                MessageBox.Show("Value out of range", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBox.Focus();
                return false;
            }

            return true;
        }

        private bool CheckIntField(TextBox txtBox, int MinVal, int MaxVal)
        {
            int Value = 0;

            try
            {
                Value = Convert.ToInt32(txtBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBox.Focus();
                return false;
            }

            // check  that value is ranged between MinVal and MaxVal
            if ((Value < MinVal) || (Value > MaxVal))
            {
                MessageBox.Show("Value out of range", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBox.Focus();
                return false;
            }

            return true;
        }


        private void lstDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            // show modes list according to device
            int DeviceID = DemoForm.GetDeviceIDFromName(lstDevice.Items[lstDevice.SelectedIndex].ToString());
            int ScanMode = 0;

            // save current selection
            if (optFullHiRes.Checked)
                ScanMode = ScanModes.FullHiRes;
            else if (optFullLowRes.Checked)
                ScanMode = ScanModes.FullLowRes;
            else if (optRollIQS.Checked)
                ScanMode = ScanModes.RollIQS;
            else if (optRollGA.Checked)
                ScanMode = ScanModes.RollGA;
            else if (optRollJoint.Checked)
                ScanMode = ScanModes.RollJoint;
            else if (optRollThenar.Checked)
                ScanMode = ScanModes.RollThenar;

            if ((DeviceID == GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_DS84C) ||
                (DeviceID == GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_MC517) ||
                (DeviceID == GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_MS527) ||
                (DeviceID == GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_DS84t) ||
                (DeviceID == GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_MS527t)) // 2.3.0.0
                optFullLowRes.Enabled = true;
            else
                optFullLowRes.Enabled = false;

            if ((DeviceID == GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_DS26) ||
                (DeviceID == GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_DS32) ||
                (DeviceID == GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_DSID20))
                optRollIQS.Enabled = false;
            else
                optRollIQS.Enabled = true;

            if ((DeviceID == GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_MC517) ||
                (DeviceID == GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_DS32) ||
                (DeviceID == GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_MS527) ||
                (DeviceID == GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_MS527t)) // 2.3.0.0
                optRollGA.Enabled = true;
            else
                optRollGA.Enabled = false;

            if ((DeviceID == GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_MS527)
                || (DeviceID == GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_MS527t))  // 2.3.0.0
            {
                optRollJoint.Enabled = true;
                optRollThenar.Enabled = true;
            }
            else
            {
                optRollJoint.Enabled = false;
                optRollThenar.Enabled = false;
            }

            // restore selection (if possible)
            // or set selection to Full Hi res (always available)
            if ((ScanMode == ScanModes.FullLowRes) && optFullLowRes.Enabled)
                optFullLowRes.Checked = true;
            else if ((ScanMode == ScanModes.RollIQS) && optRollIQS.Enabled)
                optRollIQS.Checked = true;
            else if ((ScanMode == ScanModes.RollGA) && optRollGA.Enabled)
                optRollGA.Checked = true;
            else if ((ScanMode == ScanModes.RollJoint) && optRollJoint.Enabled)
                optRollJoint.Checked = true;
            else if ((ScanMode == ScanModes.RollThenar) && optRollThenar.Enabled)
                optRollThenar.Checked = true;
            else
                optFullHiRes.Checked = true;

            // refresh the view
            optScanMode_CheckedChanged(this, null);
        }

        private void optScanMode_CheckedChanged(object sender, EventArgs e)
        {
            int DeviceID = DemoForm.GetDeviceIDFromName(lstDevice.Items[lstDevice.SelectedIndex].ToString());
            int ScanMode = 0;
            uint FrameRateOptions = 0;
		    double MaxFrameRate;
		    double MinFrameRate;
            double DefFrameRate;
            //uint RollAreaStandard = 0;
            uint ScanArea = 0;

            if (optFullHiRes.Checked)
            {
                ScanMode = ScanModes.FullHiRes;
                FrameRateOptions |= GBMSAPI_NET_FrameRateOptions.GBMSAPI_NET_FRO_FULL_RESOLUTION_MODE;
                //FrameRateOptions |= GBMSAPI_NET_FrameRateOptions.GBMSAPI_NET_FRO_HIGH_RES_IN_PREVIEW;
                ScanArea = GBMSAPI_NET_ScanAreas.GBMSAPI_NET_SA_FULL_FRAME;
            }
            else if (optFullLowRes.Checked)
            {
                ScanMode = ScanModes.FullLowRes;
                ScanArea = GBMSAPI_NET_ScanAreas.GBMSAPI_NET_SA_FULL_FRAME;
            }
            else if (optRollIQS.Checked)
            {
                //FrameRateOptions |= GBMSAPI_NET_FrameRateOptions.GBMSAPI_NET_FRO_ROLL_AREA;
                ScanMode = ScanModes.RollIQS;
                //RollAreaStandard = GBMSAPI_NET_DeviceFeatures.GBMSAPI_NET_DF_ROLL_AREA_IQS;
                ScanArea = GBMSAPI_NET_ScanAreas.GBMSAPI_NET_SA_ROLL_IQS;
            }
            else if (optRollGA.Checked)
            {
                //FrameRateOptions |= GBMSAPI_NET_FrameRateOptions.GBMSAPI_NET_FRO_ROLL_AREA;
                ScanMode = ScanModes.RollGA;
                //RollAreaStandard = GBMSAPI_NET_DeviceFeatures.GBMSAPI_NET_DF_ROLL_AREA_GA;
                ScanArea = GBMSAPI_NET_ScanAreas.GBMSAPI_NET_SA_ROLL_GA;
            }
            else if (optRollJoint.Checked)
            {
                ScanMode = ScanModes.RollJoint;
                ScanArea = GBMSAPI_NET_ScanAreas.GBMSAPI_NET_SA_ROLL_JOINT;
            }
            else if (optRollThenar.Checked)
            {
                ScanMode = ScanModes.RollThenar;
                ScanArea = GBMSAPI_NET_ScanAreas.GBMSAPI_NET_SA_ROLL_THENAR;
            }

            //GBMSAPI_NET_ScanSettingsRoutines.GBMSAPI_NET_GetFrameRateRange_Global((byte)DeviceID, FrameRateOptions, RollAreaStandard,
            //    out MaxFrameRate, out MinFrameRate, out DefFrameRate);
            GBMSAPI_NET_ScanSettingsRoutines.GBMSAPI_NET_GetFrameRateRange2((byte)DeviceID, FrameRateOptions, ScanArea,
                out MaxFrameRate, out MinFrameRate, out DefFrameRate);
            lblFrameRateRange.Text = "(" + MinFrameRate.ToString() + " - " + MaxFrameRate.ToString() + ")";

            switch (DeviceID)
            {
                case GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_DS26:
                    txtFrameRate.Text = DemoFormRef.DemoConfig.DS26FrameRate.ToString();
                    //lblFrameRateRange.Text = "(" + Convert.ToDouble(GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_DS26_FLAT_FLAT_FULLRES_MIN_FRAME_RATE).ToString()
                    //    + " - " + Convert.ToDouble(GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_DS26_FLAT_FLAT_FULLRES_MAX_FRAME_RATE).ToString() + ")";
                    break;

                case GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_DS84:
                    switch (ScanMode)
                    {
                        case ScanModes.FullHiRes:
                            txtFrameRate.Text = DemoFormRef.DemoConfig.DS84FullFrameRate.ToString();
                            //lblFrameRateRange.Text = "(" + Convert.ToDouble(GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_DS84_FLAT_FLAT_FULLRES_MIN_FRAME_RATE).ToString()
                            //    + " - " + Convert.ToDouble(GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_DS84_FLAT_FLAT_FULLRES_MAX_FRAME_RATE).ToString() + ")";
                            break;
                        case ScanModes.RollIQS:
                            txtFrameRate.Text = DemoFormRef.DemoConfig.DS84PartialFrameRate.ToString();
                            //lblFrameRateRange.Text = "(" + Convert.ToDouble(GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_DS84_ROLL_FLAT_FULLRES_MIN_FRAME_RATE).ToString()
                            //    + " - " + Convert.ToDouble(GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_DS84_ROLL_FLAT_FULLRES_MAX_FRAME_RATE).ToString() + ")";
                            break;
                    }
                    break;

                case GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_DS40I:
                    switch (ScanMode)
                    {
                        case ScanModes.FullHiRes:
                            txtFrameRate.Text = DemoFormRef.DemoConfig.DS40iFullFrameRate.ToString();
                            //lblFrameRateRange.Text = "(" + Convert.ToDouble(GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_DS40I_FLAT_FLAT_FULLRES_MIN_FRAME_RATE).ToString()
                            //    + " - " + Convert.ToDouble(GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_DS40I_FLAT_FLAT_FULLRES_MAX_FRAME_RATE).ToString() + ")";
                            break;
                        case ScanModes.RollIQS:
                            txtFrameRate.Text = DemoFormRef.DemoConfig.DS40iPartialFrameRate.ToString();
                            //lblFrameRateRange.Text = "(" + Convert.ToDouble(GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_DS40I_ROLL_ROLL_FULLRES_MIN_FRAME_RATE).ToString()
                            //    + " - " + Convert.ToDouble(GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_DS40I_ROLL_ROLL_FULLRES_MAX_FRAME_RATE).ToString() + ")";
                            break;
                    }
                    break;

                case GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_DS84C:
                    switch (ScanMode)
                    {
                        case ScanModes.FullHiRes:
                            txtFrameRate.Text = DemoFormRef.DemoConfig.DS84cFullHiResFrameRate.ToString();
                            //lblFrameRateRange.Text = "(" + Convert.ToDouble(GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_DS84C_FLAT_FLAT_FULLRES_MIN_FRAME_RATE).ToString()
                            //    + " - " + Convert.ToDouble(GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_DS84C_FLAT_FLAT_FULLRES_MAX_FRAME_RATE).ToString() + ")";
                            break;
                        case ScanModes.FullLowRes:
                            txtFrameRate.Text = DemoFormRef.DemoConfig.DS84cFullLowResFrameRate.ToString();
                            //lblFrameRateRange.Text = "(" + Convert.ToDouble(GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_DS84C_FLAT_FLAT_PREVIEW_MIN_FRAME_RATE).ToString()
                            //    + " - " + Convert.ToDouble(GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_DS84C_FLAT_FLAT_PREVIEW_MAX_FRAME_RATE).ToString() + ")";
                            break;
                        case ScanModes.RollIQS:
                            txtFrameRate.Text = DemoFormRef.DemoConfig.DS84cPartialFrameRate.ToString();
                            //lblFrameRateRange.Text = "(" + Convert.ToDouble(GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_DS84C_ROLL_ROLL_FULLRES_MIN_FRAME_RATE).ToString()
                            //    + " - " + Convert.ToDouble(GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_DS84C_ROLL_ROLL_FULLRES_MAX_FRAME_RATE).ToString() + ")";
                            break;
                    }
                    break;

                case GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_MC517:
                    switch (ScanMode)
                    {
                        case ScanModes.FullHiRes:
                            txtFrameRate.Text = DemoFormRef.DemoConfig.MC517FullHiResFrameRate.ToString();
                            //lblFrameRateRange.Text = "(" + Convert.ToDouble(GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_MC517_FLAT_FLAT_FULLRES_MIN_FRAME_RATE).ToString()
                            //    + " - " + Convert.ToDouble(GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_MC517_FLAT_FLAT_FULLRES_MAX_FRAME_RATE).ToString() + ")";
                            break;
                        case ScanModes.FullLowRes:
                            txtFrameRate.Text = DemoFormRef.DemoConfig.MC517FullLowResFrameRate.ToString();
                            //lblFrameRateRange.Text = "(" + Convert.ToDouble(GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_MC517_FLAT_FLAT_PREVIEW_MIN_FRAME_RATE).ToString()
                            //    + " - " + Convert.ToDouble(GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_MC517_FLAT_FLAT_PREVIEW_MAX_FRAME_RATE).ToString() + ")";
                            break;
                        case ScanModes.RollIQS:
                            txtFrameRate.Text = DemoFormRef.DemoConfig.MC517PartialIQSFrameRate.ToString();
                            //lblFrameRateRange.Text = "(" + Convert.ToDouble(GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_MC517_ROLL_ROLL_FULLRES_MIN_FRAME_RATE_IQS).ToString()
                            //    + " - " + Convert.ToDouble(GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_MC517_ROLL_ROLL_FULLRES_MAX_FRAME_RATE_IQS).ToString() + ")";
                            break;
                        case ScanModes.RollGA:
                            txtFrameRate.Text = DemoFormRef.DemoConfig.MC517PartialGAFrameRate.ToString();
                            //lblFrameRateRange.Text = "(" + Convert.ToDouble(GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_MC517_ROLL_ROLL_FULLRES_MIN_FRAME_RATE_GA).ToString()
                            //    + " - " + Convert.ToDouble(GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_MC517_ROLL_ROLL_FULLRES_MAX_FRAME_RATE_GA).ToString() + ")";
                            break;
                    }
                    break;

                case GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_DS32:
                    switch (ScanMode)
                    {
                        case ScanModes.FullHiRes:
                            txtFrameRate.Text = DemoFormRef.DemoConfig.DS32FullFrameRate.ToString();
                            break;
                        //case ScanModes.RollIQS:
                        case ScanModes.RollGA:
                            txtFrameRate.Text = DemoFormRef.DemoConfig.DS32PartialFrameRate.ToString();
                            break;
                    }
                    break;

                case GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_MS527:
                    switch (ScanMode)
                    {
                        case ScanModes.FullHiRes:
                            txtFrameRate.Text = DemoFormRef.DemoConfig.MS527FullHiResFrameRate.ToString();
                            break;
                        case ScanModes.FullLowRes:
                            txtFrameRate.Text = DemoFormRef.DemoConfig.MS527FullLowResFrameRate.ToString();
                            break;
                        case ScanModes.RollIQS:
                            txtFrameRate.Text = DemoFormRef.DemoConfig.MS527PartialIQSFrameRate.ToString();
                            break;
                        case ScanModes.RollGA:
                            txtFrameRate.Text = DemoFormRef.DemoConfig.MS527PartialGAFrameRate.ToString();
                            break;
                        case ScanModes.RollJoint:
                            txtFrameRate.Text = DemoFormRef.DemoConfig.MS527PartialJointFrameRate.ToString();
                            break;
                        case ScanModes.RollThenar:
                            txtFrameRate.Text = DemoFormRef.DemoConfig.MS527PartialThenarFrameRate.ToString();
                            break;
                    }
                    break;

                case GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_DS84t:
                    switch (ScanMode)
                    {
                        case ScanModes.FullHiRes:
                            txtFrameRate.Text = DemoFormRef.DemoConfig.DS84tFullHiResFrameRate.ToString();
                            break;
                        case ScanModes.FullLowRes:
                            txtFrameRate.Text = DemoFormRef.DemoConfig.DS84tFullLowResFrameRate.ToString();
                            break;
                        case ScanModes.RollIQS:
                            txtFrameRate.Text = DemoFormRef.DemoConfig.DS84tPartialFrameRate.ToString();
                            break;
                    }
                    break;

                case GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_DSID20:  // 2.0.1.0
                    txtFrameRate.Text = DemoFormRef.DemoConfig.DID20FrameRate.ToString();
                    break;

                case GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_MS527t:  // 2.3.0.0
                    switch (ScanMode)
                    {
                        case ScanModes.FullHiRes:
                            txtFrameRate.Text = DemoFormRef.DemoConfig.MS527tFullHiResFrameRate.ToString();
                            break;
                        case ScanModes.FullLowRes:
                            txtFrameRate.Text = DemoFormRef.DemoConfig.MS527tFullLowResFrameRate.ToString();
                            break;
                        case ScanModes.RollIQS:
                            txtFrameRate.Text = DemoFormRef.DemoConfig.MS527tPartialIQSFrameRate.ToString();
                            break;
                        case ScanModes.RollGA:
                            txtFrameRate.Text = DemoFormRef.DemoConfig.MS527tPartialGAFrameRate.ToString();
                            break;
                        case ScanModes.RollJoint:
                            txtFrameRate.Text = DemoFormRef.DemoConfig.MS527tPartialJointFrameRate.ToString();
                            break;
                        case ScanModes.RollThenar:
                            txtFrameRate.Text = DemoFormRef.DemoConfig.MS527tPartialThenarFrameRate.ToString();
                            break;
                    }
                    break;

            }
        }

        private void txtFrameRate_Validating(object sender, CancelEventArgs e)
        {
            double MinVal, MaxVal;
            uint FrameRateOptions = 0;
            double DefFrameRate;
            //uint RollAreaStandard = 0;
            uint ScanArea = 0;

            int DeviceID = DemoForm.GetDeviceIDFromName(lstDevice.Items[lstDevice.SelectedIndex].ToString());
            int ScanMode = 0;

            if (optFullHiRes.Checked)
            {
                ScanMode = ScanModes.FullHiRes;
                FrameRateOptions |= GBMSAPI_NET_FrameRateOptions.GBMSAPI_NET_FRO_FULL_RESOLUTION_MODE;
                ScanArea = GBMSAPI_NET_ScanAreas.GBMSAPI_NET_SA_FULL_FRAME;
            }
            else if (optFullLowRes.Checked)
            {
                ScanMode = ScanModes.FullLowRes;
                ScanArea = GBMSAPI_NET_ScanAreas.GBMSAPI_NET_SA_FULL_FRAME;
            }
            else if (optRollIQS.Checked)
            {
                //FrameRateOptions |= GBMSAPI_NET_FrameRateOptions.GBMSAPI_NET_FRO_ROLL_AREA;
                ScanMode = ScanModes.RollIQS;
                //RollAreaStandard = GBMSAPI_NET_DeviceFeatures.GBMSAPI_NET_DF_ROLL_AREA_IQS;
                ScanArea = GBMSAPI_NET_ScanAreas.GBMSAPI_NET_SA_ROLL_IQS;
            }
            else if (optRollGA.Checked)
            {
                //FrameRateOptions |= GBMSAPI_NET_FrameRateOptions.GBMSAPI_NET_FRO_ROLL_AREA;
                ScanMode = ScanModes.RollGA;
                //RollAreaStandard = GBMSAPI_NET_DeviceFeatures.GBMSAPI_NET_DF_ROLL_AREA_GA;
                ScanArea = GBMSAPI_NET_ScanAreas.GBMSAPI_NET_SA_ROLL_GA;
            }
            else if (optRollJoint.Checked)
            {
                ScanMode = ScanModes.RollJoint;
                ScanArea = GBMSAPI_NET_ScanAreas.GBMSAPI_NET_SA_ROLL_JOINT;
            }
            else if (optRollThenar.Checked)
            {
                ScanMode = ScanModes.RollThenar;
                ScanArea = GBMSAPI_NET_ScanAreas.GBMSAPI_NET_SA_ROLL_THENAR;
            }

            //GBMSAPI_NET_ScanSettingsRoutines.GBMSAPI_NET_GetFrameRateRange_Global((byte)DeviceID, FrameRateOptions, RollAreaStandard,
            //    out MaxVal, out MinVal, out DefFrameRate);
            GBMSAPI_NET_ScanSettingsRoutines.GBMSAPI_NET_GetFrameRateRange2((byte)DeviceID, FrameRateOptions, ScanArea,
                out MaxVal, out MinVal, out DefFrameRate);

            /*
            switch (DeviceID)
            {
                case GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_DS26:
                    MinVal = GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_DS26_FLAT_FLAT_FULLRES_MIN_FRAME_RATE;
                    MaxVal = GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_DS26_FLAT_FLAT_FULLRES_MAX_FRAME_RATE;
                    break;

                case GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_DS84:
                    switch (ScanMode)
                    {
                        case ScanModes.FullHiRes:
                            MinVal = GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_DS84_FLAT_FLAT_FULLRES_MIN_FRAME_RATE;
                            MaxVal = GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_DS84_FLAT_FLAT_FULLRES_MAX_FRAME_RATE;
                            break;
                        case ScanModes.RollIQS:
                            MinVal = GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_DS84_ROLL_FLAT_FULLRES_MIN_FRAME_RATE;
                            MaxVal = GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_DS84_ROLL_FLAT_FULLRES_MAX_FRAME_RATE;
                            break;
                    }
                    break;

                case GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_DS40I:
                    switch (ScanMode)
                    {
                        case ScanModes.FullHiRes:
                            MinVal = GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_DS40I_FLAT_FLAT_FULLRES_MIN_FRAME_RATE;
                            MaxVal = GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_DS40I_FLAT_FLAT_FULLRES_MAX_FRAME_RATE;
                            break;
                        case ScanModes.RollIQS:
                            MinVal = GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_DS40I_ROLL_ROLL_FULLRES_MIN_FRAME_RATE;
                            MaxVal = GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_DS40I_ROLL_ROLL_FULLRES_MAX_FRAME_RATE;
                            break;
                    }
                    break;

                case GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_DS84C:
                    switch (ScanMode)
                    {
                        case ScanModes.FullHiRes:
                            MinVal = GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_DS84C_FLAT_FLAT_FULLRES_MIN_FRAME_RATE;
                            MaxVal = GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_DS84C_FLAT_FLAT_FULLRES_MAX_FRAME_RATE;
                            break;
                        case ScanModes.FullLowRes:
                            MinVal = GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_DS84C_FLAT_FLAT_PREVIEW_MIN_FRAME_RATE;
                            MaxVal = GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_DS84C_FLAT_FLAT_PREVIEW_MAX_FRAME_RATE;
                            break;
                        case ScanModes.RollIQS:
                            MinVal = GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_DS84C_ROLL_ROLL_FULLRES_MIN_FRAME_RATE;
                            MaxVal = GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_DS84C_ROLL_ROLL_FULLRES_MAX_FRAME_RATE;
                            break;
                    }
                    break;

                case GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_MC517:
                    switch (ScanMode)
                    {
                        case ScanModes.FullHiRes:
                            MinVal = GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_MC517_FLAT_FLAT_FULLRES_MIN_FRAME_RATE;
                            MaxVal = GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_MC517_FLAT_FLAT_FULLRES_MAX_FRAME_RATE;
                            break;
                        case ScanModes.FullLowRes:
                            MinVal = GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_MC517_FLAT_FLAT_PREVIEW_MIN_FRAME_RATE;
                            MaxVal = GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_MC517_FLAT_FLAT_PREVIEW_MAX_FRAME_RATE;
                            break;
                        case ScanModes.RollIQS:
                            MinVal = GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_MC517_ROLL_ROLL_FULLRES_MIN_FRAME_RATE_IQS;
                            MaxVal = GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_MC517_ROLL_ROLL_FULLRES_MAX_FRAME_RATE_IQS;
                            break;
                        case ScanModes.RollGA:
                            MinVal = GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_MC517_ROLL_ROLL_FULLRES_MIN_FRAME_RATE_GA;
                            MaxVal = GBMSAPI_NET_FrameRateRangeLimits.GBMSAPI_NET_MC517_ROLL_ROLL_FULLRES_MAX_FRAME_RATE_GA;
                            break;
                    }
                    break;
            }
            */

            // check value range
            if (!CheckDoubleField((TextBox)sender, MinVal, MaxVal))
            {
                e.Cancel = true;
                return;
            }

            e.Cancel = false;
        }

        private void txtFrameRate_Validated(object sender, EventArgs e)
        {
            int DeviceID = DemoForm.GetDeviceIDFromName(lstDevice.Items[lstDevice.SelectedIndex].ToString());
            int ScanMode = 0;

            if (optFullHiRes.Checked)
                ScanMode = ScanModes.FullHiRes;
            else if (optFullLowRes.Checked)
                ScanMode = ScanModes.FullLowRes;
            else if (optRollIQS.Checked)
                ScanMode = ScanModes.RollIQS;
            else if (optRollGA.Checked)
                ScanMode = ScanModes.RollGA;
            else if (optRollJoint.Checked)
                ScanMode = ScanModes.RollJoint;
            else if (optRollThenar.Checked)
                ScanMode = ScanModes.RollThenar;

            switch (DeviceID)
            {
                case GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_DS26:
                    DemoFormRef.DemoConfig.DS26FrameRate = Convert.ToDouble(txtFrameRate.Text);
                    break;

                case GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_DS84:
                    switch (ScanMode)
                    {
                        case ScanModes.FullHiRes:
                            DemoFormRef.DemoConfig.DS84FullFrameRate = Convert.ToDouble(txtFrameRate.Text);
                            break;
                        case ScanModes.RollIQS:
                            DemoFormRef.DemoConfig.DS84PartialFrameRate = Convert.ToDouble(txtFrameRate.Text);
                            break;
                    }
                    break;

                case GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_DS40I:
                    switch (ScanMode)
                    {
                        case ScanModes.FullHiRes:
                            DemoFormRef.DemoConfig.DS40iFullFrameRate = Convert.ToDouble(txtFrameRate.Text);
                            break;
                        case ScanModes.RollIQS:
                            DemoFormRef.DemoConfig.DS40iPartialFrameRate = Convert.ToDouble(txtFrameRate.Text);
                            break;
                    }
                    break;

                case GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_DS84C:
                    switch (ScanMode)
                    {
                        case ScanModes.FullHiRes:
                            DemoFormRef.DemoConfig.DS84cFullHiResFrameRate = Convert.ToDouble(txtFrameRate.Text);
                            break;
                        case ScanModes.FullLowRes:
                            DemoFormRef.DemoConfig.DS84cFullLowResFrameRate = Convert.ToDouble(txtFrameRate.Text);
                            break;
                        case ScanModes.RollIQS:
                            DemoFormRef.DemoConfig.DS84cPartialFrameRate = Convert.ToDouble(txtFrameRate.Text);
                            break;
                    }
                    break;

                case GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_MC517:
                    switch (ScanMode)
                    {
                        case ScanModes.FullHiRes:
                            DemoFormRef.DemoConfig.MC517FullHiResFrameRate = Convert.ToDouble(txtFrameRate.Text);
                            break;
                        case ScanModes.FullLowRes:
                            DemoFormRef.DemoConfig.MC517FullLowResFrameRate = Convert.ToDouble(txtFrameRate.Text);
                            break;
                        case ScanModes.RollIQS:
                            DemoFormRef.DemoConfig.MC517PartialIQSFrameRate = Convert.ToDouble(txtFrameRate.Text);
                            break;
                        case ScanModes.RollGA:
                            DemoFormRef.DemoConfig.MC517PartialGAFrameRate = Convert.ToDouble(txtFrameRate.Text);
                            break;
                    }
                    break;

                case GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_DS32:
                    switch (ScanMode)
                    {
                        case ScanModes.FullHiRes:
                            DemoFormRef.DemoConfig.DS32FullFrameRate = Convert.ToDouble(txtFrameRate.Text);
                            break;
                        //case ScanModes.RollIQS:
                        case ScanModes.RollGA:
                            DemoFormRef.DemoConfig.DS32PartialFrameRate = Convert.ToDouble(txtFrameRate.Text);
                            break;
                    }
                    break;

                case GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_MS527:
                    switch (ScanMode)
                    {
                        case ScanModes.FullHiRes:
                            DemoFormRef.DemoConfig.MS527FullHiResFrameRate = Convert.ToDouble(txtFrameRate.Text);
                            break;
                        case ScanModes.FullLowRes:
                            DemoFormRef.DemoConfig.MS527FullLowResFrameRate = Convert.ToDouble(txtFrameRate.Text);
                            break;
                        case ScanModes.RollIQS:
                            DemoFormRef.DemoConfig.MS527PartialIQSFrameRate = Convert.ToDouble(txtFrameRate.Text);
                            break;
                        case ScanModes.RollGA:
                            DemoFormRef.DemoConfig.MS527PartialGAFrameRate = Convert.ToDouble(txtFrameRate.Text);
                            break;
                        case ScanModes.RollJoint:
                            DemoFormRef.DemoConfig.MS527PartialJointFrameRate = Convert.ToDouble(txtFrameRate.Text);
                            break;
                        case ScanModes.RollThenar:
                            DemoFormRef.DemoConfig.MS527PartialThenarFrameRate = Convert.ToDouble(txtFrameRate.Text);
                            break;
                    }
                    break;

                case GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_DS84t:
                    switch (ScanMode)
                    {
                        case ScanModes.FullHiRes:
                            DemoFormRef.DemoConfig.DS84tFullHiResFrameRate = Convert.ToDouble(txtFrameRate.Text);
                            break;
                        case ScanModes.FullLowRes:
                            DemoFormRef.DemoConfig.DS84tFullLowResFrameRate = Convert.ToDouble(txtFrameRate.Text);
                            break;
                        case ScanModes.RollIQS:
                            DemoFormRef.DemoConfig.DS84tPartialFrameRate = Convert.ToDouble(txtFrameRate.Text);
                            break;
                    }
                    break;

                case GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_DSID20:  // 2.0.1.0
                    DemoFormRef.DemoConfig.DID20FrameRate = Convert.ToDouble(txtFrameRate.Text);
                    break;

                case GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_MS527t:
                    switch (ScanMode)
                    {
                        case ScanModes.FullHiRes:
                            DemoFormRef.DemoConfig.MS527tFullHiResFrameRate = Convert.ToDouble(txtFrameRate.Text);
                            break;
                        case ScanModes.FullLowRes:
                            DemoFormRef.DemoConfig.MS527tFullLowResFrameRate = Convert.ToDouble(txtFrameRate.Text);
                            break;
                        case ScanModes.RollIQS:
                            DemoFormRef.DemoConfig.MS527tPartialIQSFrameRate = Convert.ToDouble(txtFrameRate.Text);
                            break;
                        case ScanModes.RollGA:
                            DemoFormRef.DemoConfig.MS527tPartialGAFrameRate = Convert.ToDouble(txtFrameRate.Text);
                            break;
                        case ScanModes.RollJoint:
                            DemoFormRef.DemoConfig.MS527tPartialJointFrameRate = Convert.ToDouble(txtFrameRate.Text);
                            break;
                        case ScanModes.RollThenar:
                            DemoFormRef.DemoConfig.MS527tPartialThenarFrameRate = Convert.ToDouble(txtFrameRate.Text);
                            break;
                    }
                    break;

            }
        }

        private void optRollAreaGA_CheckedChanged(object sender, EventArgs e)
        {
            if (optRollAreaGA.Checked)
                cmbStandard.SelectedIndex = DemoForm.ImageSizeStandards.GA_CHINA;
        }

        private void chkAdaptRollAreaPosition_CheckedChanged(object sender, EventArgs e)
        {
            grpRollDirection.Enabled = chkAdaptRollAreaPosition.Checked;

            // don't allow no roll preview
            if (chkAdaptRollAreaPosition.Checked)
            {
                if (optNoRollPreview.Checked)
                {
                    optNoRollPreview.Checked = false;
                    optRollPreviewSide.Checked = true;
                }
                optNoRollPreview.Enabled = false;
            }
            else
                optNoRollPreview.Enabled = true;
        }

        private void txtLiveSegmEvalTimeout_Validating(object sender, CancelEventArgs e)
        {
            int Value = Convert.ToInt32(((TextBox)sender).Text);
            // check only that value is > 0
            if (Value < 0)
            {
                e.Cancel = true;
                return;
            }

            e.Cancel = false;
        }

        private void txtHWFakeFingerDetectionThreshold_Validating(object sender, CancelEventArgs e)
        {
            // check only that value is ranged between 10 and 100
            if (!CheckIntField((TextBox)sender, 10, 100))
            {
                e.Cancel = true;
                return;
            }

            e.Cancel = false;
        }

        // 2.0.0.0
        private void DisplaySettings()
        {
            // acquisition options
            chkAutocapture.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.AcquisitionOptions, GBMSGUI.AcquisitionOption.Autocapture);
            chkBlockAutocapture.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.AcquisitionOptions, GBMSGUI.AcquisitionOption.BlockAutocapture);
            chkFullResPreview.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.AcquisitionOptions, GBMSGUI.AcquisitionOption.FullResPreview);
            chkRotateFinger.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.AcquisitionOptions, GBMSGUI.AcquisitionOption.RotateFinger);
            chkPalmPrintQualityCalculation.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.AcquisitionOptions, GBMSGUI.AcquisitionOption.PalmPrintQualityCalculation);
            if (GBMSGUI.CheckMask(DemoFormRef.DemoConfig.AcquisitionOptions, GBMSGUI.AcquisitionOption.NoRollPreview))
                optNoRollPreview.Checked = true;
            else if (GBMSGUI.CheckMask(DemoFormRef.DemoConfig.AcquisitionOptions, GBMSGUI.AcquisitionOption.RollPreviewManualStop))
                optRollPreviewManual.Checked = true;
            else if (GBMSGUI.CheckMask(DemoFormRef.DemoConfig.AcquisitionOptions, GBMSGUI.AcquisitionOption.RollPreviewType))
                optRollPreviewCenter.Checked = true;
            else
                optRollPreviewSide.Checked = true;
            chkSound.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.AcquisitionOptions, GBMSGUI.AcquisitionOption.Sound);
            // 2.4.0.0
            chkSoundOnRollPreviewEnd.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.AcquisitionOptions, GBMSGUI.AcquisitionOption.SoundOnRollPreviewEnd);
            // 2.4.0.0 - removed
            //chkHighSpeedPreview.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.AcquisitionOptions, GBMSGUI.AcquisitionOption.HighSpeedPreview);
            chkNoArtefactsDisplay.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.AcquisitionOptions, GBMSGUI.AcquisitionOption.NoArtefactsDisplay); // 2.0.1.0
            chkDetectInvalidPattern.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.AcquisitionOptions, GBMSGUI.AcquisitionOption.DetectInvalidPattern);
            chkDetectIncompletePattern.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.AcquisitionOptions, GBMSGUI.AcquisitionOption.DetectIncompletePattern);
            chkDetectInclination.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.AcquisitionOptions, GBMSGUI.AcquisitionOption.DetectInclination);
            chkAutoClearOutsideRoll.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.AcquisitionOptions, GBMSGUI.AcquisitionOption.AutoClearOutsideRoll);
            chkSegmentsQualityEval.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.AcquisitionOptions, GBMSGUI.AcquisitionOption.LiveSegmentsEval);
            chkAutoIncompleteSlapSegm.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.AcquisitionOptions, GBMSGUI.AcquisitionOption.AutoIncompleteSlapSegm);
            chkAdaptRollAreaPosition.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.AcquisitionOptions, GBMSGUI.AcquisitionOption.AdaptRollAreaPosition);
            chkEnableRollStripeAcquisition.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.AcquisitionOptions, GBMSGUI.AcquisitionOption.EnableRollStripeAcquisition);
            chkEnableRollStripeFeedback.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.AcquisitionOptions, GBMSGUI.AcquisitionOption.EnableRollStripeFeedback);
            chkAutoAccept.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.AcquisitionOptions, GBMSGUI.AcquisitionOption.Autoaccept);
            // 1.15.0.0
            chkDeletePalmFingerSegments.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.AcquisitionOptions, GBMSGUI.AcquisitionOption.DeletePalmFingerSegments);
            // 2.4.0.0
            chkRemoveHalo.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.AcquisitionOptions, GBMSGUI.AcquisitionOption.RemoveHaloLatent);

            // session options
            chkSequenceCheck.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.SessionOptions, GBMSGUI.SessionOption.SequenceCheck);
            chkAskUnavailabilityReason.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.SessionOptions, GBMSGUI.SessionOption.AskUnavailabilityReason);
            chkDryFingerImageEnhancement.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.SessionOptions, GBMSGUI.SessionOption.DryFingerImageEnhancement);
            // 2.0.0.0
            chkFakeFingerDetection.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.SessionOptions, GBMSGUI.SessionOption.SWFakeFingerDetection);
            // 2.0.1.0
            chkFFDBlocking.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.SessionOptions, GBMSGUI.SessionOption.FakeFingerDetectionBlocking);
            // 2.1.0.0
            chkAutoCaptureBlockForDetectedFakes.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.SessionOptions, GBMSGUI.SessionOption.AutocaptureBlockForDetectedFakes);
            // 2.4.0.0
            chkDuplicationCheck.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.SessionOptions, GBMSGUI.SessionOption.DuplicationCheck);
            chkCheckFingertipPhalange.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.SessionOptions, GBMSGUI.SessionOption.CheckFingertipPhalange);
            chkCheckFingerUpsideDown.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.SessionOptions, GBMSGUI.SessionOption.CheckFingerUpsideDown);
            chkCheckFingertipPhalangeBlocking.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.SessionOptions, GBMSGUI.SessionOption.CheckFingertipPhalangeBlocking);
            chkCheckFingerUpsideDownBlocking.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.SessionOptions, GBMSGUI.SessionOption.CheckFingerUpsideDownBlocking);
            chkSequenceCheckBlocking.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.SessionOptions, GBMSGUI.SessionOption.SequenceCheckBlocking);
            chkDuplicationCheckBlocking.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.SessionOptions, GBMSGUI.SessionOption.DuplicationCheckBlocking);
            chkNoAdvancedView.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.SessionOptions, GBMSGUI.SessionOption.NoAdvancedView);

            // settings
            chkBlockAutocaptureContrast.Checked = DemoFormRef.DemoConfig.BlockAutocaptureContrast;
            chkBlockAutocaptureContrast.Enabled = chkBlockAutocapture.Checked;

            cmbStandard.SelectedIndex = DemoFormRef.DemoConfig.ImageSizeStandard;
            // show values
            ShowImageSizeValues();

            cmbQualityAlgorithm.SelectedIndex = DemoFormRef.DemoConfig.IAFIsQualityAlgorithm - 1;

            // Image compression
            // 500 dpi
            if (DemoFormRef.DemoConfig.ImageCompression500 == DemoForm.ImageCompressions.WSQ)
                optWSQCompression500.Checked = true;
            else
                optJpeg2000Compression500.Checked = true;
            // 1000 dpi
            if (DemoFormRef.DemoConfig.ImageCompression1000 == DemoForm.ImageCompressions.WSQ)
                optWSQCompression1000.Checked = true;
            else
                optJpeg2000Compression1000.Checked = true;

            // compression rates
            // 500 dpi
            txtWSQBitRate500.Text = DemoFormRef.DemoConfig.WQSBitRate500.ToString();
            txtJPEGRate500.Text = DemoFormRef.DemoConfig.JPEGRate500.ToString();
            // 1000 dpi
            txtWSQBitRate1000.Text = DemoFormRef.DemoConfig.WQSBitRate1000.ToString();
            txtJPEGRate1000.Text = DemoFormRef.DemoConfig.JPEGRate1000.ToString();

            // Roll area size
            if (DemoFormRef.DemoConfig.RollAreaSize == GBMSAPI_NET_DeviceFeatures.GBMSAPI_NET_DF_ROLL_AREA_IQS)
                optRollAreaIQS.Checked = true;
            else
                optRollAreaGA.Checked = true;

            // select current
            cmbLanguage.SelectedIndex = DemoFormRef.DemoConfig.GUILanguage;

            // select current
            cmbFingerContactEvaluationMode.SelectedIndex = DemoFormRef.DemoConfig.FingerContactEvaluationMode - 1;

            // joint segmentation
            chkJointSegmentation.Checked = DemoFormRef.DemoConfig.JointSegmentation;

            if (DemoFormRef.DemoConfig.RollDirection == GBMSGUI.AdaptiveRollDirection.ToLeft)
                optRollToLeft.Checked = true;
            else if (DemoFormRef.DemoConfig.RollDirection == GBMSGUI.AdaptiveRollDirection.ToRight)
                optRollToRight.Checked = true;
            else
                optRollToCenter.Checked = true;

            chkAdaptRollAreaPosition_CheckedChanged(this, null);

            // BlockAutocapture Mask
            lstBlockAutocaptureDiag.Items.Clear();
            lstBlockAutocaptureDiag.Items.Add(new DiagnosticListItem(GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_SCANNER_SURFACE_NOT_NORMA),
                GBMSGUI.CheckMask(DemoFormRef.DemoConfig.BlockAutocaptureMask, GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_SCANNER_SURFACE_NOT_NORMA));
            lstBlockAutocaptureDiag.Items.Add(new DiagnosticListItem(GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_EXT_LIGHT_TOO_STRONG),
                GBMSGUI.CheckMask(DemoFormRef.DemoConfig.BlockAutocaptureMask, GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_EXT_LIGHT_TOO_STRONG));
            lstBlockAutocaptureDiag.Items.Add(new DiagnosticListItem(GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_FLAT_FINGER_SLIDING),
                GBMSGUI.CheckMask(DemoFormRef.DemoConfig.BlockAutocaptureMask, GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_FLAT_FINGER_SLIDING));
            lstBlockAutocaptureDiag.Items.Add(new DiagnosticListItem(GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_FLAT_FINGER_DISPLACED_DOWN),
                GBMSGUI.CheckMask(DemoFormRef.DemoConfig.BlockAutocaptureMask, GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_FLAT_FINGER_DISPLACED_DOWN));
            lstBlockAutocaptureDiag.Items.Add(new DiagnosticListItem(GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_FLAT_FINGER_OUT_OF_REGION_LEFT),
                GBMSGUI.CheckMask(DemoFormRef.DemoConfig.BlockAutocaptureMask, GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_FLAT_FINGER_OUT_OF_REGION_LEFT));
            lstBlockAutocaptureDiag.Items.Add(new DiagnosticListItem(GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_FLAT_FINGER_OUT_OF_REGION_RIGHT),
                GBMSGUI.CheckMask(DemoFormRef.DemoConfig.BlockAutocaptureMask, GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_FLAT_FINGER_OUT_OF_REGION_RIGHT));
            lstBlockAutocaptureDiag.Items.Add(new DiagnosticListItem(GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_FLAT_FINGER_OUT_OF_REGION_TOP),
                GBMSGUI.CheckMask(DemoFormRef.DemoConfig.BlockAutocaptureMask, GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_FLAT_FINGER_OUT_OF_REGION_TOP));
            lstBlockAutocaptureDiag.Items.Add(new DiagnosticListItem(GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_DRY_FINGER),
                GBMSGUI.CheckMask(DemoFormRef.DemoConfig.BlockAutocaptureMask, GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_DRY_FINGER));
            lstBlockAutocaptureDiag.Items.Add(new DiagnosticListItem(GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_WET_FINGER),
                GBMSGUI.CheckMask(DemoFormRef.DemoConfig.BlockAutocaptureMask, GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_WET_FINGER));

            // Ignored diagnostic mask
            lstIgnoredDiag.Items.Clear();
            lstIgnoredDiag.Items.Add(new DiagnosticListItem(GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_EXT_LIGHT_TOO_STRONG),
                GBMSGUI.CheckMask(DemoFormRef.DemoConfig.IgnoredDiagnosticMask, GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_EXT_LIGHT_TOO_STRONG));
            lstIgnoredDiag.Items.Add(new DiagnosticListItem(GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_FLAT_FINGER_SLIDING),
                GBMSGUI.CheckMask(DemoFormRef.DemoConfig.IgnoredDiagnosticMask, GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_FLAT_FINGER_SLIDING));
            lstIgnoredDiag.Items.Add(new DiagnosticListItem(GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_FLAT_FINGER_DISPLACED_DOWN),
                GBMSGUI.CheckMask(DemoFormRef.DemoConfig.IgnoredDiagnosticMask, GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_FLAT_FINGER_DISPLACED_DOWN));
            lstIgnoredDiag.Items.Add(new DiagnosticListItem(GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_FLAT_FINGER_OUT_OF_REGION_LEFT),
                GBMSGUI.CheckMask(DemoFormRef.DemoConfig.IgnoredDiagnosticMask, GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_FLAT_FINGER_OUT_OF_REGION_LEFT));
            lstIgnoredDiag.Items.Add(new DiagnosticListItem(GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_FLAT_FINGER_OUT_OF_REGION_RIGHT),
                GBMSGUI.CheckMask(DemoFormRef.DemoConfig.IgnoredDiagnosticMask, GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_FLAT_FINGER_OUT_OF_REGION_RIGHT));
            lstIgnoredDiag.Items.Add(new DiagnosticListItem(GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_FLAT_FINGER_OUT_OF_REGION_TOP),
                GBMSGUI.CheckMask(DemoFormRef.DemoConfig.IgnoredDiagnosticMask, GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_FLAT_FINGER_OUT_OF_REGION_TOP));
            lstIgnoredDiag.Items.Add(new DiagnosticListItem(GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_DRY_FINGER),
                GBMSGUI.CheckMask(DemoFormRef.DemoConfig.IgnoredDiagnosticMask, GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_DRY_FINGER));
            lstIgnoredDiag.Items.Add(new DiagnosticListItem(GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_WET_FINGER),
                GBMSGUI.CheckMask(DemoFormRef.DemoConfig.IgnoredDiagnosticMask, GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_WET_FINGER));
            lstIgnoredDiag.Items.Add(new DiagnosticListItem(GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_COMPOSITION_SLOW),
                GBMSGUI.CheckMask(DemoFormRef.DemoConfig.IgnoredDiagnosticMask, GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_COMPOSITION_SLOW));
            lstIgnoredDiag.Items.Add(new DiagnosticListItem(GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_VSROLL_DISPLACED_DOWN),
                GBMSGUI.CheckMask(DemoFormRef.DemoConfig.IgnoredDiagnosticMask, GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_VSROLL_DISPLACED_DOWN));
            lstIgnoredDiag.Items.Add(new DiagnosticListItem(GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_VSROLL_OUTSIDE_BORDER_LEFT),
                GBMSGUI.CheckMask(DemoFormRef.DemoConfig.IgnoredDiagnosticMask, GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_VSROLL_OUTSIDE_BORDER_LEFT));
            lstIgnoredDiag.Items.Add(new DiagnosticListItem(GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_VSROLL_OUTSIDE_BORDER_RIGHT),
                GBMSGUI.CheckMask(DemoFormRef.DemoConfig.IgnoredDiagnosticMask, GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_VSROLL_OUTSIDE_BORDER_RIGHT));
            lstIgnoredDiag.Items.Add(new DiagnosticListItem(GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_VSROLL_OUTSIDE_BORDER_TOP),
                GBMSGUI.CheckMask(DemoFormRef.DemoConfig.IgnoredDiagnosticMask, GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_VSROLL_OUTSIDE_BORDER_TOP));
            lstIgnoredDiag.Items.Add(new DiagnosticListItem(GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_VSROLL_IMPROPER_ROLL),
                GBMSGUI.CheckMask(DemoFormRef.DemoConfig.IgnoredDiagnosticMask, GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_VSROLL_IMPROPER_ROLL));
            lstIgnoredDiag.Items.Add(new DiagnosticListItem(GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_VSROLL_TOO_FAST_ROLL),
                GBMSGUI.CheckMask(DemoFormRef.DemoConfig.IgnoredDiagnosticMask, GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_VSROLL_TOO_FAST_ROLL));
            lstIgnoredDiag.Items.Add(new DiagnosticListItem(GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_VSROLL_TOO_NARROW_ROLL),
                GBMSGUI.CheckMask(DemoFormRef.DemoConfig.IgnoredDiagnosticMask, GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_VSROLL_TOO_NARROW_ROLL));
            lstIgnoredDiag.Items.Add(new DiagnosticListItem(GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_TOO_SHORT_VERTICAL_ROLL),
                GBMSGUI.CheckMask(DemoFormRef.DemoConfig.IgnoredDiagnosticMask, GBMSAPI_NET_DiagnosticMessages.GBMSAPI_NET_DM_TOO_SHORT_VERTICAL_ROLL));

            chkEnableBlockComposition.Checked = DemoFormRef.DemoConfig.EnableBlockComposition;
            chkEnableBlockAutocaptureLedColorFeedback.Checked = DemoFormRef.DemoConfig.EnableBlockAutocaptureLedColorFeedback;
            // 1.15.0.0
            txtLiveSegmEvalTimeout.Text = DemoFormRef.DemoConfig.LiveSegmEvalTimeout.ToString();
            // 2.0.0.0
            txtHWFakeFingerDetectionThreshold.Text = DemoFormRef.DemoConfig.HWFakeFingerDetectionThreshold.ToString();
            txtSWFakeFingerDetectionThreshold.Text = DemoFormRef.DemoConfig.SWFakeFingerDetectionThreshold.ToString();
            // 2.3.2.0
            txtMaxAllowedFakesInSlap4.Text = DemoFormRef.DemoConfig.MaxAllowedFakesInSlap4.ToString();
            txtMaxAllowedFakesInSlap2.Text = DemoFormRef.DemoConfig.MaxAllowedFakesInSlap2.ToString();

            // 2.4.0.0
            chkFlatFingerOnRollArea.Checked = DemoFormRef.DemoConfig.FlatFingerOnRollArea;
        }

        // 2.0.0.0
        private void btnSetBestPracticeSettings_Click(object sender, EventArgs e)
        {
            DemoFormRef.DemoConfig.SessionOptions = DemoForm.BestPracticeSettings.SessionOptions;
            DemoFormRef.DemoConfig.AcquisitionOptions = DemoForm.BestPracticeSettings.AcquisitionOptions;
            DemoFormRef.DemoConfig.ArtefactsThreshold1 = DemoForm.BestPracticeSettings.ArtefactsThreshold1;
            DemoFormRef.DemoConfig.ArtefactsThreshold2 = DemoForm.BestPracticeSettings.ArtefactsThreshold2;
            DemoFormRef.DemoConfig.PatternValidityThreshold = DemoForm.BestPracticeSettings.PatternValidityThreshold;
            DemoFormRef.DemoConfig.PatternCompletenessThreshold = DemoForm.BestPracticeSettings.PatternCompletenessThreshold;
            DemoFormRef.DemoConfig.LowerPalmCompletenessThreshold1 = DemoForm.BestPracticeSettings.LowerPalmCompletenessThreshold1;
            DemoFormRef.DemoConfig.LowerPalmCompletenessThreshold2 = DemoForm.BestPracticeSettings.LowerPalmCompletenessThreshold2;
            DemoFormRef.DemoConfig.NFIQQualityThreshold1 = DemoForm.BestPracticeSettings.NFIQQualityThreshold1;
            DemoFormRef.DemoConfig.NFIQQualityThreshold2 = DemoForm.BestPracticeSettings.NFIQQualityThreshold2;
            DemoFormRef.DemoConfig.GBQualityThreshold1 = DemoForm.BestPracticeSettings.GBQualityThreshold1;
            DemoFormRef.DemoConfig.GBQualityThreshold2 = DemoForm.BestPracticeSettings.GBQualityThreshold2;
            DemoFormRef.DemoConfig.BlockAutocaptureMask = DemoForm.BestPracticeSettings.BlockAutocaptureMask;
            DemoFormRef.DemoConfig.IgnoredDiagnosticMask = DemoForm.BestPracticeSettings.IgnoredDiagnosticMask;
            DemoFormRef.DemoConfig.LiveSegmEvalTimeout = 7; // 2.4.0.0

            DisplaySettings();
        }

        // 2.0.0.0
        private void btnSetDemoSettings_Click(object sender, EventArgs e)
        {
            DemoFormRef.DemoConfig.SessionOptions = DemoForm.DemoSettings.SessionOptions;
            DemoFormRef.DemoConfig.AcquisitionOptions = DemoForm.DemoSettings.AcquisitionOptions;
            DemoFormRef.DemoConfig.ArtefactsThreshold1 = DemoForm.DemoSettings.ArtefactsThreshold1;
            DemoFormRef.DemoConfig.ArtefactsThreshold2 = DemoForm.DemoSettings.ArtefactsThreshold2;
            DemoFormRef.DemoConfig.PatternValidityThreshold = DemoForm.DemoSettings.PatternValidityThreshold;
            DemoFormRef.DemoConfig.PatternCompletenessThreshold = DemoForm.DemoSettings.PatternCompletenessThreshold;
            DemoFormRef.DemoConfig.LowerPalmCompletenessThreshold1 = DemoForm.DemoSettings.LowerPalmCompletenessThreshold1;
            DemoFormRef.DemoConfig.LowerPalmCompletenessThreshold2 = DemoForm.DemoSettings.LowerPalmCompletenessThreshold2;
            DemoFormRef.DemoConfig.NFIQQualityThreshold1 = DemoForm.DemoSettings.NFIQQualityThreshold1;
            DemoFormRef.DemoConfig.NFIQQualityThreshold2 = DemoForm.DemoSettings.NFIQQualityThreshold2;
            DemoFormRef.DemoConfig.GBQualityThreshold1 = DemoForm.DemoSettings.GBQualityThreshold1;
            DemoFormRef.DemoConfig.GBQualityThreshold2 = DemoForm.DemoSettings.GBQualityThreshold2;
            DemoFormRef.DemoConfig.BlockAutocaptureMask = DemoForm.DemoSettings.BlockAutocaptureMask;
            DemoFormRef.DemoConfig.IgnoredDiagnosticMask = DemoForm.DemoSettings.IgnoredDiagnosticMask;
            DemoFormRef.DemoConfig.LiveSegmEvalTimeout = 5; // 2.4.0.0

            DisplaySettings();
        }

        private void txtSWFakeFingerDetectionThreshold_Validating(object sender, CancelEventArgs e)
        {
            // check only that value is ranged between 0 and 100
            if (!CheckIntField((TextBox)sender, 0, 100))
            {
                e.Cancel = true;
                return;
            }

            e.Cancel = false;
        }

        // 2.3.0.0
        private void ReadHeaterSettings()
        {
            GBMSAPI_NET_ExternalDevicesControlRoutines.GBMSAPI_NET_GetheaterMeanTempRange(out HeaterData.MaxTemp, out HeaterData.DefaultTemp, out HeaterData.MinTemp);
            lblHeaterRange.Text = "(" + HeaterData.MinTemp + " - " + HeaterData.MaxTemp + ")";

            int RetVal = GBMSAPI_NET_ExternalDevicesControlRoutines.GBMSAPI_NET_GetHeaterMeanTemp(out HeaterData.CurrentTemp);
            if (RetVal != GBMSAPI_NET_ErrorCodes.GBMSAPI_NET_ERROR_CODE_NO_ERROR)
            {
                if (RetVal == GBMSAPI_NET_ErrorCodes.GBMSAPI_NET_ERROR_CODE_UNAVAILABLE_OPTION)
                    lblHeaterCheckResult.Text = "Unavailable option";
                else
                    MessageBox.Show(DemoFormRef.MyGUI.GetMSAPIErrorMessage("GBMSAPI_NET_GetHeaterMeanTemp", RetVal),
                        Application.ProductName,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                grpHeater.Enabled = false;
                return;
            }
            txtHeaterTemp.Text = HeaterData.CurrentTemp.ToString();
        }

        // 2.3.0.0
        private void btnSetHeterTemp_Click(object sender, EventArgs e)
        {
            float Temp = (float)Convert.ToDouble(txtHeaterTemp.Text);

            int RetVal = GBMSAPI_NET_ExternalDevicesControlRoutines.GBMSAPI_NET_SetHeaterMeanTemp(Temp);
            if (RetVal != GBMSAPI_NET_ErrorCodes.GBMSAPI_NET_ERROR_CODE_NO_ERROR)
            {
                MessageBox.Show(DemoFormRef.MyGUI.GetMSAPIErrorMessage("GBMSAPI_NET_SetHeaterMeanTemp", RetVal),
                        Application.ProductName,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            HeaterData.CurrentTemp = Temp;
            MessageBox.Show("Heater temperature was successfully set", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // 2.3.0.0
        private void btnCheckHeater_Click(object sender, EventArgs e)
        {
            int Result;

            lblHeaterCheckResult.Text = "";

            int RetVal = GBMSAPI_NET_ExternalDevicesControlRoutines.GBMSAPI_NET_CheckHeater(out Result);
            if (RetVal != GBMSAPI_NET_ErrorCodes.GBMSAPI_NET_ERROR_CODE_NO_ERROR)
            {
                MessageBox.Show(DemoFormRef.MyGUI.GetMSAPIErrorMessage("GBMSAPI_NET_CheckHeater", RetVal),
                        Application.ProductName,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // check Result
            if (Result == GBMSAPI_NET_HeaterCheckResults.GBMSAPI_NET_HEATER_CHECK_RESULT_OK)
                lblHeaterCheckResult.Text = "Heater is working correctly";
                //MessageBox.Show("Heater is working correctly", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (Result == GBMSAPI_NET_HeaterCheckResults.GBMSAPI_NET_HEATER_CHECK_RESULT_FAIL)
                //MessageBox.Show("Heater is NOT working correctly!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblHeaterCheckResult.Text = "Heater is NOT working correctly";
        }

        private void txtHeaterTemp_Validating(object sender, CancelEventArgs e)
        {
            // check that value is ranged between min and max
            if (!CheckDoubleField((TextBox)sender, HeaterData.MinTemp, HeaterData.MaxTemp))
            {
                e.Cancel = true;
                return;
            }
            e.Cancel = false;
        }

        private void txtMaxAllowedFakesInSlap4_Validating(object sender, CancelEventArgs e)
        {
            // check only that value is ranged between 0 and 2
            if (!CheckIntField((TextBox)sender, 0, 2))
            {
                e.Cancel = true;
                return;
            }

            e.Cancel = false;
        }

        private void txtMaxAllowedFakesInSlap2_Validating(object sender, CancelEventArgs e)
        {
            // check only that value is ranged between 0 and 1
            if (!CheckIntField((TextBox)sender, 0, 1))
            {
                e.Cancel = true;
                return;
            }

            e.Cancel = false;
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}