using Npgsql;
using XRayJournal.Core2;
using XRayJournal.Core2.DTOs;
using XRayJournal.DAL2.Queries;

namespace XRayJournal.DAL2
{
    public class PatientRepository
    {
        public void AddPatient(PatientDTO patient)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(Options.ConnectionString))
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand(PatientQuery.AddPatient, connection);

                command.Parameters.Add(new NpgsqlParameter("@secondName", patient.SecondName));
                command.Parameters.Add(new NpgsqlParameter("@firstName", patient.FirstName));
                command.Parameters.Add(new NpgsqlParameter("@thirdName", patient.ThirdName));
                command.Parameters.Add(new NpgsqlParameter("@birthDate", patient.BirthDate));
                command.Parameters.Add(new NpgsqlParameter("@sex", patient.Sex));

                command.ExecuteNonQuery();
            }
        }

        public PatientDTO GetPatientById(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(Options.ConnectionString))
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand(PatientQuery.GetPatientById, connection);

                command.Parameters.Add(new NpgsqlParameter("@id", id));
                //command.ExecuteNonQuery();

                NpgsqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    PatientDTO patient = new PatientDTO()
                    {
                        Id = reader.GetInt32(0),
                        SecondName = reader.GetString(1),
                        FirstName = reader.GetString(2),
                        ThirdName = reader.GetString(3),
                        BirthDate = reader.GetFieldValue<DateOnly>(4),
                        Sex = reader.GetString(5)

                    };
                    return patient;
                }
                else
                {
                    throw new Exception($"Пациента с таким Id={id} не существует!");
                }
            }
        }
        public void DeletePatient(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(Options.ConnectionString))
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand(PatientQuery.DeletePatientById, connection);
                command.Parameters.Add(new NpgsqlParameter("@id", id));
                int n = command.ExecuteNonQuery();

                if (n == 0)
                {
                    throw new ArgumentOutOfRangeException($"Пациента с Id={id} не существует");
                }

            }
        }
        public List<PatientsWithXRaysDTO> GetPatientsWithXRays()
        {
            var patientX = new List<PatientsWithXRaysDTO>();

            using (NpgsqlConnection connection = new NpgsqlConnection(Options.ConnectionString))
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand(PatientQuery.GetPatientsWithXRays, connection);
                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var patient = new PatientsWithXRaysDTO()
                    {
                        PatientId = reader.GetInt32(0),
                        SecondName = reader.GetString(1),
                        FirstName = reader.GetString(2),
                        ThirdName = reader.IsDBNull(3) ? null : reader.GetString(3),
                        BirthDate = reader.GetFieldValue<DateOnly>(4),
                        Sex = reader.GetString(5),

                        XRayId = reader.IsDBNull(6) ? (int?)null : reader.GetInt32(6),
                        XRayName = reader.IsDBNull(7) ? null : reader.GetString(7),
                        XRayDose = reader.IsDBNull(8) ? (float?)null : reader.GetFloat(8),
                        XRayShots = reader.IsDBNull(9) ? (byte?)null : reader.GetByte(9),
                        XRayDate = reader.IsDBNull(10) ? (DateOnly?)null : reader.GetFieldValue<DateOnly>(10),
                        XRayDiagnose = reader.IsDBNull(11) ? null : reader.GetString(11),
                        XRayPatology = reader.IsDBNull(12) ? (bool?)null : reader.GetBoolean(12),
                        XRayCost = reader.IsDBNull(13) ? (int?)null : reader.GetInt32(13),
                        Doctor = reader.IsDBNull(14) ? null : reader.GetString(14),
                        Laborant = reader.IsDBNull(15) ? null : reader.GetString(15),
                        XRayModality = reader.IsDBNull(16) ? null : reader.GetString(16),
                        InOperation = reader.IsDBNull(17) ? (bool?)null : reader.GetBoolean(17),
                        Contrast = reader.IsDBNull(18) ? (bool?)null : reader.GetBoolean(18)
                    };
                    patientX.Add(patient);
                }
                return patientX;

            }
        }
    }
}
