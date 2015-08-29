using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHearts
{
    static class AI
    {
        private const int NUM_PLAYOUTS = 10;

        public static Card getBestMove(List<Card> legal, GameState state, int pIndex)
        {
            Card bestCard = legal[0];
            int maxWins = 0;
            for (int i = 0; i < legal.Count; i++)
            {
                int wins = getPlayouts(state, legal[i], pIndex);
                if(wins > maxWins)
                {
                    maxWins = wins;
                    bestCard = legal[i];
                }
            }
            return bestCard;
        }

        private static int getPlayouts(GameState state, Card card, int pIndex)
        {
            int wins = 0;

            if (state.gameOver()) return 0;
            GameState playState = state.getNextState(true, card, true);
            for (int i = 0; i < NUM_PLAYOUTS; i++)
            {
                if (playout(playState, pIndex))
                    wins++;
            }
            return wins;
        }

        private static bool playout(GameState state, int pIndex)
        {
            GameState playState = state.getNextState(true, null, true);
            while (!playState.gameOver())
            {
                playState = playState.getNextState(true, null, true);
            }

            int bestScore = state.getBestScore();
            return bestScore == pIndex;
        }
    }
}
