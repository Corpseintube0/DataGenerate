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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbBoxTableNames = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblDbName = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.dataGridViewColumns = new System.Windows.Forms.DataGridView();
            this.ColCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDataType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColGenType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.grpBoxTables.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewColumns)).BeginInit();
            this.SuspendLayout();
            // 
            // grpBoxTables
            // 
            this.grpBoxTables.Controls.Add(this.label1);
            this.grpBoxTables.Controls.Add(this.cmbBoxTableNames);
            this.grpBoxTables.Location = new System.Drawing.Point(3, 66);
            this.grpBoxTables.Name = "grpBoxTables";
            this.grpBoxTables.Size = new System.Drawing.Size(523, 75);
            this.grpBoxTables.TabIndex = 10;
            this.grpBoxTables.TabStop = false;
            this.grpBoxTables.Text = "Таблицы и столбцы";
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
            // cmbBoxTableNames
            // 
            this.cmbBoxTableNames.FormattingEnabled = true;
            this.cmbBoxTableNames.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmbBoxTableNames.Location = new System.Drawing.Point(9, 44);
            this.cmbBoxTableNames.Name = "cmbBoxTableNames";
            this.cmbBoxTableNames.Size = new System.Drawing.Size(211, 21);
            this.cmbBoxTableNames.TabIndex = 4;
            this.cmbBoxTableNames.SelectedIndexChanged += new System.EventHandler(this.cmbBoxTableNames_SelectedIndexChanged);
            cmbBoxTableNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.96936F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.03064F));
            this.tableLayoutPanel.Controls.Add(this.grpBoxTables, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.panel2, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.dataGridViewColumns, 0, 2);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 4;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.64531F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 338F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(1018, 541);
            this.tableLayoutPanel.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblDbName);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(239, 57);
            this.panel1.TabIndex = 11;
            // 
            // lblDbName
            // 
            this.lblDbName.AutoSize = true;
            this.lblDbName.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDbName.Location = new System.Drawing.Point(9, 19);
            this.lblDbName.Name = "lblDbName";
            this.lblDbName.Size = new System.Drawing.Size(75, 25);
            this.lblDbName.TabIndex = 3;
            this.lblDbName.Text = "No DB";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnUpdate);
            this.panel2.Controls.Add(this.btnGenerate);
            this.panel2.Location = new System.Drawing.Point(532, 147);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(483, 332);
            this.panel2.TabIndex = 13;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(135, 275);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(104, 35);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(18, 275);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(111, 35);
            this.btnGenerate.TabIndex = 0;
            this.btnGenerate.Text = "Генерировать";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // dataGridViewColumns
            // 
            this.dataGridViewColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewColumns.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColCheck,
            this.ColName,
            this.ColDataType,
            this.ColGenType});
            this.dataGridViewColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewColumns.Location = new System.Drawing.Point(3, 147);
            this.dataGridViewColumns.Name = "dataGridViewColumns";
            this.dataGridViewColumns.Size = new System.Drawing.Size(523, 332);
            this.dataGridViewColumns.TabIndex = 14;
            //dataGridViewColumns.ReadOnly = true;
            dataGridViewColumns.Columns[1].ReadOnly = true;
            dataGridViewColumns.Columns[2].ReadOnly = true;
            // 
            // ColCheck
            // 
            this.ColCheck.Frozen = true;
            this.ColCheck.HeaderText = "";
            this.ColCheck.Name = "ColCheck";
            this.ColCheck.Width = 30;
            // 
            // ColName
            // 
            this.ColName.Frozen = true;
            this.ColName.HeaderText = "Имя столбца";
            this.ColName.Name = "ColName";
            this.ColName.Width = 200;
            // 
            // ColDataType
            // 
            this.ColDataType.Frozen = true;
            this.ColDataType.HeaderText = "Тип данных";
            this.ColDataType.Name = "ColDataType";
            // 
            // ColGenType
            // 
            this.ColGenType.HeaderText = "Тип генератора";
            this.ColGenType.Name = "ColGenType";
            this.ColGenType.Width = 150;
            // 
            // formMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 541);
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "formMain";
            this.Text = "Генератор данных";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formMain_FormClosing);
            this.Load += new System.EventHandler(this.formMain_Load);
            this.grpBoxTables.ResumeLayout(false);
            this.grpBoxTables.PerformLayout();
            this.tableLayoutPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewColumns)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBoxTables;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbBoxTableNames;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblDbName;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.DataGridView dataGridViewColumns;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDataType;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColGenType;
    }
}