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

    private void FixedUpdate()
    {
        if (!IsHanging())
            return;

        //�� ��� ��������� ����������� ���� �������� �� ����� �������
        Vector3 shurikenPos = shurikenController.GetShuriken().transform.position;
        Vector3 characterPos = transform.position;
        if (characterPos.y > shurikenPos.y)
        {
            float sign = Mathf.Sign(shurikenPos.x - characterPos.x);
            Vector3 velocityDir = characterObj.transform.TransformVector(sign * Vector3.right);
            float speed = rb.velocity.magnitude;
            rb.velocity = velocityDir * speed;

        }
            
    }

    private void LateUpdate()
    {
        SetCharacterPos();
    }

    public void StartHanging()
    {
        if (!rb)
            return;

        //������������� ��� ��������
        hingeJoint = gameObject.AddComponent<HingeJoint>();
        hingeJoint.anchor = transform.InverseTransformPoint(shurikenController.GetShuriken().transform.position);
        hingeJoint.axis = Vector3.forward;

        SetCharacterPos();

        //��������� ������ ����, ������������ ���� �� ��������� ��� ���������
        Vector3 forceDir = characterObj.transform.TransformVector(Vector3.right);
        Vector3 force = forceDir * accelerationForce;
        rb.AddForce(force, ForceMode.Impulse);

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

    private void SetCharacterPos()
    {
        var rope = ropeController.GetRope();
        if (rope)
            characterObj.transform.eulerAngles = rope.transform.eulerAngles;
    }

}
