﻿
using Donor.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donor.Logic.Logics.Branches
{
    public interface IBranchLogic
    {
        public bool Add(Branch entity);
        public int AddAndGetId(Branch entity);
        public bool Delete(int id);
        public bool DeleteSingleByMethod(int id);
        public bool DeleteList(int id);
        public Branch? GetSingle(int id);
        public Branch? GetSingleByMethod(int id);
        public Branch? GetSingleByMethodGeoPoint(int id);
        public Branch? GetSingleByMethods(int city, int town);
        public List<Branch>? GetList(int id);
        public Task<Branch?> UpdateAsync(int id, Branch updatedEntity);
        public Task<Branch?> UpdateAsync(Branch entity, Branch updatedEntity);
        public bool CheckExistence(int branchId);
    }
}
