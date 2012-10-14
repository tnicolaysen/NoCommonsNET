using System;
using NoCommons.Common;

namespace NoCommons.Banking
{
    public class KontonummerValidator : StringNumberValidator
    {
        private const int KontoNrLength = 11;
        internal const int KontogruppeNumDigits = 2;
        internal const int RegisternummerNumDigits = 4;

        public static bool IsValid(string kontonummer)
        {
            try
            {
                GetKontonummer(kontonummer);
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        public static Kontonummer GetKontonummer(string kontonummer)
        {
            ValidateSyntax(kontonummer);
            ValidateChecksum(kontonummer);
            return new Kontonummer(kontonummer);
        }

        public static Kontonummer GetAndForceValidKontonummer(string kontonummer)
        {
            ValidateSyntax(kontonummer);
            try
            {
                ValidateChecksum(kontonummer);
            }
            catch (ArgumentException)
            {
                var k = new Kontonummer(kontonummer);
                int checksum = CalculateMod11CheckSum(GetMod11Weights(k), k);
                kontonummer = kontonummer.Substring(0, KontoNrLength - 1) + checksum;
            }
            return new Kontonummer(kontonummer);
        }

        internal static void ValidateSyntax(string kontonummer)
        {
            ValidateLengthAndAllDigits(kontonummer, KontoNrLength);
        }

        internal static void ValidateAccountTypeSyntax(string kontogruppe)
        {
            ValidateLengthAndAllDigits(kontogruppe, KontogruppeNumDigits);
        }

        internal static void ValidateRegisternummerSyntax(string registernummer)
        {
            ValidateLengthAndAllDigits(registernummer, RegisternummerNumDigits);
        }

        internal static void ValidateChecksum(string kontonummer)
        {
            var kontonummerInstance = new Kontonummer(kontonummer);
            
            int k1 = CalculateMod11CheckSum(GetMod11Weights(kontonummerInstance), kontonummerInstance);
            if (k1 != kontonummerInstance.GetChecksumDigit()) 
                throw new ArgumentException(InvalidChecksumErrorMessage + kontonummer);
        }
    }
}