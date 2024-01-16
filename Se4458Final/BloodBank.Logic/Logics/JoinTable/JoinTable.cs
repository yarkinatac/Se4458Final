using BloodBank.Data.Models;
using BloodBank.Data.Models.dto.RequestBlood.Dto;
using Donor.Data.Models;
using Location.Data.Models;
using Microsoft.EntityFrameworkCore;


namespace BloodBank.Logic.Logics.JoinTable
{
    public class JoinTable : IJoinTable
    {
        BloodBankDBContext _context = new BloodBankDBContext();
        private DbSet<RequestForBlood> BloodRequestTable { get; set; }
        public JoinTable()
        {
            BloodRequestTable = _context.Set<RequestForBlood>();
        }
        public List<RequestForBlood> CheckBloodRequestByJoinTable()

        {

            var result = (from bloodRequestTable in BloodRequestTable

                          where bloodRequestTable.DurationDate > DateTime.Now

                          select new RequestForBlood
                          {
                              RequestID = bloodRequestTable.RequestID,
                              BloodNumber = bloodRequestTable.BloodNumber,
                              BloodType = bloodRequestTable.BloodType,
                              DurationDate = bloodRequestTable.DurationDate,
                              Town = bloodRequestTable.Town,
                              City = bloodRequestTable.City,
                              HospitalLongitude = bloodRequestTable.HospitalLongitude,
                              HospitalLatitude = bloodRequestTable.HospitalLatitude,
                              HospitalEmail = bloodRequestTable.HospitalEmail,
                          }).ToList();

            return result;
        }
        public List<IdDto> AllBranchListByJoinTable()
        {
            using (var _donorDBContext = new DonorDBContext())
            {
                var result = (from branchTable in _donorDBContext.Set<Branch>()
                    
                              select new IdDto
                              {
                                  Id = branchTable.GeopointID,
                              }).ToList();

                return result;
            }
        }
        public List<RequestForBlood> AllBloodRequestListByJoinTable()
        {
            using (var _bloodBankDBContext = new BloodBankDBContext())
            {
                var result = (from bloodTable in _bloodBankDBContext.Set<RequestForBlood>()

                              select new RequestForBlood
                              {
                                  RequestID = bloodTable.RequestID,
                                  BloodNumber = bloodTable.BloodNumber,
                                  BloodType = bloodTable.BloodType,
                                  City = bloodTable.City,
                                  Town = bloodTable.Town,
                                  HospitalLatitude = bloodTable.HospitalLatitude,
                                  HospitalLongitude = bloodTable.HospitalLongitude,
                                  DurationDate = bloodTable.DurationDate,
                                  HospitalEmail = bloodTable.HospitalEmail
                              }).ToList();

                return result;
            }
        }
        public List<GeopointDto> BranchGeopointListByJoinTable()
        {
            using (var _donorDBContext = new DonorDBContext())
            using (var _locationDBContext = new LocationDBContext())
            {
                var result = (from branchTable in _donorDBContext.Set<Branch>()
                join locationTable in _locationDBContext.Set<Geopoint>() on branchTable.GeopointID equals locationTable.GeopointID
                              select new GeopointDto
                              {
                                  Latitude = locationTable.Latitude,
                                  Longitude = locationTable.Longitude,
                              }).ToList();

                return result;
            }
        }
    }
}