
using System;

using UnityEngine;
public class Level : MonoBehaviour
{
    private const float PipeWidth = 7.8f;
    private const float PipeHeadHeight = 3.75f;
    private const float CameraOrthoSize = 50f;
    private void Start()
    {
        CreatePipe(50f, 0f, true);
        CreatePipe(50f, 20f, false);
    }

    private void CreatePipe(float height, float xPosition, bool createBottom)
    {
        var pipeHeadPositionY = 0f;
        var pipeBodyPositionY = 0f;
        var pipeHead = Instantiate(GameAssets.GetInstance().GetPipeHead());
        var pipeBody = Instantiate(GameAssets.GetInstance().GetPipeBody());
        if (createBottom)
        {
            pipeHeadPositionY = height - (PipeHeadHeight / 2) - CameraOrthoSize;
            pipeBodyPositionY = -CameraOrthoSize;
        }
        else
        {
            pipeHeadPositionY = CameraOrthoSize - height + PipeHeadHeight / 2;
            pipeBodyPositionY = CameraOrthoSize;
            pipeBody.localScale = new Vector3(1, -1, 1); // So it grows inverted
        }
        
        pipeHead.SetPositionAndRotation(new Vector2(xPosition, pipeHeadPositionY), Quaternion.identity);
        
        pipeBody.SetPositionAndRotation(new Vector2(xPosition, pipeBodyPositionY), Quaternion.identity);
        var pipeBodyRenderer = pipeBody.gameObject.GetComponent<SpriteRenderer>(); ;
        pipeBodyRenderer.drawMode = SpriteDrawMode.Sliced; // Making sure Sliced is selected to be able to set size
        pipeBodyRenderer.size = new Vector2(PipeWidth, height);
        var pipeBodyBoxCollider = pipeBody.gameObject.GetComponent<BoxCollider2D>();
        pipeBodyBoxCollider.size = new Vector2(PipeWidth, height);
        pipeBodyBoxCollider.offset = new Vector2(0f, height/2);
        
    }
}