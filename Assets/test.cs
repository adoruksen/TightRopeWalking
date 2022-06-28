using UnityEngine;

public class test : MonoBehaviour
{
    public int sagDeger;
    public int solDeger;

    private void OnEnable()
    {
        //int artiliDeger = Random.Range(DifferenceValue(), Mathf.Abs(MaxValue() + DifferenceValue()) - MinValue());
        //int eksiliDeger = Random.Range(-(MaxValue()-DifferenceValue())+MinValue(), -DifferenceValue());
        int gelecekdeger = Random.Range(-MinValue(), Mathf.Abs(DifferenceValue()) + 8);
        int a = Random.Range(-(MaxValue()/*+DifferenceValue()/2*/) + 1, MaxValue()+DifferenceValue()/2);
        
        //Debug.Log($"artiliDeger {artiliDeger}, eksiliDeger {eksiliDeger}");
        Debug.Log($"gelecekdeger{a}");


        //int gelecekSayi = 
    }



    private int MinValue()
    {
        return Mathf.Min(sagDeger, solDeger);
    }

    private int MaxValue()
    {
        return Mathf.Max(sagDeger, solDeger);
    }

    private int DifferenceValue()
    {
        return Mathf.Abs(sagDeger - solDeger);
    }
}
