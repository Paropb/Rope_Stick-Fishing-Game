using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGSystem
{
    [SelectionBase]
    /// <summary>
    /// This class will pilot the CorgiController component of your character.
    /// This is where you'll implement all of your character's game rules, like jump, dash, shoot, stuff like that.
    /// Animator parameters : Grounded (bool), xSpeed (float), ySpeed (float), 
    /// CollidingLeft (bool), CollidingRight (bool), CollidingBelow (bool), CollidingAbove (bool), Idle (bool)
    /// Random : a random float between 0 and 1, updated every frame, useful to add variance to your state entry transitions for example
    /// RandomConstant : a random int (between 0 and 1000), generated at Start and that'll remain constant for the entire lifetime of this animator, useful to have different characters of the same type 
    /// behave differently
    /// </summary>
    [AddComponentMenu("Robot Game/Character/Core/Character")]
    public class RGCharacter : MonoBehaviour
    {
        /// the possible character types : player controller or AI (controlled by the computer)
        public enum CharacterTypes { Player, AI }
        /// the possible initial facing direction for your character
        public enum FacingDirections { Left, Right }
        /// the possible directions you can force your character to look at after its spawn
        public enum SpawnFacingDirections { Default, Left, Right }

    }
}
