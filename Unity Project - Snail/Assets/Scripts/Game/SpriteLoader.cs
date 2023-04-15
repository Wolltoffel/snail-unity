using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLoader
{

    public Object ground,impassable,highlight;
    public List<Object> player,slime;

    public SpriteLoader()
    {
        LoadSprites();
    }
    void LoadSprites()
    {
        ground = Resources.Load("Sprites/" + "ground");
        impassable = Resources.Load("Sprites/" + "impassable");
        highlight = Resources.Load("Sprites/" + "empty_256x256");
        
        player = new List<Object>();
        player.Add(Resources.Load("Sprites/" + "snail_blue_256x256"));
        player.Add(Resources.Load("Sprites/" + "snail_orange_256x256"));
        
        slime = new List<Object>();
        slime.Add(Resources.Load("Sprites/" + "trail_blue_256x256"));
        slime.Add(Resources.Load("Sprites/" + "trail_orange_256x256"));
    }
}
