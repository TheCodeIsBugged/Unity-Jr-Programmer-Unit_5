using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))]

public class ClickAndSwipe : MonoBehaviour
{
    private GameManager gameManager;
    private Camera cam;
    private Vector3 mousePos;

    private TrailRenderer trailRenderer;
    private BoxCollider boxCollider;

    private bool swiping = false;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        cam = Camera.main;

        trailRenderer = GetComponent<TrailRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        trailRenderer.enabled = false;
        boxCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isGameover)
        {
            if (Input.GetMouseButtonDown(0))
            {
                swiping = true;
                UpdateComponents();
            }

            else if (Input.GetMouseButtonUp(0))
            {
                swiping = false;
                UpdateComponents();
            }

            if (swiping)
            {
                UpdateMousePosition();
            }
        }
    }

    public void UpdateMousePosition()
    {
        mousePos.x = Input.mousePosition.x;
        mousePos.y = Input.mousePosition.y;

        mousePos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));
        transform.position = mousePos;
    }

    public void UpdateComponents()
    {
        trailRenderer.enabled = swiping;
        boxCollider.enabled = swiping;
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Target>().DestroyTarget();
    }
}
