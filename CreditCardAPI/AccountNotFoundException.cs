using System;

namespace CreditCardAPI
{
    public class AccountNotFoundException : Exception
    {
        public string ResourceReferenceProperty { get; set; }

        public AccountNotFoundException()
        {
        }

        public AccountNotFoundException(string message)
            : base(message)
        {
        }

        public AccountNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }

    }
}