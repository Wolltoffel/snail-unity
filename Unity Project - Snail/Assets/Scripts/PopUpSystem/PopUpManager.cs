using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class PopUpManager
{
    GameObject popUpTemplate;

    GameObject popUp;

    public PopUpManager(string templatePath)
    {
        popUpTemplate = Resources.Load(templatePath) as GameObject;
    }

   public IEnumerator showPopUp(string text,float showTime) {

        popUp = GameObject.Instantiate(popUpTemplate, new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0),Quaternion.Euler(0,0,0));
        popUp.GetComponentInChildren<TextMeshProUGUI>().text = text;
        popUp.GetComponentInChildren<Button>()?.onClick.AddListener(() => hidePopUp());
        yield return new WaitForSeconds(showTime);
        hidePopUp();
    }

    public IEnumerator showPopUp(string text, float showTime, IEnumerator followUp)
    {

        popUp = GameObject.Instantiate(popUpTemplate, new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0), Quaternion.Euler(0, 0, 0));
        popUp.GetComponentInChildren<TextMeshProUGUI>().text = text;
        popUp.GetComponentInChildren<Button>()?.onClick.AddListener(() => hidePopUp());
        yield return new WaitForSeconds(showTime);
        hidePopUp();
        yield return followUp;
    }

    public void hidePopUp()
    {
        if (popUp != null)
        {
            popUp.SetActive(false);
            GameObject.Destroy(popUp);
        }
        popUp = null;
    }

}
