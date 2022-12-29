
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.SDK3.Components;
using UnityEngine.UI;
using TMPro;
public class ShopButtonManager : UdonSharpBehaviour
{
    [HideInInspector] public VRCObjectPool objectPool;
    [HideInInspector] public ShopManager shopManager;
    [HideInInspector] public Sprite sprite;
    [HideInInspector] public string text;
    [HideInInspector] public int itemPrice;
    public Image image;
    public TextMeshProUGUI tmpro;
    void Start()
    {
        image.sprite = sprite;
    }

    public void OnBuyItem()
    {
        int id = Networking.LocalPlayer.playerId;
        GameObject wallet = GameObject.Find("PlayerWallet (" + id + ")");
        PlayerWalletManager playerWalletManager = wallet.GetComponent<PlayerWalletManager>();

        if (playerWalletManager != null)
        {
            if (playerWalletManager.CurrentMoney >= itemPrice)
            {
                Networking.SetOwner(Networking.LocalPlayer, shopManager.gameObject);
                Networking.SetOwner(Networking.LocalPlayer, objectPool.gameObject);
                GameObject obj = objectPool.TryToSpawn();
                if (obj != null)
                {
                    Networking.SetOwner(Networking.LocalPlayer, obj);
                    SellableItem sellableItem = obj.GetComponent<SellableItem>();
                    obj.transform.SetParent(null);
                    obj.transform.position = shopManager.shopSpawner.position;

                    obj.transform.rotation = new Quaternion();
                    playerWalletManager.CurrentMoney -= itemPrice;
                    shopManager.UpdateAvailableAmount(shopManager.getObjectPoolIndex(sellableItem.objectPool), -1);
                }
            }
        }

    }

    public void UpdateText(int amount)
    {
        tmpro.text = text + " - price: " + itemPrice + " - available amount: " + amount;
    }
}
