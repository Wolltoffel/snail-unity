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
        MapData[] maps = mapManager.maps;

        for (int i = 0; i < maps.Length; i++)
        {
            if (maps[i] != null) { 
            items.Add(maps[i].name);
            Debug.Log(maps[i].name);
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
