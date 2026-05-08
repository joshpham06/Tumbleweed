using Pathfinding;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickToMove : MonoBehaviour
{
    public AutoAttack AutoAttack;
    public AIDestinationSetter DestinationSetter;
    public AIPath AIPath;
    public LayerMask GroundLayer;
    public LayerMask EnemyLayer;

    private float Timer;
    private bool IsHolding;
    private Transform PlayerDestination;
    private Transform SelectedEnemy;
    private Rigidbody Rigidbody;

    void Awake()
    {
        PlayerDestination = new GameObject("MoveTarget").transform;
        PlayerDestination.position = transform.position;
        DestinationSetter.target = PlayerDestination;
        AIPath.maxSpeed = GameParameters.PlayerSpeed;
        AutoAttack.SetAttackSpeed(GameParameters.PlayerAttackSpeed);
        AutoAttack.SetDamage(GameParameters.PlayerAttackDamage);
    }

    void OnDestroy()
    {
        if (PlayerDestination != null)
            Destroy(PlayerDestination.gameObject);
    }

    void Update()
    {
        if (SelectedEnemy != null)
        {
            if (InEnemyRange())
            {
                PlayerDestination.position = transform.position; // stop
                AutoAttack.SetTarget(SelectedEnemy);
            }
            else
            {
                AutoAttack.ClearTarget();
                if (!IsHolding)
                    PlayerDestination.position = SelectedEnemy.position; // chase if enemy walked away
            }
        }

        if (!IsHolding) return;

        Timer -= Time.deltaTime;
        if (Timer <= 0f)
        {
            Timer = GameParameters.UpdateInterval;
            TryMove();
        }
    }

    public void OnPointClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsHolding = true;
            Timer = GameParameters.UpdateInterval;
            TryMove();
        }

        if (context.canceled)
            IsHolding = false;
    }

    private void TryMove()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (!Physics.Raycast(ray, out RaycastHit hit, 100f, GroundLayer | EnemyLayer)) return;

        if (hit.collider.CompareTag("Enemy"))
        {
            if (SelectedEnemy != null)
                SelectedEnemy.GetComponent<Enemy>().UnhighlightEnemy();

            SelectedEnemy = hit.collider.transform;
            SelectedEnemy.GetComponent<Enemy>().HighlightEnemy();

            if (!InEnemyRange())
                PlayerDestination.position = SelectedEnemy.position;
            else
            {
                PlayerDestination.position = transform.position;
                AutoAttack.SetTarget(SelectedEnemy);
            }
        }
        else
        {
            if (SelectedEnemy != null)
            {
                SelectedEnemy.GetComponent<Enemy>().UnhighlightEnemy();
                SelectedEnemy = null;
            }

            PlayerDestination.position = hit.point;
            AutoAttack.ClearTarget();
        }
    }

    private bool InEnemyRange()
    {
        if (Vector3.Distance(transform.position, SelectedEnemy.position) <= GameParameters.PlayerAttackRange) return true;
        return false;
    }
}