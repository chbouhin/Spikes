using UnityEngine;

public class SpikeManager : MonoBehaviour
{
    private PlayerManager player;

    private void Start()
    {
        player = FindObjectOfType<PlayerManager>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        player.Die();
    }
}
