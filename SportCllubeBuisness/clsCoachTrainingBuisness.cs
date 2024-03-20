using sportDataLayer;
using System.Data;

namespace SportCllubeBuisness
{
    public class clsCoachTrainingBuisness
    {
        public enum enMode { add, update };
        public enMode mode { get; set; }
        public int id { get; set; }
        public int coachID { get; set; }
        public int sportID { get; set; }
        public int addBy { get; set; }
        public int trainingDay { get; set; }
        public string dayilyStartAt { get; set; }
        public string dayilyEndAt { get; set; }
        public bool isAvaliable { get; set; }
        public decimal fee { get; set; }
        public clsCoachBuisness coatchInfo { get; set; }
        public clsSportBuisness sprotInfo { get; set; }


        public clsCoachTrainingBuisness()
        {

            this.mode = enMode.add;
            this.id = 0;
            this.coachID = 0;
            this.addBy = 0;
            this.sportID = 0;
            this.trainingDay = 0;
            this.dayilyStartAt = "";
            this.dayilyEndAt = "";
            this.isAvaliable = false;
            this.fee = 0;


        }

        private clsCoachTrainingBuisness(enMode mode, int id, int coachID, int addBy, int sportID, int trainingDay, string dayilyStartAt, string dayilyEndAt, bool isAvaliable, decimal fee)
        {
            this.mode = mode;
            this.id = id;
            this.coachID = coachID;
            this.addBy = addBy;
            this.sportID = sportID;
            this.trainingDay = trainingDay;
            this.dayilyStartAt = dayilyStartAt;
            this.dayilyEndAt = dayilyEndAt;
            this.isAvaliable = isAvaliable;
            this.fee = fee;
            this.sprotInfo = clsSportBuisness.findSport(sportID);
            this.coatchInfo = clsCoachBuisness.findCoachByID(coachID);
        }

        public static clsCoachTrainingBuisness findCoachTrainingByID(int id)
        {

            int coachID = 0;
            int addBy = 0;
            int sportID = 0;
            bool isAvaliable = false;
            string dayilyStartAt = "";
            string dayilyEndAt = "";
            int trainingDay = 0;
            decimal fee = 0;

            if (clsCoachsTraingingData.findCoachsTrainging(id, ref coachID, ref addBy, ref sportID, ref isAvaliable, ref dayilyStartAt, ref dayilyEndAt, ref trainingDay, ref fee))
            {
                return new clsCoachTrainingBuisness(enMode.update, id, coachID, addBy, sportID, trainingDay, dayilyStartAt, dayilyEndAt, isAvaliable, fee);
            }

            return null;
        }


        public static clsCoachTrainingBuisness findCoachTrainingByCoachID(int coachID)
        {

            int id = 0;
            int addBy = 0;
            int sportID = 0;
            bool isAvaliable = false;
            string dayilyStartAt = "";
            string dayilyEndAt = "";
            int trainingDay = 0;
            decimal fee = 0;

            if (clsCoachsTraingingData.findCoachsTrainging(ref id, coachID, ref addBy, ref sportID, ref isAvaliable, ref dayilyStartAt, ref dayilyEndAt, ref trainingDay, ref fee))
            {
                return new clsCoachTrainingBuisness(enMode.update, id, coachID, addBy, sportID, trainingDay, dayilyStartAt, dayilyEndAt, isAvaliable, fee);
            }

            return null;
        }


        private bool _add()
        {
            this.id = clsCoachsTraingingData.createCoachesTraineing(
            this.coachID,
            this.addBy,
            this.sportID,
            this.isAvaliable,
            this.dayilyStartAt,
            this.dayilyEndAt,
            this.trainingDay,
            this.fee
                );
            return (this.id != 0);
        }

        private bool _update()
        {
            return clsCoachsTraingingData.updateCoachTraining(
                this.id,
                this.coachID,
                this.addBy,
                this.sportID,
                this.isAvaliable,
                this.dayilyStartAt,
                this.dayilyEndAt,
               this.trainingDay,
            this.fee);
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


        public static DataTable getAllCoachsTraingin()
        {
            return clsCoachsTraingingData.getAllCoachTraining();
        }


        public static DataTable getCoachTrainginByID(int id)
        {
            return clsCoachsTraingingData.getAllcoachTraining(id);
        }

        public static DataTable getCoachTrainingName()
        {
            return clsCoachsTraingingData.getAllcoachTrainingName();
        }


        public static bool deleteCoachTrainginByID(int id)
        {
            return clsCoachData.deleteCoach(id);
        }


        public static bool isCoachTainginExistByCoahcID(int coachID)
        {
            return clsCoachsTraingingData.isCoachTrainingByCoach(coachID);
        }

        public static bool activateCoachTraining(int id)
        {
            return clsCoachsTraingingData.changeCoachTraingingState(id, true);
        }
        public static bool deActivateCoachTraining(int id)
        {
            return clsCoachsTraingingData.changeCoachTraingingState(id, false);
        }

        public static bool isCoachTrainingActive(int id)
        {
            return clsCoachsTraingingData.isCoachTrainingActive(id);
        }

    }
}
