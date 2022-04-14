using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] int objectCount;

    public static ObjectPool instance;

    private Dictionary<GameObject, List<GameObject>> _objectClones = new Dictionary<GameObject, List<GameObject>>();

    public void Initialize(GameObject obj)
    {
        if (_objectClones.ContainsKey(obj))
            return;

        List<GameObject> listOfClones = new List<GameObject>();

        for (int i = 0; i < objectCount; i++)
        {
            GameObject clone = Instantiate(obj, transform);
            clone.SetActive(false);
            
            PooledObject pooledObject = clone.AddComponent<PooledObject>();
            pooledObject.prefab = obj;

            listOfClones.Add(clone);
        }

        _objectClones.Add(obj, listOfClones);
    }

    public GameObject Get(GameObject obj)
    {
        if (_objectClones.ContainsKey(obj) == false)
            return null;
        if (_objectClones[obj].Count == 0)
            return null;

        GameObject clonedObject = _objectClones[obj][0];
        clonedObject.SetActive(true);
        clonedObject.transform.parent = null;

        _objectClones[obj].Remove(clonedObject);

        return clonedObject;
    }

    public void Return(GameObject clone)
    {
        PooledObject pooledObject = clone.GetComponent<PooledObject>();
        if (pooledObject == null)
            return;

        _objectClones[pooledObject.prefab].Add(clone);
        clone.transform.parent = transform;
        clone.SetActive(false);
    }

    private void Awake()
    {
        instance = this;
    }
}