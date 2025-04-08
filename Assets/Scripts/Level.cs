
using System;

using UnityEngine;
public class Level : MonoBehaviour
{
    private const float PipeWidth = 7.8f;
    private const float PipeHeadHeight = 3.75f;
    private const float CameraOrthoSize = 50f;
    private void Start()
    {
        CreatePipe(50f, 0f);
        CreatePipe(50f, 20f);
    }

    private void CreatePipe(float height, float xPosition)
    {
        var pipeHead = Instantiate(GameAssets.GetInstance().GetPipeHead());
        pipeHead.SetPositionAndRotation(new Vector2(xPosition, height - (PipeHeadHeight/2) - CameraOrthoSize), Quaternion.identity);
        
        var pipeBody = Instantiate(GameAssets.GetInstance().GetPipeBody());
        pipeBody.SetPositionAndRotation(new Vector2(xPosition, -CameraOrthoSize), Quaternion.identity);
        var pipeBodyRenderer = pipeBody.gameObject.GetComponent<SpriteRenderer>(); ;
        pipeBodyRenderer.drawMode = SpriteDrawMode.Sliced; // Making sure Sliced is selected to being able to set size
        pipeBodyRenderer.size = new Vector2(PipeWidth, height);
        var pipeBodyBoxCollider = pipeBody.gameObject.GetComponent<BoxCollider2D>();
        pipeBodyBoxCollider.size = new Vector2(PipeWidth, height);
        pipeBodyBoxCollider.offset = new Vector2(0f, height/2);
        
    }
}