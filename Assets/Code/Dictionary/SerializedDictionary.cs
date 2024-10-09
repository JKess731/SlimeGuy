using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerializedDictionary : MonoBehaviour
{
    [SerializeField]
    string dictionaryName;

    [SerializeField]
    NewDictionary newDictionary;

    Dictionary<Enum_Stat, float> dictionary;

    void Start()
    {
        dictionary = newDictionary.ToDictionary();
    }
}

[Serializable]
public class DictionaryItems
{
    public Enum_Stat stat;
    public float value;
}

public class NewDictionary
{
    [SerializeField]
    DictionaryItems[] dictItemsList;

    public Dictionary<Enum_Stat, float> ToDictionary()
    {
        Dictionary<Enum_Stat, float> dict = new Dictionary<Enum_Stat, float>();

        foreach(DictionaryItems item in dictItemsList)
        {
            dict.Add(item.stat, item.value);
        }

        return dict;
    }
}
