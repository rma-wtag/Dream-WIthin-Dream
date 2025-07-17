using Dream_Within_Dream.Populator;

namespace Dream_Within_Dream;

public class Program {

    public static void Main(string[] args) {
        DataPopulator populator = new DataPopulator();
        var result = populator.GenerateDreams(10000000);

        Console.WriteLine(result.Count);
    }

}