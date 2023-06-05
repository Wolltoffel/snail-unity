using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DropdownHandler : MonoBehaviour
{
    [SerializeField] GameData gameData;
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
            gameData.SetSelectedMap(selectableMaps[dropdownIndex]);
        }

        else
            dropdown.value = selectableMaps_names.IndexOf(gameData.GetSelectedMap().name); //Select preselected map


        //Set up Droppdown vor Value Changes
        dropdown.onValueChanged.AddListener(delegate { setMap(dropdown); }); //Adjust dropdown on change

        dropdownIndex = dropdown.value;
    }

    void setMap(TMPro.TMP_Dropdown dropdown)
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
            Debug.Log(mapValidity);
            //Throw error message
        }
            
    }
}
