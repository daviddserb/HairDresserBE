using System.Reflection;

namespace hairDresser.Presentation.TimeLogger
{
    public static class MethodTimeLogger
    {
        public static ILogger Logger;
        public static void Log(MethodBase methodBase, TimeSpan timeSpan, string message)
        {
            Logger.LogTrace("{Class}.{Method} - {Message} in {Duration}",
                methodBase.DeclaringType!.Name, methodBase.Name, message, timeSpan);
        }
    }
}