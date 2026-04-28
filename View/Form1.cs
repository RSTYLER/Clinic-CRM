using Clinic_Data;
using Clinic_Model.intefaces;

namespace Clinic_CRM;

public partial class Form1 : Form
{
    private readonly IClinicDataService _dataService;
    private const string ConnectionString = @"Data Source=JADESTAR\SQLEXPRESS;Initial Catalog=Clinic_DB;Integrated Security=True;Encrypt=False;TrustServerCertificate=False";

    public Form1()
    {
        InitializeComponent();
        _dataService = new ClinicDataService(ConnectionString);
    }

    private async void btnShowPatients_Click(object? sender, EventArgs e)
    {
        try
        {
            btnShowPatients.Enabled = false;
            var patients = await _dataService.Patients.GetAllWithDetailsAsync(30);
            dgvPatients.DataSource = patients.ToList();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при получении данных: {ex.Message}");
        }
        finally
        {
            btnShowPatients.Enabled = true;
        }
    }
}