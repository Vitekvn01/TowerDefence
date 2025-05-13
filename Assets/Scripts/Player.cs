using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public Wallet Wallet { get; }
    public Score Score { get; }

    public Player(Wallet wallet, Score score)
    {
        Wallet = wallet;
        Score = score;
    }
}
