using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;

    private void OnTriggerEnter(Collider other)
    {
       /* if (other.tag.Equals("Obstacle"))
        {
            gameController.GameOver();
        }*/
        if (other.tag.Equals("Coin"))
        {
            gameController.IncreaseCoinCount();
        }
    }
}
