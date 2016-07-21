namespace GUI
{
    partial class formMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpBoxTables = new System.Windows.Forms.GroupBox();
            this.cmbBoxTableNames = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblDbName = new System.Windows.Forms.Label();
            this.grpBoxTables.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBoxTables
            // 
            this.grpBoxTables.Controls.Add(this.label1);
            this.grpBoxTables.Controls.Add(this.cmbBoxTableNames);
            this.grpBoxTables.Location = new System.Drawing.Point(3, 67);
            this.grpBoxTables.Name = "grpBoxTables";
            this.grpBoxTables.Size = new System.Drawing.Size(239, 206);
            this.grpBoxTables.TabIndex = 10;
            this.grpBoxTables.TabStop = false;
            this.grpBoxTables.Text = "Таблицы и столбцы";
            // 
            // cmbBoxTableNames
            // 
            this.cmbBoxTableNames.FormattingEnabled = true;
            this.cmbBoxTableNames.Location = new System.Drawing.Point(9, 44);
            this.cmbBoxTableNames.Name = "cmbBoxTableNames";
            this.cmbBoxTableNames.Size = new System.Drawing.Size(121, 21);
            this.cmbBoxTableNames.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Выберите таблицу";
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.48705F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 57.51295F));
            this.tableLayoutPanel.Controls.Add(this.grpBoxTables, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.64531F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85.35469F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(579, 463);
            this.tableLayoutPanel.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblDbName);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(239, 58);
            this.panel1.TabIndex = 11;
            // 
            // lblDbName
            // 
            this.lblDbName.AutoSize = true;
            this.lblDbName.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDbName.Location = new System.Drawing.Point(9, 19);
            this.lblDbName.Name = "lblDbName";
            this.lblDbName.Size = new System.Drawing.Size(63, 21);
            this.lblDbName.TabIndex = 3;
            this.lblDbName.Text = "No DB";
            // 
            // formMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 463);
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "formMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formMain_FormClosing);
            this.Load += new System.EventHandler(this.formMain_Load);
            this.grpBoxTables.ResumeLayout(false);
            this.grpBoxTables.PerformLayout();
            this.tableLayoutPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBoxTables;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbBoxTableNames;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblDbName;
    }
}