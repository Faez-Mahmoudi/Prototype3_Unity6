using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] private float speed = 30.0f;
    private float leftBound = -15;

    // Update is called once per frame
    void Update()
    {
        // Stop move left on gameOver
        if(MainManager.Instance.isGameActive)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        // Destroy obstacle that exit bounds
        if(transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
