using UnityEngine;

public class ShurikenController : MonoBehaviour
{
    public Shuriken shurikenInstance;
    private GameObject shurikenObj;
    public float shurikenSpeed = 5f;
    public float shurikenRotationSpeed = 1.5f; //Количество оборотов сюрикена в секунду 

    public void CreateShuriken(Vector3 throwDir)
    {
        shurikenObj = Instantiate(shurikenInstance.gameObject, transform.position, Quaternion.identity);
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
    }

    public GameObject GetShuriken()
    {
        return shurikenObj;
    }

    public void DestroyShuriken()
    {
        Destroy(shurikenObj);
    }

    public void AddCallbackOnBranchHit(Shuriken.OnBranchHit onBranchHit)
    {
        shurikenObj.GetComponent<Shuriken>().onBranchHit += onBranchHit;
    }

    public bool IsShurikenExist()
    {
        if (shurikenObj)
            return true;

        return false;
    }

}
