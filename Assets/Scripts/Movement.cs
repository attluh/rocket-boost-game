using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;
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
        if (Input.GetKey(KeyCode.Space))
        {
            Thrusting();
        }
        else
        {
            StopThrust();
        }
    }

    private void ProcessRotation()
    {
        // Prefer going right
        if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else
        {
            StopRotating();
        }
    }

    private void Thrusting()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }

        if (!mainBooster.isPlaying)
        {
            mainBooster.Play();
        }

        body.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
    }

    private void StopThrust()
    {
        audioSource.Stop();
        mainBooster.Stop();
    }

    private void RotateRight()
    {
        applyRotation(-rotationThrust, leftBooster);
    }

    private void RotateLeft()
    {
        applyRotation(rotationThrust, rightBooster);
    }

    private void StopRotating()
    {
        rightBooster.Stop();
        leftBooster.Stop();
    }

    private void applyRotation(float rotationThisFrame, ParticleSystem particlesThisFrame)
    {
        body.freezeRotation = true;
        if (!particlesThisFrame.isPlaying)
        {
            particlesThisFrame.Play();
        }
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        body.freezeRotation = false;
    }
}
