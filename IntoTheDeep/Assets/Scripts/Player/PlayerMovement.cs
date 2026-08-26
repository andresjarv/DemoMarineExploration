using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento Acuático")]
    [SerializeField] private float swimSpeed = 3f;
    [SerializeField] private float sprintSpeed = 6f;
    [SerializeField] private float rotationSpeed = 8f;
    [SerializeField] private float buoyancyForce = 0.5f;

    [Header("Audio Settings")]
    [Tooltip("Drag the Player's AudioSource here")]
    [SerializeField] private AudioSource swimAudioSource;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isUnderwater = true;
    private float targetSpeed;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    void Update()
    {
        // 1. Input Capture
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        targetSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : swimSpeed;

        // 2. Audio Control Logic
        // We check if the magnitude is greater than 0.1 to avoid joystick drift issues
        bool isMoving = moveInput.magnitude > 0.1f;

        if (isMoving && !swimAudioSource.isPlaying)
        {
            swimAudioSource.Play();
        }
        else if (!isMoving && swimAudioSource.isPlaying)
        {
            swimAudioSource.Pause();
        }
    }

    void FixedUpdate()
    {
        // 2. Aplicar físicas
        MovePlayer();
        ApplyBuoyancy();
    }

    private void MovePlayer()
    {
        if (moveInput.sqrMagnitude > 0.01f)
        {
            rb.linearVelocity = moveInput.normalized * targetSpeed;
            float targetAngle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg;
            float newAngle = Mathf.LerpAngle(rb.rotation, targetAngle - 90, rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(newAngle);
        }
        else
        {
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, 5f * Time.fixedDeltaTime);
        }
    }

    private void ApplyBuoyancy()
    {
        if (isUnderwater)
            rb.AddForce(Vector2.up * buoyancyForce, ForceMode2D.Force);
    }
}