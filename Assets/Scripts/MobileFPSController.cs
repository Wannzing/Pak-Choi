using UnityEngine;

public class MobileFPSController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float lookSpeed = 2f;
    public float jumpForce = 5f;
    public float gravity = -9.81f;
    public Transform cameraTransform;

    private CharacterController controller;
    private Vector3 velocity;
    private float pitch = 0f;

    public Joystick movementJoystick;
    private Vector2 lookInput;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();
        HandleLookTouch(); // NEW
        Look();
        ApplyGravity();

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y = jumpForce;
        }
    }

    void Move()
    {
        Vector3 move = transform.right * movementJoystick.Horizontal + transform.forward * movementJoystick.Vertical;
        controller.Move(move * moveSpeed * Time.deltaTime);
    }

    void HandleLookTouch()
    {
        lookInput = Vector2.zero; // Reset every frame

        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                // Only respond to right side of screen and moving touch
                if (touch.phase == TouchPhase.Moved && touch.position.x > Screen.width / 2)
                {
                    lookInput = touch.deltaPosition;
                    break;
                }
            }
        }
    }

    void Look()
    {
        if (lookInput == Vector2.zero)
            return;

        float yaw = lookInput.x * lookSpeed * Time.deltaTime;
        float pitchDelta = -lookInput.y * lookSpeed * Time.deltaTime;

        pitch = Mathf.Clamp(pitch + pitchDelta, -90f, 90f);

        transform.Rotate(Vector3.up * yaw);
        cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    void ApplyGravity()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (controller.isGrounded)
        {
            velocity.y = jumpForce;
        }
    }
}