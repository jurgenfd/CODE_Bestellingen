using Bestellingen.PaymentStrategy;
using System;

namespace Bestellingen.PaymentStrategy
{
    public interface IPaymentStrategy
    {
        void Pay(double amount);
    }
}