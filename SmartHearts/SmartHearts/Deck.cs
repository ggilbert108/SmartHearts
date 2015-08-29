using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHearts
{
    class Deck
    {
        private List<Card> cards;

        public Deck()
        {
            cards = new List<Card>();

            Suit[] suits = { Suit.Clubs, Suit.Hearts, Suit.Spades, Suit.Diamonds };
            for (int i = 2; i <= 14; i++)
            {
                foreach (Suit suit in suits)
                {
                    Card card = new Card(suit, i);
                    cards.Add(card);
                }
            }
            shuffle();
        }

        public Deck(List<Card> cards)
        {
            this.cards = cards;
        }

        private void shuffle()
        {
            for (int i = cards.Count - 1; i > 0; i--)
            {
                int j = Util.random.Next(i);
                Card temp = cards[i];
                cards[i] = cards[j];
                cards[j] = temp;
            }
        }

        public void distribute(Player[] players, int startPlayer)
        {
            int cur = startPlayer;

            foreach (Card card in cards)
            {
                players[cur].giveCard(card);
                cur = (cur + 1) % players.Length;
            }
        }

        public void removeCard(Card card)
        {
            cards.Remove(card);
        }

        public Deck clone()
        {
            List<Card> copyCards = new List<Card>();
            foreach (Card card in cards)
            {
                copyCards.Add(card);
            }
            return new Deck(copyCards);
        }
    }
}
