using UnityEngine;

public class TreeGenerator : MonoBehaviour
{
    public float distanceForGenerate = 50f;
    public float minTreeInterval = 60f;
    public float maxTreeInterval = 60f;

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

        float treeInterval = Random.Range(minTreeInterval, maxTreeInterval);
        Vector3 lastTreePos = lastTree.transform.position;
        Vector3 treePos = new Vector3(lastTreePos.x + treeInterval, lastTreePos.y, lastTreePos.z);
        lastTree = Instantiate(treeInstance, treePos, Quaternion.identity);
    }

    bool IsShouldGenerateTree()
    {
        float distance = Mathf.Abs(playerObj.transform.position.x - lastTree.transform.position.x);
        if (distance < distanceForGenerate)
            return true;

        return false;
    }

}
