using UnityEngine;

public class FireBomb : MonoBehaviour
{
    private AudioSource bombAudio;
    [SerializeField] private AudioClip bombSound;
    [SerializeField] private ParticleSystem explosionRubbleParticle;

    void Start()
    {
        bombAudio = GetComponent<AudioSource>();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            explosionRubbleParticle.Play();
            bombAudio.PlayOneShot(bombSound, 1.0f);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
