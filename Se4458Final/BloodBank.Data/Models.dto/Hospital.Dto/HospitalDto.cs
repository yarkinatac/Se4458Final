
namespace BloodBank.Data.Models.dto.Hospital.Dto
{
    public class HospitalDto
    {
        public string Name { get; set; } = null!;
        public int APlusBlood { get; set; }
        public int AMinusBlood { get; set; }
        public int BPlusBlood { get; set; }
        public int BMinusBlood { get; set; }
        public int AbPlusBlood { get; set; }
        public int AbMinusBlood { get; set; }
        public int ZeroPlusBlood { get; set; }
        public int ZeroMinusBlood { get; set; }
        
    }
}
