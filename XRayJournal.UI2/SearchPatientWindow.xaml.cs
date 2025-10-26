using System.Windows;
using XRayJournal.Core2.DTOs;
using XRayJournal.BLL2;

namespace XRayJournal.UI2
{
    public partial class SearchPatientWindow : Window
    {
        private PatientLogic _patientLogic; // Вызов бизнес-логики без необходимости делать это каждый раз в каждом методе
        private PatientDTO _currentPatient; // Поле для работы с текущим пациентом в данном окне
        public SearchPatientWindow()
        {
            InitializeComponent();
            _patientLogic = new PatientLogic(); // инициализация поля, чтобы оно не было null
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
                if (!int.TryParse(IdEnterTextBox.Text, out int patientId))
                {
                    InfoTextBox.Text = "Ошибка: введите корректный id пациента!";
                    return;
                }
                // Получаем пациента из базы через BLL
                _currentPatient = _patientLogic.GetPatientById(patientId);
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
                if (_currentPatient == null)
                {
                    InfoTextBox.Text = "Ошибка: сначала найдите пациента для удаления!";
                    return;
                }
                _patientLogic.DeletePatient(_currentPatient.Id);
                ClearFields();
                InfoTextBox.Text = $"Пациент с Id {_currentPatient.Id} удалён.";
                _currentPatient = null;
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
                var updatedPatient = new PatientDTO()
                {
                    Id = _currentPatient.Id,
                    SecondName = PatientSecondNameTextBox.Text.Trim(),
                    FirstName = PatientFirstNameTextBox.Text.Trim(),
                    ThirdName = PatientThirdNameTextBox.Text.Trim(),
                    BirthDate = DateOnly.Parse(PatientBirthDateTextBox.Text),
                    Sex = PatientSexTextBox.Text.Trim()
                };

                // Обновляем пациента в базе через BLL
                _patientLogic.UpdatePatient(updatedPatient);
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

        private void ClearFields()
        {
            PatientIDTextBox.Text = string.Empty;
            PatientSecondNameTextBox.Text = string.Empty;
            PatientFirstNameTextBox.Text = string.Empty;
            PatientThirdNameTextBox.Text = string.Empty;
            PatientBirthDateTextBox.Text = string.Empty;
            PatientSexTextBox.Text = string.Empty;
        }
    }
}
