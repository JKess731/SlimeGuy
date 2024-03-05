using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public InputBuffer inputBuffer;
    
    private void Start()
    {
        instance = this;
    }

    private void OnGUI()
    {
        int xSpace = 20;
        int ySpace = 25;

        for (int i = 0; i < inputBuffer.inputList.Count; i++)
        {
            GUI.Label(new Rect(xSpace, ySpace * i, 100, 20), inputBuffer.inputList[i].button + ":");
            for (int j = 0; j < inputBuffer.inputList[i].buffer.Count; j++)
            {
                if (inputBuffer.inputList[i].buffer[j].used)
                {
                    GUI.Label(new Rect(xSpace * j, 10 + ySpace * i, 100, 20), inputBuffer.inputList[i].buffer[j].hold.ToString() + "*");
                }
                else
                {
                    GUI.Label(new Rect(xSpace * j, 10 + ySpace * i, 100, 20), inputBuffer.inputList[i].buffer[j].hold.ToString());
                }
            }
        }
    }
}
