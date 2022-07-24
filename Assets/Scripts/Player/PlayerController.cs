using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float MAX_RAY_DISTANCE = 100f;

    public Camera mainCamera;

    public GameObject shurikenInstance;
    public float shurikenSpeed = 5f;
    public float shurikenRotationSpeed = 1.5f; //Количество оборотов сюрикена в секунду 

    private Rigidbody rb;

    new private HingeJoint hingeJoint;

    private RopeController ropeController;
    private GameObject shurikenObj;

    public float accelerationForce = 1000f; //Сила, применяющаяся к игроку, когда он цепляется к ветке

    public float shurikenDestroyTime = 1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        ropeController = GetComponent<RopeController>();
    }

    void Update()
    {
        //Можем кинуть новый сюрикен только если отпустили верёвку
        if (Input.GetKeyDown(KeyCode.Mouse0) && ropeController && !hingeJoint && !shurikenObj)
        {
            ThrowShuriken();
            StartCoroutine("DestroyShuriken");
        }

        if (Input.GetKeyUp(KeyCode.Mouse0) && hingeJoint && ropeController && shurikenObj)
        {
            Destroy(hingeJoint);
            ropeController.DestroyRope();
            Destroy(shurikenObj);
            StopCoroutine("DestroyShuriken");
        }

    }

    private void ThrowShuriken()
    {
        Vector3 throwDir = GetMouseClickPoint() - transform.position;
        throwDir.Normalize();

        shurikenObj = Instantiate(shurikenInstance, transform.position, Quaternion.identity);
        if (!shurikenObj)
            return;

        Rigidbody shurikenRb = shurikenObj.GetComponent<Rigidbody>();
        if (!shurikenRb)
            return;

        shurikenRb.velocity = throwDir * shurikenSpeed;
        //Устанавливаем скорость вращения сюрикена в радианах
        shurikenRb.maxAngularVelocity = shurikenRotationSpeed * 2 * Mathf.PI;
        shurikenRb.angularVelocity = -Vector3.forward * shurikenRotationSpeed * 2 * Mathf.PI;

        Shuriken shuriken = shurikenObj.GetComponent<Shuriken>();
        if (!shuriken)
            return;

        shuriken.onBranchHit += delegate ()
        {
            if (!rb)
                return;

            rb.useGravity = true;

            GameObject ropePart = ropeController.CreateFragmentedRope();

            //Устанавливаем ось вращения
            hingeJoint = gameObject.AddComponent<HingeJoint>();
            hingeJoint.connectedBody = ropePart.GetComponent<Rigidbody>();
            hingeJoint.axis = Vector3.forward;

            //Добавляем вектор силы, направленный вниз
            rb.AddForce(new Vector3(0, -accelerationForce, 0), ForceMode.Impulse);

            StopCoroutine("DestroyShuriken");
        };

        //Создаём верёвку и устанавливаем её параметры
        ropeController.StartCreateRope(shurikenObj);

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
        yield return new WaitForSeconds(shurikenDestroyTime);
        if(ropeController && shurikenObj)
        {
            ropeController.DestroyRope();
            Destroy(shurikenObj);
        }
        
    }

}
