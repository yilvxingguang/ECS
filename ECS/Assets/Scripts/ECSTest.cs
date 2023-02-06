using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random; 
public struct Cmoponent : IComponentData
{
}
public struct MoveSpeedComponent : IComponentData
{
    public float MoveSpeed;
} 
public class System : ComponentSystem
{
    struct Group
    {
        public int Length;
    }

     Group group;

    protected override void OnUpdate()
    {
        for (int i = 0; i < group.Length; i++)
        {
            //
        }
    }
}

public class ECSTest : MonoBehaviour
{
    [SerializeField] private Mesh mesh;
    [SerializeField] private Material material;
    void Start()
    {

        var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        // Add entity achetype
        var entityArchetype = entityManager.CreateArchetype(
            typeof(MoveSpeedComponent),
            typeof(Translation),
            typeof(RenderMesh),
            typeof(LocalToWorld),
            typeof(RenderBounds));
        var entityArray = new NativeArray<Entity>(1, Allocator.Temp);
        // Instantiate entities
        entityManager.CreateEntity(entityArchetype, entityArray);
        var entity = entityArray[0];
        entityManager.SetComponentData(entity, new MoveSpeedComponent { MoveSpeed = Random.Range(1f, 2f) });
        entityManager.SetComponentData(entity, new Translation { Value = new float3(Random.Range(-8f, 8f), Random.Range(-5f, 5f), 0) });
        entityManager.SetSharedComponentData(entity, new RenderMesh
        {
            mesh = this.mesh,
            material = this.material
        });
        Debug.Log("Test");

        entityArray.Dispose();
    } 
}
