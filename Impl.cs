namespace proj1
{
    enum Suit
    {
        Hearts,
        Clubs,
        Diamonds,
        Spades
    }
    public readonly struct CardType
    {
        public CardType(string nam, int va)
        {
            name = nam;
            val = va;
        }
        public string name { get; init; }
        public int val { get; init; }
    }
    class CardValue
    {
        public const int SuitLength = 13;
        public static bool CloseTo21 = false;
        public static CardType getAce()
        {
            if (CloseTo21)
            {
                return Ace1;
            }
            else { return Ace11; }
        }
        public static CardType Ace = getAce();
        public static CardType Ace11 = new CardType("Ace", 11);
        public static CardType Ace1 = new CardType("Ace", 1);
        public static CardType Two = new CardType("Two", 2);
        public static CardType Three = new CardType("Three", 3);
        public static CardType Four = new CardType("Four", 4);
        public static CardType Five = new CardType("Five", 5);
        public static CardType Six = new CardType("Six", 6);
        public static CardType Seven = new CardType("Seven", 7);
        public static CardType Eight = new CardType("Eight", 8);
        public static CardType Nine = new CardType("Nine", 9);
        public static CardType Ten = new CardType("Ten", 10);
        public static CardType King = new CardType("King", 10);
        public static CardType Queen = new CardType("Queen", 10);
        public static CardType Jack = new CardType("Jack", 10);
        public CardType[] Cards = new CardType[SuitLength]
        {
            Ace,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King
        };
        public CardType getCardFromNum(int num) => Cards[num];
    }
    class Card : IComparable<Card>
    {
        public Card() { }
        public bool CloseTo21 = false;
        public Suit CardSuit;
        public CardType CardVal;
        //NOTE: { get; } only, we do not want users/devs to be setting the
        //value of a card
        public int CompareTo(Card card)
        {
            return (CardVal.val).CompareTo(card.CardVal.val);
        }
        public override string ToString() =>
            $"{CardVal.name} of {CardSuit}, which is valued at {CardVal.val}";
    }
    class Player
    {
        public List<Card> hand = new List<Card>();
        public Card shown = hand[0];
        public string? name;
        public int score()
        {
            return hand.Aggregate(0, (acc, x) => acc + x.CardVal.val);
        }
        public Player() { }
        void Hit() { }
        void Stand() { }
    }
    class Board
    {
        public List<Card> Deck = new List<Card>();
        public Board()
        {
            //initialise deck
            Suit[] _suit = new Suit[4] {
                Suit.Hearts, Suit.Diamonds, Suit.Clubs, Suit.Spades
            };
            int _suitI = 0;
            CardType[] _Cards = new CardValue().Cards;
            for (int i = 0; i < (CardValue.SuitLength * 4); i++)
            {
                Card c = new Card();
                c.CardVal = _Cards[(i % CardValue.SuitLength)];
                //set the card to be the modulo of the index and the number within
                //a suit
                if (((i % CardValue.SuitLength) == 0) && (i > 1)) { _suitI++; }
                //if it is at the start of the suit and greater than 1 then go
                //to the next suit for the index
                c.CardSuit = _suit[_suitI];
                Deck.Add(c);
            }
        }
        public List<Player> players = new List<Player>();
        public List<Player> hittingPlayers = new List<Player>();
        public void InitDeal()
        {
            var rnd = new Random();
            foreach (Player p in players)
            {
                int num;
                num = rnd.Next(0, Deck.Count);
                p.hand.Add(Deck[num]);
                Deck.RemoveAt(num);

                num = rnd.Next(0, Deck.Count);
                p.hand.Add(Deck[num]);
                Deck.RemoveAt(num);
            }
        }
        public void Deal()
        {
            var rnd = new Random();
            foreach (Player p in players)
            {
                int num;
                num = rnd.Next(0, Deck.Count);
                p.hand.Add(Deck[num]);
                Deck.RemoveAt(num);
            }
        }
    }
}
