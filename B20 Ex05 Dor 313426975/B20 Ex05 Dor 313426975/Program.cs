namespace MemoryLogic
{
    public class Program
    {
        public static void Main()
        {
            SystemManager gameTime = new SystemManager();

            gameTime.PlayMatchGame();

            // wait for enter
            System.Console.WriteLine("Please press 'Enter' to exit...");
            System.Console.ReadLine();
        }
    }
}
