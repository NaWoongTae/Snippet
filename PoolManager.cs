using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : TSingleton<PoolManager>
{
    public enum Poolject { Bullet, Bullet2 }

    /// <summary> Object Pool List </summary>
    Dictionary<Poolject, ObjectPool> poolList;

    /// <summary> 초기화 </summary>
    protected override void Init()
    {
        base.Init();

        poolList = new Dictionary<Poolject, ObjectPool>();
    }

    /// <summary> (새 프리팹)으로 (오브젝트 Pool) 제작 요청 </summary>
    public void SubmitPrefab(GameObject fab, Transform root = null, int capital = 5)
    {
        Poolject poject = EnumHelper.StringToEnum<Poolject>(fab.name);

        poolList.Add(poject, new ObjectPool().Init(() => 
        {
            return factory(fab, root);
        }, capital));
    }

    /// <summary> 생산 </summary>
    Pooler factory(GameObject fab, Transform root)
    {
        var plr = new Pooler();
        plr.pObject = Instantiate(fab, root);

        return plr;
    }

    /// <summary> GameObject 형태로 받기 </summary>
    public GameObject GetPoolingObject(Poolject obj)
    {
        return poolList[obj].Product();
    }
    /// <summary> T 형태로 받기 </summary>
    public T GetPoolingComp<T>(Poolject obj)
    {        
        return poolList[obj].Product().GetComponent<T>();
    }

    /// <summary> 릴리즈 </summary>
    public void ReleaseObject(Poolject obj, GameObject plr)
    {
        poolList[obj].Release(plr);
    }
}
