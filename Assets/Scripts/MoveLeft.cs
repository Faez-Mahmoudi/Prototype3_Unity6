using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] private float speed = 20.0f;
    private UIHandler uiHandler;
    private float leftBound = -10;
    private float rightBound = 42;
    private float nextScoreToAddSpeed = 333;

    private void Start()
    {
        uiHandler = GameObject.Find("Canvas").GetComponent<UIHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        // Stop move left on gameOver
        if(MainManager.Instance.isGameActive)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        // Destroy obstacle that exit bounds
        if(transform.position.x < leftBound && !(gameObject.CompareTag("BackGround")))
        {
            Destroy(gameObject);
        }
        if(transform.position.x > rightBound && !(gameObject.CompareTag("BackGround")))
        {
            Destroy(gameObject);
        }

        // Increase speed each 333 scores
        if(uiHandler.score >= nextScoreToAddSpeed)
        {
            if(gameObject.CompareTag("Obstacle") || gameObject.CompareTag("BackGround"))
                speed ++;
            else if(gameObject.CompareTag("Projectile"))
                speed = -5;
            else
                speed += 0.33f;

            nextScoreToAddSpeed += 333;
        }
    }
}
