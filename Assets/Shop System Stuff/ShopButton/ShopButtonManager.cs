
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
        tmpro.text = text + " price: " + itemPrice;
    }

    public void OnBuyItem()
    {
        int id = Networking.LocalPlayer.playerId;
        GameObject wallet = GameObject.Find("PlayerWallet (" + id + ")");
        PlayerWalletManager playerWalletManager = wallet.GetComponent<PlayerWalletManager>();

        if (playerWalletManager != null)
        {
            if (playerWalletManager.CurrentMoney > itemPrice)
            {
                GameObject obj = objectPool.TryToSpawn();
                if (obj != null)
                {
                    SellableItem sellableItem = obj.GetComponent<SellableItem>();
                    sellableItem.objectPool = objectPool;
                    obj.transform.SetParent(null);
                    playerWalletManager.CurrentMoney -= itemPrice;
                }
            }
        }

    }
}
