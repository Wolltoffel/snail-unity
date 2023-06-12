using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettings: SaveData
{
    public float maxTurnDuration; //In Seconds
    public int maxTurnsWithoutCapture;
    public float maxComputationTimePerTurn; //In Milliseconds
    public float miniCPUAgentTurnDuration; //In Seconds
    public int minMapSize, maxMapSize;
    public bool requireSquareMap;

    public PlayerSettings(float maxTurnDuration, int maxTurnsWithoutCapture, float maxComputationTimePerTurn, float miniCPUAgentTurnDuration, int minMapSize, int maxMapSize, bool requireSquareMap)
    {
        this.maxTurnDuration = maxTurnDuration;
        this.maxTurnsWithoutCapture = maxTurnsWithoutCapture;
        this.maxComputationTimePerTurn = maxComputationTimePerTurn;
        this.miniCPUAgentTurnDuration = miniCPUAgentTurnDuration;
        this.minMapSize = minMapSize;
        this.maxMapSize = maxMapSize;
        this.requireSquareMap = requireSquareMap;
    }

    public PlayerSettings()
    {
        LoadDefaultValues();
    }

    public override void LoadDefaultValues()
    {
        maxTurnDuration = 30;
        maxTurnsWithoutCapture = 5;
        maxComputationTimePerTurn = 500;
        miniCPUAgentTurnDuration = 1;
        minMapSize = 5;
        maxMapSize = 20;
        requireSquareMap = false;
    }

    public void CorrectInvalidEntries()
    {
        if (maxTurnDuration <= 0)
            maxTurnDuration = 30;
        if (maxTurnsWithoutCapture <= 0)
            maxTurnsWithoutCapture = 5;
        if (maxComputationTimePerTurn <= 500)
            maxComputationTimePerTurn = 500;
        if (miniCPUAgentTurnDuration <= 0)
            miniCPUAgentTurnDuration = 1;
        if (minMapSize <= 0)
            minMapSize = 5;
        if (maxMapSize <= 2)
            maxMapSize *= 5;
    }
}
