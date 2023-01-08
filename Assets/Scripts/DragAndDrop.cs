using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public static DragAndDrop Instance;
    [SerializeField] float dragAndDropDistance = 3f;
    [SerializeField] LayerMask dragAndDropLayerMask;
    [SerializeField] private Transform grabPointTransform;

    public static bool grabbedGameMedia = false;

    Grabbable grabbable;

    void Awake()
    {
        if (Instance != null)
        { Destroy(this); }
        else
            Instance = this;
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Camera mainCamera = GetComponent<Camera>();
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = mainCamera.ScreenPointToRay(mousePosition);
            // Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

            if (Physics.Raycast(ray, out RaycastHit raycastHit, dragAndDropDistance, dragAndDropLayerMask))
            {
                if (grabbable == null)
                {
                    if (raycastHit.transform.TryGetComponent(out grabbable))
                    {
                        grabbable.Grab(grabPointTransform);
                        if (grabbable.gameObject.GetComponent<GameMedia>())
                        {
                            grabbedGameMedia = true;
                        }
                    }
                }
            }

        }
        else
        {
            if (grabbable != null)
            {
                grabbedGameMedia = false;
                grabbable.Drop();
                grabbable = null;
            }

        }
    }

    public GameObject GetGrabbableGameObject()
    {
        return grabbable.gameObject;
    }
}
