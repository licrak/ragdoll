using UnityEngine;

public class RagdollSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ragdollPrefab;
    [SerializeField] private Transform originalRootBone;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RagdollSetup ragdollSetup = Instantiate(
                ragdollPrefab,
                transform.position,
                transform.rotation).GetComponent<RagdollSetup>();
            ragdollSetup.Setup(originalRootBone);
            Destroy(gameObject);
        }
    }
}
