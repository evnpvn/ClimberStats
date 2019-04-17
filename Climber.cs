using System;
using System.Collections.Generic;
using System.Linq;

namespace ClimberStats
{
    public class Climber
    {
        public string FullName { get; set{ "";} }
        public string FirstName 
        { 
          get { return FirstName = (FullName.Split(" ", 2)).FirstOrDefault(); }
          private set { } 
        }
        public string LastName
        { 
          get { return FirstName = (FullName.Split(" ", 2)).LastOrDefault(); }
          private set { } 
        }
        public string Nationality { get; set; }

        public double Height { get; set; }
        public double Weight { get; set; }
        public double Bmi 
        { 
          get 
          { 
            if(Height == 0)
            {
              return Bmi = 0;
            }
            else
            {
              return Bmi =  Weight/((Math.Pow(Height/100,2))); 
            }
          }
          private set { } 
        }           
        public int BirthYear { get; set; }
    }
}