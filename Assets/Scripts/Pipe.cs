using System;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] private Transform headPipe;
    private Transform _pipeBody;

    private void OnEnable()
    {
        var selectedAssets = GameManager.Instance.SelectedAssets;
        _pipeBody.GetComponent<SpriteRenderer>().sprite = selectedAssets.PipeBody;
        headPipe.GetComponent<SpriteRenderer>().sprite = selectedAssets.PipeHead;
    }

    public void SetHeight(bool isInverted, float height)
    {
        int direction = 0;
        if(isInverted) direction = -1;
        else direction = 1;
        
        
    }
}
