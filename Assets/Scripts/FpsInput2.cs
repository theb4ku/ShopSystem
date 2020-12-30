using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/Fps Input")]
public class FpsInput2 : MonoBehaviour
{

    public float speed = 6.0f;
    private CharacterController _charController;
    public float gravity = -9.8f;
    // Start is called before the first frame update
    void Start()
    {
        _charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal");
        float deltaZ = Input.GetAxis("Vertical");
        Vector3 move = Vector3.right * deltaX + Vector3.forward * deltaZ;
        move = transform.TransformDirection(move);

        
        _charController.Move(move * speed * Time.deltaTime);

    }
}
