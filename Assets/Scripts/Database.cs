using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Database
{
    [System.Serializable]
    public class CarTransform
    {
        public Vector3 Position;
        public Quaternion Rotation;
    }

    [System.Serializable]
    public class PlayerTransform
    {
        public Vector3 Position;
        public Quaternion Rotation;
    }

    [System.Serializable]
    public class SaveFile
    {
        public CarTransform CarTransform;
        public PlayerTransform PlayerTransform;
    }
}
