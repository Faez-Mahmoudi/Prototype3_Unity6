using UnityEngine;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> obstaclePrefabs;
    //[SerializeField] private Vector3 spawnPos = new Vector3(40, 1.5f, 0);
    [SerializeField] private GameObject spawnPos1; 
    [SerializeField] private GameObject spawnPos2; 
    private PlayerController playerController;
    private float startDelay = 2;
    private float repeatRate = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    
    // Spawn obstacale at spawnPos
    private void SpawnObstacle()
    {
        // Stop obstacle spawning on gameOver
        if(!playerController.gameOver)
        {
            int index = Random.Range(0, obstaclePrefabs.Count);
            if(index < 3)
                Instantiate(obstaclePrefabs[index], spawnPos1.transform.position, gameObject.transform.rotation);
            else
                Instantiate(obstaclePrefabs[index], spawnPos2.transform.position, gameObject.transform.rotation);
        }
    }
}
