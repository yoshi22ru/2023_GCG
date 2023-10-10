using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

public class RoomList : IEnumerable<RoomInfo>
{
    private Dictionary<string, RoomInfo> dictionaly = new Dictionary<string, RoomInfo>();
    private (string, RoomInfo, GameObject) buttons;

    public void Update(List<RoomInfo> changedRoomList) {
        foreach (var info in changedRoomList) {
            if (!info.RemovedFromList) {
                dictionaly[info.Name] = info;
            } else {
                dictionaly.Remove(info.Name);
            }
        }
    }

    public void Clear() {
        dictionaly.Clear();
    }

    public bool TryGetRoomInfo(string roomName, out RoomInfo roomInfo) {
        return dictionaly.TryGetValue(roomName, out roomInfo);
    }

    public IEnumerator<RoomInfo> GetEnumerator() {
        foreach (var kvp in dictionaly) {
            yield return kvp.Value;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }
}
