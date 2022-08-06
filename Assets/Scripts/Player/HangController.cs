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

    public float accelerationForce = 1000f; //—ила, примен€юща€с€ к игроку, когда он цепл€етс€ к ветке

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        shurikenController = GetComponent<ShurikenController>();
        ropeController = GetComponent<RopeController>();
    }

    private void LateUpdate()
    {
        SetCharacterPos();
    }

    public void StartHanging()
    {
        if (!rb)
            return;

        //”станавливаем ось вращени€
        hingeJoint = gameObject.AddComponent<HingeJoint>();
        hingeJoint.anchor = transform.InverseTransformPoint(shurikenController.GetShuriken().transform.position);
        hingeJoint.axis = Vector3.forward;

        SetCharacterPos();

        //ѕримен€ем вектор силы, направленный вниз по локальной оси персонажа
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
