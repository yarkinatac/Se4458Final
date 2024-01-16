using AutoMapper;
using BloodBank.Data.Models;
using BloodBank.Logic.Logics.Hospitals;
using Donor.Data.Models;
using Donor.Logic.Logics.Branches;
using Donor.Logic.Logics.DonationHistories;
using Donor.Logic.Logics.Donors;
using DonorWebAPI.Services.Location;

namespace DonorWebAPI.Services.Donors
{
    public class DonorService : IDonorService
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IBranchLogic _branchLogic;
        private readonly IDonationHistoryLogic _donationHistoryLogic;
        private readonly ILocationService _locationService;
        private readonly IHospitalLogic _hospitalLogic;


        public DonorService(IMapper mapper, IConfiguration configuration, ILocationService locationService, IBranchLogic branchLogic, IDonationHistoryLogic donationHistoryLogic, IHospitalLogic hospitalLogic)

        {
            _mapper = mapper;
            _configuration = configuration;
            _locationService = locationService;
            _branchLogic = branchLogic;
            _donationHistoryLogic = donationHistoryLogic;
            _hospitalLogic = hospitalLogic;
        }
        public async Task<bool> UpdateBranchBloodNumber(Branch branch ,string bloodType, int bloodCount)
        {
           
            if (bloodType == "A+")
            {
                branch.APlusBlood = branch.APlusBlood + bloodCount;
                Branch? updatedBranch = await _branchLogic.UpdateAsync(branch.BranchID, branch);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "A-")
            {
                branch.AMinusBlood = branch.AMinusBlood + bloodCount;
                Branch? updatedBranch = await _branchLogic.UpdateAsync(branch.BranchID, branch);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "B+")
            {
                branch.BPlusBlood = branch.BPlusBlood + bloodCount;
                Branch? updatedBranch = await _branchLogic.UpdateAsync(branch.BranchID, branch);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "B-")
            {
                branch.BMinusBlood = branch.BMinusBlood + bloodCount;
                Branch? updatedBranch = await _branchLogic.UpdateAsync(branch.BranchID, branch);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "AB+")
            {
                branch.AbPlusBlood = branch.AbPlusBlood + bloodCount;
                Branch? updatedBranch = await _branchLogic.UpdateAsync(branch.BranchID, branch);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "AB-")
            {
                branch.AbMinusBlood = branch.AbMinusBlood + bloodCount;
                Branch? updatedBranch = await _branchLogic.UpdateAsync(branch.BranchID, branch);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "O+")
            {
                branch.ZeroPlusBlood = branch.ZeroPlusBlood + bloodCount;
                Branch? updatedBranch = await _branchLogic.UpdateAsync(branch.BranchID, branch);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "O-")
            {
                branch.ZeroMinusBlood = branch.ZeroMinusBlood + bloodCount;
                Branch? updatedBranch = await _branchLogic.UpdateAsync(branch.BranchID, branch);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
           
        }
        public bool HasBlood(Branch branch, string bloodType, int bloodCount)
        {

            if (bloodType == "A+")
            {
                if (branch.APlusBlood < bloodCount)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "A-")
            {
                if (branch.AMinusBlood < bloodCount)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "B+")
            {
                if (branch.BPlusBlood < bloodCount)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "B-")
            {
                if (branch.BMinusBlood < bloodCount)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "AB+")
            {
                if (branch.AbPlusBlood < bloodCount)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "AB-")
            {
                if (branch.AbMinusBlood < bloodCount)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "O+")
            {
                if (branch.ZeroPlusBlood < bloodCount)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "O-")
            {
                if (branch.ZeroMinusBlood < bloodCount)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }

        }
        public async Task<bool> UndoUpdateBranchBloodNumber(Branch branch, string bloodType, int bloodCount)
        {

            if (bloodType == "A+")
            {
                branch.APlusBlood = branch.APlusBlood - bloodCount;
                Branch? updatedBranch = await _branchLogic.UpdateAsync(branch.BranchID, branch);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "A-")
            {
                branch.AMinusBlood = branch.AMinusBlood - bloodCount;
                Branch? updatedBranch = await _branchLogic.UpdateAsync(branch.BranchID, branch);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "B+")
            {
                branch.BPlusBlood = branch.BPlusBlood - bloodCount;
                Branch? updatedBranch = await _branchLogic.UpdateAsync(branch.BranchID, branch);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "B-")
            {
                branch.BMinusBlood = branch.BMinusBlood - bloodCount;
                Branch? updatedBranch = await _branchLogic.UpdateAsync(branch.BranchID, branch);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "AB+")
            {
                branch.AbPlusBlood = branch.AbPlusBlood - bloodCount;
                Branch? updatedBranch = await _branchLogic.UpdateAsync(branch.BranchID, branch);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "AB-")
            {
                branch.AbMinusBlood = branch.AbMinusBlood - bloodCount;
                Branch? updatedBranch = await _branchLogic.UpdateAsync(branch.BranchID, branch);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "O+")
            {
                branch.ZeroPlusBlood = branch.ZeroPlusBlood - bloodCount;
                Branch? updatedBranch = await _branchLogic.UpdateAsync(branch.BranchID, branch);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "O-")
            {
                branch.ZeroMinusBlood = branch.ZeroMinusBlood - bloodCount;
                Branch? updatedBranch = await _branchLogic.UpdateAsync(branch.BranchID, branch);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }

        }
        public async Task<bool> UpdateHospitalBloodNumber(Hospital hospital, string bloodType, int bloodCount)
        {

            if (bloodType == "A+")
            {
                hospital.APlusBlood = hospital.APlusBlood + bloodCount;
                Hospital? updatedBranch = await _hospitalLogic.UpdateAsyncHospitals(hospital.HospitalID, hospital);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "A-")
            {
                hospital.AMinusBlood = hospital.AMinusBlood + bloodCount;
                Hospital? updatedBranch = await _hospitalLogic.UpdateAsyncHospitals(hospital.HospitalID, hospital);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "B+")
            {
                hospital.BPlusBlood = hospital.BPlusBlood + bloodCount;
                Hospital? updatedBranch = await _hospitalLogic.UpdateAsyncHospitals(hospital.HospitalID, hospital);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "B-")
            {
                hospital.BMinusBlood = hospital.BMinusBlood + bloodCount;
                Hospital? updatedBranch = await _hospitalLogic.UpdateAsyncHospitals(hospital.HospitalID, hospital);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "AB+")
            {
                hospital.AbPlusBlood = hospital.AbPlusBlood + bloodCount;
                Hospital? updatedBranch = await _hospitalLogic.UpdateAsyncHospitals(hospital.HospitalID, hospital);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "AB-")
            {
                hospital.AbMinusBlood = hospital.AbMinusBlood + bloodCount;
                Hospital? updatedBranch = await _hospitalLogic.UpdateAsyncHospitals(hospital.HospitalID, hospital);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "O+")
            {
                hospital.ZeroPlusBlood = hospital.ZeroPlusBlood + bloodCount;
                Hospital? updatedBranch = await _hospitalLogic.UpdateAsyncHospitals(hospital.HospitalID, hospital);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "O-")
            {
                hospital.ZeroMinusBlood = hospital.ZeroMinusBlood + bloodCount;
                Hospital? updatedBranch = await _hospitalLogic.UpdateAsyncHospitals(hospital.HospitalID, hospital);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }

        }
        public async Task<bool> UndoUpdateHospitalBloodNumber(Hospital hospital, string bloodType, int bloodCount)
        {

            if (bloodType == "A+")
            {
                hospital.APlusBlood = hospital.APlusBlood - bloodCount;
                Hospital? updatedBranch = await _hospitalLogic.UpdateAsyncHospitals(hospital.HospitalID, hospital);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "A-")
            {
                hospital.AMinusBlood = hospital.AMinusBlood - bloodCount;
                Hospital? updatedBranch = await _hospitalLogic.UpdateAsyncHospitals(hospital.HospitalID, hospital);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "B+")
            {
                hospital.BPlusBlood = hospital.BPlusBlood - bloodCount;
                Hospital? updatedBranch = await _hospitalLogic.UpdateAsyncHospitals(hospital.HospitalID, hospital);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "B-")
            {
                hospital.BMinusBlood = hospital.BMinusBlood - bloodCount;
                Hospital? updatedBranch = await _hospitalLogic.UpdateAsyncHospitals(hospital.HospitalID, hospital);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "AB+")
            {
                hospital.AbPlusBlood = hospital.AbPlusBlood - bloodCount;
                Hospital? updatedBranch = await _hospitalLogic.UpdateAsyncHospitals(hospital.HospitalID, hospital);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "AB-")
            {
                hospital.AbMinusBlood = hospital.AbMinusBlood - bloodCount;
                Hospital? updatedBranch = await _hospitalLogic.UpdateAsyncHospitals(hospital.HospitalID, hospital);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "O+")
            {
                hospital.ZeroPlusBlood = hospital.ZeroPlusBlood - bloodCount;
                Hospital? updatedBranch = await _hospitalLogic.UpdateAsyncHospitals(hospital.HospitalID, hospital);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "O-")
            {
                hospital.ZeroMinusBlood = hospital.ZeroMinusBlood - bloodCount;
                Hospital? updatedBranch = await _hospitalLogic.UpdateAsyncHospitals(hospital.HospitalID, hospital);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
