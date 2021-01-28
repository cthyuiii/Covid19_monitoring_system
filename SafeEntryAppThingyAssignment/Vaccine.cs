using System;
using System.Collections.Generic;
using System.Text;

namespace SafeEntryAppThingyAssignment
{
    class Vaccine
    {
        public string vaccinated { get; set; }

        public string mrn { get; set; }

        public string vaccineName { get; set; }

        public string vaccineCenter { get; set; }

        public DateTime vaccinatedTime { get; set; }

        public Vaccine(string Vaccinated,string Mrn, string VaccineName, string VaccineCenter, DateTime VaccinatedTime)
        {
            Vaccinated = vaccinated;
            Mrn = mrn;
            VaccineName = vaccineName;
            VaccineCenter = vaccineCenter;
            VaccinatedTime = vaccinatedTime;
        }

        public override string ToString()
        {
            return "Vaccinated?" + vaccinated +"\tMedical Reference Number:" + mrn +
                "\tVaccine Name: " + vaccineName + "\tVaccination Center: " + vaccineCenter
                + "\tTime of Vaccination: " + vaccinatedTime;
        }
    }
}
