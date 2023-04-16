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

    public ActionInfo(Action action)
    {
        this.action = action;
    }
}
