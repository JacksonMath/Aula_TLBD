using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyObject enemySettings;

    [Header ("Atributos")]
    [SerializeField] private string name;
    [SerializeField] private float health;
    [SerializeField] private float speed;
    [SerializeField] private float attack;

    [Header("Visuals")]
    [SerializeField] private Material enemyMaterial;

    private MeshRenderer enemyMesh;

    public void Start()
    {
        enemyMesh = GetComponent<MeshRenderer>();

        name = enemySettings.name;
        health = enemySettings.health;
        speed = enemySettings.speed;
        attack = enemySettings.attack;
        enemyMaterial = enemySettings.enemyMaterial; 
        
        if (enemyMaterial != null )
        enemyMesh.material = enemyMaterial;
    }
}
