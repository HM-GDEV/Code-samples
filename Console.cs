using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    public static Console Instance;
    public GameObject player;
    public InputAction[] inputActions;
    public InputField cmdHistory;
    public InputField cmdField;

    public void Awake()
    {
        Instance = this;
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            ExecuteCommand();
        }
    }

    public void ExecuteCommand()
    {
        if (cmdField.text == null)
        {
            cmdField.ActivateInputField();
            return;
        }

        string userInput = cmdField.text.ToLower().ToString();
        cmdHistory.text = userInput + "\n" + cmdHistory.text;
        char[] delimiterCharacters = {' '};
        string[] separatedInput = userInput.Split(delimiterCharacters);

        for(int i = 0; i < inputActions.Length; i++)
        {
            InputAction action = inputActions[i];
            if(action.keyWord == separatedInput[0])
            {
                action.InputCommand(separatedInput, player);
            }
        }

        cmdField.text = null;
        cmdField.ActivateInputField();
    }
}
