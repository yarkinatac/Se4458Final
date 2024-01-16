using Donor.Data.Models;
using Donor.Data.Repository.DonationHistories;
using Donor.Data.Repository.Donors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donor.Logic.Logics.Donors
{
    public class DonorLogic : IDonorLogic
    {
        private IDonorRepository _repository;
        public DonorLogic(IDonorRepository repository)
        {
            _repository = repository;
        }
        public bool AddDonor(Data.Models.Donor entity)
        {
            bool addResult = _repository.Add(entity);
            return addResult;
        }
        public int AddAndGetIdDonor(Data.Models.Donor entity)
        {
            int addResult = _repository.AddAndGetId(entity);
            return addResult;
        }
        public bool DeleteDonor(int id)
        {
            bool deleteResult = _repository.Delete(id);
            return deleteResult;
        }
        public bool DeleteSingleByMethod(int id)
        {
            Func<Data.Models.Donor, bool> filter = filter => filter.DonorID == id;
            bool deleteResult = _repository.DeleteSingleByMethod(filter);
            return deleteResult;
        }
        public bool DeleteList(int id)
        {
            Func<Data.Models.Donor, bool> filter = filter => filter.DonorID == id;
            bool deleteResult = _repository.DeleteList(filter);
            return deleteResult;
        }

        public Data.Models.Donor? GetSingle(int id)
        {
            throw new NotImplementedException();
        }

        public Data.Models.Donor? GetSingleDonors(int id)
        {
            Data.Models.Donor? result = _repository.GetSingle(id);
            return result;
        }
        public Data.Models.Donor? GetSingleByMethod(int id)
        {
            Func<Data.Models.Donor, bool> filter = filter => filter.DonorID == id;
            Data.Models.Donor? result = _repository.GetSingleByMethod(filter);
            return result;
        }
        public Data.Models.Donor? GetSingleByMethods(string name, string surname)
        {
            Func<Data.Models.Donor, bool> filter = filter => filter.Name == name;
            Func<Data.Models.Donor, bool> filter2 = filter => filter.Surname == surname;
            Data.Models.Donor? result = _repository.GetSingleByMethod(filter, filter2);
            return result;
        }
        public List<Data.Models.Donor>? GetListDonors(int id)
        {
            Func<Data.Models.Donor, bool> filter = filter => filter.DonorID == id;
            var list = _repository.GetList(filter);
            return list;
        }
        
        public async Task<Data.Models.Donor?> UpdateAsync(int id, Data.Models.Donor updatedEntity)
        {
            Func<Data.Models.Donor, bool> filter = filter => filter.DonorID == id;
            Data.Models.Donor? updateResult = await _repository.UpdateAsync(filter, updatedEntity);
            return updateResult;
        }
        public async Task<Data.Models.Donor?> UpdateAsync(Data.Models.Donor entity, Data.Models.Donor updatedEntity)
        {
            Data.Models.Donor? updateResult = await _repository.UpdateAsync(entity, updatedEntity);
            return updateResult;
        }

    }
}
