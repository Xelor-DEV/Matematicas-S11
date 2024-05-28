using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public float upForce = 200f; // Fuerza de impulso hacia arriba
    public float downForce = 200f; // Fuerza de impulso hacia abajo
    private Rigidbody rb; // Referencia al componente Rigidbody del jugador
    private Vector2 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Obtenemos el componente Rigidbody
    }
    public void SetDirection(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }
    void Update()
    {
        if (Input.GetButtonDown("Jump")) // Si el jugador presiona el botón de salto (espacio por defecto)
        {
            rb.AddForce(Vector3.up * upForce); // Aplicamos una fuerza hacia arriba
        }

        if (Input.GetButtonDown("Down")) // Si el jugador presiona el botón de abajo (podrías configurarlo en los Input Settings)
        {
            rb.AddForce(Vector3.down * downForce); // Aplicamos una fuerza hacia abajo
        }

        // Rotación del personaje basada en su momentum
        float angle = Mathf.Lerp(0, 90, rb.velocity.y / 20);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, -angle));
    }
    private void FixedUpdate()
    {
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle")) // Si chocamos con un obstáculo
        {
            rb.AddForce(Vector3.left * 100f); // Aplicamos una fuerza hacia la izquierda
            rb.AddForce(Vector3.up * 50f); // Aplicamos una pequeña fuerza hacia arriba para empujar al pájaro por donde debería pasar
        }
    }
}
