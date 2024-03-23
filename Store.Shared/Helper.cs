using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Store.Shared
{
    public static class Helper
    {
        public static string RemoveAccents(string text)
        {
            // Substituir caracteres acentuados por suas versões não acentuadas
            return Regex.Replace(text.Normalize(NormalizationForm.FormD), @"[\p{Diacritic}]", string.Empty);
        }
    }
}
