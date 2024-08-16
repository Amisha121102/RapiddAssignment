using System;
using System.Collections.Generic;

namespace RapiddAssignment
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            TimeEntryDetails details = new TimeEntryDetails();
            TableGenerator tableGenerator = new TableGenerator();
            PieChartGenerator pieChartGenerator = new PieChartGenerator();
            string key = "vO17RnE8vuzXzPJo5eaLLjXjmRW07law99QTD90zat9FfOQJKKUcgQ==";
            string apiUrl = "https://rc-vault-fap-live-1.azurewebsites.net/api/gettimeentries?code=vO17RnE8vuzXzPJo5eaLLjXjmRW07law99QTD90zat9FfOQJKKUcgQ==";
            List<TimeEntry> timeEntries = details.GetTimeEntriesAsync(apiUrl).Result;

            // Generate HTML Table
            string html = tableGenerator.GenerateHtmlTable(timeEntries);
            tableGenerator.SaveHtmlToFile(html, "EmployeeWorkHours.html");

            // Generate Pie Chart
            pieChartGenerator.GeneratePieChart("EmployeeWorkHours.png",timeEntries);

        }
    }
}
