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

    public override void loadDefaultValues()
    {
        maxTurnDuration = 30;
        maxTurnsWithoutCapture = 5;
        maxComputationTimePerTurn = 500;
        miniCPUAgentTurnDuration = 1;
        minMapSize = 5;
        maxMapSize = 20;
        requireSquareMap = false;
    }
}
