using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServiceLibrary
{
    public static class UnitsConvert
    {
        public static Sex ConvertSex(string sex)
        {
            Sex cSex = Sex.Male;
            switch (sex)
            {
                case "М":
                    cSex = Sex.Male;
                    break;
                case "Ж":
                    cSex = Sex.Female;
                    break;
            }
            return cSex;
        }
        public static string ConvertSex(Sex sex)
        {
            string cSex = "";
            switch (sex)
            {
                case Sex.Male:
                    cSex = "М";
                    break;
                case Sex.Female:
                    cSex = "Ж";
                    break;
            }
            return cSex;
        }
        public static Units ConvertUnit(string unit)
        {
            Units cUnit = Units.Nothing;
            switch (unit)
            {
                case "шт":
                    cUnit = Units.Pieces;
                    break;
                case "л":
                    cUnit = Units.Liters;
                    break;
                case "нч":
                    cUnit = Units.Hours;
                    break;
                case "кг":
                    cUnit = Units.Kilograms;
                    break;
            }
            return cUnit;
        }
        public static string ConvertUnit(Units unit)
        {
            string cUnit = "";
            switch (unit)
            {
                case Units.Pieces:
                    cUnit = "шт";
                    break;
                case Units.Liters:
                    cUnit = "л";
                    break;
                case Units.Hours:
                    cUnit = "нч";
                    break;
                case Units.Kilograms:
                    cUnit = "кг";
                    break;
            }
            return cUnit;
        }
    }
}
