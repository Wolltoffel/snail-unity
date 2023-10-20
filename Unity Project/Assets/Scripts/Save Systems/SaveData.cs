using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract base class for save data objects.
/// </summary>
public abstract class SaveData
{
    /// <summary>
    /// Loads the default values for the save data.
    /// </summary>
    public abstract void LoadDefaultValues();
}