using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private UIHandler uiHandler;
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;

    [SerializeField] private float jumpForce = 10.0f;
    [SerializeField] private float gravityModifier;
    //public bool gameOver = false;
    private int jumpCount = 0;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip crashSound;
    [SerializeField] private AudioClip bombSound;
    [SerializeField] private AudioClip moneySound;

    [Header("Particles")]
    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private ParticleSystem fireworkParticle;
    [SerializeField] private ParticleSystem dirtParticle;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        uiHandler = GameObject.Find("Canvas").GetComponent<UIHandler>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && jumpCount<2 && MainManager.Instance.isGameActive)
        {
            Jump();
            jumpCount++;
        } 
    }

    private void Jump()
    {
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        playerAnim.SetTrigger("Jump_trig");
        dirtParticle.Stop();
        playerAudio.PlayOneShot(jumpSound, 0.5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
            if(MainManager.Instance.isGameActive)
                dirtParticle.Play();
        }
        else if(collision.gameObject.CompareTag("Obstacle"))
        {
            dirtParticle.Stop();
            explosionParticle.Play();  
            playerAudio.PlayOneShot(crashSound, 1.0f);
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            MainManager.Instance.isGameActive = false;
            Debug.Log("Game Over!");
            
        }
        else if(collision.gameObject.CompareTag("Bomb"))
        {
            //dirtParticle.Stop();
            fireworkParticle.Play();
            Destroy(collision.gameObject);  
            playerAudio.PlayOneShot(bombSound, 1.0f);
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 2);
            MainManager.Instance.isGameActive = false;
            Debug.Log("Game Over!");   
        }
        else if(collision.gameObject.CompareTag("Money"))
        {
            Destroy(collision.gameObject);  
            playerAudio.PlayOneShot(moneySound, 1.0f);   
        }
    }
}
