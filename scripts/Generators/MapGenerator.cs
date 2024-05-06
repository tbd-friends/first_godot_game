using Godot;
using System;

public partial class MapGenerator : TileMap
{
    [Export] private int MapWidth;
    [Export] private int MapHeight;
    [Export] private int Seed;

    private Vector2 _cellSize;
    private float _width;
    private float _height;
    private int[,] _grid;

    private Random _randomNumberGenerator;

    public override void _Ready()
    {
        _cellSize = TileSet.TileSize;
        _width = MapWidth / _cellSize.X;
        _height = MapHeight / _cellSize.Y;

        _randomNumberGenerator = new Random(Seed);

        InitializeGrid();

        CreateRandomPath();

        SpawnTiles();
    }

    private void InitializeGrid()
    {
        _grid = new int[MapWidth, MapHeight];
    }

    private Vector2 GetRandomDirection()
    {
        var directions = new[]
        {
            new Vector2(0, -1),
            new Vector2(0, 1),
            new Vector2(-1, 0),
            new Vector2(1, 0)
        };

        return directions[_randomNumberGenerator.Next(0, directions.Length)];
    }

    private void CreateRandomPath()
    {
        var maxIterations = 1000;
        var iteration = 0;

        var walker = Vector2.Zero;

        while (iteration < maxIterations)
        {
            var direction = GetRandomDirection();

            if (!IsWithinMapBounds(walker, direction))
            {
                continue;
            }

            walker += direction;

            _grid[(int)walker.X, (int)walker.Y] = 1;

            iteration++;
        }
    }

    private bool IsWithinMapBounds(Vector2 walker, Vector2 direction)
    {
        return (walker.X + direction.X < 0) &&
               (walker.X + direction.X >= MapWidth) &&
               (walker.Y + direction.Y < 0) &&
               (walker.Y + direction.Y > MapHeight);
    }

    private void SpawnTiles()
    {
        for (int x = 0; x < MapWidth; x++)
        {
            for (int y = 0; y < MapHeight; y++)
            {
                if (_grid[x, y] == 1)
                {
                    SetCell(0, new Vector2I(x, y), 0, new Vector2I(1, 3));
                }
            }
        }
    }
}