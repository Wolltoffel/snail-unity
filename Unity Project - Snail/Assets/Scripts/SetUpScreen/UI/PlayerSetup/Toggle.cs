using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle : MonoBehaviour
{
    [SerializeField] GameObject humanToggle;
    [SerializeField] GameObject computerToggle;

    enum ActiveAgent{
        computer,
        human
    }
    ActiveAgent activeAgent;

    [SerializeField]int index;

    private void Start()
    {
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
                activeAgent = ActiveAgent.computer;
                switchToComputer(index);
                break;
            case ActiveAgent.computer:
                activeAgent = ActiveAgent.human;
                switchToHuman(index);
                break;
        }
    }

    void switchToHuman(int indexInput)
    {
        computerToggle.SetActive(false);
        humanToggle.SetActive(true);
        PlayerConfig.SetAgent(Player.Agent.human, index);
    }

    void switchToComputer(int indexInput)
    {
        computerToggle.SetActive(true);
        humanToggle.SetActive(false);
        PlayerConfig.SetAgent(Player.Agent.computer, index);
    }
}
