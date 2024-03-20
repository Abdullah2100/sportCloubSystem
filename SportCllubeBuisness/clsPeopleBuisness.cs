using sportDataLayer;
using System;
using System.Data;

namespace SportCllubeBuisness
{
    public class clsPeopleBuisness
    {
        public enum enMode { add, update };
        public enMode mode { get; set; }
        public int id { get; set; }
        public string firstName { get; set; }
        public string secondName { get; set; }
        public string thirdName { get; set; }
        public string familyName { get; set; }
        public DateTime brithday { get; set; }
        public bool gender { get; set; }
        public int nationalityID { get; set; }
        public clsNaitonalityBuisness nationalityIfno;
        public string address { get; set; }
        public string phone { get; set; }
        public string fullName
        {
            get { return this.firstName + " " + this.secondName + " " + this.thirdName + " " + this.familyName; }
        }

        public clsPeopleBuisness()
        {
            this.mode = enMode.add;
            this.id = 0;
            this.firstName = "";
            this.secondName = "";
            string thirdName = "";
            string familyName = "";
            DateTime brithday = DateTime.Now;
            bool gender = false;
            int nationalityID = 0;
            string address = "";
            string phone = "";
        }

        private clsPeopleBuisness(enMode mode, int id, string firstName, string secondName, string thirdName, string familyName, DateTime brithday, bool gender, int nationalityID, string address, string phone)
        {
            this.mode = mode;
            this.id = id;
            this.firstName = firstName;
            this.secondName = secondName;
            this.thirdName = thirdName;
            this.familyName = familyName;
            this.brithday = brithday;
            this.gender = gender;
            this.nationalityID = nationalityID;
            this.nationalityIfno = clsNaitonalityBuisness.findNationalityByID(nationalityID);
            this.address = address;
            this.phone = phone;
        }

        public static clsPeopleBuisness findPeopleByID(int id)
        {
            string firstName = "";
            string secondName = "";
            string thirdName = "";
            string familyName = "";
            DateTime brithday = DateTime.Now;
            bool gender = false;
            int nationalityID = 0;
            string address = "";
            string phone = " ";
            if (clsPeoplesData.findPeoplesByID(id, ref firstName, ref secondName, ref thirdName, ref familyName, ref brithday, ref gender, ref nationalityID, ref address, ref phone))
            {

                return new clsPeopleBuisness(enMode.update, id, firstName, secondName, thirdName, familyName, brithday, gender, nationalityID, address, phone);
            }
            return null;
        }

        protected static clsPeopleBuisness findPeopleByFullName(string fullName)
        {
            int id = 0;
            string firstName = "";
            string secondName = "";
            string thirdName = "";
            string familyName = "";
            DateTime brithday = DateTime.Now;
            bool gender = false;
            int nationalityID = 0;
            string address = "";
            string phone = " ";
            if (clsPeoplesData.findPeoplesByFullName(fullName, ref id, ref firstName, ref secondName, ref thirdName, ref familyName, ref brithday, ref gender, ref nationalityID, ref address, ref phone))
            {

                return new clsPeopleBuisness(enMode.update, id, firstName, secondName, thirdName, familyName, brithday, gender, nationalityID, address, phone);
            }
            return null;
        }


        public static clsPeopleBuisness findPeopleByPhone(string phone)
        {
            string firstName = "";
            string secondName = "";
            string thirdName = "";
            string familyName = "";
            DateTime brithday = DateTime.Now;
            bool gender = false;
            int nationalityID = 0;
            string address = "";
            int id = 0;
            if (clsPeoplesData.findPeoplesByPhone(ref id, ref firstName, ref secondName, ref thirdName, ref familyName, ref brithday, ref gender, ref nationalityID, ref address, phone))
            {

                return new clsPeopleBuisness(enMode.update, id, firstName, secondName, thirdName, familyName, brithday, gender, nationalityID, address, phone);
            }
            return null;
        }
        private bool _add()
        {
            this.id = clsPeoplesData.createPeoples(
                this.id,
                this.firstName,
                this.secondName,
                this.thirdName,
                this.familyName,
                this.brithday,
                this.gender,
                this.nationalityID,
                this.address,
                this.phone);
            return (this.id != 0);
        }

        private bool _update()
        {
            return clsPeoplesData.updatePeoples(
                           this.id,
                           this.firstName,
                           this.secondName,
                           this.thirdName,
                           this.familyName,
                           this.brithday,
                           this.gender,
                           this.nationalityID,
                           this.address,
                           this.phone);
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


        public static DataTable getAllPeople()
        {
            return clsPeoplesData.getAllPeoples();
        }

        public static bool deletePeople(int id)
        {
            return clsPeoplesData.deletePoeplByID(id);
        }


        public static bool isPeopleExistByPhon(string pheon)
        {
            return clsPeoplesData.isPersonExistByPhone(pheon);
        }
    }
}
