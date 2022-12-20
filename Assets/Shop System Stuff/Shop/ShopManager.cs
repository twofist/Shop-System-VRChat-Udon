
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.SDK3.Components;
using UnityEngine.UI;
using TMPro;

public class ShopManager : UdonSharpBehaviour
{
    public VRCObjectPool[] shopItems;
    public Sprite[] itemImages;
    public string[] itemNames;
    public int[] itemPrices;
    public GameObject buttonPrefab;
    public GameObject shopList;
    public TextMeshProUGUI playerMoney;
    public Transform shopSpawner;
    ShopButtonManager[] buttons;
    [HideInInspector][UdonSynced] public int[] availabeAmounts;
    void Start()
    {
        AddItems();
    }

    public void AddItems()
    {
        availabeAmounts = new int[shopItems.Length];
        buttons = new ShopButtonManager[shopItems.Length];
        for (int i = 0; i < shopItems.Length; i++)
        {
            GameObject button = Instantiate(buttonPrefab, shopList.transform);
            ShopButtonManager shopButtonManager = button.GetComponent<ShopButtonManager>();
            buttons[i] = shopButtonManager;
            if (shopButtonManager != null)
            {
                shopButtonManager.objectPool = shopItems[i];
                shopButtonManager.shopManager = this;

                if (itemImages[i] != null)
                {
                    shopButtonManager.sprite = itemImages[i];
                }

                if (itemNames[i] != null)
                {
                    shopButtonManager.text = itemNames[i];
                }

                shopButtonManager.itemPrice = itemPrices[i];
            }
            availabeAmounts[i] = shopItems[i].transform.childCount;
        }
        UpdateAvailableAmountsForPlayers();
    }

    public void OnMoneyChanged(int money)
    {
        playerMoney.text = Networking.LocalPlayer.displayName + " money amount: " + money;
    }

    public void UpdateAvailableAmount(int index, int amount)
    {
        if (index < 0) return;
        if (Networking.GetOwner(gameObject) != Networking.LocalPlayer)
        {
            UpdateAvailableAmountsForPlayers();
            return;
        }
        availabeAmounts[index] += amount;
        RequestSerialization();
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "UpdateAvailableAmountsForPlayers");
    }

    public void UpdateAvailableAmountsForPlayers()
    {
        for (int i = 0; i < shopItems.Length; i++)
        {
            buttons[i].UpdateText(availabeAmounts[i]);
        }
    }

    public override void OnDeserialization()
    {
        base.OnDeserialization();
        UpdateAvailableAmountsForPlayers();
    }

    public int getObjectPoolIndex(VRCObjectPool pool)
    {
        for (int i = 0; i < shopItems.Length; i++)
        {
            if (pool == shopItems[i])
            {
                return i;
            }
        }
        return -1;
    }
}
