using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class LuminaCoreController : MonoBehaviour
{
    [SerializeField] private ParticleSystem dustParticleSystem;
    [SerializeField] private GameObject timeline;

    private Rigidbody rb;
    private ParticleSystem.VelocityOverLifetimeModule velocityOverLifetime;

    private Camera camera;

    private class ActiveForce
    {
        public float force;
        public Vector3 direction;
        public float remainingTime;
    }
    private readonly List<ActiveForce> activeForces = new();

    private float idleTime;

    private void Start()
    {
        camera = Camera.main;
        if (camera == null)
        {
            Debug.LogError("Error: MainCamera not found in active scene.");
        }

        rb = GetComponent<Rigidbody>();

        velocityOverLifetime = dustParticleSystem.velocityOverLifetime;


        idleTime = 0;
    }

    public void OnAttack(InputValue value)
    {
        if (value.isPressed)
        {
            AttackAction();
        }
        idleTime = 0;
    }

    public void OnAttack()
    {
        AttackAction();
    }

    private void AttackAction()
    {
        Vector3 velDir = rb.linearVelocity.normalized;
        Vector3 randomDir =
            (
                -velDir * 0.2f +
                Random.insideUnitSphere * 0.8f
            ).normalized;

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

        // 速度に応じてパーティクルの速度を調整
        velocityOverLifetime.xMultiplier = -velocity.x;
        velocityOverLifetime.yMultiplier = -velocity.y;
        velocityOverLifetime.zMultiplier = -velocity.z;
    }

    private Vector3 cameraVelocity;

    void LateUpdate()
    {
        Vector3 velocity = rb.linearVelocity;

        Vector3 targetPosition =
            transform.position
            - transform.forward * 5.0f
            - velocity
            + Vector3.up * 2.0f;

        camera.transform.position = Vector3.SmoothDamp(
            camera.transform.position,
            targetPosition,
            ref cameraVelocity,
            1.5f
        );

        Quaternion targetRotation = Quaternion.LookRotation(
            transform.position + Vector3.up - camera.transform.position
        );

        camera.transform.rotation = Quaternion.Slerp(
            camera.transform.rotation,
            targetRotation,
            Time.deltaTime * 3.0f
        );
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

        idleTime += Time.fixedDeltaTime;
        if (timeline != null)
        {
            if (idleTime > 5.0f)
            {
                timeline.SetActive(true);
            }
            else
            {
                timeline.SetActive(false);
            }
        }

    }
}