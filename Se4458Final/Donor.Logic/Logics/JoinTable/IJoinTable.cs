using Donor.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donor.Logic.Logics.JoinTable
{
    public interface IJoinTable
    {
        public List<Data.Models.Donor> FindDonorByJoinTable(int branchId, string name, string surname);
        public List<DonationHistory> CheckDonationListByJoinTable();
        public List<Data.Models.Donor> GetDonorListByJoinTable(int id);
    }
}