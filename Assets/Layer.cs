using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


[Serializable]
public class Layer
{
    public Neuron[] neuronList;
    public Layer(int nmbneurons, int nmbneuronsprev)
    {
        neuronList = new Neuron[nmbneurons];
        for (int i = 0; i < neuronList.Length; i++)
        {
            neuronList[i] = new Neuron(nmbneuronsprev);
        }
        initWeights();
    }

    public void initWeights()
    {
        for (int i = 0; i < neuronList.Length; i++)
        {
            neuronList[i].randomizeWeights();
        }
    }
    public void mutateWeights(double learningrate)
    {
        
        for (int i = 0; i < neuronList.Length; i++)
        {
            neuronList[i].mutateWeight(learningrate);
        }
    }
}

