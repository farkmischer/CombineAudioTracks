namespace CombineAudioTracks
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            combineTracksButton = new Button();
            selectFilesButton = new Button();
            selectedFileListBox = new ListBox();
            selectDirectoryButton = new Button();
            selectedPathTextBox = new TextBox();
            SuspendLayout();
            // 
            // combineTracksButton
            // 
            combineTracksButton.Location = new Point(12, 361);
            combineTracksButton.Name = "combineTracksButton";
            combineTracksButton.Size = new Size(75, 23);
            combineTracksButton.TabIndex = 0;
            combineTracksButton.Text = "Combine Tracks";
            combineTracksButton.UseVisualStyleBackColor = true;
            combineTracksButton.Click += combineTracksButton_Click;
            // 
            // selectFilesButton
            // 
            selectFilesButton.Location = new Point(12, 112);
            selectFilesButton.Name = "selectFilesButton";
            selectFilesButton.Size = new Size(75, 23);
            selectFilesButton.TabIndex = 1;
            selectFilesButton.Text = "Select Files";
            selectFilesButton.UseVisualStyleBackColor = true;
            selectFilesButton.Click += selectFilesButton_Click;
            // 
            // selectedFileListBox
            // 
            selectedFileListBox.FormattingEnabled = true;
            selectedFileListBox.ItemHeight = 15;
            selectedFileListBox.Location = new Point(12, 141);
            selectedFileListBox.Name = "selectedFileListBox";
            selectedFileListBox.Size = new Size(1090, 214);
            selectedFileListBox.TabIndex = 2;
            // 
            // selectDirectoryButton
            // 
            selectDirectoryButton.Location = new Point(12, 29);
            selectDirectoryButton.Name = "selectDirectoryButton";
            selectDirectoryButton.Size = new Size(142, 23);
            selectDirectoryButton.TabIndex = 3;
            selectDirectoryButton.Text = "Select Directory";
            selectDirectoryButton.UseVisualStyleBackColor = true;
            selectDirectoryButton.Click += selectDirectoryButton_Click;
            // 
            // selectedPathTextBox
            // 
            selectedPathTextBox.Location = new Point(12, 73);
            selectedPathTextBox.Name = "selectedPathTextBox";
            selectedPathTextBox.Size = new Size(596, 23);
            selectedPathTextBox.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1126, 450);
            Controls.Add(selectedPathTextBox);
            Controls.Add(selectDirectoryButton);
            Controls.Add(selectedFileListBox);
            Controls.Add(selectFilesButton);
            Controls.Add(combineTracksButton);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button combineTracksButton;
        private Button selectFilesButton;
        private ListBox selectedFileListBox;
        private Button selectDirectoryButton;
        private TextBox selectedPathTextBox;
    }
}
