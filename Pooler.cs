using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler
{
    GameObject _object;
    public GameObject pObject 
    { 
        get { return _object; }
        set { if (_object == null) _object = value; }
    }

    public bool IsUse { get; protected set; }
    public virtual void Init()
    {
        IsUse = true;
    }

    public virtual void Release()
    {
        IsUse = false;
    }
}
