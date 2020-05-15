using Innovatrics.IFace;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace BIODV
{
	public class Icao
	{
		public Icao()
		{
		}

		private void CargaPestanaFotoFrontal()
		{
		}

		private static void CheckImageFile(string imagePath, ref string respuesta)
		{
			respuesta = "1";
			if (!File.Exists(imagePath))
			{
				respuesta = "0";
			}
		}

		public static DataTable GetDistinctRecords(DataTable dt, string[] Columns)
		{
			DataTable dtUniqRecords = new DataTable();
			return dt.DefaultView.ToTable(true, Columns);
		}

		private static ImageFormat GetImageFormat(string fileName)
		{
			ImageFormat bmp;
			string extension = Path.GetExtension(fileName);
			if (string.IsNullOrEmpty(extension))
			{
				Environment.Exit(1);
			}
			string lower = extension.ToLower();
			if (lower != null)
			{
				switch (lower)
				{
					case ".bmp":
					{
						bmp = ImageFormat.Bmp;
						break;
					}
					case ".gif":
					{
						bmp = ImageFormat.Gif;
						break;
					}
					case ".ico":
					{
						bmp = ImageFormat.Icon;
						break;
					}
					case ".jpg":
					case ".jpeg":
					{
						bmp = ImageFormat.Jpeg;
						break;
					}
					case ".png":
					{
						bmp = ImageFormat.Png;
						break;
					}
					case ".tif":
					case ".tiff":
					{
						bmp = ImageFormat.Tiff;
						break;
					}
					default:
					{
						if (lower != ".wmf")
						{
							throw new NotImplementedException();
						}
						bmp = ImageFormat.Wmf;
						break;
					}
				}
				return bmp;
			}
			throw new NotImplementedException();
		}

		public static void ImagenRecorteIcao(Bitmap originalImage, ref int PorcentajeImg, ref DataTable dtIcao, ref Image imgcrop)
		{
			string licenseFile = "";
			IFace iface = IFace.Instance;
			try
			{
				if (licenseFile != "")
				{
					iface.InitWithLicense(File.ReadAllBytes(licenseFile));
				}
				else
				{
					iface.Init();
				}
				int minEyeDistance = 40;
				int maxEyeDistance = 1000;
				int maxFaces = 10;
				string detectionMode = "accurate";
				string segmentationMaskFile = "";
				int segmentationIndex = 0;
				string cropFaceFile = "ok";
				int cropIndex = 0;
				if (originalImage != null)
				{
					FaceHandler faceHandler = new FaceHandler();
					faceHandler.SetParam("fd.speed_accuracy_mode", detectionMode);
					faceHandler.SetParam("hss.matting_type", "matting_global");
					faceHandler.SetParam("img.face_crop_full_frontal_extended_scale", "50");
					Face[] faces = faceHandler.DetectFaces(originalImage, (float)minEyeDistance, (float)maxEyeDistance, maxFaces);
					if (faces.Length != 0)
					{
						for (int i = 0; i < (int)faces.Length; i++)
						{
							Face face = faces[i];
							DataTable dt = new DataTable();
							Icao.ShowInfo(face, faceHandler, ref dt, ref PorcentajeImg);
							dtIcao = dt;
							if ((segmentationMaskFile == "" ? false : i == segmentationIndex))
							{
								try
								{
									Image image = face.GetSegmentation(FaceCropMethod.FullFrontalExtended, SegmentationImageType.MaskedAlpha, null);
									image.Save(segmentationMaskFile, Icao.GetImageFormat(segmentationMaskFile));
								}
								catch (IFaceException faceException)
								{
									if (faceException.Code == IFaceExceptionCode.AlgorithmNotAvailable)
									{
									}
								}
							}
							if ((cropFaceFile == "" ? false : i == cropIndex))
							{
								imgcrop = face.GetCropImage(FaceCropMethod.TokenFrontal, null);
							}
						}
						Face[] faceArray = faces;
						for (int j = 0; j < (int)faceArray.Length; j++)
						{
							faceArray[j].Dispose();
						}
						faceHandler.Dispose();
					}
					else
					{
						return;
					}
				}
				iface.Terminate();
			}
			catch (IFaceException faceException1)
			{
				if (faceException1.Message != "Falling outside the image boundaries")
				{
				}
				iface.Terminate();
			}
		}

		private static void ParseArguments(string[] args, ref int minEyeDistance, ref int maxEyeDistance, ref int faces, ref string path, ref string segmentationMaskFile, ref string croppedFaceFile, ref string licenseFile, ref string detectionMode, ref int segmentationIndex, ref int cropIndex)
		{
			bool doShowInfo = false;
			bool imagePresent = false;
			try
			{
				int i = 0;
				while (i < (int)args.Length)
				{
					string lower = args[i].Trim().ToLower();
					if (lower != null)
					{
						switch (lower)
						{
							case "-h":
							{
								doShowInfo = true;
								break;
							}
							case "-i":
							{
								path = args[i + 1];
								imagePresent = true;
								break;
							}
							case "-l":
							{
								licenseFile = args[i + 1];
								break;
							}
							case "-m":
							{
								minEyeDistance = int.Parse(args[i + 1]);
								break;
							}
							case "-n":
							{
								maxEyeDistance = int.Parse(args[i + 1]);
								break;
							}
							case "-f":
							{
								faces = int.Parse(args[i + 1]);
								break;
							}
							case "-fm":
							{
								detectionMode = args[i + 1];
								break;
							}
							case "-s":
							{
								segmentationMaskFile = args[i + 1];
								break;
							}
							case "-si":
							{
								segmentationIndex = int.Parse(args[i + 1]);
								break;
							}
							case "-c":
							{
								croppedFaceFile = args[i + 1];
								break;
							}
							case "-ci":
							{
								cropIndex = int.Parse(args[i + 1]);
								break;
							}
						}
					}
					if (!doShowInfo)
					{
						i++;
					}
					else
					{
						break;
					}
				}
			}
			catch
			{
				doShowInfo = true;
			}
			if ((!imagePresent | doShowInfo ? true : args.Length == 0))
			{
				Icao.ShowInfo();
			}
		}

		private static void ShowInfo()
		{
		}

		private static void ShowInfo(Face face, FaceHandler handler, ref DataTable dtget, ref int PorImg)
		{
			double ValorRes;
			string Valor;
			DataTable dt = new DataTable();
			dt.Columns.Add("Tipo");
			dt.Columns.Add("Detalle");
			dt.Columns.Add("Respuesta");
			dt.Columns.Add("Valor");
			try
			{
				double rostro = (double)(face.GetBasicInfo(handler).Score / 10000f * 100f);
				PorImg = (short)rostro;
				FaceFeatureId[] featureIds = Enum.GetValues(typeof(FaceFeatureId)).Cast<FaceFeatureId>().ToArray<FaceFeatureId>();
				FaceFeature[] features = face.GetFeatures(featureIds, handler);
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
				DataRow fila = dt.NewRow();
				for (int i = 0; i < (int)featureIds.Length; i++)
				{
					fila = dt.NewRow();
					try
					{
						if (featureIds[i] == FaceFeatureId.Unknown)
						{
							goto Label0;
						}
						else if (features[i] != null)
						{
							PointF pos = features[i].Pos;
							if (features[i].Score.HasValue)
							{
								fila["Tipo"] = "Rasgos";
								fila["Detalle"] = featureToName[features[i].FeatureID];
								fila["Respuesta"] = "Confiable";
								fila["Valor"] = "2";
							}
							else
							{
								fila["Tipo"] = "Rasgos";
								fila["Detalle"] = featureToName[features[i].FeatureID];
								fila["Respuesta"] = "No Confiable";
								fila["Valor"] = "1";
							}
							dt.Rows.Add(fila);
						}
						else
						{
							fila["Tipo"] = "Rasgos";
							fila["Detalle"] = featureToName[features[i].FeatureID];
							fila["Respuesta"] = "No se detecto";
							fila["Valor"] = "0";
							dt.Rows.Add(fila);
							goto Label0;
						}
					}
					catch (Exception exception)
					{
						Exception ex = exception;
						fila["Tipo"] = "Rasgos";
						fila["Detalle"] = featureToName[features[i].FeatureID];
						fila["Respuesta"] = ex.Message;
						fila["Valor"] = "ERROR";
						dt.Rows.Add(fila);
					}
				Label0:
					 Valor = "0";
				}
				
				try
				{
					ValorRes = (double)face.GetAttribute(FaceAttributeId.EyeDistance, handler);
					Valor = (ValorRes >= 1 ? "2 Bueno" : "0 Malo");
					fila = dt.NewRow();
					fila["Tipo"] = "Atributos";
					fila["Detalle"] = "Distancia del ojo";
					fila["Respuesta"] = ValorRes;
					fila["Valor"] = Valor;
					dt.Rows.Add(fila);
				}
				catch (Exception exception1)
				{
					Exception ex = exception1;
					fila = dt.NewRow();
					fila["Tipo"] = "Atributos";
					fila["Detalle"] = "";
					fila["Respuesta"] = ex.Message;
					fila["Valor"] = "ERROR";
					dt.Rows.Add(fila);
				}
				try
				{
					ValorRes = (double)face.GetAttribute(FaceAttributeId.Sharpness, handler);
					Valor = "";
					if (ValorRes <= -3000)
					{
						Valor = "0 Malo";
					}
					if ((ValorRes <= -3000 ? false : ValorRes <= 3000))
					{
						Valor = "1 Regular";
					}
					if (ValorRes > 5000)
					{
						Valor = "2 Bueno";
					}
					fila = dt.NewRow();
					fila["Tipo"] = "Atributos";
					fila["Detalle"] = "Nitidez";
					fila["Respuesta"] = ValorRes;
					fila["Valor"] = Valor;
					dt.Rows.Add(fila);
				}
				catch (Exception exception2)
				{
					Exception ex = exception2;
					fila = dt.NewRow();
					fila["Tipo"] = "Atributos";
					fila["Detalle"] = "";
					fila["Respuesta"] = ex.Message;
					fila["Valor"] = "ERROR";
					dt.Rows.Add(fila);
				}
				try
				{
					ValorRes = (double)face.GetAttribute(FaceAttributeId.Brightness, handler);
					Valor = "";
					if (ValorRes < -5000)
					{
						Valor = "0 Malo";
					}
					if ((ValorRes <= -5000 ? false : ValorRes <= 5000))
					{
						Valor = "1 Regular";
					}
					if (ValorRes > 5000)
					{
						Valor = "2 Bueno";
					}
					fila = dt.NewRow();
					fila["Tipo"] = "Atributos";
					fila["Detalle"] = "Brillo";
					fila["Respuesta"] = ValorRes;
					fila["Valor"] = Valor;
					dt.Rows.Add(fila);
				}
				catch (Exception exception3)
				{
					Exception ex = exception3;
					fila = dt.NewRow();
					fila["Tipo"] = "Atributos";
					fila["Detalle"] = "Brillo";
					fila["Respuesta"] = ex.Message;
					fila["Valor"] = "ERROR";
					dt.Rows.Add(fila);
				}
				try
				{
					ValorRes = (double)face.GetAttribute(FaceAttributeId.Contrast, handler);
					Valor = "";
					if (ValorRes < -3000)
					{
						Valor = "0 Malo";
					}
					if ((ValorRes <= -3000 ? false : ValorRes <= 3000))
					{
						Valor = "1 Regular";
					}
					if (ValorRes > 3000)
					{
						Valor = "2 Bueno";
					}
					fila = dt.NewRow();
					fila["Tipo"] = "Atributos";
					fila["Detalle"] = "Contraste";
					fila["Respuesta"] = ValorRes;
					fila["Valor"] = Valor;
					dt.Rows.Add(fila);
				}
				catch (Exception exception4)
				{
					Exception ex = exception4;
					fila = dt.NewRow();
					fila["Tipo"] = "Atributos";
					fila["Detalle"] = "Contraste";
					fila["Respuesta"] = ex.Message;
					fila["Valor"] = "ERROR";
					dt.Rows.Add(fila);
				}
				try
				{
					ValorRes = (double)face.GetAttribute(FaceAttributeId.UniqueIntensityLevels, handler);
					Valor = "";
					if (ValorRes <= -3000)
					{
						Valor = "0 Malo";
					}
					if ((ValorRes <= -3000 ? false : ValorRes <= 3000))
					{
						Valor = "1 Regular";
					}
					if (ValorRes > 5000)
					{
						Valor = "2 Bueno";
					}
					fila = dt.NewRow();
					fila["Tipo"] = "Atributos";
					fila["Detalle"] = "Nivel de intensidad";
					fila["Respuesta"] = ValorRes;
					fila["Valor"] = Valor;
					dt.Rows.Add(fila);
				}
				catch (Exception exception5)
				{
					Exception ex = exception5;
					fila = dt.NewRow();
					fila["Tipo"] = "Atributos";
					fila["Detalle"] = "Nivel de intensidad";
					fila["Respuesta"] = ex.Message;
					fila["Valor"] = "ERROR";
					dt.Rows.Add(fila);
				}
				try
				{
					ValorRes = (double)face.GetAttribute(FaceAttributeId.Shadow, handler);
					Valor = "";
					if (ValorRes <= -3000)
					{
						Valor = "0 Malo";
					}
					if ((ValorRes <= -3000 ? false : ValorRes <= 0))
					{
						Valor = "1 Regular";
					}
					if (ValorRes > 0)
					{
						Valor = "2 Bueno";
					}
					fila = dt.NewRow();
					fila["Tipo"] = "Atributos";
					fila["Detalle"] = "Sombra";
					fila["Respuesta"] = ValorRes;
					fila["Valor"] = Valor;
					dt.Rows.Add(fila);
				}
				catch (Exception exception6)
				{
					Exception ex = exception6;
					fila = dt.NewRow();
					fila["Tipo"] = "Atributos";
					fila["Detalle"] = "Sombra";
					fila["Respuesta"] = ex.Message;
					fila["Valor"] = "ERROR";
					dt.Rows.Add(fila);
				}
				try
				{
					ValorRes = (double)face.GetAttribute(FaceAttributeId.NoseShadow, handler);
					Valor = "";
					if (ValorRes <= -3000)
					{
						Valor = "0 Malo";
					}
					if ((ValorRes <= -3000 ? false : ValorRes <= 0))
					{
						Valor = "1 Regular";
					}
					if (ValorRes > 0)
					{
						Valor = "2 Bueno";
					}
					fila = dt.NewRow();
					fila["Tipo"] = "Atributos";
					fila["Detalle"] = "Sombra en nariz";
					fila["Respuesta"] = ValorRes;
					fila["Valor"] = Valor;
					dt.Rows.Add(fila);
				}
				catch (Exception exception7)
				{
					Exception ex = exception7;
					fila = dt.NewRow();
					fila["Tipo"] = "Atributos";
					fila["Detalle"] = "Sombra en nariz";
					fila["Respuesta"] = ex.Message;
					fila["Valor"] = "ERROR";
					dt.Rows.Add(fila);
				}
				try
				{
					ValorRes = (double)face.GetAttribute(FaceAttributeId.Specularity, handler);
					Valor = "";
					if (ValorRes <= -3000)
					{
						Valor = "0 Malo";
					}
					if ((ValorRes <= -3000 ? false : ValorRes <= 0))
					{
						Valor = "1 Regular";
					}
					if (ValorRes > 0)
					{
						Valor = "2 Bueno";
					}
					fila = dt.NewRow();
					fila["Tipo"] = "Atributos";
					fila["Detalle"] = "Especularidad (Focos)";
					fila["Respuesta"] = ValorRes;
					fila["Valor"] = Valor;
					dt.Rows.Add(fila);
				}
				catch (Exception exception8)
				{
					Exception ex = exception8;
					fila = dt.NewRow();
					fila["Tipo"] = "Atributos";
					fila["Detalle"] = "Especularidad (Focos)";
					fila["Respuesta"] = ex.Message;
					fila["Valor"] = "ERROR";
					dt.Rows.Add(fila);
				}
				try
				{
					ValorRes = (double)face.GetAttribute(FaceAttributeId.EyeGaze, handler);
					Valor = "";
					if (ValorRes <= -3000)
					{
						Valor = "0 Mala";
					}
					if ((ValorRes <= -3000 ? false : ValorRes <= 3000))
					{
						Valor = "1 Regular";
					}
					if (ValorRes > 3000)
					{
						Valor = "2 Buena";
					}
					fila = dt.NewRow();
					fila["Tipo"] = "Atributos";
					fila["Detalle"] = "Mirada";
					fila["Respuesta"] = ValorRes;
					fila["Valor"] = Valor;
					dt.Rows.Add(fila);
				}
				catch (Exception exception9)
				{
					Exception ex = exception9;
					fila = dt.NewRow();
					fila["Tipo"] = "Atributos";
					fila["Detalle"] = "Mirada";
					fila["Respuesta"] = ex.Message;
					fila["Valor"] = "ERROR";
					dt.Rows.Add(fila);
				}
				try
				{
					ValorRes = (double)face.GetAttribute(FaceAttributeId.Roll, handler);
					Valor = "";
					if (ValorRes <= -7500)
					{
						Valor = "0 Demasiado Inclinado a la Izquierda";
					}
					if ((ValorRes <= -7500 ? false : ValorRes <= -5000))
					{
						Valor = "1 Regularmente Inclinado a la Izquierda";
					}
					if ((ValorRes <= -5000 ? false : ValorRes <= 5000))
					{
						Valor = "2 Bueno";
					}
					if ((ValorRes <= 5000 ? false : ValorRes <= 7500))
					{
						Valor = "1 Regularmente Inclinado a la Derecha";
					}
					if (ValorRes > 7500)
					{
						Valor = "0 Demasiado Inclinado a la Derecha";
					}
					fila = dt.NewRow();
					fila["Tipo"] = "Atributos";
					fila["Detalle"] = "Inclinacion Cabeza";
					fila["Respuesta"] = ValorRes;
					fila["Valor"] = Valor;
					dt.Rows.Add(fila);
				}
				catch (Exception exception10)
				{
					Exception ex = exception10;
					fila = dt.NewRow();
					fila["Tipo"] = "Atributos";
					fila["Detalle"] = "Inclinacion Cabeza";
					fila["Respuesta"] = ex.Message;
					fila["Valor"] = "ERROR";
					dt.Rows.Add(fila);
				}
				try
				{
					ValorRes = (double)face.GetAttribute(FaceAttributeId.Pitch, handler);
					Valor = "";
					if (ValorRes <= -7500)
					{
						Valor = "0 Demasiado Alto";
					}
					if ((ValorRes <= -7500 ? false : ValorRes <= -6500))
					{
						Valor = "1 Regularmente Alto";
					}
					if ((ValorRes <= -6500 ? false : ValorRes <= 6500))
					{
						Valor = "2 Bueno";
					}
					if ((ValorRes <= 6500 ? false : ValorRes <= 7500))
					{
						Valor = "1 Regularmente Bajo";
					}
					if (ValorRes > 7500)
					{
						Valor = "0 Demasiado Bajo";
					}
					fila = dt.NewRow();
					fila["Tipo"] = "Atributos";
					fila["Detalle"] = "Cabeceo";
					fila["Respuesta"] = ValorRes;
					fila["Valor"] = Valor;
					dt.Rows.Add(fila);
				}
				catch (Exception exception11)
				{
					Exception ex = exception11;
					fila = dt.NewRow();
					fila["Tipo"] = "Atributos";
					fila["Detalle"] = "Cabeceo";
					fila["Respuesta"] = ex.Message;
					fila["Valor"] = "ERROR";
					dt.Rows.Add(fila);
				}
				try
				{
					ValorRes = (double)face.GetAttribute(FaceAttributeId.Yaw, handler);
					Valor = "";
					if (ValorRes <= -3000)
					{
						Valor = "0 Demasiado Girado Izquierda";
					}
					if ((ValorRes <= -3000 ? false : ValorRes <= -15000))
					{
						Valor = "1 Regularmente Girado Izquierda";
					}
					if ((ValorRes <= -1500 ? false : ValorRes <= 1500))
					{
						Valor = "2 Bueno";
					}
					if ((ValorRes <= 1500 ? false : ValorRes <= 3000))
					{
						Valor = "1 Regularmente Girado a la Derecha";
					}
					if (ValorRes > 7500)
					{
						Valor = "0 Demasiado Girado a la Derecha";
					}
					fila = dt.NewRow();
					fila["Tipo"] = "Atributos";
					fila["Detalle"] = "Giro de la cabeza";
					fila["Respuesta"] = ValorRes;
					fila["Valor"] = Valor;
					dt.Rows.Add(fila);
				}
				catch (Exception exception12)
				{
					Exception ex = exception12;
					fila = dt.NewRow();
					fila["Tipo"] = "Atributos";
					fila["Detalle"] = "Guiñada de la cabeza";
					fila["Respuesta"] = ex.Message;
					fila["Valor"] = "ERROR";
					dt.Rows.Add(fila);
				}
				try
				{
					ValorRes = (double)face.GetAttribute(FaceAttributeId.EyeStatusL, handler);
					Valor = "";
					if (ValorRes <= -1000)
					{
						Valor = "0 Malo";
					}
					if ((ValorRes <= -1000 ? false : ValorRes <= 1000))
					{
						Valor = "1 Regular";
					}
					if (ValorRes > 1000)
					{
						Valor = "2 Bueno";
					}
					fila = dt.NewRow();
					fila["Tipo"] = "Atributos";
					fila["Detalle"] = "Estado ojo izquierdo";
					fila["Respuesta"] = ValorRes;
					fila["Valor"] = Valor;
					dt.Rows.Add(fila);
				}
				catch (Exception exception13)
				{
					Exception ex = exception13;
					fila = dt.NewRow();
					fila["Tipo"] = "Atributos";
					fila["Detalle"] = "Estado ojo izquierdo";
					fila["Respuesta"] = ex.Message;
					fila["Valor"] = "ERROR";
					dt.Rows.Add(fila);
				}
				try
				{
					ValorRes = (double)face.GetAttribute(FaceAttributeId.EyeStatusR, handler);
					Valor = "";
					if (ValorRes <= -1000)
					{
						Valor = "0 Malo";
					}
					if ((ValorRes <= -1000 ? false : ValorRes <= 1000))
					{
						Valor = "1 Regular";
					}
					if (ValorRes > 1000)
					{
						Valor = "2 Bueno";
					}
					fila = dt.NewRow();
					fila["Tipo"] = "Atributos";
					fila["Detalle"] = "Estado ojo derecho";
					fila["Respuesta"] = ValorRes;
					fila["Valor"] = Valor;
					dt.Rows.Add(fila);
				}
				catch (Exception exception14)
				{
					Exception ex = exception14;
					fila = dt.NewRow();
					fila["Tipo"] = "Atributos";
					fila["Detalle"] = "Estado ojo derecho";
					fila["Respuesta"] = ex.Message;
					fila["Valor"] = "ERROR";
					dt.Rows.Add(fila);
				}
				try
				{
					ValorRes = (double)face.GetAttribute(FaceAttributeId.GlassStatus, handler);
					Valor = "";
					if (ValorRes <= 0)
					{
						Valor = "9 NO";
					}
					if (ValorRes > 0)
					{
						Valor = "9 SI";
					}
					fila = dt.NewRow();
					fila["Tipo"] = "Atributos";
					fila["Detalle"] = "Gafas";
					fila["Respuesta"] = ValorRes;
					fila["Valor"] = Valor;
					dt.Rows.Add(fila);
					if (Valor == "9 SI")
					{
						try
						{
							ValorRes = (double)face.GetAttribute(FaceAttributeId.HeavyFrame, handler);
							Valor = "";
							if (ValorRes <= -3000)
							{
								Valor = "0 Muy Resaltante";
							}
							if (ValorRes > -3000)
							{
								Valor = "2 Aceptable";
							}
							fila = dt.NewRow();
							fila["Tipo"] = "Atributos";
							fila["Detalle"] = "Lentes Montura Pesada";
							fila["Respuesta"] = ValorRes;
							fila["Valor"] = Valor;
							dt.Rows.Add(fila);
						}
						catch (Exception exception15)
						{
							Exception ex = exception15;
							fila = dt.NewRow();
							fila["Tipo"] = "Atributos";
							fila["Detalle"] = "Lentes Montura Pesada";
							fila["Respuesta"] = ex.Message;
							fila["Valor"] = "ERROR";
							dt.Rows.Add(fila);
						}
					}
				}
				catch (Exception exception16)
				{
					Exception ex = exception16;
					fila = dt.NewRow();
					fila["Tipo"] = "Atributos";
					fila["Detalle"] = "Gafas";
					fila["Respuesta"] = ex.Message;
					fila["Valor"] = "ERROR";
					dt.Rows.Add(fila);
				}
				try
				{
					ValorRes = (double)face.GetAttribute(FaceAttributeId.MouthStatus, handler);
					Valor = "";
					if (ValorRes <= -2000)
					{
						Valor = "0 Malo";
					}
					if ((ValorRes <= -2000 ? false : ValorRes <= 1000))
					{
						Valor = "1 Regular";
					}
					if (ValorRes > 1000)
					{
						Valor = "2 Bueno";
					}
					fila = dt.NewRow();
					fila["Tipo"] = "Atributos";
					fila["Detalle"] = "Estado Boca";
					fila["Respuesta"] = ValorRes;
					fila["Valor"] = Valor;
					dt.Rows.Add(fila);
				}
				catch (Exception exception17)
				{
					Exception ex = exception17;
					fila = dt.NewRow();
					fila["Tipo"] = "Atributos";
					fila["Detalle"] = "Estado Boca";
					fila["Respuesta"] = ex.Message;
					fila["Valor"] = "ERROR";
					dt.Rows.Add(fila);
				}
				try
				{
					ValorRes = (double)face.GetAttribute(FaceAttributeId.BackgroundUniformity, handler);
					Valor = "";
					if (ValorRes <= 0)
					{
						Valor = "0 Malo";
					}
					if ((ValorRes <= 0 ? false : ValorRes <= 3000))
					{
						Valor = "1 Regular";
					}
					if (ValorRes > 3000)
					{
						Valor = "2 Bueno";
					}
					fila = dt.NewRow();
					fila["Tipo"] = "Atributos";
					fila["Detalle"] = "Fondo";
					fila["Respuesta"] = ValorRes;
					fila["Valor"] = Valor;
					dt.Rows.Add(fila);
				}
				catch (Exception exception18)
				{
					Exception ex = exception18;
					fila = dt.NewRow();
					fila["Tipo"] = "Atributos";
					fila["Detalle"] = "Fondo";
					fila["Respuesta"] = ex.Message;
					fila["Valor"] = "ERROR";
					dt.Rows.Add(fila);
				}
				try
				{
					ValorRes = (double)face.GetAttribute(FaceAttributeId.RedEyeL, handler);
					Valor = "";
					if (ValorRes <= -3000)
					{
						Valor = "0 Malo";
					}
					if ((ValorRes <= -3000 ? false : ValorRes <= 3000))
					{
						Valor = "1 Regular";
					}
					if (ValorRes > 3000)
					{
						Valor = "2 Bueno";
					}
					fila = dt.NewRow();
					fila["Tipo"] = "Atributos";
					fila["Detalle"] = "Ojo Rojo Izquierdo";
					fila["Respuesta"] = ValorRes;
					fila["Valor"] = Valor;
					dt.Rows.Add(fila);
				}
				catch (Exception exception19)
				{
					Exception ex = exception19;
					fila = dt.NewRow();
					fila["Tipo"] = "Atributos";
					fila["Detalle"] = "Ojo Rojo Izquierdo";
					fila["Respuesta"] = ex.Message;
					fila["Valor"] = "ERROR";
					dt.Rows.Add(fila);
				}
				try
				{
					ValorRes = (double)face.GetAttribute(FaceAttributeId.RedEyeR, handler);
					Valor = "";
					if (ValorRes <= -3000)
					{
						Valor = "0 Malo";
					}
					if ((ValorRes <= -3000 ? false : ValorRes <= 3000))
					{
						Valor = "1 Regular";
					}
					if (ValorRes > 3000)
					{
						Valor = "2 Bueno";
					}
					fila = dt.NewRow();
					fila["Tipo"] = "Atributos";
					fila["Detalle"] = "Ojo Rojo Derecho";
					fila["Respuesta"] = ValorRes;
					fila["Valor"] = Valor;
					dt.Rows.Add(fila);
				}
				catch (Exception exception20)
				{
					Exception ex = exception20;
					fila = dt.NewRow();
					fila["Tipo"] = "Atributos";
					fila["Detalle"] = "Ojo Rojo Derecho";
					fila["Respuesta"] = ex.Message;
					fila["Valor"] = "ERROR";
					dt.Rows.Add(fila);
				}
				try
				{
					ValorRes = (double)face.GetAttribute(FaceAttributeId.Gender, handler);
					Valor = "";
					if (ValorRes < 0)
					{
						Valor = "9 Masculino";
					}
					if (ValorRes > 0)
					{
						Valor = "9 Femenino";
					}
					fila = dt.NewRow();
					fila["Tipo"] = "Atributos";
					fila["Detalle"] = "Género";
					fila["Respuesta"] = ValorRes;
					fila["Valor"] = Valor;
					dt.Rows.Add(fila);
				}
				catch (Exception exception21)
				{
					Exception ex = exception21;
					fila = dt.NewRow();
					fila["Tipo"] = "Atributos";
					fila["Detalle"] = "Género";
					fila["Respuesta"] = ex.Message;
					fila["Valor"] = "ERROR";
					dt.Rows.Add(fila);
				}
				try
				{
					ValorRes = (double)face.GetAttribute(FaceAttributeId.Age, handler);
					Valor = string.Concat("9 ", Convert.ToString(Convert.ToInt32(ValorRes)));
					fila = dt.NewRow();
					fila["Tipo"] = "Atributos";
					fila["Detalle"] = "Edad";
					fila["Respuesta"] = ValorRes;
					fila["Valor"] = Valor;
					dt.Rows.Add(fila);
				}
				catch (Exception exception22)
				{
					Exception ex = exception22;
					fila = dt.NewRow();
					fila["Tipo"] = "Atributos";
					fila["Detalle"] = "Edad";
					fila["Respuesta"] = ex.Message;
					fila["Valor"] = "ERROR";
					dt.Rows.Add(fila);
				}
				dtget = dt;
			}
			catch
			{
				dtget = dt;
			}
		}

		private delegate void VoidDelegate();
	}
}