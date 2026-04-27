using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGenerator : MonoBehaviour
{
    // Unity properties to view in Unity inspector GUI
    [Header("Tilemaps")]
    [SerializeField] private Tilemap floorTilemap;
    [SerializeField] private Tilemap wallTilemap;

    [Header("Tiles")]
    [SerializeField] private TileBase floorTile;
    [SerializeField] private TileBase wallTile;

    [Header("Map Settings")]
    [SerializeField] private int mapWidth = 80;
    [SerializeField] private int mapHeight = 50;

    [Header("Room Settings")]
    [SerializeField] private int sectionColumns = 4;
    [SerializeField] private int sectionRows = 3;
    [SerializeField] private int minRoomWidth = 6;
    [SerializeField] private int minRoomHeight = 6;
    [SerializeField] private int maxRoomWidth = 14;
    [SerializeField] private int maxRoomHeight = 10;

    [Header("Path Settings")]
    [SerializeField] private int randomWalkSteps = 200;
    
    // Important Declarations
    private HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
    private List<Room> rooms = new List<Room>();

    private void Start()
    {
        GenerateDungeon();
    }

    public void GenerateDungeon()
    {
        ClearDungeon();

        CreateRoomsWithPartitioning();
        ConnectRoomsWithRandomWalk();
        CreateWalls();

        PaintTiles();
    }

    // Removes all tiles from the tilemap
    private void ClearDungeon()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();

        floorPositions.Clear();
        rooms.Clear();
    }

    // Creates the rooms with a partitioning algorithm
    private void CreateRoomsWithPartitioning()
    {
        int sectionWidth = mapWidth / sectionColumns;
        int sectionHeight = mapHeight / sectionRows;

        for (int x = 0; x < sectionColumns; x++)
        {
            for (int y = 0; y < sectionRows; y++)
            {
                int sectionX = x * sectionWidth;
                int sectionY = y * sectionHeight;

                int roomWidth = Random.Range(minRoomWidth, maxRoomWidth + 1);
                int roomHeight = Random.Range(minRoomHeight, maxRoomHeight + 1);

                roomWidth = Mathf.Min(roomWidth, sectionWidth - 2);
                roomHeight = Mathf.Min(roomHeight, sectionHeight - 2);

                int roomX = Random.Range(sectionX + 1, sectionX + sectionWidth - roomWidth - 1);
                int roomY = Random.Range(sectionY + 1, sectionY + sectionHeight - roomHeight - 1);

                Room newRoom = new Room(roomX, roomY, roomWidth, roomHeight);
                rooms.Add(newRoom);

                CarveRoom(newRoom);
            }
        }
    }
    
    // Used to cut a rectangular room from the map and marks it as a walkable floor
    private void CarveRoom(Room room)
    {
        for (int x = room.xMin; x < room.xMax; x++)
        {
            for (int y = room.yMin; y < room.yMax; y++)
            {
                floorPositions.Add(new Vector2Int(x, y));
            }
        }
    }

    // Connect the rooms with random walk algorithm
    private void ConnectRoomsWithRandomWalk()
    {
        if (rooms.Count <= 1)
        {
            return;
        }

        for (int i = 0; i < rooms.Count - 1; i++)
        {
            Vector2Int start = rooms[i].Center;
            Vector2Int end = rooms[i + 1].Center;

            RandomWalkPath(start, end);
        }
    }
    // Random Walk algorithm
    private void RandomWalkPath(Vector2Int start, Vector2Int end)
    {
        Vector2Int currentPosition = start;

        for (int i = 0; i < randomWalkSteps; i++)
        {
            floorPositions.Add(currentPosition);

            if (currentPosition == end)
            {
                break;
            }

            Vector2Int direction = GetRandomDirectionTowardTarget(currentPosition, end);
            currentPosition += direction;

            currentPosition.x = Mathf.Clamp(currentPosition.x, 1, mapWidth - 2);
            currentPosition.y = Mathf.Clamp(currentPosition.y, 1, mapHeight - 2);
        }
    }
    // Create a random direction towards different rooms
    private Vector2Int GetRandomDirectionTowardTarget(Vector2Int current, Vector2Int target)
    {
        List<Vector2Int> possibleDirections = new List<Vector2Int>();

        if (target.x > current.x)
        {
            possibleDirections.Add(Vector2Int.right);
        }
        else if (target.x < current.x)
        {
            possibleDirections.Add(Vector2Int.left);
        }

        if (target.y > current.y)
        {
            possibleDirections.Add(Vector2Int.up);
        }
        else if (target.y < current.y)
        {
            possibleDirections.Add(Vector2Int.down);
        }

        // Randomize paths.
        possibleDirections.Add(Vector2Int.up);
        possibleDirections.Add(Vector2Int.down);
        possibleDirections.Add(Vector2Int.left);
        possibleDirections.Add(Vector2Int.right);

        return possibleDirections[Random.Range(0, possibleDirections.Count)];
    }

    // Fills map edges with walls
    private void CreateWalls()
    {
        foreach (Vector2Int floorPosition in floorPositions)
        {
            foreach (Vector2Int direction in GetCardinalAndDiagonalDirections())
            {
                Vector2Int neighborPosition = floorPosition + direction;

                if (!floorPositions.Contains(neighborPosition))
                {
                    wallTilemap.SetTile(
                        new Vector3Int(neighborPosition.x, neighborPosition.y, 0),
                        wallTile
                    );
                }
            }
        }
    }

    // Render the tiles into the tilemap
    private void PaintTiles()
    {
        foreach (Vector2Int floorPosition in floorPositions)
        {
            floorTilemap.SetTile(
                new Vector3Int(floorPosition.x, floorPosition.y, 0),
                floorTile
            );
        }
    }

    // Find all 8 directions around a tile
    private List<Vector2Int> GetCardinalAndDiagonalDirections()
    {
        return new List<Vector2Int>
        {
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right,

            new Vector2Int(1, 1),
            new Vector2Int(1, -1),
            new Vector2Int(-1, 1),
            new Vector2Int(-1, -1)
        };
    }
}

// View in Unity inspector GUI
[System.Serializable]
public class Room
{
    // More Declarations. Errors may start with these declarations.
    public int xMin;
    public int yMin;
    public int width;
    public int height;

    public int xMax => xMin + width;
    public int yMax => yMin + height;

    // Find the centerpoint of the rectangular room
    public Vector2Int Center
    {
        get
        {
            int centerX = xMin + width / 2;
            int centerY = yMin + height / 2;
            return new Vector2Int(centerX, centerY);
        }
    }

    public Room(int x, int y, int width, int height)
    {
        this.xMin = x;
        this.yMin = y;
        this.width = width;
        this.height = height;
    }
}