// <copyright file="FormTranscriber.cs" company="Shkyrockett" >
//     Copyright © 2020 - 2022 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks>
// </remarks>

using AudioFileTranscriptionPlayground.Properties;
using NAudio.Wave;
using System.Speech.Recognition;
using System.Text;

namespace AudioFileTranscriptionPlayground
{
    /// <summary>
    /// The form1.
    /// </summary>
    public partial class FormTranscriber
        : Form
    {
        private static bool completed;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormTranscriber"/> class.
        /// </summary>
        public FormTranscriber()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Forms the transcriber_ load.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void FormTranscriber_Load(object sender, EventArgs e)
        {
            textBoxFile.Text = Settings.Default.SelectedFile;
        }

        /// <summary>
        /// Forms the transcriber_ form closed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void FormTranscriber_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Default.SelectedFile = textBoxFile.Text;
            Settings.Default.Save();
        }

        /// <summary>
        /// ButtonSelectFile click event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void ButtonSelectFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Path.GetDirectoryName(textBoxFile.Text);
            openFileDialog1.FileName = textBoxFile.Text;
            switch (openFileDialog1.ShowDialog())
            {
                case DialogResult.OK:
                case DialogResult.Yes:
                    textBoxFile.Text = openFileDialog1.FileName;
                    textBoxTranscript.Text = string.Empty;
                    break;
                case DialogResult.TryAgain:
                case DialogResult.Continue:
                case DialogResult.No:
                case DialogResult.Cancel:
                case DialogResult.Abort:
                case DialogResult.Retry:
                case DialogResult.Ignore:
                case DialogResult.None:
                default:
                    break;
            }
        }

        /// <summary>
        /// ButtonExecute click event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private async void ButtonExecute_Click(object sender, EventArgs e)
        {
            buttonSelectFile.Enabled = false;
            buttonExecute.Enabled = false;
            buttonTranscribe.Enabled = false;
            buttonStopTranscription.Enabled = true;
            textBoxFile.Enabled = false;
            textBoxTranscript.Enabled = false;
            textBoxTranscript.PlaceholderText = $"Processing: {Path.GetFileName(textBoxFile.Text)}";
            textBoxTranscript.Text = string.Empty;
            buttonSaveResultsTextBox.Enabled = textBoxTranscript.Text.Length > 0;
            progressBar.Visible = true;

            //await Task.Run(OldMethod);
            await Task.Run(LoopBasedTranscriptionMethod);

            progressBar.Visible = false;
            textBoxTranscript.PlaceholderText = "Awaiting audio file selection...";
            textBoxTranscript.Enabled = true;
            textBoxFile.Enabled = true;
            buttonStopTranscription.Enabled = false;
            buttonTranscribe.Enabled = true;
            buttonExecute.Enabled = true;
            buttonSelectFile.Enabled = true;
            buttonSaveResultsTextBox.Enabled = textBoxTranscript.Text.Length > 0;
        }

        /// <summary>
        /// buttons the transcribe_ click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private async void ButtonTranscribe_Click(object sender, EventArgs e)
        {
            buttonSelectFile.Enabled = false;
            buttonExecute.Enabled = false;
            buttonTranscribe.Enabled = false;
            buttonStopTranscription.Enabled = true;
            textBoxFile.Enabled = false;
            textBoxTranscript.Enabled = false;
            textBoxTranscript.PlaceholderText = $"Processing: {Path.GetFileName(textBoxFile.Text)}";
            textBoxTranscript.Text = string.Empty;
            buttonSaveResultsTextBox.Enabled = textBoxTranscript.Text.Length > 0;
            progressBar.Visible = true;

            //await Task.Run(OldMethod);
            await Task.Run(EventBasedTranscriptionMethod);

            progressBar.Visible = false;
            textBoxTranscript.PlaceholderText = "Awaiting audio file selection...";
            textBoxTranscript.Enabled = true;
            textBoxFile.Enabled = true;
            buttonStopTranscription.Enabled = false;
            buttonTranscribe.Enabled = true;
            buttonExecute.Enabled = true;
            buttonSelectFile.Enabled = true;
            buttonSaveResultsTextBox.Enabled = textBoxTranscript.Text.Length > 0;
        }

        /// <summary>
        /// Buttons the stop transcription_ click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void ButtonStopTranscription_Click(object sender, EventArgs e)
        {
            completed = true;
            buttonStopTranscription.Enabled = false;
        }

        /// <summary>
        /// Buttons the save results text box_ click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void ButtonSaveResultsTextBox_Click(object sender, EventArgs e)
        {
            var file = textBoxFile.Text;
            var path = Path.GetDirectoryName(file);
            var filename = Path.GetFileNameWithoutExtension(file);
            var textFile = Path.Combine(path, $"{filename}.txt");

            saveFileDialog1.InitialDirectory = Path.GetDirectoryName(textFile);
            saveFileDialog1.FileName = textFile;
            switch (saveFileDialog1.ShowDialog())
            {
                case DialogResult.OK:
                case DialogResult.Yes:
                    if (File.Exists(textFile))
                    {
                        switch (MessageBox.Show($"Would you like to overwrite the file {textFile}?", "Overwrite .txt file?", MessageBoxButtons.YesNo))
                        {
                            case DialogResult.Yes:
                                File.Delete(textFile);
                                break;
                            default:
                                return;
                        }
                    }

                    File.WriteAllText(textFile, textBoxTranscript.Text);
                    break;
                case DialogResult.TryAgain:
                case DialogResult.Continue:
                case DialogResult.No:
                case DialogResult.Cancel:
                case DialogResult.Abort:
                case DialogResult.Retry:
                case DialogResult.Ignore:
                case DialogResult.None:
                default:
                    break;
            }
        }

        /// <summary>
        /// The while loop based transcription method.
        /// </summary>
        private void LoopBasedTranscriptionMethod()
        {
            var file = textBoxFile.Text;
            var path = Path.GetDirectoryName(file);
            var filename = Path.GetFileNameWithoutExtension(file);
            var extension = Path.GetExtension(file);
            var textFile = Path.Combine(path, $"{filename}.txt");

            using SpeechRecognitionEngine recognizer = new();
            Grammar dictation = new DictationGrammar { Name = "Dictation Grammar" };
            recognizer.LoadGrammar(dictation);

            if (extension.Equals(".wav", StringComparison.OrdinalIgnoreCase))
            {
                textBoxTranscript.PlaceholderText = $"Transcribing {Path.GetFileName(file)} file…";
                var text = GetText(recognizer, file).ToString();

                if (textBoxTranscript.InvokeRequired)
                {
                    Invoke((MethodInvoker)(() => textBoxTranscript.Text = text));
                }
                else
                {
                    textBoxTranscript.Text = text;
                }

                if (File.Exists(textFile))
                {
                    switch (MessageBox.Show($"Would you like to overwrite the file {textFile}?", "Overwrite .txt file?", MessageBoxButtons.YesNo))
                    {
                        case DialogResult.Yes:
                            File.Delete(textFile);
                            break;
                        default:
                            return;
                    }
                }

                File.WriteAllText(textFile, text);
            }
            else if (extension.Equals(".mp3", StringComparison.OrdinalIgnoreCase))
            {
                //using var mp3 = new Mp3FileReader(file);
                //using var stream = WaveFormatConversionStream.CreatePcmStream(mp3);
                //sre.SetInputToWaveStream(stream);

                string newFile = Path.Combine(path, $"{filename}.wav");
                if (Path.Exists(newFile))
                {
                    switch (MessageBox.Show($"Would you like to overwrite the file {newFile}?", "Overwrite .wav file?", MessageBoxButtons.YesNoCancel))
                    {
                        case DialogResult.Yes:
                            File.Delete(newFile);
                            break;
                        case DialogResult.No:
                            goto SkipConversion;
                        default:
                            return;
                    }
                }

                textBoxTranscript.PlaceholderText = $"Converting {Path.GetFileName(file)} to {Path.GetFileName(newFile)}…";
                ConvertMp3ToWav(file, newFile);

            SkipConversion:

                textBoxTranscript.PlaceholderText = $"Transcribing {Path.GetFileName(newFile)} file…";
                var text = GetText(recognizer, newFile).ToString();

                if (textBoxTranscript.InvokeRequired)
                {
                    Invoke((MethodInvoker)(() => textBoxTranscript.Text = text));
                }
                else
                {
                    textBoxTranscript.Text = text;
                }

                if (File.Exists(textFile))
                {
                    switch (MessageBox.Show($"Would you like to overwrite the file {textFile}?", "Overwrite .txt file?", MessageBoxButtons.YesNo))
                    {
                        case DialogResult.Yes:
                            File.Delete(textFile);
                            break;
                        default:
                            return;
                    }
                }

                File.WriteAllText(textFile, text);
            }
        }

        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <param name="recognizer">The recognizer.</param>
        /// <param name="wavFile"></param>
        /// <returns>A StringBuilder.</returns>
        private static StringBuilder GetText(SpeechRecognitionEngine recognizer, string wavFile)
        {
            recognizer.SetInputToWaveFile(wavFile);
            recognizer.BabbleTimeout = new TimeSpan(int.MaxValue);
            recognizer.InitialSilenceTimeout = new TimeSpan(int.MaxValue);
            recognizer.EndSilenceTimeout = new TimeSpan(100000000);
            recognizer.EndSilenceTimeoutAmbiguous = new TimeSpan(100000000);

            StringBuilder sb = new();
            while (true)
            {
                try
                {
                    if (recognizer.Recognize()?.Text is not string recText)
                    {
                        break;
                    }

                    sb.AppendLine(recText);
                }
                catch (Exception)
                {
                    break;
                }
            }

            return sb;
        }

        /// <summary>
        /// The event based transcription method.
        /// </summary>
        private void EventBasedTranscriptionMethod()
        {
            var file = textBoxFile.Text;
            var path = Path.GetDirectoryName(file);
            var filename = Path.GetFileNameWithoutExtension(file);
            var extension = Path.GetExtension(file);
            //var textFile = Path.Combine(path, $"{filename}.txt");

            using SpeechRecognitionEngine recognizer = new();
            // Create and load a grammar.  
            Grammar dictation = new DictationGrammar { Name = "Dictation Grammar" };
            recognizer.LoadGrammar(dictation);

            if (extension.Equals(".wav", StringComparison.OrdinalIgnoreCase))
            {
                textBoxTranscript.PlaceholderText = $"Transcribing {Path.GetFileName(file)} file…";
                StartRecognition(recognizer, file);
            }
            else if (extension.Equals(".mp3", StringComparison.OrdinalIgnoreCase))
            {
                string newFile = Path.Combine(path, $"{filename}.wav");
                if (Path.Exists(newFile))
                {
                    switch (MessageBox.Show($"Would you like to overwrite the file {newFile}?", "Overwrite .wav file?", MessageBoxButtons.YesNoCancel))
                    {
                        case DialogResult.Yes:
                            File.Delete(newFile);
                            break;
                        case DialogResult.No:
                            goto SkipConversion;
                        default:
                            return;
                    }
                }

                textBoxTranscript.PlaceholderText = $"Converting {Path.GetFileName(file)} to {Path.GetFileName(newFile)}…";
                ConvertMp3ToWav(file, newFile);

            SkipConversion:

                textBoxTranscript.PlaceholderText = $"Transcribing {Path.GetFileName(newFile)} file…";
                StartRecognition(recognizer, newFile);
            }
        }

        /// <summary>
        /// Starts the recognition.
        /// </summary>
        /// <param name="recognizer">The recognizer.</param>
        /// <param name="wavFile"></param>
        private void StartRecognition(SpeechRecognitionEngine recognizer, string wavFile)
        {
            // Configure the input to the recognizer.  
            recognizer.SetInputToWaveFile(wavFile);

            // Attach event handlers for the results of recognition.  
            recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(Recognizer_SpeechRecognized);
            recognizer.RecognizeCompleted += new EventHandler<RecognizeCompletedEventArgs>(Recognizer_RecognizeCompleted);

            // Perform recognition on the entire file.  
            Console.WriteLine("Starting asynchronous recognition...");
            completed = false;

            while (!completed)
            {
                try
                {
                    if (recognizer.Recognize()?.Text is not string recText)
                    {
                        break;
                    }
                }
                catch (Exception)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Handle the SpeechRecognized event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void Recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result?.Text is not null)
            {
                if (textBoxTranscript.InvokeRequired)
                {
                    Invoke((MethodInvoker)(() => textBoxTranscript.Text += $"{e.Result.Text}{Environment.NewLine}"));
                    Invoke((MethodInvoker)(() => textBoxTranscript.SelectionStart = textBoxTranscript.Text.Length));
                    Invoke((MethodInvoker)(() => textBoxTranscript.ScrollToCaret()));
                }
                else
                {
                    textBoxTranscript.Text += e.Result.Text;
                    textBoxTranscript.SelectionStart = textBoxTranscript.Text.Length;
                    textBoxTranscript.ScrollToCaret();
                }
            }
        }

        /// <summary>
        /// Handle the RecognizeCompleted event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void Recognizer_RecognizeCompleted(object sender, RecognizeCompletedEventArgs e)
        {
            if (e.Error is not null)
            {
                Console.WriteLine("  Error encountered, {0}: {1}",
                e.Error.GetType().Name, e.Error.Message);
            }
            if (e.Cancelled)
            {
                Console.WriteLine("  Operation canceled.");
            }
            if (e.InputStreamEnded)
            {
                Console.WriteLine("  End of stream encountered.");
            }

            completed = true;
        }

        /// <summary>
        /// Converts the mp3 to wav.
        /// </summary>
        /// <param name="inPath">The _in path_.</param>
        /// <param name="outPath">The _out path_.</param>
        private static void ConvertMp3ToWav(string inPath, string outPath)
        {
            using Mp3FileReader mp3 = new(inPath);
            using WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(mp3);
            WaveFileWriter.CreateWaveFile(outPath, pcm);
        }
    }
}
