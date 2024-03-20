using sportDataLayer;
using System.Data;

namespace SportCllubeBuisness
{
    public class clsNaitonalityBuisness
    {
        public enum enMode { add, update };
        public enMode mode;
        public int id { get; set; }

        public string name { get; set; }

        public clsNaitonalityBuisness()
        {
            mode = enMode.add;
            id = 0;
            name = "fack";
        }

        private clsNaitonalityBuisness(enMode mode, int id, string name)
        {
            this.mode = mode;
            this.id = id;
            this.name = name;
        }

        public static clsNaitonalityBuisness findNationalityByID(int id)
        {
            string name = "";
            if (clsNationalityData.findNationalityByID(id, ref name))
            {
                return new clsNaitonalityBuisness(enMode.update, id, name);
            }
            return null;
        }

        public static clsNaitonalityBuisness findNationalityByName(string name)
        {
            int addBy = 0;

            int id = 0;
            if (clsNationalityData.findNationalityByName(ref id, name))
            {
                return new clsNaitonalityBuisness(enMode.update, id, name);
            }
            return null;
        }


        public static DataTable getAllNationality()
        {
            return clsNationalityData.getAllNationality();
        }
    }
}
