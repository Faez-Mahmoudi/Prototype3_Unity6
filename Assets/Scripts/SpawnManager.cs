using UnityEngine;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> obstaclePrefabs;
    [SerializeField] private GameObject bombPrefab;
    //[SerializeField] private Vector3 spawnPos = new Vector3(40, 1.5f, 0);
    [SerializeField] private GameObject spawnPos1; 
    [SerializeField] private GameObject spawnPos2; 
    [SerializeField] private GameObject spawnPos3; 
    private PlayerController playerController;
    private float startDelay = 2;
    private float repeatRate = 2;
    private float bombStartDelay = 3;
    private float bombRepeatRate = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        InvokeRepeating("SpawnBomb", bombStartDelay, bombRepeatRate);
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
    private void SpawnBomb()
    {
        if(!playerController.gameOver)
            Instantiate(bombPrefab, spawnPos3.transform.position, gameObject.transform.rotation);
    }
}
