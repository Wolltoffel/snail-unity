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
       transform.position = new Vector3(Map.centerPosition.x, Map.centerPosition.y, transform.position.z);
    }

    void ScaleCamera()
    {
        float zoomLevel = 8;
        Vector2 leftUpperCorner = Vector2.zero;
        Vector3 rightBottomCorner = Map.size;
        float distance = Vector2.Distance(leftUpperCorner, rightBottomCorner);
        GetComponent<Camera>().orthographicSize =  (distance)/ Mathf.Sqrt(zoomLevel);
    }
}
