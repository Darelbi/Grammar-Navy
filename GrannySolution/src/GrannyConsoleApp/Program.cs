using System;
using System.IO;
using System.Linq;
using System.Globalization;
using GrannyAlgos.Corpus;
using Newtonsoft.Json;
using GrannyAlgos.Containers;
using GrannyAlgos.Normalizer;

namespace GrannyConsoleApp
{
    public class Program
    {
        public static void CheckSpecialChars(OPUSDocument document, string json)
        {
            if (document.dialogues.Where(x => 
                    x.Contains("�") ||
                    x.Contains("�") || // special character not empty string
                    x.Contains("�?�")
                    || x.Contains("�") || x.Contains("�") 
                    ).Any())
            {

                string corpusES = @"C:\Users\Dario\Documents\GitHub\Grammar-Navy-Corpus\ES\ChatSubs\open_subtitles_es\findENCODINGERRORS.txt";

                var jsonE = new string[] { json }.AsEnumerable();
                var lines = document.dialogues.AsEnumerable();
                var all = jsonE.Concat(lines);
                var joined = string.Join(";", all);
                File.AppendAllLines(corpusES, new string[] { joined });
            }
        }

        public static void Main(string[] args)
        {
            string readUno = @"C:\Users\Dario\Documents\GitHub\Grammar-Navy-Corpus\ES\ChatSubs\open_subtitles_es_realidad_uno.txt";
            string englishWords = @"C:\Users\Dario\Documents\GitHub\Grammar-Navy-Corpus\ES\ChatSubs\4000-most-common-english-words-csv.csv";
            var englishWordsToRemove= File.ReadAllLines(englishWords);

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
            forbiddenWods.Remove("fatalv
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
            forbiddenWods.Remove("a");
            forbiddenWods.Remove("y");
            forbiddenWods.Remove("o");
            forbiddenWods.Remove("e");
            forbiddenWods.Remove("u");

            for (int i = 1; i < englishWordsToRemove.Length; i++)
            {
                forbiddenWods.Add( englishWordsToRemove[i].ToLower());
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


                    Console.WriteLine("--- files:" +  files + "\nSeen elements2: " + dos.GetSeenElements());
                    Console.WriteLine("Seen elements3: " + tres.GetSeenElements());
                }

                quanti--;
                
                        files++;
                if (true)
                {
                    var jsonText = NGramClearUtils.FixSpuriousUTF8Encoding( File.ReadAllText(file));
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
            var linesdos    = linesdosraw.OrderByDescending(x => x.Priority). Select(x => (x.Priority + offdos) + ";" + x.Value);

            var linestresraw = tres.GetNodes();
            var offtres = tres.GetPriorityOffset();
            var linetres = linestresraw.OrderByDescending(x => x.Priority).Select(x => (x.Priority + offtres) + ";" + x.Value);

            var linesquatraw = quatro.GetNodes();
            var offquat = quatro.GetPriorityOffset();
            var linequat = linesquatraw.OrderByDescending(x => x.Priority).Select(x => (x.Priority + offquat) + ";" + x.Value);

            File.WriteAllLines(writeDos, linesdos, encoding: System.Text.Encoding.UTF8);
            File.WriteAllLines(writTres, linetres, encoding: System.Text.Encoding.UTF8);
            File.WriteAllLines(writQuat, linequat, encoding: System.Text.Encoding.UTF8);
            Console.WriteLine("Finished Processing in seconds: " + (System.DateTime.Now - start).TotalSeconds);
            Console.WriteLine("Seen elements: " + dos.GetSeenElements());
            Console.WriteLine("Seen elements: " + tres.GetSeenElements());
            Console.WriteLine("Seen elements: " + quatro.GetSeenElements());
            Console.ReadKey();
        }


        // ORIGINALE
        //public static void Main(string[] args)
        //{
        //    string corpusES = @"C:\Users\Dario\Documents\GitHub\Grammar-Navy-Corpus\ES\ChatSubs\open_subtitles_es\";
        //    var cinfo = new CultureInfo("es-ES");

        //    var allFiles = Directory.EnumerateFiles(corpusES, "*.jsonl", SearchOption.AllDirectories);

        //    var uno = new RandSpaceSavingCounter<string>(6000);
        //    var dos = new RandSpaceSavingCounter<string>(30000);
        //    var tres = new RandSpaceSavingCounter<string>(70000);

        //    int docNum = 1;
        //    foreach(var file in allFiles)
        //    {
        //        var jsonText = File.ReadAllText(file);
        //        try
        //        {
        //            var document = JsonConvert.DeserializeObject<OPUSDocument>(jsonText);
        //            var tokenizer = new OPUSTokenizer(document, cinfo);

        //            foreach (var dgram in tokenizer.GetNGrams(1))
        //                uno.Add(dgram);

        //            foreach (var dgram in tokenizer.GetNGrams(2))
        //                dos.Add(dgram);

        //            foreach (var dgram in tokenizer.GetNGrams(3))
        //                tres.Add(dgram);
        //        }
        //        catch(System.Exception e)
        //        {
        //            Console.WriteLine(e.Message);
        //        }
        //        Console.WriteLine("Processed doc num:" + docNum++);
        //    }


        //    string writeUno = @"C:\Users\Dario\Documents\GitHub\Grammar-Navy-Corpus\ES\ChatSubs\open_subtitles_es_realidad_uno.txt";
        //    string writeDos = @"C:\Users\Dario\Documents\GitHub\Grammar-Navy-Corpus\ES\ChatSubs\open_subtitles_es_realidad_dos.txt";
        //    string writTres = @"C:\Users\Dario\Documents\GitHub\Grammar-Navy-Corpus\ES\ChatSubs\open_subtitles_es_realidad_tres.txt";

        //    File.WriteAllLines(writeUno, uno.GetOccurences().Select(x => x.Value + ";" + x.Key));
        //    File.WriteAllLines(writeDos, dos.GetOccurences().Select(x => x.Value + ";" + x.Key));
        //    File.WriteAllLines(writTres, tres.GetOccurences().Select(x => x.Value + ";" + x.Key));
        //    Console.WriteLine("Finished Processing");
        //    Console.ReadKey();
        //}

        //public static void Main(string[] args)
        //{
        //    string corpusES = @"C:\Users\Dario\Documents\GitHub\Grammar-Navy-Corpus\ES\ChatSubs\open_subtitles_es\";
        //    string writeTo = @"C:\Users\Dario\Documents\GitHub\Grammar-Navy-Corpus\ES\ChatSubs\open_subtitles_es_test.txt";
        //    var cinfo = new CultureInfo("es-ES");

        //    var anyJson = "{\"dialogues\": [\"En una tierra m\\u00edtica y en un tiempo de magia, el destino de un gran reino descansa sobre los hombros de un joven.\\nSu nombre.\\nMerl\\u00edn.\", \"Creo que ha sido un buen viaje.\\nS\\u00ed, todos cogimos algo.\\nIncluido Merl\\u00edn.\\n\\u00bfQu\\u00e9 cogi\\u00f3?\\n\\u00a1Un resfriado!\\nSi aprendieras a rastrear, lo disfrutar\\u00edas m\\u00e1s.\\nSoy el mejor rastreador.\", \"Cre\\u00ed ver a alguien. Estaba equivocado.\\nProbablemente era un ciervo.\", \"\\u00bfEst\\u00e1s seguro?\\nS\\u00ed.\\nMoveos.\", \"\\u00bfEst\\u00e1s seguro?\\nLo vi.\", \"Debe haber una explicaci\\u00f3n sencilla.\\nNo lo creo, Gaius.\\nNo despu\\u00e9s de todo lo que me han dicho. - Aprecia al rey.\\nEl sentimiento es mutuo. Se han hecho grandes amigos.\\nEse es mi temor. Arturo no puede verlo, yo s\\u00ed.\\nNo cometer\\u00e9 ese mismo error.\", \"\\u00bfLo tienes todo?\\nS\\u00ed.\", \"Si no hubieses sido t\\u00fa.\\nLo s\\u00e9.\", \"Eres un caballero.\\nEso no importa.\", \"Quiz\\u00e1s deber\\u00eda dec\\u00edrselo a Arturo.\\nMerl\\u00edn.\", \"Estaba herida. \\u00bfQu\\u00e9 pod\\u00eda hacer?\\n\\u00bfDejar que la capturaran?\", \"Ten\\u00eda una flecha en la pierna. No puede caminar.\\nTe est\\u00e1s arriesgando.\\nNo puedo dejarla morir.\", \"Ella es.\\nalguien.\", \"No puedo explicarlo.\\n\\u00bfD\\u00f3nde est\\u00e1?\\nNecesita unos pocos d\\u00edas.\\nSe ir\\u00e1. No quiere hacer ning\\u00fan da\\u00f1o.\\nPor favor, no debes cont\\u00e1rselo a nadie.\\nSabes que si Arturo la atrapa.\\nser\\u00e1 ejecutada.\", \"\\u00bfQu\\u00e9 est\\u00e1s haciendo?\\nSoy como un cisne.\", \"Parece que no estoy haciendo nada, pero estoy haciendo un mont\\u00f3n de trabajo en el fondo.\\nInteresante.\\nYo te veo m\\u00e1s como un piojo.\\nClaro.\\nIn\\u00fatil. Irritante.\\nVamos. Tenemos que ir de patrulla.\", \"\\u00a1Merl\\u00edn!\\nAqu\\u00ed.\", \"Se trata de mantenerse alerta. - \\u00bfQu\\u00e9 ves?\\nVeo un par de calzones que necesitan lavarse.\", \"\\u00bfAhora qu\\u00e9 ves?\\nBrillante.\", \"Un par de calzones que necesitan lavarse. - \\u00a1En la rama! - Est\\u00e1 rota.\\n\\u00bfQu\\u00e9 te dice eso?\\nAlgo dej\\u00f3 un rastro.\\nEs reciente. Un animal.\\n\\u00bfEso crees?\\nUn ciervo.\\nTendr\\u00eda que ser uno grande. Con una cornamenta muy grande.\\n\\u00bfQu\\u00e9 te hace decir eso?\\nPorque nos est\\u00e1 mirando.\", \"Ayer estaba seco. Anoche llovi\\u00f3.\\nEstas marcas son recientes.\", \"Nuestros hombres han estado patrullando este \\u00e1rea d\\u00eda y noche.\\nPobrablemente es uno de ellos.\\n\\u00bfQui\\u00e9n?\\nMe dieron sus informes.\\nNadie ha estado por aqu\\u00ed.\", \"\\u00bfPor qu\\u00e9? - \\u00a1No se lo dije!\\nMe diste tu palabra. -Lo juro.\", \"Lo hiciste porque me odias.\\nNo. - Esta vez has ido demasiado lejos.\\nMe lo pagar\\u00e1s, Merl\\u00edn.\\n\\u00bfQu\\u00e9 ocurre?\", \"\\u00bfDe qu\\u00e9 va todo esto?\\nNo es nada.\", \"\\u00bfMerl\\u00edn?\\nNo es nada.\", \"\\u00bfEras parte de un grupo de sajones que atac\\u00f3 un cargamento de armas para Camelot?\\nS\\u00ed.\", \"\\u00bfY actuabas bajo las \\u00f3rdenes de Morgana Pendragon?\\nLo que hice, lo hice por cuenta propia, por mi gente y por nuestro derecho a ser libres.\\nNo tengo disputas con los druidas.\\nHe pasado mi vida huyendo por mis creencias y he visto c\\u00f3mo mataban a aquellos que amaba.\\nUna vez, quiz\\u00e1s.\\nPero yo no soy mi padre.\\n\\u00bfNo matas a los que usan la magia?\", \"y Camelot los que lo pagar\\u00e9is.\\nEn tus palabras.\", \"Pi\\u00e9nsalo mejor.\\nNo hay nada que pueda hacer.\", \"Es diferente.\\n\\u00bfC\\u00f3mo?\", \"Mordred no solo va a liberar a Kara.\\nSe ir\\u00e1 con ella. - Si lo hace no se reconciliar\\u00e1 con Arturo.\\nLa decisi\\u00f3n de Arturo ya ha puesto a Mordred en su contra.\\nCon la chica de su lado, correr\\u00e1 a los brazos de Morgana.\\nNo puedes querer la muerte de Kara.\", \"Pero tampoco quiero la de Arturo.\\nMientras Mordred permanezca entre estos muros, a\\u00fan habr\\u00e1 esperanza.\\nTengo que detenerle.\", \"\\u00bfQu\\u00e9 es?\\nArturo.\", \"\\u00bfVas a dec\\u00edrmelo?\\n\\u00c9l.\", \"Vamos a registrar la ciudadela.\\nSe habr\\u00e1n ido. Registrad el bosque.\", \"Quiero que los capturen.\\n\\u00bfVivos?\\nSon fugitivos. La ley es clara.\\nDisp\\u00f3n de tantos jinetes como puedas.\\nAparta todos tus sentimientos personales.\", \"Necesitas descansar.\\nNo podemos.\", \"Por aqu\\u00ed.\\nMe pareci\\u00f3 ver algo.\", \"Por que Arturo no es nada sin Emrys.\\nY Emrys no es nada sin la magia.\"]}";

        //    var obj = JsonConvert.DeserializeObject<OPUSDocument>(anyJson);
        //    var token = new OPUSTokenizer(obj, cinfo);

        //    var uno = new RandSpaceSavingCounter<string>(15);
        //    var dos = new RandSpaceSavingCounter<string>(30);
        //    var tres = new RandSpaceSavingCounter<string>(35);

        //    foreach (var dgram in token.GetNGrams(1))
        //        uno.Add(dgram);

        //    foreach (var dgram in token.GetNGrams(2))
        //        dos.Add(dgram);

        //    foreach (var dgram in token.GetNGrams(3))
        //        tres.Add(dgram);

        //    string writeUno = @"C:\Users\Dario\Documents\GitHub\Grammar-Navy-Corpus\ES\ChatSubs\open_subtitles_es_test_uno.txt";
        //    string writeDos = @"C:\Users\Dario\Documents\GitHub\Grammar-Navy-Corpus\ES\ChatSubs\open_subtitles_es_test_dos.txt";
        //    string writTres = @"C:\Users\Dario\Documents\GitHub\Grammar-Navy-Corpus\ES\ChatSubs\open_subtitles_es_test_tres.txt";

        //    File.WriteAllLines(writeUno, uno.GetOccurences().Select(x => x.Key + " " + x.Value));
        //    File.WriteAllLines(writeDos, dos.GetOccurences().Select(x => x.Key + " " + x.Value));
        //    File.WriteAllLines(writTres, tres.GetOccurences().Select(x => x.Key + " " + x.Value));

        //    List<string> lines = new List<string>();

        //    for (int n = 1; n <= 3; n++)
        //        foreach (var dgram in token.GetNGrams(n))
        //            lines.Add(dgram);

        //    File.WriteAllLines(writeTo, lines);
        //}


        //public static void Main(string[] args)
        //{
        //    string corpusES = @"C:\Users\Dario\Documents\GitHub\Grammar-Navy-Corpus\ES\ChatSubs\open_subtitles_es\";
        //    string writeTo = @"C:\Users\Dario\Documents\GitHub\Grammar-Navy-Corpus\ES\ChatSubs\open_subtitles_es_test.txt";
        //    var cinfo = new CultureInfo("es-ES");

        //    var anyJson = "{\"dialogues\": [\"En una tierra m\\u00edtica y en un tiempo de magia, el destino de un gran reino descansa sobre los hombros de un joven.\\nSu nombre.\\nMerl\\u00edn.\", \"Creo que ha sido un buen viaje.\\nS\\u00ed, todos cogimos algo.\\nIncluido Merl\\u00edn.\\n\\u00bfQu\\u00e9 cogi\\u00f3?\\n\\u00a1Un resfriado!\\nSi aprendieras a rastrear, lo disfrutar\\u00edas m\\u00e1s.\\nSoy el mejor rastreador.\", \"Cre\\u00ed ver a alguien. Estaba equivocado.\\nProbablemente era un ciervo.\", \"\\u00bfEst\\u00e1s seguro?\\nS\\u00ed.\\nMoveos.\", \"\\u00bfEst\\u00e1s seguro?\\nLo vi.\", \"Debe haber una explicaci\\u00f3n sencilla.\\nNo lo creo, Gaius.\\nNo despu\\u00e9s de todo lo que me han dicho. - Aprecia al rey.\\nEl sentimiento es mutuo. Se han hecho grandes amigos.\\nEse es mi temor. Arturo no puede verlo, yo s\\u00ed.\\nNo cometer\\u00e9 ese mismo error.\", \"\\u00bfLo tienes todo?\\nS\\u00ed.\", \"Si no hubieses sido t\\u00fa.\\nLo s\\u00e9.\", \"Eres un caballero.\\nEso no importa.\", \"Quiz\\u00e1s deber\\u00eda dec\\u00edrselo a Arturo.\\nMerl\\u00edn.\", \"Estaba herida. \\u00bfQu\\u00e9 pod\\u00eda hacer?\\n\\u00bfDejar que la capturaran?\", \"Ten\\u00eda una flecha en la pierna. No puede caminar.\\nTe est\\u00e1s arriesgando.\\nNo puedo dejarla morir.\", \"Ella es.\\nalguien.\", \"No puedo explicarlo.\\n\\u00bfD\\u00f3nde est\\u00e1?\\nNecesita unos pocos d\\u00edas.\\nSe ir\\u00e1. No quiere hacer ning\\u00fan da\\u00f1o.\\nPor favor, no debes cont\\u00e1rselo a nadie.\\nSabes que si Arturo la atrapa.\\nser\\u00e1 ejecutada.\", \"\\u00bfQu\\u00e9 est\\u00e1s haciendo?\\nSoy como un cisne.\", \"Parece que no estoy haciendo nada, pero estoy haciendo un mont\\u00f3n de trabajo en el fondo.\\nInteresante.\\nYo te veo m\\u00e1s como un piojo.\\nClaro.\\nIn\\u00fatil. Irritante.\\nVamos. Tenemos que ir de patrulla.\", \"\\u00a1Merl\\u00edn!\\nAqu\\u00ed.\", \"Se trata de mantenerse alerta. - \\u00bfQu\\u00e9 ves?\\nVeo un par de calzones que necesitan lavarse.\", \"\\u00bfAhora qu\\u00e9 ves?\\nBrillante.\", \"Un par de calzones que necesitan lavarse. - \\u00a1En la rama! - Est\\u00e1 rota.\\n\\u00bfQu\\u00e9 te dice eso?\\nAlgo dej\\u00f3 un rastro.\\nEs reciente. Un animal.\\n\\u00bfEso crees?\\nUn ciervo.\\nTendr\\u00eda que ser uno grande. Con una cornamenta muy grande.\\n\\u00bfQu\\u00e9 te hace decir eso?\\nPorque nos est\\u00e1 mirando.\", \"Ayer estaba seco. Anoche llovi\\u00f3.\\nEstas marcas son recientes.\", \"Nuestros hombres han estado patrullando este \\u00e1rea d\\u00eda y noche.\\nPobrablemente es uno de ellos.\\n\\u00bfQui\\u00e9n?\\nMe dieron sus informes.\\nNadie ha estado por aqu\\u00ed.\", \"\\u00bfPor qu\\u00e9? - \\u00a1No se lo dije!\\nMe diste tu palabra. -Lo juro.\", \"Lo hiciste porque me odias.\\nNo. - Esta vez has ido demasiado lejos.\\nMe lo pagar\\u00e1s, Merl\\u00edn.\\n\\u00bfQu\\u00e9 ocurre?\", \"\\u00bfDe qu\\u00e9 va todo esto?\\nNo es nada.\", \"\\u00bfMerl\\u00edn?\\nNo es nada.\", \"\\u00bfEras parte de un grupo de sajones que atac\\u00f3 un cargamento de armas para Camelot?\\nS\\u00ed.\", \"\\u00bfY actuabas bajo las \\u00f3rdenes de Morgana Pendragon?\\nLo que hice, lo hice por cuenta propia, por mi gente y por nuestro derecho a ser libres.\\nNo tengo disputas con los druidas.\\nHe pasado mi vida huyendo por mis creencias y he visto c\\u00f3mo mataban a aquellos que amaba.\\nUna vez, quiz\\u00e1s.\\nPero yo no soy mi padre.\\n\\u00bfNo matas a los que usan la magia?\", \"y Camelot los que lo pagar\\u00e9is.\\nEn tus palabras.\", \"Pi\\u00e9nsalo mejor.\\nNo hay nada que pueda hacer.\", \"Es diferente.\\n\\u00bfC\\u00f3mo?\", \"Mordred no solo va a liberar a Kara.\\nSe ir\\u00e1 con ella. - Si lo hace no se reconciliar\\u00e1 con Arturo.\\nLa decisi\\u00f3n de Arturo ya ha puesto a Mordred en su contra.\\nCon la chica de su lado, correr\\u00e1 a los brazos de Morgana.\\nNo puedes querer la muerte de Kara.\", \"Pero tampoco quiero la de Arturo.\\nMientras Mordred permanezca entre estos muros, a\\u00fan habr\\u00e1 esperanza.\\nTengo que detenerle.\", \"\\u00bfQu\\u00e9 es?\\nArturo.\", \"\\u00bfVas a dec\\u00edrmelo?\\n\\u00c9l.\", \"Vamos a registrar la ciudadela.\\nSe habr\\u00e1n ido. Registrad el bosque.\", \"Quiero que los capturen.\\n\\u00bfVivos?\\nSon fugitivos. La ley es clara.\\nDisp\\u00f3n de tantos jinetes como puedas.\\nAparta todos tus sentimientos personales.\", \"Necesitas descansar.\\nNo podemos.\", \"Por aqu\\u00ed.\\nMe pareci\\u00f3 ver algo.\", \"Por que Arturo no es nada sin Emrys.\\nY Emrys no es nada sin la magia.\"]}";
        //    //var ser = Json
        //    var obj = JsonConvert.DeserializeObject<OPUSDocument>(anyJson);
        //    var token = new OPUSTokenizer(obj, cinfo);

        //    List<string> lines = new List<string>();

        //    for (int n = 1; n <= 3; n++)
        //        foreach (var dgram in token.GetNGrams(n))
        //            lines.Add(dgram);

        //    File.WriteAllLines(writeTo, lines);

        //    //var files = Directory.EnumerateFiles(corpusES, "*.json", SearchOption.AllDirectories);
        //    //foreach (var file in files)
        //    //{

        //    //    string jsonText = File.ReadAllText(file);
        //    //    var obj = JsonSerializer.Deserialize<OPUSDocument>(jsonText);
        //    //}
        //}
    }
}

