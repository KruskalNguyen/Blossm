using BlossmMudBlazor.DTO;

namespace BlossmMudBlazor.Patterms
{
    public class BlossmSingleTon
    {
        private static volatile BlossmSingleTon instance = null;
        private static readonly object lockObject = new object();
        private static EmployeeProfile employeeProfile = null;
        private static List<PurchaseRequestStatus> prStatus = null;
        private static List<Priovity> priovities = null;
        private static List<BlossmAPI.Models.Branch> Branches = null;
        
        private BlossmSingleTon()
        {

        }
        public static BlossmSingleTon Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                        instance = new BlossmSingleTon();
                    
                    return instance;
                }
            }
        }

        #region EmployeeProfile
        public void SetEmployeeProfile(EmployeeProfile emp)
        {
            if(employeeProfile == null)
                employeeProfile = emp;
        }

        public EmployeeProfile GetEmployeeProfile()
        {
            if (employeeProfile != null)
                return employeeProfile;
            return null;
        }

        public void ResetEmployeeProfile()
        {
            employeeProfile = null;
        }

        public char GetFirstChar()
        {
            return employeeProfile.lastName.ToUpper()[0];
        }
        #endregion

        #region PurchaseRequestStatus
        public void SetPRStatus(List<PurchaseRequestStatus> status)
        {
            if (prStatus == null)
            {
                if(GetEmployeeProfile().idBranch == 1)
                    prStatus = status;
                else
                    prStatus = status.Where(p => p.id != 4).ToList();
                
            }
                
        }

        public List<PurchaseRequestStatus> GetPRStatus()
        {
            if (prStatus != null)
                return prStatus;
            return null;
        }

        public void ResetPRStatus()
        {
            prStatus = null;
        }
        #endregion

        #region Priovities
        public void SetPriovities(List<Priovity> pri)
        {
            if (priovities == null)
                priovities = pri;
        }

        public List<Priovity> GetPriovities()
        {
            if (priovities != null)
                return priovities;
            return null;
        }

        public void ResetPriovities()
        {
            priovities = null;
        }
        #endregion

        #region Branches

        public void SetBranches(List<BlossmAPI.Models.Branch> bra)
        {
            if (Branches == null)
                Branches = bra;
        }

        public List<BlossmAPI.Models.Branch> GetBranches()
        {
            if (Branches != null)
                return Branches;
            return null;
        }

        public void ResetBranches()
        {
            Branches = null;
        }


        #endregion
    }
}
