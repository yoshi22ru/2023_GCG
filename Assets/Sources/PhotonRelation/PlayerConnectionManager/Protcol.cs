using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Protcol
{
    public static (short,short,short) PositionSerialize(Vector3 vector3) {
        short vx = (short)Mathf.FloorToInt(vector3.x * 100);
        short vy = (short)Mathf.FloorToInt(vector3.y * 100);
        short vz = (short)Mathf.FloorToInt(vector3.z * 100);
        
        return (vx, vy, vz);
    }

    public static Vector3 PositionDeserialise(short vx, short vy, short vz) {
        return new Vector3(vx/100f, vy/100f, vz/100f);
    }

    public static byte RotationSerialize(Quaternion quaternion) {
        Vector3 vector3 = quaternion.eulerAngles;
        byte rotate = (byte)Mathf.FloorToInt(vector3.y);

        return rotate;
    }

    public static Quaternion RotationDeserialize(byte rotate) {
        return Quaternion.Euler(0f, rotate, 0f);
    }
}
