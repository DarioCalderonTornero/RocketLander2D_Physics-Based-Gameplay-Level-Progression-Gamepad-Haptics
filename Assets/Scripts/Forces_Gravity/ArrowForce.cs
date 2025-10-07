using UnityEngine;

public class ArrowForce : MonoBehaviour
{
    private Vector3 moveDirection;
    private float speed;

    public void Init(Vector3 direction, float arrowSpeed)
    {
        moveDirection = direction.normalized;
        speed = arrowSpeed;
    }

    private void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;
    }

}
