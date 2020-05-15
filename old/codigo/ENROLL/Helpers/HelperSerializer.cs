using ENROLL.Model;
using ENROLL.Core;
using Datys.Enroll.Core;
using GBMSGUI_NET;
using JPEG_NET_WRAPPER;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Linq;
using System.Xml.Serialization;
using WSQPACK_NET_WRAPPER;

namespace ENROLL.Helpers
{
    public class HelperSerializer
    {
        private static Random random;

        static HelperSerializer()
        {
            HelperSerializer.random = new Random();
        }

        public HelperSerializer()
        {
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            Image image;
            Image returnImage = null;
            try
            {
                if (byteArrayIn != null)
                {
                    returnImage = Image.FromStream(new MemoryStream(byteArrayIn));
                }
                image = returnImage;
            }
            catch (Exception e)
            {
                Console.WriteLine($"ArithmeticException Handler: {e}");
                image = returnImage;
            }
            return image;
        }

        public Image CambiarTamanoImagen(Image pImagen, int pAncho_Alto)
        {
            Image image;
            int ancho = pImagen.Width;
            int alto = pImagen.Height;
            double ope = (double)(alto - ancho);
            double por_dimension = 0;
            double nuevo_ancho = 0;
            double nuevo_alto = 0;
            if (ope > 0)
            {
                if (alto > pAncho_Alto)
                {
                    por_dimension = (double)ancho / (double)alto;
                    nuevo_alto = (double)pAncho_Alto;
                    nuevo_ancho = nuevo_alto * por_dimension;
                }
            }
            else if (ancho > pAncho_Alto)
            {
                por_dimension = (double)alto / (double)ancho;
                nuevo_ancho = (double)pAncho_Alto;
                nuevo_alto = nuevo_ancho * por_dimension;
            }
            if (por_dimension <= 0)
            {
                image = pImagen;
            }
            else
            {
                Bitmap vBitmap = new Bitmap(Convert.ToInt32(nuevo_ancho), Convert.ToInt32(nuevo_alto));
                using (Graphics vGraphics = Graphics.FromImage(vBitmap))
                {
                    vGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    vGraphics.DrawImage(pImagen, 0, 0, Convert.ToInt32(nuevo_ancho), Convert.ToInt32(nuevo_alto));
                }
                image = vBitmap;
            }
            return image;
        }

        public byte[] CompressToJP2(Bitmap bmpImage, int Rate)
        {
            int ImageSizeX = bmpImage.Width;
            int ImageSizeY = bmpImage.Height;
            byte[] RawImage = new byte[ImageSizeX * ImageSizeY];
            GBMSGUI.BitmapToRawImage(bmpImage, RawImage);
            NW_GBJPEG_JPX_Encode JP2Encoder = new NW_GBJPEG_JPX_Encode(RawImage, (uint)ImageSizeX, (uint)ImageSizeY)
            {
                BitPerPixel = NW_GBJPEG_PIXEL_DEPTH_DEFINITIONS.NW_GBJPEG_8_BITS_PER_PIXEL,
                CompressionRate = (ushort)Rate,
                CompressionAlgorithm = NW_GBJPEG_ALGORITHMS_DEFINITIONS.NW_GBJPEG_COMP_ALG_JPEG2000
            };
            return JP2Encoder.Encoded;
        }

        public byte[] CompressToJPEG(Bitmap bmpImage, int Quality)
        {
            int ImageSizeX = bmpImage.Width;
            int ImageSizeY = bmpImage.Height;
            byte[] RawImage = new byte[ImageSizeX * ImageSizeY];
            GBMSGUI.BitmapToRawImage(bmpImage, RawImage);
            NW_GBJPEG_JPX_Encode JPEGEncoder = new NW_GBJPEG_JPX_Encode(RawImage, (uint)ImageSizeX, (uint)ImageSizeY)
            {
                BitPerPixel = NW_GBJPEG_PIXEL_DEPTH_DEFINITIONS.NW_GBJPEG_8_BITS_PER_PIXEL,
                CompressionQuality = (ushort)Quality,
                CompressionAlgorithm = NW_GBJPEG_ALGORITHMS_DEFINITIONS.NW_GBJPEG_COMP_ALG_JPG
            };
            return JPEGEncoder.Encoded;
        }

