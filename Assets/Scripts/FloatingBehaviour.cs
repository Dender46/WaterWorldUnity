using UnityEngine;

public class FloatingBehaviour : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float depthBeforeSubmerged = 1.0f;
    [SerializeField] float displacementAmount = 3.0f;
    [SerializeField] float countOfFloaters = 1.0f;
    [SerializeField] float waterDrag = 0.99f;
    [SerializeField] float waterAngularDrag = 0.5f;

    public bool isSubmerged;

    float displacementMultiplier;

    void FixedUpdate()
    {
        rb.AddForceAtPosition(Physics.gravity / countOfFloaters, transform.position, ForceMode.Acceleration);

        float waveHeight = WaveManager.instance.GetWaveHeight(transform.position.x, transform.position.z);
        if (transform.position.y < waveHeight)
        {
            isSubmerged = true;
            displacementMultiplier = Mathf.Clamp01((waveHeight - transform.position.y) / depthBeforeSubmerged) * displacementAmount;
            rb.AddForceAtPosition(Vector3.up * Mathf.Abs(Physics.gravity.y) * displacementMultiplier, transform.position, ForceMode.Acceleration);
            
            rb.AddForce(displacementMultiplier * -rb.velocity * waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
            rb.AddTorque(displacementMultiplier * -rb.angularVelocity * waterAngularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
        else
        {
            isSubmerged = false;
        }
    }
}
