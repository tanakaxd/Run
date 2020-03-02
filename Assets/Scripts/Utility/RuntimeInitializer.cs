using UnityEngine;

public class RuntimeInitializer
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitializeBeforeSceneLoad()
    {
        // ゲーム中に常に存在するオブジェクトを生成、およびシーンの変更時にも破棄されないようにする。
        var engine = new GameObject("Engine", typeof(Engine));
        GameObject.DontDestroyOnLoad(engine);
        var itemManager = GameObject.Instantiate(Resources.Load("ItemManager"));
        GameObject.DontDestroyOnLoad(itemManager);
    }

} // class RuntimeInitializer