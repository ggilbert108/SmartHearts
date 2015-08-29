using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SmartHearts
{
    class Player
    {
        public int index;
        public List<Card> hand;
        public Deck unknownCards;
        public int score;

        public Player(int index)
        {
            this.index = index;
            hand = new List<Card>();
            unknownCards = new Deck();
            score = 0;
        }

        public Player(int index, List<Card> hand, Deck unknownCards, int score)
        {
            this.index = index;
            this.hand = hand;
            this.unknownCards = unknownCards;
            this.score = score;
        }

        public Card playCardSmart(Suit leadSuit, GameState state)
        {
            List<Card> legal = getLegalMoves(leadSuit);

            Card played = AI.getBestMove(legal, state, index);
            Console.WriteLine(hand.Remove(played));
            return played;
        }

        public Card playCardRandom(Suit leadSuit)
        {
            List<Card> legal = getLegalMoves(leadSuit);
            int playIndex = Util.random.Next(legal.Count);
            Card played = legal[playIndex];

            hand.Remove(played);
            return played;
        }

        private List<Card> getLegalMoves(Suit leadSuit)
        {
            if (!hasSuit(leadSuit) || leadSuit == Suit.None) return hand;

            List<Card> legal = new List<Card>();
            foreach (Card card in hand)
            {
                if (card.suit == leadSuit)
                {
                    legal.Add(card);
                }
            }
            return legal;
        }

        public void giveCard(Card card)
        {
            hand.Add(card);
            seeCard(card);
        }

        public void removeCard(Card card)
        {
            hand.Remove(card);
        }

        public void seeCard(Card card)
        {
            unknownCards.removeCard(card);
        }

        public bool hasCard(Card compare)
        {
            foreach (Card card in hand)
            {
                if (card.Equals(compare))
                    return true;
            }
            return false;
        }

        public Player clone()
        {
            List<Card> cardsCopy = new List<Card>();
            foreach (Card card in hand)
            {
                cardsCopy.Add(card);
            }
            Deck deckCopy = unknownCards.clone();

            return new Player(index, cardsCopy, deckCopy, score);
        }

        private bool hasSuit(Suit suit)
        {
            foreach (Card card in hand)
            {
                if (card.suit == suit)
                    return true;
            }
            return false;
        }
    }
}
