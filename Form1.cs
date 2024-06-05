using System;

using Accord;
using Accord.MachineLearning;
using System.Windows.Forms;
using System.Collections;

namespace similarityApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void create_list(string[] client_products)
        {
            double[][] client_vectors_xd = vectorize(client_products);
            double[][] client_vectors_3d = reduce_dimensionality(client_vectors_xd);

            for (int i = 0; i < client_vectors_3d.Count; i++){
                for (int j = 0; j < central_vectors_3d.Count; j++){
                    calculate_distance(client_vectors_3d[i], central_vectors_3d[j]);
                }
            }
        }

        private double calculate_distance(double[] p1, double[] p2)
        {
            double deltaX = p1[0] - p2[0];
            double deltaY = p1[1] - p2[1];
            double deltaZ = p1[2] - p2[2];
            
            return Math.Sqrt(deltaX^2 + deltaY^2 + deltaZ^2);
        }

        private double[][] vectorize(string[] texts)
        {
            string[][] words = Accord.MachineLearning.Tools.Tokenize(texts);
            var vectorizer = new Accord.MachineLearning.TFIDF()
            {
                    Tf = Accord.MachineLearning.TermFrequency.Log,
                    Idf = Accord.MachineLearning.InverseDocumentFrequency.Default
            };

            vectorizer.Learn(words);

            double[][] transformed = vectorizer.Transform(words);

            return transformed;
        }

        private double[][] reduce_dimensionality(double[][] vectors)
        {
            var tsne = new Accord.MachineLearning.Clustering.TSNE()
        {
            NumberOfOutputs = 3,
            Perplexity = 0.99
        };

        double[][] result = tsne.Transform(vectors);

        foreach (var point in result)
        {
            MessageBox.Show($"({point[0]}, {point[1]}, {point[2]})");
        }
        return result;
        }
    }
}
