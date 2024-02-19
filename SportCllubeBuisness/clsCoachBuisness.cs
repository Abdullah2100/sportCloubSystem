using sportDataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static SportCllubeBuisness.clsPeopleBuisness;

namespace SportCllubeBuisness
{
    public class clsCoachBuisness:clsPeopleBuisness
    {
      public enum enCoachMode { add, update};
        public enCoachMode mode {  get; set; }
        public    int id { get; set; }
        public int personID { get; set; }
        public DateTime startTraingDate { get; set; }
        public DateTime? endTraingDate { get; set; }
        public string personalImage { get; set; }
        public bool isActive  { get; set; }
            
       

        public clsCoachBuisness()
        {

            this.mode = enCoachMode.add;
            this.id = 0;
            this.personID = 0;
            this.startTraingDate = DateTime.Now;
            this.endTraingDate = null;
            this.personalImage = "";
            this.isActive = true;

        }
    
        private clsCoachBuisness(enCoachMode mode, int id, int personID, DateTime startTraingDate, DateTime? endTraingDate, string personalImage, bool isActive)
        {
            clsPeopleBuisness personInfo = clsPeopleBuisness.findPeopleByID(personID);
            base.mode = personInfo.mode;
            base.id = personInfo.id;
            base.firstName = personInfo.firstName;
            base.secondName = personInfo.secondName;
            base.thirdName =  personInfo.thirdName;
            base.familyName = personInfo.familyName;
            base.brithday =     personInfo.brithday;
            base.gender = personInfo.gender;
            base.nationalityID = personInfo.nationalityID;
            base.address = personInfo.address; 
            base.phone = personInfo.phone;
            this.mode = mode;
            this.id = id;
            this.personID = personID;
            this.startTraingDate = startTraingDate;
            this.endTraingDate = endTraingDate;
            this.personalImage = personalImage;
            this.isActive = isActive;

        }

        
      
        public static  clsCoachBuisness findCoachByID (int id)
        {
            int personID = 0;
            DateTime startTraingDate = DateTime.Now;
            DateTime? endTraingDate = null;
            string personalImage = "";
                bool isActive = false;
            if (clsCoachData.findCoachByID(id, ref personID, ref startTraingDate, ref endTraingDate, ref personalImage, ref isActive))
            {
                return new  clsCoachBuisness(enCoachMode.update,id,  personID,  startTraingDate,  endTraingDate,  personalImage,  isActive); ;
            }
            
            return null;
        }


        public static clsCoachBuisness findCoachByPersonID(int personID)
        {
            int id = 0;
            DateTime startTraingDate = DateTime.Now;
            DateTime? endTraingDate = null;
            string personalImage = "";
            bool isActive = false;
            if (clsCoachData.findCoachByPersonID(ref id,  personID, ref startTraingDate, ref endTraingDate, ref personalImage, ref isActive))
            {
                return new clsCoachBuisness(enCoachMode.update, id, personID, startTraingDate, endTraingDate, personalImage, isActive); ;
            }

            return null;
        }

        private bool _add()
        {
            this.id= clsCoachData.createCoach(
                  this.personID ,
                  this.startTraingDate,
                  this.endTraingDate,
                  this.personalImage ,
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
                this.personalImage,
                this.isActive) ;
        }
   
  
        public virtual bool save()
        {
          
            

            switch(mode)
            {
                case enCoachMode.add:
                    {
                        if (_add())
                        {
                            return true;
                        }
                        return false;
                    }
                case enCoachMode.update:
                    {
                        if (_update())
                            return true;
                        return false;
                    } 
            }
            return false;
        } 
   
   
        public static  DataTable getAllCoachs()
        {
            return clsCoachData.getAllCoaches();
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
            return clsCoachData.UpdateCoachState(id,true);
        }
        public static bool deActivateCoach(int id)
        {
            return clsCoachData.UpdateCoachState(id, false);
        }

       
    }
}
