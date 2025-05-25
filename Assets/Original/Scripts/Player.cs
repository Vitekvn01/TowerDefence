using IInterfaces;

public class Player
{
    public IWallet Wallet { get; private set;}

    public Player(Wallet wallet)
    {
        Wallet = wallet;
    }
}
