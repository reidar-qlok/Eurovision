using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCTisdag.Models
{
    public enum LeaveStatus
    {
        Pending, // Väntar
        Approved, // Godkänd
        Denied // avslag
    }
    public class Leave
    {
        [Key]
        public int LeaveId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Type { get; set; }// Semester, Sjukdom
        public LeaveStatus Status { get; set; } = LeaveStatus.Pending; // Godkänd, Avslagen, eller Pending
        [ForeignKey("Employee")]
        public int FkEmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}
