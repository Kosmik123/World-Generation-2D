using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement2D : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    public Rigidbody2D Rigidbody2D
    {
        get
        {
            if (_rigidbody == null)
                _rigidbody = GetComponent<Rigidbody2D>();
            return _rigidbody;
        }
    }

    [SerializeField]
    private float moveSpeed = 5;

    private void Update()
    {
        Vector2 direction = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical"));

        if (direction.sqrMagnitude > 1)
            direction.Normalize();

        Rigidbody2D.velocity = direction * moveSpeed;
    }
}
