using XRayJournal.Core2.DTOs;
using XRayJournal.DAL2;

namespace XRayJournal.BLL2
{
    public class PatientLogic
    {
        // Создаём защищённое поле _patientRepository типа PatientRepository, для хранения ссылки на репозиторий
        private readonly PatientRepository _patientRepository;
        // Инициализируем поле в конструкторе (композиция)
        public PatientLogic()
        {
            _patientRepository = new PatientRepository();
        }
        // Теперь логика пациента может ходить в репозиторий пациента,
        // поле _patientRepository созданного объекта PatientLogic не может быть заменено на другой объект
        public PatientDTO GetPatientById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id пациента должен быть положительным числом", nameof(id));
            }
            try
            {
                var patient = _patientRepository.GetPatientById(id);
                return patient;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка: patient id {id}, {ex.Message}", ex);
            }
        }

        public void AddPatient(PatientDTO patient)
        {
            ValidatePatientData(patient);
            _patientRepository.AddPatient(patient);
        }

        public void UpdatePatient(PatientDTO patient) 
        {
            ValidatePatientData(patient);
            _patientRepository.UpdatePatient(patient);
        }

        public void DeletePatient(int id) 
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id пациента должен быть положительным числом", nameof(id));
            }
            _patientRepository.DeletePatient(id);
        }
        private void ValidatePatientData(PatientDTO patient)
        {
            if (string.IsNullOrWhiteSpace(patient.SecondName))
            {
                throw new ArgumentException("Фамилия пациента обязательна для заполнения");
            }
            if (string.IsNullOrWhiteSpace(patient.FirstName))
            {
                throw new ArgumentException("Имя пациента обязательно для заполнения");
            }
            if (string.IsNullOrWhiteSpace(patient.Sex))
            {
                throw new ArgumentException("Пол пациента обязателен для заполнения");
            }
            var age = DateTime.Now.Year - patient.BirthDate.Year;
            if (age < 0 || age > 150)
            {
                throw new ArgumentException("некорректная дата рождения");
            }
        }
    }
}
