
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class ShopSystemWorldManager : UdonSharpBehaviour
{
    public GameObject playerWalletPrefab;
    public ShopManager[] shopManagers;
    void Start()
    {

    }

    public override void OnPlayerJoined(VRCPlayerApi player)
    {
        base.OnPlayerJoined(player);
        if (player.playerId == Networking.LocalPlayer.playerId)
        {
            GameObject obj = Instantiate(playerWalletPrefab);
            obj.name = "PlayerWallet (" + player.playerId + ")";
            obj.GetComponent<PlayerWalletManager>().shopManagers = shopManagers;
        }
    }
}
