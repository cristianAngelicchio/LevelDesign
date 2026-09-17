using Platformer.Mechanics;
using UnityEngine;

public class RisingWater : MonoBehaviour
{
    public bool isRising = false;

    [Header("Rising Speed")]
    public float risingSpeed = 1f;
    public float doubleSpeedDistance = 5f;
    public float tripleSpeedDistance = 10f;

    [Header("Shake")]
    public float shakeAmount = 0.02f;

    public PlayerController player;

    private Vector3 originalPosition;
    private Vector3 risingPosition;

    void Start()
    {
        originalPosition = transform.position;
        risingPosition = originalPosition;
    }

    void Update()
    {
        if (!isRising)
            return;

        float currentSpeed = risingSpeed;

        if (player != null)
        {
            float distance = player.transform.position.y - transform.position.y;

            if (distance >= tripleSpeedDistance)
            {
                currentSpeed = risingSpeed * 3f;
            }
            else if (distance >= doubleSpeedDistance)
            {
                currentSpeed = risingSpeed * 2f;
            }
        }

        // Rise
        risingPosition += Vector3.up * currentSpeed * Time.deltaTime;

        // Small shake
        float shakeX = Random.Range(-shakeAmount, shakeAmount);
        float shakeY = Random.Range(-shakeAmount, shakeAmount);

        transform.position = risingPosition + new Vector3(shakeX, shakeY, 0f);
    }

    public void StartRising()
    {
        isRising = true;
    }

    public void ResetWater()
    {
        risingPosition = originalPosition;
        transform.position = originalPosition;
    }
}