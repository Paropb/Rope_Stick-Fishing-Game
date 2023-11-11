using System;
public enum AttackTypes
{
    Normal, ClockwiseAttack, CounterClockwiseAttack, ToBeDetermined
}
public enum RotationalDirections
{
    None = 0,
    Clockwise = 1,
    CounterClockwise = 2,
}
public enum CombatInputs
{
    CounterClockwise = 0,
    Clockwise = 1,
    ToBeDetermined = 2
}
public enum EntityTypes
{
    None = 0,
    Player = 1,
    Enemy = 2,
    Level = 3,
    Dasher = 4
}
