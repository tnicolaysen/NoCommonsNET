using NoCommons.Common;

namespace NoCommons.Banking
{
    /// <summary>
    /// This class represent a Norwegian KID-nummer - a number used to identify 
    /// a customer on invoices. A Kidnummer consists of digits only, and the last
    /// digit is a checksum digit (either mod10 or mod11).
    /// </summary>
    public class Kidnummer : StringNumber
    {
        /// <summary>
        /// <p>
        /// This constructor is meant to be used by <see cref="KidnummerValidator"/>.
        /// The reason for this is that a user of this lib should not be allowed
        /// to create an instance of this class that is invalid.
        /// </p>
        /// 
        /// <p>
        /// To create a new instance of this class, use <see cref="KidnummerValidator.GetKidnummer" />
        /// </p>
        /// </summary>
        /// <param name="kontonummer">A valid KID-nummer</param>
        protected internal Kidnummer(string kontonummer) : base(kontonummer)
        {
        }
    }
}