using StarterAssets;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PickUpThrow : MonoBehaviour
{
    public StarterAssetsInputs _input;

    private ThrowableObject currentHeld;
    private Rigidbody currentRB;

    public Transform holdPoint;

    public float throwForce = 10f;
    private bool carrying;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();

    }

    // Update is called once per frame
    void Update()
    {
        if (_input.throwing && carrying && currentHeld != null)
        {
            Debug.Log("Thrown!");
            Throwing();
        }
    }

    void PickUp()
    {
      if (currentHeld == null || currentRB == null) return;

        carrying = true;

        // The RigidBody is: Made Kinematic to turn off collisions, parented to the holdPoint and moved to it.  
        currentRB.isKinematic = true;
        currentHeld.transform.parent = holdPoint.transform;
        currentHeld.transform.position = holdPoint.transform.position;

    }

    void Throwing()
    {
        if (currentHeld == null || currentRB == null) return;

            carrying = false;

        // Kinmatic is turned off to allow collsions again, it's unparented from the holdPoint and a force is added.   
        currentRB.isKinematic = false;
        currentHeld.transform.parent = null;

        currentRB.AddForce(holdPoint.forward * throwForce, ForceMode.Impulse);

        // The public variables are reset for next time an object is picked up.
        currentRB = null;
        currentHeld = null;
    }


    // The box is picked up by the player colliding with it.
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
    // Gets the ThrowableObject script and RigidBody from the player controller colliding with the box.
        ThrowableObject hitHeld = hit.gameObject.GetComponent<ThrowableObject>();
        Rigidbody hitRB = hit.gameObject.GetComponent<Rigidbody>();
        
        if (hitHeld == null ) return;

        // If player is not carrying an object, the ThrowableObject script and RigidBody are assigned to class variables and PickUp() method is called.
        if (!carrying)
        {
            currentHeld = hitHeld;
            currentRB = hitRB;

            PickUp();
        }

        Debug.Log($" Currently held is {currentHeld}");
        
    }
}
