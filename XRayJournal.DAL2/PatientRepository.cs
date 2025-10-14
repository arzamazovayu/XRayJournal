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
            //2:04:30 решили вопрос с переменной среды
            using (NpgsqlConnection connection = new NpgsqlConnection(Options.ConnectionString))
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand(PatientQuery.AddPatient, connection);

                //NpgsqlParameter parameter= new NpgsqlParameter("@secondName", patient.SecondName);

                command.Parameters.Add(new NpgsqlParameter("@secondName", patient.SecondName));
                command.ExecuteNonQuery();
            }
        }
    }
}
