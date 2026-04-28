using Clinic_Data;
using Clinic_Model.intefaces;

namespace Clinic_CRM;

public partial class Form1 : Form
{
    private readonly IClinicDataService _dataService;
    private const string ConnectionString = @"Data Source=JADESTAR\SQLEXPRESS;Initial Catalog=ClinicDB;Integrated Security=True;Encrypt=False;TrustServerCertificate=False";

    public Form1()
    {
        InitializeComponent();
        _dataService = new ClinicDataService(ConnectionString);
        this.Load += Form1_Load;
    }

    private async void Form1_Load(object? sender, EventArgs e)
    {
        try
        {
            await LoadData();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
        }
    }

    private async Task LoadData()
    {
        // Пример получения данных для View
        var patients = await _dataService.Patients.GetAllWithDetailsAsync();
        
        // Здесь можно привязать к DataGridView, если бы он был на форме:
        // dataGridView1.DataSource = patients.ToList();
    }
}