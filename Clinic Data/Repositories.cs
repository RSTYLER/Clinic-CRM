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
            Name = reader["Name"].ToString() ?? string.Empty,
            BedCount = (int)reader["BedCount"],
            AvgTreatmentDuration = (int)reader["AvgTreatmentDuration"]
        };

        public Task<Department?> GetByIdAsync(int id) => GetByIdAsync("SELECT * FROM Departments WHERE Id = @Id", id);
        public Task<IEnumerable<Department>> GetAllAsync() => GetAllAsync("SELECT * FROM Departments");

        public Task AddAsync(Department entity) => ExecuteNonQueryAsync(
            "INSERT INTO Departments (Name, BedCount, AvgTreatmentDuration) VALUES (@Name, @BedCount, @AvgTreatmentDuration)",
            new SqlParameter("@Name", entity.Name),
            new SqlParameter("@BedCount", entity.BedCount),
            new SqlParameter("@AvgTreatmentDuration", entity.AvgTreatmentDuration));

        public Task UpdateAsync(Department entity) => ExecuteNonQueryAsync(
            "UPDATE Departments SET Name = @Name, BedCount = @BedCount, AvgTreatmentDuration = @AvgTreatmentDuration WHERE Id = @Id",
            new SqlParameter("@Name", entity.Name),
            new SqlParameter("@BedCount", entity.BedCount),
            new SqlParameter("@AvgTreatmentDuration", entity.AvgTreatmentDuration),
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
            Office = reader["Office"].ToString() ?? string.Empty,
            Phone = reader["Phone"].ToString() ?? string.Empty
        };

        public Task<Doctor?> GetByIdAsync(int id) => GetByIdAsync("SELECT * FROM Doctors WHERE Id = @Id", id);
        public Task<IEnumerable<Doctor>> GetAllAsync() => GetAllAsync("SELECT * FROM Doctors");

        public async Task<IEnumerable<DoctorDisplayModel>> GetAllWithDetailsAsync()
        {
            var result = new List<DoctorDisplayModel>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = @"SELECT doc.Id, doc.FullName, dep.Name as DepartmentName, doc.Phone 
                              FROM Doctors doc 
                              LEFT JOIN Departments dep ON doc.DepartmentId = dep.Id";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new DoctorDisplayModel
                            {
                                Id = (int)reader["Id"],
                                FullName = reader["FullName"].ToString() ?? string.Empty,
                                DepartmentName = reader["DepartmentName"].ToString() ?? string.Empty,
                                Phone = reader["Phone"].ToString() ?? string.Empty
                            });
                        }
                    }
                }
            }
            return result;
        }

        public Task AddAsync(Doctor entity) => ExecuteNonQueryAsync(
            "INSERT INTO Doctors (FullName, DepartmentId, Office, Phone) VALUES (@FullName, @DepartmentId, @Office, @Phone)",
            new SqlParameter("@FullName", entity.FullName),
            new SqlParameter("@DepartmentId", entity.DepartmentId),
            new SqlParameter("@Office", entity.Office),
            new SqlParameter("@Phone", entity.Phone));

        public Task UpdateAsync(Doctor entity) => ExecuteNonQueryAsync(
            "UPDATE Doctors SET FullName = @FullName, DepartmentId = @DepartmentId, Office = @Office, Phone = @Phone WHERE Id = @Id",
            new SqlParameter("@FullName", entity.FullName),
            new SqlParameter("@DepartmentId", entity.DepartmentId),
            new SqlParameter("@Office", entity.Office),
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
            BaseTreatmentCost = (decimal)reader["BaseTreatmentCost"]
        };

        public Task<Disease?> GetByIdAsync(int id) => GetByIdAsync("SELECT * FROM Diseases WHERE Id = @Id", id);
        public Task<IEnumerable<Disease>> GetAllAsync() => GetAllAsync("SELECT * FROM Diseases");

        public Task AddAsync(Disease entity) => ExecuteNonQueryAsync(
            "INSERT INTO Diseases (Name, BaseTreatmentCost) VALUES (@Name, @BaseTreatmentCost)",
            new SqlParameter("@Name", entity.Name),
            new SqlParameter("@BaseTreatmentCost", entity.BaseTreatmentCost));

        public Task UpdateAsync(Disease entity) => ExecuteNonQueryAsync(
            "UPDATE Diseases SET Name = @Name, BaseTreatmentCost = @BaseTreatmentCost WHERE Id = @Id",
            new SqlParameter("@Name", entity.Name),
            new SqlParameter("@BaseTreatmentCost", entity.BaseTreatmentCost),
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
            BirthDate = (DateTime)reader["BirthDate"]
        };

        public Task<Patient?> GetByIdAsync(int id) => GetByIdAsync("SELECT * FROM Patients WHERE Id = @Id", id);
        public Task<IEnumerable<Patient>> GetAllAsync() => GetAllAsync("SELECT * FROM Patients");

        public async Task<IEnumerable<PatientDisplayModel>> GetAllWithDetailsAsync(int? count = null)
        {
            var result = new List<PatientDisplayModel>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var topClause = count.HasValue ? $"TOP ({count.Value})" : "";
                var query = $@"SELECT {topClause} Id, FullName, BirthDate FROM Patients";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new PatientDisplayModel
                            {
                                Id = (int)reader["Id"],
                                FullName = reader["FullName"].ToString() ?? string.Empty,
                                BirthDate = (DateTime)reader["BirthDate"]
                            });
                        }
                    }
                }
            }
            return result;
        }

        public Task AddAsync(Patient entity) => ExecuteNonQueryAsync(
            "INSERT INTO Patients (FullName, BirthDate) VALUES (@FullName, @BirthDate)",
            new SqlParameter("@FullName", entity.FullName),
            new SqlParameter("@BirthDate", entity.BirthDate));

        public Task UpdateAsync(Patient entity) => ExecuteNonQueryAsync(
            "UPDATE Patients SET FullName = @FullName, BirthDate = @BirthDate WHERE Id = @Id",
            new SqlParameter("@FullName", entity.FullName),
            new SqlParameter("@BirthDate", entity.BirthDate),
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
            DoctorId = (int)reader["DoctorId"],
            DiseaseId = (int)reader["DiseaseId"],
            AdmissionDate = (DateTime)reader["AdmissionDate"],
            DischargeDate = reader["DischargeDate"] as DateTime?,
            DurationDays = (int)reader["DurationDays"],
            Status = reader["Status"].ToString() ?? string.Empty
        };

        public Task<Treatment?> GetByIdAsync(int id) => GetByIdAsync("SELECT * FROM Treatments WHERE Id = @Id", id);
        public Task<IEnumerable<Treatment>> GetAllAsync() => GetAllAsync("SELECT * FROM Treatments");

        public async Task<IEnumerable<TreatmentDisplayModel>> GetAllWithDetailsAsync(int? count = null)
        {
            var result = new List<TreatmentDisplayModel>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var topClause = count.HasValue ? $"TOP ({count.Value})" : "";
                var query = $@"SELECT {topClause} t.Id, p.FullName as PatientName, doc.FullName as DoctorName, dis.Name as DiseaseName, 
                              t.AdmissionDate, t.DischargeDate, t.DurationDays, t.Status 
                              FROM Treatments t 
                              LEFT JOIN Patients p ON t.PatientId = p.Id 
                              LEFT JOIN Doctors doc ON t.DoctorId = doc.Id 
                              LEFT JOIN Diseases dis ON t.DiseaseId = dis.Id";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new TreatmentDisplayModel
                            {
                                Id = (int)reader["Id"],
                                PatientName = reader["PatientName"].ToString() ?? string.Empty,
                                DoctorName = reader["DoctorName"].ToString() ?? string.Empty,
                                DiseaseName = reader["DiseaseName"].ToString() ?? string.Empty,
                                AdmissionDate = (DateTime)reader["AdmissionDate"],
                                DischargeDate = reader["DischargeDate"] as DateTime?,
                                DurationDays = (int)reader["DurationDays"],
                                Status = reader["Status"].ToString() ?? string.Empty
                            });
                        }
                    }
                }
            }
            return result;
        }

        public Task AddAsync(Treatment entity) => ExecuteNonQueryAsync(
            "INSERT INTO Treatments (PatientId, DoctorId, DiseaseId, AdmissionDate, DischargeDate, DurationDays, Status) VALUES (@PatientId, @DoctorId, @DiseaseId, @AdmissionDate, @DischargeDate, @DurationDays, @Status)",
            new SqlParameter("@PatientId", entity.PatientId),
            new SqlParameter("@DoctorId", entity.DoctorId),
            new SqlParameter("@DiseaseId", entity.DiseaseId),
            new SqlParameter("@AdmissionDate", entity.AdmissionDate),
            new SqlParameter("@DischargeDate", (object?)entity.DischargeDate ?? DBNull.Value),
            new SqlParameter("@DurationDays", entity.DurationDays),
            new SqlParameter("@Status", entity.Status));

        public Task UpdateAsync(Treatment entity) => ExecuteNonQueryAsync(
            "UPDATE Treatments SET PatientId = @PatientId, DoctorId = @DoctorId, DiseaseId = @DiseaseId, AdmissionDate = @AdmissionDate, DischargeDate = @DischargeDate, DurationDays = @DurationDays, Status = @Status WHERE Id = @Id",
            new SqlParameter("@PatientId", entity.PatientId),
            new SqlParameter("@DoctorId", entity.DoctorId),
            new SqlParameter("@DiseaseId", entity.DiseaseId),
            new SqlParameter("@AdmissionDate", entity.AdmissionDate),
            new SqlParameter("@DischargeDate", (object?)entity.DischargeDate ?? DBNull.Value),
            new SqlParameter("@DurationDays", entity.DurationDays),
            new SqlParameter("@Status", entity.Status),
            new SqlParameter("@Id", entity.Id));

        public Task DeleteAsync(int id) => ExecuteNonQueryAsync(
            "DELETE FROM Treatments WHERE Id = @Id",
            new SqlParameter("@Id", id));
    }

    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(string connectionString) : base(connectionString) { }

        protected override Payment Map(SqlDataReader reader) => new Payment
        {
            Id = (int)reader["Id"],
            TreatmentId = (int)reader["TreatmentId"],
            Amount = (decimal)reader["Amount"],
            PaymentDate = (DateTime)reader["PaymentDate"]
        };

        public Task<Payment?> GetByIdAsync(int id) => GetByIdAsync("SELECT * FROM Payments WHERE Id = @Id", id);
        public Task<IEnumerable<Payment>> GetAllAsync() => GetAllAsync("SELECT * FROM Payments");

        public Task AddAsync(Payment entity) => ExecuteNonQueryAsync(
            "INSERT INTO Payments (TreatmentId, Amount, PaymentDate) VALUES (@TreatmentId, @Amount, @PaymentDate)",
            new SqlParameter("@TreatmentId", entity.TreatmentId),
            new SqlParameter("@Amount", entity.Amount),
            new SqlParameter("@PaymentDate", entity.PaymentDate));

        public Task UpdateAsync(Payment entity) => ExecuteNonQueryAsync(
            "UPDATE Payments SET TreatmentId = @TreatmentId, Amount = @Amount, PaymentDate = @PaymentDate WHERE Id = @Id",
            new SqlParameter("@TreatmentId", entity.TreatmentId),
            new SqlParameter("@Amount", entity.Amount),
            new SqlParameter("@PaymentDate", entity.PaymentDate),
            new SqlParameter("@Id", entity.Id));

        public Task DeleteAsync(int id) => ExecuteNonQueryAsync(
            "DELETE FROM Payments WHERE Id = @Id",
            new SqlParameter("@Id", id));
    }
}
