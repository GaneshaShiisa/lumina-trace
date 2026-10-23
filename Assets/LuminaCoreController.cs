using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class LuminaCoreController : MonoBehaviour
{
    [SerializeField] private ParticleSystem dustParticleSystem;

    private Rigidbody rb;
    private ParticleSystem.ShapeModule particleShape;
    private ParticleSystem.VelocityOverLifetimeModule velocityOverLifetime;

    private Vector3 direction;
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
        if (camera == null)
        {
            Debug.LogError("Error: MainCamera not found in active scene.");
        }

        direction = transform.forward;
        rb = GetComponent<Rigidbody>();

        velocityOverLifetime = dustParticleSystem.velocityOverLifetime;
    }

    public void OnAttack(InputValue value)
    {
        if (value.isPressed)
        {
            rb.AddForce(direction * 1f, ForceMode.Impulse);
        }
    }

    private void Update()
    {
        Vector3 velocity = rb.linearVelocity;

        // カメラの位置をプレイヤーの後方に設定し、少し上にオフセット
        Vector3 targetPosition = transform.position - (transform.forward * 5.0f) - velocity + (Vector3.up * 2.0f);
        camera.transform.position = targetPosition;
        camera.transform.LookAt(transform.position + Vector3.up * 1.0f);

        // 速度に応じてパーティクルの速度を調整
        velocityOverLifetime.xMultiplier = -velocity.x;
        velocityOverLifetime.yMultiplier = -velocity.y;
        velocityOverLifetime.zMultiplier = -velocity.z;
    }
}