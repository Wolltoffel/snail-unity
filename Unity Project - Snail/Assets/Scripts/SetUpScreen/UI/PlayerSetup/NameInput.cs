using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameInput : MonoBehaviour
{
    public void setName(int index)
    {
        string playername = GetComponent<TMP_InputField>().text;
        PlayerSetUp.SetName(playername, index);
    }

}
