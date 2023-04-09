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

    [SerializeField]int zoomLevel;
    void PlaceCamera()
    {
       transform.position = new Vector3(Map.centerPosition.x, Map.centerPosition.y, transform.position.z);
    }

    void ScaleCamera()
    {
        GetComponent<Camera>().orthographicSize =  (Map.size.x * Map.tileSize)/ zoomLevel;
    }
}
