using System;
using NoCommons.Common;

namespace NoCommons.Banking
{
    /// <summary>
    /// Validates <see cref="Kidnummer"/>.
    /// </summary>
    public class KidnummerValidator : StringNumberValidator {

        public const string LenghtErrorMessage = "A valid kidnummer is between 2 and 25 digits";

        /// <summary>
        /// Return true if the provided String is a valid KID-nummmer.
        /// </summary>
        /// <param name="kidnummer">String representing a Kidnummer</param>
        /// <returns>True if valid</returns>
        public static bool IsValid(String kidnummer) {
            try {
                GetKidnummer(kidnummer);
                return true;
            } catch (ArgumentException) {
                return false;
            }
        }

        /**
	     * Returns an object that represents a Kidnummer.
	     * 
	     * @param kidnummer
	     *            A String containing a Kidnummer
	     * @return A Kidnummer instance
	     * @throws IllegalArgumentException
	     *             thrown if String contains an invalid Kidnummer
	     */
        public static Kidnummer GetKidnummer(string kidnummer) {
            ValidateSyntax(kidnummer);
            ValidateChecksum(kidnummer);
            return new Kidnummer(kidnummer);
        }

        internal static void ValidateSyntax(string kidnummer) {
            ValidateAllDigits(kidnummer);
            ValidateLengthInRange(kidnummer, 2, 25);
        }

        private static void ValidateLengthInRange(string kidnummer, int i, int j) {
            if (kidnummer == null || kidnummer.Length < i || kidnummer.Length > j) {
                throw new ArgumentException(LenghtErrorMessage);
            }
        }

        internal static void ValidateChecksum(String kidnummer) {
            StringNumber k = new Kidnummer(kidnummer);
            int kMod10 = CalculateMod10CheckSum(GetMod10Weights(k), k);
            int kMod11 = CalculateMod11CheckSum(GetMod11Weights(k), k);
            if (kMod10 != k.GetChecksumDigit() && kMod11 != k.GetChecksumDigit()) {
                throw new ArgumentException(InvalidChecksumErrorMessage + kidnummer);
            }
        }

    }
}