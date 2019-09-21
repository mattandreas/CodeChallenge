using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Model;

namespace Service
{
    public class GeneratorService
    {
        private readonly Generator _gen;

        public ObservableCollection<string> Words { get; } = new ObservableCollection<string>();

        public GeneratorService()
        {
            _gen = new Generator();
        }

        public void CheckWord(string input)
        {
            Words.Clear();
            var newWords = _gen.CheckInput(input);
            foreach (var word in newWords)
            {
                Words.Add(word);
            }
        }
    }
}
