using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell : MonoBehaviour
{
    [SerializeField]
    private GameObject _leftWall;

    [SerializeField]
    private GameObject _rightWall;

    [SerializeField]
    private GameObject _frontWall;

    [SerializeField]
    private GameObject _rearWall;

    [SerializeField]
    private GameObject _unvisitedBlock;

    public bool IsVisited { get; private set; }

    public int X;
    public int Z;

    public void Visit()
    {
        IsVisited = true;
        _unvisitedBlock.SetActive(false);
    }

    public void ClearLeftWall()
    {
        _leftWall.SetActive(false);
    }

    public void ClearRightWall()
    {
        _rightWall.SetActive(false);
    }

    public void ClearFrontWall()
    {
        _frontWall.SetActive(false);
    }

    public void ClearRearWall()
    {
        _rearWall.SetActive(false);
    }


    [SerializeField]
    private GameObject _doorPrefab;  // P�idej prefab pro dve�e

    public void ReplaceLeftWallWithDoor()
    {
        // Skryj ze�
        if (_leftWall != null)
        {
            _leftWall.SetActive(false);  // Skryje levou ze�
        }

        // Vytvo� dve�e
        if (_doorPrefab != null)
        {
            GameObject door = Instantiate(_doorPrefab, transform.position, Quaternion.identity);
            door.transform.SetParent(transform);  // Dve�e budou jako child aktu�ln� bu�ky
            door.transform.localPosition = new Vector3(-0.5f, 0.39f, 0.3f); // Uprav pozici dve��
            door.transform.localRotation = Quaternion.Euler(0, 90, 0);
        }
    }
}
