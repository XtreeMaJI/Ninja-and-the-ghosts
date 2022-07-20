using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public const float MIN_DISTANCE_FOR_GENERATE = 25f;

    public GameObject branchInstance;
    
    public GameObject playerObj;

    private GameObject lastBranch;

    public float branchYPos { get; private set; }

    private void Start()
    {
        lastBranch = FindObjectOfType<Branch>().gameObject;
        branchYPos = lastBranch.transform.position.y;  
    }

    void Update()
    {
        if (ShouldGenerateBranch())
            GenerateBranch();
    }


    void GenerateBranch()
    {
        if (!branchInstance || !lastBranch)
            return;

        float branchesInterval = 25f;
        Vector3 lastBranchPos = lastBranch.transform.position;
        Vector3 branchPos = new Vector3(lastBranchPos.x + branchesInterval, lastBranchPos.y, lastBranchPos.z);
        lastBranch = Instantiate(branchInstance, branchPos, Quaternion.identity);
    }

    bool ShouldGenerateBranch() 
    {
        float distance = Mathf.Abs(playerObj.transform.position.x - lastBranch.transform.position.x);
        if (distance < MIN_DISTANCE_FOR_GENERATE)
            return true;

        return false;
    }

}
