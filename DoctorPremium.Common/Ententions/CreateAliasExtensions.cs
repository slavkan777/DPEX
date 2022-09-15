using System;
using System.Text.RegularExpressions;
using DoctorPremium.Common.Ententions;

namespace CleverCMS.Web.Core.Extensions
{
    public static class CreateAliasExtensions
    {
        public static string CreateAlias(string title, int id)
        {
            // convert multiple spaces into one space
            String rezult = Regex.Replace(title.ToLower(), @"\s+", " ").Trim();
			rezult = LanguageExtension.GetLatinCodeFromCyrillic(rezult);
            
            // invalid chars
            rezult = Regex.Replace(rezult, @"[^a-z0-9\s-]", "");

            // cut and trim it 
            rezult = rezult.Substring(0, rezult.Length <= 80 ? rezult.Length : rezult.LastIndexOf(' ', 80)).Trim();

            // hyphens
            rezult = Regex.Replace(rezult, @"\s", "-");    
            return String.Format("{0}-{1}", rezult, id);
        }
    }
}