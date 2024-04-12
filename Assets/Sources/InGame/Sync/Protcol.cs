using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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

    public static long TransformSerialize(Vector3 position, Quaternion rotation) {
        short px, py, pz;
        (px,py,pz)  = PositionSerialize(position);
        byte rotate = RotationSerialize(rotation);
        return ((long)rotate << 48) | ((long)px << 32) | ((long)py << 16) | (long)pz;
    }

    public static (Vector3, Quaternion) TransformDeserialize(long trans) {
        byte rotate = (byte) ((trans >> 48) & 0xFF);
        short px = (short) ((trans >> 32) & 0x_FFFF);
        short py = (short) ((trans >> 16) & 0x_FFFF);
        short pz = (short) (trans & 0x_FFFF);
        Vector3 position = PositionDeserialise(px , py, pz);
        Quaternion rotation = RotationDeserialize(rotate);
        return (position, rotation);
    }
}
