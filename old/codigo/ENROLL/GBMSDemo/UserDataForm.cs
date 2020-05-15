using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Globalization;
using GBMSGUI_NET;
using GBMSAPI_NET.GBMSAPI_NET_Defines.GBMSAPI_NET_DeviceCharacteristicsDefines;
using GBMSAPI_NET.GBMSAPI_NET_LibraryFunctions;
using GBMSAPI_NET.GBMSAPI_NET_Defines.GBMSAPI_NET_ErrorCodesDefines;
using GBMSAPI_NET.GBMSAPI_NET_Defines.GBMSAPI_NET_VisualInterfaceLCDDefines;
using GBMSAPI_NET.GBMSAPI_NET_Defines.GBMSAPI_NET_AcquisitionProcessDefines;
using AN2K_NET_WRAPPER;
using JPEG_NET_WRAPPER;
using WSQPACK_NET_WRAPPER;
using System.Text.RegularExpressions;
using An2k_2011_NetWrapper;
using An2k_Engine_Net_Wrapper;
#if GBDCGUI_DEMO
using GBDCGUI_Net;
#endif

// UserDataForm: form for user images acquisition
namespace GBMSDemo
{
    public partial class UserDataForm : Form
    {
        public String ImagesPath;       // path where to store/read images
        public bool ViewMode;           // true = view mode; false = new mode
        public bool FingerprintCard;    // true = Acquire from Fingerprint Card

        private DemoForm.UserData UserData; // class where to store user data
        private DemoForm DemoFormRef;       // reference to main form
        private Byte[] ImageBuffer;         // buffer for images
        private GBMSGUI MyGUI = new GBMSGUI();  // GBMSGUI class
        //private GCHandle gcUserDataFormHandle;

        // AfterStartCallback, if needed
        //private static GBMSGUI.AfterStartCallback AfterStartCallbackRef = new GBMSGUI.AfterStartCallback(AfterStartCallback);

        //private static GCHandle gcUserDataFormHandle_St;
        /*
        public static bool AfterStartCallback(IntPtr Param)
        {
            bool ret;

            // retrieve form reference
            if (!gcUserDataFormHandle_St.IsAllocated)
                gcUserDataFormHandle_St = GCHandle.FromIntPtr(Param);

            UserDataForm UserDataFormRef = (UserDataForm)gcUserDataFormHandle_St.Target;

            // call form method
            ret = UserDataFormRef.AfterStartCallback();

            return ret;
        }
        */

        // class for scanned object list item
        private class ScanItem : Object
        {
            public uint ScanObjID;  // ID of the object
            public String Text;     // Descriptive text (used also to build filename)
            public PictureBox ImagePictureBox;  // picturebox where to show image
            public bool Supported;              // if object supported by the current scanner
            public ScanItemData ItemData = new ScanItemData();  // other data collected after scanning

            public ScanItem(uint ScanObjID, String Text, PictureBox ImagePictureBox)
            {
                this.ScanObjID = ScanObjID;
                this.Text = Text;
                this.ImagePictureBox = ImagePictureBox;
            }

            public override String ToString()
            {
                return Text;
            }
        }

        private ScanItem CurrentScanItem;

        // data for scan items
        private class ScanItemData
        {
	        public int Quality;
	        public int QualityAlgorithm;
            public int UnavailabilityReason;
        }

        // data for segments
        private class SegmentData
        {
            public ScanItemData ItemData = new ScanItemData();
            public Rectangle BoundingBox = new Rectangle();
        }
        private SegmentData[] LeftSlapSegmentsData = new SegmentData[4];
        private SegmentData[] RightSlapSegmentsData = new SegmentData[4];
        private SegmentData[] TwoThumbsSegmentsData = new SegmentData[2];

        // joint bounding boxes
        private class JointsData
        {
            public Rectangle[] PhalangeBBox = new Rectangle[3];
        }
        private JointsData[] JointsBBoxData = new JointsData[40]; // 10 fingers, 4 views
        private int IndexOfJointsData(uint ScannedObjectID)
        {
            int Index = -1;

            // assing an index of JointsBBoxData array to each joint object
            switch (ScannedObjectID)
            {
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_LEFT_THUMB:
                    Index = 0;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_LEFT_INDEX:
                    Index = 1;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_LEFT_MIDDLE:
                    Index = 2;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_LEFT_RING:
                    Index = 3;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_LEFT_LITTLE:
                    Index = 4;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_RIGHT_THUMB:
                    Index = 5;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_RIGHT_INDEX:
                    Index = 6;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_RIGHT_MIDDLE:
                    Index = 7;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_RIGHT_RING:
                    Index = 8;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_RIGHT_LITTLE:
                    Index = 9;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_LEFT_THUMB:
                    Index = 10;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_LEFT_INDEX:
                    Index = 11;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_LEFT_MIDDLE:
                    Index = 12;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_LEFT_RING:
                    Index = 13;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_LEFT_LITTLE:
                    Index = 14;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_RIGHT_THUMB:
                    Index = 15;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_RIGHT_INDEX:
                    Index = 16;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_RIGHT_MIDDLE:
                    Index = 17;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_RIGHT_RING:
                    Index = 18;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_RIGHT_LITTLE:
                    Index = 19;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_LEFT_THUMB:
                    Index = 20;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_LEFT_INDEX:
                    Index = 21;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_LEFT_MIDDLE:
                    Index = 22;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_LEFT_RING:
                    Index = 23;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_LEFT_LITTLE:
                    Index = 24;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_RIGHT_THUMB:
                    Index = 25;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_RIGHT_INDEX:
                    Index = 26;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_RIGHT_MIDDLE:
                    Index = 27;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_RIGHT_RING:
                    Index = 28;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_RIGHT_LITTLE:
                    Index = 29;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_LEFT_THUMB:
                    Index = 30;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_LEFT_INDEX:
                    Index = 31;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_LEFT_MIDDLE:
                    Index = 32;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_LEFT_RING:
                    Index = 33;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_LEFT_LITTLE:
                    Index = 34;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_RIGHT_THUMB:
                    Index = 35;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_RIGHT_INDEX:
                    Index = 36;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_RIGHT_MIDDLE:
                    Index = 37;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_RIGHT_RING:
                    Index = 38;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_RIGHT_LITTLE:
                    Index = 39;
                    break;
            }

            return Index;
        }
        private void ResetAllJointsData()
        {
            for (int i = 0; i < 40; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    JointsBBoxData[i].PhalangeBBox[j].X = 0;
                    JointsBBoxData[i].PhalangeBBox[j].Y = 0;
                    JointsBBoxData[i].PhalangeBBox[j].Width = 0;
                    JointsBBoxData[i].PhalangeBBox[j].Height = 0;
                }
            }
        }
        private void ResetJointsData(uint ScanObjID)
        {
            int Index = IndexOfJointsData(ScanObjID);
            for (int j = 0; j < 3; j++)
            {
                JointsBBoxData[Index].PhalangeBBox[j].X = 0;
                JointsBBoxData[Index].PhalangeBBox[j].Y = 0;
                JointsBBoxData[Index].PhalangeBBox[j].Width = 0;
                JointsBBoxData[Index].PhalangeBBox[j].Height = 0;
            }
        }

        // delegate for OnAfterStart
        public delegate bool AfterStartEvent();

        public AfterStartEvent AfterStartDelegate;

        public UserDataForm(DemoForm DemoFormRef)
        {
            // reference to main form
            this.DemoFormRef = DemoFormRef;

            FingerprintCard = false;

            InitializeComponent();

            // delegate for callback
            AfterStartDelegate = new AfterStartEvent(OnAfterStart);

            // assign popupmenu to items
            pboxLeftFourFingers.ContextMenu = popAcquireItemMenu;
            pboxRightFourFingers.ContextMenu = popAcquireItemMenu;
            pboxTwoThumbs.ContextMenu = popAcquireItemMenu;
            pboxLeftRolledThumb.ContextMenu = popAcquireItemMenu;
            pboxLeftRolledIndex.ContextMenu = popAcquireItemMenu;
            pboxLeftRolledMiddle.ContextMenu = popAcquireItemMenu;
            pboxLeftRolledRing.ContextMenu = popAcquireItemMenu;
            pboxLeftRolledLittle.ContextMenu = popAcquireItemMenu;
            pboxRightRolledThumb.ContextMenu = popAcquireItemMenu;
            pboxRightRolledIndex.ContextMenu = popAcquireItemMenu;
            pboxRightRolledMiddle.ContextMenu = popAcquireItemMenu;
            pboxRightRolledRing.ContextMenu = popAcquireItemMenu;
            pboxRightRolledLittle.ContextMenu = popAcquireItemMenu;
            pboxLeftUpperPalm.ContextMenu = popAcquireItemMenu;
            pboxLeftLowerPalm.ContextMenu = popAcquireItemMenu;
            pboxLeftWritersPalm.ContextMenu = popAcquireItemMenu;
            pboxRightUpperPalm.ContextMenu = popAcquireItemMenu;
            pboxRightLowerPalm.ContextMenu = popAcquireItemMenu;
            pboxRightWritersPalm.ContextMenu = popAcquireItemMenu;
            pboxLeftFlatThumb.ContextMenu = popAcquireItemMenu;
            pboxLeftFlatIndex.ContextMenu = popAcquireItemMenu;
            pboxLeftFlatMiddle.ContextMenu = popAcquireItemMenu;
            pboxLeftFlatRing.ContextMenu = popAcquireItemMenu;
            pboxLeftFlatLittle.ContextMenu = popAcquireItemMenu;
            pboxRightFlatThumb.ContextMenu = popAcquireItemMenu;
            pboxRightFlatIndex.ContextMenu = popAcquireItemMenu;
            pboxRightFlatMiddle.ContextMenu = popAcquireItemMenu;
            pboxRightFlatRing.ContextMenu = popAcquireItemMenu;
            pboxRightFlatLittle.ContextMenu = popAcquireItemMenu;
            //V1.2 - DS40
            pboxLeftTwoFingers.ContextMenu = popAcquireItemMenu;
            pboxRightTwoFingers.ContextMenu = popAcquireItemMenu;
            pboxTwoIndexes.ContextMenu = popAcquireItemMenu;
            //V1.10 - supplemental objects for MS527
            pboxLeftThumbRolledTip.ContextMenu = popAcquireItemMenu;
            pboxLeftThumbRolledJoint.ContextMenu = popAcquireItemMenu;
            pboxLeftThumbJointLeft.ContextMenu = popAcquireItemMenu;
            pboxLeftThumbJointCenter.ContextMenu = popAcquireItemMenu;
            pboxLeftThumbJointRight.ContextMenu = popAcquireItemMenu;
            pboxLeftIndexRolledTip.ContextMenu = popAcquireItemMenu;
            pboxLeftIndexRolledJoint.ContextMenu = popAcquireItemMenu;
            pboxLeftIndexJointLeft.ContextMenu = popAcquireItemMenu;
            pboxLeftIndexJointCenter.ContextMenu = popAcquireItemMenu;
            pboxLeftIndexJointRight.ContextMenu = popAcquireItemMenu;
            pboxLeftMiddleRolledTip.ContextMenu = popAcquireItemMenu;
            pboxLeftMiddleRolledJoint.ContextMenu = popAcquireItemMenu;
            pboxLeftMiddleJointLeft.ContextMenu = popAcquireItemMenu;
            pboxLeftMiddleJointCenter.ContextMenu = popAcquireItemMenu;
            pboxLeftMiddleJointRight.ContextMenu = popAcquireItemMenu;
            pboxLeftRingRolledTip.ContextMenu = popAcquireItemMenu;
            pboxLeftRingRolledJoint.ContextMenu = popAcquireItemMenu;
            pboxLeftRingJointLeft.ContextMenu = popAcquireItemMenu;
            pboxLeftRingJointCenter.ContextMenu = popAcquireItemMenu;
            pboxLeftRingJointRight.ContextMenu = popAcquireItemMenu;
            pboxLeftLittleRolledTip.ContextMenu = popAcquireItemMenu;
            pboxLeftLittleRolledJoint.ContextMenu = popAcquireItemMenu;
            pboxLeftLittleJointLeft.ContextMenu = popAcquireItemMenu;
            pboxLeftLittleJointCenter.ContextMenu = popAcquireItemMenu;
            pboxLeftLittleJointRight.ContextMenu = popAcquireItemMenu;
            pboxRightThumbRolledTip.ContextMenu = popAcquireItemMenu;
            pboxRightThumbRolledJoint.ContextMenu = popAcquireItemMenu;
            pboxRightThumbJointLeft.ContextMenu = popAcquireItemMenu;
            pboxRightThumbJointCenter.ContextMenu = popAcquireItemMenu;
            pboxRightThumbJointRight.ContextMenu = popAcquireItemMenu;
            pboxRightIndexRolledTip.ContextMenu = popAcquireItemMenu;
            pboxRightIndexRolledJoint.ContextMenu = popAcquireItemMenu;
            pboxRightIndexJointLeft.ContextMenu = popAcquireItemMenu;
            pboxRightIndexJointCenter.ContextMenu = popAcquireItemMenu;
            pboxRightIndexJointRight.ContextMenu = popAcquireItemMenu;
            pboxRightMiddleRolledTip.ContextMenu = popAcquireItemMenu;
            pboxRightMiddleRolledJoint.ContextMenu = popAcquireItemMenu;
            pboxRightMiddleJointLeft.ContextMenu = popAcquireItemMenu;
            pboxRightMiddleJointCenter.ContextMenu = popAcquireItemMenu;
            pboxRightMiddleJointRight.ContextMenu = popAcquireItemMenu;
            pboxRightRingRolledTip.ContextMenu = popAcquireItemMenu;
            pboxRightRingRolledJoint.ContextMenu = popAcquireItemMenu;
            pboxRightRingJointLeft.ContextMenu = popAcquireItemMenu;
            pboxRightRingJointCenter.ContextMenu = popAcquireItemMenu;
            pboxRightRingJointRight.ContextMenu = popAcquireItemMenu;
            pboxRightLittleRolledTip.ContextMenu = popAcquireItemMenu;
            pboxRightLittleRolledJoint.ContextMenu = popAcquireItemMenu;
            pboxRightLittleJointLeft.ContextMenu = popAcquireItemMenu;
            pboxRightLittleJointCenter.ContextMenu = popAcquireItemMenu;
            pboxRightLittleJointRight.ContextMenu = popAcquireItemMenu;
            pboxLeftRolledThenar.ContextMenu = popAcquireItemMenu;
            pboxRightRolledThenar.ContextMenu = popAcquireItemMenu;
            // 1.13.4.0
            pboxLeftRolledUpThumb.ContextMenu = popAcquireItemMenu;
            pboxLeftRolledUpIndex.ContextMenu = popAcquireItemMenu;
            pboxLeftRolledUpMiddle.ContextMenu = popAcquireItemMenu;
            pboxLeftRolledUpRing.ContextMenu = popAcquireItemMenu;
            pboxLeftRolledUpLittle.ContextMenu = popAcquireItemMenu;
            pboxRightRolledUpThumb.ContextMenu = popAcquireItemMenu;
            pboxRightRolledUpIndex.ContextMenu = popAcquireItemMenu;
            pboxRightRolledUpMiddle.ContextMenu = popAcquireItemMenu;
            pboxRightRolledUpRing.ContextMenu = popAcquireItemMenu;
            pboxRightRolledUpLittle.ContextMenu = popAcquireItemMenu;
            pboxLeftRolledDownThumb.ContextMenu = popAcquireItemMenu;
            pboxLeftRolledDownIndex.ContextMenu = popAcquireItemMenu;
            pboxLeftRolledDownMiddle.ContextMenu = popAcquireItemMenu;
            pboxLeftRolledDownRing.ContextMenu = popAcquireItemMenu;
            pboxLeftRolledDownLittle.ContextMenu = popAcquireItemMenu;
            pboxRightRolledDownThumb.ContextMenu = popAcquireItemMenu;
            pboxRightRolledDownIndex.ContextMenu = popAcquireItemMenu;
            pboxRightRolledDownMiddle.ContextMenu = popAcquireItemMenu;
            pboxRightRolledDownRing.ContextMenu = popAcquireItemMenu;
            pboxRightRolledDownLittle.ContextMenu = popAcquireItemMenu;
            // 1.15.0.0
            pboxLeftRolledHypothenar.ContextMenu = popAcquireItemMenu;
            pboxRightRolledHypothenar.ContextMenu = popAcquireItemMenu;
        }

