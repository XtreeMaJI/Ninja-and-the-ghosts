using UnityEngine;

public class TreeGenerator : MonoBehaviour
{
    public float DISTANCE_FOR_GENERATE = 50f;
    public float MIN_TREE_INTERVAL = 60f;
    public float MAX_TREE_INTERVAL = 60f;

    public GameObject treeInstance;
    private GameObject playerObj;
    private GameObject lastTree;

    private void Start()
    {
        playerObj = Object.FindObjectOfType<PlayerController>().gameObject;
        lastTree = Object.FindObjectOfType<InteractebleTree>().gameObject;
    }


    void Update()
    {
        if (IsShouldGenerateTree())
            GenerateTree();
    }

    void GenerateTree()
    {
        if (!treeInstance || !lastTree)
            return;

        float treeInterval = Random.Range(MIN_TREE_INTERVAL, MAX_TREE_INTERVAL);
        Vector3 lastBranchPos = lastTree.transform.position;
        Vector3 branchPos = new Vector3(lastBranchPos.x + treeInterval, lastBranchPos.y, lastBranchPos.z);
        lastTree = Instantiate(treeInstance, branchPos, Quaternion.identity);
    }

    bool IsShouldGenerateTree()
    {
        float distance = Mathf.Abs(playerObj.transform.position.x - lastTree.transform.position.x);
        if (distance < DISTANCE_FOR_GENERATE)
            return true;

        return false;
    }

}
