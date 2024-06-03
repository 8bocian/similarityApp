using System;

using Accord;
using Accord.MachineLearning;
using System.Windows.Forms;

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
            MessageBox.Show("Button Clicked!");
            vectorize(["", ""]);
        }

        private void vectorize(string[] texts)
        {
            // Example array of strings
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

            // Learn the TF-IDF from the given documents
            vectorizer.Learn(words);

            // Transform the documents into their TF-IDF representation
            double[][] transformed = vectorizer.Transform(words);

            // Display the transformed vectors
            for (int i = 0; i < transformed.Length; i++)
            {
                Console.WriteLine($"Document {i + 1}:");
                Console.WriteLine(string.Join(", ", transformed[i]));
                Console.WriteLine();
            }
        }

        // private void reduce_dimensionality(double[][] vectors)
        // {
        //     var tsne = new TSNE<Distance.SquareEuclidean>()
        // {
        //     NumberOfOutputs = 3 // Target dimensionality
        // };

        // // Compute the reduction
        // double[][] result = tsne.Transform(data);

        // // Print the results
        // foreach (var point in result)
        // {
        //     Console.WriteLine($"({point[0]}, {point[1]}, {point[2]})");
        // }
        // }
    }
}