using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip successSFX;
    [SerializeField] AudioClip crashSFX;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other) {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Gotta make it to the finish line, boss.");
                break;
            case "Finish":
                Debug.Log("Grats you hit the landing pad!");
                StartSuccessSequence();
                break;
            // case "Fuel":
            //     Debug.Log("Grats you earned extra fuel.");
            //     break;
            default:
                Debug.Log("Well... it wasn't good what you hit.");
                StartCrashSequence();
                break;
        }
    }

    void ReloadLevel()
    {
        // Determines the active scene and reloads it
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void LoadNextLevel()
    {
        // Determines the next scene and loads it
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }

    void StartCrashSequence()
    {
        // todo: add particle effect upon crash

        // Prevent player from moving on the failed level
        GetComponent<Movement>().enabled = false;

        audioSource.PlayOneShot(crashSFX);

        // After a brief pause, reload the level
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void StartSuccessSequence()
    {
        // Prevent player from moving on the completed level
        GetComponent<Movement>().enabled = false;

        audioSource.PlayOneShot(successSFX);

        // After a brief pause, load the next level
        Invoke("LoadNextLevel", levelLoadDelay);
    }
}
