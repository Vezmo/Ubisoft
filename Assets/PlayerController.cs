using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public CharacterController Controller;
  public Transform CameraTransform;

  public float Gravity;
  public float RunSpeed;
  public float SprintSpeed;
  public float LeftStickDeadZone;

  private float m_currentSpeed;
  private Vector2 m_leftStickInput;
  private float m_velocityY;
  Vector3 m_velocity;

  public float TurnSmoothTime;
  private float m_turnSmoothVelocity;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    if (!IsGrounded())
      ApplyGravity();
    else
      m_velocityY = 0;

    Move();


  }


  void Move()
  {
    m_leftStickInput.x = Input.GetAxisRaw("LHorizontal");
    m_leftStickInput.y = Input.GetAxisRaw("LVertical");

    if (DirectionalInput())
    {
      if (Input.GetButton("B"))
      {
        m_currentSpeed = m_leftStickInput.magnitude * SprintSpeed;

      }
      else
      {
        m_currentSpeed = m_leftStickInput.magnitude * RunSpeed;
      }

      m_leftStickInput.Normalize();
      float rotation = Mathf.Atan2(m_leftStickInput.x, m_leftStickInput.y) * Mathf.Rad2Deg + CameraTransform.eulerAngles.y;
      transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, rotation, ref m_turnSmoothVelocity, TurnSmoothTime); //We always move forward, but we rotate the character

      m_velocity = transform.forward * m_currentSpeed;
      Controller.Move(m_velocity * Time.deltaTime);
    }
   
  }

  bool IsGrounded()
  {
    if (Controller.isGrounded)
      return true;

    Vector3 bottom = Controller.transform.position;
    RaycastHit hit;

    if (Physics.Raycast(bottom, Vector3.down, out hit, 0.2f))
    {
      Controller.Move(Vector3.down * hit.distance);
      return true;
    }

    return false;
  }

  void ApplyGravity()
  {
    m_velocityY += Time.deltaTime * Gravity;
    Controller.Move(Vector3.up * m_velocityY * Time.deltaTime);
  }

  bool DirectionalInput()
  {
    return (Mathf.Abs(m_leftStickInput.x) >= LeftStickDeadZone || Mathf.Abs(m_leftStickInput.y) >= LeftStickDeadZone);
  }
}
