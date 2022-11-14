#define DBG
namespace proj1
{
    class Program
    {
        static void Main(string[] args)
        {
            Player James = new Player(){ name = "James", };
            Player Joe = new Player()  { name = "Joe", };

            Board currBoard = new Board();
            currBoard.Register(James);
            currBoard.Register(Joe);
#if DBG
            ////initialise with a dead-mans hand
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
            foreach (Card x in currBoard.Deck){
                Console.WriteLine($"  {x},");
            }
            Console.WriteLine("\b}");
            //Console.WriteLine("hand := {");
            //foreach (Card x in James.hand)
            //{
            //    Console.WriteLine($"  {x},");
            //}
            //Console.WriteLine("\b}");
            while(Console.ReadKey().Key != ConsoleKey.Enter){
                Thread.Sleep(1);
            }
#endif
            Console.Clear();
            currBoard.EventLoop();
        }
    }
}
