using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapiddAssignment
{
    internal class TableGenerator
    {
        public string GenerateHtmlTable(List<TimeEntry> timeEntries)
        {
            TimeEntryDetails details = new TimeEntryDetails();
            var groupedEntries = timeEntries
                .GroupBy(e => e.EmployeeName)
                .Select(g => new
                {
                    EmployeeName = g.Key,
                    TotalHoursWorked = g.Sum(e => details.GetTotalHoursWorked(e))
                })
                .OrderByDescending(e => e.TotalHoursWorked);

            string html = "<html><body><table border='1'><tr><th>Name</th><th>Total Time Worked (hours)</th></tr>";
            foreach (var entry in groupedEntries)
            {
                string rowColor = entry.TotalHoursWorked < 100 ? " style='background-color:red;'" : "";
                html += $"<tr{rowColor}><td>{entry.EmployeeName}</td><td>{entry.TotalHoursWorked:F2}</td></tr>";
            }
            html += "</table></body></html>";

            return html;
        }
        public void SaveHtmlToFile(string html, string filePath)
        {
            File.WriteAllText(filePath, html);
        }
    }
}
