using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBEU.Gramm.Middleware
{
    public class ImageConvertion
    {
        public static async Task<string> ProcessImage(string fileName)
        {
            var nname = Guid.NewGuid().ToString() + Guid.NewGuid();
            var process = new ProcessStartInfo();
            process.FileName = "convert";
            process.Arguments = $"Contents/Temp/{fileName} Contents/{nname}.jpg";
            var task= Process.Start(process);
            await task.WaitForExitAsync();
            process.Arguments = $"Contents/Temp/{fileName} -resize 45% Contents/{nname}-middle.jpg";
            task = Process.Start(process);
            await task.WaitForExitAsync();
            process.Arguments = $"Contents/Temp/{fileName} -resize 15% Contents/{nname}-low.jpg";
            task = Process.Start(process);
            await task.WaitForExitAsync();
            File.Delete($"Contents/Temp/"+fileName);
            File.Move($"Contents/{nname}.jpg", $"Contents/{nname}");
            File.Move($"Contents/{nname}-middle.jpg", $"Contents/{nname}-middle");
            File.Move($"Contents/{nname}-low.jpg", $"Contents/{nname}-low");
            return nname;
        }
    }
}
