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
    public partial class AddPatientWindow : Window
    {
        public AddPatientWindow()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверяем корректность ввода данных
                if (string.IsNullOrEmpty(SecondNameBox.Text)) //проверка строковых значений
                {
                    InfoBox.Text = "Ошибка: Введите фамилию пациента!";
                    return;
                }
                if (string.IsNullOrEmpty(FirstNameBox.Text))
                {
                    InfoBox.Text = "Ошибка: Введите имя пациента!";
                    return;
                }
                if (string.IsNullOrEmpty(BirthDateBox.Text))
                {
                    InfoBox.Text = "Ошибка: Введите дату рождения пациента!";
                    return;
                }
                if (!DateOnly.TryParse(BirthDateBox.Text, out DateOnly birthDate)) //проверка цифровых значений
                {
                    InfoBox.Text = "Ошибка: неверный формат даты! Используйте дд.мм.гггг";
                    return;
                }
                string selectedSex = ""; //извлекаем текст из комбобокса
                if (SexComboBox.SelectedItem is ComboBoxItem comboBoxItem)
                {
                    selectedSex = comboBoxItem.Content.ToString();
                }
                    //создаём ДТО пациента для передачи в AddPatient
                    PatientDTO patient = new PatientDTO()
                    {
                        SecondName = SecondNameBox.Text.Trim(),
                        FirstName = FirstNameBox.Text.Trim(),
                        ThirdName = ThirdNameBox.Text?.Trim() ?? "", //отчество может быть пустым
                        BirthDate = birthDate,
                        Sex = selectedSex
                    };
                //добавляем пациента в базу
                new PatientRepository().AddPatient(patient);

                InfoBox.Text = $"Пациент {patient.SecondName} добавлен.";
                //чистим поля
                ClearFields();
            }
            catch (Exception ex)
            {
                InfoBox.Text = $"Ошибка: {ex.Message}";
            }
        }
        private void ClearFields() //метод чистки полей (просто приравниваем пустые значения)
        {
            SecondNameBox.Text = "";
            FirstNameBox.Text = "";
            ThirdNameBox.Text = "";
            BirthDateBox.Text = "";
            SexComboBox.SelectedIndex = 2;

        }
    }
}
