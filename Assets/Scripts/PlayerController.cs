﻿using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float MAX_RAY_DISTANCE = 100f;

    public Camera mainCamera;
    public GameObject debugHitObj;

    public GameObject shurikenInstance;
    public float shurikenSpeed = 5f;
    public float shurikenRotationSpeed = 1.5f; //Количество оборотов сюрикена в секунду 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ThrowShuriken();
        }
        
    }

    private void ThrowShuriken()
    {
        Vector3 throwDir = GetMouseClickPoint() - transform.position;
        throwDir.Normalize();

        GameObject shuriken = Instantiate(shurikenInstance, transform.position, Quaternion.identity);
        Rigidbody shurikenRb = shuriken.GetComponent<Rigidbody>();
        if (shurikenRb)
        {
            shurikenRb.velocity = throwDir * shurikenSpeed;
            //Устанавливаем скорость вращения сюрикена в радианах
            shurikenRb.maxAngularVelocity = shurikenRotationSpeed * 2 * Mathf.PI;
            shurikenRb.angularVelocity = -Vector3.forward * shurikenRotationSpeed * 2 * Mathf.PI;
        }

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

}
