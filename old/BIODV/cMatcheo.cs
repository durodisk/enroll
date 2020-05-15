using BIODV.Util;
using Innovatrics.IDKit;
using Innovatrics.IDKit.Structures;
using Innovatrics.Sdk.Commons.Enums;
using System;
using System.ComponentModel;
using System.Drawing;

namespace BIODV
{
	internal static class cMatcheo
	{
		public static byte[] converterDemo(Image x)
		{
			byte[] numArray;
			try
			{
				numArray = (byte[])(new ImageConverter()).ConvertTo(x, typeof(byte[]));
			}
			catch
			{
				numArray = null;
			}
			return numArray;
		}

		private static void ShowSystemInfo()
		{
			IDKit.GetHardwareId();
			IDKit.GetProductString();
		}

		public static string verificarhuellas(int dedo, Image pHuellaPapel, byte[] pHuellaBio)
		{
			string message;
			string resultado = "";
			try
			{
				IDKit.InitModule();
				Connection connection = new Connection();
				cMatcheo.ShowSystemInfo();
				IDKit.GetUserLimit();
				Serializer ser = new Serializer();
				User papel = IDKit.InitUser();
				Image dedopapel = pHuellaPapel;
				if (dedo == 1)
				{
					IDKit.AddFingerprint(papel, FingerPosition.LeftThumb, cMatcheo.converterDemo(dedopapel));
				}
				if (dedo == 2)
				{
					IDKit.AddFingerprint(papel, FingerPosition.LeftIndex, cMatcheo.converterDemo(dedopapel));
				}
				if (dedo == 3)
				{
					IDKit.AddFingerprint(papel, FingerPosition.LeftMiddle, cMatcheo.converterDemo(dedopapel));
				}
				if (dedo == 4)
				{
					IDKit.AddFingerprint(papel, FingerPosition.LeftRing, cMatcheo.converterDemo(dedopapel));
				}
				if (dedo == 5)
				{
					IDKit.AddFingerprint(papel, FingerPosition.LeftLittle, cMatcheo.converterDemo(dedopapel));
				}
				if (dedo == 6)
				{
					IDKit.AddFingerprint(papel, FingerPosition.RightThumb, cMatcheo.converterDemo(dedopapel));
				}
				if (dedo == 7)
				{
					IDKit.AddFingerprint(papel, FingerPosition.RightIndex, cMatcheo.converterDemo(dedopapel));
				}
				if (dedo == 8)
				{
					IDKit.AddFingerprint(papel, FingerPosition.RightMiddle, cMatcheo.converterDemo(dedopapel));
				}
				if (dedo == 9)
				{
					IDKit.AddFingerprint(papel, FingerPosition.RightRing, cMatcheo.converterDemo(dedopapel));
				}
				if (dedo == 10)
				{
					IDKit.AddFingerprint(papel, FingerPosition.RightLittle, cMatcheo.converterDemo(dedopapel));
				}
				User en_vivo = IDKit.InitUser();
				Image dedobio = ser.byteArrayToImage(pHuellaBio);
				if (dedo == 1)
				{
					IDKit.AddFingerprint(en_vivo, FingerPosition.LeftThumb, cMatcheo.converterDemo(dedobio));
				}
				if (dedo == 2)
				{
					IDKit.AddFingerprint(en_vivo, FingerPosition.LeftIndex, cMatcheo.converterDemo(dedobio));
				}
				if (dedo == 3)
				{
					IDKit.AddFingerprint(en_vivo, FingerPosition.LeftMiddle, cMatcheo.converterDemo(dedobio));
				}
				if (dedo == 4)
				{
					IDKit.AddFingerprint(en_vivo, FingerPosition.LeftRing, cMatcheo.converterDemo(dedobio));
				}
				if (dedo == 5)
				{
					IDKit.AddFingerprint(en_vivo, FingerPosition.LeftLittle, cMatcheo.converterDemo(dedobio));
				}
				if (dedo == 6)
				{
					IDKit.AddFingerprint(en_vivo, FingerPosition.RightThumb, cMatcheo.converterDemo(dedobio));
				}
				if (dedo == 7)
				{
					IDKit.AddFingerprint(en_vivo, FingerPosition.RightIndex, cMatcheo.converterDemo(dedobio));
				}
				if (dedo == 8)
				{
					IDKit.AddFingerprint(en_vivo, FingerPosition.RightMiddle, cMatcheo.converterDemo(dedobio));
				}
				if (dedo == 9)
				{
					IDKit.AddFingerprint(en_vivo, FingerPosition.RightRing, cMatcheo.converterDemo(dedobio));
				}
				if (dedo == 10)
				{
					IDKit.AddFingerprint(en_vivo, FingerPosition.RightLittle, cMatcheo.converterDemo(dedobio));
				}
				MatchResult matchResult2 = connection.MatchUsersEx(papel, en_vivo);
				string valido = "";
				valido = (!matchResult2.Valid ? "NO" : "SI");
				int score = matchResult2.Score;
				resultado = string.Concat("Huella Valida: ", valido, " / Similitud Aproximada: ", score.ToString());
				IDKit.FreeUser(papel);
				IDKit.FreeUser(en_vivo);
				connection.Close();
				IDKit.TerminateModule();
				message = resultado;
			}
			catch (Exception exception)
			{
				message = exception.Message;
			}
			return message;
		}
	}
}