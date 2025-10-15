using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using XRayJournal.Core2.DTOs;
using XRayJournal.DAL2;

namespace XRayJournal.UI2
{
    public partial class SearchPatientWindow : Window
    {
        public SearchPatientWindow()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверяем корректность ввода Id
                if (!int.TryParse(IdEnterTextBox.Text, out int a))
                {
                    PatientInfoTextBox.Text = "Ошибка: введите id пациента!";
                    return;
                }
                PatientDTO p = new PatientRepository().GetPatientById(a);

                PatientInfoTextBox.Text = p.ToString();
            }
            catch (Exception ex)
            {
                PatientInfoTextBox.Text = $"Ошибка: {ex.Message}";
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверяем корректность ввода Id
                if (!int.TryParse(IdEnterTextBox.Text, out int a))
                {
                    PatientInfoTextBox.Text = "Ошибка: введите id пациента!";
                    return;
                }
                new PatientRepository().DeletePatient(a);

                PatientInfoTextBox.Text = $"Пациент с Id {a} удалён.";
            }
            catch (Exception ex)
            {
                PatientInfoTextBox.Text = $"Ошибка: {ex.Message}";
            }
        }
    }
}
