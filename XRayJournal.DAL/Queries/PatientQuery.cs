using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XRayJournal.DAL.Queries
{
    public class PatientQuery
    {
        public const string AddPatient =
            """
            INSERT 
            INTO public."Patient"("SecondName", "FirstName", "ThirdName", "BirthDate", "Sex")
            VALUES (@secondName, @firstName, @thirdName, @birthDate, @Sex);
            """;
    }
}
