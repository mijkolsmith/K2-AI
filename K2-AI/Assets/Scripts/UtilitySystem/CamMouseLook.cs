using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMouseLook : MonoBehaviour
{
    Vector2 mouseLook;
    Vector2 smoothV;
    public float sensitivity = 2.0f;
    public float smoothing = 5.0f;

    GameObject character;

    void Start()
    {
        //set the character to the parent of the camera gameobject
        character = transform.parent.gameObject;
    }

    void Update()
    {
        //get the input of the mouse axes
        var mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        //apply a scaling for mouse sensitivity
        mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        
        //add smoothing
        smoothV.x = Mathf.Lerp(smoothV.x, mouseDelta.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, mouseDelta.y, 1f / smoothing);

        //include the new results to update the rotations of the camera and the player
        mouseLook += smoothV;

        //lock the movement upwards and downwards at 90 degrees
        mouseLook.y = Mathf.Clamp(mouseLook.y, -90f, 90f);

        //add the vertical mouse movement to the rotation of the camera
        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);

        //add the horizontal mouse movement to the rotation of the player
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
    }
}
