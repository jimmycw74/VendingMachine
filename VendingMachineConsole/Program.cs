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

            //check all arguments
            foreach(string arg in args)
            {
                if (arg.ToLower().Equals("--help")) Console.WriteLine(vm.getHelpDisplay());
            }

            Console.WriteLine(vm.getInitialDisplay());
            while (!command.Equals("QUIT"))
            {
                Console.Write("> ");
                command = Console.ReadLine().Trim().ToUpper();
                Console.WriteLine(vm.processCommand(command));
            }
        }
    }
}

//console run:
//dotnet run --project .\VendingMachineConsole\ -- --help



