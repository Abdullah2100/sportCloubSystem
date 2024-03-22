using sportDataLayer;
using System;
using System.Data;

namespace SportCllubeBuisness
{
    public class clsSportBuisness
    {
        public enum enMode { add, update };
        public enMode mode { get; set; }
        public int id { get; set; }
        public int? addBy { get; set; }
        public string name { get; set; }
        public DateTime createdAt { get; set; }
        public bool isActive { get; set; }

        public clsSportBuisness()
        {
            this.mode = enMode.add;
            this.id = 0;
            this.addBy = null;
            this.name = "";
            this.createdAt = DateTime.Now;
            this.isActive = true;
        }

        private clsSportBuisness(enMode mode, int id, int? addBy, string name, DateTime createdAt, bool isActive)
        {
            this.mode = mode;
            this.id = id;
            this.addBy = addBy;
            this.name = name;
            this.createdAt = createdAt;
            this.isActive = isActive;
        }

        public static clsSportBuisness findSport(int id)
        {
            int? addBy = null;
            string name = "ahemd";
            DateTime createdAt = DateTime.Now;
            bool isActive = true;

            if (clsSportData.findSportByID(id, ref addBy, ref name, ref createdAt, ref isActive))
            {
                return new clsSportBuisness(enMode.update, id, addBy, name, createdAt, isActive);
            }
            return null;
        }

        public static clsSportBuisness findSport(string name)
        {
            int id = 0;
            int? addBy = null;
            DateTime createdAt = DateTime.Now;
            bool isActive = true;

            if (clsSportData.findSportByName(ref id, ref addBy, name, ref createdAt, ref isActive))
            {
                return new clsSportBuisness(enMode.update, id, addBy, name, createdAt, isActive);
            }
            return null;
        }

        private bool _add()
        {
            return clsSportData.addNewSport(this.addBy, this.name);
        }
        private bool _update()
        {
            return clsSportData.updateSport(this.id, this.addBy, this.name, this.isActive);
        }

        public bool save()
        {
            switch (mode)
            {
                case enMode.add:
                    {
                        if (_add())
                        {
                            return true;
                        }
                        return false;
                    }
                case enMode.update:
                    {
                        if (_update())
                        {
                            return true;
                        }
                        return false;
                    }

            }
            return false;
        }

        public static DataTable getAllSport()
        {
            return clsSportData.getAllSport();
        }

        public static DataTable getAllActiveSportName()
        {
            return clsSportData.getAllActiveSportName();
        }

        public static bool isSportExistByName(string name)
        {
            return clsSportData.isExistByName(name);
        }

        public bool isSportExistByName()
        {
            return clsSportData.isExistByName(name);
        }

        public static bool isSportActiveByID(int id)
        {
            return clsSportData.isSportActiveByID(id);
        }

        public static bool isSportActiveByName(string name)
        {
            return clsSportData.isSportActiveByName(name);
        }


        public static bool deleteSport(int id)
        {
            return clsSportData.deleteSport(id);
        }

        public static bool activeSport(int id)
        {
            return clsSportData.updateSportState(id, true);
        }

        public bool activeSport()
        {
            return clsSportData.updateSportState(this.id, true);
        }

        public static bool deActiveSport(int id)
        {
            return clsSportData.updateSportState(id, false);
        }

        public bool deActiveSport()
        {
            return clsSportData.updateSportState(this.id, false);
        }

    }
}
