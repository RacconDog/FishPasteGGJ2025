using UnityEngine;

public class VisionCone : MonoBehaviour
{
    public EnemyController enemy;

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.transform == enemy.player)
            {enemy.isInVisionCone = true;}
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.transform == enemy.player)
            {enemy.isInVisionCone = false;}
    }
}
