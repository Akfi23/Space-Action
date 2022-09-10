using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 708.akfi extensions 
/// </summary>
public static class FastExtensions : object
{
    [SerializeField] private static Vector3 upVector=Vector3.up;
    [SerializeField] private static Vector3 downVector=Vector3.down;
    [SerializeField] private static Vector3 forwardVector=Vector3.forward;
    [SerializeField] private static Vector3 backwardVector = Vector3.back;
    [SerializeField] private static Vector3 leftVector=Vector3.left;
    [SerializeField] private static Vector3 rightVector=Vector3.right;
    [SerializeField] private static Vector3 zeroVector = Vector3.zero;
    [SerializeField] private static Vector3 oneVector = Vector3.one;
    [SerializeField] private static Quaternion identity => Quaternion.identity;

    public static Vector3 UpVector => upVector;
    public static Vector3 DownVector => downVector;
    public static Vector3 ForwardVector => forwardVector;
    public static Vector3 BackwardVector => backwardVector;
    public static Vector3 LeftVector => leftVector;
    public static Vector3 RightVector => rightVector;
    public static Vector3 ZeroVector => zeroVector;
    public static Vector3 OneVector => oneVector;
    public static Quaternion Identity => identity;

    #region Vector
    public static Vector3 RandomVector3(float XMin, float XMax, float YMin, float YMax, float ZMin, float ZMax)
    {
        Vector3 Result = new Vector3(UnityEngine.Random.Range(XMin, XMax), UnityEngine.Random.Range(YMin, YMax), UnityEngine.Random.Range(ZMin, ZMax));
        return Result;
    }

