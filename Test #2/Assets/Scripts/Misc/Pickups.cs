using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    public enum PickupType
    {
        Speed = 0,
        Life = 1,
        Score = 2,
        Jump = 3,
    }

    public PickupType currentPickup;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController temp = collision.gameObject.GetComponent<PlayerController>();

            switch (currentPickup)
            {
                case PickupType.Speed:
                    temp.StartSpeedBoostChange();
                    break;
                case PickupType.Life:
                    temp.lives++;
                    break;
                case PickupType.Score:
                    temp.bills++;
                    break;

                case PickupType.Jump:
                    temp.StartJumpForceChange();
                    break;
            }

            Destroy(gameObject);
        }
    }
}