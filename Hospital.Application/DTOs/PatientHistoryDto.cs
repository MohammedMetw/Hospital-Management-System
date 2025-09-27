using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Application.DTOs
{
    //  Main DTO 
    public class PatientHistoryDto
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public List<AppointmentHistoryDto> Appointments { get; set; }
        public List<PrescriptionHistoryDto> Prescriptions { get; set; }
        public List<BillHistoryDto> PharmacyBills { get; set; }
    }

    //  DTO for each Appointment 
    public class AppointmentHistoryDto
    {
        public int Id { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public string Status { get; set; }
        public string DoctorName { get; set; }
    }

    //  DTO for each Prescription 
    public class PrescriptionHistoryDto
    {
        public int Id { get; set; }
        public DateTime PrescriptionDate { get; set; }
        public List<string> Medicines { get; set; } 
    }

    public class BillHistoryDto
    {
        public int DispenseLogId { get; set; }
        public DateTime DispenseDate { get; set; }
        public string MedicineName { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; } // The calculated price for this specific transaction
        public string PharmacistName { get; set; }
    }

}
