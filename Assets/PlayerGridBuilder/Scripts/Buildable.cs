using UnityEngine;

namespace PlayerGridBuilder
{
    /// <summary>
    /// The type as which this buildable should be treated 
    /// </summary>
    public enum BuildableType
    {
        /// <summary>
        /// Used for wall, fences, railings, and other objects that are placed on grid edges
        /// </summary>
        WALL,
        /// <summary>
        /// Used for ramps, floors, ceilings, and other objects that are placed inside grid cells.
        /// </summary>
        CUBE
    }
    
    /// <summary>
    /// A marker component used to identify objects in the BuildSystem
    /// </summary>
    public class Buildable : MonoBehaviour
    {
        /// <summary>
        /// The type of buildable as which this object should be treated
        /// </summary>
        public BuildableType buildableType;
    }
}