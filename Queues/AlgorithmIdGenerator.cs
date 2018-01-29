using System;
using System.Collections.Specialized;

namespace QuantConnect.Queues
{
    public class AlgorithmIdGenerator
    {
        private readonly Random _random;
        private StringCollection _reserved;

        /// <summary>
        /// String value used to separate the different words that make up a generated code name.
        /// </summary>
        public string Separator { get; set; }

        /// <summary>
        /// Array of WordBanks which will be accessed sequentially (by order of array index) to provide words for code name generation.
        /// </summary>
        public WordBank[] Parts { get; set; }

        /// <summary>
        /// The casing format for generated code names.
        /// </summary>
        public Casing Casing { get; set; }

        /// <summary>
        /// String value used as a suffix for the generated code name.
        /// </summary>
        public string EndsWith { get; set; }

        public AlgorithmIdGenerator(string separator = " ", Casing casing = Casing.PascalCase) {
            Parts = new WordBank[] { WordBank.Adjectives, WordBank.Colors, WordBank.Animals };
            Separator = separator;
            Casing = casing;
            _random = new Random();
            EndsWith = "";
            _reserved = new StringCollection();
        }

        public AlgorithmIdGenerator(string separator, Casing casing, params WordBank[] wordBanks) : this(separator, casing)
        {
            Parts = wordBanks;
            _reserved = new StringCollection();
        }

        public string GenerateUnique()
        {
            var name = Generate();
            if (_reserved.Contains(name))
            {
                return GenerateUnique();
            }
            return name;
        }

        private string Generate()
        {
            var name = string.Empty;
            for (int i = 0; i < Parts.Length; i++)
            {
                var repositoryContents = Parts[i].Get();
                var index = _random.Next(repositoryContents.Length);
                var part = repositoryContents[index];
                var partWords = part.Split(' ');
                foreach (var partWord in partWords)
                {
                    var word = partWord;
                    switch (Casing)
                    {
                        case Casing.LowerCase:
                            word = word.ToLower();
                            break;
                        case Casing.UpperCase:
                            word = word.ToUpper();
                            break;
                        case Casing.PascalCase:
                            word = word.FirstCharToUpper();
                            break;
                        case Casing.CamelCase:
                            if (string.IsNullOrEmpty(name))
                            {
                                word = word.ToLower();
                            }
                            else
                                word = word.FirstCharToUpper();
                            break;
                    }

                    if (string.IsNullOrEmpty(name))
                        name = word;
                    else
                        name += word;
                    name += Separator;
                }
            }
            if (Separator.Length > 0)
                name = name.Remove(name.Length - Separator.Length);
            return name + EndsWith;
        }
    }
}
