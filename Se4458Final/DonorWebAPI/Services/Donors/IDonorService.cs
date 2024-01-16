using BloodBank.Data.Models;

using Donor.Data.Models;


namespace DonorWebAPI.Services.Donors
{
    public interface IDonorService
    {
        public Task<bool> UpdateBranchBloodNumber(Branch branch, string bloodType, int bloodCount);
        public Task<bool> UndoUpdateBranchBloodNumber(Branch branch, string bloodType, int bloodCount);
        public bool HasBlood(Branch branch, string bloodType, int bloodCount);
        public Task<bool> UpdateHospitalBloodNumber(Hospital hospital, string bloodType, int bloodCount);
        public Task<bool> UndoUpdateHospitalBloodNumber(Hospital hospital, string bloodType, int bloodCount);
    }
}
