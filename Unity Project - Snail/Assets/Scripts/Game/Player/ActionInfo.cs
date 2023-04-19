using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActionInfo : EventArgs
{
    public enum Action
    {
        slide, skip, capture,empty
    };
    public Action action;
    public Player player;

    public ActionInfo(Action action, Player player)
    {
        this.action = action;
        this.player = player;
    }
}
