using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Player[] player;
    MapData activeMap;

    private void Start()
    {
        player = new Player[2];
       
    }
}
