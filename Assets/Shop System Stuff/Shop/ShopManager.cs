
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.SDK3.Components;
using UnityEngine.UI;
using TMPro;

public class ShopManager : UdonSharpBehaviour
{
    public VRCObjectPool[] ShopItems;
    public Sprite[] ItemImages;
    public string[] ItemNames;
    public int[] ItemPrices;
    public GameObject buttonPrefab;
    public GameObject shopList;
    public TextMeshProUGUI playerMoney;
    public Transform shopSpawner;
    void Start()
    {
        AddItems();
    }

    public void AddItems()
    {
        for (int i = 0; i < ShopItems.Length; i++)
        {
            GameObject button = Instantiate(buttonPrefab, shopList.transform);
            ShopButtonManager shopButtonManager = button.GetComponent<ShopButtonManager>();
            if (shopButtonManager != null)
            {
                shopButtonManager.objectPool = ShopItems[i];
                shopButtonManager.shopManager = this;

                if (ItemImages[i] != null)
                {
                    shopButtonManager.sprite = ItemImages[i];
                }

                if (ItemNames[i] != null)
                {
                    shopButtonManager.text = ItemNames[i];
                }

                shopButtonManager.itemPrice = ItemPrices[i];
            }

        }
    }

    public void OnMoneyChanged(int money)
    {
        playerMoney.text = Networking.LocalPlayer.displayName + " money amount: " + money;
    }
}
