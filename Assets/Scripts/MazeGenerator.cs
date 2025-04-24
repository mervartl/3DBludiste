using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Linq;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField]
    private MazeCell _mazeCellPrefab;

    [SerializeField]
    private int _mazeWidth;

    [SerializeField]
    private int _mazeDepth;

    [SerializeField]
    private GameObject keyPrefab;

    [SerializeField]
    private GameObject batteryPrefab;

    [SerializeField]
    private int numberOfKeys;

    [SerializeField]
    private int numberOfBatteries;

    public static int keysNeeded;


    private MazeCell[,] _mazeGrid;

    List<MazeCell> availableCells = new List<MazeCell>();

    IEnumerator Start()
    {
        _mazeGrid = new MazeCell[_mazeWidth, _mazeDepth];
        for (int x = 0; x < _mazeWidth; x++)
        {
            for (int z = 0; z < _mazeDepth; z++)
            {
                _mazeGrid[x, z] = Instantiate(_mazeCellPrefab, new Vector3(x * 2, 0, z * 2), Quaternion.identity);
                _mazeGrid[x, z].X = x;
                _mazeGrid[x, z].Z = z;
                availableCells.Add(_mazeGrid[x, z]);
            }
        }

        _mazeGrid[0, 0].ReplaceLeftWallWithDoor();
        availableCells.RemoveAt(0); // Klic ani baterka se nebudou generovat v startovaci bunce
        

        yield return GenerateMaze(null, _mazeGrid[0, 0]);

        GenerateKeys();

        if(numberOfBatteries > 0)
        {
            GenerateBatteries();
        }
    }

    private IEnumerator GenerateMaze(MazeCell previousCell, MazeCell currentCell)
    {
        currentCell.Visit();
        ClearWalls(previousCell, currentCell);

        MazeCell nextCell;

        do
        {
            nextCell = GetNextUnvisitedCell(currentCell);

            if (nextCell != null)
            {
                yield return GenerateMaze(currentCell, nextCell);
            }
        } while (nextCell != null);
    }

    private void GenerateKeys()
    {
        for (int i = 0; i < numberOfKeys; i++)
        {
            if (availableCells.Count > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, availableCells.Count);
                MazeCell randomCell = availableCells[randomIndex];
                availableCells.RemoveAt(randomIndex);  // Odstraní buòku, aby se klíèe neobjevily ve stejné buòce
                Instantiate(keyPrefab, randomCell.transform.position, Quaternion.identity);
            }
        }
    }

    private void GenerateBatteries()
    {
        for (int i = 0; i < numberOfBatteries; i++)
        {
            if (availableCells.Count > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, availableCells.Count);
                MazeCell randomCell = availableCells[randomIndex];
                availableCells.RemoveAt(randomIndex);  // Odstraní buòku, aby se baterky neobjevily ve stejné buòce
                Vector3 spawnPosition = randomCell.transform.position;
                spawnPosition.y = Mathf.Max(spawnPosition.y, 0.15f); // Nastaví souøadnice baterky nad zemí

                Instantiate(batteryPrefab, spawnPosition, Quaternion.identity); // Vytvoøení nové instance objektu
            }
        }
    }


    private MazeCell GetNextUnvisitedCell(MazeCell currentCell)
    {
        var unvisitedCells = GetUnvisitedCells(currentCell);

        return unvisitedCells.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();
    }

    private IEnumerable<MazeCell> GetUnvisitedCells(MazeCell currentCell)
    {
        int x = currentCell.X;
        int z = currentCell.Z;

        foreach (var (dx, dz) in new (int, int)[] { (1, 0), (-1, 0), (0, 1), (0, -1) })
        {
            int newX = x + dx;
            int newZ = z + dz;

            bool isValid = newX >= 0 && newX < _mazeWidth && newZ >= 0 && newZ < _mazeDepth;

            if (!isValid) continue;

            var neighbor = _mazeGrid[newX, newZ];

            if (!neighbor.IsVisited)
            {
                yield return neighbor;
            }
        }
    }

    private void ClearWalls(MazeCell previousCell, MazeCell currentCell)
    {
        if (previousCell == null) return;

        int dx = currentCell.X - previousCell.X;
        int dz = currentCell.Z - previousCell.Z;

        switch ((dx, dz))
        {
            case (1, 0):
                previousCell.ClearRightWall();
                currentCell.ClearLeftWall();
                break;
            case (-1, 0):
                previousCell.ClearLeftWall();
                currentCell.ClearRightWall();
                break;
            case (0, 1):
                previousCell.ClearFrontWall();
                currentCell.ClearRearWall();
                break;
            case (0, -1):
                previousCell.ClearRearWall();
                currentCell.ClearFrontWall();
                break;
        }
    }
}
