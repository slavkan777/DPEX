using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorPremium.Common.Ententions
{
	public static class LanguageExtension
	{
		public static string GetLatinCodeFromCyrillic(string str)
		{
			str = str.Replace("б", "b");
			str = str.Replace("в", "v");
			str = str.Replace("г", "g");
			str = str.Replace("х", "h");
			str = str.Replace("д", "d");
			str = str.Replace("э", "ye");
			str = str.Replace("ж", "zh");
			str = str.Replace("з", "z");
			str = str.Replace("и", "i");
			str = str.Replace("ы", "i");
			str = str.Replace("й", "y");
			str = str.Replace("к", "k");
			str = str.Replace("л", "l");
			str = str.Replace("м", "m");
			str = str.Replace("н", "n");
			str = str.Replace("п", "p");
			str = str.Replace("р", "r");
			str = str.Replace("с", "s");
			str = str.Replace("ч", "ch");
			str = str.Replace("ш", "sh");
			str = str.Replace("щ", "shch");
			str = str.Replace('ъ', '"');
			str = str.Replace("ю", "yu");
			str = str.Replace("я", "ya");
			str = str.Replace('ь', '"');
			str = str.Replace('т', 't');
			str = str.Replace('ц', 'c');
			str = str.Replace('о', 'o');
			str = str.Replace('е', 'e');
			str = str.Replace('ё', 'e');
			str = str.Replace('а', 'a');
			str = str.Replace('ф', 'f');
			str = str.Replace("у", "u");
			str = str.Replace('х', 'x');
			str = str.Replace(' ', '_');
			return str;
		}
	}
}
