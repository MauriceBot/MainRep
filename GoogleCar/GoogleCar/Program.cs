using System;

namespace GoogleCar
{   
    /// <summary>
    /// Simple neural network.
    /// Learns how to differentiate between C and A.
    /// </summary>
    class Program
    {
        // Connections between neurons.
        static float[] weights = new float[20];

        static string a = System.IO.File.ReadAllText("A.txt");
        static string c = System.IO.File.ReadAllText("C.txt");

        // Epsilon.
        const float LEARNINGSPEED = 0.05f;
        const float SUCCESS = 0.001f;
        const float THRESHOLD = 0.5f;

        static void Main(string[] args)
        {
            GenerateRandomWeights();
            ProvokeNeuralActivity();
            Console.ReadKey();
        }

        /// <summary>
        /// Provokes neural activity and really gets gets those neurons firing.
        /// </summary>
        private static void ProvokeNeuralActivity()
        {
            float totalError = 1;
            while (totalError > SUCCESS)
            {
                totalError = 0;
                float result = Guess(a);
                float errorA = 1 - result;
                Learn(errorA, a);
                Comment(result);

                result = Guess(c);
                float errorB = 0 - result;
                Learn(errorB, c);
                Comment(result);

                totalError += Math.Abs(errorA) + Math.Abs(errorB);
            }
        }

        /// <summary>
        /// Widrow-Hoff learning rule.
        /// </summary>
        /// <param name="error">-1, 0 or 1</param>
        /// <param name="image"> A or C</param>
        private static void Learn(float error,string image)
        {
            for (int i = 0; i < weights.Length; i++)
                weights[i] += LEARNINGSPEED * error * (int)Char.GetNumericValue(image[i]);
        }

        /// <summary>
        /// Returns the networks guess.
        /// The guess is a float between 0 and 1.
        /// </summary>
        /// <param name="image"> A or C </param>
        /// <returns></returns>
        private static float Guess(string image)
        {
            float guess = 0;
            for (int i = 0; i < a.Length; i++)
                guess += weights[i] * (int)Char.GetNumericValue(image[i]);
            return guess;
        }

                /// <summary>
        /// Prints feedback on the learning process to the console.
        /// </summary>
        /// <param name="result"> What the program guessed. </param>
        private static void Comment(float result)
        {
            if (result > THRESHOLD)
                Console.WriteLine("I think it's A with a certainty of " + (result * 100) + "%");
            else
                Console.WriteLine("I think it's C with a certainty of " + ((1 - result) * 100) + "%");
        }

        /// <summary>
        /// Fills the weights array with random floats
        /// </summary>
        private static void GenerateRandomWeights()
        {
            Random r = new Random();
            for (int i = 0; i < weights.Length; i++)
                weights[i] = r.Next(0, 101) / 100f / 20f;
        }
    }
}
