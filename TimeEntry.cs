using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
namespace RapiddAssignment
{
    public class TimeEntry
    {
        public string Id { get; set; }
        public string EmployeeName { get; set; }
        public DateTime StarTimeUtc { get; set; }
        public DateTime EndTimeUtc { get; set; }
        public string EntryNotes { get; set; }

    }
    public class TimeEntryDetails { 
        public async Task<List<TimeEntry>> GetTimeEntriesAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<TimeEntry>>(json);
            }
        }
        public double GetTotalHoursWorked(TimeEntry entry)
        {
            DateTime startTime = entry.StarTimeUtc;
            DateTime endTime = entry.EndTimeUtc;

            // Handle cases where the end time is earlier than the start time
            if (endTime < startTime)
            {
                Console.WriteLine($"Error: End time is earlier than start time for entry {entry.Id}");
                return 0; // or handle as appropriate
            }

            // Calculate the duration
            TimeSpan duration = endTime - startTime;

            // Return the total hours
            return duration.TotalHours;
        }
    }
}
