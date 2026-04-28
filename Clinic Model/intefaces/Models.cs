namespace Clinic_Model.intefaces;

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class Doctor
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public int DepartmentId { get; set; }
    public string Phone { get; set; } = string.Empty;
}

public class Disease
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal TreatmentCost { get; set; }
}

public class Patient
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public int DiseaseId { get; set; }
    public int DoctorId { get; set; }
    public DateTime AdmissionDate { get; set; }
    public DateTime? DischargeDate { get; set; }
}

public class Treatment
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public decimal Cost { get; set; }
    public int DurationDays { get; set; }
    public string TreatmentHistory { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}
