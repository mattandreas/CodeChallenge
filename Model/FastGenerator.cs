using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace Model
{
    //Sadly, this method is actually slower.
    public class FastGenerator : IGenerator, IDisposable
    {
        private readonly IntPtr _gen;

        private GCHandle _handle;

        private readonly IntPtr[] _words;

        [DllImport("FastGenerator.dll", CallingConvention=CallingConvention.Cdecl)]
        private static extern IntPtr CreateGenerator(IntPtr strArray, int size);

        [DllImport("FastGenerator.dll", CallingConvention=CallingConvention.Cdecl)]
        private static extern IntPtr CallCheckInput(IntPtr generator, string input, out int count);
                
        [DllImport("FastGenerator.dll", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DisposeGenerator(IntPtr generator);


        public FastGenerator()
        {
            var words = LoadWords();

            _words = new IntPtr[words.Length];

            for (var i = 0; i < words.Length; i++)
            {
                _words[i] = Marshal.StringToHGlobalAnsi(words[i]);
            }

            _handle = GCHandle.Alloc(_words, GCHandleType.Pinned);

            _gen = CreateGenerator(_handle.AddrOfPinnedObject(), _words.Length);
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
            var words = new List<string>();

            var ptr = CallCheckInput(_gen, input, out var count);

            for (var i = 0; i < count; i++)
            {
                var p = Marshal.ReadIntPtr(ptr, i * 4);
                var s = Marshal.PtrToStringAnsi(p);
                words.Add(s);
            }

            return words;
        }

        private void ReleaseUnmanagedResources()
        {
            DisposeGenerator(_gen);
            _handle.Free();
            foreach (var ptr in _words)
            {
                Marshal.FreeHGlobal(ptr);
            }
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        ~FastGenerator()
        {
            ReleaseUnmanagedResources();
        }
    }
}