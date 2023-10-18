namespace Magic_Villa_VillaApi.Logging
{
    public class Logging : ILogging
    {
        public void Log(string message, string type)
        {
            if(type == "ERROR")
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error - {message}");
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.WriteLine(message);
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }
    }
}
