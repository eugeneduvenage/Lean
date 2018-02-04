using System;
using QuantConnect.Queues.WordRepositories;

namespace QuantConnect.Queues
{
    /// <summary>
    /// Code modified from https://github.com/colinmxs/CodenameGenerator
    /// </summary>    
    public class WordBank
    {
        protected readonly string Name;
        protected readonly Word Value;
        protected readonly WordRepository Repo;

        public static readonly WordBank Adjectives = new WordBank(Word.Adjective, "Adjectives", new AdjectivesRepository());

        public static readonly WordBank Animals = new WordBank(Word.Animal, "Animals", new AnimalNamesRepository());

        public static readonly WordBank Colors = new WordBank(Word.Color, "Colors", new ColorNamesRepository());

        public WordBank(Word value, string name, WordRepository repo)
        {
            Name = name;
            Value = value;
            Repo = repo;
        }

        public override string ToString()
        {
            return Name;
        }

        public static implicit operator Word(WordBank @enum)
        {
            return @enum.Value;
        }

        public static implicit operator string(WordBank @enum)
        {
            return @enum.Name;
        }

        public string[] Get()
        {
            return Repo.Get();
        }
    }
}
