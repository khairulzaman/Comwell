using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.GData.Client;
using Google.GData.Spreadsheets;

namespace Comwell.Model
{
    public class Submission
    {
        public string CSF { get; set; }
        public string PlannedDate { get; set; }
        public string SubmittedDate { get; set; }
        public string SubmissionType { get; set; }
        public string Milestone { get; set; }
        public string Title { get; set; }
        public string Number { get; set; }
        public string Revision { get; set; }
        public string Remarks { get; set; }
        public string UpdatedDate { get; set; }

        public static List<Submission> Submissions 
        {
            get
            {
                if (Submissions == null)
                    p_Submissions = new List<Submission>();
                return p_Submissions;
            }
            set
            {
                p_Submissions = value;
            }
        }
        private static List<Submission> p_Submissions;

        public override string ToString()
        {
            return "Document " + Number + " titled " + Title + " Rev. " + Revision + " was submitted on " + SubmittedDate;
        }

        public static void ProcessSubmissions(AtomFeed feed)
        {
            int i = 0;
            Submission submission = new Submission();
            foreach(ListEntry entry in feed.Entries)
            {
                if (i>0)
                {
                    Submissions.Add(submission);
                    i = 0;
                    submission = new Submission();
                }
                foreach(ListEntry.Custom listrow in entry.Elements)
                {
                    switch (i)
                    {
                        case 0: submission.CSF = listrow.Value;
                            break;
                        case 1: submission.PlannedDate = listrow.Value;
                            break;
                        case 2: submission.SubmittedDate = listrow.Value;
                            break;
                        case 3: submission.SubmissionType = listrow.Value;
                            break;
                        case 4: submission.Milestone = listrow.Value;
                            break;
                        case 5: submission.Title = listrow.Value;
                            break;
                        case 6: submission.Number = listrow.Value;
                            break;
                        case 7: submission.Revision = listrow.Value;
                            break;
                        case 9: submission.UpdatedDate = listrow.Value;
                            break;
                    }
                    i++;
                }
            }
        }
    }
}
