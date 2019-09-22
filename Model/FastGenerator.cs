using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Model
{
    public class FastGenerator : IGenerator
    {
        private readonly IntPtr _gen;

        [DllImport("FastGenerator.dll", CallingConvention=CallingConvention.Cdecl)]
        private static extern IntPtr CreateGenerator(string[] strArray, int size);

        [DllImport("FastGenerator.dll", CallingConvention=CallingConvention.Cdecl)]
        private static extern string[] CallCheckInput(IntPtr generator, string input);
                
        [DllImport("FastGenerator.dll", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DisposeGenerator(IntPtr generator);


        public FastGenerator()
        {
            var words = LoadWords();
            _gen = CreateGenerator(words, words.Length);
        }

        private static string[] LoadWords()
        {
            var words = new List<string>();
            using (var fs = File.Open(@"words.txt", FileMode.Open, FileAccess.Read))
            using (var bs = new BufferedStream(fs))
            using (var sr = new StreamReader(bs))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    words.Add(line);
                }
            }

            return words.ToArray();
        }

        public IList CheckInput(string input)
        {
            var words = CallCheckInput(_gen, input);

            return words != null ? words.ToList() : new List<string>();
        }
    }
}