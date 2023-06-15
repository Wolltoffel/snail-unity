using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the visual representation of a player and his slime in the game.
/// </summary>
public class PlayerVisual
{
    GameObject playerAsset;
    GameObject slimeAsset;

    GameObject playerReference;
    List <GameObject> slimeReferences;

    SurrenderInteractable skipInteractable;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerVisual"/> class.
    /// </summary>
    /// <param name="playerAsset">The player asset GameObject.</param>
    /// <param name="slimeAsset">The slime asset GameObject.</param>
    public PlayerVisual(GameObject playerAsset,GameObject slimeAsset)
    {
        this.slimeAsset = slimeAsset;
        this.playerAsset = playerAsset;
        slimeReferences = new List<GameObject>();
    }

    /// <summary>
    /// Spawns the player objects in the game world.
    /// </summary>
    /// <param name="worldPosition">The world position to spawn the player at.</param>
    /// <param name="mapParent">The parent GameObject for the player.</param>
    /// <param name="playerName">The name of the player.</param>
    /// <param name="gameController">The game controller.</param>
    /// <param name="playerAgent">The agent controlling the player.</param>
    public void SpawnPlayerObjects(Vector3 worldPosition, GameObject mapParent, string playerName, GameController gameController, PlayerAgent playerAgent)
    {
        playerReference = GameObject.Instantiate(playerAsset, worldPosition, Quaternion.Euler(Vector3.zero));
        playerReference.name = playerName + "_Snail";
        playerReference.transform.SetParent(mapParent.transform);
        playerReference.AddComponent<BoxCollider2D>();
        if (playerAgent == PlayerAgent.Human)
        {
                skipInteractable = playerReference.AddComponent<SurrenderInteractable>();
                skipInteractable.AddGameController(gameController);
        }
    }

    /// <summary>
    /// Spawns a slime object on a tile.
    /// </summary>
    /// <param name="tile">The tile to spawn the slime on.</param>
    /// <param name="parent">The parent GameObject for the slime.</param>
    public void SpawnSlime(Tile tile, GameObject parent) {
        GameObject slimeReferenceLocal = GameObject.Instantiate(slimeAsset, tile.worldPosition, Quaternion.Euler(Vector3.zero));
        slimeReferenceLocal.transform.SetParent(parent.transform);
        slimeReferenceLocal.name = $"Slime{tile.position.x}/{tile.position.y}";
        slimeReferences.Add(slimeReferenceLocal);
    }

    /// <summary>
    /// Moves the player to a target position.
    /// </summary>
    /// <param name="target">The target position to move towards.</param>
    /// <returns>An IEnumerator for the movement coroutine.</returns>
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

    /// <summary>
    /// Sets the activity state of the skip button.
    /// </summary>
    /// <param name="active">The desired activity state.</param>
    public void SetSkipButtonActive(bool active)
    {
        if (skipInteractable != null)
        {
            skipInteractable.enabled = active;
        }
    }

    /// <summary>
    /// Resets the player and slimes visual representations.
    /// </summary>
    public void Reset()
    {
        GameObject.Destroy(playerReference);

        for (int i = 0; i < slimeReferences.Count; i++)
        {
            GameObject.Destroy(slimeReferences[i]);
        }
        slimeReferences.Clear();
    }

}
