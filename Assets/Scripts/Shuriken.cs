using UnityEngine;

public class Shuriken : MonoBehaviour
{
    private Rigidbody rb;

    public delegate void OnBranchHit();
    public OnBranchHit onBranchHit;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Branch")
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.detectCollisions = false;

            onBranchHit();
        }
    }

    private void OnDestroy()
    {
        onBranchHit = null;
    }

}
