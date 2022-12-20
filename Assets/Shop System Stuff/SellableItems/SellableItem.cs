
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.SDK3.Components;
public class SellableItem : UdonSharpBehaviour
{
    public int sellPrice = 1;
    public VRCObjectPool objectPool;
    [HideInInspector][UdonSynced] public bool isRemovedFromParent = false;
    void Start()
    {

    }

    private void Update()
    {
        if (isRemovedFromParent && transform.parent != null)
        {
            transform.SetParent(null);
        }
        else if (!isRemovedFromParent && transform.parent == null)
        {
            ReturnToObjectPool();
        }
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
        isRemovedFromParent = false;
        ReturnToObjectPool();
    }
}
