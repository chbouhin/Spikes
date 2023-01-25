using UnityEngine;

public class WallManager : MonoBehaviour
{
    [SerializeField] private PlayerManager player;

    private void OnCollisionEnter2D(Collision2D col)
    {
        player.ChangeDirection();
    }
}
