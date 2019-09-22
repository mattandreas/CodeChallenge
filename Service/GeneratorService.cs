using System.Collections;
using System.Collections.ObjectModel;
using Model;

namespace Service
{
    public class GeneratorService : IService
    {
        private readonly IGenerator _gen;

        public IList Words { get; } = new ObservableCollection<string>();

        public GeneratorService()
        {
            _gen = new Generator();
            //_gen = new FastGenerator();
        }

        public void CheckWord(string input)
        {
            Words.Clear();
            var newWords = _gen.CheckInput(input);
            foreach (string word in newWords)
            {
                Words.Add(word);
            }
        }
    }
}
