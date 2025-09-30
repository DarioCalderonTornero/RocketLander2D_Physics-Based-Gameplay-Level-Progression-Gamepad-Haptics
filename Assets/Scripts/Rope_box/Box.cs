using UnityEngine;

public class Box : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject ropeGameObject;

    public void Interact(Lander lander)
    {
        //Debug.Log("Box trigger");
        Instantiate(ropeGameObject, lander.transform.position, Quaternion.identity, lander.transform);
        Destroy(this.gameObject);
    }
}
