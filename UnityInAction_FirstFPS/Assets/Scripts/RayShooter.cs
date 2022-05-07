using System;
using System.Collections;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
    private Camera _camera;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        var center = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
        var ray = _camera.ScreenPointToRay(center);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            var hitObject = hit.transform.gameObject;
            var target = hitObject.GetComponent<ReactiveTarget>();
            if(target == null)
            {
                StartCoroutine(nameof(CreateHitIndicator), hit.point);
            }
            else
            {
                target.ReactToHit();
            }
        }
    }

    private IEnumerator CreateHitIndicator(Vector3 position)
    {
        var hole = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        hole.transform.position = position;
        hole.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        yield return new WaitForSeconds(2);

        Destroy(hole);
    }

    private void OnGUI()
    {
        int size = 20;
        var posX = _camera.pixelWidth / 2;
        var posY = _camera.pixelHeight / 2;

        GUI.Label(new Rect(posX, posY, size, size), "*");
    }
}