        public byte[] CompressToWSQ(Bitmap bmpImage, double Rate, int Dpi)
        {
            int ImageSizeX = bmpImage.Width;
            int ImageSizeY = bmpImage.Height;
            byte[] RawImage = new byte[ImageSizeX * ImageSizeY];
            GBMSGUI.BitmapToRawImage(bmpImage, RawImage);
            NW_WSQPACK_Compress WSQEncoder = new NW_WSQPACK_Compress(RawImage, ImageSizeX, ImageSizeY)
            {
                BitPerPixel = NW_WSQPACK_PIXEL_DEPTH_DEFINITIONS.NW_WSQPACK_8_BITS_PER_PIXEL,
                CompressionRate = (float)Rate,
                ImageResolution = Dpi
            };
            return WSQEncoder.Encoded;
        }

        public T Deserialize<T>(string input)
        where T : class
        {
            T t;
            XmlSerializer ser = new XmlSerializer(typeof(T));
            using (StringReader sr = new StringReader(input))
            {
                t = (T)ser.Deserialize(sr);
            }
            return t;
        }

        public CapturedPerson DeserializeEpd(string pRuta)
        {
            FileStream fs = new FileStream((new FileInfo(pRuta)).FullName, FileMode.Open);
            CapturedPerson PersonaCapturada = new CapturedPerson();
            BinaryFormatter Novo = new BinaryFormatter();
            try
            {
                try
                {
                    PersonaCapturada = (CapturedPerson)Novo.Deserialize(fs);
                }
                catch (SerializationException e)
                {
                    if (e.InnerException != null)
                    {
                        string err = e.InnerException.Message;
                    }
                    throw;
                }
            }
            finally
            {
                fs.Close();
            }
            return PersonaCapturada;


        }

        public object DeserializeObject(string pRuta)
        {
            FileStream fs = new FileStream((new FileInfo(pRuta)).FullName, FileMode.Open);
            object PersonaCapturada = new object();
            try
            {

                try
                {

                    PersonaCapturada = (new BinaryFormatter()).Deserialize(fs);
                }
                catch (SerializationException serializationException)
                {
                    Console.WriteLine($"ArithmeticException Handler: {serializationException}");
                    throw;
                }
            }
            finally
            {

                fs.Close();
            }
            return PersonaCapturada;
        }

