using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuotesApp.Contracts;
using QuotesApp.Models.Favqs.RequestModelS;

namespace QuotesApp
{
    class QuatesApp
    {
        private readonly IFavqsClient _favqsClient;
        public QuatesApp(IFavqsClient favqsClient)
        {
            _favqsClient = favqsClient;
        }

        public async Task StartAsync()
        {
            Console.WriteLine("--------------------------------");
            Console.WriteLine("|WELCOME TO FAV QUOTES|");
            Console.WriteLine("--------------------------------");

            while (true)
            {
                Console.Write("\nAvailable commands: \n" +
                    "1 - Login; \n" +
                    "2 - Exit. \n\n");

                Console.Write("Your choise: ");
                var command = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();

                switch (command)
                {
                    case 1:
                        Console.Write("Enter username or email: ");
                        var usernameSignup = Console.ReadLine();

                        Console.Write("Enter password: ");
                        var passwordSignup = Console.ReadLine();

                        var user = await _favqsClient.CreateSession(new LoginRequestModel
                        {
                            User = new User
                            {
                                Login = usernameSignup,
                                Password = passwordSignup
                            }
                        });

                        var login = false;

                        if (user.UserToken != null)
                        {
                            login = true;
                            Console.WriteLine("\nLogin is successful!\n");
                        }

                        if (user.UserToken == null)
                        {
                            Console.WriteLine("\nInvalid username or password!");
                        }

                        int quoteId;

                        while (login)
                        {
                            Console.WriteLine("Available commands:");
                            Console.WriteLine("1 - Show all quotes");
                            Console.WriteLine("2 - Get quote by Id");
                            Console.WriteLine("3 - Add quote");
                            Console.WriteLine("4 - Fav quote");
                            Console.WriteLine("5 - Exit");

                            var chosenCommand = Console.ReadLine();

                            switch (chosenCommand)
                            {
                                case "1":
                                    var quotesDefault = await _favqsClient.GetAllQuotes(1);

                                    Console.WriteLine(quotesDefault);

                                    foreach (var quoteResponseModel in quotesDefault.Quotes)
                                    {
                                        Console.WriteLine(quoteResponseModel);
                                    }

                                    Console.WriteLine("Enter page number to see: ");

                                    var page = Convert.ToInt32(Console.ReadLine());

                                    var quotesByUser = await _favqsClient.GetAllQuotes(page);

                                    Console.WriteLine(quotesByUser);

                                    foreach (var quoteResponseModel in quotesByUser.Quotes)
                                    {
                                        Console.WriteLine(quoteResponseModel);
                                    }

                                    break;
                                case "2":
                                    Console.WriteLine("\nEnter quote ID you want to see: ");

                                    quoteId = Convert.ToInt32(Console.ReadLine());

                                    var quote = await _favqsClient.GetQuote(quoteId);

                                    Console.WriteLine(quote);

                                    break;
                                case "3":
                                    Console.WriteLine("\nEnter author's name: ");
                                    var authorName = Console.ReadLine();

                                    Console.WriteLine("\nEnter author's quote: ");
                                    var quoteBody = Console.ReadLine();

                                    var quoteAdd = await _favqsClient.AddQuote(new QuoteRequestModel
                                    {
                                        Quote = new QuateShort
                                        {
                                            Author = authorName,
                                            Body = quoteBody
                                        }
                                    }, user.UserToken);

                                    Console.WriteLine(quoteAdd);

                                    break;
                                case "4":
                                    Console.WriteLine("\nEnter quote ID you want to fav: ");

                                    quoteId = Convert.ToInt32(Console.ReadLine());

                                    var quoteFav = await _favqsClient.FavQuote(quoteId, user.UserToken);

                                    Console.WriteLine(quoteFav);

                                    break;
                                case "5":
                                    var sessionDestroyed = await _favqsClient.DestroySession(user.UserToken);
                                    Console.WriteLine(sessionDestroyed);

                                    login = false;
                                    
                                    break;
                                default:
                                    Console.WriteLine("Bad command! \n");
                                    break;
                            }
                        }

                        break;
                    case 2:
                        return;
                    default:
                        Console.WriteLine("Bad command! \n");
                        break;
                }
            }
        }
    }
}
