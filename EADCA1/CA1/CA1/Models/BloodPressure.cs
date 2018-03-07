// X00122026
// Graham Lalor

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CA1.Models
{
    public class BloodPressure
    {
        [Range(1,200)]
        [Required]
        public int Systolic { get; set; }

        [Range(1, 200)]
        [Required]
        public int Diastolic { get; set; }

        public String calculateCategory()
        {
            String result = "";
            if(Systolic < 90 || Diastolic < 60)
            {
                result = "Low blood pressure";
            }
            else if (Systolic < 120 && Diastolic < 80)
            {
                result = "Normal blood pressure";
            }
            else if (Systolic < 139 || Diastolic < 89)
            {
                result = "Pre-hypertension";
            }
            else if (Systolic < 159 || Diastolic < 99)
            {
                result = "Stage 1 hypertension";
            }
            else
            {
                result = "Stage 2 hypertension";
            }
            return result;
        }

    }

    public class Patient
    {
        public int ID { get; set; }

        public List<BloodPressure> readings;
        
    }
}