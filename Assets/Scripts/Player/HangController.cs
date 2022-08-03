using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangController : MonoBehaviour
{
    private Rigidbody rb;
    private ShurikenController shurikenController;
    new private HingeJoint hingeJoint;

    public float accelerationForce = 1000f; //Сила, применяющаяся к игроку, когда он цепляется к ветке

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        shurikenController = GetComponent<ShurikenController>();
    }

    public void StartHanging()
    {
        if (!rb)
            return;

        rb.useGravity = true;

        //Устанавливаем ось вращения
        hingeJoint = gameObject.AddComponent<HingeJoint>();
        hingeJoint.anchor = transform.InverseTransformPoint(shurikenController.GetShuriken().transform.position);
        hingeJoint.axis = Vector3.forward;

        //Добавляем вектор силы, направленный вниз
        rb.AddForce(new Vector3(0, -accelerationForce, 0), ForceMode.Impulse);
    }

    public void StopHanging()
    {
        Destroy(hingeJoint);
    }

    public bool IsHanging()
    {
        if (hingeJoint)
            return true;

        return false;
    }

}
