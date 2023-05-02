using System;

namespace Labb3Library
{
    public class WordModel
    {
        public string[] Translations { get; set; }
        public int FromLanguage { get; }
        public int ToLanguage { get; }
        public WordModel(params string[] translations)
        {
            Translations = translations;
        }
        public WordModel(int fromLanguage, int toLanguage, params string[] translations)
        {
            Translations = translations;
            FromLanguage = fromLanguage;
            ToLanguage = toLanguage;
            
        }
    }
}
