// Decompiled with JetBrains decompiler
// Type: SampleSegmentation.SegmentacionRod
// Assembly: SampleSegmentation, Version=1.0.7377.2474, Culture=neutral, PublicKeyToken=null
// MVID: 8EEE9A37-5580-46FE-B8FF-BE51BC690095
// Assembly location: C:\c#\Enroll\codigo\bin\x64\Debug\SampleSegmentation.dll

using Innovatrics.ISegLib;
using Innovatrics.ISegLib.Enums;
using Innovatrics.Sdk.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace ENROLL.Helpers.SegmentacionRod

{
    public class SegmentacionRod : Form
    {
        private static readonly TextWriter Output = Console.Out;
        private IContainer components = (IContainer)null;

        public SegmentacionRod()
        {
            this.InitializeComponent();
        }

        private void Segmentacion_Load(object sender, EventArgs e)
        {
        }

        public Bitmap CropImage(Bitmap source, Rectangle section)
        {
            Bitmap bitmap = new Bitmap(section.Width, section.Height);
            using (Graphics graphics = Graphics.FromImage((Image)bitmap))
            {
                graphics.DrawImage((Image)source, 0, 0, section, GraphicsUnit.Pixel);
                return (Bitmap)this.CambiarTamanoImagen((Image)bitmap, 400, 500);
            }
        }

        public Image CambiarTamanoImagen(Image pImagen, int pAncho, int pAlto)
        {
            Bitmap bitmap = new Bitmap(pAncho, pAlto);
            using (Graphics graphics = Graphics.FromImage((Image)bitmap))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(pImagen, 0, 0, pAncho, pAlto);
            }
            return (Image)bitmap;
        }

        public Dictionary<string, string> enviar(
          Image pbDactilarIzq,
          Image pbDactilarDer,
          Image pbpalmaizquierda,
          Image pbpalmaderecha,
          ref DataTable dt)
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Tipo");
                dataTable.Columns.Add("Nombre");
                dataTable.Columns.Add("Posicion");
                dataTable.Columns.Add("Q");
                dataTable.Columns.Add("N");
                Dictionary<string, string> dictionary1 = new Dictionary<string, string>();
                Innovatrics.ISegLib.ISegLib instance = Innovatrics.ISegLib.ISegLib.Instance;
                instance.Init();
                int num1 = 0;
                for (int index1 = 0; index1 < 4; ++index1)
                {
                    int num2 = 0;
                    RawImage image = (RawImage)null;
                    string str1 = "";
                    Bitmap source = (Bitmap)null;
                    if (index1 == 0 && pbDactilarDer != null)
                    {
                        image = RawImageExtension.ConvertImageToRaw(pbDactilarDer);
                        str1 = "pbDactilarDer.bmp";
                        source = new Bitmap(pbDactilarDer);
                    }
                    if (index1 == 1 && pbDactilarIzq != null)
                    {
                        image = RawImageExtension.ConvertImageToRaw(pbDactilarIzq);
                        str1 = "pbDactilarIzq.bmp";
                        source = new Bitmap(pbDactilarIzq);
                    }
                    if (index1 == 2 && pbpalmaizquierda != null)
                    {
                        image = RawImageExtension.ConvertImageToRaw(pbpalmaizquierda);
                        str1 = "pbpalmaizquierda.bmp";
                    }
                    if (index1 == 3 && pbpalmaderecha != null)
                    {
                        image = RawImageExtension.ConvertImageToRaw(pbpalmaderecha);
                        str1 = "pbpalmaderecha.bmp";
                    }
                    int minFingersCount = 1;
                    int maxFingersCount = 4;
                    int expectedFingersCount = 4;
                    int maxRotation = 40;
                    int outWidth = 400;
                    int outHeight = 500;
                    byte maxValue = byte.MaxValue;
                    try
                    {
                        if (str1 == "pbDactilarIzq.bmp" || str1 == "pbDactilarDer.bmp")
                        {
                            int width1 = source.Width;
                            int height = source.Height;
                            int width2 = width1 / 5;
                            Rectangle section;
                            if (str1 == "pbDactilarDer.bmp")
                            {
                                int x = 1;
                                for (int index2 = 1; index2 <= 5; ++index2)
                                {
                                    section = new Rectangle(new Point(x, 1), new Size(width2, height));
                                    Bitmap bitmap = this.CropImage(source, section);
                                    string str2 = "dedo" + index2.ToString() + str1;
                                    x = width2 * index2;
                                    RawImage raw = RawImageExtension.ConvertImageToRaw((Image)bitmap);
                                    bitmap.Dispose();
                                    try
                                    {
                                        using (SegmentationResult segmentationResult = instance.SegmentFingerprints(raw, expectedFingersCount, minFingersCount, maxFingersCount, maxRotation, SegmentationOptions.None, outWidth, outHeight, maxValue))
                                        {
                                            int globalAngle = segmentationResult.GlobalAngle;
                                            int length = segmentationResult.Fingerprints.Length;
                                            IList<SegmentationInfoDefines> segmentationFeedback = segmentationResult.SegmentationFeedback;
                                            if (!instance.SegmentationFeedbackHasMissingFinger(segmentationFeedback))

                                                if (!segmentationFeedback.Contains(SegmentationInfoDefines.SegmentationInfoLeftHand))

                                                    if (!segmentationFeedback.Contains(SegmentationInfoDefines.SegmentationInfoRightHand))
                                                        for (int index3 = 0; index3 < segmentationResult.Fingerprints.Length; ++index3)
                                                        {
                                                            SegmentedFingerprint fingerprint = segmentationResult.Fingerprints[index3];
                                                            dictionary1.Add(index2.ToString() + "_" + str2, "Dedo:" + index2.ToString() + "|Q " + fingerprint.RawImage.GetImageQuality().ToString() + " |N " + fingerprint.RawImage.GetNFIQScore().ToString());
                                                            dataTable.Rows.Add((object)nameof(pbDactilarDer), (object)(index2.ToString() + "_" + str2), (object)index2, (object)fingerprint.RawImage.GetImageQuality(), (object)fingerprint.RawImage.GetNFIQScore());
                                                            ++num1;
                                                            ++num2;
                                                        }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        ++num1;
                                        dataTable.Rows.Add((object)nameof(pbDactilarDer), null, null, null, null);
                                        if (!(ex.Message == "Invalid image or unsupported image format."))
                                        {
                                            if (!(ex.Message == "Found less fingers than expected."))
                                            {
                                            }

                                        }
                                    }
                                }
                            }
                            if (str1 == "pbDactilarIzq.bmp")
                            {
                                int x = 1;
                                int num3 = 1;
                                for (int index2 = 6; index2 <= 10; ++index2)
                                {
                                    section = new Rectangle(new Point(x, 1), new Size(width2, height));
                                    Bitmap bitmap = this.CropImage(source, section);
                                    string str2 = "dedo" + index2.ToString() + str1;
                                    x = width2 * num3;
                                    ++num3;
                                    RawImage raw = RawImageExtension.ConvertImageToRaw((Image)bitmap);
                                    bitmap.Dispose();
                                    try
                                    {
                                        using (SegmentationResult segmentationResult = instance.SegmentFingerprints(raw, expectedFingersCount, minFingersCount, maxFingersCount, maxRotation, SegmentationOptions.None, outWidth, outHeight, maxValue))
                                        {
                                            int globalAngle = segmentationResult.GlobalAngle;
                                            int length = segmentationResult.Fingerprints.Length;
                                            IList<SegmentationInfoDefines> segmentationFeedback = segmentationResult.SegmentationFeedback;
                                            if (instance.SegmentationFeedbackHasMissingFinger(segmentationFeedback))
                                            {
                                                dictionary1.Add(index2.ToString() + "_" + str2, "La imagen enviada, no contiene todos los dedos.");
                                                dictionary1.Add(index2.ToString() + "_" + str2, "Desaparecido " + instance.GetFingerName(segmentationFeedback));
                                            }
                                            if (!segmentationFeedback.Contains(SegmentationInfoDefines.SegmentationInfoLeftHand))

                                                if (!segmentationFeedback.Contains(SegmentationInfoDefines.SegmentationInfoRightHand))

                                                    for (int index3 = 0; index3 < segmentationResult.Fingerprints.Length; ++index3)
                                                    {
                                                        SegmentedFingerprint fingerprint = segmentationResult.Fingerprints[index3];
                                                        dictionary1.Add(index2.ToString() + "_" + str2, "Dedo:" + index2.ToString() + "|Q " + fingerprint.RawImage.GetImageQuality().ToString() + " |N " + fingerprint.RawImage.GetNFIQScore().ToString());
                                                        dataTable.Rows.Add((object)nameof(pbDactilarIzq), (object)(index2.ToString() + "_" + str2), (object)index2, (object)fingerprint.RawImage.GetImageQuality(), (object)fingerprint.RawImage.GetNFIQScore());
                                                        ++num2;
                                                        ++num1;
                                                    }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        dataTable.Rows.Add((object)nameof(pbDactilarIzq), null, null, null, null);
                                        ++num1;
                                        if (!(ex.Message == "Invalid image or unsupported image format."))
                                        {
                                            if (!(ex.Message == "Found less fingers than expected."))
                                            {

                                            }

                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            try
                            {
                                using (SegmentationResult segmentationResult = instance.SegmentFingerprints(image, expectedFingersCount, minFingersCount, maxFingersCount, maxRotation, SegmentationOptions.None, outWidth, outHeight, maxValue))
                                {
                                    int globalAngle = segmentationResult.GlobalAngle;
                                    int length = segmentationResult.Fingerprints.Length;
                                    IList<SegmentationInfoDefines> segmentationFeedback = segmentationResult.SegmentationFeedback;
                                    if (instance.SegmentationFeedbackHasMissingFinger(segmentationFeedback))
                                    {
                                        Dictionary<string, string> dictionary2 = dictionary1;
                                        int num3 = num2;
                                        int num4 = num3 + 1;
                                        int num5 = num3;
                                        string key1 = num5.ToString() + "_" + str1;
                                        dictionary2.Add(key1, "La imagen enviada, no contiene todos los dedos.");
                                        Dictionary<string, string> dictionary3 = dictionary1;
                                        int num6 = num4;
                                        num2 = num6 + 1;
                                        num5 = num6;
                                        string key2 = num5.ToString() + "_" + str1;
                                        string str2 = "Desaparecido " + instance.GetFingerName(segmentationFeedback);
                                        dictionary3.Add(key2, str2);
                                    }
                                    if (!segmentationFeedback.Contains(SegmentationInfoDefines.SegmentationInfoLeftHand))

                                        if (!segmentationFeedback.Contains(SegmentationInfoDefines.SegmentationInfoRightHand))

                                            for (int index2 = 0; index2 < segmentationResult.Fingerprints.Length; ++index2)
                                            {
                                                ++num2;
                                                SegmentedFingerprint fingerprint = segmentationResult.Fingerprints[index2];
                                                Dictionary<string, string> dictionary2 = dictionary1;
                                                string key = num2.ToString() + "_" + str1;
                                                string[] strArray = new string[6]
                                                {
                      "Dedo:",
                      num2.ToString(),
                      "|Q ",
                      null,
                      null,
                      null
                                                };
                                                int num3 = fingerprint.RawImage.GetImageQuality();
                                                strArray[3] = num3.ToString();
                                                strArray[4] = " |N ";
                                                num3 = fingerprint.RawImage.GetNFIQScore();
                                                strArray[5] = num3.ToString();
                                                string str2 = string.Concat(strArray);
                                                dictionary2.Add(key, str2);
                                                if (index1 == 2)
                                                {
                                                    DataRowCollection rows = dataTable.Rows;
                                                    object[] objArray = new object[5]
                                                    {
                        (object) "pbpalma",
                        null,
                        null,
                        null,
                        null
                                                    };
                                                    num3 = 11;
                                                    objArray[1] = (object)(num3.ToString() + "_" + str1);
                                                    objArray[2] = (object)11;
                                                    objArray[3] = (object)fingerprint.RawImage.GetImageQuality();
                                                    objArray[4] = (object)fingerprint.RawImage.GetNFIQScore();
                                                    rows.Add(objArray);
                                                }
                                                if (index1 == 3)
                                                {
                                                    DataRowCollection rows = dataTable.Rows;
                                                    object[] objArray = new object[5]
                                                    {
                        (object) "pbpalma",
                        null,
                        null,
                        null,
                        null
                                                    };
                                                    num3 = 12;
                                                    objArray[1] = (object)(num3.ToString() + "_" + str1);
                                                    objArray[2] = (object)12;
                                                    objArray[3] = (object)fingerprint.RawImage.GetImageQuality();
                                                    objArray[4] = (object)fingerprint.RawImage.GetNFIQScore();
                                                    rows.Add(objArray);
                                                }
                                                ++num1;
                                            }
                                }
                            }
                            catch (Exception ex)
                            {
                                if (str1 == "")

                                    if (index1 == 2 || index1 == 3)
                                        dataTable.Rows.Add((object)"pbpalma", null, null, null, null);
                                    else
                                        dataTable.Rows.Add(new object[5]);
                                ++num1;
                                if (!(ex.Message == "Invalid image or unsupported image format."))
                                {
                                    if (!(ex.Message == "Found less fingers than expected."))
                                    {
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
                instance.Terminate();
                dt = dataTable;
                return dictionary1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return (Dictionary<string, string>)null;
            }
        }

        public Dictionary<string, string> enviarUno(Image pImagen, ref DataTable dt)
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Tipo");
                dataTable.Columns.Add("Nombre");
                dataTable.Columns.Add("Posicion");
                dataTable.Columns.Add("Q");
                dataTable.Columns.Add("N");
                Dictionary<string, string> dictionary1 = new Dictionary<string, string>();
                Innovatrics.ISegLib.ISegLib instance = Innovatrics.ISegLib.ISegLib.Instance;
                instance.Init();
                int num1 = 0;
                int num2 = 0;
                RawImage image = (RawImage)null;
                string str1 = "";
                Bitmap bitmap = (Bitmap)null;
                if (pImagen != null)
                {
                    image = RawImageExtension.ConvertImageToRaw(pImagen);
                    str1 = "pbDactilarDer.bmp";
                    bitmap = new Bitmap(pImagen);
                }
                int minFingersCount = 1;
                int maxFingersCount = 1;
                int expectedFingersCount = 1;
                int maxRotation = 1;
                int outWidth = 400;
                int outHeight = 500;
                byte maxValue = byte.MaxValue;
                try
                {
                    using (SegmentationResult segmentationResult = instance.SegmentFingerprints(image, expectedFingersCount, minFingersCount, maxFingersCount, maxRotation, SegmentationOptions.None, outWidth, outHeight, maxValue))
                    {
                        int globalAngle = segmentationResult.GlobalAngle;
                        int length = segmentationResult.Fingerprints.Length;
                        IList<SegmentationInfoDefines> segmentationFeedback = segmentationResult.SegmentationFeedback;
                        int num3;
                        if (instance.SegmentationFeedbackHasMissingFinger(segmentationFeedback))
                        {
                            Dictionary<string, string> dictionary2 = dictionary1;
                            int num4 = num2;
                            int num5 = num4 + 1;
                            num3 = num4;
                            string key1 = num3.ToString() + "_" + str1;
                            dictionary2.Add(key1, "La imagen enviada, no contiene todos los dedos.");
                            Dictionary<string, string> dictionary3 = dictionary1;
                            int num6 = num5;
                            num2 = num6 + 1;
                            num3 = num6;
                            string key2 = num3.ToString() + "_" + str1;
                            string str2 = "Desaparecido " + instance.GetFingerName(segmentationFeedback);
                            dictionary3.Add(key2, str2);
                        }
                        if (!segmentationFeedback.Contains(SegmentationInfoDefines.SegmentationInfoLeftHand))
                        { }
                        if (!segmentationFeedback.Contains(SegmentationInfoDefines.SegmentationInfoRightHand))
                        { }
                        for (int index = 0; index < segmentationResult.Fingerprints.Length; ++index)
                        {
                            ++num2;
                            SegmentedFingerprint fingerprint = segmentationResult.Fingerprints[index];
                            Dictionary<string, string> dictionary2 = dictionary1;
                            string key = num2.ToString() + "_" + str1;
                            string[] strArray = new string[6]
                            {
                "Dedo:",
                num2.ToString(),
                "|Q ",
                null,
                null,
                null
                            };
                            num3 = fingerprint.RawImage.GetImageQuality();
                            strArray[3] = num3.ToString();
                            strArray[4] = " |N ";
                            num3 = fingerprint.RawImage.GetNFIQScore();
                            strArray[5] = num3.ToString();
                            string str2 = string.Concat(strArray);
                            dictionary2.Add(key, str2);
                            DataRowCollection rows = dataTable.Rows;
                            object[] objArray = new object[5]
                            {
                (object) "pbHuella",
                null,
                null,
                null,
                null
                            };
                            num3 = 11;
                            objArray[1] = (object)(num3.ToString() + "_" + str1);
                            objArray[2] = (object)11;
                            objArray[3] = (object)fingerprint.RawImage.GetImageQuality();
                            objArray[4] = (object)fingerprint.RawImage.GetNFIQScore();
                            rows.Add(objArray);
                            ++num1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (str1 == "")
                    { }
                    dataTable.Rows.Add((object)"pbHuella", null, null, null, null);
                    int num3 = num1 + 1;
                    if (!(ex.Message == "Invalid image or unsupported image format."))
                    {
                        if (!(ex.Message == "Found less fingers than expected."))
                        { }
                    }
                }
                instance.Terminate();
                dt = dataTable;
                return dictionary1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return (Dictionary<string, string>)null;
            }
        }

        public byte[] imgToByteArray(Image img)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                img.Save((Stream)memoryStream, img.RawFormat);
                return memoryStream.ToArray();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(215, 125);
            this.Name = "SegmentacionRod";
            this.Text = "Segmentacion";
            this.Load += new EventHandler(this.Segmentacion_Load);
            this.ResumeLayout(false);
        }
    }
}
