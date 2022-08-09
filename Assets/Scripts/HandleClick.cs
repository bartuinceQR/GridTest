using UnityEngine;

public class HandleClick : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (MainController.Instance.GetInput())
        {
            if (Input.GetMouseButtonDown(0))
            {
                SendClickRay(true);
            }
            else if (Input.GetMouseButtonDown(1)) //debug
            {
                SendClickRay(false);
            }
        }
    }

    void SendClickRay(bool isSetter)
    {
        Vector2 origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero);

        if (hit)
        {
            GridElement element = hit.transform.GetComponent<GridElement>();
            if (element != null)
            {
                if (isSetter)
                    element.Enable();
                else
                    element.Disable();
            }
        }
    }
    
    
}
