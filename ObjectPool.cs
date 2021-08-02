using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    Queue<Pooler> Container;
    Dictionary<GameObject, Pooler> NowUsing;
    Func<Pooler> Factory;

    public ObjectPool Init(Func<Pooler> factory, int capital)
    {
        Factory = factory;

        Container = new Queue<Pooler>();
        NowUsing = new Dictionary<GameObject, Pooler>();

        for (int i = 0; i < capital; i++)
        {
            ProductToContain();
        }

        return this;
    }

    /// <summary> 미사용에 생성 </summary>
    void ProductToContain()
    {
        Pooler t = Factory();
        Container.Enqueue(t);
        t.pObject.SetActive(false);
    }

    /// <summary> 풀에서 가져오거나, 생산 </summary>
    public GameObject Product()
    {
        Pooler plr;

        if (Container.Count > 0)
        {
            plr = Container.Dequeue();
        }
        else
        {
            plr = Factory(); 
        }

        NowUsing.Add(plr.pObject, plr);
        plr.Init();
        plr.pObject.SetActive(true);

        return plr.pObject;
    }

    /// <summary> 릴리즈 </summary>
    public void Release(GameObject obj)
    {
        if (NowUsing.ContainsKey(obj))
        {
            Pooler plr = NowUsing[obj];

            plr.Release();
            Container.Enqueue(plr);
            NowUsing.Remove(obj);

            plr.pObject.SetActive(false);
        }
    }

    /// <summary> 전체 릴리즈 </summary>
    public void AllRelease()
    {
        Pooler plr;
        foreach (KeyValuePair<GameObject, Pooler> obj in NowUsing)
        {
            plr = NowUsing[obj.Key];

            plr.Release();
            Container.Enqueue(plr);
            NowUsing.Remove(obj.Key);

            plr.pObject.SetActive(false);
        }
    }
}
