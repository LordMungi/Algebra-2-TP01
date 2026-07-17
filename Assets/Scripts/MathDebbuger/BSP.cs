using CustomMath;
using System.Collections.Generic;
using UnityEngine;

public class BSP : MonoBehaviour
{
    private Portal[] portals;
    private Room[] rooms;

    [SerializeField] private Camera cam;
    private Dictionary<Room, List<Portal>> portalsInRoom = new Dictionary<Room, List<Portal>>();

    List<Room> visibleRooms = new List<Room>();
    List<Room> visitedRooms = new List<Room>();

    void Start()
    {
        portals = FindObjectsByType<Portal>(FindObjectsSortMode.None);
        rooms = FindObjectsByType<Room>(FindObjectsSortMode.None);

        SetPortalsPerRoom();
    }

    private void Update()
    {
        CullRooms();
        Debug.Log(visibleRooms.Count);
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

    private void CullRooms()
    {
        Plane[] f = GeometryUtility.CalculateFrustumPlanes(cam);
        MyPlane[] frustum = new MyPlane[6];
        for (int i = 0; i < f.Length; i++)
            frustum[i] = f[i];

        visibleRooms = new List<Room>();
        visitedRooms = new List<Room>();

        foreach (Room r in rooms)
            r.HideRoom();

        CheckRoom(GetPointRoom(cam.transform.position), cam.transform.position, frustum);
        
        foreach (Room r in visibleRooms)
            r.ShowRoom();
    }

    private void CheckRoom(Room r, Vec3 camPos, MyPlane[] frustum)
    {
        if (!visitedRooms.Contains(r))
        {
            visibleRooms.Add(r);
            visitedRooms.Add(r);

            foreach (Portal portal in GetPortalsOfRoom(r))
            {
                bool passedPortal = false;

                foreach (Vec3 point in portal.Points)
                {
                    passedPortal = true;
                    
                    foreach (MyPlane plane in frustum)
                    {

                        if (plane.GetSide(point))
                            passedPortal = false;
                    }

                    if (passedPortal)
                    {
                        Room nextRoom = (portal.room1 == r) ? portal.room2 : portal.room1;

                        MyPlane[] newFrustum = new MyPlane[frustum.Length + 4];
                        for (int i = 0; i < frustum.Length; i++)
                            newFrustum[i] = frustum[i];

                        for (int i = 0; i < 4; i++)
                        {
                            Vec3 p1 = portal.Points[i];
                            Vec3 p2 = portal.Points[(i + 1) % 4];

                            Vec3 normal = Vec3.Cross(p1 - camPos, p2 - camPos).normalized;
                            float dist = Vec3.Dot(normal, camPos);

                            MyPlane newPlane = new MyPlane(normal, dist);

                            if (newPlane.GetSide(portal.transform.position))
                            {
                                newPlane.Flip();
                            }

                            newFrustum[frustum.Length + i] = newPlane;
                        }

                        CheckRoom(nextRoom, camPos, newFrustum);
                        break;
                    }
                }
            }
        }
    }
}
