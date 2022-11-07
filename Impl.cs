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
        public static CardType Ace
        {
            get
            {
                if (CloseTo21) { return Ace1; } else { return Ace11; };
            }
        }
        public static readonly CardType Ace11 = new CardType("Ace", 11);
        public static readonly CardType Ace1 = new CardType("Ace", 1);
        public static readonly CardType Two = new CardType("Two", 2);
        public static readonly CardType Three = new CardType("Three", 3);
        public static readonly CardType Four = new CardType("Four", 4);
        public static readonly CardType Five = new CardType("Five", 5);
        public static readonly CardType Six = new CardType("Six", 6);
        public static readonly CardType Seven = new CardType("Seven", 7);
        public static readonly CardType Eight = new CardType("Eight", 8);
        public static readonly CardType Nine = new CardType("Nine", 9);
        public static readonly CardType Ten = new CardType("Ten", 10);
        public static readonly CardType King = new CardType("King", 10);
        public static readonly CardType Queen = new CardType("Queen", 10);
        public static readonly CardType Jack = new CardType("Jack", 10);
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
            if (card == null) { return 0; }
            return (CardVal.val).CompareTo(card.CardVal.val);
        }
        public override string ToString() =>
            $"{CardVal.name} of {CardSuit}, which is valued at {CardVal.val}";
    }
    class Player
    {
        public List<Card> hand = new List<Card>();
        public Board? assignedBoard;
        public Card shown() => hand[0];
        public string? name;
        public int score() => hand.Aggregate(0, (acc, x) => acc + x.CardVal.val);
        public Player() { }
        //we can get away with this as after every loop over the players we merely
        //execute Deal then
        public void Hit()
        {
            (assignedBoard.hittingPlayers).Add(this);
        }
        public void Stand()
        {
            (assignedBoard.hittingPlayers).Remove(this);
        }
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
                if ((i > 1) &&
                    ((i % CardValue.SuitLength) == 0))
                {
                    _suitI++;
                }
                //if it is at the start of the suit and greater than 1 then go
                //to the next suit for the index
                c.CardSuit = _suit[_suitI];
                Deck.Add(c);
            }
        }
        public List<Player> players = new List<Player>();
        public List<Player> hittingPlayers = new List<Player>();
        public void InitDealAll()
        {
            foreach (Player p in players)
            {
                //handle improperly registered players
                if (p.assignedBoard == null) { p.assignedBoard = this; }
                if ((p.assignedBoard != null) && (p.hand.Count == 0))
                    InitDeal(p);
            }
        }
        public void InitDeal(Player p)
        {
            var rnd = new Random();
            int num;
            num = rnd.Next(0, Deck.Count);
            p.hand.Add(Deck[num]);
            Deck.RemoveAt(num);

            num = rnd.Next(0, Deck.Count);
            p.hand.Add(Deck[num]);
            Deck.RemoveAt(num);
        }
        public void Deal()
        {
            var rnd = new Random();
            foreach (Player p in hittingPlayers)
            {
                int num;
                num = rnd.Next(0, Deck.Count);
                p.hand.Add(Deck[num]);
                Deck.RemoveAt(num);
            }
        }
        public void Register(Player p)
        {
            players.Add(p);
            p.assignedBoard = this;
            InitDeal(p);
        }
        public void EventLoop()
        {
            InitDealAll(); // catch all players who are added directly to respective lists
            bool gameActive = true;
            while (gameActive)
            {
                foreach (Player p in players)
                {
                    hittingPlayers.Clear();
                //remove hanging state otherwise we will double deal by accident
                prompt:
                    Console.WriteLine($"shown := {{{p.shown()}}} and {p.hand.Count - 1} not shown");
                    Console.WriteLine($"score:= {p.score()}");
                    Console.WriteLine("Do you hit or stand?");
                switchS:
                    Console.Write($"{p.name}> ");
                    switch (Console.ReadLine().Trim().ToLower().Substring(0, 1))
                    {
                        case "h": p.Hit(); break;
                        case "s": p.Stand(); break;
                        case "p":
                            Console.WriteLine("hand:={");
                            foreach (Card c in p.hand)
                            {
                                Console.WriteLine($"  {c},");
                            }
                            Console.WriteLine("}");
                            goto switchS;
                        case "q":
                        case "e":
                            Environment.Exit(0);
                            break;
                        //why does this need a break its a literal program abort??
                        default:
                            Console.WriteLine("command not understood.");
                            goto prompt;
                    }
                    Console.WriteLine("Dealer Plays");
                    Deal();
                }
            }
        }
    }
}
