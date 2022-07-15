using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float MAX_RAY_DISTANCE = 100f;

    public Camera mainCamera;
    public GameObject spawnObj;

    void Start()
    {
        
    }

    void Update()
    {

        Vector3 screenMousePos = Input.mousePosition;
        Ray rayFromMouse = mainCamera.ScreenPointToRay(screenMousePos);

        Debug.DrawRay(mainCamera.transform.position, rayFromMouse.direction * 100, Color.yellow);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ThrowRope();
        }
    }

    private void ThrowRope()
    {
        Vector3 screenMousePos = Input.mousePosition;
        Ray rayFromMouse = mainCamera.ScreenPointToRay(screenMousePos);

        Debug.DrawRay(mainCamera.transform.position, rayFromMouse.direction *100, Color.yellow);

        RaycastHit rayInfo;
        int layerMask = 1 << LayerMask.NameToLayer("MouseRayBlocker");
        if (Physics.Raycast(rayFromMouse, out rayInfo, MAX_RAY_DISTANCE, layerMask))
        {
            Instantiate(spawnObj, rayInfo.point, Quaternion.identity);
        }

    }

}
