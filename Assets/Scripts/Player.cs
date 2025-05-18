using IInterfaces;

public class Player
{
    public IWallet Wallet { get; private set;}
    public IScore Score { get; private set;}

    public Player(Wallet wallet, Score score)
    {
        Wallet = wallet;
        Score = score;
    }
}
