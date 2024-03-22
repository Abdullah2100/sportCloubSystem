using sportDataLayer;
using System;
using System.Data;

namespace SportCllubeBuisness
{
    public class clsCoachBuisness
    {
        public enum enMode { add, update };
        public enMode mode { get; set; }
        public int id { get; set; }
        public int personID { get; set; }
        public int? addBy { get; set; }
        public DateTime startTraingDate { get; set; }
        public DateTime? endTraingDate { get; set; }
        public bool isActive { get; set; }

        public clsPeopleBuisness personInfo;

        public string fullName
        {
            get { return personInfo.fullName; }
        }


        public clsCoachBuisness()
        {

            this.mode = enMode.add;
            this.id = 0;
            this.personID = 0;
            this.startTraingDate = DateTime.Now;
            this.endTraingDate = null;
            this.isActive = true;
            this.addBy = null;

        }

        private clsCoachBuisness(enMode mode, int id, int personID, int? addBy, DateTime startTraingDate, DateTime? endTraingDate, bool isActive)
        {

            this.mode = mode;
            this.id = id;
            this.personID = personID;
            this.addBy = addBy;
            this.startTraingDate = startTraingDate;
            this.endTraingDate = endTraingDate;
            this.isActive = isActive;
            this.personInfo = clsPeopleBuisness.findPeopleByID(personID);

        }



        public static clsCoachBuisness findCoachByID(int id)
        {
            int personID = 0;
            int? addBy = null;
            DateTime startTraingDate = DateTime.Now;
            DateTime? endTraingDate = null;
            bool isActive = false;


            if (clsCoachData.findCoachByID(id, ref personID, ref addBy, ref startTraingDate, ref endTraingDate, ref isActive))
            {
                return new clsCoachBuisness(enMode.update, id, personID, addBy, startTraingDate, endTraingDate, isActive); ;
            }

            return null;
        }

        public static clsCoachBuisness findCoachByFullName(string fullname)
        {
            clsPeopleBuisness _person = clsPeopleBuisness.findPeopleByFullName(fullname);
            if (_person == null)
            {
                return null;
            }


            int personID = _person.id;
            int? addBy = null;
            int id = 0;
            DateTime startTraingDate = DateTime.Now;
            DateTime? endTraingDate = null;
            bool isActive = false;

            if (clsCoachData.findCoachByPersonID(ref id, personID, ref addBy, ref startTraingDate, ref endTraingDate, ref isActive))
            {
                return new clsCoachBuisness(enMode.update, id, personID, addBy, startTraingDate, endTraingDate, isActive); ;
            }

            return null;
        }



        public static clsCoachBuisness findCoachByPersonID(int personID)
        {
            int id = 0;
            int? addBy = null;
            DateTime startTraingDate = DateTime.Now;
            DateTime? endTraingDate = null;
            bool isActive = false;
            if (clsCoachData.findCoachByPersonID(ref id, personID, ref addBy, ref startTraingDate, ref endTraingDate, ref isActive))
            {
                return new clsCoachBuisness(enMode.update, id, personID, addBy, startTraingDate, endTraingDate, isActive); ;
            }

            return null;
        }

        private bool _add()
        {
            this.id = clsCoachData.createCoach(
                  this.personID,
                  this.addBy,
                  this.startTraingDate,
                  this.endTraingDate,
                  this.isActive
                );
            return (this.id != 0);
        }

        private bool _update()
        {
            return clsCoachData.updateCoach(
                this.id,
                this.personID,
                this.addBy,
                this.startTraingDate,
                this.endTraingDate,
                this.isActive);
        }


        public virtual bool save()
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
                            return true;
                        return false;
                    }
            }
            return false;
        }


        public static DataTable getAllCoachs()
        {
            return clsCoachData.getAllCoaches();
        }

        public static DataTable getAllActiveCoachsName()
        {
            return clsCoachData.getAllActiveCoachsName();
        }

        public static bool deleteCoach(int id)
        {
            return clsCoachData.deleteCoach(id);
        }


        public static bool isCoachExistByPerson(int personID)
        {
            return clsCoachData.isCoachExistByPersonID(personID);
        }

        public static bool isCoachExistByID(int id)
        {
            return clsCoachData.isCoachExistByID(id);
        }

        public static bool isCoachActive(int id)
        {
            return clsCoachData.isCoachActive(id);
        }
        public static bool activateCoach(int id)
        {
            return clsCoachData.UpdateCoachState(id, true);
        }
        public static bool deActivateCoach(int id)
        {
            return clsCoachData.UpdateCoachState(id, false);
        }


    }
}
