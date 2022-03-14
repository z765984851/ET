using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.SceneManagement;

namespace ET
{

  
    public class AddressableComponentAwakeSystem: AwakeSystem<AddressableComponent>
    {
        public override void Awake(AddressableComponent self)
        {
            self.Awake();
        }
    }

  

    public class AddressableComponent : Entity, IAwake
    {
        public static AddressableComponent Instance { get; set; }

        public  Dictionary<string, object> assetCached = new Dictionary<string, object>();
        public  Dictionary<string, Sprite> spriteCached = new Dictionary<string, Sprite>();
        public  Dictionary<string, AsyncOperationHandle> handleCached = new Dictionary<string, AsyncOperationHandle>();
        
        /// <summary>
        /// Addressable组件是否已经初始化
        /// </summary>
        private bool initialize = false;

        /// <summary>
        /// 更新的资源需要添加的标签
        /// </summary>
        private string updateLabel = "hotfix";

      
        

        public void Awake()
        {
            Instance = this;
        }
        
        
        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }

            base.Dispose();

            Instance = null;
            foreach (var file in this.assetCached)
            {
                if (assetCached.ContainsKey(file.Key))
                {
                    Addressables.Release(assetCached[file.Key]);
                }
          

                if (this.handleCached.ContainsKey(file.Key))
                {
                    Addressables.Release(handleCached[file.Key]);
                }
            }
            this.assetCached.Clear();
            this.handleCached.Clear();
            this.spriteCached.Clear();

        }
        
        
        /// <summary>
        /// Addressable异步初始化
        /// </summary>
        public ETTask AddressableInitializeAsync()
        {
            if (initialize)
            {
                Log.Error("Addressable has been initiallize");
                return null;
            }
            else
            {
                Log.Info("Addressable initiallize Start");
                ETTask tcs = ETTask.Create();
                AsyncOperationHandle initializeHandle = Addressables.InitializeAsync();
                initializeHandle.Completed += (handle) =>
                {
                   
                    if (initializeHandle.Status==AsyncOperationStatus.Succeeded)
                    {
                        Log.Info("Addressable initiallize succeeded");
                        initialize = true;
                        tcs.SetResult();
                    }
                    else
                    {
                        tcs.SetException(handle.OperationException);
                    }
                };

                return tcs.GetAwaiter();
            }
        }
        
        /// <summary>
        /// 获取需要更新的资源的大小
        /// </summary>
        /// <returns>返回大小，单位Byte，如果大于0说明需要有更新</returns>
        public ETTask<long> AddressableGetDownloadSizeAsync()
        {
            if (initialize)
            {
                Log.Info("Get downloadsize start");
                ETTask<long> tcs = ETTask<long>.Create();
                AsyncOperationHandle<long> downloadSizeHandle = Addressables.GetDownloadSizeAsync(this.updateLabel);
                downloadSizeHandle.Completed += (handle) =>
                {
                    if (handle.Status==AsyncOperationStatus.Succeeded)
                    {
                        Log.Info($"Get downloadsize succeeded : {handle.Result} ");
                        tcs.SetResult(handle.Result);
                    }
                    else
                    {
                        tcs.SetException(handle.OperationException);
                    }
                    
                };
                return tcs.GetAwaiter();
            }
            else
            {
                Log.Error("Please initialize first");
                return null;
            }
        }
        
        /// <summary>
        /// 下载需要更新的资源 调用的时候不使用await等待 而是使用while(task.IsCompleted)检测是否完成
        /// Example :
        /// var task=DownloadUpdateAssetsAsync();
        /// while(task.IsCompleted){
        ///      ...
        ///      await TimerComponent.Instance.WaitFrameAsync();
        /// }
        /// ...
        /// </summary>
        public async ETTask DownloadUpdateAssetsAsync()
        {
            if (initialize)
            {
                ETTask tcs = ETTask.Create();
                AsyncOperationHandle downloadHandle = Addressables.DownloadDependenciesAsync(this.updateLabel);

                var needDownloadSize = downloadHandle.GetDownloadStatus().TotalBytes;
                while (!downloadHandle.IsDone)
                {
                    Game.EventSystem.Publish(new EventType.DownloadInfo()
                    {
                        NeedDownloadSize = needDownloadSize,
                        DownloadedSize = downloadHandle.GetDownloadStatus().DownloadedBytes
                    });
                    await TimerComponent.Instance.WaitFrameAsync();
                }
                

                if (downloadHandle.Status==AsyncOperationStatus.Succeeded)
                {
                    Log.Info($"Download update assets succeeded  ");
                    
                    tcs.SetResult();
                }
                else
                {
                    tcs.SetException(downloadHandle.OperationException);
                }
            }
            else
            {
                Log.Error("Please initialize first");
            }
        }
        
        /// <summary>
        /// 通过标签或名称获取所有对应的资源的信息
        /// </summary>
        /// <param name="label"></param>
        /// <param name="onComplete"></param>
        /// <typeparam name="T"></typeparam>
        public ETTask<IList<IResourceLocation>> GetResLoaction(string label)
        {
           
            ETTask<IList<IResourceLocation>> tcs = ETTask<IList<IResourceLocation>>.Create();
            var getHandle = Addressables.LoadResourceLocationsAsync(label);
            getHandle.Completed += (handle) =>
            {
                if (handle.Status==AsyncOperationStatus.Succeeded)
                {
                   
                    tcs.SetResult(handle.Result);
                }
                else
                {
                    tcs.SetException(handle.OperationException);
                }
            };
            return tcs.GetAwaiter();
        }
        
        /// <summary>
        /// 加载一个指定类型的资源
        /// </summary>
        /// <param name="name"></param>
        /// <param name="onComplete"></param>
        /// <typeparam name="T"></typeparam>
        public ETTask<T> LoadAsset<T>(string name)
        {
            ETTask<T> tcs = ETTask<T>.Create();
            if (assetCached.ContainsKey(name))
            {
                tcs.SetResult((T)this.assetCached[name]);
            }
            else
            {
                AsyncOperationHandle<T> handle =  Addressables.LoadAssetAsync<T>(name);
                handle.Completed += (result) =>
                {
                    if (result.Status==AsyncOperationStatus.Succeeded)
                    {
                        assetCached[name]=result.Result;
                        if (!handleCached.ContainsKey(name))
                        {
                            handleCached[name]=handle;
                        }
                        tcs.SetResult((T)this.assetCached[name]);
                    }
                    else
                    {
                        tcs.SetException(result.OperationException);
                    }
                };
              
            }
            return tcs.GetAwaiter();
        }
        
        
        /// <summary>
        /// 加载指定类型的资源,需要对资源单个管理,所以先使用location获取所有label对应的资源再单个加载
        /// </summary>
        /// <param name="name"></param>
        /// <param name="onComplete"></param>
        public async ETTask LoadAssets(string name,Action<int,int> onLoading)
        {
           
            var getTask = GetResLoaction(name);
            while (!getTask.IsCompleted)
            {
               await TimerComponent.Instance.WaitFrameAsync();
            }

            var locationList = getTask.GetResult();
            int assetsLength = locationList.Count;
            int currentFinish = 0;
           
            foreach (var resourceLocation in locationList)
            {
                await this.LoadAsset<UnityEngine.Object>(resourceLocation.PrimaryKey);
                currentFinish++;
               
                Game.EventSystem.Publish(new EventType.LoadAsset(){AssetsCount = assetsLength,LoadedCount = currentFinish});
            }
            
        }


        public ETTask LoadScene(string sceneName,LoadSceneMode sceneMode=LoadSceneMode.Single)
        {
            var handle = Addressables.LoadSceneAsync(sceneName,sceneMode);
            ETTask tcs = ETTask.Create();
            if (assetCached.ContainsKey(sceneName))
            {
                tcs.SetResult();
            }
            else
            {
                handle.Completed += (result) =>
                {
                    if (result.Status==AsyncOperationStatus.Succeeded)
                    {
                        assetCached[sceneName]=result.Result;
                       
                        tcs.SetResult();
                    }
                    else
                    {
                        tcs.SetException(result.OperationException);
                    }
                };
            }

            return tcs.GetAwaiter();
        }
        
        /// <summary>
        /// 释放key对应的资源
        /// </summary>
        /// <param name="key"></param>
        public void ReleaseAsset(string key)
        {
            if (assetCached.ContainsKey(key))
            {
                Addressables.Release(assetCached[key]);
               
                assetCached.Remove(key);
              
            }
          

            if (this.handleCached.ContainsKey(key))
            {
               
                Addressables.Release(handleCached[key]);
                handleCached.Remove(key);
            }
        }
        
        /// <summary>
        /// 根据key获取一个指定类型资源
        /// </summary>
        /// <param name="key"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetAsset<T>(string key)
        {
            T obj=default ;
            if (assetCached.ContainsKey(key))
            {
                obj = (T)assetCached[key];
            }
            else
            {
                Debug.Log($"未找到对应的资源{key}");
            }
            return obj;
        }
        
        /// <summary>
        /// 获取一个Sprite
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Sprite GetSprite(string key)
        {
            Sprite sp = null;
            if (spriteCached.ContainsKey(key))
            {
                sp = spriteCached[key];
            }
            else
            {
                Texture2D texture = GetAsset<Texture2D>(key);
                sp = Sprite.Create(texture,new Rect(0,0,texture.width,texture.height),Vector2.one*0.5f);
                spriteCached[key] = sp;
            }
     
            return sp;
        }

        
    }
}