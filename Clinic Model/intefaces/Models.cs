namespace Clinic_Model.intefaces;

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int BedCount { get; set; }
    public int AvgTreatmentDuration { get; set; }
}

public class Doctor
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public int DepartmentId { get; set; }
    public string Office { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}

public class Disease
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal BaseTreatmentCost { get; set; }
}

public class Patient
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
}

public class Treatment
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public int DiseaseId { get; set; }
    public DateTime AdmissionDate { get; set; }
    public DateTime? DischargeDate { get; set; }
    public int DurationDays { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class Payment
{
    public int Id { get; set; }
    public int TreatmentId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
}
