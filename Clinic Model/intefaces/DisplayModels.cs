namespace Clinic_Model.intefaces;

public class DoctorDisplayModel
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string DepartmentName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}

public class PatientDisplayModel
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string DiseaseName { get; set; } = string.Empty;
    public string DoctorName { get; set; } = string.Empty;
    public DateTime AdmissionDate { get; set; }
    public DateTime? DischargeDate { get; set; }
}
