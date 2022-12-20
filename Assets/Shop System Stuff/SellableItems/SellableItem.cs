
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.SDK3.Components;
public class SellableItem : UdonSharpBehaviour
{
    public int sellPrice = 1;
    [HideInInspector] public VRCObjectPool objectPool;
    void Start()
    {

    }

    public void ReturnToObjectPool()
    {
        if (objectPool != null)
        {
            transform.SetParent(objectPool.transform);
            transform.position = Vector3.zero;
            objectPool.Return(gameObject);
        }
    }
}
