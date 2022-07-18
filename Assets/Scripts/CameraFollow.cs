using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject playerObj;

    private void LateUpdate()
    {
        if (playerObj == null)
            return;

        Vector3 playerPos = playerObj.transform.position;
        Vector3 cameraPos = transform.position;
        Vector3 newCameraPos = new Vector3(playerPos.x, cameraPos.y, cameraPos.z);
        transform.position = newCameraPos;
    }

}
