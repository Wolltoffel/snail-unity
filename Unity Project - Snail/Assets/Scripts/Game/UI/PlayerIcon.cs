using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerIcon : MonoBehaviour
{
    [SerializeField] int playerIndex;

    private void Start()
    {
        Sprite playerSprite = AssetLoader.LoadPlayerSprites()[playerIndex];
        GetComponent<Image>().sprite = playerSprite;
    }
}
