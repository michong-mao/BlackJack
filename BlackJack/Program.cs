using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public enum CardNumber
    {
        Ace = 1,
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

    }
    public enum CardSuite
    {
        Club = 1,
        Spade,
        Diamond,
        Heart
    }

    public class Card
    {
        public CardNumber CardNumber { get; set; }
        //1-13 -> A-10,J,Q,K
        public CardSuite CardSuite { get; set; }
        public int CardValue { get; set; }
        public Card(CardSuite suite, CardNumber number)
        {
            CardSuite = suite;
            CardNumber = number;
            if ((int)CardNumber == 11 || (int)CardNumber == 12 || (int)CardNumber == 13)
            {
                CardValue = 10;
            }
            else if ((int)CardNumber == 1)
            {
                CardValue = 11;
            }
            else
            {
                CardValue = (int)CardNumber;
            }
        }

        public override string ToString()
        {
            return "[" + CardSuite + " " + CardNumber + "],";
        }

    }

    public class Deck
    {
        public List<Card> newDeck = new List<Card>();
        public void makeDeck()
        {
            for (int i = 1; i <= 4; i++)
            {
                for (int j = 1; j <= 13; j++)
                {
                    newDeck.Add(new Card((CardSuite)i, (CardNumber)j));
                }
            }
        }
    }

    public class Player
    {
        public int ChipCount { get; set; }
        public int WinCount { get; set; }
        public int LossCount { get; set; }
        public int TieCount { get; set; }
        public int TotalGamesPlayed { get; set; }
        public double WinRate { get; set; }
        public int cardSum { get; set; }
    }
    public class Dealer
    {
        public int cardSum { get; set; }
    }


    //main program
    class Program
    {
        static void Main(string[] args)
        {


            Boolean mainMenu = true;
            while (mainMenu == true)
            {
                Console.WriteLine("Welcome to BlackJack! \nPlease make a selection: \n1.Start Game \n2.Help \n3.Exit");
                int mainInput = Convert.ToInt32(Console.ReadLine());
                if (mainInput == 1)
                {
                    playGame();
                    
                }
                else if (mainInput == 2)
                {
                    Console.WriteLine("This is a no chip BlackJack. The Chip function will come in the future");
                    Console.WriteLine("Dealer Must Stay for 17 or higher. \nBlack Jack has 3:1 payout. Enjoy the Game!");
                }
                else
                {
                    mainMenu = false;
                    //Exit
                }
            }
        }

        public static void playGame()
        {
            Player player1 = new Player();
            player1.cardSum = 0;
            player1.TotalGamesPlayed = 0;
            player1.WinCount = 0;
            //players init
            Dealer dealer1 = new Dealer();
            dealer1.cardSum = 0;

            Random rng = new Random();
            Deck freshDeck = new Deck();
            freshDeck.makeDeck();
            //randomize the cards
            for (int i = 0; i < freshDeck.newDeck.Count; i++)
            {
                var temp = freshDeck.newDeck[i];
                int randomIndex = rng.Next(i, freshDeck.newDeck.Count);
                freshDeck.newDeck[i] = freshDeck.newDeck[randomIndex];
                freshDeck.newDeck[randomIndex] = temp;

            }
            Boolean subMenu = true;
            while (subMenu)
            {
                playerTurn();
                dealerTurn();
            }
           
            
            void dealerTurn()
            {
                if (dealer1.cardSum == 0)
                {
                    dealer1.cardSum = freshDeck.newDeck[0].CardValue + freshDeck.newDeck[1].CardValue;
                    freshDeck.newDeck.RemoveAt(0);
                    freshDeck.newDeck.RemoveAt(1);
                }
                else
                if (dealer1.cardSum == 21)
                {
                    Console.WriteLine("Dealer Total is 21, You Lost");
                }
                else if (dealer1.cardSum >= 17)
                {
                    if (player1.cardSum > dealer1.cardSum)
                    { Console.WriteLine("Dealer Total is " + dealer1.cardSum + " You Won!"); }
                    else if (player1.cardSum > dealer1.cardSum)
                    {
                        Console.WriteLine("Dealer Total is " + dealer1.cardSum + " Tie!");
                    }
                    else
                    {
                        //Recursive
                        dealer1.cardSum += freshDeck.newDeck[0].CardValue;
                        freshDeck.newDeck.RemoveAt(0);
                        dealerTurn();
                    }

                }
                
            }

            void playerTurn()
            {
                player1.cardSum = freshDeck.newDeck[0].CardValue + freshDeck.newDeck[1].CardValue;
                freshDeck.newDeck.RemoveAt(0);
                freshDeck.newDeck.RemoveAt(1);
                if (player1.cardSum == 21)
                {
                    Console.WriteLine("Your Total is 21, You Won");
                }
                else
                {
                    Console.WriteLine("You card sum is: " + player1.cardSum);
                    Boolean keepGoing = true;
                    while (keepGoing)
                    {
                        Console.WriteLine("Do you want to: \n1.hit. \n2.Stay(under construction)");
                        string respond = Console.ReadLine();
                        if (respond.Equals("1"))
                        {
                            player1.cardSum += freshDeck.newDeck[0].CardValue;
                            freshDeck.newDeck.RemoveAt(0);
                            if (player1.cardSum == 21) { Console.WriteLine("You got 21. You Won!"); keepGoing = false; }
                            else if (player1.cardSum > 21) { Console.WriteLine("You got " + player1.cardSum + " Busted, you have lost!"); keepGoing = false; }
                            else
                            {
                                Console.WriteLine("You card sum is: " + player1.cardSum);
                            }
                        }
                        else if (respond.Equals("2"))
                        {
                            keepGoing = false;
                            
                        }
                        
                    }
                    
                }
                
            }

        }
    }
}
