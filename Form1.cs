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
            string[] documents = {
                @"This is the first document.",
                @"This document is the second document.",
                @"And this is the third one.",
                @"Is this the first document?"
            };
        }

        private void create_list(string[] client_products)
        {
            double[][] vecotrs_xd = vectorize(client_products);
            double[][] vectors_3d = reduce_dimensionality(vectors);

            Queue top_10 = new Queue(10);

            foreach (var client_product in client_products){
                foreach (var central_product in central_products){
                    calculate_distance(client_product, central_product);
                    
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
            string[] documents = {
                @"This is the first document.",
                @"This document is the second document.",
                @"And this is the third one.",
                @"Is this the first document?"
            };

            string[][] words = Accord.MachineLearning.Tools.Tokenize(documents);
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
