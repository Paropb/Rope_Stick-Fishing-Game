using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGSystem
{
    public class MGCamera : MonoBehaviour
    {
        private void Update()
        {
            transform.eulerAngles = Vector3.zero;
        }
    }
}
