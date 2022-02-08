using VendingMachine;

namespace VendingMachineConsole
{
    class Program
    {
        static void Main(String[] args)
        {
            Console.WriteLine("VendingMachine Console Interface - WELCOME");

            VendingMachineFacade vm = new();
            string command = "";

            while (!command.Equals("QUIT"))
            {
                Console.Write("> ");
                command = Console.ReadLine().Trim().ToUpper();
                Console.WriteLine(vm.processCommand(command));
            }
        }
    }
}




