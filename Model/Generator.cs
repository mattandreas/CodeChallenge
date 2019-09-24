using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Model
{
    public class Generator : IGenerator
    {
        private readonly List<string> _words = new List<string>();

        public Generator()
        {
            LoadWords();
        }

        private void LoadWords()
        {
            using (var fs = File.Open(@"words.txt", FileMode.Open, FileAccess.Read))
            using (var bs = new BufferedStream(fs))
            using (var sr = new StreamReader(bs))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    _words.Add(line);
                }
            }
        }

        public IList CheckInput(string input)
        {
            return _words.Where(word => CheckWord(input, word)).ToList();
        }

        private static bool CheckWord(string input, string wordToCheck)
        {
            var sequence = input.ToCharArray().ToList();
            foreach (var character in wordToCheck)
            {
                if (!sequence.Contains(character))
                    return false;
                sequence.Remove(character);
            }
            return true;
        }

    }
}
