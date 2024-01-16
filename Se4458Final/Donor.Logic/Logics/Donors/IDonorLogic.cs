using Donor.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donor.Logic.Logics.Donors
{
    public interface IDonorLogic
    {
        public bool AddDonor(Data.Models.Donor entity);
        public int AddAndGetIdDonor(Data.Models.Donor entity);
        public bool DeleteDonor(int id);
        public bool DeleteSingleByMethod(int id);
        public bool DeleteList(int id);
        public Data.Models.Donor? GetSingle(int id);
        public Data.Models.Donor? GetSingleByMethod(int id);
        public Data.Models.Donor? GetSingleByMethods(string name, string surname);
        public List<Data.Models.Donor>? GetListDonors(int id);
        public Task<Data.Models.Donor?> UpdateAsync(int id, Data.Models.Donor updatedEntity);
        public Task<Data.Models.Donor?> UpdateAsync(Data.Models.Donor entity, Data.Models.Donor updatedEntity);
    }
}
