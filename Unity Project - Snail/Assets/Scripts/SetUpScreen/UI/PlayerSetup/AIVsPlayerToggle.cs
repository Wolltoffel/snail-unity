using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIVsPlayerToggle : MonoBehaviour
{
    [SerializeField] GameObject humanToggle;
    [SerializeField] GameObject computerToggle;

    ActiveAgent activeAgent;

    Player player;

    [SerializeField]int index;

    private void Start()
    {
        player = SetUpScreenData.setUpScreenData.players[index];

     loadDefaultValues();

    }
    void loadDefaultValues()
    {
        activeAgent = ActiveAgent.human;
        switchToHuman(index);
    }

    public void toggle()
    {
        switch (activeAgent)
        {
            case ActiveAgent.human:
                switchToComputer(index);
                break;
            case ActiveAgent.computer:
                switchToHuman(index);
                break;
        }
    }

    void switchToHuman(int indexInput)
    {
        activeAgent = ActiveAgent.human;
        computerToggle.SetActive(false);
        humanToggle.SetActive(true);
        SetUpScreenData.setUpScreenData.setAgent(ActiveAgent.human, index);
    }

    void switchToComputer(int indexInput)
    {
        activeAgent = ActiveAgent.computer;
        computerToggle.SetActive(true);
        humanToggle.SetActive(false);
        SetUpScreenData.setUpScreenData.setAgent(ActiveAgent.computer, index);
    }
}
