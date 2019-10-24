using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdventureGame : MonoBehaviour
{
    [SerializeField] Text textComponent;
    [SerializeField] State startingState;
    [SerializeField] Text coinsComponent;
    [SerializeField] GameObject knifeComponent;

    State currentState;
    int coins = 0;
    bool knife = false;

    // Start is called before the first frame update
    void Start()
    {
        currentState = startingState;
        textComponent.text = currentState.GetStateStory();
        coinsComponent.text = ("0");
    }

    // Update is called once per frame
    void Update()
    {
        ManageState();
        ShowKnife();
    }

    private void ShowKnife()
    {
        if (currentState.GetKnife())
        {
            knifeComponent.SetActive(true);
            knife = true;
        }
    }

    private void ManageState()
    {
        var nextStates = currentState.GetNextStates();

        // Condition
        Debug.Log(currentState.GetCondition());
        if (currentState.GetCondition() != "")
        {
            int i = GetConditionResult(currentState.GetCondition());
            Debug.Log(GetConditionResult(currentState.GetCondition()));
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
            {
                currentState = nextStates[i];
                textComponent.text = currentState.GetStateStory();
                GetRoomCoins();
            }
        }
        else
        {
            // No choice
            if (nextStates.Length == 1)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    currentState = nextStates[0];
                    textComponent.text = currentState.GetStateStory();
                    GetRoomCoins();
                }
            }

        

            // Choices
            for (int i = 0; i<nextStates.Length; i++)
            {
                if (Input.GetKeyDown(KeyCode.Keypad1+i) || Input.GetKeyDown(KeyCode.Alpha1+i))
                {
                    Debug.Log("Selected choice " + (i + 1));
                    currentState = nextStates[i];
                    textComponent.text = currentState.GetStateStory();
                    GetRoomCoins();
                }
            }
        }
        

        // To quit the game
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Chose to quit");
            currentState = startingState;
            textComponent.text = currentState.GetStateStory();
            coins = 0;
            coinsComponent.text = coins.ToString();

        }

    }

    private void GetRoomCoins()
    {
        coins += currentState.GetCoins();
        if (coins < 0)
        {
            coins = 0;
        }
        coinsComponent.text = coins.ToString();
    }

    private int GetConditionResult(string conditionToContinue)
    {
        bool condition = false;
        if (conditionToContinue == "knife")
        {
            condition = knife;
        }
        else if (conditionToContinue == "coins")
        {
            condition = (coins > 20);
        }

        if (condition) //(bool)this.GetType().GetField(conditionToContinue).GetValue(this))
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
