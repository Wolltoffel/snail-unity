using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActionInfo : EventArgs
{
    public ActionType actionType;
    public Player player;

    public ActionInfo(ActionType actionType, Player player)
    {
        this.actionType = actionType;
        this.player = player;
    }
}
