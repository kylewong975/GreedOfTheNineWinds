using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Camera2DFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 cursor;
    public float damping = 1;
    //public float lookAheadFactor = 3;
    public float lookAheadReturnSpeed = 0.5f;
    public float lookAheadMoveThreshold = 0.1f;
    public float maxCursorDistance = 2;
    public bool seePastChar = false;
    public bool followCursor = true;

    private float m_OffsetZ;
    private Vector3 m_LastTargetPosition;
    private Vector3 m_CurrentVelocity;
    private Vector3 m_LookAheadPos;

	// called first
	void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode){
		target = GameManager.ins.playerController.transform;
	}

    // Use this for initialization
    private void Start()
    {
        m_LastTargetPosition = midPointTarget();
        m_OffsetZ = (transform.position - target.position).z;
        transform.parent = null;
    }

    private Vector3 midPointTarget()
    {
        //clamps cursor distance to 2
        if (Math.Abs(cursor.x - target.position.x) > maxCursorDistance)
            cursor.x = target.position.x + maxCursorDistance * Mathf.Clamp(cursor.x - target.position.x, -1, 1);
        if (Math.Abs(cursor.y - target.position.y) > maxCursorDistance)
            cursor.y = target.position.y + maxCursorDistance * Mathf.Clamp(cursor.y - target.position.y, -1, 1);

        if (seePastChar)
            return new Vector3(cursor.x, cursor.y, cursor.z);
        else if (followCursor)
            return new Vector3((target.position.x + cursor.x) / 2, (target.position.y + cursor.y) / 2, target.position.z);
        else
            return new Vector3(target.position.x, target.position.y, target.position.z);
    }


    // Update is called once per frame
    private void Update()
    {
        cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (target != null)
        {
            // only update lookahead pos if accelerating or changed direction
            float xMoveDelta = (midPointTarget() - m_LastTargetPosition).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                //m_LookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
                m_LookAheadPos = Vector3.right * Mathf.Sign(xMoveDelta);
            }
            else
            {
                m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
            }

            //Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward * m_OffsetZ;
            Vector3 aheadTargetPos = midPointTarget() + m_LookAheadPos + Vector3.forward * m_OffsetZ;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

            transform.position = newPos;

            m_LastTargetPosition = midPointTarget();
        }
    }
}
