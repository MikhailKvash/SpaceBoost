using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
  [SerializeField] private float _endDelay;
  
  [SerializeField] private AudioClip _crashSound;
  [SerializeField] private AudioClip _finishSound;

  private Movement _movement;
  private AudioSource _audioSource;
  private bool _isTransitioning;

  private void Start()
  {
    _movement = GetComponent<Movement>();
    _audioSource = GetComponent<AudioSource>();
  }

  private void OnCollisionEnter(Collision other)
  {
    if(_isTransitioning)
      return;
    
    switch (other.gameObject.tag)
    {
      case "Friendly":
        Debug.Log("Friendly");
        break;
      case "Finish":
        StartSuccessSequence();
        break;
      default:
        StartCrashSequence();
        break;
    }
  }

  private void StartSuccessSequence()
  {
    _isTransitioning = true;
    _audioSource.PlayOneShot(_finishSound);
    _movement.enabled = false;
    Invoke(nameof(LoadNextLevel), _endDelay);
  }

  private void StartCrashSequence()
  {
    _isTransitioning = true;
    _audioSource.PlayOneShot(_crashSound);
    _movement.enabled = false;
    Invoke(nameof(ReloadLevel), _endDelay);
  }

  private void LoadNextLevel()
  {
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    int nextSceneIndex = currentSceneIndex + 1;
    
    if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
    {
      nextSceneIndex = 0;
    }
    SceneManager.LoadScene(nextSceneIndex);
  }

  private void ReloadLevel()
  {
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentSceneIndex);
  }
}
