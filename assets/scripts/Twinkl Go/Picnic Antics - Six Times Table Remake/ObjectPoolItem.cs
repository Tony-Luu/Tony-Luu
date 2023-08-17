using UnityEngine;
using System;

[Serializable]
public class ObjectPoolItem
{
    public int AmountToPool;
    public GameObject ObjectToPool;
    public bool ShouldExpand;

}
