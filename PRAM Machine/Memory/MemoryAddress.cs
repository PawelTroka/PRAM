using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PRAM_Machine.Memory {
    /// <summary>
    /// Class representing address in PRAM memory.
    /// Address contains name of memory row and cell index in this row.
    /// </summary>
    public class MemoryAddress {
        #region ClassMembers
        //Name of memory row
        private string memoryName;
        //Index in this row
        private int address;
        //Flag indicating empty request (addresses with this flag set won't be processed)
        private bool empty;
        #endregion

        #region Constructors
        /// <summary>
        /// This builds empty memory address.
        /// Property empty is set to true (address won't be processed unless you change it to false).
        /// </summary>
        public MemoryAddress() {
            this.empty = true;
        }

        /// <summary>
        /// This builds non empty memory address (this will be processed).
        /// </summary>
        /// <param name="memoryName">Name of memory row</param>
        /// <param name="address">Index in this row</param>
        public MemoryAddress(string memoryName, int address) {
            this.MemoryName = memoryName;
            this.Address = address;
            this.empty = false;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Index in memory row
        /// </summary>
        public int Address {
            get { return address; }
            set { address = value; }
        }

        /// <summary>
        /// Name of memory row
        /// </summary>
        public string MemoryName {
            get { return memoryName; }
            set { memoryName = value; }
        }

        /// <summary>
        /// If address is empty or not
        /// </summary>
        public bool Empty {
            get { return empty; }
            set { empty = value; }
        }
        #endregion
    }
}
