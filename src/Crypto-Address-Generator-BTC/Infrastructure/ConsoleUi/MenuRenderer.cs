namespace CryptoAddressGeneratorBTC.Infrastructure.ConsoleUi
{
    public class MenuRenderer
    {
        public void RenderHeader(string title)
        {
            System.Console.WriteLine();
            System.Console.WriteLine("  ╔══════════════════════════════════════════════════════════╗");
            System.Console.WriteLine($"  ║              {title,-46}║");
            System.Console.WriteLine("  ╚══════════════════════════════════════════════════════════╝");
            System.Console.WriteLine();
        }

        public void RenderMenu(string[] options)
        {
            System.Console.WriteLine("Select an option:");
            for (int i = 0; i < options.Length; i++)
            {
                System.Console.WriteLine($"  {i + 1}. {options[i]}");
            }
            System.Console.Write("> ");
        }
    }
}
