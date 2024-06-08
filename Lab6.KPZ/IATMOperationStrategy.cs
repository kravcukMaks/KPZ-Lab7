namespace ClassLibrary1
{
    public interface IATMOperationStrategy
    {
        void Execute(Account account, AutomatedTellerMachine atm, double amount);
    }

    public class WithdrawStrategy : IATMOperationStrategy
    {
        public void Execute(Account account, AutomatedTellerMachine atm, double amount)
        {
            if (amount <= atm.Money && amount <= account.Balance)
            {
                atm.WithdrawMoney(amount);
                account.Withdraw((int)amount);
            }
            else
            {
                Console.WriteLine("Недостатньо коштів у банкоматі або на рахунку.");
            }
        }
    }

    public class BalanceInquiryStrategy : IATMOperationStrategy
    {
        public void Execute(Account account, AutomatedTellerMachine atm, double amount)
        {
            Console.WriteLine($"Баланс: {account.Balance}");
        }
    }
}