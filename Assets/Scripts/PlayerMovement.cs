using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    public bool RotateControls;
    public bool ForceControls;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ForceControls == true)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                rb.AddRelativeForce(Vector3.forward * 5000, ForceMode.Impulse);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                rb.AddRelativeForce(Vector3.back * 5000, ForceMode.Impulse);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                rb.AddRelativeForce(Vector3.left * 5000, ForceMode.Impulse);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                rb.AddRelativeForce(Vector3.right * 5000, ForceMode.Impulse);
            }
        }

        if (RotateControls == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                transform.Rotate(0.0f, 5.0f, 0.0f, Space.Self);
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                transform.Rotate(0.0f, -5.0f, 0.0f, Space.Self);
            }
        }
    }
}
