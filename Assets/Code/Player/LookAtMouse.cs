using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Ediosn Li
//Makes object look at Mouse
[RequireComponent(typeof(PlayerInput))]
public class LookAtMouse : MonoBehaviour
{
    //[SerializeField] private Transform pointer;
    [SerializeField] private float speed = 10;

    private UnityEngine.InputSystem.PlayerInput _input;

    private InputAction _mousePosition;

    private Vector3 _direction;

    // Start is called before the first frame update
    void Start()
    {
        // Cache references to components and actions
        _input = GetComponent<UnityEngine.InputSystem.PlayerInput>();
        _mousePosition = _input.actions["Look"]; // Must be the same name as your Mouse Pointer action
    }


    void Update()
    {
        // Move and update pointer
        //transform.Translate(speed * Time.deltaTime * _direction);
        UpdatePointer();
    }

    public void OnMove(InputValue val)
    {
        Vector2 v = val.Get<Vector2>();
        Vector3 direction = v.normalized;

        // Only called when you press or release a key, meaning we have to save this value
        // and move in Update until direction is set to 0 (when you release the key)
        _direction = new Vector3(direction.x, direction.y, 0f);
    }


    // Called continuously for gamepads, but for mouse it only calls when you move the mouse
    // So if we want the mouse to stay stationary, but keep the pointer pointing at it as we move around,
    // we will need to do some extra work (this is what UpdatePointer() is for)
    public void OnLook(InputValue val)
    {
        // Get input action value as a Vector2 (mouse position in screen space)
        var v = val.Get<Vector2>();
        Vector3 direction;

        // Pointer position and direction need to be handled per control scheme, since v represents
        // the mouse position for MK, and the stick direction for gamepad
        if (_input.currentControlScheme == "Keyboard&Mouse")
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(v);
            worldPos.z = 0;
            Vector3 pos = (worldPos - transform.position).normalized; // Place the pointer on a unit circle around the player's position
            pos.z = 0;
            //pointer.localPosition = pos;

            // Point in the same direction as a vector going FROM player TO mouse position (screen space)
            // If we pointed from the pointer's pos to the mouse pos, when the mouse is inside the player
            // the pointer would be pointing inward, but we want it to always point outward. Pointing away
            // from the player towards mouse pos ensures this vector is always facing away from the player.
            direction = (Vector3)v - Camera.main.WorldToScreenPoint(transform.position);
        }
        else
        {
            if (v.sqrMagnitude == 0f) return; // Break if stick is not being pressed, so the pointer doesn't snap to player center
            //pointer.localPosition = v.normalized;
            // Stick input already falls within a unit circle, but normalizing forces it to only be on the edge of that circle
            // So even a slight tilt will put the pointer all the way on the edge of that circle.

            // Direction to point in is from player's position to pointer's position (all in screen space) as before,
            // but we need to do some extra work to get the pointer's screen position, since v in this case does not
            // refer to a screen space position, but a direction within the unit circle
            direction = Camera.main.WorldToScreenPoint(transform.position + (Vector3)v)
                        - Camera.main.WorldToScreenPoint(transform.position);
        }

        // Get the angle around the Z axis to set rotation to in order to point towards the mouse cursor
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void UpdatePointer()
    {
        // Gamepad sends OnLook events continuously when the stick is held, so this is just to
        // update the pointer's to point at the mouse properly when the player is moving, but
        // the mouse is not (as the mouse only sends input events to OnLook when moving)
        if (_input.currentControlScheme != "Keyboard&Mouse") return;

        // Read mouse position from the Look input action we defined
        Vector2 mousePos = _mousePosition.ReadValue<Vector2>();

        // All this is the same as above.
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.z = 0;
        Vector3 pos = (worldPos - transform.position).normalized;
        pos.z = 0;
        //pointer.localPosition = pos;

        Vector3 direction = (Vector3)mousePos - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public Quaternion getRotation()
    {
        return transform.rotation;
    }
    /*
    void CheckInput()
    {
        foreach (InputDevice i in input.devices)
        {
            if (i is Gamepad)
            {
                isGamepad = true;
            }
            else if (i is Mouse)
            {
                isMouse = false;
            }
        }

        if (isGamepad)
        {
            float h = Input.GetAxis("Horizontal") * Time.deltaTime;
            float v = Input.GetAxis("Vertical") * Time.deltaTime;

            Vector3 joystickPos = new Vector3(h, v);
            Vector3 rotation = joystickPos - transform.position;
            float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, rotZ);
        }
        else if (isMouse)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 rotation = mousePosition - transform.position;
            float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, rotZ);
        }
    }
    */
}
