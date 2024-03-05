using System.Configuration;

using System.Diagnostics;

namespace sportDataLayer
{
    public class clsAppEventHandler
    {

        static string appEventName = ConfigurationManager.AppSettings["appEventName"];
        private static void _createLogIfItNoteCreated()
        {
            if (!EventLog.Exists(appEventName))
            {
                EventLog.CreateEventSource(appEventName, "Application");
            }

        }



        public static void createNewEventLog(string error)
        {
            _createLogIfItNoteCreated();
            EventLog.WriteEntry(appEventName, error);
        }
    }
}
