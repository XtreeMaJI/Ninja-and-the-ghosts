using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentDestroyer : MonoBehaviour
{
    private List<GameObject> envObjList = new List<GameObject>();
    public float distToDestroy = 100f;
    public float timeBetweenChecks = 2f;
    private GameObject playerObj;

    private void Start()
    {
        StartCoroutine("DestroyFarObj");
        playerObj = Object.FindObjectOfType<PlayerController>().gameObject;
    }

    public void AddToList(GameObject newObj)
    {
        envObjList.Add(newObj);
    }

    private IEnumerator DestroyFarObj()
    {
        yield return new WaitForSeconds(timeBetweenChecks);

        envObjList.RemoveAll(obj => obj == null);

        foreach (var obj in envObjList)
        {
            float playerXPos = playerObj.transform.position.x;
            float objXPos = obj.transform.position.x;
            if (Mathf.Abs(playerXPos - objXPos) > distToDestroy)
                Destroy(obj);
            else
                //Когда дошли до первого объекта слева, который уничтожать не нужно - можем дальше не проверять
                break;
        }

        StartCoroutine("DestroyFarObj");
    }


}
