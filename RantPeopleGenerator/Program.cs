using System;
using System.Collections.Generic;
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

            if(!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "output")))
            {
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "output"));
            }

            for (int i = 0; i < 3; i++)
            {
                File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "output", i + ".yaml"), engine.Do(yamlTemplate));
            }
        }
    }
}
