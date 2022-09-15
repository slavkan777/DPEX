using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoctorPremium.Common.Ententions;

namespace DoctorPremium.Web.Helpers
{
	public static class ImageNameHelper
	{
		public static string GetFileNameForPatientPhoto(int patientid, string fileName)
		{
			return String.Concat("p", patientid, "-", LanguageExtension.GetLatinCodeFromCyrillic(fileName.Length > 198 ? fileName.Substring(fileName.Length - 198, 198) : fileName));
		}

		public static string GetFileNameForPatientDoc(int patientid, string fileName)
		{
			return String.Concat(DateTime.UtcNow.Date.ToString("d").Replace("/","."), "_", "doc-p", patientid, "-", LanguageExtension.GetLatinCodeFromCyrillic(fileName.Length > 194 ? fileName.Substring(fileName.Length - 194, 194) : fileName));
		}

		public static string GetFileNameUserInfoPhoto(int userInfoid, string fileName)
		{
			return String.Concat("ui", userInfoid, "-", LanguageExtension.GetLatinCodeFromCyrillic(fileName.Length > 198 ? fileName.Substring(fileName.Length - 198, 198) : fileName));
		}
	}
}