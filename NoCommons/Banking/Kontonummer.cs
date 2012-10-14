using System.Text;
using NoCommons.Common;

namespace NoCommons.Banking
{
    public class Kontonummer : StringNumber
    {
        public Kontonummer(string kontonummer) : base(kontonummer)
        {
        }

        /// <summary>
        /// Gets the bank's registration number from the kontonummer.
        /// </summary>
        /// <returns>
        /// 'xxxx' if the kontonummer is 'xxxx.yy.zzzzc'
        /// </returns>
        public string GetRegisternummer()
        {
            return GetValue().Substring(0, 4);
        }

        /// <summary>
        /// Gets the account group from the kontonummer.
        /// </summary>
        /// <returns>
        /// 'yy' if the kontonummer is 'xxxx.yy.zzzzc'
        /// </returns>
        public string GetKontogruppe()
        {
            return GetValue().Substring(4, 2);
        }

        /// <summary>
        /// Gets the customer account number from the kontonummer.
        /// </summary>
        /// <returns>
        /// 'zzzzc' if the kontonummer is 'xxxx.yy.zzzzc'
        /// </returns>
        public string GetKundenummer()
        {
            return GetValue().Substring(6, 5);
        }

        /// <summary>
        /// Gets the control digit from the kontonummer.
        /// </summary>
        /// <returns>
        /// 'c' if the kontonummer is 'xxxx.yy.zzzzc'
        /// </returns>
        public string GetKontrollsiffer()
        {
            return GetValue().Substring(10, 1);
        }

        public string GetGroupedValue()
        {
            return string.Format("{0}.{1}.{2}", GetRegisternummer(), GetKontogruppe(), GetKundenummer());
        }
    }
}