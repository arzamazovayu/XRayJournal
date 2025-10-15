using Npgsql;
using System.Data;
using System.Windows;
using XRayJournal.DAL2.Queries;

namespace XRayJournal.UI2
{
    public partial class MainWindow : Window
    {
        private NpgsqlDataAdapter _adapter;
        private NpgsqlConnection _connection;
        private DataSet _dataSet;

        public MainWindow()
        {
            InitializeComponent();

            string cs = Environment.GetEnvironmentVariable("postgres");
            string selectQuery = PatientQuery.GetPatientsWithXRays;

            _connection = new NpgsqlConnection(cs);
            _connection.Open();
            _adapter = new NpgsqlDataAdapter(selectQuery, _connection);
            _dataSet = new DataSet();

            //_adapter.Fill(_dataSet, "dtPatients");
            //_connection.Close();

            //dgPatients.ItemsSource = _dataSet.Tables["dtPatients"].DefaultView;
            _adapter.Fill(_dataSet, "dtPatientsWithXRays");
            _connection.Close();

            dgPatients.ItemsSource = _dataSet.Tables["dtPatientsWithXRays"].DefaultView;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            NpgsqlCommandBuilder commandBuilder = new NpgsqlCommandBuilder(_adapter);
            //_adapter.Update(_dataSet, "dtPatients");
            _adapter.Update(_dataSet, "dtPatientsWithXRays");
            _dataSet.Clear();
            //_adapter.Fill(_dataSet, "dtPatients");
            _adapter.Fill(_dataSet, "dtPatientsWithXRays");
            dgPatients.UpdateLayout();
        }

        private void SearchPatientButton_Click(object sender, RoutedEventArgs e)
        {
            SearchPatientWindow SPWindow = new SearchPatientWindow();
            SPWindow.Owner = this;
            SPWindow.Show();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            AddPatientWindow APWindow = new AddPatientWindow();
            APWindow.Owner = this;
            APWindow.Show();
        }
    }
}