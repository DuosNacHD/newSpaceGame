using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] spawnedObjects;

    private void Start()
    {
        InvokeRepeating("Spawn",2,3);
    }
    void Spawn()
    {
        int value = Random.Range(0,spawnedObjects.Length - 1);

        Instantiate(spawnedObjects[value], transform.position,Quaternion.identity);
        
    }
}
