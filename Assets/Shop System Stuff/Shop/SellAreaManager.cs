
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;


public class SellAreaManager : UdonSharpBehaviour
{
    public ShopManager shopManager;
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        SellableItem sellableItem = other.gameObject.GetComponent<SellableItem>();
        if (sellableItem != null)
        {
            Networking.SetOwner(Networking.GetOwner(sellableItem.gameObject), shopManager.gameObject);
            if (Networking.GetOwner(sellableItem.gameObject) != Networking.LocalPlayer)
            {
                sellableItem.KillObject();
                shopManager.UpdateAvailableAmount(shopManager.getObjectPoolIndex(sellableItem.objectPool), 1);
            }
            else
            {
                int id = Networking.LocalPlayer.playerId;
                GameObject wallet = GameObject.Find("PlayerWallet (" + id + ")");
                if (wallet != null)
                {
                    PlayerWalletManager playerWalletManager = wallet.GetComponent<PlayerWalletManager>();
                    if (playerWalletManager != null)
                    {
                        playerWalletManager.CurrentMoney += sellableItem.sellPrice;
                        sellableItem.KillObject();
                        shopManager.UpdateAvailableAmount(shopManager.getObjectPoolIndex(sellableItem.objectPool), 1);
                    }
                }
            }
        }
    }
}
