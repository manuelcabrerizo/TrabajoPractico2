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

// this class is an ObjectPool
// to get an object call Alloc and to free it call Free
// Alloc: if there is an open object return that object else return null
// Free: free the object so it can be reuse

public class ObjectPool : MonoBehaviour
{
    // array where the objects in the pool are stores
    private PoolObject[] _objectPool;
    // array of indices into the free positions of the object array
    private int[] _freeList;
    // index of the first free position of the object array
    private int _firstFree;
    
    // size of the arrays, this is the maximun count of elements can be allocated at the
    // same time
    [SerializeField] private int maxCout = 10;
    [SerializeField] private GameObject prefab;

    private void Awake()
    {
        // alloc memory for all the arrays
        _objectPool = new PoolObject[maxCout];
        _freeList = new int[maxCout];
        
        // initialize the data array, the first free index and the freelist.
        // set the free list to point to all the elements of the data array,
        // because the array its empty at this point
        _firstFree = 0;
        for (int i = 0; i < maxCout; i++)
        {
            _objectPool[i] = new PoolObject(Instantiate(prefab), -1);
            _objectPool[i].Go.SetActive(false);
            _freeList[i] = i + 1;
        }
        _freeList[maxCout - 1] = -1;
    }
    
    private void OnDestroy()
    {
        for(int i = 0; i < _objectPool.Length; i++)
        {
            Destroy(_objectPool[i].Go);
        }
    }
    
    public PoolObject Alloc()
    {
        // if we have and open block ( _firstFree > -1 )
        // get that block and unpdate the first free to
        // point to the next free block. if _firstFree == -1
        // its means that all the block are ocupide and return null
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
        // free the object by adding the index
        // to the free list
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
