using UnityEngine;

public class Shuriken : MonoBehaviour
{
    private Rigidbody rb;

    public delegate void OnBranchHit();
    public OnBranchHit onBranchHit;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Branch")
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.detectCollisions = false;

            audioSource.Play();

            onBranchHit();
        }
    }

    private void OnDestroy()
    {
        onBranchHit = null;
    }

}
