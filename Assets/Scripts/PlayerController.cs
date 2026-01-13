using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private UIHandler uiHandler;
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;

    [SerializeField] private float jumpForce = 600.0f;
    //[SerializeField] private float gravityModifier = 2.0f;
    private int jumpCount;

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
        //Physics.gravity *= gravityModifier;
        Physics.gravity = new Vector3(0, -19.62f, 0);
        jumpCount = 0;        
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
            GameOver(1, explosionParticle, crashSound);   
        }
        else if(collision.gameObject.CompareTag("Bomb"))
        {
            Destroy(collision.gameObject); 
            GameOver(2, fireworkParticle, bombSound); 
        }
        else if(collision.gameObject.CompareTag("Money"))
        {
            Destroy(collision.gameObject);  
            playerAudio.PlayOneShot(moneySound, 1.0f);
            uiHandler.AddDollar(1);   
        }
    }

    private void GameOver(int deathType, ParticleSystem particle, AudioClip sound)
    {
        particle.Play();  
        playerAudio.PlayOneShot(sound, 1.0f);
        playerAnim.SetBool("Death_b", true);
        playerAnim.SetInteger("DeathType_int", deathType);
        MainManager.Instance.isGameActive = false;
        MainManager.Instance.SaveScore();
    }
}