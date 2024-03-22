using sportDataLayer;
using System;
using System.Data;

namespace SportCllubeBuisness
{
    public class clsMemberSubscriptionsBuisness
    {
        public enum enMode { add, update };
        public enMode mode { get; set; }
        public int id { get; set; }
        public int coatchTrainingID { get; set; }
        public int memberID { get; set; }
        public int? addBy { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public decimal fee { get; set; }
        public clsCoachTrainingBuisness coatchTrainingInfo { get; set; }
        public clsMemberBuisness memberInfo { get; set; }

        public clsMemberSubscriptionsBuisness()
        {

            this.mode = enMode.add;
            this.id = 0;
            this.coatchTrainingID = 0;
            this.memberID = 0;
            this.addBy = null;
            this.startDate = DateTime.Now;
            this.endDate = DateTime.Now;
            this.fee = 0;
        }

        private clsMemberSubscriptionsBuisness(
            enMode mode, int id, int coatchTrainingID, int memberID, int? addBy, DateTime startDate, DateTime endDate, decimal fee)
        {
            this.mode = mode;
            this.id = id;
            this.coatchTrainingID = coatchTrainingID;
            this.memberID = memberID;
            this.addBy = addBy;
            this.startDate = startDate;
            this.endDate = endDate;
            this.fee = fee;
            this.coatchTrainingInfo = clsCoachTrainingBuisness.findCoachTrainingByID(coatchTrainingID);
            this.memberInfo = clsMemberBuisness.findMemberByID(memberID);
        }


        public static clsMemberSubscriptionsBuisness findMemberSubscrioptionsByID(int id)
        {
            int coatchTrainingID = 0;
            int memberID = 0;
            int? addBy = null;
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;
            decimal fee = 0;
            if (clsMemberSubscriptionsData.findMemberSubscriptionByID(id, ref memberID, ref addBy, ref coatchTrainingID, ref fee, ref startDate, ref endDate))
            {
                return new clsMemberSubscriptionsBuisness(enMode.update, id, coatchTrainingID, memberID, addBy, startDate, endDate, fee);
            }

            return null;

        }
        public static clsMemberSubscriptionsBuisness findMemberSubscriptionByMemberID(int memberID)
        {

            int id = 0;
            int? addBy = null;

            int coatchTrainingID = 0;
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;
            decimal fee = 0;

            if (clsMemberSubscriptionsData.findMemberSubscriptionByMemberID(ref id, memberID, ref addBy, ref coatchTrainingID, ref fee, ref startDate, ref endDate))
            {
                return new clsMemberSubscriptionsBuisness(enMode.update, id, memberID, coatchTrainingID, addBy, startDate, endDate, fee);
            }

            return null;
        }


        private bool _add()
        {
            this.id = clsMemberSubscriptionsData.createMemberSubscriptions(
            this.memberID,
            this.addBy,
            this.coatchTrainingID,
            this.fee,
            this.startDate,
            this.endDate
                );
            return (this.id != 0);
        }

        private bool _update()
        {
            return clsMemberSubscriptionsData.updateMemberSubscriptoion(
                this.id,
                this.memberID,
                this.addBy,
                this.coatchTrainingID,
                this.fee,
                this.startDate,
                this.endDate
                );
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


        public static DataTable getAllMemberSubscription()
        {
            return clsMemberSubscriptionsData.getAllMemberSubscription();
        }


        public static bool deleteMemberSubscriptionByID(int id)
        {
            return clsMemberSubscriptionsData.deleteMemberSubscription(id);
        }


        public static bool isMemberSubscritpionExistByID(int id)
        {
            return clsMemberSubscriptionsData.isMemberSubscriptionExistByID(id);
        }

        public static bool isMemberSubscritpionExistByMemberID(int memberID)
        {
            return clsMemberSubscriptionsData.isMemberSubscriptionExistByMemberID(memberID);
        }

    }
}