    public static Vector3 RandomVector3()
    {
        Vector3 Result = new Vector3(UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360));
        return Result;
    }
    #endregion

    #region Closest and Farest objects from List
    public static Transform GetClosestInCollection<T>(this Transform Target, List<T> List) where T :MonoBehaviour
    {
        int index = 0;
        float min = Mathf.Infinity;
        for (int i = 0; i < List.Count; i++)
        {
            float dist = Vector3.Distance(List[i].transform.position, Target.position);
            if (dist < min)
            {
                index = i;
                min = dist;
            }
        }
        return List[index].transform;
    }

    public static Transform GetClosestInCollectionFromTarget <T> (Transform Target, List<T> List) where T : MonoBehaviour
    {
        int index = 0;
        float min = Mathf.Infinity;
        for (int i = 0; i < List.Count; i++)
        {
            float dist = Vector3.Distance(List[i].transform.position, Target.position);
            if (dist < min)
            {
                index = i;
                min = dist;
            }
        }
        return List[index].transform;
    }

    public static Transform GetFarestInCollection <T> (this Transform Target, List<T> List) where T:MonoBehaviour
    {
        int index = 0;
        float max = 0;

        for (int i = 0; i < List.Count; i++)
        {
            float dist = Vector3.Distance(List[i].transform.position, Target.position);
            if (dist > max)
            {
                index = i;
                max = dist;
            }
        }
        return List[index].transform;
    }

    public static Transform GetFarestInCollectionFromTarget<T>(Transform Target, List<T> List) where T:MonoBehaviour
    {
        int index = 0;
        float max = 0;

        for (int i = 0; i < List.Count; i++)
        {
            float dist = Vector3.Distance(List[i].transform.position, Target.position);
            if (dist > max)
            {
                index = i;
                max = dist;
            }
        }
        return List[index].transform;
    }

    public static Vector3 GetClosestVectorForAllCollections(this Transform position, IEnumerable<Transform> otherPositions)
    {
        var closest = Vector3.zero;
        var shortestDistance = Mathf.Infinity;
        
        foreach (var otherPosition in otherPositions)
        {
            var distance = (position.position - otherPosition.position).sqrMagnitude;

            if (distance < shortestDistance)
            {
                closest = otherPosition.position;
                shortestDistance = distance;
            }
        }

        return closest;
    }

    public static Vector3 GetFarestVectorForAllCollections(this Transform position,IEnumerable<Transform> otherPositions)
    {
        var closest = Vector3.zero;
        var shortestDistance = Mathf.Infinity;

        foreach (var otherPosition in otherPositions)
        {
            var distance = (position.position - otherPosition.position).sqrMagnitude;

            if (distance > shortestDistance)
            {
                closest = otherPosition.position;
                shortestDistance = distance;
            }
        }

        return closest;
    }


    #endregion

    #region ShuffleItemsInList
    public static List<MonoBehaviour> ShuffleList(this List<MonoBehaviour> List)
    {
        for (int i = List.Count - 1; i >= 1; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            var temp = List[j];
            List[j] = List[i];
            List[i] = temp;
        }

        return List;
    }
    #endregion

    #region Get Min or Max from List
    public static int Minimal(List<int> List)
    {
        int Result = int.MaxValue;
        for (int i = 0; i < List.Count; i++)
            if (List[i] < Result)
                Result = List[i];
        return List.IndexOf(Result);
    }

    public static int Minimal(List<float> List)
    {
        float Result = float.MaxValue;
        for (int i = 0; i < List.Count; i++)
            if (List[i] < Result)
                Result = List[i];
        return List.IndexOf(Result);
    }

    public static int Smallest(List<int> List)
    {
        return Minimal(List);
    }

    public static int Smallest(this List<float> List)
    {
        return Minimal(List);
    }

    public static int Maximal(List<int> List)
    {
        int Result = int.MinValue;
        for (int i = 0; i < List.Count; i++)
            if (List[i] > Result)
                Result = List[i];
        return List.IndexOf(Result);
    }

    public static int Maximal(List<float> List)
    {
        float Result = float.MinValue;
        for (int i = 0; i < List.Count; i++)
            if (List[i] > Result)
                Result = List[i];
        return List.IndexOf(Result);
    }

    public static int Biggest(List<int> List)
    {
        return Maximal(List);
    }

    public static int Biggest(List<float> List)
    {
        return Maximal(List);
    }
    #endregion

    #region Coroutine
    public static void StartRoutine(this MonoBehaviour behaviour,IEnumerator cachedRoutine, IEnumerator targetRoutine)
    {
        if (cachedRoutine != null)
        {
            behaviour.StopCoroutine(cachedRoutine);
        }

        cachedRoutine = targetRoutine;

        behaviour.StartCoroutine(cachedRoutine);
    }

    public static void StopRoutine(this MonoBehaviour behaviour,IEnumerator cachedRoutine)
    {
        if (cachedRoutine != null)
        {
            behaviour.StopCoroutine(cachedRoutine);
        }
    }

    #endregion

    #region SideChecker

    public static float AngleDir(Transform targetDir,Transform fromObject)
    {
        float angle = Vector3.SignedAngle(fromObject.transform.forward, targetDir.position - fromObject.position, Vector3.up);
        angle = Mathf.Sign(angle);

        return angle;
    }
    #endregion

    #region Parabola and Parabola Tween Moving
    public static Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        System.Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }

    public static void ParaMove(this Transform Target, Transform Point, float time, float height = 1, bool destroy = false)
    {
        DOTween.Kill(Target);
        float Timer = 0;
        Vector3 StartPos = Target.position;
        DOTween.To(() => Timer, x => Timer = x, 1, time)
            .OnUpdate(() => { Target.position = FastExtensions.Parabola(StartPos, Point.position, height, Timer); })
            .OnComplete(() =>
            {
                if (destroy)
                    UnityEngine.Object.Destroy(Target.gameObject);
                else Target.position = FastExtensions.Parabola(StartPos, Point.position, height, Timer);
            });
    }
    #endregion

    #region UI
    public static bool PointerOverUI()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    public static void SetText(this TMP_Text tmpObj,string text)
    {
        tmpObj.text = text;
    }
    #endregion

    #region Childs
    public static void DisableChilds(this GameObject target)
    {
        var childArray = target.GetComponentsInChildren<GameObject>();

        for (int i = 0; i < childArray.Length; i++)
        {
            childArray[i].SetActive(false);
        }
    }

    public static void DestroyChilds(this GameObject target)
    {
        var childArray = target.GetComponentsInChildren<GameObject>();

        for (int i = 0; i < childArray.Length; i++)
        {
            UnityEngine.Object.Destroy(childArray[i].gameObject);
        }
    }
    #endregion

    #region Transform
    public static void ResetTransformation(this Transform transform)
    {
        transform.position = zeroVector;
        transform.localRotation = identity;
        transform.localScale = oneVector;
    }

    public static void ResetChildPositions(this Transform transform, bool recursive = false)
    {
        foreach (Transform child in transform)
        {
            child.position = zeroVector;

            if (recursive)
            {
                child.ResetChildPositions(recursive);
            }
        }
    }

    public static void SetZ(this Transform transform, float z)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, z);
    }

    public static void SetY(this Transform transform, float y)
    {
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }

    public static void SetX(this Transform transform, float x)
    {
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

    public static void SetChildLayers(this Transform transform, string layerName, bool recursive = false)
    {
        var layer = LayerMask.NameToLayer(layerName);
        SetChildLayersHelper(transform, layer, recursive);
    }

    static void SetChildLayersHelper(Transform transform, int layer, bool recursive)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.layer = layer;

            if (recursive)
            {
                SetChildLayersHelper(child, layer, recursive);
            }
        }
    }

    public static void AddChildren(this Transform transform, Component[] children)
    {
        Array.ForEach(children, child => child.transform.parent = transform);
    }

    public static void AddChildren(this Transform transform, GameObject[] children)
    {
        Array.ForEach(children, child => child.transform.parent = transform);
    }

    #endregion

    #region Random
    public static T GetRandomItem<T>(this IList<T> list)
    {
        return list[UnityEngine.Random.Range(0, list.Count)];
    }

    public static float GetRandom(float min,float max)
    {
        return UnityEngine.Random.Range(min, max);
    }

    public static int GetRandom(int min,int max)
    {
        return UnityEngine.Random.Range(min, max);
    }
    #endregion

    #region Component
    public static T AddComponent<T>(this Component component) where T : Component
    {
        return component.gameObject.AddComponent<T>();
    }

    public static T GetOrAddComponent<T>(this Component component) where T : Component
    {
        T tempComponent;

        component.TryGetComponent(out T requiredComponent);
        tempComponent = requiredComponent;

        if (tempComponent == null)
        {
            return component.AddComponent<T>();
        }
        else
        {
            return tempComponent;
        }
    }

    public static bool HasComponent<T>(this Component component) where T : Component
    {
        T tempComponent;
        component.TryGetComponent<T>(out T requiredComponent);
        tempComponent = requiredComponent;
        return tempComponent != null;
    }
    #endregion

    #region RigidBody
    public static void ChangeDirection(this Rigidbody rigidbody, Vector3 direction)
    {
        rigidbody.velocity = direction * rigidbody.velocity.magnitude;
    }
    #endregion

    public static void Slowmo(bool slowmo)
    {
        Time.timeScale = slowmo ? 0.4f : 1;
    }
}
