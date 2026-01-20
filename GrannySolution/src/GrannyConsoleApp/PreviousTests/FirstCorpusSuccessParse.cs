using System;
using System.IO;
using System.Linq;
using System.Globalization;
using GrannyAlgos.Corpus;
using Newtonsoft.Json;
using GrannyAlgos.Containers;

namespace GrannyConsoleApp.PreviousTests
{
    public static class FirstCorpusSuccessParse
    {
        public static void FirstTest()
        {
            string corpusES = @"C:\Users\Dario\Documents\GitHub\Grammar-Navy-Corpus\ES\ChatSubs\open_subtitles_es\";
            var cinfo = new CultureInfo("es-ES");

            var allFiles = Directory.EnumerateFiles(corpusES, "*.jsonl", SearchOption.AllDirectories);

            var uno = new RandSpaceSavingCounter<string>(6000);
            var dos = new RandSpaceSavingCounter<string>(20000);
            var tres = new RandSpaceSavingCounter<string>(40000);

            int quanti = 1000;
            int docNum = 1;
            foreach (var file in allFiles)
            {
                if (quanti < 0)
                {
                    quanti = 1000;
                    Console.WriteLine("Seen elements1: " + uno.GetSeenElements());
                    Console.WriteLine("Seen elements2: " + dos.GetSeenElements());
                    Console.WriteLine("Seen elements3: " + tres.GetSeenElements());
                }

                quanti--;
                var jsonText = File.ReadAllText(file);
                try
                {
                    var document = JsonConvert.DeserializeObject<OPUSDocument>(jsonText);
                    var tokenizer = new OPUSTokenizer(document, cinfo);

                    //foreach (var dgram in tokenizer.GetNGrams(1))
                    //    uno.Add(dgram);

                    //foreach (var dgram in tokenizer.GetNGrams(2))
                    //    dos.Add(dgram);

                    //foreach (var dgram in tokenizer.GetNGrams(3))
                    //    tres.Add(dgram);
                }
                catch (System.Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                // Console.WriteLine("Processed doc num:" + docNum++);
            }


            string writeUno = @"C:\Users\Dario\Documents\GitHub\Grammar-Navy-Corpus\ES\ChatSubs\open_subtitles_es_realidad_uno.txt";
            string writeDos = @"C:\Users\Dario\Documents\GitHub\Grammar-Navy-Corpus\ES\ChatSubs\open_subtitles_es_realidad_dos.txt";
            string writTres = @"C:\Users\Dario\Documents\GitHub\Grammar-Navy-Corpus\ES\ChatSubs\open_subtitles_es_realidad_tres.txt";

            //File.WriteAllLines(writeUno, uno.GetAsCsvLines().Select(x => x.Value + ";" + x.Key));
            //File.WriteAllLines(writeDos, dos.GetAsCsvLines().Select(x => x.Value + ";" + x.Key));
            //File.WriteAllLines(writTres, tres.GetAsCsvLines().Select(x => x.Value + ";" + x.Key));
            Console.WriteLine("Finished Processing");
            Console.WriteLine("Seen elements: " + uno.GetSeenElements());
            Console.WriteLine("Seen elements: " + dos.GetSeenElements());
            Console.WriteLine("Seen elements: " + tres.GetSeenElements());
            Console.ReadKey();
        }
    }
}
