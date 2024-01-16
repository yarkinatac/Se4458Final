
namespace BloodBank.Data.Models
{
    public class RequestForBlood
    {
        public int RequestID { get; set; }
        public int BloodNumber { get; set; }
        public string BloodType { get; set; } = null!;
        public DateTime DurationDate { get; set; }
        public string City { get; set; } = null!;
        public string Town { get; set; } = null!;
        public double HospitalLongitude { get; set; }
        public double HospitalLatitude { get; set; }
        public string HospitalEmail { get; set; } = null!;

    }
}
