using System.Windows;
using PRAM_Machine.Machine;

namespace PRAM_Machine.Gui
{
    public static class SimulatorGui
    {
        public static void Run(IPRAMMachine machine)
        {
            var application = new Application();
            application.Run(new MainWindow(machine));
        }
    }
}