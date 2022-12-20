
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.SDK3.Components;
public class SellableItem : UdonSharpBehaviour
{
    public int sellPrice = 1;
    public VRCObjectPool objectPool;

    void Start()
    {

    }

    private void Update()
    {

    }

    void ReturnToObjectPool()
    {
        if (objectPool != null)
        {
            transform.SetParent(objectPool.transform);
            objectPool.Return(gameObject);
        }
    }

    public void KillObject()
    {
        ReturnToObjectPool();
    }

    public override void OnPickup()
    {
        base.OnPickup();
        Networking.SetOwner(Networking.LocalPlayer, gameObject);
    }
}
