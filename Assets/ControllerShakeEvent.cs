using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class ControllerShakeEvent : MonoBehaviour
{
  int m_downCount;

  [Range(0, 100)]
  public float LeftMotorIntensity;
  [Range(0, 100)]
  public float RightMotorIntensity;
  public float Duration;

  PlayerIndex m_playerIndex;
  Timer m_timer;

  const float INTENSITY_RATIO = 655.35f;

  void Start()
  {
    m_playerIndex = PlayerIndex.One;
    m_timer = new Timer(Duration);
    m_timer.OnComplete += Timer_OnComplete;
  }

  public void Activate(PlayerIndex _playerIndex)
  {
    GamePad.SetVibration(m_playerIndex, LeftMotorIntensity * INTENSITY_RATIO, RightMotorIntensity * INTENSITY_RATIO);
    m_timer.Start();

  }

  void Update()
  {
    m_timer.Loop(Time.deltaTime);

    if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed && m_downCount < 1)
    {
      m_downCount++;
      Activate(PlayerIndex.One);
    }

    if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Released)
    {
      m_downCount = 0;
    }

  }

  private void Timer_OnComplete()
  {
    Stop();
  }

  private void Stop()
  {
    GamePad.SetVibration(m_playerIndex, 0, 0);
  }


 
}
