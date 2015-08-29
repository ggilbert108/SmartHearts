using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHearts
{
    class GameState
    {
        private Player[] players;
        private int turnIndex;
        private int cardsPlayedInTrick;
        private Card[] trick;
        private Suit leadSuit;

        public GameState()
        {
            players = new Player[4];

            for (int i = 0; i < 4; i++)
                players[i] = new Player(i);

            Deck deck = new Deck();
            deck.distribute(players, 0);
            turnIndex = getFirstPlayer();
            trick = new Card[4];
            leadSuit = Suit.None;
            cardsPlayedInTrick = 0;
        }

        public GameState(Player[] players, int turnIndex, Card[] trick, Suit leadSuit, int cardsPlayedInTrick)
        {
            this.players = new Player[4];
            for (int i = 0; i < 4; i++)
            {
                this.players[i] = players[i].clone();
            }
            this.turnIndex = turnIndex;
            this.trick = trick;
            this.leadSuit = leadSuit;
            this.cardsPlayedInTrick = cardsPlayedInTrick;
        }


        public GameState getNextState(bool preserve, Card played, bool playRandom)
        {
            GameState newState = new GameState(players, turnIndex, trick, leadSuit, cardsPlayedInTrick);
            if (preserve)
            {
                if (played == null)
                {
                    newState.getNextState(playRandom);
                }
                else
                {
                    newState.getNextState(played);
                }
            }
            return newState;
        }

        public void getNextState(Card played)
        {
            trick[turnIndex] = played;
            players[turnIndex].removeCard(played);
            Console.WriteLine(played);

            if (leadSuit == Suit.None)
            {
                leadSuit = played.suit;
            }
            cardsPlayedInTrick++;

            if (cardsPlayedInTrick == 4)
            {
                scoreTrick(leadSuit);
            }
            else
            {
                turnIndex = (turnIndex + 1) % 4;
            }

        }

        private void getNextState(bool playRandom)
        {
            Card played;
            if (turnIndex == 3 && !playRandom)
            {
                played = players[turnIndex].playCardSmart(leadSuit, this);
            }
            else
            {
                played = players[turnIndex].playCardRandom(leadSuit);
            }
            trick[turnIndex] = played;

            Console.WriteLine(played);

            if (leadSuit == Suit.None)
            {
                leadSuit = played.suit;
            }
            cardsPlayedInTrick++;

            if (cardsPlayedInTrick == 4)
            {
                scoreTrick(leadSuit);
            }
            else
            {
                turnIndex = (turnIndex + 1) % 4;
            }
        }

        private void scoreTrick(Suit leadSuit)
        {
            int pIndex = (turnIndex + 1) % 4;
            int maxRank = 0;
            int winner = pIndex;

            int trickScore = 0;
            for (int i = 0; i < 4; i++)
            {
                Card card = trick[i];

                if (card.suit == Suit.Hearts)
                    trickScore++;
                if (card.suit == Suit.Spades && card.rank == 12)
                    trickScore += 13;

                if (card.rank > maxRank)
                {
                    winner = pIndex;
                    maxRank = card.rank;
                }
                pIndex = (pIndex + 1) % 4;
            }
            players[winner].score += trickScore;

            //reset trick
            turnIndex = winner;
            trick = new Card[4];
            leadSuit = Suit.None;
            cardsPlayedInTrick = 0;
        }

        public bool gameOver()
        {
            for (int i = 0; i < 4; i++)
            {
                if (players[i].hand.Count > 0)
                    return false;
            }
            return true;
        }

        public int getBestScore()
        {
            int min = 100;
            int minIndex = -1;
            for (int i = 0; i < 4; i++)
            {
                if (players[i].score < min)
                {
                    min = players[i].score;
                    minIndex = i;
                }
            }
            return minIndex;
        }

        private int getFirstPlayer()
        {
            Card startCard = new Card(Suit.Clubs, 2);
            for (int i = 0; i < 4; i++)
            {
                if (players[i].hasCard(startCard))
                {
                    return i;
                }
            }
            return -1;
        }

        public void printScores()
        {
            foreach (Player player in players)
            {
                Console.WriteLine("Player " + player.index + " score: " + player.score);
            }
        }
    }
}
