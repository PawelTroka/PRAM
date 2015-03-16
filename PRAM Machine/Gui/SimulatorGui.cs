using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PRAM_Machine.Machine;
using System.Windows;

namespace PRAM_Machine.Gui {
    public static class SimulatorGui {
        public static void Run(IPRAMMachine machine) {
            Application application = new Application();
            application.Run(new MainWindow(machine));
        }
    }
}
