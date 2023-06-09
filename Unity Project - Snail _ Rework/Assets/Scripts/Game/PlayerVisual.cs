using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual
{
    GameObject playerAsset;
    GameObject slimeAsset;

    GameObject playerReference;
    List <GameObject> slimeReferences;

    SurrenderInteractable skipInteractable;

    public PlayerVisual(GameObject playerAsset,GameObject slimeAsset)
    {
        this.slimeAsset = slimeAsset;
        this.playerAsset = playerAsset;
        slimeReferences = new List<GameObject>();
    }

    public void SpawnPlayerObjects(Vector3 worldPosition, GameObject mapParent, string playerName, GameController gameController)
    {
        playerReference = GameObject.Instantiate(playerAsset, worldPosition, Quaternion.Euler(Vector3.zero));
        playerReference.name = playerName + "_Snail";
        playerReference.transform.SetParent(mapParent.transform);
        playerReference.AddComponent<BoxCollider2D>();
        skipInteractable   = playerReference.AddComponent<SurrenderInteractable>();
        skipInteractable.InsertGameData(gameController);
    }

    public void SpawnSlime(Tile tile, GameObject parent) {
        GameObject slimeReferenceLocal = GameObject.Instantiate(slimeAsset, tile.worldPosition, Quaternion.Euler(Vector3.zero));
        slimeReferenceLocal.transform.SetParent(parent.transform);
        slimeReferenceLocal.name = $"Slime{tile.position.x}/{tile.position.y}";
        slimeReferences.Add(slimeReferenceLocal);
    }

    public IEnumerator Move(Vector3 target)
    {
        Vector3 playerPosition = playerReference.transform.position;
        Vector3 startPosition = playerPosition;
        float startTime = Time.time;
        while (Vector3.Distance(playerPosition, target) > 0.01f)
        {
            float t = (Time.time - startTime) / 0.5f;
            playerPosition = Vector3.Lerp(startPosition, target, t);
            playerReference.transform.position = playerPosition;
            yield return null;
        }
    }

    public void SetSkipButtonActive(bool active)
    {
        skipInteractable.enabled = active;   
    }

    public void Reset()
    {
        GameObject.Destroy(playerReference);

        for (int i = 0; i < slimeReferences.Count; i++)
        {
            GameObject.Destroy(slimeReferences[i]);
        }
    }

}
