using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Innovatrics.IFace;


namespace ENROLL.Helpers
{
    class MyIface
    {
        private string detectionMode = "accurate";
        private Bitmap originalImage;
        public double FaceScore { get; set; }
        public Image CroppedImg { get; set; }

        private IFace iface;

        //primero agarro con un enum todos los valores despues los casteo a un array de FaceFeatureId
        FaceFeatureId[] featureIds = Enum.GetValues(typeof(FaceFeatureId)).Cast<FaceFeatureId>().ToArray<FaceFeatureId>();
        //creo un diccionario de datos para key value para mostrar la tabla
        Dictionary<FaceFeatureId, string> featureToName = new Dictionary<FaceFeatureId, string>()
        {
            { FaceFeatureId.RightEyeOuterCorner, "Esquina exterior del ojo derecho" },
            { FaceFeatureId.RightEyeCentre, "Centro del ojo derecho" },
            { FaceFeatureId.RightEyeInnerCorner, "Esquina interior del ojo derecho" },
            { FaceFeatureId.LeftEyeInnerCorner, "Esquina interior del ojo izquierdo" },
            { FaceFeatureId.LeftEyeCentre, "Centro del ojo izquierdo" },
            { FaceFeatureId.LeftEyeOuterCorner, "Esquina exterior del ojo izquierdo" },
            { FaceFeatureId.NoseTip, "Punta de la nariz" },
            { FaceFeatureId.MouthRightCorner, "Boca esquina derecha" },
            { FaceFeatureId.MouthCenter, "Centro de la boca" },
            { FaceFeatureId.MouthLeftCorner, "Boca esquina izquierda" },
            { FaceFeatureId.MouthUpperEdge, "Borde superior de la boca" },
            { FaceFeatureId.MouthLowerEdge, "Boca borde inferior" },
            { FaceFeatureId.RightEyeBrowOuterEnd, "Extremo exterior de la ceja derecha" },
            { FaceFeatureId.RightEyeBrowInnerEnd, "Extremo interno de la ceja derecha" },
            { FaceFeatureId.LeftEyeBrowInnerEnd, "Extremo interno de la ceja izquierda" },
            { FaceFeatureId.LeftEyeBrowOuterEnd, "Extremo exterior de la ceja izquierda" },
            { FaceFeatureId.FaceRightEdge, "Cara borde derecho" },
            { FaceFeatureId.FaceChinTip, "Punta de la barbilla" },
            { FaceFeatureId.FaceLeftEdge, "Cara borde izquierdo" }
        };
        public MyIface()
        {
            iface = IFace.Instance;
            try
            {
                iface.Init();
            }
            //catch (Exception LicenseAlreadyInitialized ex)
            //{

            //}
            catch (Exception ex)
            {

                if (ex.Message != "License was already initialized")
                    MessageBox.Show("Licencia no activada o caducada por favor contactese con Sistemas o reinicie el programa err:code " + ex.Message, "Error");

                //   Console.WriteLine(ex.Message);

            }
        }
        public void Terminate()
        {
            iface.Terminate();
        }
        public DataTable GetFaceData(Bitmap originalImage)
        {
            this.originalImage = originalImage;
            FaceHandler faceHandler = new FaceHandler();
            DataTable tabla = new DataTable();
            faceHandler.SetParam("fd.speed_accuracy_mode", detectionMode);
            faceHandler.SetParam("hss.matting_type", "matting_global");
            faceHandler.SetParam("img.face_crop_full_frontal_extended_scale", "50");
            Face[] faces = faceHandler.DetectFaces(originalImage, (float)40, (float)1000, (int)1);

            if (faces.Length == 0)
            {
                MessageBox.Show("No se Pudo detectar ningun rostro en la imagen", "Error");
                return tabla;
            }

            if (faces.Length > 1)
            {
                MessageBox.Show("Error", "Mas de un rostro detectado en la imagen");
                return tabla;
            }
            //obtener la cara
            Face face = faces[0];
            //saco todos los features con el metodo GetFeatures
            FaceFeature[] faceFeatures = face.GetFeatures(featureIds, faceHandler);
            // creo una tabla para almacenar los valores
            FaceScore = (double)(face.GetBasicInfo(faceHandler).Score / 10000f * 100f);
            // int? quality = CalculateQuality(face, faceFeatures);

            CroppedImg = face.GetCropImage(FaceCropMethod.TokenFrontal, faceHandler);
            // Console.WriteLine(quality);

            tabla.Columns.Add("Tipo");
            tabla.Columns.Add("Detalle");
            tabla.Columns.Add("Respuesta");
            tabla.Columns.Add("Valor");
            tabla.Columns.Add("Error");
            //  DataRow fila;
            foreach (FaceFeature faceFeature in faceFeatures)
            {
                // string test = faceFeature.ToString();
                if (faceFeature != null && featureToName.ContainsKey(faceFeature.FeatureID))
                {
                    DataRow fila1 = tabla.NewRow();
                    fila1["Tipo"] = "Rasgos";
                    fila1["Detalle"] = featureToName[faceFeature.FeatureID];
                    if (faceFeature.Score.HasValue)
                    {
                        fila1["Respuesta"] = "Confiable" + FaceScore;
                        fila1["Valor"] = "2";
                    }
                    else
                    {
                        fila1["Respuesta"] = "No Confiable";
                        fila1["Valor"] = (int)FaceScore + "%";
                    }
                    tabla.Rows.Add(fila1);
                }
            }

            string Valor;
            /*"Distancia del ojo"*/

            DataRow fila = tabla.NewRow(); double? ValorRes = null;
            try { ValorRes = face.GetAttribute(FaceAttributeId.EyeDistance, faceHandler); }
            catch (Exception ex) { fila["Error"] = ex.Message; }
            fila["Tipo"] = "Atributos";
            fila["Detalle"] = "Distancia del ojo";
            fila["Respuesta"] = ValorRes;
            fila["Valor"] = ValorRes > 0 ? "2 Bueno" : "0 Malo";
            tabla.Rows.Add(fila);

            /* --"Nitidez"-- */
            fila = tabla.NewRow(); ValorRes = null;
            try { ValorRes = (double)face.GetAttribute(FaceAttributeId.Sharpness, faceHandler); }
            catch (Exception ex) { fila["Error"] = ex.Message; }
            fila["Tipo"] = "Atributos";
            fila["Detalle"] = "Nitidez";
            fila["Respuesta"] = ValorRes;
            fila["Valor"] = ValorRes <= -3000 ? "0 Malo" : (ValorRes <= 3000 ? "1 Regular" : "2 Bueno");
            tabla.Rows.Add(fila);

            /* --"Brillo"-- */
            fila = tabla.NewRow(); ValorRes = null;
            try { ValorRes = (double)face.GetAttribute(FaceAttributeId.Brightness, faceHandler); }
            catch (Exception ex) { fila["Error"] = ex.Message; }
            fila["Tipo"] = "Atributos";
            fila["Detalle"] = "Brillo";
            fila["Respuesta"] = ValorRes;
            fila["Valor"] = ValorRes <= -5000 ? "0 Malo" : (ValorRes <= 5000 ? "1 Regular" : "2 Bueno");
            tabla.Rows.Add(fila);

            /* --"Contraste"-- */
            fila = tabla.NewRow(); ValorRes = null;
            try { ValorRes = (double)face.GetAttribute(FaceAttributeId.Contrast, faceHandler); }
            catch (Exception ex) { fila["Error"] = ex.Message; }
            fila["Tipo"] = "Atributos";
            fila["Detalle"] = "Contraste";
            fila["Respuesta"] = ValorRes;
            fila["Valor"] = ValorRes <= -3000 ? "0 Malo" : (ValorRes <= 3000 ? "1 Regular" : "2 Bueno");
            tabla.Rows.Add(fila);


            /* --"Nivel de intensidad"-- */
            fila = tabla.NewRow(); ValorRes = null;
            try { ValorRes = (double)face.GetAttribute(FaceAttributeId.UniqueIntensityLevels, faceHandler); }
            catch (Exception ex) { fila["Error"] = ex.Message; }
            fila["Tipo"] = "Atributos";
            fila["Detalle"] = "Nivel de intensidad";
            fila["Respuesta"] = ValorRes;
            fila["Valor"] = ValorRes <= -3000 ? "0 Malo" : (ValorRes <= 0 ? "1 Regular" : "2 Bueno");
            tabla.Rows.Add(fila);


            /* --Sombra"-- */
            fila = tabla.NewRow(); ValorRes = null;
            try { ValorRes = (double)face.GetAttribute(FaceAttributeId.Shadow, faceHandler); }
            catch (Exception ex) { fila["Error"] = ex.Message; }
            fila["Tipo"] = "Atributos";
            fila["Detalle"] = "Sombra";
            fila["Respuesta"] = ValorRes;
            fila["Valor"] = ValorRes <= -3000 ? "0 Malo" : (ValorRes <= 0 ? "1 Regular" : "2 Bueno");
            tabla.Rows.Add(fila);


            /* --Sombra en nariz"-- */
            fila = tabla.NewRow(); ValorRes = null;
            try { ValorRes = (double)face.GetAttribute(FaceAttributeId.NoseShadow, faceHandler); }
            catch (Exception ex) { fila["Error"] = ex.Message; }
            fila["Tipo"] = "Atributos";
            fila["Detalle"] = "Sombra en nariz";
            fila["Respuesta"] = ValorRes;
            fila["Valor"] = ValorRes <= -3000 ? "0 Malo" : (ValorRes <= 0 ? "1 Regular" : "2 Bueno");
            tabla.Rows.Add(fila);


            /* --  Especularidad (Focos)  "-- */
            fila = tabla.NewRow(); ValorRes = null;
            try { ValorRes = (double)face.GetAttribute(FaceAttributeId.Specularity, faceHandler); }
            catch (Exception ex) { fila["Error"] = ex.Message; }
            fila["Tipo"] = "Atributos";
            fila["Detalle"] = "Especularidad (Focos)";
            fila["Respuesta"] = ValorRes;
            fila["Valor"] = ValorRes <= -3000 ? "0 Malo" : (ValorRes <= 0 ? "1 Regular" : "2 Bueno");
            tabla.Rows.Add(fila);


            /* --  Mirada  -- */
            fila = tabla.NewRow(); ValorRes = null;
            try { ValorRes = (double)face.GetAttribute(FaceAttributeId.EyeGaze, faceHandler); }
            catch (Exception ex) { fila["Error"] = ex.Message; }
            fila["Tipo"] = "Atributos";
            fila["Detalle"] = "Mirada";
            fila["Respuesta"] = ValorRes;
            fila["Valor"] = ValorRes <= -3000 ? "0 Malo" : (ValorRes <= 0 ? "1 Regular" : "2 Bueno");
            tabla.Rows.Add(fila);

            /* --  Inclinacion Cabeza  -- */
            fila = tabla.NewRow(); ValorRes = null;
            try { ValorRes = (double)face.GetAttribute(FaceAttributeId.Roll, faceHandler); }
            catch (Exception ex) { fila["Error"] = ex.Message; }
            fila["Tipo"] = "Atributos";
            fila["Detalle"] = "Inclinación Horizontal Cabeza";
            fila["Respuesta"] = ValorRes;
            fila["Valor"] = ValorRes <= -7500 ? "0 Demasiado Inclinado a la Izquierda" : (ValorRes <= -5000 ? "1 Regularmente Inclinado a la Izquierda" : (ValorRes <= 5000 ? "2 Bueno" : (ValorRes <= 7500 ? "1 Regularmente Inclinado a la Derecha" : "0 Demasiado Inclinado a la Derecha")));
            tabla.Rows.Add(fila);

            /* -- Cabeceo  -- */
            fila = tabla.NewRow(); ValorRes = null;
            try { ValorRes = (double)face.GetAttribute(FaceAttributeId.Pitch, faceHandler); }
            catch (Exception ex) { fila["Error"] = ex.Message; }
            fila["Tipo"] = "Atributos";
            fila["Detalle"] = "Giro Vertical Cabeza";
            fila["Respuesta"] = ValorRes;
            fila["Valor"] = ValorRes <= -7500 ? "0 Demasiado Girado a Abajo" : (ValorRes <= -5000 ? "1 Girado Inclinado Abajo" : (ValorRes <= 5000 ? "2 Bueno" : (ValorRes <= 7500 ? "1 Regularmente Girado a la Arriba" : "0 Demasiado Girado a la Arriba")));
            tabla.Rows.Add(fila);

            /* -- "Giro de la cabeza"  -- */
            fila = tabla.NewRow(); ValorRes = null;
            try { ValorRes = (double)face.GetAttribute(FaceAttributeId.Yaw, faceHandler); }
            catch (Exception ex) { fila["Error"] = ex.Message; }
            fila["Tipo"] = "Atributos";
            fila["Detalle"] = "Giro Horizontal Cabeza";
            fila["Respuesta"] = ValorRes;
            fila["Valor"] = ValorRes <= -5000 ? "0 Demasiado Girado Izquierda" : (ValorRes <= -2000 ? "1 Regularmente Girado Izquierda" : (ValorRes <= 2000 ? "2 Bueno" : (ValorRes <= 5000 ? "1 Regularmente Girado a la Derecha" : "0 Demasiado Girado a la Derecha")));
            tabla.Rows.Add(fila);


            /*  - Estado ojo izquierdo -  ***/
            fila = tabla.NewRow(); ValorRes = null;
            try { ValorRes = (double)face.GetAttribute(FaceAttributeId.EyeStatusL, faceHandler); }
            catch (Exception ex) { fila["Error"] = ex.Message; }
            fila["Tipo"] = "Atributos";
            fila["Detalle"] = "Estado ojo izquierdo";
            fila["Respuesta"] = ValorRes;
            fila["Valor"] = ValorRes <= -1000 ? "0 Malo" : (ValorRes <= 1000 ? "1 Regular" : "2 Bueno");
            tabla.Rows.Add(fila);

            /*  -  Estado ojo derecho -  ***/
            fila = tabla.NewRow(); ValorRes = null;
            try { ValorRes = (double)face.GetAttribute(FaceAttributeId.EyeStatusR, faceHandler); }
            catch (Exception ex) { fila["Error"] = ex.Message; }
            fila["Tipo"] = "Atributos";
            fila["Detalle"] = "Estado ojo derecho";
            fila["Respuesta"] = ValorRes;
            fila["Valor"] = ValorRes <= -1000 ? "0 Malo" : (ValorRes <= 1000 ? "1 Regular" : "2 Bueno");
            tabla.Rows.Add(fila);

            /*  -  Gafas -  ***/
            fila = tabla.NewRow(); ValorRes = null;
            try { ValorRes = (double)face.GetAttribute(FaceAttributeId.GlassStatus, faceHandler); }
            catch (Exception ex) { fila["Error"] = ex.Message; }
            finally
            {
                if (ValorRes <= 1100)
                {
                    Valor = "9 NO";
                }
                else
                {
                    Valor = "9 SI";
                }
                fila["Tipo"] = "Atributos";
                fila["Detalle"] = "Gafas";
                fila["Respuesta"] = ValorRes;
                fila["Valor"] = Valor;
                tabla.Rows.Add(fila);
            }

            /* ** si tiene lentes  **/
            if (Valor == "9 SI")
            {
                fila = tabla.NewRow(); ValorRes = null;
                try { ValorRes = (double)face.GetAttribute(FaceAttributeId.HeavyFrame, faceHandler); }
                catch (Exception ex) { fila["Error"] = ex.Message; }
                finally
                {
                    fila["Tipo"] = "Atributos";
                    fila["Detalle"] = "Lentes Montura Pesada";
                    fila["Respuesta"] = ValorRes;
                    fila["Valor"] = ValorRes <= -3000 ? "0 Muy Resaltante" : "2 Aceptable";
                    tabla.Rows.Add(fila);
                }
            }

            /* ** Estado Boca  **/
            fila = tabla.NewRow(); ValorRes = null;
            try { ValorRes = (double)face.GetAttribute(FaceAttributeId.MouthStatus, faceHandler); }
            catch (Exception ex) { fila["Error"] = ex.Message; }
            fila["Tipo"] = "Atributos";
            fila["Detalle"] = "Estado Boca";
            fila["Respuesta"] = ValorRes;
            fila["Valor"] = ValorRes <= -2000 ? "0 Malo Boca abierta " : (ValorRes <= 2000 ? "1 Regular" : "2 Bueno");
            tabla.Rows.Add(fila);


            /* ** Fondo  **/
            fila = tabla.NewRow(); ValorRes = null;
            try { ValorRes = (double)face.GetAttribute(FaceAttributeId.BackgroundUniformity, faceHandler); }
            catch (Exception ex) { fila["Error"] = ex.Message; }
            fila["Tipo"] = "Atributos";
            fila["Detalle"] = "Fondo";
            fila["Respuesta"] = ValorRes;
            fila["Valor"] = ValorRes <= -3000 ? "0 Malo" : (ValorRes <= 3000 ? "1 Regular" : "2 Bueno");
            tabla.Rows.Add(fila);


            /* ** Ojo Rojo Izquierdo  **/
            fila = tabla.NewRow(); ValorRes = null;
            try { ValorRes = (double)face.GetAttribute(FaceAttributeId.RedEyeL, faceHandler); }
            catch (Exception ex) { fila["Error"] = ex.Message; }
            fila["Tipo"] = "Atributos";
            fila["Detalle"] = "Ojo Rojo Izquierdo";
            fila["Respuesta"] = ValorRes;
            fila["Valor"] = ValorRes <= -2000 ? "2 Bueno" : (ValorRes <= 2000 ? "1 Regular" : "0  Malo");
            tabla.Rows.Add(fila);


            /* **   "Ojo Rojo Derecho    **/
            fila = tabla.NewRow(); ValorRes = null;
            try { ValorRes = (double)face.GetAttribute(FaceAttributeId.RedEyeR, faceHandler); }
            catch (Exception ex) { fila["Error"] = ex.Message; }
            fila["Tipo"] = "Atributos";
            fila["Detalle"] = "Ojo Rojo Derecho";
            fila["Respuesta"] = ValorRes;
            fila["Valor"] = ValorRes <= -2000 ? "2 Bueno" : (ValorRes <= 2000 ? "1 Regular" : "0 Malo");
            tabla.Rows.Add(fila);


            /* **   Género    **/
            fila = tabla.NewRow(); ValorRes = null;
            try { ValorRes = (double)face.GetAttribute(FaceAttributeId.Gender, faceHandler); }
            catch (Exception ex) { fila["Error"] = ex.Message; }
            fila["Tipo"] = "Atributos";
            fila["Detalle"] = "Género";
            fila["Respuesta"] = ValorRes;
            fila["Valor"] = ValorRes < 0 ? "9 Masculino" : "9 Femenino";
            tabla.Rows.Add(fila);

            /* **   Edad    **/
            fila = tabla.NewRow(); ValorRes = null;
            try { ValorRes = (double)face.GetAttribute(FaceAttributeId.Age, faceHandler); }
            catch (Exception ex) { fila["Error"] = ex.Message; }
            fila["Tipo"] = "Atributos";
            fila["Detalle"] = "Edad";
            fila["Respuesta"] = ValorRes;
            fila["Valor"] = string.Concat("9 ", Convert.ToString(Convert.ToInt32(ValorRes)));
            tabla.Rows.Add(fila);


            return tabla;

        }

        /**/

    }
}
