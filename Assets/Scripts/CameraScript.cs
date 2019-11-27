using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private bool doMovement = true;
    public float panSpeed = 30f;
    public float panBorderThickness = 10f;

    public float scrollSpeed = 5f;
    public float minY = 10f;
    public float maxY = 80f;
    void Update()
    {
        //if escape is pressed than disables the game
        if (Input.GetKeyDown(KeyCode.Escape))
            doMovement = !doMovement;

        if (!doMovement)
            return;

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            //speed of camera is indipendant on framerate
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            //speed of camera is indipendant on framerate
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            //speed of camera is indipendant on framerate
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            //speed of camera is indipendant on framerate
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        //scroll view
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        print("scroll");

        Vector3 pos = transform.position;

        pos.y -= scroll *  1000 * scrollSpeed * Time.deltaTime;
        //Clamps the minimum and maximum of scorlling 
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;
    }
}
