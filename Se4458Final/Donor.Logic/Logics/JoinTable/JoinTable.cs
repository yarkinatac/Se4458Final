using Donor.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Donor.Logic.Logics.JoinTable
{
    public class JoinTable : IJoinTable
    {
        DonorDBContext _context = new DonorDBContext();
        private DbSet<Data.Models.Donor> DonorTable { get; set; }
        private DbSet<Branch> BranchTable { get; set; }
        private DbSet<DonationHistory> DonationHistoryTable { get; set; }

        public JoinTable()
        {
            DonorTable = _context.Set<Data.Models.Donor>();
            BranchTable = _context.Set<Branch>();
            DonationHistoryTable = _context.Set<DonationHistory>();
        }
        
        public List<Data.Models.Donor> FindDonorByJoinTable(int branchId, string name, string surname)

        {

            var result = (from donorTable in DonorTable
                          join branchTable in BranchTable on donorTable.BranchID equals branchTable.BranchID

                          where donorTable.Name == name && donorTable.Surname == surname 

                          select new Data.Models.Donor
                          {
                              DonorID = donorTable.DonorID,
                              BranchID = donorTable.BranchID,
                              Name = donorTable.Name,
                              Surname = donorTable.Surname,
                              BloodType = donorTable.BloodType,
                              PhoneNumber = donorTable.PhoneNumber,
                              City = donorTable.City,
                              Town = donorTable.Town
                          }).ToList();

            return result;


        }
        public List<DonationHistory> CheckDonationListByJoinTable()

        {

            var result = (from donationHistoryTable in DonationHistoryTable

                          where (donationHistoryTable.DonationDate - DateTime.Now).TotalSeconds > 0 

                          select new DonationHistory
                          {
                            HistoryID = donationHistoryTable.HistoryID,
                            DonorID = donationHistoryTable.DonorID,
                            TupleNumber = donationHistoryTable.TupleNumber,
                            DonationDate  = donationHistoryTable.DonationDate,
                          }).ToList();

            return result;


        }
        public List<Data.Models.Donor> GetDonorListByJoinTable(int id)

        {

            var result = (from donorTable in DonorTable

                          where donorTable.BranchID == id

                          select new Data.Models.Donor
                          {
                              DonorID = donorTable.DonorID,
                              BranchID = donorTable.BranchID,
                              BloodType = donorTable.BloodType,
                              City = donorTable.City,
                              Town = donorTable.Town,
                              Name = donorTable.Name,
                              Surname = donorTable.Surname,
                              PhoneNumber = donorTable.PhoneNumber,
                              
                          }).ToList();

            return result;


        }

    }
}