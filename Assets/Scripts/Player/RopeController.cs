using UnityEngine;

public class RopeController : MonoBehaviour
{
    public StretchingRope stretchingRopeInst;
    private GameObject rope;

    public void CreateRope(Transform shurikenTransform)
    {
        rope = Instantiate(stretchingRopeInst.gameObject);
        var stretchingRope = rope.GetComponent<StretchingRope>();
        if (!stretchingRope)
            return;

        stretchingRope.playerTransform = transform;
        stretchingRope.shurikenTransform = shurikenTransform;
    }

    public GameObject GetRope()
    {
        return rope;
    }

    public void DestroyRope()
    {
        Destroy(rope);
    }

}
