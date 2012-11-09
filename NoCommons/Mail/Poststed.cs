using System;

namespace NoCommons.Mail
{
    public class Poststed
    {
        private readonly string poststedstring;

        public Poststed(string poststedstring)
        {
            if (poststedstring == null)
            {
                throw new ArgumentException();
            }
            this.poststedstring = poststedstring.ToUpper().Trim();
        }

        public string getValue()
        {
            return poststedstring;
        }


        public override string ToString()
        {
            return getValue();
        }

        public override int GetHashCode()
        {
            const int prime = 31;
            var result = 1;
            result = prime*result + ((poststedstring == null) ? 0 : poststedstring.GetHashCode());
            return result;
        }

        public override bool Equals(Object obj)
        {
            if (this == obj)
            {
                return true;
            }
            if (obj == null)
            {
                return false;
            }
            if (GetType() != obj.GetType())
            {
                return false;
            }
            var other = (Poststed) obj;
            if (poststedstring == null)
            {
                if (other.poststedstring != null)
                {
                    return false;
                }
            }
            else if (!poststedstring.Equals(other.poststedstring))
            {
                return false;
            }
            return true;
        }
    }
}