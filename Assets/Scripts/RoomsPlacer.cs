using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomsPlacer : MonoBehaviour
{
    public Room[] Rooms;
    public Room StartRoom;
    public Room BossRoom;
    private Room[,] SpawnedRooms;
    private float WidthRoom = 4.6f;
    private float HeightRoom = 3.1f;

    public void Start()
    {
        SpawnedRooms = new Room[22, 22];

        Room Start = Instantiate(StartRoom);
        Start.transform.position = new Vector2(0, 0);
        SpawnedRooms[11, 11] = Start;

        for (int i = 0; i < 8; i++)
        {
            PlaceOneRoom();
        }

        PlaceRoomBoss();
    }

    public void Delete()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Room");

        for (int i = 0; i < 10; i++)
        {
            Destroy(obj[i]);
        }
    }

    void PlaceOneRoom()
    {
        HashSet<Vector2Int> vacantPlaces = new HashSet<Vector2Int>();
        for (int X = 0; X < SpawnedRooms.GetLength(0); X++)
        {
            for (int Y = 0; Y < SpawnedRooms.GetLength(1); Y++)
            {
                if (SpawnedRooms[X, Y] == null)
                {
                    continue;
                }

                int MaxX = SpawnedRooms.GetLength(0) - 1;
                int MaxY = SpawnedRooms.GetLength(1) - 1;

                if (X > 0 && SpawnedRooms[X - 1, Y] == null)
                {
                    vacantPlaces.Add(new Vector2Int(X - 1, Y));
                }

                if (Y > 0 && SpawnedRooms[X, Y - 1] == null)
                {
                    vacantPlaces.Add(new Vector2Int(X, Y - 1));
                }

                if (X < MaxX && SpawnedRooms[X + 1, Y] == null)
                {
                    vacantPlaces.Add(new Vector2Int(X + 1, Y));
                }

                if (Y < MaxY && SpawnedRooms[X, Y + 1] == null)
                {
                    vacantPlaces.Add(new Vector2Int(X, Y + 1));
                }
            }
        }

        Room newRoom = Instantiate(Rooms[Random.Range(0, Rooms.Length)]);
        int limit = 500;
        while (limit-- > 0)
        {
            Vector2Int Position = vacantPlaces.ElementAt(Random.Range(0, vacantPlaces.Count));
            if (DoorInRoom(newRoom, Position))
            {
                newRoom.transform.position = new Vector2((Position.x - 11) * WidthRoom, (Position.y - 11) * HeightRoom);
                SpawnedRooms[Position.x, Position.y] = newRoom;
                break;
            }
        }

    }

    int ManyRoomNull(int X, int Y)
    {
        int many = 0;
        if (X == 0)
        {
            many++;
            if (SpawnedRooms[X + 1, Y] == null) many++;
        }
        else if (X == SpawnedRooms.GetLength(0) - 1)
        {
            many++;
            if (SpawnedRooms[X - 1, Y] == null) many++;
        }
        else if (Y == 0 || Y == SpawnedRooms.GetLength(1) - 1)
        {
            if (SpawnedRooms[X + 1, Y] == null) many++;
            if (SpawnedRooms[X - 1, Y] == null) many++;
        }

        if (Y == 0)
        {
            many++;
            if (SpawnedRooms[X, Y + 1] == null) many++;
        }
        else if (Y == SpawnedRooms.GetLength(1) - 1)
        {
            many++;
            if (SpawnedRooms[X, Y - 1] == null) many++;
        }
        else if (X == 0 || X == SpawnedRooms.GetLength(0) - 1)
        {
            if (SpawnedRooms[X, Y + 1] == null) many++;
            if (SpawnedRooms[X, Y - 1] == null) many++;
        }

        if (X != 0 && Y != 0 && X != SpawnedRooms.GetLength(0) - 1 && Y != SpawnedRooms.GetLength(1) - 1)
        {
            if (SpawnedRooms[X - 1, Y] == null) many++;
            if (SpawnedRooms[X, Y - 1] == null) many++;
            if (SpawnedRooms[X + 1, Y] == null) many++;
            if (SpawnedRooms[X, Y + 1] == null) many++;
        }

        return many;
    }

    void PlaceRoomBoss()
    {
        HashSet<Vector2Int> vacantPlaces = new HashSet<Vector2Int>();
        for (int X = 0; X < SpawnedRooms.GetLength(0); X++)
        {
            for (int Y = 0; Y < SpawnedRooms.GetLength(1); Y++)
            {
                int MaxX = SpawnedRooms.GetLength(0) - 1;
                int MaxY = SpawnedRooms.GetLength(1) - 1;

                if (SpawnedRooms[X, Y] == null && SpawnedRooms[MaxX - X, MaxY - Y] == null)
                {
                    continue;
                }

                if (SpawnedRooms[X, Y] != null)
                {
                    if (X > 0 && SpawnedRooms[X - 1, Y] == null && ManyRoomNull(X - 1, Y) == 3)
                    {
                        vacantPlaces.Add(new Vector2Int(X - 1, Y));
                    }

                    if (Y > 0 && SpawnedRooms[X, Y - 1] == null && ManyRoomNull(X, Y - 1) == 3)
                    {
                        vacantPlaces.Add(new Vector2Int(X, Y - 1));
                    }

                    if (X < MaxX && SpawnedRooms[X + 1, Y] == null && ManyRoomNull(X + 1, Y) == 3)
                    {
                        vacantPlaces.Add(new Vector2Int(X + 1, Y));
                    }

                    if (Y < MaxY && SpawnedRooms[X, Y + 1] == null && ManyRoomNull(X, Y + 1) == 3)
                    {
                        vacantPlaces.Add(new Vector2Int(X, Y + 1));
                    }
                }

                if (SpawnedRooms[MaxX - X, MaxY - Y] != null)
                {
                    if (X > 0 && SpawnedRooms[MaxX - X - 1, MaxY - Y] == null &&
                        ManyRoomNull(MaxX - X - 1, MaxY - Y) == 3)
                    {
                        vacantPlaces.Add(new Vector2Int(MaxX - X - 1, MaxY - Y));
                    }

                    if (Y > 0 && SpawnedRooms[MaxX - X, MaxY - Y - 1] == null &&
                        ManyRoomNull(MaxX - X, MaxY - Y - 1) == 3)
                    {
                        vacantPlaces.Add(new Vector2Int(MaxX - X, MaxY - Y - 1));
                    }

                    if (X < MaxX && SpawnedRooms[MaxX - X + 1, MaxY - Y] == null &&
                        ManyRoomNull(MaxX - X + 1, MaxY - Y) == 3)
                    {
                        vacantPlaces.Add(new Vector2Int(MaxX - X + 1, MaxY - Y));
                    }

                    if (Y < MaxY && SpawnedRooms[MaxX - X, MaxY - Y + 1] == null &&
                        ManyRoomNull(MaxX - X, MaxY - Y + 1) == 3)
                    {
                        vacantPlaces.Add(new Vector2Int(MaxX - X, MaxY - Y + 1));
                    }
                }
            }
        }

        if (vacantPlaces.Count != 0)
        {
            Room newRoom = Instantiate(BossRoom);

            int limit = 500;
            while (limit-- > 0)
            {
                Vector2Int Position = vacantPlaces.ElementAt(Random.Range(0, vacantPlaces.Count));
                if (DoorInRoom(newRoom, Position))
                {
                    newRoom.transform.position =
                        new Vector2((Position.x - 11) * WidthRoom, (Position.y - 11) * HeightRoom);
                    SpawnedRooms[Position.x, Position.y] = newRoom;
                    return;
                }
            }
        }

    }

    private bool DoorInRoom(Room room, Vector2Int vect)
    {
        int MaxX = SpawnedRooms.GetLength(0) - 1;
        int MaxY = SpawnedRooms.GetLength(1) - 1;

        List<Vector2Int> neighbours = new List<Vector2Int>();

        if (room.DoorBoss)
        {
            if (room.DoorBossUp != null && vect.y < MaxY && SpawnedRooms[vect.x, vect.y + 1]?.DoorBossDown != null)
                neighbours.Add(Vector2Int.up);
            if (room.DoorBossDown != null && vect.y > 0 && SpawnedRooms[vect.x, vect.y - 1]?.DoorBossUp != null)
                neighbours.Add(Vector2Int.down);
            if (room.DoorBossRight != null && vect.x < MaxX && SpawnedRooms[vect.x + 1, vect.y]?.DoorBossLeft != null)
                neighbours.Add(Vector2Int.right);
            if (room.DoorBossLeft != null && vect.x > 0 && SpawnedRooms[vect.x - 1, vect.y]?.DoorBossRight != null)
                neighbours.Add(Vector2Int.left);

            if (neighbours.Count == 0) return false;

            Vector2Int SelectedDiverc = neighbours[Random.Range(0, neighbours.Count)];
            Room SelectedRoom = SpawnedRooms[vect.x + SelectedDiverc.x, vect.y + SelectedDiverc.y];

            if (SelectedDiverc == Vector2Int.up)
            {
                room.DoorBossUp.SetActive(true);
                SelectedRoom.DoorBossDown.SetActive(true);
                room.ColliderUp.SetActive(false);
                SelectedRoom.ColliderDown.SetActive(false);
            }
            else if (SelectedDiverc == Vector2Int.down)
            {
                room.DoorBossDown.SetActive(true);
                SelectedRoom.DoorBossUp.SetActive(true);
                room.ColliderDown.SetActive(false);
                SelectedRoom.ColliderUp.SetActive(false);
            }
            else if (SelectedDiverc == Vector2Int.right)
            {
                room.DoorBossRight.SetActive(true);
                SelectedRoom.DoorBossLeft.SetActive(true);
                room.ColliderRight.SetActive(false);
                SelectedRoom.ColliderLeft.SetActive(false);
            }
            else if (SelectedDiverc == Vector2Int.left)
            {
                room.DoorBossLeft.SetActive(true);
                SelectedRoom.DoorBossRight.SetActive(true);
                room.ColliderLeft.SetActive(false);
                SelectedRoom.ColliderRight.SetActive(false);
            }
        }
        else
        {
            if (room.DoorUp != null && vect.y < MaxY && SpawnedRooms[vect.x, vect.y + 1]?.DoorDown != null)
                neighbours.Add(Vector2Int.up);
            if (room.DoorDown != null && vect.y > 0 && SpawnedRooms[vect.x, vect.y - 1]?.DoorUp != null)
                neighbours.Add(Vector2Int.down);
            if (room.DoorRight != null && vect.x < MaxX && SpawnedRooms[vect.x + 1, vect.y]?.DoorLeft != null)
                neighbours.Add(Vector2Int.right);
            if (room.DoorLeft != null && vect.x > 0 && SpawnedRooms[vect.x - 1, vect.y]?.DoorRight != null)
                neighbours.Add(Vector2Int.left);


            if (neighbours.Count == 0) return false;

            Vector2Int SelectedDiverc = neighbours[Random.Range(0, neighbours.Count)];
            Room SelectedRoom = SpawnedRooms[vect.x + SelectedDiverc.x, vect.y + SelectedDiverc.y];

            if (SelectedDiverc == Vector2Int.up)
            {
                room.DoorUp.SetActive(true);
                SelectedRoom.DoorDown.SetActive(true);
                room.ColliderUp.SetActive(false);
                SelectedRoom.ColliderDown.SetActive(false);
            }
            else if (SelectedDiverc == Vector2Int.down)
            {
                room.DoorDown.SetActive(true);
                SelectedRoom.DoorUp.SetActive(true);
                room.ColliderDown.SetActive(false);
                SelectedRoom.ColliderUp.SetActive(false);
            }
            else if (SelectedDiverc == Vector2Int.right)
            {
                room.DoorRight.SetActive(true);
                SelectedRoom.DoorLeft.SetActive(true);
                room.ColliderRight.SetActive(false);
                SelectedRoom.ColliderLeft.SetActive(false);
            }
            else if (SelectedDiverc == Vector2Int.left)
            {
                room.DoorLeft.SetActive(true);
                SelectedRoom.DoorRight.SetActive(true);
                room.ColliderLeft.SetActive(false);
                SelectedRoom.ColliderRight.SetActive(false);
            }
        }

        return true;
    }
}
