#define DBG
namespace proj1
{
    class Program
    {
        static void Main(string[] args)
        {
            Player James = new Player()
            {
                //initialise with a dead-mans hand
                name = "James",
            };

            Board currBoard = new Board();
            currBoard.players.Add(James);

#if DBG
            //James.hand.Add((new Card()
            //{
            //    CardSuit = Suit.Clubs,
            //    CardVal = CardValue.Ace,
            //}));
            //James.hand.Add((new Card()
            //{
            //    CardSuit = Suit.Spades,
            //    CardVal = CardValue.Ace
            //}));
            //James.hand.Add((new Card()
            //{
            //    CardSuit = Suit.Clubs,
            //    CardVal = CardValue.Eight
            //}));
            //James.hand.Add((new Card()
            //{
            //    CardSuit = Suit.Spades,
            //    CardVal = CardValue.Eight
            //}));
            //Console.WriteLine("{0}", (new Card()
            //{
            //    CardSuit = Suit.Clubs,
            //    CardVal = CardValue.Ace
            //    //try to find a way to cascade this or at least make it easier
            //    //to work with
            //}));
            //Console.WriteLine(James.score());
            Console.WriteLine("deck := {");
            foreach (Card x in currBoard.Deck)
            {
                Console.WriteLine($"  {x},");
            }
            Console.WriteLine("\b}");
            currBoard.InitDeal();
            Console.WriteLine("hand := {");
            foreach (Card x in James.hand)
            {
                Console.WriteLine($"  {x},");
            }
            Console.WriteLine("\b}");
#endif
        }
    }
}
