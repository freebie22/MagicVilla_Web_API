namespace Magic_Villa_VillaApi.Logging
{
    public interface ILogging
    {
        void Log(string message, LoggingTypes type);
    }

    public interface ILogging <out T> 
    {
        /// <summary>
        /// Writes in console information about action
        /// </summary>
        /// <param name="message">Message that writes in console</param>
        /// <param name="type">Type of logger</param>
        void Log(string message, LoggingTypes type);
    }

}
