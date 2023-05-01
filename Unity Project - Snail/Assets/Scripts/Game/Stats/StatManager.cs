using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    static StatData stats;
    public static ResultInfo resultInfo;
    public static StatManager statManager;

    private void Awake()
    {
        if (statManager != null)
            Destroy(this);
        else
            statManager = this;
    }

    public void overrideStats(StatData newStats)
    {
        stats = newStats;
    }

    public StatData giveCurrentStats()
    {
        return stats;
    }
}
