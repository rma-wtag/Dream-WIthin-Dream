using Dream_Within_Dream.Model;
using Dream_Within_Dream.Populator;

namespace Dream_Within_Dream;

public class Program {

    public static async Task Main(string[] args) {

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("*****WELCOME TO INCEPTION - DREAM WITHIN DREAM APP*****");
        Console.WriteLine();
        Console.ResetColor();

        Console.WriteLine("Populating Dreams....");
        Console.WriteLine("Populating Finished!");
        Console.WriteLine();
        DataPopulator populator = new DataPopulator();
        var generateDreams = Task.Run(() => populator.GenerateDreams(1000000));

        Console.WriteLine("1.Get all dreams");
        Console.WriteLine("2.Get dreams by ID");
        Console.Write("Enter which operation you want to perform : ");
        int.TryParse(Console.ReadLine(), out int queryInt);

        List<Dream> dreams = await generateDreams;
        //Console.WriteLine("Dream Population finished!");
        //Console.WriteLine("No. of dreams created : "+ dreams.Count); 

        switch (queryInt) {
            case 1:
                break;
            case 2:
                break;
            default:
                Console.WriteLine("Sorry, it is not a defined operation");
                break;
        }
    }

}