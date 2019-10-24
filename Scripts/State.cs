using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State")]
public class State : ScriptableObject
{
    [TextArea(15,14)] [SerializeField] string storyText;
    [SerializeField] State[] nextStates;
    [SerializeField] int coins;
    [SerializeField] bool knife;
    [SerializeField] string conditionToContinue;
    // TODO: for state FightMom: check if knife was taken to go to next room accordingly 
    // TODO: for state Victory: check number of coins

    public string GetStateStory()
    {
        return storyText;
    }

    public State[] GetNextStates()
    {
        return nextStates;
    }

    public int GetCoins()
    {
        return coins;
    }

    public bool GetKnife()
    {
        return knife;
    }

    public string GetCondition()
    {
        return conditionToContinue;
    }
}
