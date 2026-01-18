using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private UIHandler uiHandler;
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject tip;
    [SerializeField] private float jumpForce = 600.0f;
    //[SerializeField] private float gravityModifier = 2.0f;
    private int jumpCount;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip crashSound;
    [SerializeField] private AudioClip bombSound;
    [SerializeField] private AudioClip moneySound;
    [SerializeField] private AudioClip fireSound;

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

        if(Input.GetKeyDown(KeyCode.F) && MainManager.Instance.bombAmount > 0 && MainManager.Instance.isGameActive)
        {
            Instantiate(projectilePrefab, tip.transform.position , projectilePrefab.transform.rotation);
            playerAudio.PlayOneShot(fireSound, 1.0f);
            uiHandler.AddBomb(-1);
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
            fireworkParticle.transform.position = gameObject.transform.position;
            GameOver(2, fireworkParticle, bombSound); 
        }
        else if(collision.gameObject.CompareTag("GoodBomb"))
        {
            Destroy(collision.gameObject);
            GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
            if(obstacles != null)
            {
                foreach (var item in obstacles)
                {
                    ExplosionEffect(item.transform.position);
                    Destroy(item);
                }
            } 
        }
        else if(collision.gameObject.CompareTag("FireBomb"))
        {
            Destroy(collision.gameObject);  
            playerAudio.PlayOneShot(moneySound, 1.0f);

            uiHandler.AddBomb(1);
        }
        else if(collision.gameObject.CompareTag("Money") || collision.gameObject.CompareTag("ExtraMoney"))
        {
            Destroy(collision.gameObject);  
            playerAudio.PlayOneShot(moneySound, 1.0f);

            if(collision.gameObject.CompareTag("ExtraMoney"))
                uiHandler.AddDollar(-1);
            else
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

    public void ExplosionEffect(Vector3 explosionPos)
    {
        playerAudio.PlayOneShot(bombSound, 1.0f);
        fireworkParticle.transform.position = explosionPos;
        fireworkParticle.Play();
    }
}