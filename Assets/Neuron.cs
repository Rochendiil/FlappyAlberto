using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class Neuron
    {

    public double[] inputs;
    public double[] weights;
    public double error;
    private Random r = new Random(DateTime.Now.Millisecond);

    public Neuron(int nmbneuronprev)
    {
       inputs = new double[nmbneuronprev];
       weights = new double[nmbneuronprev];

    }

        
    public double output
    {
        get {
                double output = 0;
                for (int i = 0; i < inputs.Length; i++)
                {
                    output +=weights[i] * inputs[i];
                };
                return output; }
    }

    public void randomizeWeights()
    {
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] = UnityEngine.Random.value;
        }   
        
    }
    public void mutateWeight(double learningrate)
    {
        
        for (int i = 0; i < weights.Length; i++)
        {
            double rando = (UnityEngine.Random.value * 2) - 1;
            weights[i] = weights[i] + (rando * learningrate);
        }
    }

}

