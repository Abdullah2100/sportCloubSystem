using sportDataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportCllubeBuisness
{
    public class clsNaitonalityBuisness
    {
        public enum enNationalityMode { add, update};
        public enNationalityMode mode;
        public int id { get; set; }

        public string name { get; set; }    

        public clsNaitonalityBuisness() {
            mode = enNationalityMode.add;
            id = 0;
            name = "fack";
        }

        private clsNaitonalityBuisness(enNationalityMode mode, int id, string name)
        {
            this.mode = mode;
            this.id = id;
            this.name = name;
        }

        public static clsNaitonalityBuisness findNationalityByID(int id)
        {
            string name = "";
            if(clsNationalityData.findNationalityByID(id,ref name))
            {
                return new  clsNaitonalityBuisness(enNationalityMode.update, id, name);
            }
            return null;
        }

        public static clsNaitonalityBuisness findNationalityByName(string name)
        {
            int  id = 0;
            if (clsNationalityData.findNationalityByName(ref id,  name))
            {
                return new clsNaitonalityBuisness(enNationalityMode.update, id, name);
            }
            return null;
        }


        public static DataTable getAllNationality()
        {
            return clsNationalityData.getAllNationality();
        }
    }
}
