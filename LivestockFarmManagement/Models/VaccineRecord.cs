namespace LivestockFarmManagement.Models
{
    public class VaccineRecord
    {
        public int Id { get; set; }
        public int LivestockId { get; set; }
        public string VaccineName { get; set; } = string.Empty;
        public DateTime AdministeredDate { get; set; } = DateTime.Now;
        public DateTime NextDueDate { get; set; } = DateTime.Now.AddMonths(6);
        public string AdministeredBy { get; set; } = string.Empty;

        public Livestock? Livestock { get; set; }
    }
}