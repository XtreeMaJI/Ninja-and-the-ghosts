using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    public GameObject ropePartInstance;
    private List<GameObject> ropeList = new List<GameObject>();

    private GameObject shurikenObj;
    private GameObject stretchingRopeObj;

    public StretchingRope stretchingRopeInstance; 

    public void StartCreateRope(GameObject newShurikenObj)
    {
        shurikenObj = newShurikenObj;

        stretchingRopeObj = Instantiate(stretchingRopeInstance.gameObject, transform.position, Quaternion.identity);
        StretchingRope stretchingRope = stretchingRopeObj.GetComponent<StretchingRope>();
        if (!stretchingRope)
            return;

        stretchingRope.playerTransform = transform;
        stretchingRope.shurikenTransform = shurikenObj.transform;

        Shuriken shuriken = shurikenObj.GetComponent<Shuriken>();
        if (!shuriken)
            return;

    }

    public GameObject CreateFragmentedRope()
    {
        float ropeLen = stretchingRopeObj.transform.localScale.y;
        float currentLen = 0f;
        Vector3 ropeAngle = stretchingRopeObj.transform.eulerAngles;

        while (ropeLen > currentLen)
        {
            currentLen += ropePartInstance.transform.localScale.y;
            
            Quaternion newRot = Quaternion.Euler(ropeAngle);

            float createdPercent = currentLen / ropeLen;
            Vector3 newPos = Vector3.Lerp(transform.position, shurikenObj.transform.position, createdPercent);

            GameObject newRopePart = Instantiate(ropePartInstance, newPos, newRot);

            if (ropeList.Count != 0)
            {
                HingeJoint prevRopePartJoint = ropeList[ropeList.Count - 1].GetComponent<HingeJoint>();
                prevRopePartJoint.connectedBody = newRopePart.GetComponent<Rigidbody>();
            }

            ropeList.Add(newRopePart);
            
        }

        HingeJoint hingeJoint = ropeList[ropeList.Count - 1].GetComponent<HingeJoint>();
        hingeJoint.connectedBody = shurikenObj.GetComponent<Rigidbody>();

        Destroy(stretchingRopeObj);

        return ropeList[0];

    }

    public void DestroyRope()
    {
        if (stretchingRopeObj)
            Destroy(stretchingRopeObj);

        foreach(var rope in ropeList)
            Destroy(rope);

        ropeList.Clear();

    }

}
