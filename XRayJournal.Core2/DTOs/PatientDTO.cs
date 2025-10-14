using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XRayJournal.Core2.DTOs
{
    public class PatientDTO
    {
        public int Id { get; set; }
        public string SecondName { get; set; }
        public string FirstName { get; set; }
        public string ThirdName { get; set; }
        public DateOnly BirthDate { get; set; } //нужно найти тип данных для даты
    }
}
