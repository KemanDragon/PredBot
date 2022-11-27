
namespace OriBotV3 {
	partial class BotWindow {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.ProgramLog = new System.Windows.Forms.RichTextBox();
            this.PingSoundOption_Bot = new System.Windows.Forms.CheckBox();
            this.BtnSendCommand = new System.Windows.Forms.Button();
            this.PingSoundOption_Me = new System.Windows.Forms.CheckBox();
            this.LabelSound = new System.Windows.Forms.Label();
            this.CommandEntryBox = new System.Windows.Forms.TextBox();
            this.BtnClearLog = new System.Windows.Forms.Button();
            this.BoxScrollAlways = new System.Windows.Forms.CheckBox();
            this.BoxVerboseLog = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // ProgramLog
            // 
            this.ProgramLog.BackColor = System.Drawing.Color.Black;
            this.ProgramLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ProgramLog.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ProgramLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ProgramLog.ForeColor = System.Drawing.SystemColors.Control;
            this.ProgramLog.Location = new System.Drawing.Point(15, 14);
            this.ProgramLog.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ProgramLog.Name = "ProgramLog";
            this.ProgramLog.ReadOnly = true;
            this.ProgramLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.ProgramLog.Size = new System.Drawing.Size(1236, 441);
            this.ProgramLog.TabIndex = 0;
            this.ProgramLog.Text = "";
            this.ProgramLog.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.ProgramLog_LinkClicked);
            this.ProgramLog.TextChanged += new System.EventHandler(this.ProgramLog_TextChanged);
            // 
            // PingSoundOption_Bot
            // 
            this.PingSoundOption_Bot.AutoSize = true;
            this.PingSoundOption_Bot.ForeColor = System.Drawing.SystemColors.Control;
            this.PingSoundOption_Bot.Location = new System.Drawing.Point(14, 486);
            this.PingSoundOption_Bot.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PingSoundOption_Bot.Name = "PingSoundOption_Bot";
            this.PingSoundOption_Bot.Size = new System.Drawing.Size(149, 19);
            this.PingSoundOption_Bot.TabIndex = 2;
            this.PingSoundOption_Bot.Text = "Someone pings the bot";
            this.PingSoundOption_Bot.UseVisualStyleBackColor = true;
            // 
            // BtnSendCommand
            // 
            this.BtnSendCommand.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.BtnSendCommand.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnSendCommand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSendCommand.ForeColor = System.Drawing.SystemColors.ControlText;
            this.BtnSendCommand.Location = new System.Drawing.Point(1018, 463);
            this.BtnSendCommand.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnSendCommand.Name = "BtnSendCommand";
            this.BtnSendCommand.Size = new System.Drawing.Size(114, 69);
            this.BtnSendCommand.TabIndex = 3;
            this.BtnSendCommand.Text = "Send Command";
            this.BtnSendCommand.UseVisualStyleBackColor = false;
            this.BtnSendCommand.Click += new System.EventHandler(this.BtnSendCommand_Click);
            // 
            // PingSoundOption_Me
            // 
            this.PingSoundOption_Me.AutoSize = true;
            this.PingSoundOption_Me.ForeColor = System.Drawing.SystemColors.Control;
            this.PingSoundOption_Me.Location = new System.Drawing.Point(14, 509);
            this.PingSoundOption_Me.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PingSoundOption_Me.Name = "PingSoundOption_Me";
            this.PingSoundOption_Me.Size = new System.Drawing.Size(131, 19);
            this.PingSoundOption_Me.TabIndex = 4;
            this.PingSoundOption_Me.Text = "Someone pings you";
            this.PingSoundOption_Me.UseVisualStyleBackColor = true;
            // 
            // LabelSound
            // 
            this.LabelSound.AutoSize = true;
            this.LabelSound.ForeColor = System.Drawing.SystemColors.Control;
            this.LabelSound.Location = new System.Drawing.Point(14, 463);
            this.LabelSound.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelSound.Name = "LabelSound";
            this.LabelSound.Size = new System.Drawing.Size(115, 15);
            this.LabelSound.TabIndex = 5;
            this.LabelSound.Text = "Play a sound when...";
            // 
            // CommandEntryBox
            // 
            this.CommandEntryBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.CommandEntryBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CommandEntryBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CommandEntryBox.ForeColor = System.Drawing.Color.White;
            this.CommandEntryBox.Location = new System.Drawing.Point(214, 463);
            this.CommandEntryBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.CommandEntryBox.Multiline = true;
            this.CommandEntryBox.Name = "CommandEntryBox";
            this.CommandEntryBox.Size = new System.Drawing.Size(798, 69);
            this.CommandEntryBox.TabIndex = 6;
            this.CommandEntryBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDownCmdEntry);
            // 
            // BtnClearLog
            // 
            this.BtnClearLog.BackColor = System.Drawing.Color.Maroon;
            this.BtnClearLog.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnClearLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnClearLog.ForeColor = System.Drawing.SystemColors.ControlText;
            this.BtnClearLog.Location = new System.Drawing.Point(1150, 505);
            this.BtnClearLog.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnClearLog.Name = "BtnClearLog";
            this.BtnClearLog.Size = new System.Drawing.Size(100, 27);
            this.BtnClearLog.TabIndex = 8;
            this.BtnClearLog.Text = "Clear Log";
            this.BtnClearLog.UseVisualStyleBackColor = false;
            this.BtnClearLog.Click += new System.EventHandler(this.BtnClearLog_Click);
            // 
            // BoxScrollAlways
            // 
            this.BoxScrollAlways.AutoSize = true;
            this.BoxScrollAlways.Checked = true;
            this.BoxScrollAlways.CheckState = System.Windows.Forms.CheckState.Checked;
            this.BoxScrollAlways.ForeColor = System.Drawing.SystemColors.Control;
            this.BoxScrollAlways.Location = new System.Drawing.Point(1150, 482);
            this.BoxScrollAlways.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BoxScrollAlways.Name = "BoxScrollAlways";
            this.BoxScrollAlways.Size = new System.Drawing.Size(106, 19);
            this.BoxScrollAlways.TabIndex = 9;
            this.BoxScrollAlways.Text = "Stay At Bottom";
            this.BoxScrollAlways.UseVisualStyleBackColor = true;
            // 
            // BoxVerboseLog
            // 
            this.BoxVerboseLog.AutoSize = true;
            this.BoxVerboseLog.ForeColor = System.Drawing.SystemColors.Control;
            this.BoxVerboseLog.Location = new System.Drawing.Point(1150, 462);
            this.BoxVerboseLog.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BoxVerboseLog.Name = "BoxVerboseLog";
            this.BoxVerboseLog.Size = new System.Drawing.Size(90, 19);
            this.BoxVerboseLog.TabIndex = 10;
            this.BoxVerboseLog.Text = "Verbose Log";
            this.BoxVerboseLog.ThreeState = true;
            this.BoxVerboseLog.UseVisualStyleBackColor = true;
            this.BoxVerboseLog.CheckStateChanged += new System.EventHandler(this.BoxVerboseLog_CheckStateChanged);
            // 
            // BotWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.ClientSize = new System.Drawing.Size(1265, 546);
            this.Controls.Add(this.BoxVerboseLog);
            this.Controls.Add(this.BoxScrollAlways);
            this.Controls.Add(this.BtnClearLog);
            this.Controls.Add(this.CommandEntryBox);
            this.Controls.Add(this.LabelSound);
            this.Controls.Add(this.PingSoundOption_Me);
            this.Controls.Add(this.BtnSendCommand);
            this.Controls.Add(this.PingSoundOption_Bot);
            this.Controls.Add(this.ProgramLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "BotWindow";
            this.ShowIcon = false;
            this.Text = "Ori Bot Interface";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.RichTextBox ProgramLog;
		private System.Windows.Forms.Button BtnSendCommand;
		private System.Windows.Forms.Label LabelSound;
		private System.Windows.Forms.TextBox CommandEntryBox;
		private System.Windows.Forms.Button BtnClearLog;
		private System.Windows.Forms.CheckBox BoxVerboseLog;
		public System.Windows.Forms.CheckBox BoxScrollAlways;
		public System.Windows.Forms.CheckBox PingSoundOption_Bot;
		public System.Windows.Forms.CheckBox PingSoundOption_Me;
	}
}

