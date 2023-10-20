using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the dropdown menu for selecting maps in the SetUpScreen.
/// </summary>
public class MapDropdownHandler : MonoBehaviour
{
    [SerializeField] GameData gameData;
    [SerializeField] WrongMapPopUp wrongMapPopUp;
    int dropdownIndex = 0;
    List<MapData> selectableMaps;
    List<string> selectableMaps_name;
   

    private void Start()
    {
        TMPro.TMP_Dropdown dropdown = GetComponent<TMPro.TMP_Dropdown>();
        dropdown.options.Clear();

        //Insert Loaded MapData into dropdown menu
        List<string> errormaps_name;
        selectableMaps = gameData.GetSelectableMaps(out errormaps_name);
        selectableMaps_name = new List<string>();

        //Show erromessage with faulty map
        if (errormaps_name.Count > 0)
        {
            string errorMessage = "The follwing maps are not correctly formatted:";

            for (int i = 0; i < errormaps_name.Count; i++)
            {
                errorMessage += $"\n <b>{errormaps_name[i]}</b>";
            }

            wrongMapPopUp.showPopUp(errorMessage);
        }
            


        //Add maps to dropdown
        for (int i = 0;i<selectableMaps.Count;i++)
        {
          selectableMaps_name.Add(selectableMaps[i].name);
          dropdown.options.Add(new TMPro.TMP_Dropdown.OptionData() { text = selectableMaps[i].name });
        }

        //Set Default or preselected Value for Dropdown as Standard
        if (gameData.GetSelectedMap() == null)
        {
            dropdownIndex = selectableMaps_name.IndexOf("default");
            dropdown.value = dropdownIndex;
            if (selectableMaps.Count>0)
                gameData.SetSelectedMap(selectableMaps[dropdownIndex]);
        }

        else
            dropdown.value = selectableMaps_name.IndexOf(gameData.GetSelectedMap().name); //Select preselected map

        //Set up Droppdown vor Value Changes
        dropdown.onValueChanged.AddListener(delegate { SetMap(dropdown); }); //Adjust dropdown on change

        dropdownIndex = dropdown.value;
    }

    /// <summary>
    /// Sets the selected map based on the dropdown value.
    /// </summary>
    /// <param name="dropdown">The dropdown component.</param>
    void SetMap(TMPro.TMP_Dropdown dropdown)
    {
        int index = dropdown.value;
        string mapValidity = gameData.checkMapValidity(selectableMaps[index]);
        if (mapValidity ==  "Valid")
        {
            dropdownIndex = index;
            gameData.SetSelectedMap(selectableMaps[index]);
            dropdown.value = dropdownIndex;
        }
        else
        {
            dropdown.value = dropdownIndex;
            wrongMapPopUp.showPopUp(mapValidity);
        }
            
    }
}
