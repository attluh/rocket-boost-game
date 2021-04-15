using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    Rigidbody body;
    AudioSource audioSource;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space)) {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
            body.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void ProcessRotation()
    {
        // Prefer going right
        if (Input.GetKey(KeyCode.A))
        {
            applyRotation(rotationThrust);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            applyRotation(-rotationThrust);
        }
    }

    private void applyRotation(float rotationThisFrame)
    {
        body.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        body.freezeRotation = false;
    }
}
