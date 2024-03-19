using sportDataLayer;

namespace SportCllubeBuisness
{
    public class clsEventHandlerBuisness
    {

        public static void addingEvent(string message)
        {
            clsAppEventHandler.createNewEventLog(message);
        }


    }
}
