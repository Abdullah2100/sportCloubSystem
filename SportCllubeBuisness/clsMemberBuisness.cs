using sportDataLayer;
using System.Data;

namespace SportCllubeBuisness
{
    public class clsMemberBuisness
    {
        public enum enMode { add, update };
        public enMode mode { get; set; }
        public int id { get; set; }
        public int personID { get; set; }
        public bool isActive { get; set; }

        public clsPeopleBuisness personInfo;

        public clsMemberBuisness()
        {
            this.mode = enMode.add;
            this.id = 0;
            this.personID = 0;
            this.isActive = false;

        }

        private clsMemberBuisness(enMode mode, int id, int personID, bool isActive)
        {
            this.mode = mode;
            this.id = id;
            this.personID = personID;
            this.isActive = isActive;
            personInfo = clsPeopleBuisness.findPeopleByID(personID);
        }


        public static clsMemberBuisness findMemberByID(int id)
        {
            int personID = 0;
            bool isActive = false;
            if (clsMemberData.findMemberByID(id, ref personID, ref isActive))
            {
                return new clsMemberBuisness(enMode.update, id, personID, isActive);
            }

            return null;
        }


        public static clsMemberBuisness findMemberByPersonID(int personID)
        {
            int id = 0;
            bool isActive = false;
            if (clsMemberData.findMemberByPersonID(ref id, personID, ref isActive))
            {
                return new clsMemberBuisness(enMode.update, id, personID, isActive);
            }

            return null;
        }

        private bool _add()
        {
            this.id = clsMemberData.createMember(
                this.personID,
                this.isActive
                );
            return (this.id != 0);
        }

        private bool _update()
        {
            return clsMemberData.updateMember(
                           this.id,
                           this.personID,
                           this.isActive);
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
                            return true;
                        return false;
                    }
            }
            return false;
        }


        public static DataTable getAllMember()
        {
            return clsMemberData.getAllMember();
        }

        public static bool deleteMemberByID(int id)
        {
            return clsMemberData.deleteMemberByID(id);
        }


        public static bool isMemberExistByPersonID(int personID)
        {
            return clsMemberData.isMemberExistByPeopleID(personID);
        }

        public static bool isMemberExistById(int id)
        {
            return clsMemberData.isMemberExistByID(id);
        }

        public static bool activateMember(int id)
        {
            return clsMemberData.UpdatememberState(id, true);
        }
        public static bool deActivateMember(int id)
        {
            return clsMemberData.UpdatememberState(id, false);
        }

        public static bool isMemberActiveByID(int id)
        {
            return clsMemberData.isMemberActivaeByID(id);
        }
    }
}
