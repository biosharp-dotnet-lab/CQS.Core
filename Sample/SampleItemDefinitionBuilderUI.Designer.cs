﻿namespace CQS.Sample
{
  partial class SampleItemDefinitionBuilderUI
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
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.gvItems = new System.Windows.Forms.DataGridView();
      this.colAnnotationName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Example = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colPropertyName = new System.Windows.Forms.DataGridViewComboBoxColumn();
      this.fileDefinitionItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
      this.panel1 = new System.Windows.Forms.Panel();
      this.label1 = new System.Windows.Forms.Label();
      this.dvGrids = new System.Windows.Forms.DataGridView();
      this.defaultValueBindingSource = new System.Windows.Forms.BindingSource(this.components);
      this.panel2 = new System.Windows.Forms.Panel();
      this.label2 = new System.Windows.Forms.Label();
      this.btnInit = new System.Windows.Forms.Button();
      this.btnLoad = new System.Windows.Forms.Button();
      this.btnSave = new System.Windows.Forms.Button();
      this.btnSaveAndNext = new System.Windows.Forms.Button();
      this.btnClose = new System.Windows.Forms.Button();
      this.btnTest = new System.Windows.Forms.Button();
      this.dlgOpenFormatFile = new System.Windows.Forms.OpenFileDialog();
      this.dlgSaveFormatFile = new System.Windows.Forms.SaveFileDialog();
      this.columnFiles = new RCPA.Gui.FileField();
      this.colPropertyName2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.btnSaveAndLast = new System.Windows.Forms.Button();
      this.btnSaveAndPrev = new System.Windows.Forms.Button();
      this.btnSaveAndFirst = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvItems)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.fileDefinitionItemBindingSource)).BeginInit();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dvGrids)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.defaultValueBindingSource)).BeginInit();
      this.panel2.SuspendLayout();
      this.SuspendLayout();
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer1.IsSplitterFixed = true;
      this.splitContainer1.Location = new System.Drawing.Point(0, 23);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.AutoScroll = true;
      this.splitContainer1.Panel2.Controls.Add(this.btnInit);
      this.splitContainer1.Panel2.Controls.Add(this.btnLoad);
      this.splitContainer1.Panel2.Controls.Add(this.btnSave);
      this.splitContainer1.Panel2.Controls.Add(this.btnSaveAndFirst);
      this.splitContainer1.Panel2.Controls.Add(this.btnSaveAndPrev);
      this.splitContainer1.Panel2.Controls.Add(this.btnSaveAndNext);
      this.splitContainer1.Panel2.Controls.Add(this.btnSaveAndLast);
      this.splitContainer1.Panel2.Controls.Add(this.btnTest);
      this.splitContainer1.Panel2.Controls.Add(this.btnClose);
      this.splitContainer1.Size = new System.Drawing.Size(975, 481);
      this.splitContainer1.SplitterDistance = 447;
      this.splitContainer1.TabIndex = 0;
      // 
      // splitContainer2
      // 
      this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer2.Location = new System.Drawing.Point(0, 0);
      this.splitContainer2.Name = "splitContainer2";
      // 
      // splitContainer2.Panel1
      // 
      this.splitContainer2.Panel1.Controls.Add(this.gvItems);
      this.splitContainer2.Panel1.Controls.Add(this.panel1);
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.AutoScroll = true;
      this.splitContainer2.Panel2.Controls.Add(this.dvGrids);
      this.splitContainer2.Panel2.Controls.Add(this.panel2);
      this.splitContainer2.Size = new System.Drawing.Size(975, 447);
      this.splitContainer2.SplitterDistance = 672;
      this.splitContainer2.TabIndex = 2;
      // 
      // gvItems
      // 
      this.gvItems.AllowUserToAddRows = false;
      this.gvItems.AllowUserToDeleteRows = false;
      this.gvItems.AutoGenerateColumns = false;
      this.gvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gvItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAnnotationName,
            this.Example,
            this.colPropertyName});
      this.gvItems.DataSource = this.fileDefinitionItemBindingSource;
      this.gvItems.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gvItems.Location = new System.Drawing.Point(0, 27);
      this.gvItems.Name = "gvItems";
      this.gvItems.RowTemplate.Height = 23;
      this.gvItems.Size = new System.Drawing.Size(672, 420);
      this.gvItems.TabIndex = 3;
      // 
      // colAnnotationName
      // 
      this.colAnnotationName.DataPropertyName = "AnnotationName";
      this.colAnnotationName.HeaderText = "AnnotationName";
      this.colAnnotationName.Name = "colAnnotationName";
      this.colAnnotationName.ReadOnly = true;
      this.colAnnotationName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.colAnnotationName.Width = 300;
      // 
      // Example
      // 
      this.Example.DataPropertyName = "Example";
      this.Example.HeaderText = "Examples";
      this.Example.Name = "Example";
      this.Example.ReadOnly = true;
      this.Example.Width = 300;
      // 
      // colPropertyName
      // 
      this.colPropertyName.DataPropertyName = "PropertyName";
      this.colPropertyName.HeaderText = "PropertyName";
      this.colPropertyName.Name = "colPropertyName";
      this.colPropertyName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.colPropertyName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
      this.colPropertyName.Width = 300;
      // 
      // fileDefinitionItemBindingSource
      // 
      this.fileDefinitionItemBindingSource.DataSource = typeof(RCPA.Format.FileDefinitionItem);
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.label1);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(672, 27);
      this.panel1.TabIndex = 2;
      // 
      // label1
      // 
      this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label1.Location = new System.Drawing.Point(0, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(672, 27);
      this.label1.TabIndex = 0;
      this.label1.Text = "Annotation/property mapping";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // dvGrids
      // 
      this.dvGrids.AllowUserToAddRows = false;
      this.dvGrids.AllowUserToDeleteRows = false;
      this.dvGrids.AutoGenerateColumns = false;
      this.dvGrids.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dvGrids.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPropertyName2,
            this.dataGridViewTextBoxColumn2});
      this.dvGrids.DataSource = this.defaultValueBindingSource;
      this.dvGrids.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dvGrids.Location = new System.Drawing.Point(0, 27);
      this.dvGrids.Name = "dvGrids";
      this.dvGrids.Size = new System.Drawing.Size(299, 420);
      this.dvGrids.TabIndex = 3;
      // 
      // defaultValueBindingSource
      // 
      this.defaultValueBindingSource.DataSource = typeof(RCPA.Format.DefaultValue);
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.label2);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel2.Location = new System.Drawing.Point(0, 0);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(299, 27);
      this.panel2.TabIndex = 3;
      // 
      // label2
      // 
      this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label2.Location = new System.Drawing.Point(0, 0);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(299, 27);
      this.label2.TabIndex = 0;
      this.label2.Text = "Default value";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // btnInit
      // 
      this.btnInit.Dock = System.Windows.Forms.DockStyle.Right;
      this.btnInit.Location = new System.Drawing.Point(75, 0);
      this.btnInit.Name = "btnInit";
      this.btnInit.Size = new System.Drawing.Size(100, 30);
      this.btnInit.TabIndex = 0;
      this.btnInit.Text = "&New from data...";
      this.btnInit.UseVisualStyleBackColor = true;
      this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
      // 
      // btnLoad
      // 
      this.btnLoad.Dock = System.Windows.Forms.DockStyle.Right;
      this.btnLoad.Location = new System.Drawing.Point(175, 0);
      this.btnLoad.Name = "btnLoad";
      this.btnLoad.Size = new System.Drawing.Size(100, 30);
      this.btnLoad.TabIndex = 0;
      this.btnLoad.Text = "&Load format...";
      this.btnLoad.UseVisualStyleBackColor = true;
      this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
      // 
      // btnSave
      // 
      this.btnSave.Dock = System.Windows.Forms.DockStyle.Right;
      this.btnSave.Enabled = false;
      this.btnSave.Location = new System.Drawing.Point(275, 0);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(100, 30);
      this.btnSave.TabIndex = 0;
      this.btnSave.Text = "&Save format...";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
      // 
      // btnSaveAndNext
      // 
      this.btnSaveAndNext.Dock = System.Windows.Forms.DockStyle.Right;
      this.btnSaveAndNext.Location = new System.Drawing.Point(575, 0);
      this.btnSaveAndNext.Name = "btnSaveAndNext";
      this.btnSaveAndNext.Size = new System.Drawing.Size(100, 30);
      this.btnSaveAndNext.TabIndex = 2;
      this.btnSaveAndNext.Text = ">";
      this.btnSaveAndNext.UseVisualStyleBackColor = true;
      this.btnSaveAndNext.Click += new System.EventHandler(this.btnSaveAndNext_Click);
      // 
      // btnClose
      // 
      this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
      this.btnClose.Location = new System.Drawing.Point(875, 0);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(100, 30);
      this.btnClose.TabIndex = 0;
      this.btnClose.Text = "&Close";
      this.btnClose.UseVisualStyleBackColor = true;
      this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
      // 
      // btnTest
      // 
      this.btnTest.Dock = System.Windows.Forms.DockStyle.Right;
      this.btnTest.Enabled = false;
      this.btnTest.Location = new System.Drawing.Point(775, 0);
      this.btnTest.Name = "btnTest";
      this.btnTest.Size = new System.Drawing.Size(100, 30);
      this.btnTest.TabIndex = 1;
      this.btnTest.Text = "&Test format...";
      this.btnTest.UseVisualStyleBackColor = true;
      this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
      // 
      // dlgOpenFormatFile
      // 
      this.dlgOpenFormatFile.DefaultExt = "siformat";
      this.dlgOpenFormatFile.Filter = "SampleInfo format file|*.siformat|All files|*.*";
      this.dlgOpenFormatFile.Title = "Open SampleInfo definition file";
      // 
      // dlgSaveFormatFile
      // 
      this.dlgSaveFormatFile.DefaultExt = "siformat";
      this.dlgSaveFormatFile.Filter = "SampleInfo format file|*.siformat|All files|*.*";
      this.dlgSaveFormatFile.Title = "Save SampleInfo definition file";
      // 
      // columnFiles
      // 
      this.columnFiles.AfterBrowseFileEvent = null;
      this.columnFiles.Dock = System.Windows.Forms.DockStyle.Top;
      this.columnFiles.FullName = "";
      this.columnFiles.Key = "ColumnFile";
      this.columnFiles.LoadButtonVisible = true;
      this.columnFiles.Location = new System.Drawing.Point(0, 0);
      this.columnFiles.Name = "columnFiles";
      this.columnFiles.OpenButtonText = "Browse All File ...";
      this.columnFiles.PreCondition = null;
      this.columnFiles.Size = new System.Drawing.Size(975, 23);
      this.columnFiles.TabIndex = 1;
      this.columnFiles.WidthOpenButton = 226;
      // 
      // colPropertyName2
      // 
      this.colPropertyName2.DataPropertyName = "PropertyName";
      this.colPropertyName2.HeaderText = "PropertyName";
      this.colPropertyName2.Name = "colPropertyName2";
      this.colPropertyName2.ReadOnly = true;
      this.colPropertyName2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.colPropertyName2.Width = 150;
      // 
      // dataGridViewTextBoxColumn2
      // 
      this.dataGridViewTextBoxColumn2.DataPropertyName = "Value";
      this.dataGridViewTextBoxColumn2.HeaderText = "Value";
      this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
      this.dataGridViewTextBoxColumn2.Width = 300;
      // 
      // btnSaveAndLast
      // 
      this.btnSaveAndLast.Dock = System.Windows.Forms.DockStyle.Right;
      this.btnSaveAndLast.Location = new System.Drawing.Point(675, 0);
      this.btnSaveAndLast.Name = "btnSaveAndLast";
      this.btnSaveAndLast.Size = new System.Drawing.Size(100, 30);
      this.btnSaveAndLast.TabIndex = 3;
      this.btnSaveAndLast.Text = ">|";
      this.btnSaveAndLast.UseVisualStyleBackColor = true;
      this.btnSaveAndLast.Click += new System.EventHandler(this.btnSaveAndLast_Click);
      // 
      // btnSaveAndPrev
      // 
      this.btnSaveAndPrev.Dock = System.Windows.Forms.DockStyle.Right;
      this.btnSaveAndPrev.Location = new System.Drawing.Point(475, 0);
      this.btnSaveAndPrev.Name = "btnSaveAndPrev";
      this.btnSaveAndPrev.Size = new System.Drawing.Size(100, 30);
      this.btnSaveAndPrev.TabIndex = 4;
      this.btnSaveAndPrev.Text = "<";
      this.btnSaveAndPrev.UseVisualStyleBackColor = true;
      this.btnSaveAndPrev.Click += new System.EventHandler(this.btnSaveAndPrev_Click);
      // 
      // btnSaveAndFirst
      // 
      this.btnSaveAndFirst.Dock = System.Windows.Forms.DockStyle.Right;
      this.btnSaveAndFirst.Location = new System.Drawing.Point(375, 0);
      this.btnSaveAndFirst.Name = "btnSaveAndFirst";
      this.btnSaveAndFirst.Size = new System.Drawing.Size(100, 30);
      this.btnSaveAndFirst.TabIndex = 5;
      this.btnSaveAndFirst.Text = "|<";
      this.btnSaveAndFirst.UseVisualStyleBackColor = true;
      this.btnSaveAndFirst.Click += new System.EventHandler(this.btnSaveAndFirst_Click);
      // 
      // SampleItemDefinitionBuilderUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(975, 504);
      this.Controls.Add(this.splitContainer1);
      this.Controls.Add(this.columnFiles);
      this.Name = "SampleItemDefinitionBuilderUI";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Breast Cancer Sample Info Definition Builder";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
      this.splitContainer2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.gvItems)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.fileDefinitionItemBindingSource)).EndInit();
      this.panel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dvGrids)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.defaultValueBindingSource)).EndInit();
      this.panel2.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.BindingSource fileDefinitionItemBindingSource;
    private System.Windows.Forms.Button btnInit;
    private System.Windows.Forms.Button btnLoad;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.OpenFileDialog dlgOpenFormatFile;
    private System.Windows.Forms.SaveFileDialog dlgSaveFormatFile;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private System.Windows.Forms.DataGridView gvItems;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.DataGridView dvGrids;
    private System.Windows.Forms.BindingSource defaultValueBindingSource;
    private System.Windows.Forms.Button btnTest;
    private System.Windows.Forms.DataGridViewTextBoxColumn colAnnotationName;
    private System.Windows.Forms.DataGridViewTextBoxColumn Example;
    private System.Windows.Forms.DataGridViewComboBoxColumn colPropertyName;
    private RCPA.Gui.FileField columnFiles;
    private System.Windows.Forms.Button btnSaveAndNext;
    private System.Windows.Forms.DataGridViewTextBoxColumn colPropertyName2;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    private System.Windows.Forms.Button btnSaveAndFirst;
    private System.Windows.Forms.Button btnSaveAndPrev;
    private System.Windows.Forms.Button btnSaveAndLast;
  }
}