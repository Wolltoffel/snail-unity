using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpManager
{
    GameObject popUpTemplate;
    GameObject canvas;
    GameObject popUp;


    public PopUpManager(string templatePath)
    {
        popUpTemplate = Resources.Load(templatePath) as GameObject;
    }

   public void showPopUp(string text,float showTime) {

        popUp = GameObject.Instantiate(popUpTemplate, new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0),Quaternion.Euler(0,0,0));
        popUp.GetComponentInChildren<TextMeshProUGUI>().text = text;
        popUp.AddComponent<PopUp>().setData(showTime, this);
    }


    public void hidePopUp()
    {
        popUp.SetActive(false);
        GameObject.Destroy(popUp);
        popUp = null;
    }

}
