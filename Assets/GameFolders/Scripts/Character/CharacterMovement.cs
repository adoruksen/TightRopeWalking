using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private void Update()
    {
        MoveForward();
    }
    private void MoveForward()
    {
        transform.position += Vector3.forward * Time.deltaTime * 8f;
    }

    
}
