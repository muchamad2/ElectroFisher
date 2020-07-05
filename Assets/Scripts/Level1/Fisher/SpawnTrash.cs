using UnityEngine;

public class SpawnTrash : MonoBehaviour {
    [SerializeField] float xRange = 10;
    [SerializeField] float yRange = 3;
    [SerializeField] int numObjects = 15;

    public GameObject[] objects;

    private void Start() {
        Spawn();
    }

    void Spawn(){
        
        for(int i=0;i<numObjects;i++){
            Vector3 spawnLoc = new Vector3(Random.Range(-xRange,xRange),Random.Range(-yRange,yRange),0);
            int objectPick = Random.Range(1,objects.Length);
            GameObject obj = Instantiate(objects[objectPick],spawnLoc,Quaternion.identity);
            obj.transform.SetParent(this.transform);
        }
    }
}