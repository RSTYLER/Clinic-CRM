using Clinic_Model.intefaces;

namespace Clinic_Data;

public class ClinicDataService : IClinicDataService
{
    public IDepartmentRepository Departments { get; }
    public IDoctorRepository Doctors { get; }
    public IDiseaseRepository Diseases { get; }
    public IPatientRepository Patients { get; }
    public ITreatmentRepository Treatments { get; }

    public ClinicDataService(string connectionString)
    {
        Departments = new SqlRepositories.DepartmentRepository(connectionString);
        Doctors = new SqlRepositories.DoctorRepository(connectionString);
        Diseases = new SqlRepositories.DiseaseRepository(connectionString);
        Patients = new SqlRepositories.PatientRepository(connectionString);
        Treatments = new SqlRepositories.TreatmentRepository(connectionString);
    }
}
