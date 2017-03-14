using UnityEngine;
using PsdRebuilder;


public class Main : MonoBehaviour
{
    void Start ()
    {
        PanelCreator.Instance.Create("BattlePreparePanel");
    }
}
