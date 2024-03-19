using UnityEngine;

public class Movement : MonoBehaviour
{
  [SerializeField] private float _thrustForce;
  [SerializeField] private float _rotationForce;
  
  [SerializeField] private AudioClip _engineSound;
  
  private Rigidbody _rigidbody;
  private AudioSource _audioSource;

  private void Start()
  {
    _rigidbody = GetComponent<Rigidbody>();
    _audioSource = GetComponent<AudioSource>();
  }

  private void Update()
  {
    ProcessThrust();
    ProcessRotation();
  }

  private void ProcessThrust()
  {
    if (Input.GetKey(KeyCode.Space))
    {
      _rigidbody.AddRelativeForce(Vector3.up * (_thrustForce * Time.deltaTime));
      
      if (_audioSource.isPlaying != true)
      {
        _audioSource.PlayOneShot(_engineSound);
      }
    }
    else
    {
      _audioSource.Stop();
    }
  }
  
  private void ProcessRotation()
  {
    if (Input.GetKey(KeyCode.A))
    {
      ApplyRotation(_rotationForce);
    }
    else if (Input.GetKey(KeyCode.D))
    {
      ApplyRotation(-_rotationForce);
    }
  }

  private void ApplyRotation(float rotationThisFrame)
  {
    _rigidbody.freezeRotation = true;
    transform.Rotate(Vector3.forward * (rotationThisFrame * Time.deltaTime));
    _rigidbody.freezeRotation = false;
  }
}
