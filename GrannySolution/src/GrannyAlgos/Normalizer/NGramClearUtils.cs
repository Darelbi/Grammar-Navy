using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace GrannyAlgos.Normalizer
{
    public static class NGramClearUtils
    {
        private static readonly Regex NotLettersRegex =
            new Regex(@"\p{P}|\p{S}|\p{N}", RegexOptions.Compiled);
        private static readonly Regex SentenceSplit =
            new Regex(@"(?<=[\.!?;:…])\s+", RegexOptions.Compiled);

        public static string[] SplitSentences(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return Array.Empty<string>();

            return SentenceSplit.Split(text);
        }

        public static string CleanForNgrams(string input, CultureInfo info)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            // 1. Lowercase (rispettando la lingua)
            string lower = info.TextInfo.ToLower(input);

            // 2. Rimuovi punteggiatura e simboli
            string noPunct = NotLettersRegex.Replace(lower, " ");

            // 3. Normalizza gli spazi
            return Regex.Replace(noPunct, @"\s+", " ").Trim();
        }
    }
}
