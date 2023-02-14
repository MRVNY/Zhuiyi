using UnityEngine;

public class Gong : Magic
{
    private GameObject Arrow;
    
    void Start()
    {
        dontDestroy.Add("Gong");
        canDestroy.Add("Bubble");
    }

    public void Attack()
    {
        if(UI.JC != null) UI.JC.SetRumble (160, 320, 0.6f, 200);
        Arrow = Instantiate(gameObject, transform.position, transform.rotation, null);
        Arrow.transform.localScale = Vector3.one * 0.05f;
        
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 1));
        Vector3 targetPosition;
        
        if (Physics.Raycast(ray, out hit))
            targetPosition = hit.point;
        else
            targetPosition = ray.GetPoint(10);

        Vector3 direction = targetPosition - Arrow.transform.position;
        Arrow.GetComponent<Rigidbody>().AddForce(direction * 100);
        Arrow.transform.LookAt(targetPosition);
    }

    private void Update()
    {
        if (transform.parent != null)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 1));
            Vector3 targetPosition;

            if (Physics.Raycast(ray, out hit) && LayerMask.LayerToName(hit.transform.gameObject.layer)=="Enemy")
                targetPosition = hit.point;
            else
                targetPosition = ray.GetPoint(10);
            
            transform.parent.LookAt(targetPosition);

            if (Time.timeScale == 1 && 
                (Input.GetKeyDown(KeyCode.Space) || (UI.JC!=null && (UI.JC.GetButtonDown(Joycon.Button.SHOULDER_1) 
                                                                     || UI.JC.GetAccel().magnitude>5))) )
            {
                Attack();
            }
        }
    }
}
