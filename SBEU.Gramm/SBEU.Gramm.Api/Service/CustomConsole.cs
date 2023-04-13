namespace SBEU.Gramm.Api.Service
{
    public static class CustomConsole
    {
        /* A semaphore that is used to prevent multiple threads from writing to the console at the same
        time. */
        private static SemaphoreSlim semsl = new SemaphoreSlim(1);
        /// <summary>
        /// It takes a list of ConsoleLog objects, and writes them to the console
        /// </summary>
        public static async Task WriteLog(params ConsoleLog[] logs)
        {
            semsl.Wait();
            Write(ConsoleLog.CL($@"[{DateTime.Now:dd.MM HH:mm}] ", ConsoleColor.DarkGray));
            foreach (var log in logs)
            {
                Write(log);
            }
            semsl.Release();
        }

        /// <summary>
        /// It writes the text to the console with the specified color
        /// </summary>
        /// <param name="ConsoleLog">The object that contains the text and color to write to the
        /// console.</param>
        private static void Write(ConsoleLog log)
        {
            Console.ForegroundColor = log.Color;
            if (log.NewLine) 
                Console.WriteLine(log.Text);
            else 
                Console.Write(log.Text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    public class ConsoleLog
    {
        public string Text { get; set; }
        public ConsoleColor Color { get; set; } 
        /* A property that is used to determine whether to write the text to the console with a new
        line or not. */
        public bool NewLine { get; set; } 

        /// <summary>
        /// It returns a new instance of the ConsoleLog class, which is a class that I created to hold
        /// the text, color, and newline properties
        /// </summary>
        /// <param name="text">The text to be displayed in the console.</param>
        /// <param name="ConsoleColor">The color of the text.</param>
        /// <param name="newLine">If true, the text will be printed on a new line.</param>
        /// <returns>
        /// A new instance of the ConsoleLog class.
        /// </returns>
        public static ConsoleLog CL(string text, ConsoleColor color = ConsoleColor.White, bool newLine = false)
        {
            return new ConsoleLog { Color = color, NewLine = newLine, Text = text };
        }
    }
}
