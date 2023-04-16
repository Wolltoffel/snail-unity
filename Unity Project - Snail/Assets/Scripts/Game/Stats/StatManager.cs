using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    public static StatData stats;

    void setData(int rounds, int winnerScore, string winner) {
        stats = new StatData(rounds, winnerScore, winner);
    }
}
