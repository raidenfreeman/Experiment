using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Text hAxis;
    public Text vAxis;
    public Text hAxis2;
    public Text vAxis2;

    [SerializeField] public float speed = 3f;

    [SerializeField] private Rigidbody _rigidbody;

    [SerializeField] private Transform model;

    private Vector3 movement;

    public bool UseMovePosition = false;

    void Awake()
    {
        if (_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
    }

    void FixedUpdate()
    {
        var horizontalRawAxis = Input.GetAxisRaw("Horizontal");
        var verticalRawAxis = Input.GetAxisRaw("Vertical");
        if (UseMovePosition)
        {
            var newPosition = transform.position + new Vector3(horizontalRawAxis * Time.deltaTime * speed,0,
                speed * verticalRawAxis * Time.deltaTime);
            _rigidbody.MovePosition(newPosition);
        }
        else
        {
            // Move the player around the scene.
            Move(horizontalRawAxis, verticalRawAxis);
        }
    }

    void Move(float h, float v)
    {
        var mv = new Vector3(h, 0, v).normalized;
        _rigidbody.velocity = mv * speed; // movement;

        if (mv != Vector3.zero)
        {
            model.rotation = Quaternion.LookRotation(mv, Vector3.up);
        }
    }
}