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
    public Transform PlayerDestination;
    public Transform SelectedEnemy;
    
    private float Timer;
    private bool IsHolding;
    private Rigidbody Rigidbody;

    void Awake()
    {
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
                AutoAttack.SetTarget(SelectedEnemy);
                if (!KeyboardToMove.IsMoving)
                    PlayerDestination.position = transform.position;
            }
            else
            {
                AutoAttack.ClearTarget();
                if (!IsHolding && !KeyboardToMove.IsMoving)
                    PlayerDestination.position = SelectedEnemy.position;
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
    
    public void ClearSelectedEnemy()
    {
        if (SelectedEnemy != null)
        {
            SelectedEnemy.GetComponent<Enemy>().UnhighlightEnemy();
            SelectedEnemy = null;
        }
        AutoAttack.ClearTarget();
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
    
    public bool InEnemyRange()
    {
        if (Vector3.Distance(transform.position, SelectedEnemy.position) <= GameParameters.PlayerAttackRange) return true;
        return false;
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
}