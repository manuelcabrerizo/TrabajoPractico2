using UnityEngine;
public class PoolObject
{
    public GameObject Go { get; }
    public int Index { get; set; }
    public PoolObject(GameObject go, int index)
    {
        Go = go;
        Index = index;
    }
    public void SetPosition(Vector2 pos)
    {
        Go.transform.position = pos;
    }
}

public class ObjectPool : MonoBehaviour
{
    private PoolObject[] _objectPool;
    private int[] _freeList;
    private int _firstFree;
    
    [SerializeField] private GameObject prefab;
    [SerializeField] private int maxCout = 10;
    private void Awake()
    {
        _objectPool = new PoolObject[maxCout];
        _freeList = new int[maxCout];
        _firstFree = 0;

        for (int i = 0; i < maxCout; i++)
        {
            _objectPool[i] = new PoolObject(Instantiate(prefab), -1);
            _objectPool[i].Go.SetActive(false);
            _freeList[i] = i + 1;
        }
        _freeList[maxCout - 1] = -1;
    }
    
    public PoolObject Alloc()
    {
        PoolObject po = null;
        if (_firstFree > -1)
        {
            po = _objectPool[_firstFree];
            po.Index = _firstFree;
            int nextFree = _freeList[_firstFree];
            _freeList[_firstFree] = -1;
            _firstFree = nextFree;
            po.Go.SetActive(true);
        }
        return po;
    }

    public void Free(PoolObject po)
    {
        if (po != null)
        {
            _freeList[po.Index] = _firstFree;
            _firstFree = po.Index;
            po.Go.SetActive(false);
            po.Index = -1;
        }
    }

    public GameObject[] GetGameObjects()
    {
        GameObject[] gameObjects = new GameObject[_objectPool.Length];
        for(int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i] = _objectPool[i].Go;
        }
        return gameObjects;
    }
}
