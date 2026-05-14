using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Camera cam;

    const float SPEED = 5;
    const float SENSITIVITY = 10;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        transform.Translate((cam.transform.right * Input.GetAxis("Horizontal") + cam.transform.forward * Input.GetAxis("Vertical")) * SPEED * Time.deltaTime);
        cam.transform.RotateAround(transform.position, transform.up, Input.GetAxis("Mouse X") * SENSITIVITY);
    }
}
