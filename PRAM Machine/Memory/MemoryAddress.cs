namespace PRAM_Machine.Memory
{
    /// <summary>
    ///     Class representing address in PRAM memory.
    ///     Address contains name of memory row and cell index in this row.
    /// </summary>
    public class MemoryAddress
    {
        #region ClassMembers

        //Name of memory row
        //Index in this row
        //Flag indicating empty request (addresses with this flag set won't be processed)

        #endregion

        #region Constructors

        /// <summary>
        ///     This builds empty memory address.
        ///     Property empty is set to true (address won't be processed unless you change it to false).
        /// </summary>
        public MemoryAddress()
        {
            Empty = true;
        }

        /// <summary>
        ///     This builds non empty memory address (this will be processed).
        /// </summary>
        /// <param name="memoryName">Name of memory row</param>
        /// <param name="address">Index in this row</param>
        public MemoryAddress(string memoryName, int address)
        {
            MemoryName = memoryName;
            Address = address;
            Empty = false;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Index in memory row
        /// </summary>
        public int Address { get; set; }

        /// <summary>
        ///     Name of memory row
        /// </summary>
        public string MemoryName { get; set; }

        /// <summary>
        ///     If address is empty or not
        /// </summary>
        public bool Empty { get; set; }

        #endregion
    }
}