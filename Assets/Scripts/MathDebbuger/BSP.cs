using CustomMath;
using System.Collections.Generic;
using UnityEngine;

public class BSP : MonoBehaviour
{
    private Portal[] portals;
    private Room[] rooms;

    [SerializeField] private Camera cam;
    private Dictionary<Room, List<Portal>> portalsInRoom = new Dictionary<Room, List<Portal>>();

    void Start()
    {
        portals = FindObjectsByType<Portal>(FindObjectsSortMode.None);
        rooms = FindObjectsByType<Room>(FindObjectsSortMode.None);

        SetPortalsPerRoom();
    }

    private void Update()
    {
        Debug.Log(GetPointRoom(cam.transform.position).name);
    }

    private void SetPortalsPerRoom()
    {
        foreach (Portal p in portals)
        {
            if (!portalsInRoom.ContainsKey(p.room1))
                portalsInRoom.Add(p.room1, new List<Portal>());
            portalsInRoom[p.room1].Add(p);

            if (!portalsInRoom.ContainsKey(p.room2))
                portalsInRoom.Add(p.room2, new List<Portal>());
            portalsInRoom[p.room2].Add(p);
        }
    }

    public List<Portal> GetPortalsOfRoom(Room room)
    {
        if (portalsInRoom.TryGetValue(room, out List<Portal> result))
            return result;
        return new List<Portal>();
    }

    public Room GetPointRoom(Vec3 point)
    {
        foreach (Room r in rooms)
        {
            if (r.IsPointInsideRoom(point))
            {
                return r;
            }
        }
        return null;
    }
}
