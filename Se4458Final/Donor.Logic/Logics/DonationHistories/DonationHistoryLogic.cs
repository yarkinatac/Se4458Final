﻿using Donor.Data.Models;
using Donor.Data.Repository.Branches;
using Donor.Data.Repository.DonationHistories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donor.Logic.Logics.DonationHistories
{
    public class DonationHistoryLogic : IDonationHistoryLogic
    {
        private IDonationHistoryRepository _repository;
        public DonationHistoryLogic(IDonationHistoryRepository repository)
        {
            _repository = repository;
        }
        public bool Add(DonationHistory entity)
        {
            bool addResult = _repository.Add(entity);
            return addResult;
        }
        public int AddAndGetId(DonationHistory entity)
        {
            int addResult = _repository.AddAndGetId(entity);
            return addResult;
        }
        public bool Delete(int id)
        {
            bool deleteResult = _repository.Delete(id);
            return deleteResult;
        }
        public bool DeleteSingleByMethod(int id)
        {
            Func<DonationHistory, bool> filter = filter => filter.HistoryID == id;
            bool deleteResult = _repository.DeleteSingleByMethod(filter);
            return deleteResult;
        }
        public bool DeleteList(int id)
        {
            Func<DonationHistory, bool> filter = filter => filter.HistoryID == id;
            bool deleteResult = _repository.DeleteList(filter);
            return deleteResult;
        }
        public DonationHistory? GetSingle(int id)
        {
            DonationHistory? result = _repository.GetSingle(id);
            return result;
        }
        public DonationHistory? GetSingleByMethod(int id)
        {
            Func<DonationHistory, bool> filter = filter => filter.HistoryID == id;
            DonationHistory? result = _repository.GetSingleByMethod(filter);
            return result;
        }
 
        public List<DonationHistory>? GetList(int id)
        {
            Func<DonationHistory, bool> filter = filter => filter.HistoryID == id;
            var list = _repository.GetList(filter);
            return list;
        }
        
        public async Task<DonationHistory?> UpdateAsync(int id, DonationHistory updatedEntity)
        {
            Func<DonationHistory, bool> filter = filter => filter.HistoryID == id;
            DonationHistory? updateResult = await _repository.UpdateAsync(filter, updatedEntity);
            return updateResult;
        }
        public async Task<DonationHistory?> UpdateAsync(DonationHistory entity, DonationHistory updatedEntity)
        {
            DonationHistory? updateResult = await _repository.UpdateAsync(entity, updatedEntity);
            return updateResult;
        }

    }
}
