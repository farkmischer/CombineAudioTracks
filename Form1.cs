using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
namespace CombineAudioTracks
{
    public partial class Form1 : Form
    {
        private int dragIndex;
        private bool dragging;
        private string selectedPath;
        public BindingList<SelectedFile> selectedFiles = new BindingList<SelectedFile>();

        public Form1()
        {
            InitializeComponent();

            // Ensure FFmpeg libraries are loaded
            // string ffmpegPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ffmpeg");
            // C:\Users\admin\AppData\Local\Microsoft\WinGet\Packages\Gyan.FFmpeg_Microsoft.Winget.Source_8wekyb3d8bbwe\ffmpeg-7.1-full_build\bin
            // string ffmpegPath = Path.Combine(@"C:\Users\admin\AppData\Local\Microsoft\WinGet\Packages\Gyan.FFmpeg_Microsoft.Winget.Source_8wekyb3d8bbwe\ffmpeg-7.1-full_build\bin", "ffmpeg");
            // Environment.SetEnvironmentVariable("PATH", Environment.GetEnvironmentVariable("PATH") + ";" + ffmpegPath);

            selectedFileListBox.AllowDrop = true;
            selectedFileListBox.DataSource = new BindingSource { DataSource = selectedFiles };
            selectedFileListBox.DisplayMember = "FileName";
            selectedFileListBox.ValueMember = "FileNameAndPath";

            selectedFileListBox.MouseDown += selectedFileListBox_MouseDown;
            selectedFileListBox.MouseMove += selectedFileListBox_MouseMove;
            selectedFileListBox.DragOver += selectedFileListBox_DragOver;
            selectedFileListBox.DragDrop += selectedFileListBox_DragDrop;
        }

        private void combineTracksButton_Click(object sender, EventArgs e)
        {
            //string inputFilePath = "videos.txt"; // Path to your text file
            

            // Create file that will be the input to FFMpeg
            var todaysDate = DateTime.Now.ToShortDateString().Replace("/", "-");
            string inputFilePath = @$"C:\temp\{todaysDate}_AudioFilesToCombine_{Guid.NewGuid().ToString()}.txt";
            string outputFilePath = @$"C:\temp\{todaysDate}_CombinedAudioFile_{Guid.NewGuid().ToString()}.m4a"; // Path to the output file

            // var combinedFilePaths = string.Join(",/n", selectedFiles.Select(s => s.FileNameAndPath));
            // string combinedFilePaths = string.Empty;
            StringBuilder combinedFilePaths = new StringBuilder();

            foreach (var fileNameAndPath in selectedFiles.Select(s => s.FileNameAndPath))
            {
                combinedFilePaths.AppendLine($"file '{fileNameAndPath}'");
            }

            // Create the file
            using (FileStream fs = File.Create(inputFilePath))
            {
                // Write text to the file
                byte[] info = new UTF8Encoding(true).GetBytes(combinedFilePaths.ToString());
                fs.Write(info, 0, info.Length);
            }

            // Read and display the file content
            //using (StreamReader sr = File.OpenText(inputFilePath))
            //{
            //    string s = "";
            //    while ((s = sr.ReadLine()) != null)
            //    {
            //        MessageBox.Show(s);
            //    }
            //}

            // Build the FFmpeg command
            string ffmpegCommand = $"-f concat -safe 0 -i {inputFilePath} -c copy {outputFilePath}";

            // Console.WriteLine(ffmpegCommand);

            // Execute the command
            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                // WorkingDirectory = @"C:\Users\admin\AppData\Local\Microsoft\WinGet\Packages\Gyan.FFmpeg_Microsoft.Winget.Source_8wekyb3d8bbwe\ffmpeg-7.1-full_build\bin\ffmpeg.exe",
                // FileName = "ffmpeg",
                FileName = @"C:\Users\admin\AppData\Local\Microsoft\WinGet\Packages\Gyan.FFmpeg_Microsoft.Winget.Source_8wekyb3d8bbwe\ffmpeg-7.1-full_build\bin\ffmpeg.exe",
                Arguments = ffmpegCommand,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            using (Process process = Process.Start(processStartInfo))
            {
                process.OutputDataReceived += (sender, e) => Console.WriteLine(e.Data);
                process.ErrorDataReceived += (sender, e) => Console.WriteLine(e.Data);
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit();
            }

            // Delete the file
            if (File.Exists(inputFilePath))
            {
                File.Delete(inputFilePath);
                // MessageBox.Show("File deleted.");
            }
        }

        private void selectFilesButton_Click(object sender, EventArgs e)
        {
            // Create a new instance of the OpenFileDialog
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // Set properties to allow multiple file selection
                openFileDialog.Multiselect = true;
                openFileDialog.ValidateNames = false;
                openFileDialog.CheckFileExists = false;
                openFileDialog.CheckPathExists = true;
                openFileDialog.FileName = "Folder Selection.";
                openFileDialog.InitialDirectory = selectedPath;

                // Show the dialog and check if the user clicked the OK button
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the selected file paths
                    string[] fileNames = openFileDialog.SafeFileNames;

                    // Create a HashSet to store unique folder paths
                    HashSet<string> folderPaths = new HashSet<string>();

                    // Extract the folder paths from the selected files
                    foreach (string fileName in fileNames)
                    {
                        selectedFiles.Add(new SelectedFile() { FileName = fileName, FileNameAndPath = $"{selectedPath}\\{fileName}" });
                        
                        //string folderPath = Path.GetDirectoryName(fileName);
                        //folderPaths.Add(folderPath);
                    }

                    //// Display the selected folder paths
                    //foreach (string folderPath in folderPaths)
                    //{
                    //    Console.WriteLine("Selected folder: " + folderPath);
                    //}
                }
            }
        }

        private void selectedFileListBox_MouseDown(object sender, MouseEventArgs e)
        {
            dragIndex = selectedFileListBox.IndexFromPoint(e.X, e.Y);
            dragging = dragIndex >= 0;
        }

        private void selectedFileListBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging && e.Button == MouseButtons.Left)
            {
                selectedFileListBox.DoDragDrop(selectedFileListBox.Items[dragIndex], DragDropEffects.Move);
                dragging = false;
            }
        }

        private void selectedFileListBox_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void selectedFileListBox_DragDrop(object sender, DragEventArgs e)
        {
            Point point = selectedFileListBox.PointToClient(new Point(e.X, e.Y));
            int index = selectedFileListBox.IndexFromPoint(point);

            if (index < 0) index = selectedFileListBox.Items.Count - 1;

            object data = e.Data.GetData(typeof(string));
            selectedFileListBox.Items.Remove(data);
            selectedFileListBox.Items.Insert(index, data);
        }

        private void selectDirectoryButton_Click(object sender, EventArgs e)
        {
            // Create a new instance of the OpenFileDialog
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                // Show the dialog and check if the user clicked the OK button
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the selected file paths
                    selectedPath = folderBrowserDialog.SelectedPath;

                    selectedPathTextBox.Text = selectedPath;
                }
            }
        }
    }

    public class SelectedFile
    {
        public string FileName { get; set; }
        public string FileNameAndPath { get; set; }
    }
}
