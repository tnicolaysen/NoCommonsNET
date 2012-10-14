using System;
using System.Linq;

namespace NoCommons.Common
{
    public abstract class StringNumberValidator
    {
        internal const string InvalidChecksumErrorMessage = "Invalid checksum : ";
        internal const string SyntaxErrorMessage = "Only digits are allowed : ";

        private static readonly int[] BaseMod11Weights = new[] {2, 3, 4, 5, 6, 7};

        protected static int CalculateMod11CheckSum(int[] weights, StringNumber number)
        {
            int checksum = CalculateChecksum(weights, number, false)%11;
            if (checksum == 1) 
                throw new ArgumentException(InvalidChecksumErrorMessage + number);

            if (checksum == 0) 
                return 0;
            else 
                return 11 - checksum;
        }

        protected static int CalculateMod10CheckSum(int[] weights, StringNumber number)
        {
            int checksum = CalculateChecksum(weights, number, true)%10;
            
            if (checksum == 0) 
                return 0;
            else 
                return 10 - checksum;
        }

        private static int CalculateChecksum(int[] weights, StringNumber number, bool tverrsum)
        {
            int checksum = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                int weight = weights[i];
                int currentNumber = number.GetAt(weights.Length - 1 - i);
                int product = weight*currentNumber;

                if (tverrsum)
                {
                    if (product > 9) checksum += product - 9;
                    else checksum += product;
                }
                else 
                {
                    checksum += product;
                }
            }
            return checksum;
        }

        protected static void ValidateLengthAndAllDigits(string numberString, int length)
        {
            if (numberString == null || numberString.Length != length)
                throw new ArgumentException(SyntaxErrorMessage + numberString);

            ValidateAllDigits(numberString);
        }

        protected static void ValidateAllDigits(string numberString)
        {
            if (string.IsNullOrEmpty(numberString))
                throw new ArgumentException(SyntaxErrorMessage + numberString);

            if (numberString.Any(NotADigit))
                throw new ArgumentException(SyntaxErrorMessage + numberString);
        }

        private static bool NotADigit(char c)
        {
            return !char.IsDigit((c));
        }

        protected static int[] GetMod10Weights(StringNumber k)
        {
            var weights = new int[k.GetLength() - 1];
            for (int i = 0; i < weights.Length; i++)
            {
                if ((i%2) == 0) 
                    weights[i] = 2;
                else 
                    weights[i] = 1;
            }
            return weights;
        }

        protected static int[] GetMod11Weights(StringNumber k)
        {
            var weights = new int[k.GetLength() - 1];
            for (int i = 0; i < weights.Length; i++)
            {
                int j = i%BaseMod11Weights.Length;
                weights[i] = BaseMod11Weights[j];
            }
            return weights;
        }
    }
}