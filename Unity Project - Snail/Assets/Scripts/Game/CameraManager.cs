using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private void Start()
    {
        PlaceCamera();
        ScaleCamera();
    }

    void PlaceCamera()
    {
       transform.position = new Vector3(MapBuilder.centerPosition.x, MapBuilder.centerPosition.y, transform.position.z);
    }

    void ScaleCamera()
    {
        Vector2 leftUpperCorner = Vector2.zero;
        Vector2 mapSize = MapBuilder.size;
        Vector3 rightBottomCorner = new Vector3(mapSize.x, -mapSize.y, 0);
        float distance = Vector2.Distance(leftUpperCorner, rightBottomCorner);
        GetComponent<Camera>().orthographicSize =  (distance)/ Mathf.Sqrt(8);
    }
}
