namespace WindowMasterLib {
	partial class ActionForm {
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
			this.bOK = new System.Windows.Forms.Button();
			this.bCancel = new System.Windows.Forms.Button();
			this.propertyGrid = new System.Windows.Forms.PropertyGrid();
			this.panelProperty = new System.Windows.Forms.Panel();
			this.lbActions = new System.Windows.Forms.ListBox();
			this.tbDescription = new System.Windows.Forms.TextBox();
			this.panelNameDescr = new System.Windows.Forms.Panel();
			this.lActionName = new System.Windows.Forms.Label();
			this.panelProperty.SuspendLayout();
			this.panelNameDescr.SuspendLayout();
			this.SuspendLayout();
			// 
			// bOK
			// 
			this.bOK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.bOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.bOK.Location = new System.Drawing.Point(12, 428);
			this.bOK.Name = "bOK";
			this.bOK.Size = new System.Drawing.Size(75, 23);
			this.bOK.TabIndex = 0;
			this.bOK.Text = "&OK";
			this.bOK.UseVisualStyleBackColor = true;
			this.bOK.Click += new System.EventHandler(this.bOK_Click);
			// 
			// bCancel
			// 
			this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.bCancel.Location = new System.Drawing.Point(198, 428);
			this.bCancel.Name = "bCancel";
			this.bCancel.Size = new System.Drawing.Size(75, 23);
			this.bCancel.TabIndex = 1;
			this.bCancel.Text = "&Cancel";
			this.bCancel.UseVisualStyleBackColor = true;
			// 
			// propertyGrid
			// 
			this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyGrid.Location = new System.Drawing.Point(0, 0);
			this.propertyGrid.Name = "propertyGrid";
			this.propertyGrid.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
			this.propertyGrid.Size = new System.Drawing.Size(257, 212);
			this.propertyGrid.TabIndex = 3;
			// 
			// panelProperty
			// 
			this.panelProperty.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.panelProperty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panelProperty.Controls.Add(this.propertyGrid);
			this.panelProperty.Location = new System.Drawing.Point(12, 204);
			this.panelProperty.Name = "panelProperty";
			this.panelProperty.Size = new System.Drawing.Size(261, 216);
			this.panelProperty.TabIndex = 4;
			// 
			// lbActions
			// 
			this.lbActions.FormattingEnabled = true;
			this.lbActions.Location = new System.Drawing.Point(12, 77);
			this.lbActions.Name = "lbActions";
			this.lbActions.Size = new System.Drawing.Size(261, 121);
			this.lbActions.TabIndex = 3;
			this.lbActions.SelectedIndexChanged += new System.EventHandler(this.lbActions_SelectedIndexChanged);
			// 
			// tbDescription
			// 
			this.tbDescription.Location = new System.Drawing.Point(12, 31);
			this.tbDescription.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
			this.tbDescription.Multiline = true;
			this.tbDescription.Name = "tbDescription";
			this.tbDescription.ReadOnly = true;
			this.tbDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbDescription.Size = new System.Drawing.Size(261, 37);
			this.tbDescription.TabIndex = 4;
			// 
			// panelNameDescr
			// 
			this.panelNameDescr.Controls.Add(this.lActionName);
			this.panelNameDescr.Controls.Add(this.tbDescription);
			this.panelNameDescr.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelNameDescr.Location = new System.Drawing.Point(0, 0);
			this.panelNameDescr.Name = "panelNameDescr";
			this.panelNameDescr.Size = new System.Drawing.Size(285, 71);
			this.panelNameDescr.TabIndex = 5;
			// 
			// lActionName
			// 
			this.lActionName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lActionName.Location = new System.Drawing.Point(79, 9);
			this.lActionName.Name = "lActionName";
			this.lActionName.Size = new System.Drawing.Size(127, 13);
			this.lActionName.TabIndex = 5;
			this.lActionName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ActionForm
			// 
			this.AcceptButton = this.bOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.bCancel;
			this.ClientSize = new System.Drawing.Size(285, 463);
			this.Controls.Add(this.lbActions);
			this.Controls.Add(this.panelNameDescr);
			this.Controls.Add(this.panelProperty);
			this.Controls.Add(this.bCancel);
			this.Controls.Add(this.bOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "ActionForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Action Configuration";
			this.panelProperty.ResumeLayout(false);
			this.panelNameDescr.ResumeLayout(false);
			this.panelNameDescr.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button bOK;
		private System.Windows.Forms.Button bCancel;
		private System.Windows.Forms.PropertyGrid propertyGrid;
		private System.Windows.Forms.Panel panelProperty;
		private System.Windows.Forms.ListBox lbActions;
		private System.Windows.Forms.TextBox tbDescription;
		private System.Windows.Forms.Panel panelNameDescr;
		private System.Windows.Forms.Label lActionName;
	}
}