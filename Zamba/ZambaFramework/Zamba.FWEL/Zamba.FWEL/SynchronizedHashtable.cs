
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
public class SynchronizedHashtable
{
    //Inherits Hashtable


    public Hashtable _syncronizedHT;
    public SynchronizedHashtable()
    {
        _syncronizedHT = Hashtable.Synchronized(new Hashtable());
    }

    public void Add(object key, object value)
    {
        _syncronizedHT.Add(key, value);
    }

    public object this[object key]
    {
        get { return _syncronizedHT[key]; }
        set { _syncronizedHT[key] = value; }
    }

    public void Clear()
    {
        _syncronizedHT.Clear();
    }

    public bool Contains(object key)
    {
        return _syncronizedHT.Contains(key);
    }

    public bool ContainsKey(object key)
    {
        return _syncronizedHT.ContainsKey(key);
    }

    public object Clone()
    {
        return _syncronizedHT.Clone();
    }

    public bool ContainsValue(object value)
    {
        return _syncronizedHT.ContainsValue(value);
    }

    public int Count
    {
        get { return _syncronizedHT.Count; }
    }

    public void Remove(object key)
    {
        _syncronizedHT.Remove(key);
    }

    public void CopyTo(System.Array array, int arrayIndex)
    {
        _syncronizedHT.CopyTo(array, arrayIndex);
    }

    public bool Equals(object obj)
    {
        return _syncronizedHT.Equals(obj);
    }

    public System.Collections.IDictionaryEnumerator GetEnumerator()
    {
        return _syncronizedHT.GetEnumerator();
    }

    public object SyncRoot
    {
        get { return _syncronizedHT.SyncRoot; }
    }

    public ICollection Values
    {
        get { return _syncronizedHT.Values; }
    }

    public ICollection Keys
    {
        get { return _syncronizedHT.Keys; }
    }

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================

