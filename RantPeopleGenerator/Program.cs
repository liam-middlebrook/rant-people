using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rant;

namespace RantPeopleGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            RantEngine engine = new RantEngine(Path.Combine(Directory.GetCurrentDirectory(), "vocab"));
            string yamlTemplate = "";
            StreamReader sr = null;

            try
            {
                sr = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "template.yaml"));
                yamlTemplate = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                }
            }

            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "output")))
            {
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "output"));
            }

            Stopwatch watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < 32767; i++)
            {
                string firstName = engine.Do("<name>");
                string lastName = engine.Do("<noun><noun>");
                string nick = engine.Do("<adj><noun-animal>");
                string tld = engine.Do("<noun>");
                string noun0 = engine.Do("<noun>");
                string noun1 = engine.Do("<noun.plural>");
                string verb0 = engine.Do("<verb>");
                string num0 = engine.Do("[n:1990;2035]");
                string noun2 = engine.Do("<noun-animal>");
                string num1 = engine.Do("[n:10;99]");
                string adj0 = engine.Do("<adj>");
                string noun3 = engine.Do("<noun-animal>");
                string verb1 = engine.Do("<verb>");
                File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "output", nick.Replace(" ", "-") + ".yaml"),
                    string.Format(
                    yamlTemplate,
                    firstName.Replace(" ","-"),
                    lastName.Replace(" ", "-"),
                    nick.Replace(" ", "-"),
                    tld.Replace(" ", "-"),
                    noun0.Replace(" ", "-"),
                    noun1.Replace(" ", "-"),
                    verb0.Replace(" ", "-"),
                    num0.Replace(" ", "-"),
                    noun2.Replace(" ", "-"),
                    num1.Replace(" ", "-"),
                    adj0.Replace(" ", "-"),
                    noun3.Replace(" ", "-"),
                    verb1.Replace(" ", "-")));
            }
            watch.Stop();

            Console.WriteLine("Generation took {0} seconds!", watch.Elapsed.TotalSeconds);
            Console.Read();
        }
    }
}
