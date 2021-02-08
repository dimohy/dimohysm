using System;
using System.Diagnostics;
using System.IO;

namespace Dimohysm.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var filename = "./temp.dat";

            CreateTempFile(filename);

            using var mmfSpan = FileMemory.Connect(filename);
            var span = mmfSpan.AsReadOnlyBytes();
            var sum = 0L;

            var sw = Stopwatch.StartNew();
            for (var count = 1; count <= 100; count++)
            {
                for (var i = 0; i < span.Length; i++)
                {
                    sum += span[i];
                }
                Console.WriteLine($"Count: {count}");
            }
            sw.Stop();
            Console.WriteLine($"Sum: {sum}");
            Console.WriteLine($"Elapsed Time: {sw.ElapsedMilliseconds}");


            static void CreateTempFile(string filename)
            {
                if (File.Exists(filename) == true)
                    return;
                
                using var fs = File.OpenWrite(filename);
                var length = 1000 * 1000 * 1000;
                for (var i = 0; i < length; i++)
                    fs.WriteByte((byte)i);
            }
        }
    }
}
