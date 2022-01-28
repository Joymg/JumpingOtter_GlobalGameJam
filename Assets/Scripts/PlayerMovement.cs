using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;


[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{

    [SerializeField,Range(2f,10f)] private float movementSpeed = 5f;


    private Vector3 _playerInput;
    public CharacterController characterController;
    public Transform camera;
    
    public float turnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;
    
    // Update is called once per frame
    void Update()
    {
        _playerInput.x = Input.GetAxisRaw("Horizontal");
        _playerInput.z = Input.GetAxisRaw("Vertical");
        

        

        if (_playerInput.magnitude >= 0.1)
        {
            float targetAngle = Mathf.Atan2(_playerInput.x, _playerInput.z) 
                * Mathf.Rad2Deg + camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(
                transform.eulerAngles.y, targetAngle,ref _turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle,0f);
            
            //Multiplying a Quaternion by a vector gives a vector
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            Debug.Log(moveDir);
            characterController.Move(moveDir.normalized * movementSpeed * Time.deltaTime);

        }
    }
    
    
}
