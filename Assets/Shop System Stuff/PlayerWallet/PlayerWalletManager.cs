
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class PlayerWalletManager : UdonSharpBehaviour
{
    public int CurrentMoney = 50;
    int previousMoney;
    [HideInInspector] public ShopManager[] shopManagers;
    void Start()
    {

    }
    private void Update()
    {
        if (CurrentMoney != previousMoney)
        {
            MoneyChanged();
            previousMoney = CurrentMoney;
        }
    }

    void MoneyChanged()
    {
        for (int i = 0; i < shopManagers.Length; i++)
        {
            shopManagers[i].OnMoneyChanged(CurrentMoney);
        }
    }
}
