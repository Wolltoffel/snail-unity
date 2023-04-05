using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapDropdownHandler : MonoBehaviour
{
    public MapManager mapManager;

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
            Debug.Log(map.name);
            }
        }

        foreach (var item in items)
        {
            dropdown.options.Add(new TMPro.TMP_Dropdown.OptionData() {text = item});
        }
        dropdown.onValueChanged.AddListener(delegate { setMap(dropdown); });

    }

    void setMap(TMPro.TMP_Dropdown dropdown)
    {
        int index = dropdown.value;
        mapManager.selectedMap = mapManager.maps[index];
    }
}
