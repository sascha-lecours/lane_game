using UnityEngine;
using System.Collections;

public class SnapToMouseScript : MonoBehaviour
{
    public string snapHotkey = null;
    public string snapHotkey2 = null;
    private Plane dragPlane;

    private Camera myMainCamera;

    void Start()
    {
        myMainCamera = Camera.main; // Camera.main is expensive ; cache it here
    }

    void snapToMousePosition()
    {
        dragPlane = new Plane(myMainCamera.transform.forward, transform.position);
        Ray camRay = myMainCamera.ScreenPointToRay(Input.mousePosition);
       
        float planeDist;
        dragPlane.Raycast(camRay, out planeDist);
        transform.position = camRay.GetPoint(planeDist);
    }

    void Update()
    {
        if (snapHotkey != null)
        {
            if (Input.GetKey(snapHotkey) || Input.GetKey(snapHotkey2))
            {
                snapToMousePosition();
            }
        }
    }
}