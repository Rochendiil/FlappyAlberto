using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

[Serializable]
public class Network : IComparable 
{
    private Layer inputLayer;
    private Layer[] hiddenLayers;
    private Layer outputLayer;
    public double learningrate = 0.1f;
    public int fitness;
    public Network(Network net)
    {
        hiddenLayers = new Layer[net.hiddenLayers.Length];
        for (int i = 0; i < net.hiddenLayers.Length; i++)
        {
            hiddenLayers[i] = net.hiddenLayers[i];
        }
        
        outputLayer = net.outputLayer;
    }
    public Network(int[] neuronsinlayer)
    {
        hiddenLayers = new Layer[neuronsinlayer.Length - 2];
        for (int i = 0; i < hiddenLayers.Length; i++)
        {

            hiddenLayers[i] = new Layer(neuronsinlayer[i + 1], neuronsinlayer[i]);

        }
        outputLayer = new Layer(neuronsinlayer[neuronsinlayer.Length - 1], neuronsinlayer[neuronsinlayer.Length - 2]);
    }
    public double[] feedforward(double[] input)
    {
        for (int j = 0; j < hiddenLayers[0].neuronList.Length; j++)
        {
            hiddenLayers[0].neuronList[j].inputs = input;
        }
        for (int i = 1; i < hiddenLayers.Length; i++)
        {
            for (int j = 0; j < hiddenLayers[i].neuronList.Length; j++)
            {
                for (int k = 0; k < hiddenLayers[i - 1].neuronList.Length - 1; k++)
                {
                    hiddenLayers[i].neuronList[j].inputs[k] = hiddenLayers[i - 1].neuronList[k].output;
                }
            }
        }
        for (int i = 0; i < outputLayer.neuronList.Length; i++)
        {
            for (int j = 0; j < hiddenLayers[hiddenLayers.Length - 1].neuronList.Length; j++)
            {
                outputLayer.neuronList[i].inputs[j] = hiddenLayers[hiddenLayers.Length - 1].neuronList[j].output;
            }
        }
        double[] output = new double[outputLayer.neuronList.Length];
        for (int i = 0; i < outputLayer.neuronList.Length; i++)
        {
            output[i] = sigmoid.output(outputLayer.neuronList[i].output);
        }
        return output;
    }
    public void mutateWeights()
    {
        for (int i = 0; i < hiddenLayers.Length; i++)
        {
            hiddenLayers[i].mutateWeights(learningrate);
        }
        outputLayer.mutateWeights(learningrate);
    }

    public int CompareTo(object obj)
    {
        Network compare = (Network)obj;
        if(fitness > compare.fitness)
        {
            return -1;
        }
        if (fitness < compare.fitness)
        {
            return 1;
        }
        return 0;
    }

    public Network Clone()
    {
        using (var ms = new MemoryStream())
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(ms, this);
            ms.Position = 0;
            return (Network)formatter.Deserialize(ms);
        }
    }
}
