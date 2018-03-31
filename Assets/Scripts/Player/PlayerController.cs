using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using InControl;

public class PlayerController : MonoBehaviour
{

    public Text hAxis;
    public Text vAxis;
    public Text hAxis2;
    public Text vAxis2;

    [SerializeField]
    public float speed = 3f;

    [SerializeField]
    public float turnSpeed = 3f;

    [SerializeField]
    private Rigidbody rigidbody;
    [SerializeField]
    private float GroundCheckDistance;

    [SerializeField]
    private Transform model;

    private bool IsGrounded;

    private Vector3 GroundNormal;
    private int floorMask;
    private Vector3 movement;
    float camRayLength = 100f;          // The length of the ray from the camera into the scene.

    void Awake()
    {
        // Create a layer mask for the floor layer.
        floorMask = LayerMask.GetMask("Floor");

        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //if (InputManager.ActiveDevice.Action2)
        //{
        //    rigidbody.AddForce(transform.forward * 3);
        //}
        //else
        //{

        // Store the input axes.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // Move the player around the scene.
        Move(h, v);

        // Turn the player to face the mouse cursor.
        //Turning();
        //}
    }

    void Move(float h, float v)
    {

        //Debug.Log($"{h}  {v}");
        // Set the movement vector based on the axis input.
        //movement.Set(h, 0f, v);

        // Normalise the movement vector and make it proportional to the speed per second.
        //movement = movement.normalized * speed * Time.fixedDeltaTime;

        // Move the player to it's current position plus the movement.
        //rigidbody.MovePosition(transform.position + movement);

        var mv = new Vector3(h, 0, v).normalized;
        rigidbody.velocity = mv * speed;// movement;


        /*float dt = Time.fixedDeltaTime;
        Vector3 force = rigidbody.mass * (movement- rigidbody.velocity * dt) / (dt);
        rigidbody.AddForce(force);*/

        if (mv != Vector3.zero)
        {
            model.rotation = Quaternion.LookRotation(mv, Vector3.up);
        }

        //if (mv != Vector3.zero)
        //{
        //    rigidbody.rotation = Quaternion.LookRotation(mv, Vector3.up);
        //    // rigidbody.MoveRotation(Quaternion.LookRotation(mv, Vector3.up));
        //}
        //else
        //{
        //    rigidbody.angularVelocity = Vector3.zero;
        //}
    }

    void Turning()
    {
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        // Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit, camRayLength))
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 playerToMouse = floorHit.point - transform.position;

            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            // Set the player's rotation to this new rotation.
            rigidbody.MoveRotation(newRotation);
        }
    }

    // Use this for initialization
    //    void Start()
    //    {
    //        var mainCameraTransform = Camera.main?.transform ?? this.transform;
    //        rigidbody = GetComponent<Rigidbody>();
    //        Observable.EveryFixedUpdate().Select(_ => Input.GetAxis("Horizontal")).Subscribe(x =>
    //        {
    //#if UNITY_EDITOR
    //            hAxis.text = x.ToString();
    //#endif
    //            /*
    //            float h = Input.GetAxis("Horizontal");
    //            float v = Input.GetAxis("Vertical");

    //            // calculate move direction to pass to character
    //            // calculate camera relative direction to move:
    //            var mainCameraForward = Vector3.Scale(mainCameraTransform.forward, new Vector3(1, 0, 1)).normalized;
    //            var move = v * mainCameraForward + h * mainCameraTransform.right;

    //            if (move.magnitude > 1f) move.Normalize();
    //            move = transform.InverseTransformDirection(move);
    //            CheckGroundStatus();
    //            move = Vector3.ProjectOnPlane(move, GroundNormal);
    //            //m_TurnAmount = Mathf.Atan2(move.x, move.z);
    //            //m_ForwardAmount = move.z;
    //            */
    //            var adsfsd = Vector3.right * Time.deltaTime * x * speed;
    //            rigidbody.MovePosition(transform.position + adsfsd);
    //        });
    //        Observable.EveryUpdate().Select(_ => Input.GetAxis("Vertical")).Subscribe(x =>
    //        {
    //#if UNITY_EDITOR
    //            vAxis.text = x.ToString();
    //#endif
    //            var ggwp = Vector3.forward * Time.deltaTime * x * speed;
    //            //rigidbody.MovePosition(transform.position + ggwp);
    //        });
    //        Observable.EveryUpdate().Select(_ => Input.GetAxis("Axis 3")).Subscribe(x =>
    //        {
    //#if UNITY_EDITOR
    //            hAxis2.text = x.ToString();
    //#endif
    //            var z = Input.GetAxis("Axis 6");
    //            if (z != 0 && x != 0)
    //            {
    //                transform.rotation = Quaternion.LookRotation(new Vector3(x, 0, z));

    //            }
    //        });
    //        Observable.EveryUpdate().Select(_ => Input.GetAxis("Axis 6")).Subscribe(x =>
    //        {
    //#if UNITY_EDITOR
    //            vAxis2.text = x.ToString();
    //#endif
    //        });
    //    }

    void CheckGroundStatus()
    {
        RaycastHit hitInfo;
#if UNITY_EDITOR
        // helper to visualise the ground check ray in the scene view
        Debug.DrawLine(transform.position, transform.position + (Vector3.down * GroundCheckDistance));
#endif
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, GroundCheckDistance))
        {
            GroundNormal = hitInfo.normal;
            IsGrounded = true;
        }
        else
        {
            IsGrounded = false;
            GroundNormal = Vector3.up;
        }
    }

}
