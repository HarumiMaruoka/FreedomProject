using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TestArrow : MonoBehaviour
{
    [SerializeField]
    private float _shootPower;
    [SerializeField]
    private Vector3 _shootAngle = default;

    private Rigidbody _rigidbody = null;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.AddForce(_shootAngle.normalized * _shootPower, ForceMode.Impulse);
    }

    void Update()
    {

    }
}
