using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangController : MonoBehaviour
{
    private Rigidbody rb;
    private ShurikenController shurikenController;
    new private HingeJoint hingeJoint;

    private RopeController ropeController;
    public GameObject characterObj;

    public float accelerationForce = 1000f; //����, ������������� � ������, ����� �� ��������� � �����

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        shurikenController = GetComponent<ShurikenController>();
        ropeController = GetComponent<RopeController>();
    }

    private void LateUpdate()
    {
        var rope = ropeController.GetRope();
        if (rope)
            characterObj.transform.eulerAngles = rope.transform.eulerAngles;
    }

    public void StartHanging()
    {
        if (!rb)
            return;

        rb.useGravity = true;

        //������������� ��� ��������
        hingeJoint = gameObject.AddComponent<HingeJoint>();
        hingeJoint.anchor = transform.InverseTransformPoint(shurikenController.GetShuriken().transform.position);
        hingeJoint.axis = Vector3.forward;

        //��������� ������ ����, ������������ ����
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
