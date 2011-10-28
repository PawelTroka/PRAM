using System;
using PRAM_Machine.Gui;
using System.Windows;

namespace PRAM_Machine.Samples {
    class UsageSamples {
        [STAThread]
        static void Main() {
            //SimulatorGui.Run(FastAddition.FastAdditionSetup.Setup(10));
            //SimulatorGui.Run(VectorAddition.VectorAdditionSetup.Setup(15));
            SimulatorGui.Run(FastAdditionDivideAndConquer.FastAdditionDivideAndConquerSetup.Setup(20));
            //SimulatorGui.Run(LogicalAnd.LogicalAndSetup.Setup(5));
            //SimulatorGui.Run(MatrixMultiplication.MatrixMultiplicationSetup.Setup(3));
            //SimulatorGui.Run(Sorting.SortingSetup.Setup(4));
            //SimulatorGui.Run(ListRanking.ListRankingSetup.Setup(10));
        }
    }
}
