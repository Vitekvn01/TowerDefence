
using System;

namespace IInterfaces
{
    public interface IWallet
    {
        public int Money { get; }
        
        public event Action<int> OnChangeCountEvent;
        
        public void AddMoney(int count);

        public void RemoveMoney(int count);
    }
}