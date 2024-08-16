using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace RapiddAssignment
{
    public class PieChartGenerator
    {
        public void GeneratePieChart(string filePath,List<TimeEntry> timeEntries)
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

            List<double> values = new List<double>();
            List<string> labels = new List<string>();

            foreach (var entry in groupedEntries)
            {
                values.Add(entry.TotalHoursWorked);
                labels.Add(entry.EmployeeName);
            }

            if (values.Count != labels.Count || values.Count == 0)
            {
                throw new ArgumentException("Values and labels must have the same length and be non-empty.");
            }

            int width = 600;
            int height = 400;
            using (Bitmap bitmap = new Bitmap(width, height))
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
                Rectangle rect = new Rectangle(0, 0, width, height);
                float total = 0;
                foreach (var value in values)
                {
                    total += (float)value;
                }

                float startAngle = 0;
                for (int i = 0; i < values.Count; i++)
                {
                    float sweepAngle = (float)(values[i] / total * 360);
                    g.FillPie(new SolidBrush(GetColor(i)), rect, startAngle, sweepAngle);
                    startAngle += sweepAngle;
                }

                // Save the image
                bitmap.Save(filePath, ImageFormat.Png);
            }
        }

        private static Color GetColor(int index)
        {
            // Returns a color based on the index for better visual differentiation
            switch (index)
            {
                case 0: return Color.Red;
                case 1: return Color.Green;
                case 2: return Color.Blue;
                case 3: return Color.Orange;
                case 4: return Color.Purple;
                case 5: return Color.Yellow;
                case 6: return Color.Orchid;
                case 7: return Color.GreenYellow;
                case 8: return Color.AliceBlue;
                case 9: return Color.Black;
                case 10: return Color.Olive;
                default: return Color.Gray;
            }
        }
    }
}