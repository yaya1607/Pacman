using UnityEngine;

public class CameraScale : MonoBehaviour
{
    public Camera mainCamera;
    public Transform tilemap;
    public float targetSize = 18f;
    public float aspectRatio = 1920f / 1080f;

    private void Start()
    {
        float targetWidth = targetSize * aspectRatio;
        mainCamera.orthographicSize = targetSize;
        mainCamera.aspect = aspectRatio;

        Vector3 cameraPosition = new Vector3(tilemap.position.x, tilemap.position.y +1,tilemap.position.z -20) ;
        mainCamera.transform.position = cameraPosition;
    }
}
