using Cysharp.Threading.Tasks;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Hello World!");
        PrintByeWorldAsync().Forget();
    }

    private async UniTask PrintByeWorldAsync()
    {
        await UniTask.Delay(3000);
        Debug.Log("Bye World!");
    }
}
