﻿using System;
namespace QuantConnect.Queues.WordRepositories
{
    public class WordRepository
    {
        private readonly string[] _words;

        public WordRepository(params string[] words)
        {
            _words = words;
        }

        public string[] Get()
        {
            return _words;
        }
    }
}