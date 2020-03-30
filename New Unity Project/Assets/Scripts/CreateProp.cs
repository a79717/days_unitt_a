using UnityEngine;

public class CreateProp : MonoBehaviour
{
    [Header("要生成的道具")]
    public GameObject prop;
    [Header("X 軸最小值")]
    public float xMin;
    [Header("X 軸最大值")]
    public float xMax;
    [Header("生成頻率"), Range(0.1f, 3f)]
    public float interva1 = 1;



    /// <summary>
    /// 生成道具物件
    /// </summary>
    private void CreatePropObject()
    {
        float x = Random.Range(xMin, xMax);             //設定髓機 X 座標介於最小值與最大值之間
        Vector3 pos = new Vector3(x, 7, 0);             //設定道具座標，三維向量(髓機X，高度，0)

        //生成(道具，座標，角度)
        //Quaternion.identity 為零度角 = (0, 0, 0)
        Instantiate(prop, pos, Quaternion.identity);    //實例化(物件，座標，角度)
 
   }

    private void Start()
    {
        float r = Random.Range(0f, 2f);

        InvokeRepeating("CreatePropObject", r, interva1);       //延遲重複呼叫("方法名稱"，延遲，頻率)
    }
}
