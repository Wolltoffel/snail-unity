using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp: MonoBehaviour {

    float showTimeCounter;
    float maxShowTime;
    PopUpManager popUpManager;
    
     private void Update()
     {
        checkPopUpActivation();
     }

    void checkPopUpActivation()
    {
        showTimeCounter += Time.deltaTime;

        if (showTimeCounter >= maxShowTime)
        {
            showTimeCounter = 0;
            popUpManager.hidePopUp();
        }
    }

    public void setData(float maxShowTime, PopUpManager popUpManager)
    {
        this.maxShowTime = maxShowTime;
        this.popUpManager = popUpManager;
    }


}

