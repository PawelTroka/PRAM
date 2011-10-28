using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PRAM_Machine.Machine;
using PRAM_Machine.Memory;

namespace PRAM_Machine.Models {
    /// <summary>
    /// Model of the PRAM. This model contains named rows of variables
    /// </summary>
    public class PRAMModel : Dictionary<string, List<dynamic>> {
    }
}
