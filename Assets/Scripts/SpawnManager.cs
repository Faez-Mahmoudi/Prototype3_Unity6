using UnityEngine;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> obstaclePrefabs;
    [SerializeField] private List<GameObject> airPrefabs;
    [SerializeField] private GameObject spawnPos1; 
    [SerializeField] private GameObject spawnPos2; 
    [SerializeField] private GameObject spawnPos3; 
    private PlayerController playerController;
    private float startDelay = 2;
    private float repeatRate = 2;
    private float airStartDelay = 3;
    private float airRepeatRate = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        InvokeRepeating("SpawnAir", airStartDelay, airRepeatRate);
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

    // Spawn bomb at spawnPos3
    private void SpawnAir()
    {
        int index = Random.Range(0, airPrefabs.Count);
        if(!playerController.gameOver)
            Instantiate(airPrefabs[index], spawnPos3.transform.position, gameObject.transform.rotation);
    }
}
