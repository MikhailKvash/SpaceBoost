using UnityEngine;

public class Movement : MonoBehaviour
{
  [SerializeField] private float _thrustForce;
  [SerializeField] private float _rotationForce;
  
  private Rigidbody _rigidbody;

  private void Start()
  {
    _rigidbody = GetComponent<Rigidbody>();
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
