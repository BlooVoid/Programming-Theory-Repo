using UnityEngine;

public class PlayerController : BaseController
{
    public static PlayerController Instance { get; private set; }

    [Header("Movement Input")]
    [SerializeField] private KeyCode moveUp = KeyCode.W;
    [SerializeField] private KeyCode moveDown = KeyCode.S;
    [SerializeField] private KeyCode moveLeft = KeyCode.A;
    [SerializeField] private KeyCode moveRight = KeyCode.D;

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }

    protected override void Update()
    {
        base.Update();
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector2 inputDirection = Vector2.zero;

        if (Input.GetKey(moveUp))
        {
            inputDirection.y = +1;
        }

        if (Input.GetKey(moveDown))
        {
            inputDirection.y = -1;
        }

        if (Input.GetKey(moveLeft))
        {
            inputDirection.x = -1;
        }

        if (Input.GetKey(moveRight))
        {
            inputDirection.x = +1;
        }

        inputDirection = inputDirection.normalized;

        Vector3 worldPosition = transform.position + MovementSpeed * Time.deltaTime * (Vector3)inputDirection;

        Vector3 viewportPos = Camera.main.WorldToViewportPoint(worldPosition);

        float xPadding = 0.005f;
        float yPadding = 0.025f;

        bool isHorizontalClamped = viewportPos.x <= 0 + xPadding || viewportPos.x >= 1 - xPadding;
        bool isVerticalClamped = viewportPos.y <= 0 + yPadding || viewportPos.y >= 1 - yPadding;

        if (isHorizontalClamped)
        {
            inputDirection.x = 0;
        }

        if (isVerticalClamped)
        {
            inputDirection.y = 0;
        }

        transform.position += MovementSpeed * Time.deltaTime * (Vector3)inputDirection;
    }

    public float GetHealthNormailzed()
    {
        return Mathf.Clamp01(CurrentHealth / MaxHealth);
    }
}
