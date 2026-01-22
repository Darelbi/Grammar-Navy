using System;
using System.IO;
using System.Linq;
using System.Globalization;
using GrannyAlgos.Corpus;
using Newtonsoft.Json;
using GrannyAlgos.Containers;
using GrannyAlgos.Normalizer;

namespace GrannyConsoleApp.PreviousTests
{
    public class BuildCorpusClean
    {
        public static void BuildCorpusCleanMethod()
        {
            string readUno = @"C:\Users\Dario\Documents\GitHub\Grammar-Navy-Corpus\ES\ChatSubs\open_subtitles_es_realidad_uno.txt";
            string englishWords = @"C:\Users\Dario\Documents\GitHub\Grammar-Navy-Corpus\ES\ChatSubs\4000-most-common-english-words-csv.csv";
            var englishWordsToRemove = File.ReadAllLines(englishWords);

            var forbidWords = new NGramList(File.ReadAllLines(readUno));
            var forbiddenWods = forbidWords.GetTopNGrams(100);
            forbiddenWods.Add("www");
            forbiddenWods.Add("subtitulos");
            forbiddenWods.Add("tusubtitulo");
            forbiddenWods.Add("myspace");

            // words to remove, some english abbreviations and phonetic sounds on which I'm not interested.
            forbiddenWods.Add("i");
            forbiddenWods.Add("t");

            forbiddenWods.Add("m");
            forbiddenWods.Add("mm");
            forbiddenWods.Add("ahh");
            forbiddenWods.Add("ehh");
            forbiddenWods.Add("ohh");
            forbiddenWods.Add("uyy");
            forbiddenWods.Add("uyyy");
            forbiddenWods.Add("mmm");
            forbiddenWods.Add("hmm");
            forbiddenWods.Add("hmmm");
            forbiddenWods.Add("emmm");
            forbiddenWods.Add("ejem");
            forbiddenWods.Add("agh");
            forbiddenWods.Add("aghh");
            forbiddenWods.Add("ugh");
            forbiddenWods.Add("argh");
            forbiddenWods.Add("grr");
            forbiddenWods.Add("grrr");
            forbiddenWods.Add("pff");
            forbiddenWods.Add("pfff");
            forbiddenWods.Add("ufff");
            forbiddenWods.Add("bahh");
            forbiddenWods.Add("ajáá");
            forbiddenWods.Add("jeje");
            forbiddenWods.Add("jaja");
            forbiddenWods.Add("jajaja");
            forbiddenWods.Add("jojo");
            forbiddenWods.Add("jijiji");
            forbiddenWods.Add("snif");
            forbiddenWods.Add("snifff");
            forbiddenWods.Add("zzz");
            forbiddenWods.Add("muac");
            forbiddenWods.Add("toc");
            forbiddenWods.Add("pum");
            forbiddenWods.Add("zas");
            forbiddenWods.Add("plaf");
            forbiddenWods.Add("plof");
            forbiddenWods.Add("crash");
            forbiddenWods.Add("boom");
            forbiddenWods.Add("bang");
            forbiddenWods.Add("glu");
            forbiddenWods.Add("gluglu");
            forbiddenWods.Add("achís");
            forbiddenWods.Add("achú");
            forbiddenWods.Add("mu");
            forbiddenWods.Add("miau");
            forbiddenWods.Add("guau");
            forbiddenWods.Add("kss");
            forbiddenWods.Add("shh");
            forbiddenWods.Add("sshh");
            forbiddenWods.Add("tss");
            forbiddenWods.Add("tsss");
            forbiddenWods.Add("brr");
            forbiddenWods.Add("brrr");
            forbiddenWods.Add("prr");
            forbiddenWods.Add("prrr");
            forbiddenWods.Add("ñam");
            forbiddenWods.Add("clap");
            forbiddenWods.Add("tic");
            forbiddenWods.Add("tac");
            forbiddenWods.Add("ding");
            forbiddenWods.Add("dong");
            forbiddenWods.Add("ring");
            forbiddenWods.Add("splash");
            forbiddenWods.Add("sniff");
            forbiddenWods.Add("gulp");
            forbiddenWods.Add("ah");
            forbiddenWods.Add("eh");
            forbiddenWods.Add("oh");
            forbiddenWods.Add("uh");
            forbiddenWods.Add("ay");
            forbiddenWods.Add("uy");
            forbiddenWods.Add("ey");

            forbiddenWods.Add("whoa");
            forbiddenWods.Add("ffff");
            forbiddenWods.Add("marvel");
            forbiddenWods.Add("agents");
            forbiddenWods.Add("of");

            // words that are present (not necessarily with same meaning) both in spanish and enlighs
            // or english words that is likely to find in spanish dialogues should not remove those
            // Ideally I should discard sentences with no spanish words. Another time!
            forbiddenWods.Remove("no");
            forbiddenWods.Remove("a");
            forbiddenWods.Remove("me");
            forbiddenWods.Remove("mi");
            forbiddenWods.Remove("si");
            forbiddenWods.Remove("tu");
            forbiddenWods.Remove("yo");
            forbiddenWods.Remove("hotel");
            forbiddenWods.Remove("animal");
            forbiddenWods.Remove("hospital");
            forbiddenWods.Remove("doctor");
            forbiddenWods.Remove("color");
            forbiddenWods.Remove("actor");
            forbiddenWods.Remove("error");
            forbiddenWods.Remove("idea");
            forbiddenWods.Remove("radio");
            forbiddenWods.Remove("video");
            forbiddenWods.Remove("piano");
            forbiddenWods.Remove("solo");
            forbiddenWods.Remove("gas");
            forbiddenWods.Remove("bar");
            forbiddenWods.Remove("club");
            forbiddenWods.Remove("normal");
            forbiddenWods.Remove("fatalv");
            forbiddenWods.Remove("ideal");
            forbiddenWods.Remove("total");
            forbiddenWods.Remove("general");
            forbiddenWods.Remove("central");
            forbiddenWods.Remove("final");
            forbiddenWods.Remove("global");
            forbiddenWods.Remove("local");
            forbiddenWods.Remove("natural");
            forbiddenWods.Remove("original");
            forbiddenWods.Remove("personal");
            forbiddenWods.Remove("internet");
            forbiddenWods.Remove("robot");
            forbiddenWods.Remove("taxi");
            forbiddenWods.Remove("menu");
            forbiddenWods.Remove("virus");
            forbiddenWods.Remove("test");
            forbiddenWods.Remove("yoga");
            forbiddenWods.Remove("wifi");
            forbiddenWods.Remove("email");
            forbiddenWods.Remove("chat");
            forbiddenWods.Remove("zoom");
            //forbiddenWods.Remove("a");
            //forbiddenWods.Remove("y"); // too common in spanish
            forbiddenWods.Remove("o");
            forbiddenWods.Remove("e");
            forbiddenWods.Remove("u");
            forbiddenWods.Remove("has");

            var letters = new string[] { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "z" };
            foreach (var letter in letters)
                forbiddenWods.Add(letter);

            // last words
            forbiddenWods.Add("no");
            forbiddenWods.Add("me");
            forbiddenWods.Add("y");
            forbiddenWods.Add("a");

            for (int i = 1; i < englishWordsToRemove.Length; i++)
            {
                forbiddenWods.Add(englishWordsToRemove[i].ToLower());
            }

            string corpusES = @"C:\Users\Dario\Documents\GitHub\Grammar-Navy-Corpus\ES\ChatSubs\open_subtitles_es\";
            var cinfo = new CultureInfo("es-ES");

            var allFiles = Directory.EnumerateFiles(corpusES, "*.jsonl", SearchOption.AllDirectories);

            var dos = new RandSpaceSavingCounter<string>(100000);
            var tres = new RandSpaceSavingCounter<string>(100000);
            var quatro = new RandSpaceSavingCounter<string>(100000);

            var start = System.DateTime.Now;
            int quanti = 1000;
            long files = 0;
            foreach (var file in allFiles)
            {
                if (quanti < 0)
                {
                    quanti = 1000;


                    Console.WriteLine("--- files:" + files + "\nSeen elements2: " + dos.GetSeenElements());
                    Console.WriteLine("Seen elements3: " + tres.GetSeenElements());
                }

                quanti--;

                files++;
                if (true)
                {
                    var jsonText = NGramClearUtils.FixSpuriousUTF8Encoding(File.ReadAllText(file));
                    var jsonBox = new BoxedString { Line = jsonText };
                    try
                    {
                        var document = JsonConvert.DeserializeObject<OPUSDocument>(jsonText);
                        var tokenizer = new OPUSTokenizer(document, cinfo, forbiddenWods);
                        //CheckSpecialChars(document, jsonText);
                        foreach (var dgram in tokenizer.GetNGrams(2))
                            dos.Add(dgram);

                        foreach (var dgram in tokenizer.GetNGrams(3))
                            tres.Add(dgram);

                        foreach (var dgram in tokenizer.GetNGrams(4))
                            quatro.Add(dgram);
                    }
                    catch (System.Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }

            string writeDos = @"C:\Users\Dario\Documents\GitHub\Grammar-Navy-Corpus\ES\ChatSubs\OpusCleanEngSound2Gram.csv";
            string writTres = @"C:\Users\Dario\Documents\GitHub\Grammar-Navy-Corpus\ES\ChatSubs\OpusCleanEngSound3Gram.csv";
            string writQuat = @"C:\Users\Dario\Documents\GitHub\Grammar-Navy-Corpus\ES\ChatSubs\OpusCleanEngSound4Gram.csv";

            //Console.ReadKey();
            var linesdosraw = dos.GetNodes();
            var offdos = dos.GetPriorityOffset();
            var linesdos = linesdosraw.OrderByDescending(x => x.Priority).Select(x => (x.Priority + offdos) + ";" + x.RealInsertions + ";" + x.Value);

            var linestresraw = tres.GetNodes();
            var offtres = tres.GetPriorityOffset();
            var linetres = linestresraw.OrderByDescending(x => x.Priority).Select(x => (x.Priority + offtres) + ";" + x.RealInsertions + ";" + x.Value);

            var linesquatraw = quatro.GetNodes();
            var offquat = quatro.GetPriorityOffset();
            var linequat = linesquatraw.OrderByDescending(x => x.Priority).Select(x => (x.Priority + offquat) + ";" + x.RealInsertions + ";" + x.Value);

            File.WriteAllLines(writeDos, linesdos, encoding: System.Text.Encoding.UTF8);
            File.WriteAllLines(writTres, linetres, encoding: System.Text.Encoding.UTF8);
            File.WriteAllLines(writQuat, linequat, encoding: System.Text.Encoding.UTF8);
            Console.WriteLine("Finished Processing in seconds: " + (System.DateTime.Now - start).TotalSeconds);
            Console.WriteLine("Seen elements: " + dos.GetSeenElements());
            Console.WriteLine("Seen elements: " + tres.GetSeenElements());
            Console.WriteLine("Seen elements: " + quatro.GetSeenElements());
            Console.ReadKey();
        }
    }
}
