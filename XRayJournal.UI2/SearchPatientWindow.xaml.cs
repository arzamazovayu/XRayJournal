using System.Windows;
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

        private PatientDTO _currentPatient;
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверяем корректность ввода Id
                if (!int.TryParse(IdEnterTextBox.Text, out int patientId))
                {
                    InfoTextBox.Text = "Ошибка: введите корректный id пациента!";
                    return;
                }
                // Получаем пациента из базы
                _currentPatient = new PatientRepository().GetPatientById(patientId);
                // Заполняем текстбоксы данными пациента
                FillPatientFields(_currentPatient);
            }
            catch (Exception ex)
            {
                InfoTextBox.Text = $"Ошибка: {ex.Message}";
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверяем корректность ввода Id
                if (!int.TryParse(IdEnterTextBox.Text, out int patientId))
                {
                    InfoTextBox.Text = "Ошибка: введите id пациента!";
                    return;
                }
                new PatientRepository().DeletePatient(patientId);

                InfoTextBox.Text = $"Пациент с Id {patientId} удалён.";
            }
            catch (Exception ex)
            {
                InfoTextBox.Text = $"Ошибка: {ex.Message}";
            }
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_currentPatient == null)
                {
                    InfoTextBox.Text = "Ошибка: введите id пациента!";
                    return;
                }
                // Создаем "нового" пациента из полей для обновления
                PatientDTO updatedPatient = new PatientDTO()
                {
                    Id = _currentPatient.Id,
                    SecondName = PatientSecondNameTextBox.Text.Trim(),
                    FirstName = PatientFirstNameTextBox.Text.Trim(),
                    ThirdName = PatientThirdNameTextBox.Text.Trim(),
                    BirthDate = DateOnly.Parse(PatientBirthDateTextBox.Text),
                    Sex = PatientSexTextBox.Text.Trim()
                };

                // Проверка обязательных полей
                if (string.IsNullOrWhiteSpace(updatedPatient.SecondName) ||
                    string.IsNullOrWhiteSpace(updatedPatient.FirstName) ||
                    string.IsNullOrWhiteSpace(updatedPatient.Sex))
                {
                    InfoTextBox.Text = "Ошибка: введите ФИ и пол пациента полностью!";
                    return;
                }

                // Обновляем пациента в базе
                new PatientRepository().UpdatePatient(updatedPatient);
                _currentPatient = updatedPatient;

                InfoTextBox.Text = "Данные пациента успешно обновлены!";

            }
            catch (Exception ex)
            {
                InfoTextBox.Text = $"Ошибка: {ex.Message}";
            }
        }
        private void FillPatientFields(PatientDTO patient)
        {
            PatientIDTextBox.Text = patient.Id.ToString();
            PatientSecondNameTextBox.Text = patient.SecondName;
            PatientFirstNameTextBox.Text = patient.FirstName;
            PatientThirdNameTextBox.Text = patient.ThirdName;
            PatientBirthDateTextBox.Text = patient.BirthDate.ToString("dd.MM.yyyy");
            PatientSexTextBox.Text = patient.Sex;
        }
    }
}
