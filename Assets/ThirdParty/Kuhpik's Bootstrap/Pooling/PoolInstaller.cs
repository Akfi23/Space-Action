using UnityEngine;
using Kuhpik.Pooling;
using NaughtyAttributes;
using System.Collections.Generic;

namespace Kuhpik
{
    [DefaultExecutionOrder(10)]
    public class PoolInstaller : DontDestroySingleton<PoolInstaller>
    {
        [SerializeField] bool usePooling;
        [SerializeField] [ShowIf("usePooling")] int baseCapacity;
        [SerializeField] [ShowIf("usePooling")] string loadingPath = "Pooling";

        private Dictionary<string, Pool> poolDatas = new Dictionary<string, Pool>();

        void Start()
        {
            PoolingSystem.Clear();
            if (usePooling)
            {
                var pools = Resources.LoadAll<Pool>(loadingPath);
                PoolingSystem.Init(pools, baseCapacity);

                foreach (var pool in pools)
                {
                    poolDatas.Add(pool.PoolName, pool);
                }
            }
        }

        public void InitPool()
        {
            if (usePooling)
            {
                var pools = Resources.LoadAll<Pool>(loadingPath);
                PoolingSystem.Init(pools, baseCapacity);
            }
        }

        public Pool GetPool(string poolName)
        {
            return poolDatas[poolName];
        }
    }
}