        public void GeneraUsuariosXml(string vMensaje)
        {
            DataTable dt = (new CoreFilesClient()).ObtenerUsuarios(ref vMensaje);
            Usuarios _usuarios = new Usuarios()
            {
                ListaUsuario = new List<Usuario>()
            };
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    _usuarios.ListaUsuario.Add(new Usuario()
                    {
                        Identificacion = dr["numero_documento"].ToString(),
                        Complemento = dr["complemento"].ToString(),
                        PrimerNombre = dr["primer_nombre"].ToString(),
                        SegundoNombre = dr["segundo_nombre"].ToString(),
                        PrimerApellido = dr["primer_apellido"].ToString(),
                        SegundoApellido = dr["segundo_apellido"].ToString(),
                        txtUsuario = dr["usuario"].ToString(),
                        Pass = dr["password"].ToString(),
                        Unidad = dr["unidad"].ToString()
                    });
                }
                (new XDocument(new XDeclaration("1.0", "UTF - 8", "yes"), new object[] { new XElement("Usuarios",
                    from OrderDet in _usuarios.ListaUsuario
                    select new XElement("ListaUsuario", new XElement("Usuario", new object[] { new XElement("Identificacion", OrderDet.Identificacion), new XElement("PrimerNombre", OrderDet.PrimerNombre), new XElement("SegundoNombre", OrderDet.SegundoNombre), new XElement("PrimerApellido", OrderDet.PrimerApellido), new XElement("SegundoApellido", OrderDet.SegundoApellido), new XElement("txtUsuario", OrderDet.txtUsuario), new XElement("Pass", OrderDet.Pass), new XElement("Unidad", OrderDet.Unidad) }))) })).Save("user/usuarios.xml");
            }
        }

        public void GeneraUsuariosXml(string vMensaje, Usuarios pNuevoUsuario)
        {
            (new XDocument(new XDeclaration("1.0", "UTF - 8", "yes"), new object[] { new XElement("Usuarios",
                from OrderDet in pNuevoUsuario.ListaUsuario
                select new XElement("ListaUsuario", new XElement("Usuario", new object[] { new XElement("Identificacion", OrderDet.Identificacion), new XElement("Complemento", OrderDet.Complemento), new XElement("PrimerNombre", OrderDet.PrimerNombre), new XElement("SegundoNombre", OrderDet.SegundoNombre), new XElement("PrimerApellido", OrderDet.PrimerApellido), new XElement("SegundoApellido", OrderDet.SegundoApellido), new XElement("txtUsuario", OrderDet.txtUsuario), new XElement("Pass", OrderDet.Pass), new XElement("UsuarioCreacion", OrderDet.UsuarioCreacion), new XElement("UsuarioModificacion", OrderDet.UsuarioModificacion), new XElement("FechaCreacion", (object)OrderDet.FechaCreacion), new XElement("FechaModificacion", (object)OrderDet.FechaModificacion), new XElement("Unidad", OrderDet.Unidad) }))) })).Save("user/usuarios.xml");
        }

        public byte[] ImagenToBMPByteArray(Bitmap bitmap)
        {
            byte[] result = null;
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Bmp);
                result = stream.ToArray();
            }
            return result;
        }

        public byte[] ImagenToJPGByteArray(Bitmap bitmap)
        {
            byte[] result = null;
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Jpeg);
                result = stream.ToArray();
            }
            return result;
        }

        public byte[] ImagenToPNGByteArray(Bitmap bitmap)
        {
            byte[] result = null;
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Png);
                result = stream.ToArray();
            }
            return result;
        }

        public byte[] ImageToByteArray(Image img)
        {
            byte[] numArray;
            try
            {
                numArray = (byte[])(new ImageConverter()).ConvertTo(img, typeof(byte[]));
            }
            catch
            {
                numArray = (byte[])(new ImageConverter()).ConvertTo(img, typeof(byte[]));
            }
            return numArray;
        }

        public string RandomString(int length)
        {
            string str = new string((
                from s in Enumerable.Repeat<string>("0123456789", length)
                select s[HelperSerializer.random.Next(s.Length)]).ToArray<char>());
            return str;
        }

        public Bitmap RedimensionarBitmap(Bitmap pImagen, int pAncho, int pAlto)
        {
            Bitmap vImagen = new Bitmap(pAncho, pAlto);
            using (Graphics vGrafico = Graphics.FromImage(vImagen))
            {
                vGrafico.DrawImage(pImagen, new Rectangle(0, 0, pAncho, pAlto), new Rectangle(0, 0, pImagen.Width, pImagen.Height), GraphicsUnit.Pixel);
            }
            return vImagen;
        }

        public byte[] RedimensionarByteArray(byte[] pImagen, int pAncho)
        {
            byte[] vImagen;
            int vAlto;
            int vAncho;
            double vRadio;
            using (MemoryStream vIniciaMemoryStream = new MemoryStream())
            {
                using (MemoryStream vNuevoMemoryStream = new MemoryStream())
                {
                    vIniciaMemoryStream.Write(pImagen, 0, (int)pImagen.Length);
                    Bitmap vBitmap = new Bitmap(vIniciaMemoryStream);
                    if (vBitmap.Height <= vBitmap.Width)
                    {
                        vAncho = pAncho;
                        vRadio = (double)((double)pAncho / (double)vBitmap.Width);
                        vAlto = (int)(vRadio * (double)vBitmap.Height);
                    }
                    else
                    {
                        vAlto = pAncho;
                        vRadio = (double)((double)pAncho / (double)vBitmap.Height);
                        vAncho = (int)(vRadio * (double)vBitmap.Width);
                    }
                    Bitmap vNuevoBitmap = this.RedimensionarBitmap(vBitmap, vAncho, vAlto);
                    vNuevoBitmap.Save(vNuevoMemoryStream, ImageFormat.Jpeg);
                    vImagen = vNuevoMemoryStream.ToArray();
                }
            }
            return vImagen;
        }

        public byte[] RedimensionarByteArrayBMP(byte[] pImagen, int pAncho)
        {
            byte[] vImagen;
            int vAlto;
            int vAncho;
            double vRadio;
            using (MemoryStream vIniciaMemoryStream = new MemoryStream())
            {
                using (MemoryStream vNuevoMemoryStream = new MemoryStream())
                {
                    vIniciaMemoryStream.Write(pImagen, 0, (int)pImagen.Length);
                    Bitmap vBitmap = new Bitmap(vIniciaMemoryStream);
                    if (vBitmap.Height <= vBitmap.Width)
                    {
                        vAncho = pAncho;
                        vRadio = (double)((double)pAncho / (double)vBitmap.Width);
                        vAlto = (int)(vRadio * (double)vBitmap.Height);
                    }
                    else
                    {
                        vAlto = pAncho;
                        vRadio = (double)((double)pAncho / (double)vBitmap.Height);
                        vAncho = (int)(vRadio * (double)vBitmap.Width);
                    }
                    Bitmap vNuevoBitmap = this.RedimensionarBitmap(vBitmap, vAncho, vAlto);
                    vNuevoBitmap.Save(vNuevoMemoryStream, ImageFormat.Bmp);
                    vImagen = vNuevoMemoryStream.ToArray();
                }
            }
            return vImagen;
        }

        public Image RedimensionarImagen(Image imagen, int imageAncho, int imagenAlto, int imagenResolucion)
        {
            using (Bitmap imagenBitmap = new Bitmap(imageAncho, imagenAlto, imagen.PixelFormat))
            {
                imagenBitmap.SetResolution((float)imagenResolucion, (float)imagenResolucion);
                using (Graphics imagenGraphics = Graphics.FromImage(imagenBitmap))
                {
                    imagenGraphics.SmoothingMode = SmoothingMode.AntiAlias;
                    imagenGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    imagenGraphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    imagenGraphics.DrawImage(imagen, new Rectangle(0, 0, imageAncho, imagenAlto), new Rectangle(0, 0, imagen.Width, imagen.Height), GraphicsUnit.Pixel);
                    MemoryStream imagenMemoryStream = new MemoryStream();
                    imagenBitmap.Save(imagenMemoryStream, ImageFormat.Jpeg);
                    imagen = Image.FromStream(imagenMemoryStream);
                }
            }
            return imagen;
        }

        public Image RedimensionarImagen(Image imagen, int imageAncho, int imagenAlto, int imagenResolucion, PixelFormat pixelFormat)
        {
            using (Bitmap imagenBitmap = new Bitmap(imageAncho, imagenAlto, pixelFormat))
            {
                imagenBitmap.SetResolution((float)imagenResolucion, (float)imagenResolucion);
                using (Graphics imagenGraphics = Graphics.FromImage(imagenBitmap))
                {
                    imagenGraphics.SmoothingMode = SmoothingMode.AntiAlias;
                    imagenGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    imagenGraphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    imagenGraphics.DrawImage(imagen, new Rectangle(0, 0, imageAncho, imagenAlto), new Rectangle(0, 0, imagen.Width, imagen.Height), GraphicsUnit.Pixel);
                    MemoryStream imagenMemoryStream = new MemoryStream();
                    imagenBitmap.Save(imagenMemoryStream, ImageFormat.Jpeg);
                    imagen = Image.FromStream(imagenMemoryStream);
                }
            }
            return imagen;
        }

        public string Serialize<T>(T ObjectToSerialize)
        {
            string str;
            XmlSerializer xmlSerializer = new XmlSerializer(ObjectToSerialize.GetType());
            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, ObjectToSerialize);
                str = textWriter.ToString();
            }
            return str;
        }

        public void SerializeEpd(CapturedPerson pCapturedPerson, string pRuta)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(pRuta, FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, pCapturedPerson);
            stream.Close();
        }

        public void SerializeObject(object pObjeto, string pRuta)
        {
            if (pObjeto != null)
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(pRuta, FileMode.Create, FileAccess.Write);
                formatter.Serialize(stream, pObjeto);
                stream.Close();
            }
        }

        public bool SerializeObjectBool(object pObjeto, string pRuta)
        {
            bool resul = false;
            if (pObjeto != null)
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(pRuta, FileMode.Create, FileAccess.Write);
                formatter.Serialize(stream, pObjeto);
                stream.Close();
                resul = true;
            }
            return resul;
        }
    }
}