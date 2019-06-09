﻿using System.IO;
using UnityEngine;

public class JsonData<T> : IData<T>
{
    public void Save(T data, string path = null)
    {
        var str = JsonUtility.ToJson(data);
        File.WriteAllText(path, Crypto.CryptoXor(str));
        //File.WriteAllText(path, str);
    }

    public T Load(string path = null)
    {
        var str = File.ReadAllText(path);
        return JsonUtility.FromJson<T>(Crypto.CryptoXor(str));
        //return JsonUtility.FromJson<T>(str);
    }
}