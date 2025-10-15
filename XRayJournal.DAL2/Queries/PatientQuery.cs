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
            VALUES (@secondName, @firstName, @thirdName, @birthDate, @sex);
            """;
        public const string GetPatientById =
            """                
            SELECT id, "SecondName", "FirstName", "ThirdName", "BirthDate", "Sex"
            FROM public."Patient"
            WHERE "id"=@id;
            """;
        public const string GetAllPatients =
            """
            SELECT id, "SecondName", "FirstName", "ThirdName", "BirthDate", "Sex"
            FROM public."Patient";
            """;
        public const string DeletePatientById =
            """
            DELETE FROM public."Patient"
            WHERE "id"=@id;
            """;
        public const string UpdatePatientById =
            """
            UPDATE public."Patient"
            SET 
            "SecondName" = @secondName, 
            "FirstName" = @firstName, 
            "ThirdName" = @thirdName, 
            "BirthDate" = @birthDate, 
            "Sex" = @Sex
            WHERE "id"=@id;
            """;
        public const string GetAllPatientsInfo =
            """
            SELECT id, 
            "PatientDataID", 
            "XRayExamID", 
            "PatientHistoryID", 
            "DepartmentID", 
            "PatientCategoryID", 
            "SecondName", 
            "FirstName", 
            "ThirdName", 
            "BirthDate", 
            "Sex"
            FROM public."Patient";
            """;
        public const string GetPatientsWithXRays =
            """
            SELECT 
                p."id" as PatientId,
                p."SecondName",
                p."FirstName", 
                p."ThirdName",
                p."BirthDate",
                p."Sex",
                x."id" as XRayId,
                x."XRayName",
                x."XRayDose",
                x."XRayShots",
                x."XRayDate",
                x."XRayDiagnose",
                x."XRayPatology",
                x."XRayCost",
                x."Doctor",
                x."Laborant",
                x."XRayModality",
                x."InOperation",
                x."Contrast"
            FROM public."Patient" p
            LEFT JOIN public."XRayExam" x ON p."id" = x."id"
            ORDER BY p."id", x."XRayDate";
            """;
    }
}
