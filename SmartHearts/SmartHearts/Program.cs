using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHearts
{
    class Program
    {
        static void Main(string[] args)
        {
            GameState state = new GameState();

            for (int i = 0; i < 52; i++)
            {
                state = state.getNextState(true, null, false);
            }

            state.printScores();
            Console.ReadLine();
        }
    }
}
