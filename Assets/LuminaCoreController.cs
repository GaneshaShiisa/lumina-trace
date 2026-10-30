using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class LuminaCoreController : MonoBehaviour
{
    [SerializeField] private ParticleSystem dustParticleSystem;

    private Rigidbody rb;
    private ParticleSystem.ShapeModule particleShape;
    private ParticleSystem.VelocityOverLifetimeModule velocityOverLifetime;

    private Vector3 direction;
    private Camera camera;

    private class ActiveForce
    {
        public float force;
        public Vector3 direction;
        public float remainingTime;
    }
    private List<ActiveForce> activeForces = new List<ActiveForce>();

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
            AttackAction();
        }
    }

    public void OnAttack()
    {
        AttackAction();
    }

    private void AttackAction()
    {
        Vector3 randomDir = Random.insideUnitSphere.normalized;
        float force = Random.Range(5f, 15f);
        float impactTime = Random.Range(0.5f, 2.0f);

        activeForces.Add(new ActiveForce
        {
            force = force,
            direction = randomDir,
            remainingTime = impactTime,
        });
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

    private void FixedUpdate()
    {
        // Update active forces
        Vector3 totalForce = Vector3.zero;
        for (int i = activeForces.Count - 1; i >= 0; i--)
        {
            ActiveForce activeForce = activeForces[i];
            totalForce += activeForce.direction * activeForce.force;
            activeForce.remainingTime -= Time.fixedDeltaTime;

            if (activeForce.remainingTime <= 0)
            {
                activeForces.RemoveAt(i);
            }
        }
        rb.AddForce(totalForce, ForceMode.Force);
    }
}