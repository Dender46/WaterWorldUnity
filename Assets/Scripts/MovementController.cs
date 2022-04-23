using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 12.0f;
    [SerializeField] float jumpHeight = 3.0f;
    [SerializeField] LayerMask groundLayerMask; 

    [SerializeField]GameObject groundCheck;
    CharacterController controller;
    PlayerFloatingBehaviour floatingBeh;

    public Vector3 velocity;
    bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        floatingBeh = GetComponent<PlayerFloatingBehaviour>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 moveDir = transform.right * moveX + transform.forward * moveY;
        controller.Move(moveDir * movementSpeed * Time.deltaTime);

        // Yumping
        isGrounded = Physics.CheckSphere(groundCheck.transform.position, 0.4f, groundLayerMask);
        if (isGrounded && velocity.y < 0.0f)
        {
            velocity.y = -2.0f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * Physics.gravity.y);
        }

        // Falling
        if (floatingBeh.isSubmerged)
        {
            velocity.y += floatingBeh.force.y * Time.deltaTime;
        }
        else
        {
            velocity.y += Physics.gravity.y * Time.deltaTime;
        }
        controller.Move(velocity * Time.deltaTime);
    }
}
