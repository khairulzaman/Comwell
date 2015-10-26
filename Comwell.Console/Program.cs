using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Comwell.Model;
using Comwell.Model.External;

using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.Spreadsheets;


namespace Comwell.Terminal
{
    class Program
    {
        public static SpreadsheetsService service = new SpreadsheetsService("Comwell");
        public static string CLIENT_SECRET = "H7OOjul1zXwvDCFNMl9TSFYs";
        public static string SCOPE = "https://spreadsheets.google.com/feeds https://docs.google.com/feeds";
        public static string REDIRECT_URI = "urn:ietf:wg:oauth:2.0:oob";

        [STAThread]
        public static void Main(string[] args)
        {
            Console.WriteLine("Comwell DB: Database for COM Systems");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("Interactive Test Suite");
            DoWork();
            //GApi.Authenticate();
        }

        private static void DoWork()
        {
            service = new SpreadsheetsService("Comwell");
            service.RequestFactory = GApi.RequestFactory;

            // Make the request to Google
            // See other portions of this guide for code to put here...
            Console.Write("Retrieving data... ");
            ListQuery query = new ListQuery(GApi.SpreadsheetSubmissions, "1", "private", "values");
            ListFeed feed = service.Query(query);

            Console.WriteLine("complete.");


            Console.Write("Processing data... ");

            Submission.ProcessSubmissions(feed);
            Console.WriteLine("complete.");
            Console.ReadLine();
            Console.WriteLine("There are " + Submission.Submissions.Count + " submissions data retrieved.");
            foreach (Submission sub in Submission.Submissions)
            {
                Console.WriteLine(sub.ToString());
            }
            Console.ReadLine();
        }

    }
}
