using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ApiInterpretation
{
    public class ApiInterpreter
    {
        private const string Path = "API";
        private const int QuoteModifier = 3;
        private const int ColonModifier = 1;

        public List<Standard> SixthGradeStandards = new();
        public List<Standard> SeventhGradeStandards = new();
        public List<Standard> EighthGradeStandards = new();
        public List<Standard> AlgebraStandards = new();

        /// <summary>
        /// Returns a list of standards from the API text file in resources.
        /// </summary>
        /// <returns></returns>
        public void SetupStandards()
        {
            List<Standard> standards = new List<Standard>();
            //Load the text file from resources and split at each occurrence of "{" and remove the first line
            string[] lines = Resources.Load<TextAsset>(Path).text.Split('{')[1..];
            //Loop through each line
            foreach (var line in lines)
            {
                Standard standard = new Standard();
                //remove the first line
                string lineWithoutFirstLine = line[(line.IndexOf('\n') + ColonModifier)..];
                //get an array of each line by finding each newline character
                string[] lineParts = lineWithoutFirstLine.Split('\n');
                //get the id value after the first occurrence of ":" and set to the standard's id
                standard.Id =
                    int.Parse(lineParts[0][(lineParts[0].IndexOf(':') + ColonModifier)..lineParts[0].IndexOf(',')]);

                string subjectString =
                    lineParts[1][(lineParts[1].IndexOf(':') + QuoteModifier)..lineParts[1].LastIndexOf('"')];
                standard.Subject = (Subject)System.Enum.Parse(typeof(Subject), subjectString, true);

                standard.Grade =
                    lineParts[2][(lineParts[2].IndexOf(':') + QuoteModifier)..lineParts[2].LastIndexOf('"')];
                standard.Mastery =
                    int.Parse(lineParts[3][(lineParts[3].IndexOf(':') + ColonModifier)..lineParts[3].IndexOf(',')]);
                standard.DomainId =
                    lineParts[4][(lineParts[4].IndexOf(':') + QuoteModifier)..lineParts[4].LastIndexOf('"')];
                standard.DomainName =
                    lineParts[5][(lineParts[5].IndexOf(':') + QuoteModifier)..lineParts[5].LastIndexOf('"')];
                standard.ClusterDescription =
                    lineParts[6][(lineParts[6].IndexOf(':') + QuoteModifier)..lineParts[6].LastIndexOf('"')];
                standard.StandardId =
                    lineParts[7][(lineParts[7].IndexOf(':') + QuoteModifier)..lineParts[7].LastIndexOf('"')];
                standard.StandardDescription =
                    lineParts[8][(lineParts[8].IndexOf(':') + QuoteModifier)..lineParts[8].LastIndexOf('"')];

                if (standard.Grade.Contains("6"))
                {
                    SixthGradeStandards.Add(standard);
                }
                else if (standard.Grade.Contains("7"))
                {
                    SeventhGradeStandards.Add(standard);
                }
                else if (standard.Grade.Contains("8"))
                {
                    EighthGradeStandards.Add(standard);
                }
                else if (standard.Grade.Contains("Algebra"))
                {
                    AlgebraStandards.Add(standard);
                }
            }

            SixthGradeStandards = OrderStandard(SixthGradeStandards);
            SeventhGradeStandards = OrderStandard(SeventhGradeStandards);
            EighthGradeStandards = OrderStandard(EighthGradeStandards);
            AlgebraStandards = OrderStandard(AlgebraStandards);
        }

        //Order a standard by domain name ascending then by cluster name ascending then by standard id ascending
        private List<Standard> OrderStandard(IEnumerable<Standard> standards)
        {
            List<Standard> orderedStandards =
                //Order by domain name ascending If the domain names are the same, order by cluster name ascending If the cluster names are the same, order by standard id ascending
                standards.OrderBy(standard => standard.DomainName)
                    .ThenBy(standard => standard.ClusterDescription)
                    .ThenBy(standard => standard.StandardId).ToList();

            return orderedStandards;
        }
    }
}