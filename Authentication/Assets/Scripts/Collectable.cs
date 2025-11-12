using UnityEngine;

public class Collectable : MonoBehaviour
{
    public enum PickupType
    {
        None,
        Speed,
        Jump,
    }

    public PickupType type;

    private float speedBoost=(float) 1.1;
    private float jumpBoost=(float) 1.2;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (type== PickupType.Speed)
                player.speed *= speedBoost;
            else if (type== PickupType.Jump)
                player.jumpPower *= jumpBoost;
        }
    }
}
