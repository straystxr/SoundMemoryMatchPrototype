using UnityEngine;

public class ShakeAnimation : MonoBehaviour
{
    public float shakeAmount = 15f;
    public float shakeSpeed = 10f;

    private float initialPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPosition = transform.localEulerAngles.z; ;
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;
        transform.localEulerAngles = new Vector3(0, 0, initialPosition + angle);
    }
}