        private void UserDataForm_Load(object sender, EventArgs e)
        {
            byte DeviceID;

            // handle to the form instance to pass to callback
            //gcUserDataFormHandle = GCHandle.Alloc(this, GCHandleType.Normal);

            // load scanned objects
            LoadScanObjectsList();

            // init arrays for slaps data
            int i;
            for (i = 0; i < 4; i++)
            {
                LeftSlapSegmentsData[i] = new SegmentData();
                RightSlapSegmentsData[i] = new SegmentData();
            }
            for (i = 0; i < 2; i++)
                TwoThumbsSegmentsData[i] = new SegmentData();

            // init array for joints data
            for (i = 0; i < 40; i++)
                JointsBBoxData[i] = new JointsData();

            ResetAllJointsData();

            if (!ViewMode)
            {
                // set form's caption
                Text = "New User";

                UserData = new DemoForm.UserData();

                // set device information
                if (DemoFormRef.Resolution1000Dpi)
                    UserData.AcquisitionDpi = 1000;
                else
                    UserData.AcquisitionDpi = 500;
                if (!FingerprintCard)
                {
                    UserData.DeviceName = DemoForm.GetDeviceName(DemoFormRef.CurrentDevice.DeviceID);
                    UserData.DeviceSerialNumber = DemoFormRef.CurrentDevice.DeviceSerialNumber;

                    // allocate buffer for max image size from scanner
                    ImageBuffer = new Byte[DemoFormRef.MaxImageBufferSize];
                }
                else
                {
                    UserData.DeviceName = "Fingerprint Card";
                    UserData.DeviceSerialNumber = "";
                }

                // create a new folder name
                ImagesPath = Path.GetDirectoryName(Application.ExecutablePath) + Path.DirectorySeparatorChar + Path.GetRandomFileName();
                if (!Directory.Exists(ImagesPath))
                {
                    try
                    {
                        Directory.CreateDirectory(ImagesPath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        this.Close();
                        return;
                    }
                }

                if (!FingerprintCard)
                {
                    // load configuration for types for sequence
                    chkSlaps.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.SequenceTypes, DemoForm.SequenceType.Slaps);
                    chkFlat.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.SequenceTypes, DemoForm.SequenceType.SingleFlat);
                    chkRolled.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.SequenceTypes, DemoForm.SequenceType.Rolled);
                    // 1.15.1.0
                    //chkPalms.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.SequenceTypes, DemoForm.SequenceType.Palms);
                    //V1.10
                    chkRolledJoints.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.SequenceTypes, DemoForm.SequenceType.RolledJoints);
                    chkFlatJointSides.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.SequenceTypes, DemoForm.SequenceType.FlatJointSides);
                    chkRolledTips.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.SequenceTypes, DemoForm.SequenceType.RolledTips);
                    chkRolledThenars.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.SequenceTypes, DemoForm.SequenceType.RolledThenars);
                    // 1.15.1.0
                    chkUpperPalms.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.SequenceTypes, DemoForm.SequenceType.UpperPalms);
                    chkLowerPalms.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.SequenceTypes, DemoForm.SequenceType.LowerPalms);
                    chkWritersPalms.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.SequenceTypes, DemoForm.SequenceType.WritersPalms);
                    chkRolledHypothenars.Checked = GBMSGUI.CheckMask(DemoFormRef.DemoConfig.SequenceTypes, DemoForm.SequenceType.RolledHypothenars);

                    // enable only supported objects
                    uint ObjectTypes;
                    GBMSAPI_NET_DeviceCharacteristicsRoutines.GBMSAPI_NET_GetScannableTypes(out ObjectTypes);
                    foreach (ScanItem Item in lstScannedObjects.Items)
                    {
                        if (GBMSGUI.CheckMask(ObjectTypes, GBMSAPI_NET_ScanObjectsUtilities.GBMSAPI_NET_GetTypeFromObject(Item.ScanObjID)))
                            Item.Supported = true;
                        else
                            Item.Supported = false;
                    }

                    DeviceID = DemoFormRef.CurrentDevice.DeviceID;
                    HideUnsupportedObjects(DeviceID, ObjectTypes);
                    btnScanCard.Visible = false;
                    Application.DoEvents();

                    if (DemoFormRef.LCDPresent)
                    {
                        btnAcquire.Text = btnAcquire.Text + "\n(touch button on LCD)";

                        // enable start button on LCD
                        GBMSAPI_NET_ExternalDevicesControlRoutines.GBMSAPI_NET_VUI_LCD_EnableStartButtonOnLogoScreen();
                        // start timer
                        TouchScreenTimer.Enabled = true;
                    }
                }
                else
                {
                    // show only slaps (not thumbs), rolled and flats (only thumbs)
                    tabControl1.SelectedIndex = -1;

                    tabControl1.TabPages.Remove(tabPalmPrints);
                    tabControl1.TabPages.Remove(tabSlaps2);
                    tabControl1.TabPages.Remove(tabEntireJointLeft);
                    tabControl1.TabPages.Remove(tabEntireJointRight);
                    tabControl1.TabPages.Remove(tabRolledTips);
                    tabControl1.TabPages.Remove(tabRolledThenar);
                    tabControl1.SelectedIndex = 0;

                    // hide unused
                    grpObjects.Hide();
                    grpMissingFingers.Hide();
                    btnAcquire.Visible = false;
                }
            }
            else
            {
                // set form's caption
                Text = "View User";
                txtSurname.ReadOnly = true;
                txtName.ReadOnly = true;
                btnConfig.Visible = false;
                grpObjects.Visible = false;
                btnAcquire.Visible = false;
                btnScanCard.Visible = false;

                // read user data
                UserData = DemoForm.UserData.Deserialize(ImagesPath + Path.DirectorySeparatorChar + "UserData.xml");
                txtSurname.Text = UserData.Surname;
                txtName.Text = UserData.Name;

                // read other data
                ReadItemsData();
                ReadSegmentsData();
                if (File.Exists(ImagesPath + Path.DirectorySeparatorChar + "JointsData.dat"))
                    ReadJointsData();

                DeviceID = DemoForm.GetDeviceIDFromName(UserData.DeviceName);
                HideUnsupportedObjects(DeviceID, 0xFFFFFFFF);
                Application.DoEvents();

                // load all images
                Cursor = Cursors.WaitCursor;
                LoadSavedImages();
                Cursor = Cursors.Default;

                //V1.10
                // for now not supported
                //EnableViewEJI();
            }
        }

        private void HideUnsupportedObjects(byte DeviceID, uint ObjectTypes)
        {
            tabControl1.SelectedIndex = -1;

            // hide tabs of unsupported objects
            if (!GBMSGUI.CheckMask(ObjectTypes, GBMSAPI_NET_ScannableBiometricTypes.GBMSAPI_NET_SBT_FLAT_LOWER_HALF_PALM))
            {
                tabControl1.TabPages.Remove(tabPalmPrints);
                // 1.15.1.0
                //chkPalms.Enabled = false;
                chkUpperPalms.Enabled = false;
                chkLowerPalms.Enabled = false;
                chkWritersPalms.Enabled = false;
            }

            if (!GBMSGUI.CheckMask(ObjectTypes, GBMSAPI_NET_ScannableBiometricTypes.GBMSAPI_NET_SBT_FLAT_SLAP_4))
            {
                tabControl1.TabPages.Remove(tabSlaps);
                // 1.15.0.0
                //chkSlaps.Enabled = false;
            }

            if (!GBMSGUI.CheckMask(ObjectTypes, GBMSAPI_NET_ScannableBiometricTypes.GBMSAPI_NET_SBT_FLAT_SLAP_2))
            {
                tabControl1.TabPages.Remove(tabSlaps2);
            }

            // 1.15.1.0
            if (!GBMSGUI.CheckMask(ObjectTypes, GBMSAPI_NET_ScannableBiometricTypes.GBMSAPI_NET_SBT_FLAT_SLAP_4) &&
                !GBMSGUI.CheckMask(ObjectTypes, GBMSAPI_NET_ScannableBiometricTypes.GBMSAPI_NET_SBT_FLAT_SLAP_2))
                chkSlaps.Enabled = false;

            if (!GBMSGUI.CheckMask(ObjectTypes, GBMSAPI_NET_ScannableBiometricTypes.GBMSAPI_NET_SBT_ROLL_SINGLE_FINGER))
            {
                tabControl1.TabPages.Remove(tabRolledFingerprints);
                chkRolled.Enabled = false;
            }

            //V1.10
            if (!GBMSGUI.CheckMask(ObjectTypes, GBMSAPI_NET_ScannableBiometricTypes.GBMSAPI_NET_SBT_ROLLED_JOINT))
            {
                tabControl1.TabPages.Remove(tabEntireJointLeft);
                tabControl1.TabPages.Remove(tabEntireJointRight);
                chkRolledJoints.Enabled = false;
                chkFlatJointSides.Enabled = false;
            }

            if (!GBMSGUI.CheckMask(ObjectTypes, GBMSAPI_NET_ScannableBiometricTypes.GBMSAPI_NET_SBT_ROLLED_TIP))
            {
                tabControl1.TabPages.Remove(tabRolledTips);
                chkRolledTips.Enabled = false;
            }

            if (!GBMSGUI.CheckMask(ObjectTypes, GBMSAPI_NET_ScannableBiometricTypes.GBMSAPI_NET_SBT_ROLLED_THENAR))
            {
                tabControl1.TabPages.Remove(tabRolledThenar);
                chkRolledThenars.Enabled = false;
            }

            // 1.13.4.0
            if (!GBMSGUI.CheckMask(ObjectTypes, GBMSAPI_NET_ScannableBiometricTypes.GBMSAPI_NET_SBT_ROLLED_UP))
            {
                tabControl1.TabPages.Remove(tabRolledUp);
                chkRolledUp.Enabled = false;
            }
            if (!GBMSGUI.CheckMask(ObjectTypes, GBMSAPI_NET_ScannableBiometricTypes.GBMSAPI_NET_SBT_ROLLED_DOWN))
            {
                tabControl1.TabPages.Remove(tabRolledDown);
                chkRolledDown.Enabled = false;
            }

            // 1.15.0.0
            if (!GBMSGUI.CheckMask(ObjectTypes, GBMSAPI_NET_ScannableBiometricTypes.GBMSAPI_NET_SBT_ROLLED_HYPOTHENAR))
            {
                tabControl1.TabPages.Remove(tabRolledHypothenar);
                // 1.15.1.0
                chkRolledHypothenars.Enabled = false;
            }

            tabControl1.SelectedIndex = 0;
        }

        private void LoadScanObjectsList()
        {
            ScanItem Item;

            lstScannedObjects.Items.Clear();

            // fill list with scanned objects
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_LEFT, "Left Flat Four Fingers", pboxLeftFourFingers);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_RIGHT, "Right Flat Four Fingers", pboxRightFourFingers);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_2_THUMBS, "Flat Two Thumbs", pboxTwoThumbs);
            lstScannedObjects.Items.Add(Item);
            //V1.2 - DS40
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_2_LEFT, "Left Flat Two Fingers", pboxLeftTwoFingers);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_2_RIGHT, "Right Flat Two Fingers", pboxRightTwoFingers);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_2_INDEXES, "Flat Two Indexes", pboxTwoIndexes);
            lstScannedObjects.Items.Add(Item);

            // 1.14.0.0 - moved palms before rolled (upper palm can be used for sequence check)
            // Palms
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_UPPER_HALF_PALM_LEFT, "Left Upper Half Palm", pboxLeftUpperPalm);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_UPPER_HALF_PALM_RIGHT, "Right Upper Half Palm", pboxRightUpperPalm);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_LOWER_HALF_PALM_LEFT, "Left Lower Half Palm", pboxLeftLowerPalm);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_LOWER_HALF_PALM_RIGHT, "Right Lower Half Palm", pboxRightLowerPalm);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_WRITER_PALM_LEFT, "Left Writer's Palm", pboxLeftWritersPalm);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_WRITER_PALM_RIGHT, "Right Writer's Palm", pboxRightWritersPalm);
            lstScannedObjects.Items.Add(Item);
            //V1.10
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_THENAR_LEFT, "Rolled Left Thenar", pboxLeftRolledThenar);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_THENAR_RIGHT, "Rolled Right Thenar", pboxRightRolledThenar);
            lstScannedObjects.Items.Add(Item);
            // 1.15.0.0
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_HYPOTHENAR_LEFT, "Rolled Left Hypothenar", pboxLeftRolledHypothenar);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_HYPOTHENAR_RIGHT, "Rolled Right Hypothenar", pboxRightRolledHypothenar);
            lstScannedObjects.Items.Add(Item);


            //V1.10 - group by finger
            // Left Thumb
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_LEFT_THUMB, "Left Flat Thumb", pboxLeftFlatThumb);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_LEFT_THUMB, "Left Rolled Thumb", pboxLeftRolledThumb);
            lstScannedObjects.Items.Add(Item);
            //V1.10
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_LEFT_THUMB, "Rolled Joint - Left Thumb", pboxLeftThumbRolledJoint);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_LEFT_THUMB, "Plain Joint Left Side - Left Thumb", pboxLeftThumbJointLeft);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_LEFT_THUMB, "Plain Joint Right Side - Left Thumb", pboxLeftThumbJointRight);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_LEFT_THUMB, "Rolled Joint Center - Left Thumb", pboxLeftThumbJointCenter);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_TIP_LEFT_THUMB, "Rolled Tip - Left Thumb", pboxLeftThumbRolledTip);
            lstScannedObjects.Items.Add(Item);
            // 1.13.4.0
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_UP_LEFT_THUMB, "Rolled Up - Left Thumb", pboxLeftRolledUpThumb);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_DOWN_LEFT_THUMB, "Rolled Down - Left Thumb", pboxLeftRolledDownThumb);
            lstScannedObjects.Items.Add(Item);

            // Left Index
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_LEFT_INDEX, "Left Flat Index", pboxLeftFlatIndex);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_LEFT_INDEX, "Left Rolled Index", pboxLeftRolledIndex);
            lstScannedObjects.Items.Add(Item);
            //V1.10
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_LEFT_INDEX, "Rolled Joint - Left Index", pboxLeftIndexRolledJoint);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_LEFT_INDEX, "Plain Joint Left Side - Left Index", pboxLeftIndexJointLeft);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_LEFT_INDEX, "Plain Joint Right Side - Left Index", pboxLeftIndexJointRight);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_LEFT_INDEX, "Rolled Joint Center - Left Index", pboxLeftIndexJointCenter);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_TIP_LEFT_INDEX, "Rolled Tip - Left Index", pboxLeftIndexRolledTip);
            lstScannedObjects.Items.Add(Item);
            // 1.13.4.0
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_UP_LEFT_INDEX, "Rolled Up - Left Index", pboxLeftRolledUpIndex);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_DOWN_LEFT_INDEX, "Rolled Down - Left Index", pboxLeftRolledDownIndex);
            lstScannedObjects.Items.Add(Item);

            // Left Middle
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_LEFT_MIDDLE, "Left Flat Middle", pboxLeftFlatMiddle);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_LEFT_MIDDLE, "Left Rolled Middle", pboxLeftRolledMiddle);
            lstScannedObjects.Items.Add(Item);
            //V1.10
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_LEFT_MIDDLE, "Rolled Joint - Left Middle", pboxLeftMiddleRolledJoint);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_LEFT_MIDDLE, "Plain Joint Left Side - Left Middle", pboxLeftMiddleJointLeft);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_LEFT_MIDDLE, "Plain Joint Right Side - Left Middle", pboxLeftMiddleJointRight);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_LEFT_MIDDLE, "Rolled Joint Center - Left Middle", pboxLeftMiddleJointCenter);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_TIP_LEFT_MIDDLE, "Rolled Tip - Left Middle", pboxLeftMiddleRolledTip);
            lstScannedObjects.Items.Add(Item);
            // 1.13.4.0
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_UP_LEFT_MIDDLE, "Rolled Up - Left Middle", pboxLeftRolledUpMiddle);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_DOWN_LEFT_MIDDLE, "Rolled Down - Left Middle", pboxLeftRolledDownMiddle);
            lstScannedObjects.Items.Add(Item);

            // Left Ring
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_LEFT_RING, "Left Flat Ring", pboxLeftFlatRing);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_LEFT_RING, "Left Rolled Ring", pboxLeftRolledRing);
            lstScannedObjects.Items.Add(Item);
            //V1.10
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_LEFT_RING, "Rolled Joint - Left Ring", pboxLeftRingRolledJoint);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_LEFT_RING, "Plain Joint Left Side - Left Ring", pboxLeftRingJointLeft);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_LEFT_RING, "Plain Joint Right Side - Left Ring", pboxLeftRingJointRight);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_LEFT_RING, "Rolled Joint Center - Left Ring", pboxLeftRingJointCenter);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_TIP_LEFT_RING, "Rolled Tip - Left Ring", pboxLeftRingRolledTip);
            lstScannedObjects.Items.Add(Item);
            // 1.13.4.0
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_UP_LEFT_RING, "Rolled Up - Left Ring", pboxLeftRolledUpRing);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_DOWN_LEFT_RING, "Rolled Down - Left Ring", pboxLeftRolledDownRing);
            lstScannedObjects.Items.Add(Item);

            // Left Little
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_LEFT_LITTLE, "Left Flat Little", pboxLeftFlatLittle);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_LEFT_LITTLE, "Left Rolled Little", pboxLeftRolledLittle);
            lstScannedObjects.Items.Add(Item);
            //V1.10
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_LEFT_LITTLE, "Rolled Joint - Left Little", pboxLeftLittleRolledJoint);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_LEFT_LITTLE, "Plain Joint Left Side - Left Little", pboxLeftLittleJointLeft);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_LEFT_LITTLE, "Plain Joint Right Side - Left Little", pboxLeftLittleJointRight);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_LEFT_LITTLE, "Rolled Joint Center - Left Little", pboxLeftLittleJointCenter);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_TIP_LEFT_LITTLE, "Rolled Tip - Left Little", pboxLeftLittleRolledTip);
            lstScannedObjects.Items.Add(Item);
            // 1.13.4.0
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_UP_LEFT_LITTLE, "Rolled Up - Left Little", pboxLeftRolledUpLittle);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_DOWN_LEFT_LITTLE, "Rolled Down - Left Little", pboxLeftRolledDownLittle);
            lstScannedObjects.Items.Add(Item);

            // Right Thumb
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_RIGHT_THUMB, "Right Flat Thumb", pboxRightFlatThumb);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_RIGHT_THUMB, "Right Rolled Thumb", pboxRightRolledThumb);
            lstScannedObjects.Items.Add(Item);
            //V1.10
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_RIGHT_THUMB, "Rolled Joint - Right Thumb", pboxRightThumbRolledJoint);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_RIGHT_THUMB, "Plain Joint Left Side - Right Thumb", pboxRightThumbJointLeft);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_RIGHT_THUMB, "Plain Joint Right Side - Right Thumb", pboxRightThumbJointRight);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_RIGHT_THUMB, "Rolled Joint Center - Right Thumb", pboxRightThumbJointCenter);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_TIP_RIGHT_THUMB, "Rolled Tip - Right Thumb", pboxRightThumbRolledTip);
            lstScannedObjects.Items.Add(Item);
            // 1.13.4.0
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_UP_RIGHT_THUMB, "Rolled Up - Right Thumb", pboxRightRolledUpThumb);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_DOWN_RIGHT_THUMB, "Rolled Down - Right Thumb", pboxRightRolledDownThumb);
            lstScannedObjects.Items.Add(Item);

            // Right Index
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_RIGHT_INDEX, "Right Flat Index", pboxRightFlatIndex);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_RIGHT_INDEX, "Right Rolled Index", pboxRightRolledIndex);
            lstScannedObjects.Items.Add(Item);
            //V1.10
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_RIGHT_INDEX, "Rolled Joint - Right Index", pboxRightIndexRolledJoint);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_RIGHT_INDEX, "Plain Joint Left Side - Right Index", pboxRightIndexJointLeft);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_RIGHT_INDEX, "Plain Joint Right Side - Right Index", pboxRightIndexJointRight);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_RIGHT_INDEX, "Rolled Joint Center - Right Index", pboxRightIndexJointCenter);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_TIP_RIGHT_INDEX, "Rolled Tip - Right Index", pboxRightIndexRolledTip);
            lstScannedObjects.Items.Add(Item);
            // 1.13.4.0
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_UP_RIGHT_INDEX, "Rolled Up - Right Index", pboxRightRolledUpIndex);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_DOWN_RIGHT_INDEX, "Rolled Down - Right Index", pboxRightRolledDownIndex);
            lstScannedObjects.Items.Add(Item);

            // Right Middle
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_RIGHT_MIDDLE, "Right Flat Middle", pboxRightFlatMiddle);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_RIGHT_MIDDLE, "Right Rolled Middle", pboxRightRolledMiddle);
            lstScannedObjects.Items.Add(Item);
            //V1.10
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_RIGHT_MIDDLE, "Rolled Joint - Right Middle", pboxRightMiddleRolledJoint);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_RIGHT_MIDDLE, "Plain Joint Left Side - Right Middle", pboxRightMiddleJointLeft);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_RIGHT_MIDDLE, "Plain Joint Right Side - Right Middle", pboxRightMiddleJointRight);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_RIGHT_MIDDLE, "Rolled Joint Center - Right Middle", pboxRightMiddleJointCenter);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_TIP_RIGHT_MIDDLE, "Rolled Tip - Right Middle", pboxRightMiddleRolledTip);
            lstScannedObjects.Items.Add(Item);
            // 1.13.4.0
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_UP_RIGHT_MIDDLE, "Rolled Up - Right Middle", pboxRightRolledUpMiddle);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_DOWN_RIGHT_MIDDLE, "Rolled Down - Right Middle", pboxRightRolledDownMiddle);
            lstScannedObjects.Items.Add(Item);

            // Right Ring
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_RIGHT_RING, "Right Flat Ring", pboxRightFlatRing);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_RIGHT_RING, "Right Rolled Ring", pboxRightRolledRing);
            lstScannedObjects.Items.Add(Item);
            //V1.10
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_RIGHT_RING, "Rolled Joint - Right Ring", pboxRightRingRolledJoint);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_RIGHT_RING, "Plain Joint Left Side - Right Ring", pboxRightRingJointLeft);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_RIGHT_RING, "Plain Joint Right Side - Right Ring", pboxRightRingJointRight);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_RIGHT_RING, "Rolled Joint Center - Right Ring", pboxRightRingJointCenter);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_TIP_RIGHT_RING, "Rolled Tip - Right Ring", pboxRightRingRolledTip);
            lstScannedObjects.Items.Add(Item);
            // 1.13.4.0
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_UP_RIGHT_RING, "Rolled Up - Right Ring", pboxRightRolledUpRing);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_DOWN_RIGHT_RING, "Rolled Down - Right Ring", pboxRightRolledDownRing);
            lstScannedObjects.Items.Add(Item);

            // Right Little
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_RIGHT_LITTLE, "Right Flat Little", pboxRightFlatLittle);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_RIGHT_LITTLE, "Right Rolled Little", pboxRightRolledLittle);
            lstScannedObjects.Items.Add(Item);
            //V1.10
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_RIGHT_LITTLE, "Rolled Joint - Right Little", pboxRightLittleRolledJoint);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_RIGHT_LITTLE, "Plain Joint Left Side - Right Little", pboxRightLittleJointLeft);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_RIGHT_LITTLE, "Plain Joint Right Side - Right Little", pboxRightLittleJointRight);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_RIGHT_LITTLE, "Rolled Joint Center - Right Little", pboxRightLittleJointCenter);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_TIP_RIGHT_LITTLE, "Rolled Tip - Right Little", pboxRightLittleRolledTip);
            lstScannedObjects.Items.Add(Item);
            // 1.13.4.0
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_UP_RIGHT_LITTLE, "Rolled Up - Right Little", pboxRightRolledUpLittle);
            lstScannedObjects.Items.Add(Item);
            Item = new ScanItem(GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_DOWN_RIGHT_LITTLE, "Rolled Down - Right Little", pboxRightRolledDownLittle);
            lstScannedObjects.Items.Add(Item);
        }

        private void btnAcquire_Click(object sender, EventArgs e)
        {
            // delete all files
            EmptyFolder(ImagesPath);
            // clear all pictureboxes
            foreach (ScanItem Item in lstScannedObjects.Items)
            {
                if (Item.ImagePictureBox.Image != null)
                    Item.ImagePictureBox.Image.Dispose();
                Item.ImagePictureBox.Image = null;
            }
            ResetScanItemsData();

            SelectAllScanItems();
            Acquire();

            //V1.10
            // for now not supported
            //EnableViewEJI();

            // after acquisition, re-enable timer
            GBMSAPI_NET_ExternalDevicesControlRoutines.GBMSAPI_NET_VUI_LCD_EnableStartButtonOnLogoScreen();
            TouchScreenTimer.Enabled = true;
        }

        private void Acquire()
        {
            int Index;
            int ret;
            int ImageSizeX, ImageSizeY;
            int Resolution;
            Bitmap bmp;
            uint AcqOptions = DemoFormRef.DemoConfig.AcquisitionOptions;
            uint SessionOpt = DemoFormRef.DemoConfig.SessionOptions; ;
            String PersonID;
            int AcquisitionMode;
            int QualityThreshold1, QualityThreshold2;

            byte DeviceID = DemoFormRef.CurrentDevice.DeviceID;

            uint DeviceFeatures;
            GBMSAPI_NET_DeviceCharacteristicsRoutines.GBMSAPI_NET_GetDeviceFeatures(out DeviceFeatures);

            // disable TouchScreen
            TouchScreenTimer.Enabled = false;

            PersonID = txtSurname.Text + " " + txtName.Text;

            // set language
            SetLanguage();

            // set AfterStartCallback (if needed)
            //gcUserDataFormHandle = GCHandle.Alloc(this); // pass pointer to the form instance to callback
            //MyGUI.SetAfterStartCallback(AfterStartCallbackRef, GCHandle.ToIntPtr(gcUserDataFormHandle));
            // use non-static callback
            MyGUI.SetAfterStartCallback(AfterStartCallback, IntPtr.Zero);

            // set quality algorithm
            ret = MyGUI.SetQualityAlgorithm(DemoFormRef.DemoConfig.IAFIsQualityAlgorithm);
            if (ret == GBMSGUI.ReturnCodes.Ret_Failure)
            {
                MessageBox.Show(MyGUI.GetErrorMessage() + " (SetQualityAlgorithm)",
                        Application.ProductName,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // set the thresholds according to the algorithm
            // IMPORTANT NOTE: the following thresholds are set more permissive for DEMO purpose;
            // don't use the values as an example, define your thresholds according to your application needs;
            if (DemoFormRef.DemoConfig.IAFIsQualityAlgorithm == GBMSGUI.QualityAlgorithms.NFIQAlgorithm)
            {
                //QualityThreshold1 = 3;
                //QualityThreshold2 = 4;
                // 2.0.0.0
                QualityThreshold1 = DemoFormRef.DemoConfig.NFIQQualityThreshold1;
                QualityThreshold2 = DemoFormRef.DemoConfig.NFIQQualityThreshold2;
            }
            else if (DemoFormRef.DemoConfig.IAFIsQualityAlgorithm == GBMSGUI.QualityAlgorithms.NFIQ2Algorithm)
            {
                // 2.0.0.0
                QualityThreshold1 = DemoFormRef.DemoConfig.NFIQ2QualityThreshold1;
                QualityThreshold2 = DemoFormRef.DemoConfig.NFIQ2QualityThreshold2;
            }
            else // GB algorithm
            {
                //QualityThreshold1 = 50;
                //QualityThreshold2 = 70;
                // 2.0.0.0
                QualityThreshold1 = DemoFormRef.DemoConfig.GBQualityThreshold1;
                QualityThreshold2 = DemoFormRef.DemoConfig.GBQualityThreshold2;
            }

            /*
            // other settings - for demo a little more permissive
            // see the NOTE above
            MyGUI.SetArtefactsThresholds(15, 30);
            MyGUI.SetLowerPalmCompletenessThresholds(70, 80);
            MyGUI.SetBlockAutoCaptureContrast(DemoFormRef.DemoConfig.BlockAutocaptureContrast);
            MyGUI.SetPatternValidityThreshold(65);
            MyGUI.SetPatternCompletenessThreshold(75);
            */
            // 2.0.0.0
            //MyGUI.SetArtefactsThresholds(DemoFormRef.DemoConfig.ArtefactsThreshold1, DemoFormRef.DemoConfig.ArtefactsThreshold2);
            // 2.0.1.0 - moved in the for, to adapt threshold for different objects

            MyGUI.SetArtefactsThresholds(DemoFormRef.DemoConfig.ArtefactsThreshold1, DemoFormRef.DemoConfig.ArtefactsThreshold2);
            MyGUI.SetLowerPalmCompletenessThresholds(DemoFormRef.DemoConfig.LowerPalmCompletenessThreshold1, DemoFormRef.DemoConfig.LowerPalmCompletenessThreshold2);
            // TODO ?
            MyGUI.SetBlockAutoCaptureContrast(DemoFormRef.DemoConfig.BlockAutocaptureContrast);
            MyGUI.SetPatternValidityThreshold(DemoFormRef.DemoConfig.PatternValidityThreshold);
            MyGUI.SetPatternCompletenessThreshold(DemoFormRef.DemoConfig.PatternCompletenessThreshold);

            // set window size and position
            ret = MyGUI.SetWindowSizeAndPosition(DemoFormRef.DemoConfig.WindowSize.X,
                DemoFormRef.DemoConfig.WindowSize.Y,
                DemoFormRef.DemoConfig.WindowSize.Width,
                DemoFormRef.DemoConfig.WindowSize.Height,
                DemoFormRef.DemoConfig.WindowMaximized);
            // ignore error
            /*
            if (ret == GBMSGUI.ReturnCodes.Ret_Failure)
            {
                MessageBox.Show(MyGUI.GetErrorMessage() + " (SetWindowSizeAndPosition)",
                        Application.ProductName,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            */

            MyGUI.EnableBlockAutocaptureLedColorFeedback(Convert.ToInt32(DemoFormRef.DemoConfig.EnableBlockAutocaptureLedColorFeedback));

            // because acquisition form shown by GBMSGUI is not modal, disable buttons
            btnAcquire.Enabled = false;
            mnuAcquireItem.Enabled = false;
            
            // if acquiring single item, disable sequence check
            if (lstScannedObjects.CheckedItems.Count == 1)
            {
                SessionOpt &= ~GBMSGUI.SessionOption.SequenceCheck;
                AcquisitionMode = GBMSGUI.AcquisitionModes.SingleAcquisition;
            }
            else
            {
                AcquisitionMode = GBMSGUI.AcquisitionModes.MultipleAcquisition;
            }

            // 2.0.0.0
            if (GBMSGUI.CheckMask(SessionOpt, GBMSGUI.SessionOption.SWFakeFingerDetection)
                && GBMSGUI.CheckMask(DeviceFeatures, GBMSAPI_NET_DeviceFeatures.GBMSAPI_NET_DF_SW_ANTIFAKE))
            {
                ret = GBMSAPI_NET_ScanSettingsRoutines.GBMSAPI_NET_SetSoftwareFakeFingerDetectionThreshold(DemoFormRef.DemoConfig.SWFakeFingerDetectionThreshold);
                if (ret != GBMSAPI_NET_ErrorCodes.GBMSAPI_NET_ERROR_CODE_NO_ERROR)
                {
                    MessageBox.Show(MyGUI.GetMSAPIErrorMessage("GBMSAPI_NET_SetSoftwareFakeFingerDetectionThreshold", ret),
                            Application.ProductName,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 2.3.2.0
                ret = MyGUI.SetMaxAllowedFakesInSlap4(DemoFormRef.DemoConfig.MaxAllowedFakesInSlap4);
                if (ret == GBMSGUI.ReturnCodes.Ret_Failure)
                {
                    MessageBox.Show(MyGUI.GetErrorMessage() + " (SetMaxAllowedFakesInSlap4)",
                        Application.ProductName,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                ret = MyGUI.SetMaxAllowedFakesInSlap2(DemoFormRef.DemoConfig.MaxAllowedFakesInSlap2);
                if (ret == GBMSGUI.ReturnCodes.Ret_Failure)
                {
                    MessageBox.Show(MyGUI.GetErrorMessage() + " (SetMaxAllowedFakesInSlap2)",
                        Application.ProductName,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            // 2.0.0.0
            if (GBMSGUI.CheckMask(DeviceFeatures, GBMSAPI_NET_DeviceFeatures.GBMSAPI_NET_DF_HW_ANTIFAKE))
            {
                ret = GBMSAPI_NET_ScanSettingsRoutines.GBMSAPI_NET_SetHardwareFakeFingerDetectionThreshold(DemoFormRef.DemoConfig.HWFakeFingerDetectionThreshold);
                if (ret != GBMSAPI_NET_ErrorCodes.GBMSAPI_NET_ERROR_CODE_NO_ERROR)
                {
                    MessageBox.Show(MyGUI.GetMSAPIErrorMessage("GBMSAPI_NET_SetHardwareFakeFingerDetectionThreshold", ret),
                            Application.ProductName,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // start the acquisition session
            ret = MyGUI.StartSession(AcquisitionMode, PersonID, SessionOpt);
            if (ret != GBMSGUI.ReturnCodes.Ret_Success)
            {
                // re-enable our controls
                btnAcquire.Enabled = true;
                mnuAcquireItem.Enabled = true;

                if (ret == GBMSGUI.ReturnCodes.Ret_Failure)
                    MessageBox.Show(MyGUI.GetErrorMessage() + " (StartSession)",
                        Application.ProductName,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("StartSession error " + ret.ToString(),
                        Application.ProductName,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2.4.0.0
            MyGUI.SetFlatFingerOnRollArea(DemoFormRef.DemoConfig.FlatFingerOnRollArea);

            MyGUI.SelectFingerContactEvaluationMode(DemoFormRef.DemoConfig.FingerContactEvaluationMode);

            if (GBMSGUI.CheckMask(AcqOptions, GBMSGUI.AcquisitionOption.AdaptRollAreaPosition))
                MyGUI.SetAdaptiveRollDirection(DemoFormRef.DemoConfig.RollDirection);

            MyGUI.SetBlockAutoCaptureMask(DemoFormRef.DemoConfig.BlockAutocaptureMask);

            MyGUI.SetIgnoreDiagnosticMask(DemoFormRef.DemoConfig.IgnoredDiagnosticMask);

            // get object Id from list
            //foreach (ScanItem Item in lstScannedObjects.CheckedItems)
            ScanItem Item;
            for (Index = 0; Index < lstScannedObjects.CheckedItems.Count; Index++)
            {
                Item = (ScanItem)lstScannedObjects.CheckedItems[Index];

                // skip items not supported by the scanner
                if (!Item.Supported)
                    continue;

                // skip Two thumbs for DS40
                if ((DeviceID == GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_DS40I) &&
                    (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_2_THUMBS))
                    continue;

                // save current item (used in AfterStartCallback)
                CurrentScanItem = Item;

                // set roll area size (if supported)
                if (GBMSGUI.IsRolled(Item.ScanObjID) &&
                    (GBMSGUI.CheckMask(DeviceFeatures, GBMSAPI_NET_DeviceFeatures.GBMSAPI_NET_DF_ROLL_AREA_GA)))
                {
                    if (DemoFormRef.DemoConfig.RollAreaSize == GBMSAPI_NET_DeviceFeatures.GBMSAPI_NET_DF_ROLL_AREA_IQS)
                        //GBMSAPI_NET_DeviceCharacteristicsRoutines.GBMSAPI_NET_SetRollAreaStandard(GBMSAPI_NET_DeviceFeatures.GBMSAPI_NET_DF_ROLL_AREA_IQS);
                        MyGUI.SetRollArea(GBMSGUI.RollAreaType.RollAreaIQS);
                    else
                        //GBMSAPI_NET_DeviceCharacteristicsRoutines.GBMSAPI_NET_SetRollAreaStandard(GBMSAPI_NET_DeviceFeatures.GBMSAPI_NET_DF_ROLL_AREA_GA);
                        MyGUI.SetRollArea(GBMSGUI.RollAreaType.RollAreaGA);
                }

                // 2.0.1.0 - adapt threshold for different objects
                if (GBMSGUI.IsRolledJoint(Item.ScanObjID))
                    MyGUI.SetArtefactsThresholds(DemoFormRef.DemoConfig.ArtefactsThreshold1*3, DemoFormRef.DemoConfig.ArtefactsThreshold2*3);
                else if (GBMSGUI.IsRolledThenar(Item.ScanObjID) || GBMSGUI.IsRolledHypothenar(Item.ScanObjID))
                    MyGUI.SetArtefactsThresholds(DemoFormRef.DemoConfig.ArtefactsThreshold1*5, DemoFormRef.DemoConfig.ArtefactsThreshold2*5);
                else
                    MyGUI.SetArtefactsThresholds(DemoFormRef.DemoConfig.ArtefactsThreshold1, DemoFormRef.DemoConfig.ArtefactsThreshold2);

                // set image size
                ret = SetImageSize(Item.ScanObjID);
                if (ret == GBMSGUI.ReturnCodes.Ret_Failure)
                {
                    MessageBox.Show(MyGUI.GetErrorMessage() + " (SetImageSize)",
                        Application.ProductName,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //break;      // break sequence
                }

                // set size for segments
                // 1.14.0.0 - also for upper palm
                if (GBMSGUI.IsSlap(Item.ScanObjID) || GBMSGUI.IsUpperPalm(Item.ScanObjID))
                {
                    ret = SetSegmentsImageSize(Item.ScanObjID);
                    if (ret == GBMSGUI.ReturnCodes.Ret_Failure)
                    {
                        MessageBox.Show(MyGUI.GetErrorMessage() + " (SetSegmentsImageSize)",
                        Application.ProductName,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //break;      // break sequence
                    }
                }

                // quality thresholds for each object
                ret = MyGUI.SetQualityThresholds(Item.ScanObjID, QualityThreshold1, QualityThreshold2);
                if (ret == GBMSGUI.ReturnCodes.Ret_Failure)
                {
                    MessageBox.Show(MyGUI.GetErrorMessage() + " (SetQualityThresholds)",
                        Application.ProductName,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;      // break sequence
                }

                // 1.14.0.0 - also for upper palm
                if (GBMSGUI.IsSlap(Item.ScanObjID) || GBMSGUI.IsUpperPalm(Item.ScanObjID))
                {
                    // quality thresholds for segments
                    MyGUI.SetSegmentQualityThresholds(Item.ScanObjID, 0, QualityThreshold1, QualityThreshold2);
                    MyGUI.SetSegmentQualityThresholds(Item.ScanObjID, 1, QualityThreshold1, QualityThreshold2);
                    if ((Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_LEFT) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_RIGHT) ||
                        // 2.0.1.0 - was missing
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_UPPER_HALF_PALM_LEFT) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_UPPER_HALF_PALM_RIGHT))
                    {
                        MyGUI.SetSegmentQualityThresholds(Item.ScanObjID, 2, QualityThreshold1, QualityThreshold2);
                        MyGUI.SetSegmentQualityThresholds(Item.ScanObjID, 3, QualityThreshold1, QualityThreshold2);
                    }

                    // missing fingers
                    SetMissingFingers();
                }

                //if (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_LEFT)
                //    // TEST - set left ring as unavailable
                //    MyGUI.SetSegmentUnavailabilityReason(Item.ScanObjID, 2, GBMSGUI.UnavailabilityReason.Unprintable);

                // 2.3.0.0
                // 2.3.0.1 - moved before SetFrameRate (it affects the frame rate used)
                if (GBMSGUI.CheckMask(DeviceFeatures, GBMSAPI_NET_DeviceFeatures.GBMSAPI_NET_DF_USB_3_0_SUPPORT))
                {
                    // get link speed
                    byte USBLinkSpeed = 0;
                    GBMSAPI_NET_ExternalDevicesControlRoutines.GBMSAPI_NET_GetUsbLinkSpeed(out USBLinkSpeed);
                    if (USBLinkSpeed == GBMSAPI_NET_UsbLinkValues.GBMSAPI_NET_USB_LINK_SUPER_SPEED)
                    {
                        // if connected in USB3, force full-res preview
                        AcqOptions |= GBMSGUI.AcquisitionOption.FullResPreview;
                    }
                }

                // set frame rate
                if (GBMSGUI.CheckMask(DeviceFeatures, GBMSAPI_NET_DeviceFeatures.GBMSAPI_NET_DF_FRAME_RATE_SETTING))
                {
                    //double FrameRate = 5;
                    double FrameRate = 0;
                    uint FrameRateOptions = 0;

                    if (DeviceID == GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_DS26)
                        FrameRate = DemoFormRef.DemoConfig.DS26FrameRate;
                    else if (DeviceID == GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_DS84)
                    {
                        if (GBMSGUI.IsRolled(Item.ScanObjID))
                            FrameRate = DemoFormRef.DemoConfig.DS84PartialFrameRate;
                        else
                            FrameRate = DemoFormRef.DemoConfig.DS84FullFrameRate;
                    }
                    else if (DeviceID == GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_DS40I)
                    {
                        if (GBMSGUI.IsRolled(Item.ScanObjID))
                            FrameRate = DemoFormRef.DemoConfig.DS40iPartialFrameRate;
                        else
                            FrameRate = DemoFormRef.DemoConfig.DS40iFullFrameRate;
                    }
                    else if (DeviceID == GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_DS84C)
                    {
                        if (GBMSGUI.IsRolled(Item.ScanObjID))
                            FrameRate = DemoFormRef.DemoConfig.DS84cPartialFrameRate;
                        else
                        {
                            // hi or low res
                            if (GBMSGUI.CheckMask(AcqOptions, GBMSGUI.AcquisitionOption.FullResPreview))
                                FrameRate = DemoFormRef.DemoConfig.DS84cFullHiResFrameRate;
                            else
                                FrameRate = DemoFormRef.DemoConfig.DS84cFullLowResFrameRate;
                        }
                    }
                    else if ((DeviceID == GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_MC517) ||
                        (DeviceID == GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_MSC517))
                    {
                        if (GBMSGUI.IsRolled(Item.ScanObjID))
                        {
                            // AdaptRollArea has same setting as Full low res
                            if (DemoFormRef.DemoConfig.RollAreaSize == GBMSAPI_NET_DeviceFeatures.GBMSAPI_NET_DF_ROLL_AREA_IQS)
                            {
                                if (GBMSGUI.CheckMask(AcqOptions, GBMSGUI.AcquisitionOption.AdaptRollAreaPosition))
                                    FrameRate = DemoFormRef.DemoConfig.MC517FullLowResFrameRate;
                                else
                                    FrameRate = DemoFormRef.DemoConfig.MC517PartialIQSFrameRate;
                            }
                            else
                            {
                                if (GBMSGUI.CheckMask(AcqOptions, GBMSGUI.AcquisitionOption.AdaptRollAreaPosition))
                                    FrameRate = DemoFormRef.DemoConfig.MC517FullLowResFrameRate;
                                else
                                    FrameRate = DemoFormRef.DemoConfig.MC517PartialGAFrameRate;
                            }
                        }
                        else
                        {
                            // hi or low res
                            if (GBMSGUI.CheckMask(AcqOptions, GBMSGUI.AcquisitionOption.FullResPreview))
                                FrameRate = DemoFormRef.DemoConfig.MC517FullHiResFrameRate;
                            else
                                FrameRate = DemoFormRef.DemoConfig.MC517FullLowResFrameRate;
                        }
                    }
                    else if (DeviceID == GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_DS32)
                    {
                        if (GBMSGUI.IsRolled(Item.ScanObjID))
                            FrameRate = DemoFormRef.DemoConfig.DS32PartialFrameRate;
                        else
                            FrameRate = DemoFormRef.DemoConfig.DS32FullFrameRate;
                    }
                    else if (DeviceID == GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_MS527)
                    {
                        // AdaptRollArea has same setting as Full low res
                        if (GBMSGUI.IsRolledThenar(Item.ScanObjID)
                            // 1.15.0.0
                            || GBMSGUI.IsRolledHypothenar(Item.ScanObjID))
                        {
                            if (GBMSGUI.CheckMask(AcqOptions, GBMSGUI.AcquisitionOption.AdaptRollAreaPosition))
                                FrameRate = DemoFormRef.DemoConfig.MS527FullLowResFrameRate;
                            else
                                FrameRate = DemoFormRef.DemoConfig.MS527PartialThenarFrameRate;
                        }
                        else if (GBMSGUI.IsRolledJoint(Item.ScanObjID))
                        {
                            if (GBMSGUI.CheckMask(AcqOptions, GBMSGUI.AcquisitionOption.AdaptRollAreaPosition))
                                FrameRate = DemoFormRef.DemoConfig.MS527FullLowResFrameRate;
                            else
                                FrameRate = DemoFormRef.DemoConfig.MS527PartialJointFrameRate;
                        }
                        else if (GBMSGUI.IsRolled(Item.ScanObjID))
                        {
                            if (GBMSGUI.CheckMask(AcqOptions, GBMSGUI.AcquisitionOption.AdaptRollAreaPosition))
                                FrameRate = DemoFormRef.DemoConfig.MS527FullLowResFrameRate;
                            else
                            {
                                if (DemoFormRef.DemoConfig.RollAreaSize == GBMSAPI_NET_DeviceFeatures.GBMSAPI_NET_DF_ROLL_AREA_IQS)
                                    FrameRate = DemoFormRef.DemoConfig.MS527PartialIQSFrameRate;
                                else
                                    FrameRate = DemoFormRef.DemoConfig.MS527PartialGAFrameRate;
                            }
                        }
                        else
                        {
                            // hi or low res
                            if (GBMSGUI.CheckMask(AcqOptions, GBMSGUI.AcquisitionOption.FullResPreview))
                                FrameRate = DemoFormRef.DemoConfig.MS527FullHiResFrameRate;
                            else
                                FrameRate = DemoFormRef.DemoConfig.MS527FullLowResFrameRate;
                        }
                    }
                    else if (DeviceID == GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_DS84t)
                    {
                        if (GBMSGUI.IsRolled(Item.ScanObjID))
                            FrameRate = DemoFormRef.DemoConfig.DS84tPartialFrameRate;
                        else
                        {
                            // hi or low res
                            if (GBMSGUI.CheckMask(AcqOptions, GBMSGUI.AcquisitionOption.FullResPreview))
                                FrameRate = DemoFormRef.DemoConfig.DS84tFullHiResFrameRate;
                            else
                                FrameRate = DemoFormRef.DemoConfig.DS84tFullLowResFrameRate;
                        }
                    }
                    // 2.0.1.0
                    else if (DeviceID == GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_DSID20)
                        FrameRate = DemoFormRef.DemoConfig.DID20FrameRate;
                    // 2.3.0.0
                    else if (DeviceID == GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_MS527t)
                    {
                        // AdaptRollArea has same setting as Full low res
                        if (GBMSGUI.IsRolledThenar(Item.ScanObjID)
                            // 1.15.0.0
                            || GBMSGUI.IsRolledHypothenar(Item.ScanObjID))
                        {
                            if (GBMSGUI.CheckMask(AcqOptions, GBMSGUI.AcquisitionOption.AdaptRollAreaPosition))
                                FrameRate = DemoFormRef.DemoConfig.MS527tFullLowResFrameRate;
                            else
                                FrameRate = DemoFormRef.DemoConfig.MS527tPartialThenarFrameRate;
                        }
                        else if (GBMSGUI.IsRolledJoint(Item.ScanObjID))
                        {
                            if (GBMSGUI.CheckMask(AcqOptions, GBMSGUI.AcquisitionOption.AdaptRollAreaPosition))
                                FrameRate = DemoFormRef.DemoConfig.MS527tFullLowResFrameRate;
                            else
                                FrameRate = DemoFormRef.DemoConfig.MS527tPartialJointFrameRate;
                        }
                        else if (GBMSGUI.IsRolled(Item.ScanObjID))
                        {
                            if (GBMSGUI.CheckMask(AcqOptions, GBMSGUI.AcquisitionOption.AdaptRollAreaPosition))
                                FrameRate = DemoFormRef.DemoConfig.MS527tFullLowResFrameRate;
                            else
                            {
                                if (DemoFormRef.DemoConfig.RollAreaSize == GBMSAPI_NET_DeviceFeatures.GBMSAPI_NET_DF_ROLL_AREA_IQS)
                                    FrameRate = DemoFormRef.DemoConfig.MS527tPartialIQSFrameRate;
                                else
                                    FrameRate = DemoFormRef.DemoConfig.MS527tPartialGAFrameRate;
                            }
                        }
                        else
                        {
                            // hi or low res
                            if (GBMSGUI.CheckMask(AcqOptions, GBMSGUI.AcquisitionOption.FullResPreview))
                                FrameRate = DemoFormRef.DemoConfig.MS527tFullHiResFrameRate;
                            else
                                FrameRate = DemoFormRef.DemoConfig.MS527tFullLowResFrameRate;
                        }
                    }


                    //if (GBMSGUI.IsRolled(Item.ScanObjID))
                    //    FrameRateOptions |= GBMSAPI_NET_FrameRateOptions.GBMSAPI_NET_FRO_ROLL_AREA;
                    //if (GBMSGUI.CheckMask(DemoFormRef.DemoConfig.AcquisitionOptions, GBMSGUI.AcquisitionOption.FullResPreview))
                        //FrameRateOptions |= GBMSAPI_NET_FrameRateOptions.GBMSAPI_NET_FRO_FULL_RESOLUTION_MODE;

                    uint SupportedScanAreas;
                    GBMSAPI_NET_DeviceCharacteristicsRoutines.GBMSAPI_NET_GetSupportedScanAreas(out SupportedScanAreas);

                    uint ScanArea = GBMSAPI_NET_ScanAreas.GBMSAPI_NET_SA_FULL_FRAME;

                    if (GBMSGUI.IsRolledThenar(Item.ScanObjID)
                        // 1.15.0.0
                        || GBMSGUI.IsRolledHypothenar(Item.ScanObjID))
                    {
                        ScanArea = GBMSAPI_NET_ScanAreas.GBMSAPI_NET_SA_ROLL_THENAR;
                    }
                    else if (GBMSGUI.IsRolledJoint(Item.ScanObjID) ||
                         GBMSGUI.IsFlatJoint(Item.ScanObjID))
                    {
                        ScanArea = GBMSAPI_NET_ScanAreas.GBMSAPI_NET_SA_ROLL_JOINT;
                    }
                    else if (GBMSGUI.IsRolled(Item.ScanObjID))
                    {
                        if (DemoFormRef.DemoConfig.RollAreaSize == GBMSAPI_NET_DeviceFeatures.GBMSAPI_NET_DF_ROLL_AREA_GA)
                        {
                            if (GBMSGUI.CheckMask(SupportedScanAreas, GBMSAPI_NET_ScanAreas.GBMSAPI_NET_SA_ROLL_GA))
                                ScanArea = GBMSAPI_NET_ScanAreas.GBMSAPI_NET_SA_ROLL_GA;
                            else
                                ScanArea = GBMSAPI_NET_ScanAreas.GBMSAPI_NET_SA_ROLL_IQS;
                        }
                        else
                        {
                            if (GBMSGUI.CheckMask(SupportedScanAreas, GBMSAPI_NET_ScanAreas.GBMSAPI_NET_SA_ROLL_IQS))
                                ScanArea = GBMSAPI_NET_ScanAreas.GBMSAPI_NET_SA_ROLL_IQS;
                            else
                                ScanArea = GBMSAPI_NET_ScanAreas.GBMSAPI_NET_SA_ROLL_GA;
                        }
                    }

                    if (GBMSGUI.CheckMask(AcqOptions, GBMSGUI.AcquisitionOption.AdaptRollAreaPosition))
                        FrameRateOptions |= GBMSAPI_NET_FrameRateOptions.GBMSAPI_NET_FRO_ADAPT_ROLL_AREA_POSITION;

                    if ((ScanArea == GBMSAPI_NET_ScanAreas.GBMSAPI_NET_SA_FULL_FRAME) &&
                        GBMSGUI.CheckMask(AcqOptions, GBMSGUI.AcquisitionOption.FullResPreview))
                        FrameRateOptions |= GBMSAPI_NET_FrameRateOptions.GBMSAPI_NET_FRO_HIGH_RES_IN_PREVIEW;

                    // obsolete
                    /*
                    ret = GBMSAPI_NET_ScanSettingsRoutines.GBMSAPI_NET_SetFrameRateComplete(FrameRateOptions, FrameRate);
                    if (ret != GBMSAPI_NET_ErrorCodes.GBMSAPI_NET_ERROR_CODE_NO_ERROR)
                    {
                        MessageBox.Show(MyGUI.GetMSAPIErrorMessage("GBMSAPI_NET_SetFrameRateComplete", ret),
                                Application.ProductName,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;      // break sequence
                    }
                    */

                    if (FrameRate != 0) // 2.2.0.0
                    {
                        ret = GBMSAPI_NET_ScanSettingsRoutines.GBMSAPI_NET_SetFrameRate2(ScanArea, FrameRateOptions, FrameRate);
                        if (ret != GBMSAPI_NET_ErrorCodes.GBMSAPI_NET_ERROR_CODE_NO_ERROR)
                        {
                            MessageBox.Show(MyGUI.GetMSAPIErrorMessage("GBMSAPI_NET_SetFrameRate2", ret),
                                    Application.ProductName,
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            // ignore error and continue
                            //break;      // break sequence
                        }
                    }
                }

                // set Block composition (if supported)
                if (GBMSGUI.IsRolled(Item.ScanObjID) &&
                    (GBMSGUI.CheckMask(DeviceFeatures, GBMSAPI_NET_DeviceFeatures.GBMSAPI_NET_DF_ENABLE_BLOCK_ROLL_COMPOSITION)))
                {
                    ret = GBMSAPI_NET_ScanSettingsRoutines.GBMSAPI_NET_ROLL_EnableBlockComposition(DemoFormRef.DemoConfig.EnableBlockComposition);
                    if (ret != GBMSAPI_NET_ErrorCodes.GBMSAPI_NET_ERROR_CODE_NO_ERROR)
                    {
                        MessageBox.Show(MyGUI.GetMSAPIErrorMessage("GBMSAPI_NET_ROLL_EnableBlockComposition", ret),
                                Application.ProductName,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // ignore error and continue
                    }
                }

                // manage joint segmentation option
                if (GBMSGUI.IsJoint(Item.ScanObjID) && !DemoFormRef.DemoConfig.JointSegmentation)
                    AcqOptions &= ~GBMSGUI.AcquisitionOption.Segmentation;

                if (GBMSGUI.CheckMask(AcqOptions, GBMSGUI.AcquisitionOption.LiveSegmentsEval))
                    MyGUI.SetLiveSegmEvalTimeout(DemoFormRef.DemoConfig.LiveSegmEvalTimeout);

            Repeat:
                // 2.0.1.0 - reset current item
                ResetScanItemData(Item);

                // acquire item
                ret = MyGUI.Acquire(Item.ScanObjID, AcqOptions, ImageBuffer, out ImageSizeX, out ImageSizeY, out Resolution);

                // if single acquisition (acquisition window is hidden after acquire), re-enable our window now
                if (AcquisitionMode == GBMSGUI.AcquisitionModes.SingleAcquisition)
                {
                    btnAcquire.Enabled = true;
                    mnuAcquireItem.Enabled = true;
                }

                if (ret == GBMSGUI.ReturnCodes.Ret_Failure)
                {
                    DialogResult Res = MessageBox.Show(MyGUI.GetErrorMessage() + " (Acquire)", 
                        Application.ProductName,
                        MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Exclamation);
                    if (Res == DialogResult.Abort)
                        break;      // break sequence
                    else if (Res == DialogResult.Retry)
                    {
                        // repeat same acquisition
                        goto Repeat;
                    }
                    else if (Res == DialogResult.Ignore)
                        continue;   // go on
                }
                else if (ret == GBMSGUI.ReturnCodes.Ret_BreakSequence)
                {
                    break;
                }
                else if (ret == GBMSGUI.ReturnCodes.Ret_SkipImage)
                {
                    // if option is set, get unavailability reason
                    if (GBMSGUI.CheckMask(SessionOpt, GBMSGUI.SessionOption.AskUnavailabilityReason))
                    {
                        MyGUI.GetUnavailabilityReason(Item.ScanObjID, out Item.ItemData.UnavailabilityReason);
                        // 2.0.1.0
                        DisableScanItemSameFinger(Item.ScanObjID, -1, Item.ItemData.UnavailabilityReason);
                    }

                    continue;   // go on
                }
                else if (ret == GBMSGUI.ReturnCodes.Ret_GoBack)
                {
                    if (Index > 0)
                    {
                        Index--;
                        Index--;

                        continue;
                    }
                    else
                        break;
                }
                else if (ret == GBMSGUI.ReturnCodes.Ret_Success)
                {
                    // save image in Bmp format
                    bmp = GBMSGUI.RawImageToBitmap(ImageBuffer, ImageSizeX, ImageSizeY);
                    String FileName = BuildImageFileName(Item);
                    bmp.SetResolution((float)Resolution, (float)Resolution);
                    bmp.Save(FileName, ImageFormat.Bmp);
                    bmp.Dispose();
                    // display image
                    //Item.ImagePictureBox.Load(FileName);
                    // .NET 4.0 fix
                    LoadPictureBoxImage(Item.ImagePictureBox, FileName);

                    // save quality
                    ret = MyGUI.GetIAFISQuality(Item.ScanObjID, DemoFormRef.DemoConfig.IAFIsQualityAlgorithm,
                        out Item.ItemData.Quality);
                    Item.ItemData.QualityAlgorithm = DemoFormRef.DemoConfig.IAFIsQualityAlgorithm;

                    int i, num;
                    int SegmLeft, SegmTop, SegmRight, SegmBottom, Quality, UnavailReason,
                        SegmSizeX, SegmSizeY;
                    bool SegmAvailable;
                    SegmentData[] SegmentsData;

                    // if slap, save also segments
                    // 1.14.0.0 - also for upper palm
                    // TODO - vedere se salvarli separatamente
                    if (GBMSGUI.IsSlap(Item.ScanObjID) || GBMSGUI.IsUpperPalm(Item.ScanObjID))
                    {
                        if ((Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_LEFT) ||
                            (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_UPPER_HALF_PALM_LEFT))
                        {
                            num = 4;
                            SegmentsData = LeftSlapSegmentsData;
                        }
                        else if ((Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_RIGHT) ||
                            (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_UPPER_HALF_PALM_RIGHT))
                        {
                            num = 4;
                            SegmentsData = RightSlapSegmentsData;
                        }
                        else //if (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_2_THUMBS)
                        {
                            num = 2;
                            SegmentsData = TwoThumbsSegmentsData;
                        }

                        for (i = 0; i < num; i++)
                        {
                            ret = MyGUI.GetSegmentationResult(i, DemoFormRef.DemoConfig.IAFIsQualityAlgorithm,
                                out SegmAvailable, out SegmLeft, out SegmTop, out SegmRight, out SegmBottom,
                                out UnavailReason, out Quality,
                                ImageBuffer, out SegmSizeX, out SegmSizeY, out Resolution);
                            if (ret == GBMSGUI.ReturnCodes.Ret_Failure)
                            {
                                MessageBox.Show(MyGUI.GetErrorMessage() + " (GetSegmentationResult)",
                                    Application.ProductName,
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            if (SegmAvailable)
                            {
                                // save segment image
                                bmp = GBMSGUI.RawImageToBitmap(ImageBuffer, SegmSizeX, SegmSizeY);
                                FileName = BuildSegmentFileName(Item, i);
                                bmp.SetResolution((float)Resolution, (float)Resolution);
                                bmp.Save(FileName, ImageFormat.Bmp);
                                bmp.Dispose();
                                // display image
                                PictureBox pbox = GetSegmentPictureBox(Item.ScanObjID, i);
                                //pbox.Load(FileName);
                                // .NET 4.0 fix
                                LoadPictureBoxImage(pbox, FileName);

                                // save segments data
                                SegmentsData[i].ItemData.Quality = Quality;
                                SegmentsData[i].ItemData.QualityAlgorithm = DemoFormRef.DemoConfig.IAFIsQualityAlgorithm;
                                SegmentsData[i].BoundingBox.X = SegmLeft;
                                SegmentsData[i].BoundingBox.Y = SegmTop;
                                SegmentsData[i].BoundingBox.Width = SegmRight - SegmLeft;
                                SegmentsData[i].BoundingBox.Height = SegmBottom - SegmTop;
                            }
                            else
                            {
                                // save unavailability reason
                                if (GBMSGUI.CheckMask(SessionOpt, GBMSGUI.SessionOption.AskUnavailabilityReason))
                                {
                                    SegmentsData[i].ItemData.UnavailabilityReason = UnavailReason;
                                    // 2.0.1.0
                                    DisableScanItemSameFinger(Item.ScanObjID, i, UnavailReason);
                                }
                            }
                        }
                    }
                    else if (GBMSGUI.IsJoint(Item.ScanObjID))
                    {
                        // get bounding boxes of phalanges
                        // in the next release will be saved in AN2K record
                        // 2.4.0.0
                        /*
                        if (GBMSGUI.IsThumbJoint(Item.ScanObjID))
                            num = 2;
                        else
                        */
                            num = 3;
                        for (i = 0; i < num; i++)
                        {
                            // 2.4.0.0 - for thumbs skip medial phalange (index 1)
                            if (GBMSGUI.IsThumbJoint(Item.ScanObjID) && (i == 1))
                                continue;

                            ret = MyGUI.GetSegmentationResult(i, 0,
                                out SegmAvailable, out SegmLeft, out SegmTop, out SegmRight, out SegmBottom,
                                out UnavailReason, out Quality,
                                null, out SegmSizeX, out SegmSizeY, out Resolution);
                            if (ret == GBMSGUI.ReturnCodes.Ret_Failure)
                            {
                                MessageBox.Show(MyGUI.GetErrorMessage() + " (GetSegmentationResult)",
                                    Application.ProductName,
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            // save data to write after in EBTS record
                            if (SegmAvailable)
                            {
                                JointsBBoxData[IndexOfJointsData(Item.ScanObjID)].PhalangeBBox[i].X = SegmLeft;
                                JointsBBoxData[IndexOfJointsData(Item.ScanObjID)].PhalangeBBox[i].Y = SegmTop;
                                JointsBBoxData[IndexOfJointsData(Item.ScanObjID)].PhalangeBBox[i].Width = SegmRight - SegmLeft;
                                JointsBBoxData[IndexOfJointsData(Item.ScanObjID)].PhalangeBBox[i].Height = SegmBottom - SegmTop;
                            }
                        }
                    }
                }
            }

            // stop session
            MyGUI.StopSession();

            // save current window size and position
            int WLeft, WTop, WWidth, WHeight;
            bool WMaximized;
            MyGUI.GetWindowSizeAndPosition(out WLeft, out WTop, out WWidth, out WHeight, out WMaximized);
            Rectangle wRect = new Rectangle(WLeft, WTop, WWidth, WHeight);
            DemoFormRef.DemoConfig.WindowSize = wRect;
            DemoFormRef.DemoConfig.WindowMaximized = WMaximized;

            // re-enable our controls
            btnAcquire.Enabled = true;
            mnuAcquireItem.Enabled = true;
        }

        private int SetImageSize(uint ScanObjID)
        {
            uint ObjectType = GBMSAPI_NET_ScanObjectsUtilities.GBMSAPI_NET_GetTypeFromObject(ScanObjID);
            int ret;
            double ImageWidth, ImageHeight; // size in inches
            uint PixelWidth, PixelHeight;   // size in pixels

            ImageWidth = ImageHeight = 0;

            // read from configuration the size for the current object
            if (GBMSGUI.IsUpperPalm(ScanObjID))
            {
                ImageWidth = DemoFormRef.DemoConfig.ImageSize.UpperPalmWidth;
                ImageHeight = DemoFormRef.DemoConfig.ImageSize.UpperPalmHeight;
            }
            else if (GBMSGUI.IsLowerPalm(ScanObjID))
            {
                ImageWidth = DemoFormRef.DemoConfig.ImageSize.LowerPalmWidth;
                ImageHeight = DemoFormRef.DemoConfig.ImageSize.LowerPalmHeight;
            }
            else if (GBMSGUI.IsWritersPalm(ScanObjID))
            {
                ImageWidth = DemoFormRef.DemoConfig.ImageSize.WritersPalmWidth;
                ImageHeight = DemoFormRef.DemoConfig.ImageSize.WritersPalmHeight;
            }
            else if ((ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_LEFT) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_RIGHT))
            {
                ImageWidth = DemoFormRef.DemoConfig.ImageSize.FourFingersWidth;
                ImageHeight = DemoFormRef.DemoConfig.ImageSize.FourFingersHeight;
            }
            else if (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_2_THUMBS)
            {
                ImageWidth = DemoFormRef.DemoConfig.ImageSize.TwoThumbsWidth;
                ImageHeight = DemoFormRef.DemoConfig.ImageSize.TwoThumbsHeight;
            }
            else if ((ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_LEFT_THUMB) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_RIGHT_THUMB))
            {
                ImageWidth = DemoFormRef.DemoConfig.ImageSize.FlatThumbWidth;
                ImageHeight = DemoFormRef.DemoConfig.ImageSize.FlatThumbHeight;
            }
            else if ((ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_LEFT_THUMB) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_RIGHT_THUMB))
            {
                ImageWidth = DemoFormRef.DemoConfig.ImageSize.RolledThumbWidth;
                ImageHeight = DemoFormRef.DemoConfig.ImageSize.RolledThumbHeight;
            }
            else if ((ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_LEFT_INDEX) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_RIGHT_INDEX))
            {
                ImageWidth = DemoFormRef.DemoConfig.ImageSize.RolledIndexWidth;
                ImageHeight = DemoFormRef.DemoConfig.ImageSize.RolledIndexHeight;
            }
            else if ((ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_LEFT_MIDDLE) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_RIGHT_MIDDLE))
            {
                ImageWidth = DemoFormRef.DemoConfig.ImageSize.RolledMiddleWidth;
                ImageHeight = DemoFormRef.DemoConfig.ImageSize.RolledMiddleHeight;
            }
            else if ((ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_LEFT_RING) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_RIGHT_RING))
            {
                ImageWidth = DemoFormRef.DemoConfig.ImageSize.RolledRingWidth;
                ImageHeight = DemoFormRef.DemoConfig.ImageSize.RolledRingHeight;
            }
            else if ((ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_LEFT_LITTLE) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_RIGHT_LITTLE))
            {
                ImageWidth = DemoFormRef.DemoConfig.ImageSize.RolledLittleWidth;
                ImageHeight = DemoFormRef.DemoConfig.ImageSize.RolledLittleHeight;
            }
            //V1.10 - start
            else if (GBMSGUI.IsRolledJoint(ScanObjID))
            {
                ImageWidth = DemoFormRef.DemoConfig.ImageSize.RolledJointWidth;
                ImageHeight = DemoFormRef.DemoConfig.ImageSize.RolledJointHeight;
            }
            else if (GBMSGUI.IsFlatJoint(ScanObjID))
            {
                ImageWidth = DemoFormRef.DemoConfig.ImageSize.FlatJointWidth;
                ImageHeight = DemoFormRef.DemoConfig.ImageSize.FlatJointHeight;
            }
            else if (GBMSGUI.IsRolledTip(ScanObjID))
            {
                ImageWidth = DemoFormRef.DemoConfig.ImageSize.RolledTipWidth;
                ImageHeight = DemoFormRef.DemoConfig.ImageSize.RolledTipHeight;
            }
            else if (GBMSGUI.IsRolledThenar(ScanObjID))
            {
                ImageWidth = DemoFormRef.DemoConfig.ImageSize.RolledThenarWidth;
                ImageHeight = DemoFormRef.DemoConfig.ImageSize.RolledThenarHeight;
            }
            //V1.10 - end
            // 1.15.0.0
            else if (GBMSGUI.IsRolledHypothenar(ScanObjID))
            {
                ImageWidth = DemoFormRef.DemoConfig.ImageSize.RolledHypothenarWidth;
                ImageHeight = DemoFormRef.DemoConfig.ImageSize.RolledHypothenarHeight;
            }
            // 2.4.0.0
            else if (GBMSAPI_NET_ScanObjectsUtilities.GBMSAPI_NET_GetTypeFromObject(ScanObjID) == GBMSAPI_NET_ScannableBiometricTypes.GBMSAPI_NET_SBT_ROLLED_UP)
            {
                ImageWidth = DemoFormRef.DemoConfig.ImageSize.RolledUpWidth;
                ImageHeight = DemoFormRef.DemoConfig.ImageSize.RolledUpHeight;
            }
            else if (GBMSGUI.IsFlatSingleFinger(ScanObjID))
            {
                ImageWidth = DemoFormRef.DemoConfig.ImageSize.FlatFingerWidth;
                ImageHeight = DemoFormRef.DemoConfig.ImageSize.FlatFingerHeight;
            }

            if ((ImageWidth != 0) && (ImageHeight != 0))
            {
                // convert to pixel
                if (DemoFormRef.Resolution1000Dpi)
                {
                    PixelWidth = (uint)(ImageWidth * 1000);
                    PixelHeight = (uint)(ImageHeight * 1000);
                }
                else
                {
                    PixelWidth = (uint)(ImageWidth * 500);
                    PixelHeight = (uint)(ImageHeight * 500);
                }

                // 2.0.0.0 - for DID20, set its own size (out of standard)
                /*
                if (DemoFormRef.CurrentDevice.DeviceID == GBMSAPI_NET_DeviceName.GBMSAPI_NET_DN_DSID20)
                {
                    PixelWidth = 360;
                    PixelHeight = 400;
                }
                */

                // 2.4.1.0
                // adapt to max size of current scanner
                if (PixelWidth > DemoFormRef.MaxSizeX)
                    PixelWidth = DemoFormRef.MaxSizeX;
                if (PixelHeight > DemoFormRef.MaxSizeY)
                    PixelHeight = DemoFormRef.MaxSizeY;

                ret = MyGUI.SetImageSize(ScanObjID, PixelWidth, PixelHeight);
                return ret;
            }

            return GBMSGUI.ReturnCodes.Ret_Success;
        }

        private int SetSegmentsImageSize(uint ScanObjID)
        {
            double SegmWidth, SegmHeight;     // sizes for segments
            uint PixelWidth, PixelHeight;
            int ret = GBMSGUI.ReturnCodes.Ret_Success;

            SegmWidth = SegmHeight = 0;

            if ((ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_LEFT) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_RIGHT) ||
                // 2.0.1.0 - was missing
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_UPPER_HALF_PALM_LEFT) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_UPPER_HALF_PALM_RIGHT))
            {
                // set sizes for segments using flat finger setting
                SegmWidth = DemoFormRef.DemoConfig.ImageSize.FlatFingerWidth;
                SegmHeight = DemoFormRef.DemoConfig.ImageSize.FlatFingerHeight;
            }
            else if (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_2_THUMBS)
            {
                // set sizes for segments using flat thumb setting
                SegmWidth = DemoFormRef.DemoConfig.ImageSize.FlatThumbWidth;
                SegmHeight = DemoFormRef.DemoConfig.ImageSize.FlatThumbHeight;
            }

            if ((SegmWidth != 0) && (SegmHeight != 0))
            {
                // convert to pixel
                if (DemoFormRef.Resolution1000Dpi)
                {
                    PixelWidth = (uint)(SegmWidth * 1000);
                    PixelHeight = (uint)(SegmHeight * 1000);
                }
                else
                {
                    PixelWidth = (uint)(SegmWidth * 500);
                    PixelHeight = (uint)(SegmHeight * 500);
                }

                int num;
                if (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_2_THUMBS)
                    num = 2;
                else
                    num = 4;
                for (int i = 0; i < num; i++)
                {
                    ret = MyGUI.SetSegmentImageSize(ScanObjID, i, PixelWidth, PixelHeight);
                    //if (ret != GBMSGUI.ReturnCodes.Ret_Success)
                    //    break;
                }

                return ret;
            }

            return GBMSGUI.ReturnCodes.Ret_Success;
        }

        // get the correct picturebox where display segment image (in the place of the flat fingerprints)
        private PictureBox GetSegmentPictureBox(uint ScanObjID, int Index)
        {
            // 1.14.0.0 - also upper palm
            if ((ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_LEFT) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_UPPER_HALF_PALM_LEFT))
            {
                if (Index == 0)
                    return pboxLeftFlatIndex;
                if (Index == 1)
                    return pboxLeftFlatMiddle;
                if (Index == 2)
                    return pboxLeftFlatRing;
                if (Index == 3)
                    return pboxLeftFlatLittle;
            }
            // 1.14.0.0 - also upper palm
            else if ((ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_RIGHT) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_UPPER_HALF_PALM_RIGHT))
            {
                if (Index == 0)
                    return pboxRightFlatIndex;
                if (Index == 1)
                    return pboxRightFlatMiddle;
                if (Index == 2)
                    return pboxRightFlatRing;
                if (Index == 3)
                    return pboxRightFlatLittle;
            }
            else if (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_2_THUMBS)
            {
                if (Index == 0)
                    return pboxLeftFlatThumb;
                if (Index == 1)
                    return pboxRightFlatThumb;
            }
            //V1.1
            else if (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_2_LEFT)
            {
                if (Index == 0)
                    return pboxLeftFlatIndex;
                if (Index == 1)
                    return pboxLeftFlatMiddle;
            }
            else if (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_2_RIGHT)
            {
                if (Index == 0)
                    return pboxRightFlatIndex;
                if (Index == 1)
                    return pboxRightFlatMiddle;
            }
            //V1.2
            else if (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_2_INDEXES)
            {
                if (Index == 0)
                    return pboxLeftFlatIndex;
                if (Index == 1)
                    return pboxRightFlatIndex;
            }

            return null;
        }

        private void TouchScreenTimer_Tick(object sender, EventArgs e)
        {
            Byte PressedButton;

            TouchScreenTimer.Enabled = false;

            // check LCD Start button
            GBMSAPI_NET_ExternalDevicesControlRoutines.GBMSAPI_NET_VUI_LCD_GetPressedButton(out PressedButton);
            if (PressedButton == GBMSAPI_NET_PressedButtonIDs.GBMSAPI_NET_VILCD_TOUCHSCREEN_START_BUTTON)
            {
                btnAcquire_Click(this, null);
                return;
            }

            TouchScreenTimer.Enabled = true;
        }

        private void EmptyFolder(String FolderName)
        {
            String[] Files = Directory.GetFiles(FolderName);

            foreach (String FileName in Files)
            {
                if (File.Exists(FileName))
                {
                    try
                    {
                        File.Delete(FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void UserDataForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.Cancel)
            {
                if (!ViewMode)
                {
                    if (Directory.Exists(ImagesPath))
                        // delete directory and contents
                        Directory.Delete(ImagesPath, true);
                }
            }

            TouchScreenTimer.Enabled = false;

            if (!ViewMode)
            {
                // save configuration of types for sequence
                if (chkSlaps.Checked)
                    DemoFormRef.DemoConfig.SequenceTypes |= DemoForm.SequenceType.Slaps;
                else
                    DemoFormRef.DemoConfig.SequenceTypes &= ~DemoForm.SequenceType.Slaps;
                if (chkFlat.Checked)
                    DemoFormRef.DemoConfig.SequenceTypes |= DemoForm.SequenceType.SingleFlat;
                else 
                    DemoFormRef.DemoConfig.SequenceTypes &= ~DemoForm.SequenceType.SingleFlat;
                if (chkRolled.Checked)
                    DemoFormRef.DemoConfig.SequenceTypes |= DemoForm.SequenceType.Rolled;
                else
                    DemoFormRef.DemoConfig.SequenceTypes &= ~DemoForm.SequenceType.Rolled;
                // 1.15.1.0
                /*
                if (chkPalms.Checked)
                    DemoFormRef.DemoConfig.SequenceTypes |= DemoForm.SequenceType.Palms;
                else
                    DemoFormRef.DemoConfig.SequenceTypes &= ~DemoForm.SequenceType.Palms;
                */
                if (chkUpperPalms.Checked)
                    DemoFormRef.DemoConfig.SequenceTypes |= DemoForm.SequenceType.UpperPalms;
                else
                    DemoFormRef.DemoConfig.SequenceTypes &= ~DemoForm.SequenceType.UpperPalms;
                if (chkLowerPalms.Checked)
                    DemoFormRef.DemoConfig.SequenceTypes |= DemoForm.SequenceType.LowerPalms;
                else
                    DemoFormRef.DemoConfig.SequenceTypes &= ~DemoForm.SequenceType.LowerPalms;
                if (chkWritersPalms.Checked)
                    DemoFormRef.DemoConfig.SequenceTypes |= DemoForm.SequenceType.WritersPalms;
                else
                    DemoFormRef.DemoConfig.SequenceTypes &= ~DemoForm.SequenceType.WritersPalms;

                //V1.10
                if (chkRolledJoints.Checked)
                    DemoFormRef.DemoConfig.SequenceTypes |= DemoForm.SequenceType.RolledJoints;
                else
                    DemoFormRef.DemoConfig.SequenceTypes &= ~DemoForm.SequenceType.RolledJoints;
                if (chkFlatJointSides.Checked)
                    DemoFormRef.DemoConfig.SequenceTypes |= DemoForm.SequenceType.FlatJointSides;
                else
                    DemoFormRef.DemoConfig.SequenceTypes &= ~DemoForm.SequenceType.FlatJointSides;
                if (chkRolledTips.Checked)
                    DemoFormRef.DemoConfig.SequenceTypes |= DemoForm.SequenceType.RolledTips;
                else
                    DemoFormRef.DemoConfig.SequenceTypes &= ~DemoForm.SequenceType.RolledTips;
                if (chkRolledThenars.Checked)
                    DemoFormRef.DemoConfig.SequenceTypes |= DemoForm.SequenceType.RolledThenars;
                else
                    DemoFormRef.DemoConfig.SequenceTypes &= ~DemoForm.SequenceType.RolledThenars;
                // 1.15.1.0
                if (chkRolledHypothenars.Checked)
                    DemoFormRef.DemoConfig.SequenceTypes |= DemoForm.SequenceType.RolledHypothenars;
                else
                    DemoFormRef.DemoConfig.SequenceTypes &= ~DemoForm.SequenceType.RolledHypothenars;

                // save configuration
                try
                {
                    DemoForm.Configuration.Serialize(Path.ChangeExtension(Application.ExecutablePath, ".cfg"), DemoFormRef.DemoConfig);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                if (DemoFormRef.LCDPresent)
                    // return to Logo Screen
                    GBMSAPI_NET_ExternalDevicesControlRoutines.GBMSAPI_NET_VUI_LCD_SetLogoScreen();
            }

            // free images
            foreach (ScanItem Item in lstScannedObjects.Items)
                if (Item.ImagePictureBox.Image != null)
                    Item.ImagePictureBox.Image.Dispose();
            lstScannedObjects.Dispose();

            //if (gcUserDataFormHandle.IsAllocated)
            //    gcUserDataFormHandle.Free();

            //if (UserDataForm.gcUserDataFormHandle_St.IsAllocated)
            //    UserDataForm.gcUserDataFormHandle_St.Free();
        }

        private void popAcquireItemMenu_Popup(object sender, EventArgs e)
        {
            // search current item
            foreach (ScanItem Item in lstScannedObjects.Items)
            {
                if (Item.ImagePictureBox == popAcquireItemMenu.SourceControl)
                {
                    if (FingerprintCard)
                        mnuAcquireItem.Visible = false;
                    else
                    {
                        if (Item.Supported)
                            mnuAcquireItem.Visible = true;
                        else
                            mnuAcquireItem.Visible = false;
                    }

                    if (Item.ImagePictureBox.Image != null)
                    {
                        mnuViewItem.Visible = true;
                        // 2.4.0.0
                        mnuSaveAs.Visible = true;
                    }
                    else
                    {
                        mnuViewItem.Visible = false;
                        // 2.4.0.0
                        mnuSaveAs.Visible = false;
                    }
                    break;
                }
                else
                {
                    // item not in the list
                    //mnuAcquireItem.Visible = false;
                    //mnuViewItem.Visible = false;
                }
            }
        }

        private void SelectAllScanItems()
        {
            // select all items
            for (int i = 0; i < lstScannedObjects.Items.Count; i++)
            {
                ScanItem Item = (ScanItem)lstScannedObjects.Items[i];

                if (GBMSGUI.IsSlap(Item.ScanObjID) && chkSlaps.Checked && Item.Supported)
                    lstScannedObjects.SetItemChecked(i, true);
                //else if (GBMSGUI.IsRolled(Item.ScanObjID) && chkRolled.Checked && Item.Supported)
                // IsRolled is not sufficient, it includes also joints and tips
                else if ((GBMSAPI_NET_ScanObjectsUtilities.GBMSAPI_NET_GetTypeFromObject(Item.ScanObjID) == GBMSAPI_NET_ScannableBiometricTypes.GBMSAPI_NET_SBT_ROLL_SINGLE_FINGER)
                    && chkRolled.Checked && Item.Supported)
                    lstScannedObjects.SetItemChecked(i, true);
                else if (GBMSGUI.IsFlatSingleFinger(Item.ScanObjID) && chkFlat.Checked && Item.Supported)
                    lstScannedObjects.SetItemChecked(i, true);
                // 1.15.1.0
                //else if (GBMSGUI.IsPalm(Item.ScanObjID) && chkPalms.Checked && Item.Supported)
                //    lstScannedObjects.SetItemChecked(i, true);
                else if (GBMSGUI.IsUpperPalm(Item.ScanObjID) && chkUpperPalms.Checked && Item.Supported)
                    lstScannedObjects.SetItemChecked(i, true);
                else if (GBMSGUI.IsLowerPalm(Item.ScanObjID) && chkLowerPalms.Checked && Item.Supported)
                    lstScannedObjects.SetItemChecked(i, true);
                else if (GBMSGUI.IsWritersPalm(Item.ScanObjID) && chkWritersPalms.Checked && Item.Supported)
                    lstScannedObjects.SetItemChecked(i, true);
                //V1.10
                else if (GBMSGUI.IsRolledJoint(Item.ScanObjID) && chkRolledJoints.Checked && Item.Supported)
                    lstScannedObjects.SetItemChecked(i, true);
                else if (GBMSGUI.IsFlatJoint(Item.ScanObjID) && chkFlatJointSides.Checked && Item.Supported)
                    lstScannedObjects.SetItemChecked(i, true);
                else if (GBMSGUI.IsRolledTip(Item.ScanObjID) && chkRolledTips.Checked && Item.Supported)
                    lstScannedObjects.SetItemChecked(i, true);
                else if (GBMSGUI.IsRolledThenar(Item.ScanObjID) && chkRolledThenars.Checked && Item.Supported)
                    lstScannedObjects.SetItemChecked(i, true);
                // 1.15.1.0
                else if (GBMSGUI.IsRolledHypothenar(Item.ScanObjID) && chkRolledHypothenars.Checked && Item.Supported)
                    lstScannedObjects.SetItemChecked(i, true);
                else
                    lstScannedObjects.SetItemChecked(i, false);

                // check if finger is marked as missing
                if (GBMSGUI.IsSingleFinger(Item.ScanObjID))
                {
                    if (((Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_LEFT_LITTLE) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_LEFT_LITTLE) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_LEFT_LITTLE) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_LEFT_LITTLE) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_TIP_LEFT_LITTLE) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_LEFT_LITTLE) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_LEFT_LITTLE) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_UP_LEFT_LITTLE) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_DOWN_LEFT_LITTLE))
                        && chkMissingLeftLittle.Checked)
                        lstScannedObjects.SetItemChecked(i, false);
                    if (((Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_LEFT_RING) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_LEFT_RING) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_LEFT_RING) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_LEFT_RING) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_TIP_LEFT_RING) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_LEFT_RING) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_LEFT_RING) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_UP_LEFT_RING) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_DOWN_LEFT_RING))
                        && chkMissingLeftRing.Checked)
                        lstScannedObjects.SetItemChecked(i, false);
                    if (((Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_LEFT_MIDDLE) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_LEFT_MIDDLE) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_LEFT_MIDDLE) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_LEFT_MIDDLE) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_TIP_LEFT_MIDDLE) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_LEFT_MIDDLE) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_LEFT_MIDDLE) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_UP_LEFT_MIDDLE) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_DOWN_LEFT_MIDDLE))
                        && chkMissingLeftMiddle.Checked)
                        lstScannedObjects.SetItemChecked(i, false);
                    if (((Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_LEFT_INDEX) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_LEFT_INDEX) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_LEFT_INDEX) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_LEFT_INDEX) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_TIP_LEFT_INDEX) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_LEFT_INDEX) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_LEFT_INDEX) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_UP_LEFT_INDEX) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_DOWN_LEFT_INDEX))
                        && chkMissingLeftIndex.Checked)
                        lstScannedObjects.SetItemChecked(i, false);
                    if (((Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_LEFT_THUMB) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_LEFT_THUMB) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_LEFT_THUMB) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_LEFT_THUMB) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_TIP_LEFT_THUMB) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_LEFT_THUMB) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_LEFT_THUMB) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_UP_LEFT_THUMB) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_DOWN_LEFT_THUMB))
                        && chkMissingLeftThumb.Checked)
                        lstScannedObjects.SetItemChecked(i, false);
                    if (((Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_RIGHT_THUMB) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_RIGHT_THUMB) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_RIGHT_THUMB) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_RIGHT_THUMB) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_TIP_RIGHT_THUMB) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_RIGHT_THUMB) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_RIGHT_THUMB) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_UP_RIGHT_THUMB) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_DOWN_RIGHT_THUMB))
                        && chkMissingRightThumb.Checked)
                        lstScannedObjects.SetItemChecked(i, false);
                    if (((Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_RIGHT_INDEX) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_RIGHT_INDEX) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_RIGHT_INDEX) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_RIGHT_INDEX) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_TIP_RIGHT_INDEX) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_RIGHT_INDEX) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_RIGHT_INDEX) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_UP_RIGHT_INDEX) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_DOWN_RIGHT_INDEX))
                        && chkMissingRightIndex.Checked)
                        lstScannedObjects.SetItemChecked(i, false);
                    if (((Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_RIGHT_MIDDLE) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_RIGHT_MIDDLE) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_RIGHT_MIDDLE) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_RIGHT_MIDDLE) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_TIP_RIGHT_MIDDLE) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_RIGHT_MIDDLE) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_RIGHT_MIDDLE) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_UP_RIGHT_MIDDLE) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_DOWN_RIGHT_MIDDLE))
                        && chkMissingRightMiddle.Checked)
                        lstScannedObjects.SetItemChecked(i, false);
                    if (((Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_RIGHT_RING) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_RIGHT_RING) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_RIGHT_RING) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_RIGHT_RING) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_TIP_RIGHT_RING) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_RIGHT_RING) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_RIGHT_RING) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_UP_RIGHT_RING) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_DOWN_RIGHT_RING))
                        && chkMissingRightRing.Checked)
                        lstScannedObjects.SetItemChecked(i, false);
                    if (((Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_RIGHT_LITTLE) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_RIGHT_LITTLE) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_RIGHT_LITTLE) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_RIGHT_LITTLE) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_TIP_RIGHT_LITTLE) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_RIGHT_LITTLE) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_RIGHT_LITTLE) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_UP_RIGHT_LITTLE) ||
                        (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_DOWN_RIGHT_LITTLE))
                        && chkMissingRightLittle.Checked)
                        lstScannedObjects.SetItemChecked(i, false);
                }
            }
        }

        private void UnselectAllScanItems()
        {
            // select all items
            for (int i = 0; i < lstScannedObjects.Items.Count; i++)
                lstScannedObjects.SetItemChecked(i, false);
        }

        private String BuildImageFileName(ScanItem Item)
        {
            String FileName = ImagesPath + Path.DirectorySeparatorChar + Item.Text + ".bmp";
            return FileName;
        }

        private String BuildSegmentFileName(ScanItem Item, int Index)
        {
            String FileName = ImagesPath + Path.DirectorySeparatorChar + Item.Text + "_" + Index.ToString() + ".bmp";
            return FileName;
        }

        // acquire a single item, called from context menu
        private void mnuAcquireItem_Click(object sender, EventArgs e)
        {
            UnselectAllScanItems();

            // search current item
            foreach (ScanItem Item in lstScannedObjects.Items)
            {
                if (Item.ImagePictureBox == popAcquireItemMenu.SourceControl)
                {
                    lstScannedObjects.SetItemChecked(lstScannedObjects.Items.IndexOf(Item), true);
                    // delete file
                    String FileName = BuildImageFileName(Item);
                    if (File.Exists(FileName))
                        File.Delete(FileName);
                    // clear picturebox
                    if (Item.ImagePictureBox.Image != null)
                        Item.ImagePictureBox.Image.Dispose();
                    Item.ImagePictureBox.Image = null;
                    // reset item data
                    Item.ItemData.Quality = 0;
                    Item.ItemData.QualityAlgorithm = 0;
                    Item.ItemData.UnavailabilityReason = 0;

                    // if slap, delete and clear also segments
                    // 1.14.0.0 - also for upper palm
                    if (GBMSGUI.IsSlap(Item.ScanObjID) || GBMSGUI.IsUpperPalm(Item.ScanObjID))
                    {
                        // TODO - i segmenti dell'upper palm si sovrascrivono???
                        // li devo gestire separatamente???
                        int i, num = 0;
                        if ((Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_LEFT) ||
                            (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_UPPER_HALF_PALM_LEFT))
                        {
                            num = 4;
                            ResetSlapSegmentsData(LeftSlapSegmentsData, num);
                        }
                        else if ((Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_RIGHT) ||
                            (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_UPPER_HALF_PALM_RIGHT))
                        {
                            num = 4;
                            ResetSlapSegmentsData(RightSlapSegmentsData, num);
                        }
                        else if (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_2_THUMBS)
                        {
                            num = 2;
                            ResetSlapSegmentsData(TwoThumbsSegmentsData, num);
                        }

                        for (i = 0; i < num; i++)
                        {
                            FileName = BuildSegmentFileName(Item, i);
                            if (File.Exists(FileName))
                            {
                                File.Delete(FileName);
                                // clear image
                                PictureBox pbox = GetSegmentPictureBox(Item.ScanObjID, i);
                                pbox.Image = null;
                            }
                        }
                    }

                    if (GBMSGUI.IsJoint(Item.ScanObjID))
                        ResetJointsData(Item.ScanObjID);

                    // launch acquisition with a single item selected
                    Acquire();

                    //V1.10
                    // for now not supported
                    //EnableViewEJI();

                    // after acquisition, re-enable timer
                    GBMSAPI_NET_ExternalDevicesControlRoutines.GBMSAPI_NET_VUI_LCD_EnableStartButtonOnLogoScreen();
                    TouchScreenTimer.Enabled = true;
                    break;
                }
            }

            // select all again
            SelectAllScanItems();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!ViewMode)
            {
                if (txtSurname.Text == "")
                {
                    MessageBox.Show("Specify at least Surname!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                UserData.Surname = txtSurname.Text;
                UserData.Name = txtName.Text;

                // save user data
                DemoForm.UserData.Serialize(ImagesPath + Path.DirectorySeparatorChar + "UserData.xml", UserData);

                // save other data
                SaveItemsData();
                SaveSegmentsData();
                SaveJointsData();

                // rename folder with User name
                String NewFolderName = txtSurname.Text;
                if (txtName.Text.Length != 0)
                    NewFolderName = NewFolderName + " " + txtName.Text;

                // remove invalid chars (if any)
                NewFolderName = MakeValidFileName(NewFolderName);

                NewFolderName = Path.GetDirectoryName(Application.ExecutablePath) + Path.DirectorySeparatorChar + NewFolderName;

                // check duplicate
                if (Directory.Exists(NewFolderName))
                {
                    MessageBox.Show("Duplicated name for folder! Specify a different Surname/Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    // rename folder
                    Directory.Move(ImagesPath, NewFolderName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            DialogResult = DialogResult.OK;
        }

        private static string MakeValidFileName(string name)
        {
            string invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
            string invalidReStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);
            return Regex.Replace(name, invalidReStr, "_");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            // Cancel code is in UserDataForm_FormClosing
        }

        private void mnuViewItem_Click(object sender, EventArgs e)
        {
            // search current item
            foreach (ScanItem Item in lstScannedObjects.Items)
            {
                if (Item.ImagePictureBox == popAcquireItemMenu.SourceControl)
                {
                    ViewImageForm ViewForm = new ViewImageForm();
                    ViewForm.Text = Item.Text;
                    ViewForm.bmpImage = Item.ImagePictureBox.Image;
                    ViewForm.ShowDialog();
                }
            }
        }

        private void LoadSavedImages()
        {
            foreach (ScanItem Item in lstScannedObjects.Items)
            {
                String FileName = BuildImageFileName(Item);
                if (File.Exists(FileName))
                {
                    if (Item.ImagePictureBox.Image != null)
                        Item.ImagePictureBox.Image.Dispose();
                    //Item.ImagePictureBox.Load(FileName);
                    // .NET 4.0 fix
                    LoadPictureBoxImage(Item.ImagePictureBox, FileName);
                    Application.DoEvents();
                }

                // load segments
                // 1.14.0.0 - also for upper palm
                // TODO - verificare bene
                if (GBMSGUI.IsSlap(Item.ScanObjID) || GBMSGUI.IsUpperPalm(Item.ScanObjID))
                {
                    int i, num;
                    if (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_2_THUMBS)
                        num = 2;
                    else
                        num = 4;
                    for (i = 0; i < num; i++)
                    {
                        FileName = BuildSegmentFileName(Item, i);
                        if (File.Exists(FileName))
                        {
                            // display image
                            PictureBox pbox = GetSegmentPictureBox(Item.ScanObjID, i);
                            //pbox.Load(FileName);
                            // .NET 4.0 fix
                            LoadPictureBoxImage(pbox, FileName);
                        }
                    }
                }
            }
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            ConfigurationForm ConfigForm = new ConfigurationForm(DemoFormRef);

            ConfigForm.ShowDialog();
        }

        public bool AfterStartCallback(IntPtr UserDefinedParameter)
        {
            return (bool)Invoke(AfterStartDelegate);
        }

        public bool OnAfterStart()
        {
            // for example
            /*
            if (GBMSGUI.IsRolled(CurrentScanItem.ScanObjID))
                GBMSAPI_NET_ScannerStartedRoutines.GBMSAPI_NET_ROLL_SetPreviewTimeout(1500);
            */

            return true;
        }

        // save acquired items data in a binary file
        private bool SaveItemsData()
        {
            int Count = 0;
            FileStream fs;
            BinaryWriter w;

            try
            {
                fs = new FileStream(ImagesPath + Path.DirectorySeparatorChar + "ItemsData.dat", FileMode.Create);
                w = new BinaryWriter(fs);

                // count items to be written
                foreach (ScanItem Item in lstScannedObjects.Items)
                    if ((Item.ItemData.Quality != 0) || (Item.ItemData.UnavailabilityReason != 0))
                        Count++;

                // write number of items
                w.Write(Count);

                foreach (ScanItem Item in lstScannedObjects.Items)
                {
                    if ((Item.ItemData.Quality != 0) || (Item.ItemData.UnavailabilityReason != 0))
                    {
                        // save this item
                        w.Write(Item.ScanObjID);
                        w.Write(Item.ItemData.Quality);
                        w.Write(Item.ItemData.QualityAlgorithm);
                        w.Write(Item.ItemData.UnavailabilityReason);
                    }
                }

                w.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        // read saved items data from a binary file
        private bool ReadItemsData()
        {
            FileStream fs;
            BinaryReader r;
            int Count = 0;
            uint ScanObjID;
            int Quality;
            int QualityAlgorithm;
            int UnavailabilityReason;
            ScanItem Item;

            try
            {
                fs = new FileStream(ImagesPath + Path.DirectorySeparatorChar + "ItemsData.dat", FileMode.Open);
                r = new BinaryReader(fs);

                // read number of items
                Count = r.ReadInt32();

                // read items
                for (int i = 0; i < Count; i++)
                {
                    ScanObjID = (uint)r.ReadInt32();
                    Quality = r.ReadInt32();
                    QualityAlgorithm = r.ReadInt32();
                    UnavailabilityReason = r.ReadInt32();

                    // assign values to ScanItem
                    Item = FindScanItem(ScanObjID);
                    if (Item != null)
                    {
                        Item.ItemData.Quality = Quality;
                        Item.ItemData.QualityAlgorithm = QualityAlgorithm;
                        Item.ItemData.UnavailabilityReason = UnavailabilityReason;
                    }
                }

                r.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        // save acquired segments data in a binary file
        private bool SaveSegmentsData()
        {
            int i;
            FileStream fs;
            BinaryWriter w;

            try
            {
                fs = new FileStream(ImagesPath + Path.DirectorySeparatorChar + "SegmentsData.dat", FileMode.Create);
                w = new BinaryWriter(fs);

                // left slap data
                for (i = 0; i < 4; i++)
                {
                    w.Write(LeftSlapSegmentsData[i].ItemData.Quality);
                    w.Write(LeftSlapSegmentsData[i].ItemData.QualityAlgorithm);
                    w.Write(LeftSlapSegmentsData[i].ItemData.UnavailabilityReason);
                    w.Write(LeftSlapSegmentsData[i].BoundingBox.X);
                    w.Write(LeftSlapSegmentsData[i].BoundingBox.Y);
                    w.Write(LeftSlapSegmentsData[i].BoundingBox.Width);
                    w.Write(LeftSlapSegmentsData[i].BoundingBox.Height);
                }

                // right slap data
                for (i = 0; i < 4; i++)
                {
                    w.Write(RightSlapSegmentsData[i].ItemData.Quality);
                    w.Write(RightSlapSegmentsData[i].ItemData.QualityAlgorithm);
                    w.Write(RightSlapSegmentsData[i].ItemData.UnavailabilityReason);
                    w.Write(RightSlapSegmentsData[i].BoundingBox.X);
                    w.Write(RightSlapSegmentsData[i].BoundingBox.Y);
                    w.Write(RightSlapSegmentsData[i].BoundingBox.Width);
                    w.Write(RightSlapSegmentsData[i].BoundingBox.Height);
                }

                // two thumbs data
                for (i = 0; i < 2; i++)
                {
                    w.Write(TwoThumbsSegmentsData[i].ItemData.Quality);
                    w.Write(TwoThumbsSegmentsData[i].ItemData.QualityAlgorithm);
                    w.Write(TwoThumbsSegmentsData[i].ItemData.UnavailabilityReason);
                    w.Write(TwoThumbsSegmentsData[i].BoundingBox.X);
                    w.Write(TwoThumbsSegmentsData[i].BoundingBox.Y);
                    w.Write(TwoThumbsSegmentsData[i].BoundingBox.Width);
                    w.Write(TwoThumbsSegmentsData[i].BoundingBox.Height);
                }

                w.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        // read saved segments data from a binary file
        private bool ReadSegmentsData()
        {
            int i;
            FileStream fs;
            BinaryReader r;

            try
            {
                fs = new FileStream(ImagesPath + Path.DirectorySeparatorChar + "SegmentsData.dat", FileMode.Open);
                r = new BinaryReader(fs);

                // left slap data
                for (i = 0; i < 4; i++)
                {
                    LeftSlapSegmentsData[i].ItemData.Quality = r.ReadInt32();
                    LeftSlapSegmentsData[i].ItemData.QualityAlgorithm = r.ReadInt32();
                    LeftSlapSegmentsData[i].ItemData.UnavailabilityReason = r.ReadInt32();
                    LeftSlapSegmentsData[i].BoundingBox.X = r.ReadInt32();
                    LeftSlapSegmentsData[i].BoundingBox.Y = r.ReadInt32();
                    LeftSlapSegmentsData[i].BoundingBox.Width = r.ReadInt32();
                    LeftSlapSegmentsData[i].BoundingBox.Height = r.ReadInt32();
                }

                // right slap data
                for (i = 0; i < 4; i++)
                {
                    RightSlapSegmentsData[i].ItemData.Quality = r.ReadInt32();
                    RightSlapSegmentsData[i].ItemData.QualityAlgorithm = r.ReadInt32();
                    RightSlapSegmentsData[i].ItemData.UnavailabilityReason = r.ReadInt32();
                    RightSlapSegmentsData[i].BoundingBox.X = r.ReadInt32();
                    RightSlapSegmentsData[i].BoundingBox.Y = r.ReadInt32();
                    RightSlapSegmentsData[i].BoundingBox.Width = r.ReadInt32();
                    RightSlapSegmentsData[i].BoundingBox.Height = r.ReadInt32();
                }

                // two thumbs data
                for (i = 0; i < 2; i++)
                {
                    TwoThumbsSegmentsData[i].ItemData.Quality = r.ReadInt32();
                    TwoThumbsSegmentsData[i].ItemData.QualityAlgorithm = r.ReadInt32();
                    TwoThumbsSegmentsData[i].ItemData.UnavailabilityReason = r.ReadInt32();
                    TwoThumbsSegmentsData[i].BoundingBox.X = r.ReadInt32();
                    TwoThumbsSegmentsData[i].BoundingBox.Y = r.ReadInt32();
                    TwoThumbsSegmentsData[i].BoundingBox.Width = r.ReadInt32();
                    TwoThumbsSegmentsData[i].BoundingBox.Height = r.ReadInt32();
                }

                r.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        // search an item in the list from the ID
        private ScanItem FindScanItem(uint ScanObjID)
        {
            foreach (ScanItem Item in lstScannedObjects.Items)
                if (Item.ScanObjID == ScanObjID)
                    return Item;

            return null;
        }

        // search an item in the list from the PictureBox
        private ScanItem FindScanItem(PictureBox pboxImage)
        {
            foreach (ScanItem Item in lstScannedObjects.Items)
                if (Item.ImagePictureBox == pboxImage)
                    return Item;

            return null;
        }

///*
        // exports saved data into ANSI/NIST format 2007
        private bool ExportEFTS(String FileName, bool UseTemplate, String TemplateName)
        {
            int ret;
            float Resolution;  // pixel per mm
            bool ImageAcquired;
            NW_AN2K_Type14_RECORD RecType14;
            NW_AN2K_Type15_RECORD RecType15;    // for Palms
            int ImageSizeX, ImageSizeY;
            Byte[] CompressedImage;
            int CompressedImageLength;
            String CompressionAlgorithm;
            String FingerPos;
            List<NW_AN2K_FING_AMP> AmpList = new List<NW_AN2K_FING_AMP>();         // amputation list
            List<NW_AN2K_FING_QUALITY> NFIQList;    // NIST quality list
            List<NW_AN2K_ALT_QUAL> AltQualList;     // alternate quality list
            NW_AN2K_ANSI_NIST ansi_nist;
            Byte[] ANSI_NIST_Array;
            NW_AN2K_Type2_RECORD RecType2;
            bool SlapAcquired = false;

            // show log window
            LogForm frmLog = new LogForm("Export to EFTS");
            frmLog.Show();
            this.Enabled = false;
            frmLog.LogMessage("Starting export to EFTS...");
            
            // convert resolution in pixel per mm
            Resolution = (float)UserData.AcquisitionDpi / (float)25.4;

            if (UseTemplate)
            {
                // read ansi/nist buffer from file
                ANSI_NIST_Array = File.ReadAllBytes(TemplateName);
                ansi_nist = new NW_AN2K_ANSI_NIST(ANSI_NIST_Array);

                // update record-1 with image resolution
                ansi_nist.NW_UpdateETFStype_1(Resolution, Resolution);
            }
            else
            {
                // Create ANSI_NIST structure
                ansi_nist = new NW_AN2K_ANSI_NIST(
                    NW_AN2K_VERSIONS.NW_VERSION_0400,
                    "Demo",
                    -1,
                    "Destination",
                    "Green Bit",
                    "000001",
                    null,
                    Resolution,
                    Resolution,
                    null);
                if (ansi_nist.Buffer == null)
                {
                    frmLog.LogMessage("Error creating ANSI/NIST structure");
                    frmLog.EnableClose();
                    this.Enabled = true;
                    return false;
                }
            }

            // create a Type-2 record for eventually unavailable fingers
            RecType2 = new NW_AN2K_Type2_RECORD();

            // cycle on items
            foreach (ScanItem Item in lstScannedObjects.Items)
            {
                // skip flat single fingers, if a slap is acquired
                // only information about bounding boxes will be inserted into record for slaps
                if (SlapAcquired)
                    if (GBMSGUI.IsFlatSingleFinger(Item.ScanObjID))
                        continue;

                //V1.2 - no slaps 2
                if ((Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_2_LEFT) ||
                    (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_2_RIGHT) ||
                    (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_2_INDEXES))
                    continue;

                // if image is present
                if (Item.ImagePictureBox.Image != null)
                {
                    ImageAcquired = true;

                    //// get raw image from bitmap
                    ImageSizeX = Item.ImagePictureBox.Image.Width;
                    ImageSizeY = Item.ImagePictureBox.Image.Height;

                    frmLog.LogMessage(Item.Text + ": compressing image...");

                    // compress image: JPEG2000 for 1000 dpi, WSQ for 500 dpi
                    // 07/10/2009 - image compression specified in configuration
                    int ImageCompression;
                    double WSQRate;
                    int JPEGRate;
                    if (UserData.AcquisitionDpi == 1000)
                    {
                        ImageCompression = DemoFormRef.DemoConfig.ImageCompression1000;
                        WSQRate = DemoFormRef.DemoConfig.WQSBitRate1000;
                        JPEGRate = DemoFormRef.DemoConfig.JPEGRate1000;
                    }
                    else
                    {
                        ImageCompression = DemoFormRef.DemoConfig.ImageCompression500;
                        WSQRate = DemoFormRef.DemoConfig.WQSBitRate500;
                        JPEGRate = DemoFormRef.DemoConfig.JPEGRate500;
                    }
                    if (ImageCompression == DemoForm.ImageCompressions.JPEG2000)
                    {
                        CompressedImage = CompressToJP2((Bitmap)(Item.ImagePictureBox.Image), JPEGRate);
                        CompressedImageLength = CompressedImage.Length;
                        CompressionAlgorithm = NW_AN2K_COMP_ALGORITHMS.NW_JPG2K_COMP;
                    }
                    else
                    {
                        CompressedImage = CompressToWSQ((Bitmap)(Item.ImagePictureBox.Image), WSQRate, UserData.AcquisitionDpi);
                        CompressedImageLength = CompressedImage.Length;
                        CompressionAlgorithm = NW_AN2K_COMP_ALGORITHMS.NW_COMP_WSQ;
                    }
                }
                else
                {
                    ImageAcquired = false;
                    CompressedImage = null;
                    CompressedImageLength = 0;
                    ImageSizeX = 0;
                    ImageSizeY = 0;
                    CompressionAlgorithm = null;
                }

                // if not acquired and no UnavailabilityReason specified, skip
                if (!ImageAcquired && (Item.ItemData.UnavailabilityReason == 0))
                    continue;

                if (GBMSGUI.IsSlap(Item.ScanObjID))
                {
                    SlapAcquired = true;

                    // Slaps
                    RecType14 = new NW_AN2K_Type14_RECORD();
                    SegmentData[] SegmentsData;
                    int i, num;
                    // Bounding box list
                    List<NW_AN2K_FING_SEGMENT> BoundingBoxList = new List<NW_AN2K_FING_SEGMENT>();
                    List<NW_AN2K_FING_AMP> SegmAmpList = new List<NW_AN2K_FING_AMP>();
                    NFIQList = new List<NW_AN2K_FING_QUALITY>();
                    AltQualList = new List<NW_AN2K_ALT_QUAL>();

                    if (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_LEFT)
                    {
                        num = 4;
                        SegmentsData = LeftSlapSegmentsData;
                    }
                    else if (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_RIGHT)
                    {
                        num = 4;
                        SegmentsData = RightSlapSegmentsData;
                    }
                    else // if (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_2_THUMBS)
                    {
                        num = 2;
                        SegmentsData = TwoThumbsSegmentsData;
                    }

                    for (i = 0; i < num; i++)
                    {
                        FingerPos = SegmentIndexToAN2KFingerPos(Item.ScanObjID, i);

                        if (SegmentsData[i].ItemData.Quality != 0)
                        {
                            // add bounding box item
                            BoundingBoxList.Add(new NW_AN2K_FING_SEGMENT(FingerPos,
                                (short)SegmentsData[i].BoundingBox.Left,
                                (short)SegmentsData[i].BoundingBox.Right,
                                (short)SegmentsData[i].BoundingBox.Top,
                                (short)SegmentsData[i].BoundingBox.Bottom));

                            // add quality
                            if (SegmentsData[i].ItemData.QualityAlgorithm == GBMSGUI.QualityAlgorithms.NFIQAlgorithm)
                                NFIQList.Add(new NW_AN2K_FING_QUALITY(FingerPos, (byte)SegmentsData[i].ItemData.Quality));
                            else
                                AltQualList.Add(new NW_AN2K_ALT_QUAL(FingerPos, (byte)SegmentsData[i].ItemData.Quality,
                                    0x0040, // Green Bit Vendor ID
                                    0x0001)); // Green Bit quality algorithm ID
                        }
                        else
                        {
                            if (SegmentsData[i].ItemData.UnavailabilityReason != 0)
                                SegmAmpList.Add(new NW_AN2K_FING_AMP(FingerPos, GetAmpCode(SegmentsData[i].ItemData.UnavailabilityReason)));
                        }
                    }

                    frmLog.LogMessage("Adding record...");

                    // create record
                    ret = RecType14.NW_image2ETFStype_14(CompressedImage, CompressedImageLength,
                        ImageSizeX, ImageSizeY, 8, Resolution, CompressionAlgorithm,
                        NW_AN2K_IMPRESSION_CODES.NW_ETFS_14_LIVE_SCAN_PLAN,
                        "GBMSDemo", "",
                        GBMSGUI.ScannedObjectIDToAN2KFingerID(Item.ScanObjID),
                        SegmAmpList,
                        BoundingBoxList,
                        NFIQList,
                        null,
                        AltQualList);
                    if (ret != 0)
                    {
                        frmLog.LogMessage("Error creating record type-14");
                    }

                    // add record to ANSI/NIST structure
                    ret = ansi_nist.NW_insert_ANSI_NIST_record_ETFS(RecType14, ansi_nist.num_records);
                    if (ret != 0)
                    {
                        frmLog.LogMessage("Error adding record type-14");
                    }
                }
                else if (GBMSGUI.IsPalm(Item.ScanObjID))
                {
                    FingerPos = GBMSGUI.ScannedObjectIDToAN2KFingerID(Item.ScanObjID);
                    if (ImageAcquired)
                    {
                        // Palms
                        RecType15 = new NW_AN2K_Type15_RECORD();

                        // add quality
                        List <NW_AN2K_PALM_QUAL> PalmQualList = new List <NW_AN2K_PALM_QUAL>();
                        if (Item.ItemData.QualityAlgorithm == GBMSGUI.QualityAlgorithms.GBAlgorithm)
                            PalmQualList.Add(new NW_AN2K_PALM_QUAL(FingerPos, 
                                    (byte)Item.ItemData.Quality,
                                    0x0040, // Green Bit Vendor ID
                                    0x0001)); // Green Bit quality algorithm ID

                        frmLog.LogMessage("Adding record...");

                        // create record
                        ret = RecType15.NW_image2ETFStype_15(CompressedImage, CompressedImageLength,
                            ImageSizeX, ImageSizeY, 8, Resolution, CompressionAlgorithm,
                            NW_AN2K_IMPRESSION_CODES.NW_ETFS_14_LIVE_SCAN_PLAN,
                            "GBMSDemo", "",
                            FingerPos,
                            PalmQualList);
                        if (ret != 0)
                        {
                            frmLog.LogMessage("Error creating record type-15");
                        }
    
                        // add record to ANSI/NIST structure
                        ret = ansi_nist.NW_insert_ANSI_NIST_record_ETFS(RecType15, ansi_nist.num_records);
                        if (ret != 0)
                        {
                            frmLog.LogMessage("Error adding record type-15");
                        }
                    }
                    else
                    {
                        if (Item.ItemData.UnavailabilityReason != 0)
                        {
                            // add to amputation list in type-2 record
                            AmpList.Add(new NW_AN2K_FING_AMP(FingerPos, GetAmpCode(Item.ItemData.UnavailabilityReason)));
                        }
                    }
                }
                //else if (GBMSGUI.IsRolled(Item.ScanObjID))
                else if (GBMSGUI.IsRolled(Item.ScanObjID) || GBMSGUI.IsFlatSingleFinger(Item.ScanObjID))
                {
                    FingerPos = GBMSGUI.ScannedObjectIDToAN2KFingerID(Item.ScanObjID);
                    int ImpressionCode;
                    // Rolled
                    if (ImageAcquired)
                    {
                        RecType14 = new NW_AN2K_Type14_RECORD();

                        NFIQList = new List<NW_AN2K_FING_QUALITY>();
                        AltQualList = new List<NW_AN2K_ALT_QUAL>();
                        // add quality
                        if (Item.ItemData.QualityAlgorithm == GBMSGUI.QualityAlgorithms.NFIQAlgorithm)
                            NFIQList.Add(new NW_AN2K_FING_QUALITY(FingerPos, (byte)Item.ItemData.Quality));
                        else
                            AltQualList.Add(new NW_AN2K_ALT_QUAL(FingerPos, 
                                    (byte)Item.ItemData.Quality,
                                    0x0040, // Green Bit Vendor ID
                                    0x0001)); // Green Bit quality algorithm ID

                        frmLog.LogMessage("Adding record...");

                        if (GBMSGUI.IsRolled(Item.ScanObjID))
                            ImpressionCode = NW_AN2K_IMPRESSION_CODES.NW_ETFS_14_LIVE_SCAN_ROLLED;
                        else
                            ImpressionCode = NW_AN2K_IMPRESSION_CODES.NW_ETFS_14_LIVE_SCAN_PLAN;

                        // create record
                        ret = RecType14.NW_image2ETFStype_14(CompressedImage, CompressedImageLength,
                            ImageSizeX, ImageSizeY, 8, Resolution, CompressionAlgorithm,
                            ImpressionCode,
                            "GBMSDemo", "",
                            FingerPos,
                            null,
                            null,
                            NFIQList,
                            null,
                            AltQualList);
                        if (ret != 0)
                        {
                            frmLog.LogMessage("Error creating record type-14");
                        }

                        // add record to ANSI/NIST structure
                        ret = ansi_nist.NW_insert_ANSI_NIST_record_ETFS(RecType14, ansi_nist.num_records);
                        if (ret != 0)
                        {
                            frmLog.LogMessage("Error adding record type-14");
                        }
                    }
                    else
                    {
                        if (Item.ItemData.UnavailabilityReason != 0)
                        {
                            // add to amputation list in type-2 record
                            AmpList.Add(new NW_AN2K_FING_AMP(FingerPos, GetAmpCode(Item.ItemData.UnavailabilityReason)));
                        }
                    }
                }
            }

            if (!UseTemplate)
            {
                frmLog.LogMessage("Adding Type-2 record...");

                // add Type-2 record
                ret = RecType2.NW_CreateETFStype_2("Green Bit", UserData.DeviceName, UserData.DeviceSerialNumber, AmpList);
                if (ret != 0)
                {
                    frmLog.LogMessage("Error creating record type-2");
                }
                // add Type-2 record to ANSI/NIST structure
                ret = ansi_nist.NW_insert_ANSI_NIST_record_ETFS(RecType2, ansi_nist.num_records);
                if (ret != 0)
                {
                    frmLog.LogMessage("Error adding record type-2");
                }
            }

            frmLog.LogMessage("Saving ANSI/NIST structure to file...");

            // save ansi_nist record
            ANSI_NIST_Array = ansi_nist.Buffer;
            //File.WriteAllBytes(ImagesPath + Path.DirectorySeparatorChar + "EFTS_Record.bin", ANSI_NIST_Array);
            File.WriteAllBytes(FileName, ANSI_NIST_Array);

            //frmLog.LogMessage("ANSI/NIST structure exported to EFTS_Record.bin");
            frmLog.LogMessage("ANSI/NIST structure exported to " + FileName);
            frmLog.EnableClose();
            this.Enabled = true;

            return true;
        }
//*/ 

///*
        // exports saved data into ANSI/NIST format - 2011 version
        private bool ExportEFTS_AN2K2011(String FileName, bool UseTemplate, String TemplateName)
        {
            int ret;
            float Resolution;  // pixel per mm
            bool ImageAcquired;
            An2k2011_Type14Record RecType14;
            An2k2011_Type15Record RecType15;    // for Palms
            int ImageSizeX, ImageSizeY;
            Byte[] CompressedImage;
            int CompressedImageLength;
            int CompressionAlgorithm;
            int FingerPos;
            List<An2k2011_AmputatedBandagedCode> AmpList = new List<An2k2011_AmputatedBandagedCode>();         // amputation list
            List<An2k2011_NistQualityMetric> NFIQList;    // NIST quality list
            List<An2k2011_FingerOrSegmentQualityMetric> AltQualList;     // alternate quality list
            An2k2011_Type1Record ansi_nist;
            Byte[] ANSI_NIST_Array;
            //NW_AN2K_Type2_RECORD RecType2;
            bool SlapAcquired = false;
            int idc = 0;
            int positionInAnsiNist = 1;

            // show log window
            LogForm frmLog = new LogForm("Export to EFTS");
            frmLog.Show();
            this.Enabled = false;
            frmLog.LogMessage("Starting export to EFTS...");

            // convert resolution in pixel per mm
            //Resolution = (float)UserData.AcquisitionDpi / (float)25.4;
            // set to 0 if no type-4 record are present
            Resolution = (float)0.0;

            An2k2011_Date DateOfTransaction = new An2k2011_Date();
            DateOfTransaction.Year = DateTime.Now.Year;
            DateOfTransaction.Month = DateTime.Now.Month;
            DateOfTransaction.Day = DateTime.Now.Day;

            An2k2011_Gmt GMTDate = new An2k2011_Gmt();
            GMTDate.Year = DateTime.UtcNow.Year;
            GMTDate.Month = DateTime.UtcNow.Month;
            GMTDate.Day = DateTime.UtcNow.Day;
            GMTDate.hour = DateTime.UtcNow.Hour;
            GMTDate.minute = DateTime.UtcNow.Minute;
            GMTDate.second = DateTime.UtcNow.Second;

            if (UseTemplate)
            {
                // read ansi/nist buffer from file
                ANSI_NIST_Array = File.ReadAllBytes(TemplateName);
                ansi_nist = new An2k2011_Type1Record();
                ret = ansi_nist.Create(ANSI_NIST_Array);
                if (ret != NW_AN2K_ERRORS.NW_AN2K_DLL_NO_ERROR)
                {
                    frmLog.LogMessage("Error creating ANSI/NIST structure:\n" + GetAN2KError(ret));
                    frmLog.EnableClose();
                    this.Enabled = true;
                    return false;
                }

                // TODO - adesso non e' supportato
                // update record-1 with image resolution
                //ansi_nist.NW_UpdateETFStype_1(Resolution, Resolution);
            }
            else
            {
                // Create ANSI_NIST structure
                ansi_nist = new An2k2011_Type1Record();
                ret = ansi_nist.Create(
				    "Demo", DateOfTransaction, -1, "DestAg", "Green Bit", 
                    "TCN1234", "TCR1234",
                    Resolution, Resolution,
                    "DomName", "DV1.0.0.0", 
                    GMTDate, An2k2011_Table4.CHAR_ENCODING_INDEX_ASCII, 
                    An2k2011_Table4.CHAR_ENCODING_NAME_ASCII, "1.0",
                    null,
                    "DAN", "OAN");
                if (ret != NW_AN2K_ERRORS.NW_AN2K_DLL_NO_ERROR)
                {
                    frmLog.LogMessage("Error creating ANSI/NIST structure:\n" + GetAN2KError(ret));
                    frmLog.EnableClose();
                    this.Enabled = true;
                    return false;
                }
            }

            if ((txtSurname.Text != "") || (txtName.Text != ""))
            {
                // create a Type-2 record 
                DemoRecord2 RecType2 = new DemoRecord2();
                ret = RecType2.Create(idc++, txtName.Text, txtSurname.Text);
                if (ret != NW_AN2K_ERRORS.NW_AN2K_DLL_NO_ERROR)
                {
                    frmLog.LogMessage("Error creating record type-2:\n" + GetAN2KError(ret));
                    frmLog.EnableClose();
                    this.Enabled = true;
                    return false;
                }

                frmLog.LogMessage("Adding record Type-2...");

                ret = ansi_nist.InsertRecord(RecType2, positionInAnsiNist++, false);
                if (ret != NW_AN2K_ERRORS.NW_AN2K_DLL_NO_ERROR)
                {
                    frmLog.LogMessage("Error inserting record type-2:\n" + GetAN2KError(ret));
                    frmLog.EnableClose();
                    this.Enabled = true;
                    return false;
                }
            }

            // cycle on items
            foreach (ScanItem Item in lstScannedObjects.Items)
            {
                // skip flat single fingers, if a slap is acquired
                // only information about bounding boxes will be inserted into record for slaps
                if (SlapAcquired)
                {
                    if (GBMSGUI.IsFlatSingleFinger(Item.ScanObjID))
                    {
                        // to add separaded thumbs insteas of two thumbs
                        //if ((Item.ScanObjID != GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_LEFT_THUMB) &&
                        //    (Item.ScanObjID != GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_RIGHT_THUMB))
                            continue;
                    }
                }

                // skip thwo thumbs object
                //if (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_2_THUMBS)
                //    continue;

                //V1.2 - no slaps 2
                if ((Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_2_LEFT) ||
                    (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_2_RIGHT) ||
                    (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_2_INDEXES))
                    continue;

                // if image is present
                if (Item.ImagePictureBox.Image != null)
                {
                    ImageAcquired = true;

                    //// get raw image from bitmap
                    ImageSizeX = Item.ImagePictureBox.Image.Width;
                    ImageSizeY = Item.ImagePictureBox.Image.Height;

                    frmLog.LogMessage(Item.Text + ": compressing image...");

                    // compress image: JPEG2000 for 1000 dpi, WSQ for 500 dpi
                    // 07/10/2009 - image compression specified in configuration
                    int ImageCompression;
                    double WSQRate;
                    int JPEGRate;
                    if (UserData.AcquisitionDpi == 1000)
                    {
                        ImageCompression = DemoFormRef.DemoConfig.ImageCompression1000;
                        WSQRate = DemoFormRef.DemoConfig.WQSBitRate1000;
                        JPEGRate = DemoFormRef.DemoConfig.JPEGRate1000;
                    }
                    else
                    {
                        ImageCompression = DemoFormRef.DemoConfig.ImageCompression500;
                        WSQRate = DemoFormRef.DemoConfig.WQSBitRate500;
                        JPEGRate = DemoFormRef.DemoConfig.JPEGRate500;
                    }
                    if (ImageCompression == DemoForm.ImageCompressions.JPEG2000)
                    {
                        CompressedImage = CompressToJP2((Bitmap)(Item.ImagePictureBox.Image), JPEGRate);
                        CompressedImageLength = CompressedImage.Length;
                        CompressionAlgorithm = An2k2011_Table15.CODE_JP2;
                    }
                    else
                    {
                        CompressedImage = CompressToWSQ((Bitmap)(Item.ImagePictureBox.Image), WSQRate, UserData.AcquisitionDpi);
                        CompressedImageLength = CompressedImage.Length;
                        CompressionAlgorithm = An2k2011_Table15.CODE_WSQ20;
                    }
                }
                else
                {
                    ImageAcquired = false;
                    CompressedImage = null;
                    CompressedImageLength = 0;
                    ImageSizeX = 0;
                    ImageSizeY = 0;
                    CompressionAlgorithm = 0;
                }

                // if not acquired and no UnavailabilityReason specified, skip
                if (!ImageAcquired && (Item.ItemData.UnavailabilityReason == 0))
                    continue;

                if (GBMSGUI.IsSlap(Item.ScanObjID))
                {
                    SlapAcquired = true;

                    // Slaps
                    RecType14 = new An2k2011_Type14Record();
                    SegmentData[] SegmentsData;
                    int i, num;
                    // Bounding box list
                    List<An2k2011_FingerSegmentPosition> BoundingBoxList = new List<An2k2011_FingerSegmentPosition>();
                    List<An2k2011_AmputatedBandagedCode> SegmAmpList = new List<An2k2011_AmputatedBandagedCode>();
                    NFIQList = new List<An2k2011_NistQualityMetric>();
                    AltQualList = new List<An2k2011_FingerOrSegmentQualityMetric>();

                    if (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_LEFT)
                    {
                        num = 4;
                        SegmentsData = LeftSlapSegmentsData;
                    }
                    else if (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_RIGHT)
                    {
                        num = 4;
                        SegmentsData = RightSlapSegmentsData;
                    }
                    else // if (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_2_THUMBS)
                    {
                        num = 2;
                        SegmentsData = TwoThumbsSegmentsData;
                    }

                    for (i = 0; i < num; i++)
                    {
                        FingerPos = SegmentIndexToAN2K2011FingerPos(Item.ScanObjID, i);

                        if (SegmentsData[i].ItemData.Quality != 0)
                        {
                            // add bounding box item
                            BoundingBoxList.Add(new An2k2011_FingerSegmentPosition(FingerPos,
                                (short)SegmentsData[i].BoundingBox.Left,
                                (short)SegmentsData[i].BoundingBox.Right,
                                (short)SegmentsData[i].BoundingBox.Top,
                                (short)SegmentsData[i].BoundingBox.Bottom));

                            // add quality
                            if (SegmentsData[i].ItemData.QualityAlgorithm == GBMSGUI.QualityAlgorithms.NFIQAlgorithm)
                                NFIQList.Add(new An2k2011_NistQualityMetric(FingerPos, (byte)SegmentsData[i].ItemData.Quality));
                            else
                                AltQualList.Add(new An2k2011_FingerOrSegmentQualityMetric(FingerPos, (byte)SegmentsData[i].ItemData.Quality,
                                    0x0040, // Green Bit Vendor ID
                                    0x0001)); // Green Bit quality algorithm ID
                        }
                        else
                        {
                            if (SegmentsData[i].ItemData.UnavailabilityReason != 0)
                                SegmAmpList.Add(new An2k2011_AmputatedBandagedCode(FingerPos, GetAmpCode(SegmentsData[i].ItemData.UnavailabilityReason)));
                        }
                    }

                    frmLog.LogMessage("Adding record...");

                    FingerPos = ScannedObjectIDToAN2K2011FingerPos(Item.ScanObjID);
					// per passare le liste come array, usare list.ToArray();
                    // create record
                    ret = RecType14.Create(idc++, An2k2011_Table7.FRIT_LS_OPT_CONTACT_PLAIN, 
                        "Green Bit", DateOfTransaction, 
                        ImageSizeX, ImageSizeY, 
                        1, // pixels per inch
                        UserData.AcquisitionDpi, UserData.AcquisitionDpi,
				        CompressionAlgorithm, 8, 
                        FingerPos,
                        //PrintPosDesc,
                        null,
                        null, 
                        -1,     // ScannedHorizontalPixelScale
				        -1,     //ScannedVerticalPixelScale
                        SegmAmpList.ToArray(),
                        null,   //Comment_14_1, 
                        BoundingBoxList.ToArray(), 
                        NFIQList.ToArray(), 
                        AltQualList.ToArray(), 
                        null,   // Fqm
                        null,   // Aseg,
				        -1,     // SimultaneousCapture
                        false,  // StitchedImageFlag
                        null,   // DeviceMonitoringMode
                        -1,   // FingerprintAcquisitionProfile
				        CompressedImage);
                    if (ret != 0)
                    {
                        frmLog.LogMessage("Error creating record type-14:\n" + GetAN2KError(ret));
                        frmLog.EnableClose();
                        this.Enabled = true;
                        return false;
                    }

                    // add record to ANSI/NIST structure
                    ret = ansi_nist.InsertRecord(RecType14, positionInAnsiNist++, false);
                    if (ret != 0)
                    {
                        frmLog.LogMessage("Error adding record type-14:\n" + GetAN2KError(ret));
                        frmLog.EnableClose();
                        this.Enabled = true;
                        return false;
                    }
                }
                else if (GBMSGUI.IsPalm(Item.ScanObjID) || GBMSGUI.IsRolledThenar(Item.ScanObjID)
                    // 1.15.0.0
                    || GBMSGUI.IsRolledHypothenar(Item.ScanObjID))
                {
                    FingerPos = ScannedObjectIDToAN2K2011FingerPos(Item.ScanObjID);
                    
                    if (Item.ItemData.UnavailabilityReason != 0)
                    {
                        // TODO - add record with amp code without image
                        //AmpList.Add(new NW_AN2K_FING_AMP(FingerPos, GetAmpCode(Item.ItemData.UnavailabilityReason)));
                    }

                    // Palms
                    RecType15 = new An2k2011_Type15Record();

                    // add quality
                    List<An2k2011_FingerOrSegmentQualityMetric> PalmQualList = new List<An2k2011_FingerOrSegmentQualityMetric>();
                    if (Item.ItemData.QualityAlgorithm == GBMSGUI.QualityAlgorithms.GBAlgorithm)
                        PalmQualList.Add(new An2k2011_FingerOrSegmentQualityMetric(FingerPos,
                                (byte)Item.ItemData.Quality,
                                0x0040, // Green Bit Vendor ID
                                0x0001)); // Green Bit quality algorithm ID

                    frmLog.LogMessage("Adding record...");

                    // create record
                    ret = RecType15.Create(idc++,
                        An2k2011_Table7.FRIT_LS_UNKNOWN_PALM, 
                        "Green Bit",    // SourceAgency
                        DateOfTransaction,
                        ImageSizeX, ImageSizeY,
                        1, // pixels per inch
                        UserData.AcquisitionDpi, UserData.AcquisitionDpi,
                        CompressionAlgorithm, 8,
                        FingerPos, 
                        -1,     // ScannedHorizontalPixelScale
                        -1,     // ScannedVerticalPixelScale
                        null,   // Amp
                        null,   // Comment
                        PalmQualList.ToArray(),
                        null,   // DeviceMonitoringMode
                        CompressedImage);
                    if (ret != 0)
                    {
                        frmLog.LogMessage("Error creating record type-15:\n" + GetAN2KError(ret));
                        frmLog.EnableClose();
                        this.Enabled = true;
                        return false;
                    }

                    // add record to ANSI/NIST structure
                    ret = ansi_nist.InsertRecord(RecType15, positionInAnsiNist++, false);
                    if (ret != 0)
                    {
                        frmLog.LogMessage("Error adding record type-15:\n" + GetAN2KError(ret));
                        frmLog.EnableClose();
                        this.Enabled = true;
                        return false;
                    }
                }
                //else if (GBMSGUI.IsRolled(Item.ScanObjID))
                //else if (GBMSGUI.IsRolled(Item.ScanObjID) || GBMSGUI.IsFlatSingleFinger(Item.ScanObjID))
                else
                {
                    FingerPos = ScannedObjectIDToAN2K2011FingerPos(Item.ScanObjID);
                    An2k2011_PrintPositionDescriptors PrintPosDesc = new An2k2011_PrintPositionDescriptors();
                    if (FingerPos == An2k2011_Table8.EJI_OR_TIP)
                    {
                        PrintPosDesc.DecimalFingerPosition = GetAN2K2011DecimalPosition(Item.ScanObjID);
                        PrintPosDesc.FingerImageCode = GetAN2K2011ImageCode(Item.ScanObjID);
                    }
                    else
                    {
                        PrintPosDesc.DecimalFingerPosition = An2k2011_Table8.UNKNOWN;
                        PrintPosDesc.FingerImageCode = "";
                    }

                    int ImpressionCode;

                    if (Item.ItemData.UnavailabilityReason != 0)
                    {
                        // TODO - add record with amp code without image
                        //AmpList.Add(new NW_AN2K_FING_AMP(FingerPos, GetAmpCode(Item.ItemData.UnavailabilityReason)));
                    }

                    RecType14 = new An2k2011_Type14Record();

                    NFIQList = new List<An2k2011_NistQualityMetric>();
                    AltQualList = new List<An2k2011_FingerOrSegmentQualityMetric>();
                    // add quality
                    int Pos;    // if EJI_OR_TIP, need to insert the decimal pos...
                    if (FingerPos == An2k2011_Table8.EJI_OR_TIP)
                        Pos = PrintPosDesc.DecimalFingerPosition;
                    else
                        //Pos = FingerPos;
                        Pos = GetAN2K2011DecimalPosition(Item.ScanObjID);
                    if (Item.ItemData.QualityAlgorithm == GBMSGUI.QualityAlgorithms.NFIQAlgorithm)
                        NFIQList.Add(new An2k2011_NistQualityMetric(Pos, (byte)Item.ItemData.Quality));
                    else
                        AltQualList.Add(new An2k2011_FingerOrSegmentQualityMetric(Pos,
                                (byte)Item.ItemData.Quality,
                                0x0040, // Green Bit Vendor ID
                                0x0001)); // Green Bit quality algorithm ID

                    // add bounding boxes for joint phalanges
                    List<An2k2011_PrintPositionCoordinates> PrintPosCoord = new List<An2k2011_PrintPositionCoordinates>();
                    An2k2011_PrintPositionCoordinates[] ppc = null;
                    if (GBMSGUI.IsJoint(Item.ScanObjID))
                    {
                        int idx = IndexOfJointsData(Item.ScanObjID);
                        int i, num;
                        if (GBMSGUI.IsThumbJoint(Item.ScanObjID))
                            num = 2;
                        else
                            num = 3;
                        for (i=0; i< num; i++)
                        {
                            if (JointsBBoxData[idx].PhalangeBBox[i].Width != 0)
                            {
                                string SegmLocation = "";

                                if (i == 0)
                                    SegmLocation = An2k2011_Table9.LOS_DISTAL;
                                else if ((i == 1) && !GBMSGUI.IsThumbJoint(Item.ScanObjID))
                                    SegmLocation = An2k2011_Table9.LOS_MEDIAL;
                                else
                                    SegmLocation = An2k2011_Table9.LOS_PROXIMAL;
                                // add bounding box item
                                PrintPosCoord.Add(new An2k2011_PrintPositionCoordinates(
                                    PrintPosDesc.FingerImageCode,
                                    SegmLocation,
                                    (short)JointsBBoxData[idx].PhalangeBBox[i].Left,
                                    (short)JointsBBoxData[idx].PhalangeBBox[i].Right,
                                    (short)JointsBBoxData[idx].PhalangeBBox[i].Top,
                                    (short)JointsBBoxData[idx].PhalangeBBox[i].Bottom));
                            }
                        }

                        if (PrintPosCoord.Count != 0)
                            ppc = PrintPosCoord.ToArray();
                    }

                    frmLog.LogMessage("Adding record...");

                    if (GBMSGUI.IsRolled(Item.ScanObjID))
                        ImpressionCode = An2k2011_Table7.FRIT_LS_OPT_CONTACT_ROLLED;
                    else
                        ImpressionCode = An2k2011_Table7.FRIT_LS_OPT_CONTACT_PLAIN;

                    An2k2011_FingerOrSegmentQualityMetric[] fqm = null;
                    An2k2011_FingerOrSegmentQualityMetric[] sqm = null;
                    // set the quality list to to appropriate parameter
                    if (GBMSGUI.IsJoint(Item.ScanObjID))
                        sqm = AltQualList.ToArray();
                    else
                        fqm = AltQualList.ToArray();

                    // create record
                    ret = RecType14.Create(idc++, ImpressionCode,
                        "Green Bit", DateOfTransaction,
                        ImageSizeX, ImageSizeY,
                        1, // pixels per inch
                        UserData.AcquisitionDpi, UserData.AcquisitionDpi,
                        CompressionAlgorithm, 8,
                        FingerPos,
                        PrintPosDesc,
                        ppc,
                        -1,     // ScannedHorizontalPixelScale
                        -1,     //ScannedVerticalPixelScale
                        null,
                        null,   //Comment_14_1, 
                        null,
                        NFIQList.ToArray(),
                        sqm,
                        fqm,
                        null,   // Aseg,
                        -1,     // SimultaneousCapture
                        false,  // StitchedImageFlag
                        null,   // DeviceMonitoringMode
                        -1,   // FingerprintAcquisitionProfile
                        CompressedImage);
                    if (ret != 0)
                    {
                        frmLog.LogMessage("Error creating record type-14:\n" + GetAN2KError(ret));
                        frmLog.EnableClose();
                        this.Enabled = true;
                        return false;
                    }

                    // add record to ANSI/NIST structure
                    ret = ansi_nist.InsertRecord(RecType14, positionInAnsiNist++, false);
                    if (ret != 0)
                    {
                        frmLog.LogMessage("Error adding record type-14:\n" + GetAN2KError(ret));
                        frmLog.EnableClose();
                        this.Enabled = true;
                        return false;
                    }
                }
            }

            frmLog.LogMessage("Saving ANSI/NIST structure to file...");

            // save ansi_nist record
            ANSI_NIST_Array = ansi_nist.WriteToBuffer();
            //File.WriteAllBytes(ImagesPath + Path.DirectorySeparatorChar + "EFTS_Record.bin", ANSI_NIST_Array);
            try
            {
                File.WriteAllBytes(FileName, ANSI_NIST_Array);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                frmLog.LogMessage("Error saving file");
                frmLog.EnableClose();
                this.Enabled = true;
                return false;
            }

            //frmLog.LogMessage("ANSI/NIST structure exported to EFTS_Record.bin");
            frmLog.LogMessage("ANSI/NIST structure exported to " + FileName);
            frmLog.EnableClose();
            this.Enabled = true;

            return true;
        }
//*/

        // converts slap ScanObjID and segment index into AN2K finger position
        private String SegmentIndexToAN2KFingerPos(uint ScanObjID, int Index)
        {
            switch (Index)
            {
                case 0:
                    if (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_2_THUMBS)
                        return NW_AN2K_FINGER_POSITIONS.NW_ETFS_14_FGP_LEFT_THUMB;
                    else if (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_LEFT)
                        return NW_AN2K_FINGER_POSITIONS.NW_ETFS_14_FGP_LEFT_INDEX;
                    else if (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_RIGHT)
                        return NW_AN2K_FINGER_POSITIONS.NW_ETFS_14_FGP_RIGHT_INDEX;
                    break;

                case 1:
                    if (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_2_THUMBS)
                        return NW_AN2K_FINGER_POSITIONS.NW_ETFS_14_FGP_RIGHT_THUMB;
                    else if (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_LEFT)
                        return NW_AN2K_FINGER_POSITIONS.NW_ETFS_14_FGP_LEFT_MIDDLE;
                    else if (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_RIGHT)
                        return NW_AN2K_FINGER_POSITIONS.NW_ETFS_14_FGP_RIGHT_MIDDLE;
                    break;

                case 2:
                    if (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_LEFT)
                        return NW_AN2K_FINGER_POSITIONS.NW_ETFS_14_FGP_LEFT_RING;
                    else if (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_RIGHT)
                        return NW_AN2K_FINGER_POSITIONS.NW_ETFS_14_FGP_RIGHT_RING;
                    break;

                case 3:
                    if (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_LEFT)
                        return NW_AN2K_FINGER_POSITIONS.NW_ETFS_14_FGP_LEFT_LITTLE;
                    else if (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_RIGHT)
                        return NW_AN2K_FINGER_POSITIONS.NW_ETFS_14_FGP_RIGHT_LITTLE;
                    break;
            }

            return NW_AN2K_FINGER_POSITIONS.NW_ETFS_14_FGP_UNKNOWN;
        }

        // converts slap ScanObjID and segment index into AN2K 2011 finger position
        private int SegmentIndexToAN2K2011FingerPos(uint ScanObjID, int Index)
        {
            switch (Index)
            {
                case 0:
                    if (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_2_THUMBS)
                        return An2k2011_Table8.LEFT_THUMB;
                    else if (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_LEFT)
                        return An2k2011_Table8.LEFT_INDEX;
                    else if (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_RIGHT)
                        return An2k2011_Table8.RIGHT_INDEX;
                    break;

                case 1:
                    if (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_2_THUMBS)
                        return An2k2011_Table8.RIGHT_THUMB;
                    else if (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_LEFT)
                        return An2k2011_Table8.LEFT_MIDDLE;
                    else if (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_RIGHT)
                        return An2k2011_Table8.RIGHT_MIDDLE;
                    break;

                case 2:
                    if (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_LEFT)
                        return An2k2011_Table8.LEFT_RING;
                    else if (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_RIGHT)
                        return An2k2011_Table8.RIGHT_RING;
                    break;

                case 3:
                    if (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_LEFT)
                        return An2k2011_Table8.LEFT_LITTLE;
                    else if (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_RIGHT)
                        return An2k2011_Table8.RIGHT_LITTLE;
                    break;
            }

            return An2k2011_Table8.UNKNOWN;
        }

        private String GetAmpCode(int Value)
        {
            if (Value == GBMSGUI.UnavailabilityReason.Amputated)
                return AN2K_NET_WRAPPER.NW_AN2K_AMPUTATION_CODES.NW_AMPUTATION;

            if (Value == GBMSGUI.UnavailabilityReason.Unprintable)
                return AN2K_NET_WRAPPER.NW_AN2K_AMPUTATION_CODES.NW_UNABLE_TO_PRINT;

            return "";
        }

        private void btnExportEFTS_Click(object sender, EventArgs e)
        {
            bool UseTemplate = false;
            String FileName;
            String TemplateName = "";

            // 1.15.0.0
            if (cmbAn2kFormat.SelectedIndex == -1)
            {
                MessageBox.Show("Please specify the format!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            openFileDialog1.Title = "Select ANSI/NIST Template file";
            openFileDialog1.Filter = "ANSI/NIST-ITL 1-200x files|*.nist;*.nst;*.eft;*.an2;*.an2k;*.bin";
            openFileDialog1.FileName = "";

            saveFileDialog1.Title = "Save ANSI/NIST file";
            saveFileDialog1.Filter = openFileDialog1.Filter;
            saveFileDialog1.DefaultExt = "nist";
            saveFileDialog1.FileName = "NISTFile";
            saveFileDialog1.OverwritePrompt = true;

            if (optEFTSTemplate.Checked)
            {
                // ask the user location and name of the template file
                openFileDialog1.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                TemplateName = openFileDialog1.FileName;
                UseTemplate = true;
                saveFileDialog1.InitialDirectory = Path.GetDirectoryName(openFileDialog1.FileName);
            }
            else
                saveFileDialog1.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);

            // ask the user location and name of the file to create
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            FileName = saveFileDialog1.FileName;

            Cursor = Cursors.WaitCursor;
            if (cmbAn2kFormat.SelectedIndex == 0)
                // An2k 2007
                ExportEFTS(FileName, UseTemplate, TemplateName);
            else 
                // An2k 2011
                ExportEFTS_AN2K2011(FileName, UseTemplate, TemplateName);
            Cursor = Cursors.Default;
        }

        private void ResetScanItemsData()
        {
            foreach (ScanItem Item in lstScannedObjects.Items)
            {
                Item.ItemData.Quality = 0;
                Item.ItemData.QualityAlgorithm = 0;
                Item.ItemData.UnavailabilityReason = 0;

                if ((Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_LEFT) ||
                    // 2.0.1.0
                    (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_UPPER_HALF_PALM_LEFT))
                    ResetSlapSegmentsData(LeftSlapSegmentsData, 4);
                else if ((Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_RIGHT) ||
                    // 2.0.1.0
                    (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_UPPER_HALF_PALM_RIGHT))
                    ResetSlapSegmentsData(RightSlapSegmentsData, 4);
                else if (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_2_THUMBS)
                    ResetSlapSegmentsData(TwoThumbsSegmentsData, 2);
            }
        }

        private void ResetSlapSegmentsData(SegmentData[] SegmentsData, int count)
        {
            for (int i = 0; i < count; i++)
            {
                SegmentsData[i].BoundingBox.X = 0;
                SegmentsData[i].BoundingBox.Y = 0;
                SegmentsData[i].BoundingBox.Width = 0;
                SegmentsData[i].BoundingBox.Height = 0;
                SegmentsData[i].ItemData.Quality = 0;
                SegmentsData[i].ItemData.QualityAlgorithm = 0;
                SegmentsData[i].ItemData.UnavailabilityReason = 0;
            }
        }

        // 2.0.1.0
        private void ResetScanItemData(ScanItem Item)
        {
            // delete file
            String FileName = BuildImageFileName(Item);
            if (File.Exists(FileName))
                File.Delete(FileName);
            // clear picturebox
            if (Item.ImagePictureBox.Image != null)
                Item.ImagePictureBox.Image.Dispose();
            Item.ImagePictureBox.Image = null;

            Item.ItemData.Quality = 0;
            Item.ItemData.QualityAlgorithm = 0;
            Item.ItemData.UnavailabilityReason = 0;

            if ((Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_LEFT) ||
                (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_UPPER_HALF_PALM_LEFT))
                ResetSlapSegmentsData(LeftSlapSegmentsData, 4);
            else if ((Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_RIGHT) ||
                (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_UPPER_HALF_PALM_RIGHT))
                ResetSlapSegmentsData(RightSlapSegmentsData, 4);
            else if (Item.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_2_THUMBS)
                ResetSlapSegmentsData(TwoThumbsSegmentsData, 2);
        }

        private Byte[] CompressToWSQ(Bitmap bmpImage, double Rate, int Dpi)
        {
            Byte[] RawImage;
            int ImageSizeX, ImageSizeY;
            Byte[] CompressedImage;

            // get raw image from bitmap
            ImageSizeX = bmpImage.Width;
            ImageSizeY = bmpImage.Height;
            RawImage = new byte[ImageSizeX * ImageSizeY];
            GBMSGUI.BitmapToRawImage(bmpImage, RawImage);

            // create WSQ encoder
            NW_WSQPACK_Compress WSQEncoder = new NW_WSQPACK_Compress(RawImage, ImageSizeX, ImageSizeY);
            WSQEncoder.BitPerPixel = NW_WSQPACK_PIXEL_DEPTH_DEFINITIONS.NW_WSQPACK_8_BITS_PER_PIXEL;
            //WSQEncoder.CompressionRate = NW_WSQPACK_RATE_DEFINITIONS.NW_WSQPACK_RECOMMENDED;
            WSQEncoder.CompressionRate = (float)Rate;
            WSQEncoder.ImageResolution = Dpi; // in DPI
            // encode
            CompressedImage = WSQEncoder.Encoded;

            return CompressedImage;
        }

        // PROVA
        //[DllImport("GBJpeg.dll")]
        //private static extern void GBJPEG_JP2_Set_Dpi(int Dpi);

        private Byte[] CompressToJP2(Bitmap bmpImage, int Rate)
        {
            Byte[] RawImage;
            int ImageSizeX, ImageSizeY;
            Byte[] CompressedImage;

            // get raw image from bitmap
            ImageSizeX = bmpImage.Width;
            ImageSizeY = bmpImage.Height;
            RawImage = new byte[ImageSizeX * ImageSizeY];
            GBMSGUI.BitmapToRawImage(bmpImage, RawImage);

            // PROVA 
            //GBJPEG_JP2_Set_Dpi(500);

            // create JPEG2000 encoder
            NW_GBJPEG_JPX_Encode JP2Encoder =
                new NW_GBJPEG_JPX_Encode(RawImage, (uint)ImageSizeX, (uint)ImageSizeY);
            JP2Encoder.BitPerPixel = NW_GBJPEG_PIXEL_DEPTH_DEFINITIONS.NW_GBJPEG_8_BITS_PER_PIXEL;
            JP2Encoder.CompressionRate = (ushort)Rate;
            JP2Encoder.CompressionAlgorithm = NW_GBJPEG_ALGORITHMS_DEFINITIONS.NW_GBJPEG_COMP_ALG_JPEG2000;
            // encode
            CompressedImage = JP2Encoder.Encoded;

            // DEBUG
            //File.WriteAllBytes("Image.jp2", CompressedImage);

            return CompressedImage;
        }

        private void SetMissingFingers()
        {
            //int Reason = GBMSGUI.UnavailabilityReason.Unprintable;
            // 2.0.1.0 - Reason is determined by the checkbox threestate
            int Reason;

            if ((CurrentScanItem.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_LEFT) ||
                (CurrentScanItem.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_2_LEFT) ||
                // 2.0.1.0 - was missing
                (CurrentScanItem.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_UPPER_HALF_PALM_LEFT))
            {
                if (chkMissingLeftIndex.Checked)
                {
                    // 2.0.1.0
                    if (chkMissingLeftIndex.CheckState == CheckState.Checked)
                        Reason = GBMSGUI.UnavailabilityReason.Amputated;
                    else
                        Reason = GBMSGUI.UnavailabilityReason.Unprintable;
                    MyGUI.SetSegmentUnavailabilityReason(CurrentScanItem.ScanObjID, 0, Reason);
                }
                if (chkMissingLeftMiddle.Checked)
                {
                    // 2.0.1.0
                    if (chkMissingLeftMiddle.CheckState == CheckState.Checked)
                        Reason = GBMSGUI.UnavailabilityReason.Amputated;
                    else
                        Reason = GBMSGUI.UnavailabilityReason.Unprintable;
                    MyGUI.SetSegmentUnavailabilityReason(CurrentScanItem.ScanObjID, 1, Reason);
                }
                if ((CurrentScanItem.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_LEFT) ||
                    // 2.0.1.0
                    (CurrentScanItem.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_UPPER_HALF_PALM_LEFT))
                {
                    if (chkMissingLeftRing.Checked)
                    {
                        // 2.0.1.0
                        if (chkMissingLeftRing.CheckState == CheckState.Checked)
                            Reason = GBMSGUI.UnavailabilityReason.Amputated;
                        else
                            Reason = GBMSGUI.UnavailabilityReason.Unprintable;
                        MyGUI.SetSegmentUnavailabilityReason(CurrentScanItem.ScanObjID, 2, Reason);
                    }
                    if (chkMissingLeftLittle.Checked)
                    {
                        // 2.0.1.0
                        if (chkMissingLeftLittle.CheckState == CheckState.Checked)
                            Reason = GBMSGUI.UnavailabilityReason.Amputated;
                        else
                            Reason = GBMSGUI.UnavailabilityReason.Unprintable;
                        MyGUI.SetSegmentUnavailabilityReason(CurrentScanItem.ScanObjID, 3, Reason);
                    }
                }
            }

            if ((CurrentScanItem.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_RIGHT) ||
                (CurrentScanItem.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_2_RIGHT) ||
                // 2.0.1.0 - was missing
                (CurrentScanItem.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_UPPER_HALF_PALM_RIGHT))
            {
                if (chkMissingRightIndex.Checked)
                {
                    // 2.0.1.0
                    if (chkMissingRightIndex.CheckState == CheckState.Checked)
                        Reason = GBMSGUI.UnavailabilityReason.Amputated;
                    else
                        Reason = GBMSGUI.UnavailabilityReason.Unprintable;
                    MyGUI.SetSegmentUnavailabilityReason(CurrentScanItem.ScanObjID, 0, Reason);
                }
                if (chkMissingRightMiddle.Checked)
                {
                    // 2.0.1.0
                    if (chkMissingRightMiddle.CheckState == CheckState.Checked)
                        Reason = GBMSGUI.UnavailabilityReason.Amputated;
                    else
                        Reason = GBMSGUI.UnavailabilityReason.Unprintable;
                    MyGUI.SetSegmentUnavailabilityReason(CurrentScanItem.ScanObjID, 1, Reason);
                }
                if ((CurrentScanItem.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_RIGHT) ||
                    // 2.0.1.0
                    (CurrentScanItem.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_UPPER_HALF_PALM_RIGHT))
                {
                    if (chkMissingRightRing.Checked)
                    {
                        // 2.0.1.0
                        if (chkMissingRightRing.CheckState == CheckState.Checked)
                            Reason = GBMSGUI.UnavailabilityReason.Amputated;
                        else
                            Reason = GBMSGUI.UnavailabilityReason.Unprintable;
                        MyGUI.SetSegmentUnavailabilityReason(CurrentScanItem.ScanObjID, 2, Reason);
                    }
                    if (chkMissingRightLittle.Checked)
                    {
                        // 2.0.1.0
                        if (chkMissingRightLittle.CheckState == CheckState.Checked)
                            Reason = GBMSGUI.UnavailabilityReason.Amputated;
                        else
                            Reason = GBMSGUI.UnavailabilityReason.Unprintable;
                        MyGUI.SetSegmentUnavailabilityReason(CurrentScanItem.ScanObjID, 3, Reason);
                    }
                }
            }

            if (CurrentScanItem.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_2_THUMBS)
            {
                if (chkMissingLeftThumb.Checked)
                {
                    // 2.0.1.0
                    if (chkMissingLeftThumb.CheckState == CheckState.Checked)
                        Reason = GBMSGUI.UnavailabilityReason.Amputated;
                    else
                        Reason = GBMSGUI.UnavailabilityReason.Unprintable;
                    MyGUI.SetSegmentUnavailabilityReason(CurrentScanItem.ScanObjID, 0, Reason);
                }
                if (chkMissingRightThumb.Checked)
                {
                    // 2.0.1.0
                    if (chkMissingRightThumb.CheckState == CheckState.Checked)
                        Reason = GBMSGUI.UnavailabilityReason.Amputated;
                    else
                        Reason = GBMSGUI.UnavailabilityReason.Unprintable;
                    MyGUI.SetSegmentUnavailabilityReason(CurrentScanItem.ScanObjID, 1, Reason);
                }
            }

            if (CurrentScanItem.ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_2_INDEXES)
            {
                if (chkMissingLeftIndex.Checked)
                {
                    // 2.0.1.0
                    if (chkMissingLeftIndex.CheckState == CheckState.Checked)
                        Reason = GBMSGUI.UnavailabilityReason.Amputated;
                    else
                        Reason = GBMSGUI.UnavailabilityReason.Unprintable;
                    MyGUI.SetSegmentUnavailabilityReason(CurrentScanItem.ScanObjID, 0, Reason);
                }
                if (chkMissingRightIndex.Checked)
                {
                    // 2.0.1.0
                    if (chkMissingRightIndex.CheckState == CheckState.Checked)
                        Reason = GBMSGUI.UnavailabilityReason.Amputated;
                    else
                        Reason = GBMSGUI.UnavailabilityReason.Unprintable;
                    MyGUI.SetSegmentUnavailabilityReason(CurrentScanItem.ScanObjID, 1, Reason);
                }
            }
        }

        private void SetLanguage()
        {
            if (DemoFormRef.DemoConfig.GUILanguage == DemoForm.GUILanguages.Italian)
                MyGUI.SetLanguage(CultureInfo.CreateSpecificCulture("it"));
            else if (DemoFormRef.DemoConfig.GUILanguage == DemoForm.GUILanguages.Chinese)
                MyGUI.SetLanguage(CultureInfo.CreateSpecificCulture("zh-CN"));
            else
                MyGUI.SetLanguage(CultureInfo.CreateSpecificCulture("en"));
        }

        private void btnViewEJI_Click(object sender, EventArgs e)
        {
            PictureBox pboxRolledJoint = null;
            PictureBox pboxJointLeft = null;
            PictureBox pboxJointCenter = null;
            PictureBox pboxJointRight = null;
            String FileName;

            // build filename for EJI starting from button name
            FileName = ImagesPath + Path.DirectorySeparatorChar + ((Button)sender).Name.Substring(7) + ".bmp";

            // assign the correct pictureboxes
            if (sender == btnViewEJIRightThumb)
            {
                pboxRolledJoint = pboxRightThumbRolledJoint;
                pboxJointLeft = pboxRightThumbJointLeft;
                pboxJointCenter = pboxRightThumbJointCenter;
                pboxJointRight = pboxRightThumbJointRight;
            }
            if (sender == btnViewEJIRightIndex)
            {
                pboxRolledJoint = pboxRightIndexRolledJoint;
                pboxJointLeft = pboxRightIndexJointLeft;
                pboxJointCenter = pboxRightIndexJointCenter;
                pboxJointRight = pboxRightIndexJointRight;
            }
            if (sender == btnViewEJIRightMiddle)
            {
                pboxRolledJoint = pboxRightMiddleRolledJoint;
                pboxJointLeft = pboxRightMiddleJointLeft;
                pboxJointCenter = pboxRightMiddleJointCenter;
                pboxJointRight = pboxRightMiddleJointRight;
            }
            if (sender == btnViewEJIRightRing)
            {
                pboxRolledJoint = pboxRightRingRolledJoint;
                pboxJointLeft = pboxRightRingJointLeft;
                pboxJointCenter = pboxRightRingJointCenter;
                pboxJointRight = pboxRightRingJointRight;
            }
            if (sender == btnViewEJIRightLittle)
            {
                pboxRolledJoint = pboxRightLittleRolledJoint;
                pboxJointLeft = pboxRightLittleJointLeft;
                pboxJointCenter = pboxRightLittleJointCenter;
                pboxJointRight = pboxRightLittleJointRight;
            }
            if (sender == btnViewEJILeftThumb)
            {
                pboxRolledJoint = pboxLeftThumbRolledJoint;
                pboxJointLeft = pboxLeftThumbJointLeft;
                pboxJointCenter = pboxLeftThumbJointCenter;
                pboxJointRight = pboxLeftThumbJointRight;
            }
            if (sender == btnViewEJILeftIndex)
            {
                pboxRolledJoint = pboxLeftIndexRolledJoint;
                pboxJointLeft = pboxLeftIndexJointLeft;
                pboxJointCenter = pboxLeftIndexJointCenter;
                pboxJointRight = pboxLeftIndexJointRight;
            }
            if (sender == btnViewEJILeftMiddle)
            {
                pboxRolledJoint = pboxLeftMiddleRolledJoint;
                pboxJointLeft = pboxLeftMiddleJointLeft;
                pboxJointCenter = pboxLeftMiddleJointCenter;
                pboxJointRight = pboxLeftMiddleJointRight;
            }
            if (sender == btnViewEJILeftRing)
            {
                pboxRolledJoint = pboxLeftRingRolledJoint;
                pboxJointLeft = pboxLeftRingJointLeft;
                pboxJointCenter = pboxLeftRingJointCenter;
                pboxJointRight = pboxLeftRingJointRight;
            }
            if (sender == btnViewEJILeftLittle)
            {
                pboxRolledJoint = pboxLeftLittleRolledJoint;
                pboxJointLeft = pboxLeftLittleJointLeft;
                pboxJointCenter = pboxLeftLittleJointCenter;
                pboxJointRight = pboxLeftLittleJointRight;
            }

            Bitmap EJImage;

            if (File.Exists(FileName))
            {
                EJImage = (Bitmap)Bitmap.FromFile(FileName);
            }
            else
            {
                // check that all pictureboxes are not null
                if ((pboxRolledJoint.Image == null) || (pboxJointLeft.Image == null) ||
                    (pboxJointCenter.Image == null) || (pboxJointRight.Image == null))
                {
                    MessageBox.Show("Some images are missing!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                EJImage = CreateEntireJointImage(pboxRolledJoint, pboxJointLeft, pboxJointCenter, pboxJointRight);

                // save EJI to file
                EJImage.Save(FileName, ImageFormat.Bmp);
            }

            ViewImageForm ViewForm = new ViewImageForm();
            ViewForm.Text = Path.GetFileNameWithoutExtension(FileName);
            ViewForm.bmpImage = EJImage;
            ViewForm.ShowDialog();
        }

        //V1.10
        private Bitmap CreateEntireJointImage(PictureBox pboxRolledJoint, PictureBox pboxJointLeft,
            PictureBox pboxJointCenter, PictureBox pboxJointRight)
        {
            Bitmap EJImage;
            int EJImageWidth = 2250;
            int EJImageHeight = 2500;
            int EJImageBufferSize = EJImageWidth * EJImageHeight;

            // allocate buffer for EJI, 4.5"x5"
            Byte[] EJImageBuffer = new Byte[EJImageBufferSize];
            // fill image with white
            for (int i = 0; i < (EJImageBufferSize); i++)
                Buffer.SetByte(EJImageBuffer, i, 255);

            int DestPosX = 0;
            int DestPosY = 0;

            if (ImageBuffer == null)
                ImageBuffer = new Byte[EJImageBufferSize];

            // copy Rolled Joint entirely
            GBMSGUI.BitmapToRawImage((Bitmap)(pboxRolledJoint.Image), ImageBuffer);

            GBMSGUI.CopyRawImageRegionIntoRawImage(ImageBuffer, pboxRolledJoint.Image.Width, pboxRolledJoint.Image.Height,
                0, 0, pboxRolledJoint.Image.Width, pboxRolledJoint.Image.Height,
                ref EJImageBuffer, EJImageWidth, EJImageHeight, DestPosX, DestPosY);

            // Copy Left side
            GBMSGUI.BitmapToRawImage((Bitmap)(pboxJointLeft.Image), ImageBuffer);

            DestPosX += 800;
            GBMSGUI.CopyRawImageRegionIntoRawImage(ImageBuffer, pboxJointLeft.Image.Width, pboxJointLeft.Image.Height,
                0, 0, pboxJointLeft.Image.Width, pboxJointLeft.Image.Height,
                ref EJImageBuffer, EJImageWidth, EJImageHeight, DestPosX, DestPosY);

            // Copy Center
            GBMSGUI.BitmapToRawImage((Bitmap)(pboxJointCenter.Image), ImageBuffer);

            DestPosX += 483;
            GBMSGUI.CopyRawImageRegionIntoRawImage(ImageBuffer, pboxJointCenter.Image.Width, pboxJointCenter.Image.Height,
                0, 0, pboxJointCenter.Image.Width, pboxJointCenter.Image.Height,
                ref EJImageBuffer, EJImageWidth, EJImageHeight, DestPosX, DestPosY);

            // Copy Right side
            GBMSGUI.BitmapToRawImage((Bitmap)(pboxJointRight.Image), ImageBuffer);

            DestPosX += 483;
            GBMSGUI.CopyRawImageRegionIntoRawImage(ImageBuffer, pboxJointRight.Image.Width, pboxJointRight.Image.Height,
                0, 0, pboxJointRight.Image.Width, pboxJointRight.Image.Height,
                ref EJImageBuffer, EJImageWidth, EJImageHeight, DestPosX, DestPosY);

            EJImage = GBMSGUI.RawImageToBitmap(EJImageBuffer, EJImageWidth, EJImageHeight);

            return EJImage;
        }

        //V1.10
        private void EnableViewEJI()
        {
            if ((pboxRightThumbRolledJoint.Image != null) && (pboxRightThumbJointLeft.Image != null) &&
                (pboxRightThumbJointCenter.Image != null) && (pboxRightThumbJointRight != null))
                btnViewEJIRightThumb.Enabled = true;
            else
                btnViewEJIRightThumb.Enabled = false;

            if ((pboxRightIndexRolledJoint.Image != null) && (pboxRightIndexJointLeft.Image != null) &&
                (pboxRightIndexJointCenter.Image != null) && (pboxRightIndexJointRight != null))
                btnViewEJIRightIndex.Enabled = true;
            else
                btnViewEJIRightIndex.Enabled = false;

            if ((pboxRightMiddleRolledJoint.Image != null) && (pboxRightMiddleJointLeft.Image != null) &&
                (pboxRightMiddleJointCenter.Image != null) && (pboxRightMiddleJointRight != null))
                btnViewEJIRightMiddle.Enabled = true;
            else
                btnViewEJIRightMiddle.Enabled = false;

            if ((pboxRightRingRolledJoint.Image != null) && (pboxRightRingJointLeft.Image != null) &&
                (pboxRightRingJointCenter.Image != null) && (pboxRightRingJointRight != null))
                btnViewEJIRightRing.Enabled = true;
            else
                btnViewEJIRightRing.Enabled = false;

            if ((pboxRightLittleRolledJoint.Image != null) && (pboxRightLittleJointLeft.Image != null) &&
                (pboxRightLittleJointCenter.Image != null) && (pboxRightLittleJointRight != null))
                btnViewEJIRightLittle.Enabled = true;
            else
                btnViewEJIRightLittle.Enabled = false;

            if ((pboxLeftThumbRolledJoint.Image != null) && (pboxLeftThumbJointLeft.Image != null) &&
                (pboxLeftThumbJointCenter.Image != null) && (pboxLeftThumbJointRight != null))
                btnViewEJILeftThumb.Enabled = true;
            else
                btnViewEJILeftThumb.Enabled = false;

            if ((pboxLeftThumbRolledJoint.Image != null) && (pboxLeftIndexJointLeft.Image != null) &&
                (pboxLeftIndexJointCenter.Image != null) && (pboxLeftIndexJointRight != null))
                btnViewEJILeftIndex.Enabled = true;
            else
                btnViewEJILeftIndex.Enabled = false;

            if ((pboxLeftMiddleRolledJoint.Image != null) && (pboxLeftMiddleJointLeft.Image != null) &&
                (pboxLeftMiddleJointCenter.Image != null) && (pboxLeftMiddleJointRight != null))
                btnViewEJILeftMiddle.Enabled = true;
            else
                btnViewEJILeftMiddle.Enabled = false;

            if ((pboxLeftRingRolledJoint.Image != null) && (pboxLeftRingJointLeft.Image != null) &&
                (pboxLeftRingJointCenter.Image != null) && (pboxLeftRingJointRight != null))
                btnViewEJILeftRing.Enabled = true;
            else
                btnViewEJILeftRing.Enabled = false;

            if ((pboxLeftLittleRolledJoint.Image != null) && (pboxLeftLittleJointLeft.Image != null) &&
                (pboxLeftLittleJointCenter.Image != null) && (pboxLeftLittleJointRight != null))
                btnViewEJILeftLittle.Enabled = true;
            else
                btnViewEJILeftLittle.Enabled = false;
        }

        private void btnScanCard_Click(object sender, EventArgs e)
        {
#if GBDCGUI_DEMO
            int ret;

            GBDCGUI_NetWrapper.GBDCGUI_Load();

            ret = GBDCGUI_NetWrapper.GBDCGUI_Acquire();
            if (ret != GBDCGUI_ReturnCodes.GBDCGUI_RET_FAILURE)
            {
                // TODO - display error
            }

            if (ret == GBDCGUI_ReturnCodes.GBDCGUI_RET_SUCCESS)
	        {
                UInt32 ImageSizeX;
                UInt32 ImageSizeY;
                UInt32 Dpi;
                Byte[] ImageBuffer;

		        for (int i=0; i<GBDCGUI_NetWrapper.GBDCGUI_CROP_RECT_MAX_NUM; i++)
		        {
                    GBDCGUI_NetWrapper.GBDCGUI_GetCroppedImageSize(i, out ImageSizeX, out ImageSizeY);
    
                    ImageBuffer = new Byte[ImageSizeX*ImageSizeY];

                    GBDCGUI_NetWrapper.GBDCGUI_GetCroppedImage(i, ImageBuffer, out ImageSizeX, out ImageSizeY, out Dpi);

                    // find corresponding item in the list
                    foreach (ScanItem Item in lstScannedObjects.Items)
                    {
                        if (Item.ScanObjID == GBDCGUIIdToScanObjId(i))
                        {
                            // save image in Bmp format
                            Bitmap bmp;
                            bmp = GBMSGUI.RawImageToBitmap(ImageBuffer, (int)ImageSizeX, (int)ImageSizeY);
                            String FileName = BuildImageFileName(Item);
                            bmp.SetResolution((float)Dpi, (float)Dpi);
                            bmp.Save(FileName, ImageFormat.Bmp);
                            bmp.Dispose();

                            // display image
                            Item.ImagePictureBox.Load(FileName);
                        }
                    }
		        }
	        }

            GBDCGUI_NetWrapper.GBDCGUI_Unload();
#endif
        }

#if GBDCGUI_DEMO
        private uint GBDCGUIIdToScanObjId(int RectID)
        {
            uint ScanObjID = GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SBT_NO_OBJECT;

            switch (RectID)
            {
                case GBDCGUI_RectId.GBDCGUI_ROLL_RIGHT_THUMB:
                    ScanObjID = GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_RIGHT_THUMB;
                    break;
                case GBDCGUI_RectId.GBDCGUI_ROLL_RIGHT_INDEX:
                    ScanObjID = GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_RIGHT_INDEX;
                    break;
                case GBDCGUI_RectId.GBDCGUI_ROLL_RIGHT_MIDDLE:
                    ScanObjID = GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_RIGHT_MIDDLE;
                    break;
                case GBDCGUI_RectId.GBDCGUI_ROLL_RIGHT_RING:
                    ScanObjID = GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_RIGHT_RING;
                    break;
                case GBDCGUI_RectId.GBDCGUI_ROLL_RIGHT_LITTLE:
                    ScanObjID = GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_RIGHT_LITTLE;
                    break;
                case GBDCGUI_RectId.GBDCGUI_ROLL_LEFT_THUMB:
                    ScanObjID = GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_LEFT_THUMB;
                    break;
                case GBDCGUI_RectId.GBDCGUI_ROLL_LEFT_INDEX:
                    ScanObjID = GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_LEFT_INDEX;
                    break;
                case GBDCGUI_RectId.GBDCGUI_ROLL_LEFT_MIDDLE:
                    ScanObjID = GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_LEFT_MIDDLE;
                    break;
                case GBDCGUI_RectId.GBDCGUI_ROLL_LEFT_RING:
                    ScanObjID = GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_LEFT_RING;
                    break;
                case GBDCGUI_RectId.GBDCGUI_ROLL_LEFT_LITTLE:
                    ScanObjID = GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_LEFT_LITTLE;
                    break;
                case GBDCGUI_RectId.GBDCGUI_SLAP_LEFT:
                    ScanObjID = GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_LEFT;
                    break;
                case GBDCGUI_RectId.GBDCGUI_FLAT_THUMB_LEFT:
                    ScanObjID = GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_LEFT_THUMB;
                    break;
                case GBDCGUI_RectId.GBDCGUI_FLAT_THUMB_RIGHT:
                    ScanObjID = GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_RIGHT_THUMB;
                    break;
                case GBDCGUI_RectId.GBDCGUI_SLAP_RIGHT:
                    ScanObjID = GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_RIGHT;
                    break;
            }

            return ScanObjID;
        }
#endif

        private String GetAN2KError(int ErrCode)
        {
            String ErrMsg = "";

            if (ErrCode == NW_AN2K_ERRORS.NW_AN2K_DLL_BAD_PARAMETER)
                ErrMsg = "Bad parameter";
            else if (ErrCode == NW_AN2K_ERRORS.NW_AN2K_DLL_EXCEPTION)
                ErrMsg = NW_AN2K_ERRORS.NW_AN2K_DLL_EXCEPTION_STRING;
            else if (ErrCode == NW_AN2K_ERRORS.NW_AN2K_DLL_FIELD_NOT_FOUND)
                ErrMsg = "Filed not found";
            else if (ErrCode == NW_AN2K_ERRORS.NW_AN2K_DLL_GENERIC)
                ErrMsg = "Generic error";
            else if (ErrCode == NW_AN2K_ERRORS.NW_AN2K_DLL_MEMORY)
                ErrMsg = "Memory error";
            else if (ErrCode == NW_AN2K_ERRORS.NW_AN2K_DLL_RECORD_NOT_FOUND)
                ErrMsg = "Record not found";

            return ErrMsg;
        }

        // converts ScanObjID to AN2K2011 finger position
        public static int ScannedObjectIDToAN2K2011FingerPos(uint ScanObjID)
        {
            int FingerPos = An2k2011_Table8.UNKNOWN;

            switch (ScanObjID)
            {
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_LEFT_THUMB:
                    FingerPos = An2k2011_Table8.PLAIN_LEFT_THUMB;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_LEFT_THUMB:
                    FingerPos = An2k2011_Table8.LEFT_THUMB;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_LEFT_INDEX:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_LEFT_INDEX:
                    FingerPos = An2k2011_Table8.LEFT_INDEX;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_LEFT_MIDDLE:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_LEFT_MIDDLE:
                    FingerPos = An2k2011_Table8.LEFT_MIDDLE;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_LEFT_RING:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_LEFT_RING:
                    FingerPos = An2k2011_Table8.LEFT_RING;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_LEFT_LITTLE:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_LEFT_LITTLE:
                    FingerPos = An2k2011_Table8.LEFT_LITTLE;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_RIGHT_THUMB:
                    FingerPos = An2k2011_Table8.PLAIN_RIGHT_THUMB;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_RIGHT_THUMB:
                    FingerPos = An2k2011_Table8.RIGHT_THUMB;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_RIGHT_INDEX:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_RIGHT_INDEX:
                    FingerPos = An2k2011_Table8.RIGHT_INDEX;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_RIGHT_MIDDLE:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_RIGHT_MIDDLE:
                    FingerPos = An2k2011_Table8.RIGHT_MIDDLE;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_RIGHT_RING:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_RIGHT_RING:
                    FingerPos = An2k2011_Table8.RIGHT_RING;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_RIGHT_LITTLE:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_RIGHT_LITTLE:
                    FingerPos = An2k2011_Table8.RIGHT_LITTLE;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_UPPER_HALF_PALM_LEFT:
                    FingerPos = An2k2011_Table8.LEFT_UPPER_PALM;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_UPPER_HALF_PALM_RIGHT:
                    FingerPos = An2k2011_Table8.RIGHT_UPPER_PALM;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_LOWER_HALF_PALM_LEFT:
                    FingerPos = An2k2011_Table8.LEFT_LOWER_PALM;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_LOWER_HALF_PALM_RIGHT:
                    FingerPos = An2k2011_Table8.RIGHT_LOWER_PALM;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_WRITER_PALM_LEFT:
                    FingerPos = An2k2011_Table8.LEFT_WRITERS_PALM;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_WRITER_PALM_RIGHT:
                    FingerPos = An2k2011_Table8.RIGHT_WRITERS_PALM;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_THENAR_LEFT:
                    FingerPos = An2k2011_Table8.LEFT_THENAR;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_THENAR_RIGHT:
                    FingerPos = An2k2011_Table8.RIGHT_THENAR;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_LEFT:
                    FingerPos = An2k2011_Table8.PLAIN_LEFT_SLAP_4;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_RIGHT:
                    FingerPos = An2k2011_Table8.PLAIN_RIGHT_SLAP_4;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_2_THUMBS:
                    FingerPos = An2k2011_Table8.PLAIN_LEFT_THUMBS_2;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_2_INDEXES:
                    FingerPos = An2k2011_Table8.RIGHT_LEFT_INDEX;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_2_LEFT:
                    FingerPos = An2k2011_Table8.LEFT_INDEX_MIDDLE;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_2_RIGHT:
                    FingerPos = An2k2011_Table8.RIGHT_INDEX_MIDDLE;
                    break;
                // 2.0.1.0 - hypothenars were missing
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_HYPOTHENAR_LEFT:
                    FingerPos = An2k2011_Table8.LEFT_HYPOTHENAR;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_HYPOTHENAR_RIGHT:
                    FingerPos = An2k2011_Table8.RIGHT_HYPOTHENAR;
                    break;
                default:
                    if (GBMSGUI.IsJoint(ScanObjID) || GBMSGUI.IsRolledTip(ScanObjID))
                    {
                        FingerPos = An2k2011_Table8.EJI_OR_TIP;
                        // more detail will go in other fields
                    }
                    break;
            }

            return FingerPos;
        }

        // return decimal position for joints and tips
        public static int GetAN2K2011DecimalPosition(uint ScanObjID)
        {
            int DecimalPos = An2k2011_Table8.UNKNOWN;

            // added single flat and rolled objects
            switch (ScanObjID)
            {
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_LEFT_THUMB:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_LEFT_THUMB:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_LEFT_THUMB:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_LEFT_THUMB:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_TIP_LEFT_THUMB:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_LEFT_THUMB:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_LEFT_THUMB:
                    DecimalPos = An2k2011_Table8.LEFT_THUMB;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_LEFT_INDEX:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_LEFT_INDEX:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_LEFT_INDEX:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_LEFT_INDEX:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_TIP_LEFT_INDEX:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_LEFT_INDEX:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_LEFT_INDEX:
                    DecimalPos = An2k2011_Table8.LEFT_INDEX;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_LEFT_MIDDLE:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_LEFT_MIDDLE:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_LEFT_MIDDLE:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_LEFT_MIDDLE:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_TIP_LEFT_MIDDLE:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_LEFT_MIDDLE:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_LEFT_MIDDLE:
                    DecimalPos = An2k2011_Table8.LEFT_MIDDLE;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_LEFT_RING:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_LEFT_RING:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_LEFT_RING:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_LEFT_RING:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_TIP_LEFT_RING:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_LEFT_RING:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_LEFT_RING:
                    DecimalPos = An2k2011_Table8.LEFT_RING;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_LEFT_LITTLE:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_LEFT_LITTLE:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_LEFT_LITTLE:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_LEFT_LITTLE:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_TIP_LEFT_LITTLE:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_LEFT_LITTLE:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_LEFT_LITTLE:
                    DecimalPos = An2k2011_Table8.LEFT_LITTLE;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_RIGHT_THUMB:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_RIGHT_THUMB:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_RIGHT_THUMB:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_RIGHT_THUMB:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_TIP_RIGHT_THUMB:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_RIGHT_THUMB:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_RIGHT_THUMB:
                    DecimalPos = An2k2011_Table8.RIGHT_THUMB;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_RIGHT_INDEX:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_RIGHT_INDEX:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_RIGHT_INDEX:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_RIGHT_INDEX:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_TIP_RIGHT_INDEX:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_RIGHT_INDEX:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_RIGHT_INDEX:
                    DecimalPos = An2k2011_Table8.RIGHT_INDEX;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_RIGHT_MIDDLE:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_RIGHT_MIDDLE:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_RIGHT_MIDDLE:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_RIGHT_MIDDLE:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_TIP_RIGHT_MIDDLE:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_RIGHT_MIDDLE:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_RIGHT_MIDDLE:
                    DecimalPos = An2k2011_Table8.RIGHT_MIDDLE;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_RIGHT_RING:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_RIGHT_RING:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_RIGHT_RING:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_RIGHT_RING:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_TIP_RIGHT_RING:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_RIGHT_RING:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_RIGHT_RING:
                    DecimalPos = An2k2011_Table8.RIGHT_RING;
                    break;
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_RIGHT_LITTLE:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_RIGHT_LITTLE:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_RIGHT_LITTLE:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_RIGHT_LITTLE:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_TIP_RIGHT_LITTLE:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_RIGHT_LITTLE:
                case GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_RIGHT_LITTLE:
                    DecimalPos = An2k2011_Table8.RIGHT_LITTLE;
                    break;
            }

            return DecimalPos;
        }

        // return decimal position for joints and tips
        public static String GetAN2K2011ImageCode(uint ScanObjID)
        {
            uint ObjectType = GBMSAPI_NET_ScanObjectsUtilities.GBMSAPI_NET_GetTypeFromObject(ScanObjID);
            String ImageCode = "";

            switch (ObjectType)
            {
                case GBMSAPI_NET_ScannableBiometricTypes.GBMSAPI_NET_SBT_ROLLED_JOINT:
                    ImageCode = An2k2011_Table9.FULL_FINGER_ROLLED;
                    break;
                case GBMSAPI_NET_ScannableBiometricTypes.GBMSAPI_NET_SBT_ROLLED_JOINT_CENTER:
                    ImageCode = An2k2011_Table9.FULL_FINGER_CENTER;
                    break;
                case GBMSAPI_NET_ScannableBiometricTypes.GBMSAPI_NET_SBT_PLAIN_JOINT_LEFT_SIDE:
                    ImageCode = An2k2011_Table9.FULL_FINGER_LEFT_SIDE;
                    break;
                case GBMSAPI_NET_ScannableBiometricTypes.GBMSAPI_NET_SBT_PLAIN_JOINT_RIGHT_SIDE:
                    ImageCode = An2k2011_Table9.FULL_FINGER_RIGHT_SIDE;
                    break;
                case GBMSAPI_NET_ScannableBiometricTypes.GBMSAPI_NET_SBT_ROLLED_TIP:
                    ImageCode = An2k2011_Table9.ROLLED_TIP;
                    break;
            }

            return ImageCode;
        }

        // DemoRecord2 for EBTS
        public class DemoRecord2 : An2kEngineNw_TaggedRecord
        {
            public const int NameFieldId = 3;

            public int Create(int idc, string name, string surname)
            {
                ///////////////////////////
                // check parameters
                ///////////////////////////
                if (
                    (name == null) && (surname == null)
                )
                {
                    return NW_AN2K_ERRORS.NW_AN2K_DLL_BAD_PARAMETER;
                }

                /////////////////////////////////////////
                // CREATE RECORD
                /////////////////////////////////////////
                int Retval = this.CreateEmpty(2, idc);
                if (Retval != NW_AN2K_ERRORS.NW_AN2K_DLL_NO_ERROR)
                {
                    return Retval;
                }

                /////////////////////////////////////////
                // INSERT NAME FIELD
                /////////////////////////////////////////
                An2kEngineNw_FieldForTaggedRecord dummyField = new An2kEngineNw_FieldForTaggedRecord();
                string[] strArray = new string[2];
                strArray[0] = name;
                strArray[1] = surname;
                Retval = dummyField.CreateFromValues(2, NameFieldId, strArray);
                if (Retval != NW_AN2K_ERRORS.NW_AN2K_DLL_NO_ERROR)
                {
                    return Retval;
                }
                Retval = this.InsertField(dummyField);
                if (Retval != NW_AN2K_ERRORS.NW_AN2K_DLL_NO_ERROR)
                {
                    return Retval;
                }

                return NW_AN2K_ERRORS.NW_AN2K_DLL_NO_ERROR;
            }

            public int GetInfo(out int idc, out string name, out string surname)
            {
                name = null;
                surname = null;
                idc = -1;

                /////////////////////////////////////////
                // IDC
                /////////////////////////////////////////
                An2kEngineNw_FieldForTaggedRecord dummyField = this.SearchField(2);
                if (dummyField == null)
                {
                    return NW_AN2K_ERRORS.NW_AN2K_DLL_BAD_PARAMETER;
                }
                string[] strArray;
                int RetVal = dummyField.GetValuesFromItems(out strArray);
                if (RetVal != NW_AN2K_ERRORS.NW_AN2K_DLL_NO_ERROR)
                {
                    return NW_AN2K_ERRORS.NW_AN2K_DLL_BAD_PARAMETER;
                }
                if (strArray == null || strArray.Length < 1)
                {
                    return NW_AN2K_ERRORS.NW_AN2K_DLL_BAD_PARAMETER;
                }
                if (!(int.TryParse(strArray[0], out idc)))
                    return NW_AN2K_ERRORS.NW_AN2K_DLL_BAD_PARAMETER;

                /////////////////////////////////////////
                // NAME AND SURNAME
                /////////////////////////////////////////
                dummyField = this.SearchField(NameFieldId);
                if (dummyField == null)
                {
                    return NW_AN2K_ERRORS.NW_AN2K_DLL_BAD_PARAMETER;
                }
                RetVal = dummyField.GetValuesFromItems(out strArray);
                if (RetVal != NW_AN2K_ERRORS.NW_AN2K_DLL_NO_ERROR)
                {
                    return NW_AN2K_ERRORS.NW_AN2K_DLL_BAD_PARAMETER;
                }
                if (strArray == null || strArray.Length < 2)
                {
                    return NW_AN2K_ERRORS.NW_AN2K_DLL_BAD_PARAMETER;
                }
                name = strArray[0];
                surname = strArray[1];

                return NW_AN2K_ERRORS.NW_AN2K_DLL_NO_ERROR;
            }
        }

        // save acquired Joints data in a binary file
        private bool SaveJointsData()
        {
            FileStream fs;
            BinaryWriter w;

            try
            {
                fs = new FileStream(ImagesPath + Path.DirectorySeparatorChar + "JointsData.dat", FileMode.Create);
                w = new BinaryWriter(fs);

                for (int i = 0; i < 40; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        w.Write(JointsBBoxData[i].PhalangeBBox[j].X);
                        w.Write(JointsBBoxData[i].PhalangeBBox[j].Y);
                        w.Write(JointsBBoxData[i].PhalangeBBox[j].Width);
                        w.Write(JointsBBoxData[i].PhalangeBBox[j].Height);
                    }
                }

                w.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        // read saved joints data from a binary file
        private bool ReadJointsData()
        {
            FileStream fs;
            BinaryReader r;

            try
            {
                fs = new FileStream(ImagesPath + Path.DirectorySeparatorChar + "JointsData.dat", FileMode.Open);
                r = new BinaryReader(fs);

                for (int i = 0; i < 40; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        JointsBBoxData[i].PhalangeBBox[j].X = r.ReadInt32();
                        JointsBBoxData[i].PhalangeBBox[j].Y = r.ReadInt32();
                        JointsBBoxData[i].PhalangeBBox[j].Width = r.ReadInt32();
                        JointsBBoxData[i].PhalangeBBox[j].Height = r.ReadInt32();
                    }
                }

                r.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        // fix for new .net framework 
        private void LoadPictureBoxImage(PictureBox pBox, String FileName)
        {
            FileStream fs;
            fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
            pBox.Image = System.Drawing.Image.FromStream(fs);
            fs.Close();
        }
        // 2.4.0.0
        private Bitmap LoadBitmap(String FileName)
        {
            FileStream fs;
            fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
            Bitmap Bmp = (Bitmap)System.Drawing.Image.FromStream(fs);
            fs.Close();

            return Bmp;
        }

        // 2.0.1.0 - disable same fingers set as unavailable
        private void DisableScanItemSameFinger(uint ScanObjID, int Index, int UnavailabilityReason)
        {
            // disable same fingers in the list
            for (int i = 0; i < lstScannedObjects.Items.Count; i++)
            {
                ScanItem Item = (ScanItem)lstScannedObjects.Items[i];

                if (IsSameFinger(ScanObjID, Index, Item.ScanObjID, -1))
                    lstScannedObjects.SetItemChecked(lstScannedObjects.Items.IndexOf(Item), false);
            }

            // set checkboxes for slaps
            if (IsLeftThumb(ScanObjID, Index))
            {
                if (UnavailabilityReason == GBMSGUI.UnavailabilityReason.Amputated)
                    chkMissingLeftThumb.CheckState = CheckState.Checked;
                else if (UnavailabilityReason == GBMSGUI.UnavailabilityReason.Unprintable)
                    chkMissingLeftThumb.CheckState = CheckState.Indeterminate;
            }
            if (IsLeftIndex(ScanObjID, Index))
            {
                if (UnavailabilityReason == GBMSGUI.UnavailabilityReason.Amputated)
                    chkMissingLeftIndex.CheckState = CheckState.Checked;
                else if (UnavailabilityReason == GBMSGUI.UnavailabilityReason.Unprintable)
                    chkMissingLeftIndex.CheckState = CheckState.Indeterminate;
            }
            if (IsLeftMiddle(ScanObjID, Index))
            {
                if (UnavailabilityReason == GBMSGUI.UnavailabilityReason.Amputated)
                    chkMissingLeftMiddle.CheckState = CheckState.Checked;
                else if (UnavailabilityReason == GBMSGUI.UnavailabilityReason.Unprintable)
                    chkMissingLeftMiddle.CheckState = CheckState.Indeterminate;
            }
            if (IsLeftRing(ScanObjID, Index))
            {
                if (UnavailabilityReason == GBMSGUI.UnavailabilityReason.Amputated)
                    chkMissingLeftRing.CheckState = CheckState.Checked;
                else if (UnavailabilityReason == GBMSGUI.UnavailabilityReason.Unprintable)
                    chkMissingLeftRing.CheckState = CheckState.Indeterminate;
            }
            if (IsLeftLittle(ScanObjID, Index))
            {
                if (UnavailabilityReason == GBMSGUI.UnavailabilityReason.Amputated)
                    chkMissingLeftLittle.CheckState = CheckState.Checked;
                else if (UnavailabilityReason == GBMSGUI.UnavailabilityReason.Unprintable)
                    chkMissingLeftLittle.CheckState = CheckState.Indeterminate;
            }
            if (IsRightThumb(ScanObjID, Index))
            {
                if (UnavailabilityReason == GBMSGUI.UnavailabilityReason.Amputated)
                    chkMissingRightThumb.CheckState = CheckState.Checked;
                else if (UnavailabilityReason == GBMSGUI.UnavailabilityReason.Unprintable)
                    chkMissingRightThumb.CheckState = CheckState.Indeterminate;
            }
            if (IsRightIndex(ScanObjID, Index))
            {
                if (UnavailabilityReason == GBMSGUI.UnavailabilityReason.Amputated)
                    chkMissingRightIndex.CheckState = CheckState.Checked;
                else if (UnavailabilityReason == GBMSGUI.UnavailabilityReason.Unprintable)
                    chkMissingRightIndex.CheckState = CheckState.Indeterminate;
            }
            if (IsRightMiddle(ScanObjID, Index))
            {
                if (UnavailabilityReason == GBMSGUI.UnavailabilityReason.Amputated)
                    chkMissingRightMiddle.CheckState = CheckState.Checked;
                else if (UnavailabilityReason == GBMSGUI.UnavailabilityReason.Unprintable)
                    chkMissingRightMiddle.CheckState = CheckState.Indeterminate;
            }
            if (IsRightLittle(ScanObjID, Index))
            {
                if (UnavailabilityReason == GBMSGUI.UnavailabilityReason.Amputated)
                    chkMissingRightLittle.CheckState = CheckState.Checked;
                else if (UnavailabilityReason == GBMSGUI.UnavailabilityReason.Unprintable)
                    chkMissingRightLittle.CheckState = CheckState.Indeterminate;
            }
        }

        // 2.0.1.0
        private bool IsLeftThumb(uint ScanObjID, int Index)
        {
            if ((ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_LEFT_THUMB) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_LEFT_THUMB) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_LEFT_THUMB) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_LEFT_THUMB) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_TIP_LEFT_THUMB) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_LEFT_THUMB) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_LEFT_THUMB) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_UP_LEFT_THUMB) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_DOWN_LEFT_THUMB))
                return true;

            if ((ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_2_THUMBS) && (Index == 0))
                return true;

            return false;
        }

        // 2.0.1.0
        private bool IsLeftIndex(uint ScanObjID, int Index)
        {
            if ((ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_LEFT_INDEX) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_LEFT_INDEX) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_LEFT_INDEX) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_LEFT_INDEX) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_TIP_LEFT_INDEX) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_LEFT_INDEX) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_LEFT_INDEX) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_UP_LEFT_INDEX) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_DOWN_LEFT_INDEX))
                return true;

            if (((ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_LEFT) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_UPPER_HALF_PALM_LEFT) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_2_LEFT))
                && (Index == 0))
                return true;

            return false;
        }

        // 2.0.1.0
        private bool IsLeftMiddle(uint ScanObjID, int Index)
        {
            if ((ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_LEFT_MIDDLE) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_LEFT_MIDDLE) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_LEFT_MIDDLE) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_LEFT_MIDDLE) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_TIP_LEFT_MIDDLE) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_LEFT_MIDDLE) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_LEFT_MIDDLE) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_UP_LEFT_MIDDLE) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_DOWN_LEFT_MIDDLE))
                return true;

            if (((ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_LEFT) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_UPPER_HALF_PALM_LEFT) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_2_LEFT))
                && (Index == 1))
                return true;

            return false;
        }

        // 2.0.1.0
        private bool IsLeftRing(uint ScanObjID, int Index)
        {
            if ((ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_LEFT_RING) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_LEFT_RING) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_LEFT_RING) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_LEFT_RING) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_TIP_LEFT_RING) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_LEFT_RING) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_LEFT_RING) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_UP_LEFT_RING) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_DOWN_LEFT_RING))
                return true;

            if (((ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_LEFT) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_UPPER_HALF_PALM_LEFT))
                && (Index == 2))
                return true;

            return false;
        }

        // 2.0.1.0
        private bool IsLeftLittle(uint ScanObjID, int Index)
        {
            if ((ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_LEFT_LITTLE) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_LEFT_LITTLE) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_LEFT_LITTLE) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_LEFT_LITTLE) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_TIP_LEFT_LITTLE) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_LEFT_LITTLE) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_LEFT_LITTLE) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_UP_LEFT_LITTLE) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_DOWN_LEFT_LITTLE))
                return true;

            if (((ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_LEFT) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_UPPER_HALF_PALM_LEFT))
                && (Index == 3))
                return true;

            return false;
        }

        // 2.0.1.0
        private bool IsRightThumb(uint ScanObjID, int Index)
        {
            if ((ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_RIGHT_THUMB) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_RIGHT_THUMB) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_RIGHT_THUMB) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_RIGHT_THUMB) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_TIP_RIGHT_THUMB) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_RIGHT_THUMB) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_RIGHT_THUMB) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_UP_RIGHT_THUMB) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_DOWN_RIGHT_THUMB))
                return true;

            if ((ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_2_THUMBS) && (Index == 1))
                return true;

            return false;
        }

        // 2.0.1.0
        private bool IsRightIndex(uint ScanObjID, int Index)
        {
            if ((ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_RIGHT_INDEX) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_RIGHT_INDEX) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_RIGHT_INDEX) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_RIGHT_INDEX) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_TIP_RIGHT_INDEX) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_RIGHT_INDEX) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_RIGHT_INDEX) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_UP_RIGHT_INDEX) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_DOWN_RIGHT_INDEX))
                return true;

            if (((ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_RIGHT) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_UPPER_HALF_PALM_RIGHT) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_2_RIGHT))
                && (Index == 0))
                return true;

            return false;
        }

        // 2.0.1.0
        private bool IsRightMiddle(uint ScanObjID, int Index)
        {
            if ((ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_RIGHT_MIDDLE) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_RIGHT_MIDDLE) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_RIGHT_MIDDLE) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_RIGHT_MIDDLE) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_TIP_RIGHT_MIDDLE) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_RIGHT_MIDDLE) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_RIGHT_MIDDLE) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_UP_RIGHT_MIDDLE) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_DOWN_RIGHT_MIDDLE))
                return true;

            if (((ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_RIGHT) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_UPPER_HALF_PALM_RIGHT) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_2_RIGHT))
                && (Index == 1))
                return true;

            return false;
        }

        // 2.0.1.0
        private bool IsRightRing(uint ScanObjID, int Index)
        {
            if ((ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_RIGHT_RING) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_RIGHT_RING) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_RIGHT_RING) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_RIGHT_RING) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_TIP_RIGHT_RING) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_RIGHT_RING) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_RIGHT_RING) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_UP_RIGHT_RING) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_DOWN_RIGHT_RING))
                return true;

            if (((ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_RIGHT) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_UPPER_HALF_PALM_RIGHT))
                && (Index == 2))
                return true;

            return false;
        }

        // 2.0.1.0
        private bool IsRightLittle(uint ScanObjID, int Index)
        {
            if ((ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_FLAT_RIGHT_LITTLE) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLL_RIGHT_LITTLE) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_RIGHT_LITTLE) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_JOINT_CENTER_RIGHT_LITTLE) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_TIP_RIGHT_LITTLE) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_LEFT_SIDE_RIGHT_LITTLE) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_PLAIN_JOINT_RIGHT_SIDE_RIGHT_LITTLE) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_UP_RIGHT_LITTLE) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_ROLLED_DOWN_RIGHT_LITTLE))
                return true;

            if (((ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_SLAP_4_RIGHT) ||
                (ScanObjID == GBMSAPI_NET_ScannableObjects.GBMSAPI_NET_SO_UPPER_HALF_PALM_RIGHT))
                && (Index == 3))
                return true;

            return false;
        }

        // 2.0.1.0
        private bool IsSameFinger(uint ScanObjID1, int Index1, uint ScanObjID2, int Index2)
        {
            if (IsLeftThumb(ScanObjID1, Index1) && IsLeftThumb(ScanObjID2, Index2))
                return true;
            if (IsLeftIndex(ScanObjID1, Index1) && IsLeftIndex(ScanObjID2, Index2))
                return true;
            if (IsLeftMiddle(ScanObjID1, Index1) && IsLeftMiddle(ScanObjID2, Index2))
                return true;
            if (IsLeftRing(ScanObjID1, Index1) && IsLeftRing(ScanObjID2, Index2))
                return true;
            if (IsLeftLittle(ScanObjID1, Index1) && IsLeftLittle(ScanObjID2, Index2))
                return true;
            if (IsRightThumb(ScanObjID1, Index1) && IsRightThumb(ScanObjID2, Index2))
                return true;
            if (IsRightIndex(ScanObjID1, Index1) && IsRightIndex(ScanObjID2, Index2))
                return true;
            if (IsRightMiddle(ScanObjID1, Index1) && IsRightMiddle(ScanObjID2, Index2))
                return true;
            if (IsRightRing(ScanObjID1, Index1) && IsRightRing(ScanObjID2, Index2))
                return true;
            if (IsRightLittle(ScanObjID1, Index1) && IsRightLittle(ScanObjID2, Index2))
                return true;

            return false;
        }

        // 2.4.0.0
        private void mnuSaveAs_Click(object sender, EventArgs e)
        {
            String ItemName = "";
            Bitmap BmpImage = null;
            String BmpFileName = "";

            // search current item
            foreach (ScanItem Item in lstScannedObjects.Items)
            {
                if (Item.ImagePictureBox == popAcquireItemMenu.SourceControl)
                {
                    ItemName = Item.Text;
                    BmpFileName = BuildImageFileName(Item);
                    BmpImage = LoadBitmap(BmpFileName);
                }
            }

            saveFileDialog1.Title = "Save as...";
            saveFileDialog1.Filter = "Bmp|*.bmp|WSQ|*.wsq|Jpeg|*.jpg;*.jpeg|Jpeg 2000|*.jp2;*.j2k";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.DefaultExt = "bmp";
            saveFileDialog1.FileName = ItemName;
            saveFileDialog1.OverwritePrompt = true;

            saveFileDialog1.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);

            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            
            String FileName = saveFileDialog1.FileName;
            String Extension = Path.GetExtension(FileName);

            Byte[] CompressedImage;

            if (Extension == ".bmp")
            {
                // copy file
                File.Copy(BmpFileName, FileName, true);
            }
            else if (Extension == ".wsq")
            {
                double WSQRate;
                if (UserData.AcquisitionDpi == 1000)
                    WSQRate = DemoFormRef.DemoConfig.WQSBitRate1000;
                else
                    WSQRate = DemoFormRef.DemoConfig.WQSBitRate500;
                CompressedImage = CompressToWSQ(BmpImage, WSQRate, UserData.AcquisitionDpi);
                WriteBufferToFile(CompressedImage, FileName);
            }
            else if ((Extension == ".jpg") || (Extension == ".jpeg"))
            {
                CompressedImage = CompressToJPEG(BmpImage, 100);
                WriteBufferToFile(CompressedImage, FileName);
            }
            else if ((Extension == ".jp2") || (Extension == ".j2k"))
            {
                int JPEGRate;
                if (UserData.AcquisitionDpi == 1000)
                    JPEGRate = DemoFormRef.DemoConfig.JPEGRate1000;
                else
                    JPEGRate = DemoFormRef.DemoConfig.JPEGRate500;
                CompressedImage = CompressToJP2(BmpImage, JPEGRate);
                WriteBufferToFile(CompressedImage, FileName);
            }

            BmpImage.Dispose();
        }

        // 2.4.0.0
        bool WriteBufferToFile(Byte[] Buffer, String FileName)
        {
            FileStream fs;
            BinaryWriter w;

            try
            {
                fs = new FileStream(FileName, FileMode.Create);
                w = new BinaryWriter(fs);
                w.Write(Buffer);
                w.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        // 2.4.0.0
        private Byte[] CompressToJPEG(Bitmap bmpImage, int Quality)
        {
            Byte[] RawImage;
            int ImageSizeX, ImageSizeY;
            Byte[] CompressedImage;

            // get raw image from bitmap
            ImageSizeX = bmpImage.Width;
            ImageSizeY = bmpImage.Height;
            RawImage = new byte[ImageSizeX * ImageSizeY];
            GBMSGUI.BitmapToRawImage(bmpImage, RawImage);

            // create JPEG2000 encoder
            NW_GBJPEG_JPX_Encode JPEGEncoder =
                new NW_GBJPEG_JPX_Encode(RawImage, (uint)ImageSizeX, (uint)ImageSizeY);
            JPEGEncoder.BitPerPixel = NW_GBJPEG_PIXEL_DEPTH_DEFINITIONS.NW_GBJPEG_8_BITS_PER_PIXEL;
            JPEGEncoder.CompressionQuality = (ushort)Quality;
            JPEGEncoder.CompressionAlgorithm = NW_GBJPEG_ALGORITHMS_DEFINITIONS.NW_GBJPEG_COMP_ALG_JPG;
            // encode
            CompressedImage = JPEGEncoder.Encoded;

            return CompressedImage;
        }

    }
}