namespace ClassLibrary1
{
    public abstract class AccountFactory
    {
        public abstract Account CreateAccount(long card_number, int pin_code, string name, string surname, double balance, string email, string password);
    }

    public class StandardAccountFactory : AccountFactory
    {
        public override Account CreateAccount(long card_number, int pin_code, string name, string surname, double balance, string email, string password)
        {
            return new Account(card_number, pin_code, name, surname, balance, email, password);
        }
    }
}