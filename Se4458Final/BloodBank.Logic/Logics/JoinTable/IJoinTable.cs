using BloodBank.Data.Models;
using BloodBank.Data.Models.dto.RequestBlood.Dto;


namespace BloodBank.Logic.Logics.JoinTable
{
    public interface IJoinTable
    {
        public List<RequestForBlood> CheckBloodRequestByJoinTable();
        public List<GeopointDto> BranchGeopointListByJoinTable();
        public List<IdDto> AllBranchListByJoinTable();
        public List<RequestForBlood> AllBloodRequestListByJoinTable();
    }
}