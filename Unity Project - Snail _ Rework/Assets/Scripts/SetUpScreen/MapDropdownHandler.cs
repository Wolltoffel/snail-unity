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
    List<string> selectableMaps_names;

    private void Start()
    {
        TMPro.TMP_Dropdown dropdown = GetComponent<TMPro.TMP_Dropdown>();
        dropdown.options.Clear();

        //Insert Loaded MapData into dropdown menu
        selectableMaps_names = new List<string>();
        selectableMaps = gameData.GetSelectableMaps();

        for (int i = 0;i<selectableMaps.Count;i++)
        {
          selectableMaps_names.Add(selectableMaps[i].name);
          dropdown.options.Add(new TMPro.TMP_Dropdown.OptionData() { text = selectableMaps[i].name });
        }

        //Set Default or preselected Value for Dropdown as Standard
        if (gameData.GetSelectedMap() == null)
        {
            dropdownIndex = selectableMaps_names.IndexOf("default");
            dropdown.value = dropdownIndex;
            if (selectableMaps.Count>0)
                gameData.SetSelectedMap(selectableMaps[dropdownIndex]);
        }

        else
            dropdown.value = selectableMaps_names.IndexOf(gameData.GetSelectedMap().name); //Select preselected map

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
