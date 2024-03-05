using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This class is used to store the input buffer for the player
public class InputBuffer
{
    public static string[] rawInputList = new string[]
    {
       "Attack",
       "Dash"
    };

    // This list is used to store the buffered buttons from player's input
    public List<InputButtons> inputList = new List<InputButtons>();
    
    // This function is used to update the input buffer
    public void Update()
    {
        GameManager.instance.inputBuffer = this;
        // If the input list is empty or the input list is not the same size as the raw input list,
        // then initialize the buffer
        if (inputList.Count < rawInputList.Length || inputList.Count == 0)
        {
            InitializeBuffer();
        }

        // Update the input buffer
        foreach (InputButtons input in inputList)
        {
            input.ResolveCommand();
            for (int i = 0; i < input.buffer.Count - 1; i++)
            {
                input.buffer[i].hold = input.buffer[i + 1].hold;
                input.buffer[i].used = input.buffer[i + 1].used;
            }
        }
    }

    // Initialize a list of buffered buttons
    void InitializeBuffer()
    {
        // Clear the list of button inputs
        inputList = new List<InputButtons>();

        // Add the raw input list to the input list
        foreach (string input in rawInputList)
        {
            InputButtons newButton = new InputButtons();
            newButton.button = input;
            inputList.Add(newButton);
        }
    }
}

// This class is used to store and initialize buffered buttons for the player
public class InputButtons
{
    //Needs to be changed to Unity's Input System
    public string button;                   //The name of the trigger button

    public List<ButtonState> buffer;        //The buffer window list

    public static int bufferWindow = 40;    //The buffer window frame size
    
    //When the input button is initialized, create a window of buffer frames the size of bufferWindow
    public InputButtons()
    {
        buffer = new List<ButtonState>();
        for (int i = 0; i < bufferWindow; i++)
        {
            buffer.Add(new ButtonState());
        }
    }

    public void ResolveCommand()
    {
        if (Input.GetButton(button))
        {
            //If the button is held down, then set the last button in the buffer to held
            buffer[buffer.Count - 1].HeldDown();
        }
        else
        {
            buffer[buffer.Count - 1].ReleaseHold();
        }
    }
}

//This class is used to store the states of buffered buttons
public class ButtonState
{
    public int hold;
    public bool used;

    public bool CanExecute()
    {
        if (hold == 1 && !used)
        {
            return true;
        }
        return false;
    }

    //If the button is held down, then increment the hold value
    public void HeldDown()
    {
        if(hold < 0) { hold = 1;}
        else { hold++;}
    }

    //If the button is released, then reset the hold value
    public void ReleaseHold()
    {
        if(hold > 0) { hold = -1; used = false; }
        else { hold = 0;}
    }
}
