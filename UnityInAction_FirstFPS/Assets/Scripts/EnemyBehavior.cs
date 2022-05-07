using UnityEngine;


public class EnemyBehavior : MonoBehaviour
{
    protected bool Alive = true;
    public void Kill()
    {
        Alive = false;
    }
}
