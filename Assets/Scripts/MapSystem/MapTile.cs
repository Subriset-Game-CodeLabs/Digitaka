using UnityEngine;

public class MapTile : MonoBehaviour
{
    [SerializeField]
    private string _mapName;
    [SerializeField]
    private GameObject _youAreHerePointer;

    public string MapName => _mapName;
    public void ActivePointer()
    {
        _youAreHerePointer.SetActive(true);
    }
    public void DeactivePointer()
    {
        _youAreHerePointer.SetActive(false);
    }
}
