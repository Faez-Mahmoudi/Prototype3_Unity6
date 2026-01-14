using UnityEngine;

public class SpinObject : MonoBehaviour
{
    [SerializeField] private float spinSpeed;
    [SerializeField] private int caseOfSpin = 1;

    // Update is called once per frame
    void Update()
    {
        if(caseOfSpin == 1)
        {
            transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
        }
        else if(caseOfSpin == 2)
        {
            transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
        }
    }
}
