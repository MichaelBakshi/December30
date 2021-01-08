using System;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace December30
{
    class Car
    {
        public Int64 ID { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public Int64 Year { get; set; }
        public Car()
        {

        }

        public Car(string manufacturer, string model, int year )
        {
            Manufacturer = manufacturer;
            Model = model;
            Year = year;
        }


        public override string ToString()
        {
            return $"Car: {ID} {Manufacturer} {Model} {Year} ";
        }
    }
}
