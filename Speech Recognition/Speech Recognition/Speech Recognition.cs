using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;

namespace Speech_Recognition
{
    public partial class Form1 : Form
    {
        SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnEnable_Click(object sender, EventArgs e)
        {
            recEngine.RecognizeAsync(RecognizeMode.Multiple);
            btnDisable.Enabled = true;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Choices commands = new Choices();
            commands.Add(new string[] { "Say hello", "Print my name", "Tell me a joke" });
            GrammarBuilder gBuilder = new GrammarBuilder();
            gBuilder.Append(commands);
            Grammar grammar = new Grammar(gBuilder);
            recEngine.SpeechRecognized += recEngine_SpeechRecognised;
            recEngine.LoadGrammarAsync(grammar);
            recEngine.SetInputToDefaultAudioDevice();
        }

        void recEngine_SpeechRecognised(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text)
            {
                case "Say hello":
                    MessageBox.Show("Hi Sansi. How are you?");
                    break;
                case "Print my name":
                    richTextBox1.Text += "\nSansi";
                    break;
                case "Tell me a joke":
                    richTextBox1.Text += "\nWhat do you call a warm sunny day that comes after two rainy days?";
                    richTextBox1.Text += "\n...";
                    System.Threading.Thread.Sleep(3000);
                    richTextBox1.Text += "\nMonday!";
                    break;
            }
                
        }

        private void btnDisable_Click(object sender, EventArgs e)
        {
            recEngine.RecognizeAsyncStop();
            btnEnable.Enabled = false;
        }
    }
}
