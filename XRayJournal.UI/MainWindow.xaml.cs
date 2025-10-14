using System;
using System.Data;
using System.Windows;
using Npgsql;

namespace XRayJournal.UI
{
    public partial class MainWindow : Window
    {
        private NpgsqlDataAdapter _adapter;

        private DataSet _dataSet;

        public MainWindow()
        {
            string cs = Environment.GetEnvironmentVariable("postgres");
            string selectQuery = 
                """                
                INSERT 
                INTO public."Patient"("SecondName", "FirstName", "ThirdName", "BirthDate", "Sex")
                VALUES (@secondName, @firstName, @thirdName, @birthDate, @Sex);
                """;
            using (NpgsqlConnection connection = new NpgsqlConnection(cs))
            {
                _adapter = new NpgsqlDataAdapter(selectQuery, connection);
                _dataSet = new DataSet();

                _adapter.Fill(_dataSet, "dgPatients");
            }

            InitializeComponent();

            dgPatients.ItemSource = _dataSet.Tables[0].DefaultView;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
