using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    Vector2 mapSize;
    [SerializeField] float offset;
    [SerializeField]Transform mapTransform;

        
    public void SetUpCamera(Vector2 mapSize)
    {
        this.mapSize = mapSize;
        PlaceCamera();
        ScaleCamera();
    }

    void PlaceCamera()
    {
        Vector2 centerPosition = Vector2.zero + new Vector2(mapSize.x / 2, -mapSize.y / 2);
        transform.position = mapTransform.position+ new Vector3 (centerPosition.x-0.5f,centerPosition.y+0.5f,-4);
    }

    void ScaleCamera()
    {
        Vector2 leftUpperCorner = Vector2.zero;
        Vector3 rightBottomCorner = new Vector3(mapSize.x, -mapSize.y, 0);
        float distance = Vector2.Distance(leftUpperCorner, rightBottomCorner);
        GetComponent<Camera>().orthographicSize = (distance) / Mathf.Sqrt(8) + offset;
    }
}