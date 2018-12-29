using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
  public float Sensitivity;
  public Transform FollowObject;
  public Transform Player;
  public Transform LockTest;
  public float LockOffset;
  public float dstFromTarget;
  public float dstFromFeetToCore;
  public Vector2 PitchBounds;
  public bool Locked;
  public AnimationCurve LockMoveCurve;
  public GameObject LockIcon;

  public float RotationSmoothTime;
  public float LockDuration;
  private Timer m_lockTimer;
  private Timer m_unlockTimer;


  Vector3 m_currentRotation;

  float m_yaw;
  float m_pitch;

  void Start()
  {
    if (LockIcon.activeInHierarchy)
    {
      LockIcon.SetActive(false);
    }


    m_lockTimer = new Timer(LockDuration);
    m_lockTimer.OnComplete += LockTimer_OnComplete;

    m_unlockTimer = new Timer(LockDuration);
    m_unlockTimer.OnComplete += UnlockTimer_OnComplete;

  }

 

  void Update()
  {
    if (Input.GetButtonDown("RClick") && !IsLocking())
    {
      if (!Locked)
      {
        InitiateLock();
      }
      else if (Locked)
      {
        DisengageLock();
      }
    }

    m_lockTimer.Loop();
    m_unlockTimer.Loop();
  }


  void LateUpdate()
  {
    m_yaw += Input.GetAxis("RHorizontal") * Sensitivity;
    m_pitch -= Input.GetAxis("RVertical") * Sensitivity;
    m_pitch = Mathf.Clamp(m_pitch, PitchBounds.x, PitchBounds.y);

    //Lock && Rosition
    if (IsLocking())
    {
      Quaternion desiredRotation = Quaternion.LookRotation(LockTest.transform.position - transform.position);
      transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, LockMoveCurve.Evaluate(m_lockTimer.GetNormalizedTime()));
      //m_currentRotation = new Vector3(m_pitch, m_yaw);
    }
    else if (Locked && !isUnlocking())
    {
      transform.LookAt(LockTest);
      //m_currentRotation = new Vector3(m_pitch, m_yaw);
    }
    else
    {
      m_currentRotation = new Vector3(m_pitch, m_yaw);
      transform.eulerAngles = m_currentRotation;
    }

    //Lock & Position
    if (IsLocking())
    {
      Vector3 desiredPosition = (FollowObject.position - transform.forward * dstFromTarget) + (Vector3.up * LockOffset);
      Vector3 currentPosition = Vector3.Lerp(transform.position, desiredPosition, LockMoveCurve.Evaluate(m_lockTimer.GetNormalizedTime()));

      transform.position = currentPosition;
    }
    else if (isUnlocking())
    {
      Vector3 desiredPosition = (FollowObject.position - transform.forward * dstFromTarget);
      Vector3 currentPosition = Vector3.Lerp(transform.position, desiredPosition, LockMoveCurve.Evaluate(m_unlockTimer.GetNormalizedTime()));

      transform.position = currentPosition;
    }
    else if (Locked)
    {
      Vector3 desiredPosition = (FollowObject.position - transform.forward * dstFromTarget) + (Vector3.up * LockOffset);
      transform.position = desiredPosition;
    }
    else
    {
      transform.position = (FollowObject.position - transform.forward * dstFromTarget);
    }

  }

  bool IsLocking()
  {
    return m_lockTimer.timerState == Timer.TimerState.Playing;
  }

  bool isUnlocking()
  {
    return m_unlockTimer.timerState == Timer.TimerState.Playing;
  }

  void InitiateLock()
  {
    m_lockTimer.Start();
    LockIcon.SetActive(true); //will need a delay

  }

  void DisengageLock()
  {
    m_unlockTimer.Start();
    LockIcon.SetActive(false); //will need a delay

    m_yaw = transform.eulerAngles.y;
    m_pitch = transform.eulerAngles.x;
  }


  private void LockTimer_OnComplete()
  {
    ToggleLock();
  }

  private void UnlockTimer_OnComplete()
  {
    ToggleLock();
  }

  void ToggleLock()
  {
    Locked = !Locked;
  }



}