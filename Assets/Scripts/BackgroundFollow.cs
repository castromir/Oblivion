using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    public Transform cameraTransform;

    private void LateUpdate()
    {
        if (cameraTransform != null)
        {
            transform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y, transform.position.z);
        }
    }
}
