using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    [SerializeField] private float rotationSpeed = 100f;
    private bool isFalling = false;
    private Rigidbody _compRigidBody;
    private void Awake()
    {
        _compRigidBody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        _compRigidBody.AddForce(Vector3.down * gravity);

        if (isFalling == true)
        {
            transform.Rotate(0, 0, rotationSpeed * Time.fixedDeltaTime);
        }
        else
        {
            if (transform.eulerAngles.z < 80 || transform.eulerAngles.z > 270)
            {
                transform.Rotate(0, 0, -rotationSpeed * Time.fixedDeltaTime);
            }
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed == true)
        {
            _compRigidBody.velocity = new Vector3(_compRigidBody.velocity.x, 0, _compRigidBody.velocity.z);
            _compRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isFalling = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PipeDown") || other.CompareTag("PipeUp"))
        {
            _compRigidBody.AddForce(Vector3.left * jumpForce,ForceMode.Impulse);
        }
        else if (other.CompareTag("PlayerEliminator"))
        {
            GameManager.Instance.LoadScene("GameOver");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PipeDown"))
        {
            _compRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            _compRigidBody.AddForce(Vector3.left * jumpForce, ForceMode.Impulse);
        }
        else if (other.CompareTag("PipeUp"))
        {
            _compRigidBody.AddForce(Vector3.down * jumpForce, ForceMode.Impulse);
            _compRigidBody.AddForce(Vector3.left * jumpForce, ForceMode.Impulse);
        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        _compRigidBody.velocity = new Vector3(0, 0, 0);
    }
    private void Update()
    {
        float angle = transform.rotation.eulerAngles.z;
        if (_compRigidBody.velocity.y > 0)
        {
            isFalling = false;
        }
        else if (_compRigidBody.velocity.y < 0)
        {
            isFalling = true;
        }
    }
}