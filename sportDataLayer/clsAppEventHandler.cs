using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sportDataLayer
{
    public  class clsAppEventHandler
    {
        private static string _appName { get {return  "SportCloupApp"; } }

     
        private static void _createLogIfItNoteCreated()
        {
            if (!EventLog.Exists(_appName))
            {
                EventLog.CreateEventSource(_appName,"Application");
            }
        }


      
        public static void createNewEventLog(string error)
        {
          _createLogIfItNoteCreated();
          EventLog.WriteEntry(_appName, error);
        }
    }
}
