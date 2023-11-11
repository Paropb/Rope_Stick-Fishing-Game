using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGSystem
{
    /// <summary>
	/// Checkpoint class. Will make the player respawn at this point if it dies.
	/// </summary>
	[RequireComponent(typeof(BoxCollider2D))]
    [AddComponentMenu("Robot Game/Spawn/RGCheckpoint")]
    public class RGCheckPoint : MonoBehaviour
    {

    }
}
