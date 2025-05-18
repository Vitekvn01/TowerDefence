using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemySpawner))]
public class SpawnerEditor : Editor
{
    private SerializedProperty _spawnPoints;
    private EnemySpawner spawner;

    private void OnEnable()
    {
        _spawnPoints = serializedObject.FindProperty("_spawnPoint");
        spawner = (EnemySpawner)target;
    }
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space(10);

        if (GUILayout.Button("Добавить точку спавна"))
        {
            AddSpawnPoint(spawner);
        }

        if (GUILayout.Button("Удалить точки спавна"))
        {
            if (EditorUtility.DisplayDialog("Clear All?", "Удалить все точки спавна?", "Да", "Нет"))
            {
                ClearSpawnPoints(spawner);
            }
        }
    }

    private void AddSpawnPoint(EnemySpawner spawner)
    {
        int index = _spawnPoints.arraySize;

        GameObject newPoint = new GameObject($"SpawnPoint_{index}");
        newPoint.transform.parent = spawner.transform;
        newPoint.transform.position = spawner.transform.position;

        _spawnPoints.arraySize++;
        _spawnPoints.GetArrayElementAtIndex(index).objectReferenceValue = newPoint.transform;
        serializedObject.ApplyModifiedProperties();
    }

    private void ClearSpawnPoints(EnemySpawner spawner)
    {
        for (int i = 0; i < _spawnPoints.arraySize; i++)
        {
            Transform point = _spawnPoints.GetArrayElementAtIndex(i).objectReferenceValue as Transform;
            if (point != null)
            {
                DestroyImmediate(point.gameObject);
            }
        }

        _spawnPoints.ClearArray();
        serializedObject.ApplyModifiedProperties();
    }
}
