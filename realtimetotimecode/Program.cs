using System.Reflection;
using System.Runtime.CompilerServices;

namespace realtimetotimecode
{
    internal class Program
    {
        public static string workingdirectory = Directory.GetParent(Assembly.GetExecutingAssembly().Location).ToString();
        public static string chaptersfile = "";
        public static string ffmetadatafile = "";
        public static string outMetadata = "";
        public static string output;
        public static List<chap> chaps = new List<chap>();
        static void Main(string[] args)
        {
            chaptersfile = workingdirectory + "\\chapters.txt";
            ffmetadatafile = workingdirectory + "\\FFMETADATAFILE";
            outMetadata = workingdirectory + "\\new_FFMETADATAFILE";
            Console.WriteLine("Chapter file: " + chaptersfile);
            Console.WriteLine("Metadata file: " + ffmetadatafile);
            Console.WriteLine("");
            string metadata = File.ReadAllText(ffmetadatafile);
            output = metadata + "\n";
            string[] chaptersRaw = File.ReadAllLines(chaptersfile, System.Text.Encoding.UTF8);
            foreach (string line in chaptersRaw) 
            {
                string time = line.Split(' ')[0];
                string title = line.Replace(time + " ", "");
                if(time == "") { continue; }
                string[] timecomponents = time.Split(':');
                int hrs = int.Parse(timecomponents[0]);
                int mins = int.Parse(timecomponents[1]);
                int secs = int.Parse(timecomponents[2]);
                int minutes = (hrs * 60) + mins;
                int seconds = secs + (minutes * 60);
                int timestamp = (seconds * 1000);
                chaps.Add(new chap(title, timestamp));
            }
            chap nextChap = null;
            chap currentChap = null;
            foreach (chap i in chaps)
            {
                if(currentChap == null)
                {
                    currentChap = i;
                    continue;
                }
                nextChap = i;
                string appendOutput = "[CHAPTER]\nTIMEBASE=1/1000\nSTART=" + (currentChap.starttime+1) + "\nEND=" + (nextChap.starttime) + "\ntitle=" + currentChap.title +"\n";
                output = output + appendOutput + "\n";
                currentChap = nextChap;
                Console.WriteLine(appendOutput);

            }
            File.WriteAllText(outMetadata, output);
        }
    }
    public class chap
    {
        public string title { get; set; }
        public int starttime { get; set; }
        public chap(string inTitle, int instarttime)
        {
            this.title = inTitle;
            this.starttime = instarttime;
         }
    }
}
