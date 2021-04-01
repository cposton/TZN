using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZN
{
    class Program
    {
        private const string DateTimeFormat = "yyyy-MM-dd HH:mm";

        static void Main(string[] args)
        {
            Directory.CreateDirectory("Output");

            var timeZones = TimeZoneInfo.GetSystemTimeZones().OrderBy(z => z.Id);

            foreach (var timeZone in timeZones)
            {
                Console.WriteLine("Processing {0}...", timeZone.Id);

                using (var file = File.OpenWrite($"Output\\{timeZone.Id}.txt"))
                {
                    using (var writer = new StreamWriter(file))
                    {
                        var testDateTime = new DateTimeOffset(2021, 1, 1, 0, 0, 0, TimeSpan.Zero);

                        do
                        {
                            var converted = TimeZoneInfo.ConvertTime(testDateTime, timeZone);

                            writer.WriteLine($"{testDateTime.ToString(DateTimeFormat)},{converted.ToString(DateTimeFormat)}");

                            testDateTime = testDateTime.AddMinutes(10);
                        } while (testDateTime.Year < 2051);

                        writer.Close();
                    }
                }
            }
        }
    }
}
