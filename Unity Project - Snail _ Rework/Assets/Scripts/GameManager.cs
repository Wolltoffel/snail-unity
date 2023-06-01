using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] AssetHolder assetHolder;
    MapBuilder mapBuilder;
    int activePlayerIndex;
    

    void Awake()
    {
        mapBuilder = new MapBuilder(assetHolder);
    }

   IEnumerator Start()
    {
         mapBuilder.LoadMapFromFile();
        //yield return mapBuilder.MovePlayer(activePlayerIndex);
        yield return null;
    }
}
