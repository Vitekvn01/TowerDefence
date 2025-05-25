using UnityEngine;

[CreateAssetMenu(fileName = "SceneData", menuName = "Scene Data", order = 51)]
public class SceneData : ScriptableObject
{
    [SerializeField] private string _name;
    public string Name => _name;



}
