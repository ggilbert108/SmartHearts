using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHearts
{
    class Card
    {
        public readonly Suit suit;
        public readonly int rank;

        public Card(Suit suit, int rank)
        {
            this.suit = suit;
            this.rank = rank;
        }

        public override bool Equals(object obj)
        {
            Card other = (Card)obj;
            return suit == other.suit && rank == other.rank;
        }

        public override int GetHashCode()
        {
            return ((int)suit) * 57 ^ rank;
        }

        public override string ToString()
        {
            return rank + " of " + suit.ToString();
        }
    }

    enum Suit
    {
        Clubs, Hearts, Spades, Diamonds, None
    }
}
