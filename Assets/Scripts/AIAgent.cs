using UnityEngine;
using Pathfinding;
public class AIAgent : MonoBehaviour
{
    AIPath path;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] Transform target;
    private void Start()
    {
        path = GetComponent<AIPath>();
        target = Player.Instance.gameObject.transform;
    }
    private void Update()
    {
        path.maxSpeed = moveSpeed;
        path.destination = target.position;
    }

}
