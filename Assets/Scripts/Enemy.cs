using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private List<Material> _materials;
    
    private void Start()
    {
        var mesh = GetComponent<MeshRenderer>();
        
        for (var i = 0; i < mesh.materials.Length; i++)
        {
            mesh.materials[i] = _materials[Random.Range(0, _materials.Count)];
        }
    }
}