namespace Magic_Villa_VillaApi.Logging
{
    /// <summary>
    /// Indicates type of logging message
    /// </summary>
    public enum LoggingTypes
    {
        /// <summary>
        /// Info type of Logger
        /// </summary>
        Info = 1,
        /// <summary>
        /// Error type of Logger
        /// </summary>
        Error = 2,
        Warning = 3,
        Debug = 4
    }

    /// <summary>
    /// Non-generic implementaion of interface ILogging
    /// </summary>
    public class Logging : ILogging
    {
        public void Log(string message, LoggingTypes type)
        {
            switch ((int)type)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write($"Info - ");
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(message);
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"Logger -> Error - {message}.");
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"Logger -> Warning - {message}.");
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine($"Logger -> Debug - {message}.");
                    break;

            }
        }
    }
    /// <summary>
    /// Generic implementaion of interface ILogging
    /// </summary>
    public class LoggingGeneric<T> : ILogging<T>
    {
        public void Log(string message, LoggingTypes type)
        {
            switch ((int)type)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write($"{typeof(T).Name} Info - ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(message + "\n");
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"{typeof(T).Name} Logger -> Error - {message}.");
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"{typeof(T).Name} Logger -> Warning - {message}.");
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine($"{typeof(T).Name} Logger -> Debug - {message}.");
                    break;

            }
        }
    }
}
