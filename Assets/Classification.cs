using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Barracuda;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class Classification : MonoBehaviour
{
    public int image_size = 28;

    public NNModel modelFile;

    private Model _runtimeModel;

    public Texture2D texture;

    public TextMeshProUGUI text;

    private IWorker _worker;

    [Serializable]
    public struct Prediction
    {
        public int predictedValue;
        public float[] predicted;
        public void SetPrediction(Tensor t)
        {
            predicted = t.AsFloats();
            predictedValue = Array.IndexOf(predicted, predicted.Max());
            Debug.Log(predictedValue);
        }
    }

    public Prediction prediction;

    // Start is called before the first frame update
    void Start()
    {
        _runtimeModel = ModelLoader.Load(modelFile);
        _worker = WorkerFactory.CreateWorker(WorkerFactory.Type.ComputePrecompiled, _runtimeModel);
        prediction = new Prediction();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var channelCount = 1;
            var inputX = new Tensor(texture, channelCount);

            Tensor outputY = _worker.Execute(inputX).PeekOutput();
            inputX.Dispose();
            prediction.SetPrediction(outputY);

            string textWrite = "Predicted: " + prediction.predictedValue.ToString();
            text.SetText(textWrite);
        }
    }

    private void OnDestroy()
    {
        _worker?.Dispose();
    }
}
