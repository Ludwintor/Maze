using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeApp.Maze
{
    /// <summary>
    /// Represents 2D vector with X aligned to right and Y aligned to down
    /// </summary>
    public struct Vector2 : IEquatable<Vector2>
    {
        /// <summary>
        /// Shorthand for <c>new Vector2(0, 0)</c>
        /// </summary>
        public static Vector2 Zero => new(0, 0);
        /// <summary>
        /// Shorthand for <c>new Vector2(1, 0)</c>
        /// </summary>
        public static Vector2 Right => new(1, 0);
        /// <summary>
        /// Shorthand for <c>new Vector2(-1, 0)</c>
        /// </summary>
        public static Vector2 Left => new(-1, 0);
        /// <summary>
        /// Shorthand for <c>new Vector2(0, 1)</c>
        /// </summary>
        public static Vector2 Down => new(0, 1);
        /// <summary>
        /// Shorthand for <c>new Vector2(0, -1)</c>
        /// </summary>
        public static Vector2 Up => new(0, -1);
        /// <summary>
        /// Shorthand for <c>new Vector2(1, 1)</c>
        /// </summary>
        public static Vector2 One => new(1, 1);

        public Vector2() : this(0, 0) { }

        public Vector2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Vector2 vector && Equals(vector);
        }

        public bool Equals(Vector2 other)
        {
            return X == other.X && Y == other.Y;
        }

        public static Vector2 operator +(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2(lhs.X + rhs.X, lhs.Y + rhs.Y);
        }

        public static Vector2 operator -(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2(lhs.X - rhs.X, lhs.Y - rhs.Y);
        }

        public static bool operator ==(Vector2 left, Vector2 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vector2 left, Vector2 right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return X ^ Y;
        }
    }
}
