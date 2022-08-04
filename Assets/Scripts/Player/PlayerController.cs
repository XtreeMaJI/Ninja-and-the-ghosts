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

    public bool isIdle = true;
    public Animator characterAnimator;
    public float firstJumpForce = 350f;

    public ThrowAnimationHandler throwAnimHandler;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        ropeController = GetComponent<RopeController>();
        shurikenController = GetComponent<ShurikenController>();
        hangController = GetComponent<HangController>();
        throwAnimHandler.onThrowAnimEnd = ThrowShuriken;
    }

    void Update()
    {
        //Пока не совершим первый прыжок, не можем кидать верёвку
        if ( isIdle )
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                SetAnimationState("isFlying");

                rb.useGravity = true;
                Vector3 jumpDir = new Vector3(1f, 2f, 0f);
                jumpDir.Normalize();
                rb.AddForce(jumpDir * firstJumpForce, ForceMode.Impulse);

                isIdle = false;
            }
            return;
        }

        //Можем кинуть новый сюрикен только если предыдущий уничтожен
        if (Input.GetKeyDown(KeyCode.Mouse0) && !shurikenController.IsShurikenExist())
            SetAnimationState("isThrowing");

        if (Input.GetKeyUp(KeyCode.Mouse0) && hangController.IsHanging())
        {
            hangController.StopHanging();
            shurikenController.DestroyShuriken();
            ropeController.DestroyRope();
            StopCoroutine("DestroyShuriken");
            SetAnimationState("isFlying");
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
            SetAnimationState("isHanging");
        });

        StartCoroutine("DestroyShuriken");
        SetAnimationState("isFlying");
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
        SetAnimationState("isFlying");
    }

    private void DisableAnimationStates()
    {
        characterAnimator.SetBool("isIdle", false);
        characterAnimator.SetBool("isFlying", false);
        characterAnimator.SetBool("isThrowing", false);
        characterAnimator.SetBool("isHanging", false);
    }

    private void SetAnimationState(string state)
    {
        DisableAnimationStates();
        characterAnimator.SetBool(state, true);
    }

}
