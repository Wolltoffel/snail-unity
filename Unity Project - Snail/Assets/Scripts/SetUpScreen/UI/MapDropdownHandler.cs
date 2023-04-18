using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapDropdownHandler : MonoBehaviour
{
    public MapManager mapManager;

    public MapData testData;

    private void Start()
    {
        TMPro.TMP_Dropdown dropdown = GetComponent<TMPro.TMP_Dropdown>();
        dropdown.options.Clear();

        List<string> items = new List<string>();
        List <MapData> maps = mapManager.maps;

        foreach(var map in maps)
        {
            if (map != null) { 
            items.Add(map.name);
            }
        }

        foreach (var item in items)
        {
            dropdown.options.Add(new TMPro.TMP_Dropdown.OptionData() {text = item});
        }
       
        //Set Default or preselected Value for Dropdown as Standard
        if (MapManager.selectedMap == null)
            dropdown.value = items.IndexOf("default");
        else
            dropdown.value = items.IndexOf(MapManager.selectedMap.name);

        dropdown.onValueChanged.AddListener(delegate { setMap(dropdown); });

    }

    void setMap(TMPro.TMP_Dropdown dropdown)
    {
        int index = dropdown.value;
        MapManager.selectedMap = mapManager.maps[index];
    }
}
