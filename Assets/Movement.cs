
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] Vector2 MoveSpeed;
    [SerializeField] CharacterController Controller;
    [SerializeField] Transform TransformCamera;
    Vector2 VectorInput;
    Vector3 VectorMove;
    Vector3 CameraForward;
    Vector3 CameraRight;
    private float TurnSmoothVelocity;
    [SerializeField] private float TurnSmoothTime = 0.1f; // регулирует плавность разворота

    private bool IsGrounded;
    [SerializeField] Transform GroundCheck;
    [SerializeField] float GroundDistance = 0.3f;
    [SerializeField] LayerMask GroundMask;
    private Vector3 Velocity; // воздействие гравитации
    [SerializeField] float Gravity = -9.81f;


    private void FixedUpdate()
    {
        // ѕроверка земли
        IsGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);
        if (IsGrounded && Velocity.y < 0)
        {
            Velocity.y = -2f;
        }


        // ¬ектор движени€ относительно взгл€да камеры
        CameraForward = TransformCamera.forward;
        CameraRight = TransformCamera.right;

        CameraForward.y = 0f;
        CameraRight.y = 0f;

        CameraForward.Normalize();
        CameraRight.Normalize();

        VectorMove = (CameraForward * VectorInput.y * MoveSpeed.y) + (CameraRight * VectorInput.x * MoveSpeed.x);



        // ѕлавный поворот в сторону движени€
        Vector3 DirectionMove = VectorMove;
        DirectionMove.y = 0f;

        if (DirectionMove.sqrMagnitude > 0.01f)
        {
            float TargetAngle = Mathf.Atan2(DirectionMove.x, DirectionMove.z) * Mathf.Rad2Deg;
            float SmoothedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, TargetAngle, ref TurnSmoothVelocity, TurnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, SmoothedAngle, 0f);
        }

        // ѕрименение движени€
        Controller.Move(VectorMove * Time.fixedDeltaTime);

        // √равитаци€
        Velocity.y += Gravity * Time.deltaTime;
        Controller.Move(Velocity * Time.deltaTime);


    }
    public void OnMove(InputValue value)
    {      
        VectorInput = value.Get<Vector2>();
    }
}
