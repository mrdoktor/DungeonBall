using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
#if UNITY_EDITOR
using UnityEditor;
#endif
[ExecuteInEditMode]
public class HandleMove : MonoBehaviour {
    
    //[HideInInspector]
    public Transform obstacle, turnHandle; 
    public enum MoveType {none, move, move2, rotate, turn};
    public MoveType inputX, inputY;
    public float maxAngle = 180;
    [HideInInspector] public SpriteRenderer limits;
    private Vector3 startPos, newPos, dragPos, offset;
    private Vector2 pos;
    private float localMove;
    public float fix;
    [HideInInspector] public float angle;
    [SerializeField][HideInInspector] private Vector2 _anchorPos, _margins;
    [SerializeField][HideInInspector] private float _anchorAngle, _startAngle;
    [SerializeField][HideInInspector] private Vector2 _size = new Vector2( 4, 1f);
    bool dragging;


    public Vector2 size
    {
        set 
        {
            _size = value;
            UpdateSize();
        }
        get
        {
            return _size;
        }
    }

    public Vector2 anchorPos
    {
        set 
        {
            _anchorPos = value;
            UpdateLimits();
        }
        get
        {
            return _anchorPos;
        }
    }

    public float anchorAngle
    {
        set 
        {
            _anchorAngle = value;
            UpdateLimits();
        }
        get
        {
            return _anchorAngle;
        }
    }

    public float startAngle
    {
        set 
        {
            _startAngle = value;
            UpdateLimits();
        }
        get
        {
            return _startAngle;
        }
    }

    public Vector2 margins
    {
        set 
        {
            _margins = value;
            UpdateLimits();
        }
        get
        {
            return _margins;
        }
    }

    public virtual void TouchBegan (Vector3 pos)
    {
        dragging = true; 
        var v3 = pos;//Input.mousePosition;
        v3.z = -Camera.main.transform.position.z;
        startPos = Camera.main.ScreenToWorldPoint(v3);
        offset = startPos-obstacle.position;
        dragPos  = startPos;
    }

    public virtual void TouchEnded ()
    {
        dragging = false; 
    }

    public virtual void TouchDrag (Vector3 pos)
    {
        var v3 = pos;
        v3.z = -Camera.main.transform.position.z;
        newPos = Camera.main.ScreenToWorldPoint(v3);
        Drag(newPos-dragPos);
        dragPos = newPos;
    }

    private void Drag(Vector2 move) {

        float side = 1;
        if (Vector3.Dot(offset, transform.right)>0) side =-1;
        move = new Vector2( Vector2.Dot(transform.right,move), Vector2.Dot(transform.up,move));
        if (true)
        {
            if (Mathf.Abs(move.x)>Mathf.Abs(move.y)) 
            {
                move.y = 0;
            }
            else
            {
                move.x = 0;
            }
        }

        if (inputY==MoveType.move) localMove += move.y;
        if (inputX==MoveType.move) localMove += move.x;

        if (inputY==MoveType.rotate) angle += move.y*10*side;
        if (inputX==MoveType.rotate) angle += move.x*10*side;

        if (inputY==MoveType.move2) localMove += move.y*5;
        if (inputX==MoveType.move2) localMove += move.x*5;

        if (inputY==MoveType.turn || inputX==MoveType.turn)
        {
            Vector2 oldOffset = dragPos-obstacle.position;
            float offsetAngle = RSMath.GetAngle(oldOffset.x, oldOffset.y, true);
            Vector2 newOffset = newPos-obstacle.position;
            float newAngle = RSMath.GetAngle(newOffset.x, newOffset.y, true);
            angle += newAngle-offsetAngle;
        }

        angle = RSMath.FixAngle(angle);
        if (Mathf.Abs(angle)>maxAngle) angle = maxAngle*Mathf.Sign(angle);
        UpdatePos();
    }

    public void UpdatePos()
    {
        localMove = Mathf.Min(localMove, _margins.y);
        localMove = Mathf.Max(localMove, -_margins.x);
        pos = localMove*Vector3.right + Vector3.up*fix;
        obstacle.localPosition = pos;
        obstacle.eulerAngles = Vector3.back*(anchorAngle+startAngle+angle);
    }

    public void UpdateSize()
    {
        obstacle.localScale = new Vector3(size.x, size.y, 1);
    }

    public void UpdateLimits()
    {
        transform.localPosition = anchorPos;
        transform.eulerAngles = Vector3.back*anchorAngle;
        if (!Application.isPlaying)
        {
            localMove = 0f;
            angle = 0;
        }

        UpdatePos();

        if (limits!=null)
        {
            Vector3 max =   _margins.y*Vector3.right;
            Vector3 min =   - _margins.x*Vector3.right;
            Vector3 limitPos = (max+min)/2;
            limitPos.z = 5f;
            limits.transform.parent.localPosition = limitPos;
            Vector2 limitSize = limits.size;
            limitSize.y = _margins.y + _margins.x +.5f;
            limitSize.x =0.5f;
            limits.size = limitSize;
        }
    }
}


#if UNITY_EDITOR

[CustomEditor(typeof(HandleMove))]
public class HandleMoveEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        HandleMove handleMove = (HandleMove) target;
        handleMove.anchorPos = (Vector2) EditorGUILayout.Vector2Field("AnchorPos:", handleMove.anchorPos);
        handleMove.anchorAngle = (float) EditorGUILayout.FloatField("AnchorAngle:", handleMove.anchorAngle);
        handleMove.startAngle = (float) EditorGUILayout.FloatField("StartAngle:", handleMove.startAngle);
        handleMove.margins = (Vector2) EditorGUILayout.Vector2Field("Margins:", handleMove.margins);
        EditorUtility.SetDirty(target);
    }
}
#endif