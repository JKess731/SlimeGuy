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

    Dictionary<Enum_Stats, float> dictionary;

    void Start()
    {
        dictionary = newDictionary.ToDictionary();
    }
}

[Serializable]
public class DictionaryItems
{
    public Enum_Stats stat;
    public float value;
}

public class NewDictionary
{
    [SerializeField]
    DictionaryItems[] dictItemsList;

    public Dictionary<Enum_Stats, float> ToDictionary()
    {
        Dictionary<Enum_Stats, float> dict = new Dictionary<Enum_Stats, float>();

        foreach(DictionaryItems item in dictItemsList)
        {
            dict.Add(item.stat, item.value);
        }

        return dict;
    }
}
