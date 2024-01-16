using Donor.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donor.Logic.Logics.DonationHistories
{
    public interface IDonationHistoryLogic
    {
        public bool Add(DonationHistory entity);
        public int AddAndGetId(DonationHistory entity);
        public bool Delete(int id);
        public bool DeleteSingleByMethod(int id);
        public bool DeleteList(int id);
        public DonationHistory? GetSingle(int id);
        public DonationHistory? GetSingleByMethod(int id);
        public List<DonationHistory>? GetList(int id);
        public Task<DonationHistory?> UpdateAsync(int id, DonationHistory updatedEntity);
        public Task<DonationHistory?> UpdateAsync(DonationHistory entity, DonationHistory updatedEntity);

    }
}
