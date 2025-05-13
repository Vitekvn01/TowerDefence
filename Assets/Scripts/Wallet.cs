using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Wallet
{
    private int _money;
    public int Money
    {
        get => _money;
        private set
        {
            _money = value;
            OnChangeCountEvent?.Invoke(_money);
        }
        
    }

    public event Action<int> OnChangeCountEvent;

    public Wallet(int startMoney = 0)
    {
        _money = startMoney;
    }

    public void AddMoney(int count)
    {
        Money += count;
    }

    public void RemoveMoney(int count)
    {
        Money -= count;

        if (Money < 0)
        {
            Money = 0;
        }
    }
    
    
}
