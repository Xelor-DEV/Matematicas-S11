using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeController : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody _compRigidBody;
    private void Awake()
    {
        _compRigidBody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        _compRigidBody.velocity = new Vector3(-speed, 0, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Eliminator"))
        {
            Destroy(this.gameObject);
        }
    }
}
