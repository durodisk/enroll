using ENROLL.Helpers;
using Innovatrics.IDKit;
using Innovatrics.IDKit.Structures;
using Innovatrics.Sdk.Commons.Enums;
using System;
using System.ComponentModel;
using System.Drawing;

namespace ENROLL
{
	internal static class Match
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
				Match.ShowSystemInfo();
				IDKit.GetUserLimit();
				HelperSerializer ser = new HelperSerializer();
				User papel = IDKit.InitUser();
				Image dedopapel = pHuellaPapel;
				if (dedo == 1)
				{
					IDKit.AddFingerprint(papel, FingerPosition.LeftThumb, Match.converterDemo(dedopapel));
				}
				if (dedo == 2)
				{
					IDKit.AddFingerprint(papel, FingerPosition.LeftIndex, Match.converterDemo(dedopapel));
				}
				if (dedo == 3)
				{
					IDKit.AddFingerprint(papel, FingerPosition.LeftMiddle, Match.converterDemo(dedopapel));
				}
				if (dedo == 4)
				{
					IDKit.AddFingerprint(papel, FingerPosition.LeftRing, Match.converterDemo(dedopapel));
				}
				if (dedo == 5)
				{
					IDKit.AddFingerprint(papel, FingerPosition.LeftLittle, Match.converterDemo(dedopapel));
				}
				if (dedo == 6)
				{
					IDKit.AddFingerprint(papel, FingerPosition.RightThumb, Match.converterDemo(dedopapel));
				}
				if (dedo == 7)
				{
					IDKit.AddFingerprint(papel, FingerPosition.RightIndex, Match.converterDemo(dedopapel));
				}
				if (dedo == 8)
				{
					IDKit.AddFingerprint(papel, FingerPosition.RightMiddle, Match.converterDemo(dedopapel));
				}
				if (dedo == 9)
				{
					IDKit.AddFingerprint(papel, FingerPosition.RightRing, Match.converterDemo(dedopapel));
				}
				if (dedo == 10)
				{
					IDKit.AddFingerprint(papel, FingerPosition.RightLittle, Match.converterDemo(dedopapel));
				}
				User en_vivo = IDKit.InitUser();
				Image dedobio = ser.byteArrayToImage(pHuellaBio);
				if (dedo == 1)
				{
					IDKit.AddFingerprint(en_vivo, FingerPosition.LeftThumb, Match.converterDemo(dedobio));
				}
				if (dedo == 2)
				{
					IDKit.AddFingerprint(en_vivo, FingerPosition.LeftIndex, Match.converterDemo(dedobio));
				}
				if (dedo == 3)
				{
					IDKit.AddFingerprint(en_vivo, FingerPosition.LeftMiddle, Match.converterDemo(dedobio));
				}
				if (dedo == 4)
				{
					IDKit.AddFingerprint(en_vivo, FingerPosition.LeftRing, Match.converterDemo(dedobio));
				}
				if (dedo == 5)
				{
					IDKit.AddFingerprint(en_vivo, FingerPosition.LeftLittle, Match.converterDemo(dedobio));
				}
				if (dedo == 6)
				{
					IDKit.AddFingerprint(en_vivo, FingerPosition.RightThumb, Match.converterDemo(dedobio));
				}
				if (dedo == 7)
				{
					IDKit.AddFingerprint(en_vivo, FingerPosition.RightIndex, Match.converterDemo(dedobio));
				}
				if (dedo == 8)
				{
					IDKit.AddFingerprint(en_vivo, FingerPosition.RightMiddle, Match.converterDemo(dedobio));
				}
				if (dedo == 9)
				{
					IDKit.AddFingerprint(en_vivo, FingerPosition.RightRing, Match.converterDemo(dedobio));
				}
				if (dedo == 10)
				{
					IDKit.AddFingerprint(en_vivo, FingerPosition.RightLittle, Match.converterDemo(dedobio));
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