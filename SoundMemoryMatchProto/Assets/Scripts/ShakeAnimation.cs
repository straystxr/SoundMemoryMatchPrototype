using UnityEngine;

public class ShakeAnimation : MonoBehaviour
{
    //Variables to make the Kalimba asset shake, taking into consideration how fast it should fast and how aggressively
    public float shakeAmount = 15f;
    public float shakeSpeed = 10f;

    //will get the coordinates of the asset to rotate it on the z axis
    private float initialPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPosition = transform.localEulerAngles.z; ;
    }

    // Update is called once per frame
    void Update()
    {
        //the angle will get caculated here and how fast and aggressively it shakes
        float angle = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;
        transform.localEulerAngles = new Vector3(0, 0, initialPosition + angle);
    }
}
