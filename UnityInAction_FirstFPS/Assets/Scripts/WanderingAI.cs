using UnityEngine;

public class WanderingAI : EnemyBehavior
{
    public float speed = 3.0f;
    public float obstacleRange = .5f;

    // Update is called once per frame
    void Update()
    {
        if (!Alive)
        {
            return;
        }

        transform.Translate(0, 0, speed * Time.deltaTime);
        var ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.SphereCast(ray, 0.75f, out hit))
        {
            if (hit.distance < obstacleRange)
            {
                var angle = Random.Range(-110f, 110f);
                transform.Rotate(0, angle, 0);
            }
        }
    }
}
