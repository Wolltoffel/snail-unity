using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetLoader
{

    public Object ground,impassable,highlight;
    public List<Object> player,slime;
   

    public AssetLoader()
    {
        LoadObjects();
    }
    void LoadObjects()
    {
        ground = Resources.Load("Prefabs/" + "ground");
        impassable = Resources.Load("Prefabs/" + "impassable");
        highlight = Resources.Load("Prefabs/" + "empty_256x256");
        
        player = new List<Object>();
        player.Add(Resources.Load("Prefabs/" + "snail_blue_256x256"));
        player.Add(Resources.Load("Prefabs/" + "snail_orange_256x256"));
        
        slime = new List<Object>();
        slime.Add(Resources.Load("Prefabs/" + "trail_blue_256x256"));
        slime.Add(Resources.Load("Prefabs/" + "trail_orange_256x256"));
    }

    public static Sprite[] LoadPlayerSprites() {

        object[] loadedSprites = Resources.LoadAll("Sprites",typeof(Sprite));
        Sprite[] playerSprites = new Sprite[loadedSprites.Length];

        for(int i = 0; i < playerSprites.Length; i++)
        {
            playerSprites[i] = (Sprite)loadedSprites[i];
        }

        return playerSprites;
    }

}
