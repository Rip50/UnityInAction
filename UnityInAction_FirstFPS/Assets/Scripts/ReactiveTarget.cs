using System.Collections;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReactToHit()
    {
        var behavior = GetComponent<EnemyBehavior>();
        if (behavior != null)
        {
            behavior.Kill();
        }

        StartCoroutine(nameof(Die));
    }

    public IEnumerator Die()
    {
        transform.localEulerAngles = new Vector3(-75, 0, 0);
        
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
