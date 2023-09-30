using UnityEngine;

public class PlayerLink : MonoBehaviour
{
    [SerializeField] private GameObject _ropePoint;
    [SerializeField] private GameObject _ropeLink;

    private void Start()
    {
        // Set the position of the rope link to the player
        _ropeLink.transform.position = _ropePoint.transform.position;
    }
}