using UnityEngine;

public class Movement : MonoBehaviour
{
  [SerializeField] private float _thrustForce;
  [SerializeField] private float _rotationForce;
  
  [SerializeField] private AudioClip _engineSound;
  [SerializeField] private ParticleSystem _mainThrusterParticle;
  [SerializeField] private ParticleSystem _leftThrusterParticle;
  [SerializeField] private ParticleSystem _rightThrusterParticle;
  
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

  public void StopParticles()
  {
    _mainThrusterParticle.Stop();
    _rightThrusterParticle.Stop();
    _leftThrusterParticle.Stop();
  }

  private void ProcessThrust()
  {
    if (Input.GetKey(KeyCode.Space))
    {
      StartThrusting();
    }
    else if (Input.GetKey(KeyCode.A) == false && Input.GetKey(KeyCode.D) == false)
    {
      StopThrusting();
    }
  }
  
  private void ProcessRotation()
  {
    if (Input.GetKey(KeyCode.A))
    {
      RotateLeft();
    }
    else if (Input.GetKey(KeyCode.D))
    {
      RotateRight();
    }
    else
    {
      StopRotating();
    }

    if (Input.GetKey(KeyCode.Space) == false)
    {
      _mainThrusterParticle.Stop();
    }
  }

  private void StartThrusting()
  {
    _rigidbody.AddRelativeForce(Vector3.up * (_thrustForce * Time.deltaTime));
      
    if (_audioSource.isPlaying == false)
    {
      _audioSource.PlayOneShot(_engineSound);
    }

    if (_mainThrusterParticle.isPlaying == false)
    {
      _mainThrusterParticle.Play();
    }
  }

  private void StopThrusting()
  {
    _audioSource.Stop();
    _mainThrusterParticle.Stop();
  }
  

  private void ApplyRotation(float rotationThisFrame)
  {
    _rigidbody.freezeRotation = true;
    transform.Rotate(Vector3.forward * (rotationThisFrame * Time.deltaTime));
    _rigidbody.freezeRotation = false;
  }

  private void RotateLeft()
  {
    ApplyRotation(_rotationForce);
    if (_audioSource.isPlaying == false)
    {
      _audioSource.PlayOneShot(_engineSound);
    }
    
    if (_rightThrusterParticle.isPlaying == false)
    {
      _rightThrusterParticle.Play();
    }
  }

  private void RotateRight()
  {
    ApplyRotation(-_rotationForce);
    if (_audioSource.isPlaying == false)
    {
      _audioSource.PlayOneShot(_engineSound);
    }
    
    if (_leftThrusterParticle.isPlaying == false)
    {
      _leftThrusterParticle.Play();
    }
  }

  private void StopRotating()
  {
    _rightThrusterParticle.Stop();
    _leftThrusterParticle.Stop();
    
    if(Input.GetKey(KeyCode.Space) == false)
    {
      _audioSource.Stop();
    }
  }
}
