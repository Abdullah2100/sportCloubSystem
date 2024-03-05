using sportDataLayer;
using System;
using System.Data;

namespace SportCllubeBuisness
{
    public class clsCoachBuisness : clsPeopleBuisness
    {
        public enum enMode { add, update };
        public enMode mode { get; set; }
        public int id { get; set; }
        public int personID { get; set; }
        public DateTime startTraingDate { get; set; }
        public DateTime? endTraingDate { get; set; }
        public bool isActive { get; set; }



        public clsCoachBuisness()
        {

            this.mode = enMode.add;
            this.id = 0;
            this.personID = 0;
            this.startTraingDate = DateTime.Now;
            this.endTraingDate = null;
            this.isActive = true;

        }

        private clsCoachBuisness(enMode mode, int id, int personID, DateTime startTraingDate, DateTime? endTraingDate, bool isActive)
        {
            clsPeopleBuisness personInfo = clsPeopleBuisness.findPeopleByID(personID);
            base.mode = personInfo.mode;
            base.id = personInfo.id;
            base.firstName = personInfo.firstName;
            base.secondName = personInfo.secondName;
            base.thirdName = personInfo.thirdName;
            base.familyName = personInfo.familyName;
            base.brithday = personInfo.brithday;
            base.gender = personInfo.gender;
            base.nationalityID = personInfo.nationalityID;
            base.address = personInfo.address;
            base.phone = personInfo.phone;


            this.mode = mode;
            this.id = id;
            this.personID = personID;
            this.startTraingDate = startTraingDate;
            this.endTraingDate = endTraingDate;
            this.isActive = isActive;

        }



        public static clsCoachBuisness findCoachByID(int id)
        {
            int personID = 0;
            DateTime startTraingDate = DateTime.Now;
            DateTime? endTraingDate = null;
            bool isActive = false;

            if (clsCoachData.findCoachByID(id, ref personID, ref startTraingDate, ref endTraingDate, ref isActive))
            {
                return new clsCoachBuisness(enMode.update, id, personID, startTraingDate, endTraingDate, isActive); ;
            }

            return null;
        }

        public clsCoachBuisness findCoachByFullName(string fullname)
        {
            clsPeopleBuisness _person = clsPeopleBuisness.findPeoplebyFullName(fullname);
            if (_person == null)
            {
                return null;
            }

            base.mode = _person.mode;
            base.id = _person.id;
            base.firstName = _person.firstName;
            base.secondName = _person.secondName;
            base.thirdName = _person.thirdName;
            base.familyName = _person.familyName;
            base.brithday = _person.brithday;
            base.gender = _person.gender;
            base.nationalityID = _person.nationalityID;
            base.address = _person.address;
            base.phone = _person.phone;

            int id = 0;
            int personID = base.id;
            DateTime startTraingDate = DateTime.Now;
            DateTime? endTraingDate = null;
            bool isActive = false;

            if (clsCoachData.findCoachByPersonID(ref id, personID, ref startTraingDate, ref endTraingDate, ref isActive))
            {
                return new clsCoachBuisness(enMode.update, id, personID, startTraingDate, endTraingDate, isActive); ;
            }

            return null;
        }



        public static clsCoachBuisness findCoachByPersonID(int personID)
        {
            int id = 0;
            DateTime startTraingDate = DateTime.Now;
            DateTime? endTraingDate = null;
            bool isActive = false;
            if (clsCoachData.findCoachByPersonID(ref id, personID, ref startTraingDate, ref endTraingDate, ref isActive))
            {
                return new clsCoachBuisness(enMode.update, id, personID, startTraingDate, endTraingDate, isActive); ;
            }

            return null;
        }

        private bool _add()
        {
            this.id = clsCoachData.createCoach(
                  this.personID,
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

        public static bool deleteMemberByID(int id)
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
