using Dream_Within_Dream.Actions;
using Dream_Within_Dream.Model;
using Dream_Within_Dream.Populator;
using Dream_Within_Dream.TicketBooking;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Dream_Within_Dream;

public class Program {

    public static async Task Main(string[] args) {

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("*****WELCOME TO INCEPTION - DREAM WITHIN DREAM*****");
        Console.WriteLine();
        Console.ResetColor();

        
        DataPopulator populator = new DataPopulator();
        var generateDreams = Task.Run(() => populator.GenerateDreams(1000000));
        DreamQueries dreamQueries = null;
        bool endProgram = false;

        while (true) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Here are the operations you can perform : \n");
            Console.ResetColor();

            Console.WriteLine("0.To exit from the program");
            Console.WriteLine("1.Get all dreams");
            Console.WriteLine("2.Get dreams by ID");
            Console.WriteLine("3.Book ticket by dream levels");
            Console.Write("Enter which operation you want to perform : ");
            int.TryParse(Console.ReadLine(), out int queryInt);

            if (dreamQueries == null)
            {
                List<Dream> dreams = await generateDreams;
                dreamQueries = new DreamQueries(dreams);
            }

            switch (queryInt)
            {
                case 0:
                    endProgram = true;
                    break;
                case 1:
                    handleGetAllData(dreamQueries);
                    break;
                case 2:
                    await handleGetDataById(dreamQueries);
                    break;
                case 3:
                    await handleTicketBookingByLevel(dreamQueries);
                    break;
                default:
                    Console.WriteLine("Sorry, it is not a defined operation");
                    break;
            }

            Console.WriteLine();
            Console.WriteLine();

            if (endProgram) {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Thank You for using our app!");
                Console.ResetColor();
                break;
            }
        }
    }

    private static async Task handleGetDataById(DreamQueries dreamQueries)
    {
        Console.Write("Enter Id : ");
        int id = int.Parse(Console.ReadLine());

        Dream? asyncResult = await dreamQueries.GetDataByIdAsync(id);


        if (asyncResult != null)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nFound Dream: Id={asyncResult.Id}, Level={asyncResult.Level}, Dreamer={asyncResult.Dreamer}");
            Console.WriteLine($"Description: {asyncResult.Description}");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n[Async] Dream not found!");
            Console.ResetColor();
        }
    }

    private static void handleGetAllData(DreamQueries dreamQueries)
    {
        
        var dreams = dreamQueries.DreamList;

        int pageSize = 10;
        int totalPages = (int)Math.Ceiling((double)dreams.Count / pageSize);
        int currentPage = 1;

        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Showing page {currentPage} of {totalPages} (Page size: {pageSize})");
            Console.WriteLine(new string('-', 30));

            var pageItems = dreams
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize);

            foreach (var dream in pageItems)
            {
                Console.WriteLine($"Id: {dream.Id}, Level: {dream.Level}, Dreamer: {dream.Dreamer}");
                Console.WriteLine($"Description: {dream.Description}");
                Console.WriteLine();
            }

            Console.WriteLine(new string('-', 30));
            Console.WriteLine("Commands: N = Next page | P = Previous page | Q = Quit");
            Console.Write("Press a key: ");

            var keyInfo = Console.ReadKey(true).KeyChar;

            if (keyInfo == 'n' && currentPage < totalPages)
            {
                currentPage++;
            }
            else if (keyInfo == 'p' && currentPage > 1)
            {
                currentPage--;
            }
            else if (keyInfo == 'q')
            {
                break;
            }
        }
    }
    private static async Task handleTicketBookingByLevel(DreamQueries dreamQueries) {
        var ticketService = new TicketService(100);
        var tickerBooker = new TicketBooker(dreamQueries.DreamList, ticketService);

        Console.Write("Enter for which Level you want to book ticket : ");
        int.TryParse(Console.ReadLine(), out int level);

        await tickerBooker.BookTicketsAtLevelAsync(level);
    }

}