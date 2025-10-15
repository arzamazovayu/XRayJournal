using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XRayJournal.DAL2.Queries
{
    public class XRayExamQuery
    {
        public const string GetAllXRays =
            """
            SELECT id, 
            "XRayName", 
            "XRayDose", 
            "XRayShots", 
            "XRayDate", 
            "XRayDiagnose", 
            "XRayPatology", 
            "XRayCost", 
            "Doctor", 
            "Laborant", 
            "XRayModality", 
            "InOperation", 
            "Contrast"
            FROM public."XRayExam";
            """;

    }
}
