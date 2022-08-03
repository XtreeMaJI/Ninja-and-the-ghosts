using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float MAX_RAY_DISTANCE = 100f;

    public Camera mainCamera;
    private Rigidbody rb;

    public float timeToDestroyShuriken = 1f;

    private RopeController ropeController;
    private ShurikenController shurikenController;
    private HangController hangController;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        ropeController = GetComponent<RopeController>();
        shurikenController = GetComponent<ShurikenController>();
        hangController = GetComponent<HangController>();
    }

    void Update()
    {
        //Можем кинуть новый сюрикен только если предыдущий уничтожен
        if (Input.GetKeyDown(KeyCode.Mouse0) && !shurikenController.IsShurikenExist())
            ThrowShuriken();

        if (Input.GetKeyUp(KeyCode.Mouse0) && hangController.IsHanging())
        {
            hangController.StopHanging();
            shurikenController.DestroyShuriken();
            ropeController.DestroyRope();
            StopCoroutine("DestroyShuriken");
        }

    }

    private void ThrowShuriken()
    {
        Vector3 throwDir = GetMouseClickPoint() - transform.position;
        throwDir.Normalize();

        shurikenController.CreateShuriken(throwDir);
        ropeController.CreateRope(shurikenController.GetShuriken().transform);

        shurikenController.AddCallbackOnBranchHit(delegate ()
        {
            hangController.StartHanging();
            StopCoroutine("DestroyShuriken");
        });

        StartCoroutine("DestroyShuriken");
    }

    //Получаем точку на плоскости MouseRayBlocker, куда кликнули мышкой
    private Vector3 GetMouseClickPoint()
    {
        Vector3 clickPoint = new Vector3(0, 0, 0);

        Vector3 screenMousePos = Input.mousePosition;
        Ray rayToMousePos = mainCamera.ScreenPointToRay(screenMousePos);

        RaycastHit rayInfo;
        int layerMask = 1 << LayerMask.NameToLayer("MouseRayBlocker");
        if (Physics.Raycast(rayToMousePos, out rayInfo, MAX_RAY_DISTANCE, layerMask))
            clickPoint = rayInfo.point;
        
        return clickPoint;
    }

    private IEnumerator DestroyShuriken()
    {
        yield return new WaitForSeconds(timeToDestroyShuriken);
        ropeController.DestroyRope();
        shurikenController.DestroyShuriken();
    }

}
