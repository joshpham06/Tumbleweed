using Pathfinding;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickToMove : MonoBehaviour
{
    public AutoAttack AutoAttack;

    private float Timer;
    private bool IsHolding;
    private AIDestinationSetter DestinationSetter;
    private Transform PlayerDestination;
    private Transform SelectedEnemy;

    void Awake()
    {
        DestinationSetter = GetComponent<AIDestinationSetter>();

        PlayerDestination = new GameObject("MoveTarget").transform;
        DestinationSetter.target = PlayerDestination;
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

        if (!Physics.Raycast(ray, out RaycastHit hit, 100f)) return;

        if (hit.collider.CompareTag("Enemy"))
        {
            SelectedEnemy =  hit.collider.transform;
            
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
            PlayerDestination.position = hit.point;
            AutoAttack.ClearTarget();
            SelectedEnemy = null;
        }
    }

    private bool InEnemyRange()
    {
        if (Vector3.Distance(transform.position, SelectedEnemy.position) <= GameParameters.AttackRange) return true;
        return false;
    }
}