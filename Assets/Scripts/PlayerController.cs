using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    public float mouseSensitivity = 2f;

    private CharacterController controller;
    private Vector3 velocity;
    private float turnVelocity;
    private float normalWalkSpeed;
    private float normalRunSpeed;

    public bool canMove = true;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        normalWalkSpeed = walkSpeed;
        normalRunSpeed = runSpeed;
        canMove = true;
    }

    void Update()
    {
        if (!canMove) return;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0f, mouseX, 0f);

        if (direction.magnitude >= 0.1f)
        {
            Vector3 moveDir = transform.forward * direction.z + transform.right * direction.x;
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            controller.Move(moveDir.normalized * (isRunning ? runSpeed : walkSpeed) * Time.deltaTime);
        }

        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void ApplySlow(float duration)
    {
        StartCoroutine(FreezeCoroutine(duration));
    }

    private IEnumerator FreezeCoroutine(float duration)
    {
        walkSpeed = 0f;
        runSpeed = 0f;

        UIManager.Instance.ShowTrapMessage("Ви потрапили в пастку: сповільнення на 5 сек!");

        for (int i = (int)duration; i > 0; i--)
        {
            UIManager.Instance.ShowSlowTimer(i);
            yield return new WaitForSeconds(1f);
        }
        UIManager.Instance.HideSlowTimer();
        UIManager.Instance.HideTrapMessage();

        walkSpeed = normalWalkSpeed;
        runSpeed = normalRunSpeed;
    }

    public void ResetVerticalVelocity()
    {
        velocity.y = 0f;
    }
}
