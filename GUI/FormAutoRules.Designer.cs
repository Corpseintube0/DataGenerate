namespace GUI
{
    partial class FormAutoRules
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
            this.components = new System.ComponentModel.Container();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonApplyNow = new System.Windows.Forms.Button();
            this.buttonApply = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridViewRules = new System.Windows.Forms.DataGridView();
            this.contextMenuRules = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.новоеПравило_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.сохранитьНаборПравилКакToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.загрузитьНаборПравилToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonRestoreDefault = new System.Windows.Forms.Button();
            this.ColumnCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColumnPriority = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnGenType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ColumnRule = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRules)).BeginInit();
            this.contextMenuRules.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(850, 38);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(85, 26);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(759, 38);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(85, 26);
            this.buttonOk.TabIndex = 2;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonApplyNow
            // 
            this.buttonApplyNow.Location = new System.Drawing.Point(9, 3);
            this.buttonApplyNow.Name = "buttonApplyNow";
            this.buttonApplyNow.Size = new System.Drawing.Size(228, 26);
            this.buttonApplyNow.TabIndex = 3;
            this.buttonApplyNow.Text = "Применить правила к текущему проекту";
            this.buttonApplyNow.UseVisualStyleBackColor = true;
            // 
            // buttonApply
            // 
            this.buttonApply.Enabled = false;
            this.buttonApply.Location = new System.Drawing.Point(668, 38);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(85, 26);
            this.buttonApply.TabIndex = 4;
            this.buttonApply.Text = "Применить";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridViewRules);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.buttonRestoreDefault);
            this.splitContainer1.Panel2.Controls.Add(this.buttonApplyNow);
            this.splitContainer1.Panel2.Controls.Add(this.buttonCancel);
            this.splitContainer1.Panel2.Controls.Add(this.buttonOk);
            this.splitContainer1.Panel2.Controls.Add(this.buttonApply);
            this.splitContainer1.Size = new System.Drawing.Size(947, 409);
            this.splitContainer1.SplitterDistance = 334;
            this.splitContainer1.TabIndex = 5;
            // 
            // dataGridViewRules
            // 
            this.dataGridViewRules.AllowUserToAddRows = false;
            this.dataGridViewRules.AllowUserToDeleteRows = false;
            this.dataGridViewRules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRules.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnCheck,
            this.ColumnPriority,
            this.ColumnGenType,
            this.ColumnRule});
            this.dataGridViewRules.ContextMenuStrip = this.contextMenuRules;
            this.dataGridViewRules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewRules.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewRules.MultiSelect = false;
            this.dataGridViewRules.Name = "dataGridViewRules";
            this.dataGridViewRules.RowHeadersVisible = false;
            this.dataGridViewRules.Size = new System.Drawing.Size(947, 334);
            this.dataGridViewRules.TabIndex = 3;
            this.dataGridViewRules.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewRules_CellValueChanged);
            // 
            // contextMenuRules
            // 
            this.contextMenuRules.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.новоеПравило_toolStripMenuItem,
            this.toolStripMenuItem2,
            this.сохранитьНаборПравилКакToolStripMenuItem,
            this.загрузитьНаборПравилToolStripMenuItem,
            this.toolStripMenuItem3,
            this.справкаToolStripMenuItem});
            this.contextMenuRules.Name = "contextMenuRules";
            this.contextMenuRules.Size = new System.Drawing.Size(243, 104);
            // 
            // новоеПравило_toolStripMenuItem
            // 
            this.новоеПравило_toolStripMenuItem.Name = "новоеПравило_toolStripMenuItem";
            this.новоеПравило_toolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.новоеПравило_toolStripMenuItem.Text = "Новое правило";
            this.новоеПравило_toolStripMenuItem.Click += new System.EventHandler(this.новоеПравило_toolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(239, 6);
            // 
            // сохранитьНаборПравилКакToolStripMenuItem
            // 
            this.сохранитьНаборПравилКакToolStripMenuItem.Name = "сохранитьНаборПравилКакToolStripMenuItem";
            this.сохранитьНаборПравилКакToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.сохранитьНаборПравилКакToolStripMenuItem.Text = "Сохранить набор правил как...";
            // 
            // загрузитьНаборПравилToolStripMenuItem
            // 
            this.загрузитьНаборПравилToolStripMenuItem.Name = "загрузитьНаборПравилToolStripMenuItem";
            this.загрузитьНаборПравилToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.загрузитьНаборПравилToolStripMenuItem.Text = "Загрузить набор правил...";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(239, 6);
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.справкаToolStripMenuItem.Text = "Справка";
            // 
            // buttonRestoreDefault
            // 
            this.buttonRestoreDefault.Location = new System.Drawing.Point(243, 3);
            this.buttonRestoreDefault.Name = "buttonRestoreDefault";
            this.buttonRestoreDefault.Size = new System.Drawing.Size(176, 26);
            this.buttonRestoreDefault.TabIndex = 5;
            this.buttonRestoreDefault.Text = "Восстановить по-умолчанию";
            this.buttonRestoreDefault.UseVisualStyleBackColor = true;
            this.buttonRestoreDefault.Click += new System.EventHandler(this.buttonRestoreDefault_Click);
            // 
            // ColumnCheck
            // 
            this.ColumnCheck.Frozen = true;
            this.ColumnCheck.HeaderText = "";
            this.ColumnCheck.Name = "ColumnCheck";
            this.ColumnCheck.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnCheck.Width = 20;
            // 
            // ColumnPriority
            // 
            this.ColumnPriority.Frozen = true;
            this.ColumnPriority.HeaderText = "Приоритет";
            this.ColumnPriority.Name = "ColumnPriority";
            this.ColumnPriority.ReadOnly = true;
            this.ColumnPriority.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnPriority.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnPriority.Width = 65;
            // 
            // ColumnGenType
            // 
            this.ColumnGenType.Frozen = true;
            this.ColumnGenType.HeaderText = "Генератор";
            this.ColumnGenType.Name = "ColumnGenType";
            this.ColumnGenType.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnGenType.Width = 110;
            // 
            // ColumnRule
            // 
            this.ColumnRule.Frozen = true;
            this.ColumnRule.HeaderText = "Условие";
            this.ColumnRule.Name = "ColumnRule";
            this.ColumnRule.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnRule.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnRule.Width = 1000;
            // 
            // FormAutoRules
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(947, 409);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FormAutoRules";
            this.Text = "Редактор правил для авто-назначения генераторов";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRules)).EndInit();
            this.contextMenuRules.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonApplyNow;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridViewRules;
        private System.Windows.Forms.ContextMenuStrip contextMenuRules;
        private System.Windows.Forms.ToolStripMenuItem новоеПравило_toolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem сохранитьНаборПравилКакToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem загрузитьНаборПравилToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.Button buttonRestoreDefault;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPriority;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColumnGenType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnRule;
    }
}