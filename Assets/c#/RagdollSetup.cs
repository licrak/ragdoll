using UnityEngine;

public class RagdollSetup : MonoBehaviour
{
    [SerializeField] private Transform rootBone;

    public void Setup(Transform originalRootBone)
    {
        CloneTransforms(originalRootBone, rootBone);
        //Explosion(250f,transform.position,10f);
    }
    private void CloneTransforms(Transform root, Transform clone)
    {
        foreach (Transform child in root)
        {
            Transform cloneChild = clone.Find(child.name);
            if (cloneChild != null)
            {
                cloneChild.position = child.position;
                cloneChild.rotation = child.rotation;

                CloneTransforms(child, cloneChild);
            }
        }
    }
 
    private void Explosion(Transform root, float force, Vector3 position, float radius)
    {
        foreach (Transform child in root)
        {
            if (child.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(force, position, radius);
            }
            Explosion(child, force, position, radius);
        }
    }
    public void Explosion(float force, Vector3 position, float radius)
    {
        Explosion(rootBone, force, position, radius);
    }
}