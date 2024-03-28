using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasicMinion : Minion
{

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Base");
        Debug.Log(stateMachine.currentState);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
}
