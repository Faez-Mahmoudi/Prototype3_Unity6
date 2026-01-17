using UnityEngine;

public class FireBomb : MonoBehaviour
{
    private PlayerController playerController;

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            playerController.ExplosionEffect(collision.gameObject.transform.position);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
