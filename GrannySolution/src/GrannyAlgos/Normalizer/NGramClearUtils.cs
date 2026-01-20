using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Text;

namespace GrannyAlgos.Normalizer
{
    public static class NGramClearUtils
    {
        // Some words are not valid UTF8, so I assume they come from a corrupted conversion
        private static readonly Regex CorruptedWords =
            new Regex(@"(?<=\s)\p{L}*<=>\p{L}*(?=\s)");

        private static readonly Regex NotLettersRegex =
            new Regex(@"\p{P}|\p{S}|\p{N}", RegexOptions.Compiled);
        private static readonly Regex SentenceSplit =
            new Regex(@"(?<=[\.!?;:…])\s+", RegexOptions.Compiled);
        private static readonly Regex blockRegex = 
            new Regex(@"(\\u[0-9A-Fa-f]{4}){2,}", RegexOptions.Compiled);
        private static readonly Regex seqRegex =
            new Regex(@"\\u([0-9A-Fa-f]{4})", RegexOptions.Compiled);
        private static readonly UTF8Encoding throwerEncoding 
            = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true );

        // Ok the Library I downloaded of OPUS (Open subtitles) has some UTF8 characters encoded in a wrong way
        // The single bytes of the encoding are encoded as UNICODE points individually in the JSON
        // I amend the problem for future generations.
        public static string FixSpuriousUTF8Encoding(string input)
        {
            var blockRegex = new Regex(@"(\\u[0-9A-Fa-f]{4}){2,}");
            var seqRegex = new Regex(@"\\u([0-9A-Fa-f]{4})");

            var result = blockRegex.Replace(input, match =>
            {
                // Extract sequences manually
                var seqs = new List<string>();
                foreach (Match m in seqRegex.Matches(match.Value))
                {
                    seqs.Add(m.Groups[1].Value); // hex digits only
                }

                // Too many sequences → exception
                if (seqs.Count > 2)
                    return string.Empty;

                // Convert each \uXXXX to a char
                byte[] bytes = seqs.Select(hex => Convert.ToByte(hex, 16)).ToArray();

                try
                {
                    string replacement = throwerEncoding.GetString(bytes);
                    return replacement;
                }
                catch(Exception e)
                {
                    return "<=>"; // mark the word as corrupted. Probably I can recover it but there's already enough content. Not worth the time
                }


            });

            return result;
        }


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

            string cleaned = CorruptedWords.Replace(input, "");

            // 1. Lowercase (rispettando la lingua)
            string lower = info.TextInfo.ToLower(cleaned);

            // 2. Rimuovi punteggiatura e simboli
            string noPunct = NotLettersRegex.Replace(lower, " ");

            // 3. Normalizza gli spazi
            return Regex.Replace(noPunct, @"\s+", " ").Trim();
        }
    }
}
