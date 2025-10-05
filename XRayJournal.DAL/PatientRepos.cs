using Npgsql;
using XRayJournal.Core;
using XRayJournal.Core.DTOs;
using XRayJournal.DAL.Queries;

namespace XRayJournal.DAL
{
    public class PatientRepos
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
