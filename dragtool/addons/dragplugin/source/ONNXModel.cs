using Godot;
using Microsoft.ML.OnnxRuntime.Tensors;
using Microsoft.ML.OnnxRuntime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class ONNXModel
{
    private string _modelPath;
    private int _countImgs = 1;
    private int _countChannels = 3;
    private int _imgSize = 256;

    public ONNXModel(string modelPath)
    {
        _modelPath = modelPath;
    }

    public void UseModel()
    {
        //using (var session = new InferenceSession(_modelPath))
        //{
        //    // Пример входных данных (здесь предполагается, что они уже подготовлены)
        //    ReadOnlySpan<int> inputArray = new ReadOnlySpan<int>( new int[] { _countImgs, _countChannels, _imgSize, _imgSize }); // Пример для изображений размером 224x224x3

        //    // Создание входного тензора из массива
        //    var tensor = new DenseTensor<float>(inputArray);

        //    // Выполнение модели с входными данными
        //    var outputs = session.Run(new[] { tensor }).First().AsEnumerable<float>();

        //    // Пример: Извлечение выхода нужного слоя (предположим, вы хотите получить выход слоя с индексом 3)
        //    var desiredLayerOutput = outputs.ElementAt(3);

        //    // Продолжение работы с выходом нужного слоя...
        //}
    }
}

