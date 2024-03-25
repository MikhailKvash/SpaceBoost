using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
  [SerializeField] private float _endDelay;
  
  [SerializeField] private Movement _movement;
  [SerializeField] private GameObject _visualParts;
  [SerializeField] private AudioClip _crashSound;
  [SerializeField] private AudioClip _finishSound;
  [SerializeField] private ParticleSystem _crashParticle;
  [SerializeField] private ParticleSystem _finishParticle;

  private AudioSource _audioSource;
  private bool _isTransitioning;
  private bool _collisionDisabled;

  private void Start()
  {
    _audioSource = GetComponent<AudioSource>();
  }
  
#if UNITY_EDITOR
  private void Update()
  {
    RespondToDebugKeys();
  }

  private void RespondToDebugKeys()
  {
    if (Input.GetKeyDown(KeyCode.L))
    {
      LoadNextLevel();
    }
    else if (Input.GetKeyDown(KeyCode.C))
    {
      _collisionDisabled = !_collisionDisabled;
    }
  }
#endif

  private void OnCollisionEnter(Collision other)
  {
    if(_isTransitioning || _collisionDisabled)
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
    _audioSource.Stop();
    _audioSource.PlayOneShot(_finishSound);
    _finishParticle.Play();
    _movement.enabled = false;
    _movement.StopParticles();
    Invoke(nameof(LoadNextLevel), _endDelay);
  }

  private void StartCrashSequence()
  {
    _isTransitioning = true;
    _audioSource.Stop();
    _audioSource.PlayOneShot(_crashSound);
    _crashParticle.Play();
    _visualParts.SetActive(false);
    _movement.enabled = false;
    _movement.StopParticles();
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
