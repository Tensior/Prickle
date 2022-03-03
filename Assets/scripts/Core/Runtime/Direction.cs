using System;
using UnityEngine;

namespace Core
{
    public enum Direction : short
    {
        None = 0,
        Right = 1,
        Left = -1
    }

    public static class DirectionExtensions
    {
        public static Vector2 ToVector2(this Direction direction)
        {
            return direction switch
            {
                Direction.None => Vector2.zero,
                Direction.Right => Vector2.right,
                Direction.Left => Vector2.left,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }
    }
}