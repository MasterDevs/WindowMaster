namespace WindowMasterLib {
	partial class ImportExportForm {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportExportForm));
			this.bExport = new System.Windows.Forms.Button();
			this.bImport = new System.Windows.Forms.Button();
			this.openDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveDialog = new System.Windows.Forms.SaveFileDialog();
			this.SuspendLayout();
			// 
			// bExport
			// 
			this.bExport.Location = new System.Drawing.Point(136, 13);
			this.bExport.Name = "bExport";
			this.bExport.Size = new System.Drawing.Size(75, 23);
			this.bExport.TabIndex = 0;
			this.bExport.Text = "Export";
			this.bExport.UseVisualStyleBackColor = true;
			this.bExport.Click += new System.EventHandler(this.bExport_Click);
			// 
			// bImport
			// 
			this.bImport.Location = new System.Drawing.Point(12, 13);
			this.bImport.Name = "bImport";
			this.bImport.Size = new System.Drawing.Size(75, 23);
			this.bImport.TabIndex = 1;
			this.bImport.Text = "Import";
			this.bImport.UseVisualStyleBackColor = true;
			this.bImport.Click += new System.EventHandler(this.bImport_Click);
			// 
			// openDialog
			// 
			this.openDialog.Filter = "WindowMaster Config File (*.xml) | *.xml";
			// 
			// saveDialog
			// 
			this.saveDialog.Filter = "WindowMaster Config File (*.xml) | *.xml";
			// 
			// ImportExportForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(223, 48);
			this.Controls.Add(this.bImport);
			this.Controls.Add(this.bExport);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ImportExportForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Import / Export";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button bExport;
		private System.Windows.Forms.Button bImport;
		private System.Windows.Forms.OpenFileDialog openDialog;
		private System.Windows.Forms.SaveFileDialog saveDialog;
	}
}