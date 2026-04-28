using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Clinic_Model.intefaces;

namespace Clinic_Data;

public class SqlRepositories
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(string connectionString) : base(connectionString) { }

        protected override Department Map(SqlDataReader reader) => new Department
        {
            Id = (int)reader["Id"],
            Name = reader["Name"].ToString() ?? string.Empty
        };

        public Task<Department?> GetByIdAsync(int id) => GetByIdAsync("SELECT * FROM Departments WHERE Id = @Id", id);
        public Task<IEnumerable<Department>> GetAllAsync() => GetAllAsync("SELECT * FROM Departments");

        public Task AddAsync(Department entity) => ExecuteNonQueryAsync(
            "INSERT INTO Departments (Name) VALUES (@Name)",
            new SqlParameter("@Name", entity.Name));

        public Task UpdateAsync(Department entity) => ExecuteNonQueryAsync(
            "UPDATE Departments SET Name = @Name WHERE Id = @Id",
            new SqlParameter("@Name", entity.Name),
            new SqlParameter("@Id", entity.Id));

        public Task DeleteAsync(int id) => ExecuteNonQueryAsync(
            "DELETE FROM Departments WHERE Id = @Id",
            new SqlParameter("@Id", id));
    }

    public class DoctorRepository : BaseRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(string connectionString) : base(connectionString) { }

        protected override Doctor Map(SqlDataReader reader) => new Doctor
        {
            Id = (int)reader["Id"],
            FullName = reader["FullName"].ToString() ?? string.Empty,
            DepartmentId = (int)reader["DepartmentId"],
            Phone = reader["Phone"].ToString() ?? string.Empty
        };

        public Task<Doctor?> GetByIdAsync(int id) => GetByIdAsync("SELECT * FROM Doctors WHERE Id = @Id", id);
        public Task<IEnumerable<Doctor>> GetAllAsync() => GetAllAsync("SELECT * FROM Doctors");

        public Task AddAsync(Doctor entity) => ExecuteNonQueryAsync(
            "INSERT INTO Doctors (FullName, DepartmentId, Phone) VALUES (@FullName, @DepartmentId, @Phone)",
            new SqlParameter("@FullName", entity.FullName),
            new SqlParameter("@DepartmentId", entity.DepartmentId),
            new SqlParameter("@Phone", entity.Phone));

        public Task UpdateAsync(Doctor entity) => ExecuteNonQueryAsync(
            "UPDATE Doctors SET FullName = @FullName, DepartmentId = @DepartmentId, Phone = @Phone WHERE Id = @Id",
            new SqlParameter("@FullName", entity.FullName),
            new SqlParameter("@DepartmentId", entity.DepartmentId),
            new SqlParameter("@Phone", entity.Phone),
            new SqlParameter("@Id", entity.Id));

        public Task DeleteAsync(int id) => ExecuteNonQueryAsync(
            "DELETE FROM Doctors WHERE Id = @Id",
            new SqlParameter("@Id", id));
    }

    public class DiseaseRepository : BaseRepository<Disease>, IDiseaseRepository
    {
        public DiseaseRepository(string connectionString) : base(connectionString) { }

        protected override Disease Map(SqlDataReader reader) => new Disease
        {
            Id = (int)reader["Id"],
            Name = reader["Name"].ToString() ?? string.Empty,
            TreatmentCost = (decimal)reader["TreatmentCost"]
        };

        public Task<Disease?> GetByIdAsync(int id) => GetByIdAsync("SELECT * FROM Diseases WHERE Id = @Id", id);
        public Task<IEnumerable<Disease>> GetAllAsync() => GetAllAsync("SELECT * FROM Diseases");

        public Task AddAsync(Disease entity) => ExecuteNonQueryAsync(
            "INSERT INTO Diseases (Name, TreatmentCost) VALUES (@Name, @TreatmentCost)",
            new SqlParameter("@Name", entity.Name),
            new SqlParameter("@TreatmentCost", entity.TreatmentCost));

        public Task UpdateAsync(Disease entity) => ExecuteNonQueryAsync(
            "UPDATE Diseases SET Name = @Name, TreatmentCost = @TreatmentCost WHERE Id = @Id",
            new SqlParameter("@Name", entity.Name),
            new SqlParameter("@TreatmentCost", entity.TreatmentCost),
            new SqlParameter("@Id", entity.Id));

        public Task DeleteAsync(int id) => ExecuteNonQueryAsync(
            "DELETE FROM Diseases WHERE Id = @Id",
            new SqlParameter("@Id", id));
    }

    public class PatientRepository : BaseRepository<Patient>, IPatientRepository
    {
        public PatientRepository(string connectionString) : base(connectionString) { }

        protected override Patient Map(SqlDataReader reader) => new Patient
        {
            Id = (int)reader["Id"],
            FullName = reader["FullName"].ToString() ?? string.Empty,
            DiseaseId = (int)reader["DiseaseId"],
            DoctorId = (int)reader["DoctorId"],
            AdmissionDate = (DateTime)reader["AdmissionDate"],
            DischargeDate = reader["DischargeDate"] as DateTime?
        };

        public Task<Patient?> GetByIdAsync(int id) => GetByIdAsync("SELECT * FROM Patients WHERE Id = @Id", id);
        public Task<IEnumerable<Patient>> GetAllAsync() => GetAllAsync("SELECT * FROM Patients");

        public Task AddAsync(Patient entity) => ExecuteNonQueryAsync(
            "INSERT INTO Patients (FullName, DiseaseId, DoctorId, AdmissionDate, DischargeDate) VALUES (@FullName, @DiseaseId, @DoctorId, @AdmissionDate, @DischargeDate)",
            new SqlParameter("@FullName", entity.FullName),
            new SqlParameter("@DiseaseId", entity.DiseaseId),
            new SqlParameter("@DoctorId", entity.DoctorId),
            new SqlParameter("@AdmissionDate", entity.AdmissionDate),
            new SqlParameter("@DischargeDate", (object?)entity.DischargeDate ?? DBNull.Value));

        public Task UpdateAsync(Patient entity) => ExecuteNonQueryAsync(
            "UPDATE Patients SET FullName = @FullName, DiseaseId = @DiseaseId, DoctorId = @DoctorId, AdmissionDate = @AdmissionDate, DischargeDate = @DischargeDate WHERE Id = @Id",
            new SqlParameter("@FullName", entity.FullName),
            new SqlParameter("@DiseaseId", entity.DiseaseId),
            new SqlParameter("@DoctorId", entity.DoctorId),
            new SqlParameter("@AdmissionDate", entity.AdmissionDate),
            new SqlParameter("@DischargeDate", (object?)entity.DischargeDate ?? DBNull.Value),
            new SqlParameter("@Id", entity.Id));

        public Task DeleteAsync(int id) => ExecuteNonQueryAsync(
            "DELETE FROM Patients WHERE Id = @Id",
            new SqlParameter("@Id", id));
    }

    public class TreatmentRepository : BaseRepository<Treatment>, ITreatmentRepository
    {
        public TreatmentRepository(string connectionString) : base(connectionString) { }

        protected override Treatment Map(SqlDataReader reader) => new Treatment
        {
            Id = (int)reader["Id"],
            PatientId = (int)reader["PatientId"],
            Cost = (decimal)reader["Cost"],
            DurationDays = (int)reader["DurationDays"],
            TreatmentHistory = reader["TreatmentHistory"].ToString() ?? string.Empty,
            Status = reader["Status"].ToString() ?? string.Empty
        };

        public Task<Treatment?> GetByIdAsync(int id) => GetByIdAsync("SELECT * FROM Treatments WHERE Id = @Id", id);
        public Task<IEnumerable<Treatment>> GetAllAsync() => GetAllAsync("SELECT * FROM Treatments");

        public Task AddAsync(Treatment entity) => ExecuteNonQueryAsync(
            "INSERT INTO Treatments (PatientId, Cost, DurationDays, TreatmentHistory, Status) VALUES (@PatientId, @Cost, @DurationDays, @TreatmentHistory, @Status)",
            new SqlParameter("@PatientId", entity.PatientId),
            new SqlParameter("@Cost", entity.Cost),
            new SqlParameter("@DurationDays", entity.DurationDays),
            new SqlParameter("@TreatmentHistory", entity.TreatmentHistory),
            new SqlParameter("@Status", entity.Status));

        public Task UpdateAsync(Treatment entity) => ExecuteNonQueryAsync(
            "UPDATE Treatments SET PatientId = @PatientId, Cost = @Cost, DurationDays = @DurationDays, TreatmentHistory = @TreatmentHistory, Status = @Status WHERE Id = @Id",
            new SqlParameter("@PatientId", entity.PatientId),
            new SqlParameter("@Cost", entity.Cost),
            new SqlParameter("@DurationDays", entity.DurationDays),
            new SqlParameter("@TreatmentHistory", entity.TreatmentHistory),
            new SqlParameter("@Status", entity.Status),
            new SqlParameter("@Id", entity.Id));

        public Task DeleteAsync(int id) => ExecuteNonQueryAsync(
            "DELETE FROM Treatments WHERE Id = @Id",
            new SqlParameter("@Id", id));
    }
}
