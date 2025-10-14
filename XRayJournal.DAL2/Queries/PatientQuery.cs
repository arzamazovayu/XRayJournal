using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XRayJournal.DAL2.Queries
{
    public class PatientQuery
    {
        public const string AddPatient =
           """
            INSERT 
            INTO public."Patient"("SecondName", "FirstName", "ThirdName", "BirthDate", "Sex")
            VALUES (@secondName, @firstName, @thirdName, @birthDate, @Sex);
            """;
        public const string SelectPatients =
            """                
            SELECT id, "SecondName", "FirstName", "ThirdName", "BirthDate", "Sex"
            FROM public."Patient";
            """;
    }
}
