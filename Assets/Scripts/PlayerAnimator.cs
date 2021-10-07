using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerAnimator
{ 
    public static class Params
    {
        public const string VelocityX = "VelocityX";
        public const string VelocityY = "VelocityY";
        public const string Hit = "Hit";
    }

    public static class States
    {
        public const string Idle = "Idle";
        public const string Run = "Run";
        public const string Hit = "Hit";
        public const string Jump = "Jump";
        public const string Fall = "Fall";
    }
}
