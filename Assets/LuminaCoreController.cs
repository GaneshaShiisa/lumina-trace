using UnityEngine;

public class LuminaCoreController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private float turnSlerpRate = 5.0f;

    [Header("Interaction Settings")]
    [SerializeField] private LayerMask interactionLayer = -1;

    private Vector3 targetDirection;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        targetDirection = transform.forward;
    }

    private void Update()
    {
        HandleInput();
        MoveAndRotate();
    }

    private void HandleInput()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        //     if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, interactionLayer))
        //     {
        //         SetTargetPoint(hitInfo.point);
        //     }
        //     else
        //     {
        //         // 平面に対するレイキャスト（フォールバック）
        //         Plane plane = new Plane(-mainCamera.transform.forward, transform.position);
        //         if (plane.Raycast(ray, out float enter))
        //         {
        //             SetTargetPoint(ray.GetPoint(enter));
        //         }
        //     }
        // }
    }

    private void SetTargetPoint(Vector3 point)
    {
        Vector3 direction = (point - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            targetDirection = direction;
        }
    }

    private void MoveAndRotate()
    {
        // 旋回制御 (Slerp)
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSlerpRate);

        // 前進移動
        transform.Translate(Vector3.forward * (moveSpeed * Time.deltaTime));
    }

    public void SetMoveSpeed(float speed) => moveSpeed = speed;
    public void SetTurnSlerpRate(float rate) => turnSlerpRate = rate;
}
