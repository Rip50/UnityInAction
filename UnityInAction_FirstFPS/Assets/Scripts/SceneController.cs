using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    private GameObject _enemy;

    // Update is called once per frame
    void Update()
    {
        if(_enemy == null)
        {
            _enemy = Instantiate(_enemyPrefab);
            _enemy.transform.position = new Vector3(0, 1, 0);
            _enemy.transform.Rotate(0, Random.Range(0, 360), 0);
        }
    }
}
