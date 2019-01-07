using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTDiceExam
{
    public class DiceRoll
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public int Guess { get; set; }
        public int Result { get; set; }

        public DiceRoll(int id, string name, int number, int guess, int result)
        {
            Id = id;
            Name = name;
            Number = number;
            Guess = guess;
            Result = result;
        }
        
        public DiceRoll()
        {

        }

        public override string ToString()
        {
            return Id + Name + Number + Guess + Result;
        }
    }
}
