using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGenerator : MonoBehaviour
{
    public GameObject objInstance;
    public float distanceForGenerate = 50f;
    public float minInterval = 60f;
    public float maxInterval = 60f;
    public float yPos = 0f;
    public float zPos = 0f;
    
    private GameObject playerObj;
    private Vector3 lastObjPos = Vector3.zero;

    private void Start()
    {
        playerObj = Object.FindObjectOfType<PlayerController>().gameObject;
    }

    void Update()
    {
        if (IsShouldGenerateObj())
            GenerateObj();
    }

    void GenerateObj()
    {
        if (!objInstance)
            return;

        float objInterval = Random.Range(minInterval, maxInterval);
        Vector3 objPos = new Vector3(lastObjPos.x + objInterval, yPos, zPos);
        lastObjPos = Instantiate(objInstance, objPos, Quaternion.identity).transform.position;
    }

    bool IsShouldGenerateObj()
    {
        float distance = Mathf.Abs(playerObj.transform.position.x - lastObjPos.x);
        if (distance < distanceForGenerate)
            return true;

        return false;
    }
}
