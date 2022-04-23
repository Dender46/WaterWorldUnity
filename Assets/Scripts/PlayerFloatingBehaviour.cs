using UnityEngine;

public class PlayerFloatingBehaviour : MonoBehaviour
{
    // [SerializeField] MovementController controller;
    [SerializeField] float depthBeforeSubmerged = 1.0f;
    [SerializeField] float displacementAmount = 3.0f;

    public bool isSubmerged;
    public Vector3 force;

    float displacementMultiplier;

    void Update()
    {
        if (transform.position.y < 0.0f)
        {
            isSubmerged = true;
            displacementMultiplier = Mathf.Clamp01(-transform.position.y / depthBeforeSubmerged) * displacementAmount;

            force = Vector3.up * Mathf.Abs(Physics.gravity.y) * displacementMultiplier;
        }
        else
        {
            isSubmerged = false;
        }
    }
}
