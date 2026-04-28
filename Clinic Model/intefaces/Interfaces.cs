namespace Clinic_Model.intefaces;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}

public interface IDepartmentRepository : IRepository<Department> { }
public interface IDoctorRepository : IRepository<Doctor> 
{
    Task<IEnumerable<DoctorDisplayModel>> GetAllWithDetailsAsync();
}
public interface IDiseaseRepository : IRepository<Disease> { }
public interface IPatientRepository : IRepository<Patient> 
{
    Task<IEnumerable<PatientDisplayModel>> GetAllWithDetailsAsync(int? count = null);
}
public interface ITreatmentRepository : IRepository<Treatment> { }

public interface IClinicDataService
{
    IDepartmentRepository Departments { get; }
    IDoctorRepository Doctors { get; }
    IDiseaseRepository Diseases { get; }
    IPatientRepository Patients { get; }
    ITreatmentRepository Treatments { get; }
}
