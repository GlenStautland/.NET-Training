using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JokesDB.Models
{
    public class Joke
    {
        public int Id { get; set; }
        public string JokeQuestion { get; set; }
        public string JokeAnswear { get; set; }

        public Joke()
        {

        }

    }
}
