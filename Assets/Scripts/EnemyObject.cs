using UnityEngine;

[CreateAssetMenu(fileName = "enemyAttributes",
menuName = "Enemy/New Enemy")]
public class EnemyObject : ScriptableObject
{
    public string name;
    public float health;
    public float speed;
    public float attack;

    public Material enemyMaterial;

    private MeshRenderer enemyMesh;
}
