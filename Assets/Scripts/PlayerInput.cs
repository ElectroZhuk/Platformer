using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    public delegate void OnDirectionChanged(float direction);
    public event OnDirectionChanged DirectionChanged;
    public event UnityAction Jumping;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jumping?.Invoke();
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D) == true)
        {
            DirectionChanged?.Invoke(1);
        }
        else if (Input.GetKey(KeyCode.A) == true)
        {
            DirectionChanged?.Invoke(-1);
        }
        else
        {
            DirectionChanged?.Invoke(0);
        }
    }
}
