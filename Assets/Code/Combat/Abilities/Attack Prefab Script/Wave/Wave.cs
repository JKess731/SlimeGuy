using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : Attacks
{
    [SerializeField] private GameObject _parent;

    //public void Spawn(Vector2 position, Quaternion rotation, WaveStruct parent, WaveStruct child)
    //{
    //    GameObject waveparent = Instantiate(_parent, position, rotation);
    //    try
    //    {             
    //        waveparent.GetComponent<WaveParent>().SetParentWave(parent, child); 
    //    }
    //    catch (System.Exception e)
    //    {
    //        Debug.Log(e);
    //    }
    //}
}
