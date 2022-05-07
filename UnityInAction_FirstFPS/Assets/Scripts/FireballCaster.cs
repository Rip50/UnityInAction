using UnityEngine;

public class FireballCaster : EnemyBehavior
{
    [SerializeField] private GameObject _fireballPrefab;
    private GameObject _fireball;

    // Update is called once per frame
    void Update()
    {
        if (!Alive)
        {
            return;
        }

        var ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.SphereCast(ray, 0.75f, out hit))
        {
            var hitObject = hit.transform.gameObject;
            if (hitObject.GetComponent<PlayerCharacter>())
            {
                if(_fireball == null)
                {
                    _fireball = Instantiate(_fireballPrefab);
                    _fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                    _fireball.transform.rotation = transform.rotation;
                }
            }
        }
    }
}
