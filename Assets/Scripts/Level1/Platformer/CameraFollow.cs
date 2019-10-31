using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //target to follow
    public Transform target;
    //zeros out of velocity
    Vector3 velocity = Vector3.zero;
    //time to follow
    public float smoothTime = .20f;
    //enabled and set the maximum value of Y
    public bool YMaxEnabled = false;
    public float maxY = 0f;
    //enabled and set the minimum value of Y
    public bool YMinEnabled = false;
    public float minY = 0f;
    //enabled and set the maximum value of X
    public bool XMaxEnabled = false;
    public float maxX = 0f;
    //enabled and set the minimum value of X
    public bool XMinEnabled = false;
    public float minX = 0f;

    private void FixedUpdate()
    {
        Vector3 targetPos = target.position;


        //vertical
        if (YMaxEnabled && YMinEnabled)
            targetPos.y = Mathf.Clamp(target.position.y, minY, maxY);
        else if (YMinEnabled)
            targetPos.y = Mathf.Clamp(target.position.y, minY, target.position.y);
        else if (YMaxEnabled)
            targetPos.y = Mathf.Clamp(target.position.y, target.position.y, maxY);

        //horizontal
        if (XMaxEnabled && XMinEnabled)
            targetPos.x = Mathf.Clamp(target.position.x, minX, maxX);
        else if (XMinEnabled)
            targetPos.x = Mathf.Clamp(target.position.x, minX, target.position.x);
        else if (XMaxEnabled)
            targetPos.x = Mathf.Clamp(target.position.x, target.position.x, maxX);

        targetPos.z = transform.position.z;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }
}