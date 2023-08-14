using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RunTimeSet<T> : ScriptableObject
{
    [SerializeField] private List<T> _items = new List<T>();

    public List<T> Items
    {
        get { return _items; }
        set { _items = value; }
    }

    public int Count
    {
        get { return _items.Count; }
    }

    public void Add(T t)
    {
        if(!_items.Contains(t))
        {
            _items.Add(t);
        }
    }

    public void Remove(T t)
    {
        if(_items.Contains(t))
        {
            _items.Remove(t);
        }
    }

    public void Clear()
    {
        _items.Clear();
    }
}
