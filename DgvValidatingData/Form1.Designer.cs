using System.Windows.Forms;
using System;

namespace DgvValidatingData
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonADD = new System.Windows.Forms.Button();
            this.buttonSAVE = new System.Windows.Forms.Button();
            this.buttonDEL = new System.Windows.Forms.Button();
            this.buttonREFRESH = new System.Windows.Forms.Button();
            this.buttonLink = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.sampleModelBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.sampleModelBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.buttonDelLink = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sampleModelBindingNavigator)).BeginInit();
            this.sampleModelBindingNavigator.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.buttonADD);
            this.flowLayoutPanel1.Controls.Add(this.buttonSAVE);
            this.flowLayoutPanel1.Controls.Add(this.buttonDEL);
            this.flowLayoutPanel1.Controls.Add(this.buttonREFRESH);
            this.flowLayoutPanel1.Controls.Add(this.buttonLink);
            this.flowLayoutPanel1.Controls.Add(this.buttonDelLink);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 556);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1324, 86);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // buttonADD
            // 
            this.buttonADD.Location = new System.Drawing.Point(3, 2);
            this.buttonADD.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonADD.Name = "buttonADD";
            this.buttonADD.Size = new System.Drawing.Size(96, 59);
            this.buttonADD.TabIndex = 0;
            this.buttonADD.Text = "ADD";
            this.buttonADD.UseVisualStyleBackColor = true;
            this.buttonADD.Click += new System.EventHandler(this.buttonADD_Click);
            // 
            // buttonSAVE
            // 
            this.buttonSAVE.Location = new System.Drawing.Point(105, 2);
            this.buttonSAVE.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSAVE.Name = "buttonSAVE";
            this.buttonSAVE.Size = new System.Drawing.Size(96, 59);
            this.buttonSAVE.TabIndex = 1;
            this.buttonSAVE.Text = "SAVE";
            this.buttonSAVE.UseVisualStyleBackColor = true;
            this.buttonSAVE.Click += new System.EventHandler(this.buttonSAVE_Click);
            // 
            // buttonDEL
            // 
            this.buttonDEL.Location = new System.Drawing.Point(207, 2);
            this.buttonDEL.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonDEL.Name = "buttonDEL";
            this.buttonDEL.Size = new System.Drawing.Size(96, 59);
            this.buttonDEL.TabIndex = 2;
            this.buttonDEL.Text = "DEL";
            this.buttonDEL.UseVisualStyleBackColor = true;
            this.buttonDEL.Click += new System.EventHandler(this.buttonDEL_Click);
            // 
            // buttonREFRESH
            // 
            this.buttonREFRESH.Location = new System.Drawing.Point(309, 2);
            this.buttonREFRESH.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonREFRESH.Name = "buttonREFRESH";
            this.buttonREFRESH.Size = new System.Drawing.Size(129, 59);
            this.buttonREFRESH.TabIndex = 3;
            this.buttonREFRESH.Text = "REFRESH";
            this.buttonREFRESH.UseVisualStyleBackColor = true;
            this.buttonREFRESH.Click += new System.EventHandler(this.buttonREFRESH_Click);
            // 
            // buttonLink
            // 
            this.buttonLink.Location = new System.Drawing.Point(444, 2);
            this.buttonLink.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonLink.Name = "buttonLink";
            this.buttonLink.Size = new System.Drawing.Size(129, 59);
            this.buttonLink.TabIndex = 4;
            this.buttonLink.Text = "Link";
            this.buttonLink.UseVisualStyleBackColor = true;
            this.buttonLink.Click += new System.EventHandler(this.buttonLink_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 34);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 72;
            this.dataGridView1.RowTemplate.Height = 36;
            this.dataGridView1.Size = new System.Drawing.Size(1324, 522);
            this.dataGridView1.TabIndex = 1;
            // 
            // sampleModelBindingNavigator
            // 
            this.sampleModelBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.sampleModelBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.sampleModelBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.sampleModelBindingNavigator.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.sampleModelBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem,
            this.sampleModelBindingNavigatorSaveItem});
            this.sampleModelBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.sampleModelBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.sampleModelBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.sampleModelBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.sampleModelBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.sampleModelBindingNavigator.Name = "sampleModelBindingNavigator";
            this.sampleModelBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.sampleModelBindingNavigator.Size = new System.Drawing.Size(1324, 34);
            this.sampleModelBindingNavigator.TabIndex = 2;
            this.sampleModelBindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(40, 28);
            this.bindingNavigatorAddNewItem.Text = "Add new";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(53, 28);
            this.bindingNavigatorCountItem.Text = "/ {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(40, 28);
            this.bindingNavigatorDeleteItem.Text = "Delete";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(40, 28);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(40, 28);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 34);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(60, 34);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 34);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(40, 28);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(40, 28);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 34);
            // 
            // sampleModelBindingNavigatorSaveItem
            // 
            this.sampleModelBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.sampleModelBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("sampleModelBindingNavigatorSaveItem.Image")));
            this.sampleModelBindingNavigatorSaveItem.Name = "sampleModelBindingNavigatorSaveItem";
            this.sampleModelBindingNavigatorSaveItem.Size = new System.Drawing.Size(40, 28);
            this.sampleModelBindingNavigatorSaveItem.Text = "Save Data";
            // 
            // buttonDelLink
            // 
            this.buttonDelLink.Location = new System.Drawing.Point(579, 2);
            this.buttonDelLink.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonDelLink.Name = "buttonDelLink";
            this.buttonDelLink.Size = new System.Drawing.Size(129, 59);
            this.buttonDelLink.TabIndex = 5;
            this.buttonDelLink.Text = "DelLink";
            this.buttonDelLink.UseVisualStyleBackColor = true;
            this.buttonDelLink.Click += new System.EventHandler(this.buttonDelLink_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1324, 642);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.sampleModelBindingNavigator);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sampleModelBindingNavigator)).EndInit();
            this.sampleModelBindingNavigator.ResumeLayout(false);
            this.sampleModelBindingNavigator.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private Button buttonADD;
        private Button buttonSAVE;
        private Button buttonDEL;
        private Button buttonREFRESH;
        private DataGridView dataGridView1;
        private BindingNavigator sampleModelBindingNavigator;
        private ToolStripButton bindingNavigatorAddNewItem;
        private ToolStripLabel bindingNavigatorCountItem;
        private ToolStripButton bindingNavigatorDeleteItem;
        private ToolStripButton bindingNavigatorMoveFirstItem;
        private ToolStripButton bindingNavigatorMovePreviousItem;
        private ToolStripSeparator bindingNavigatorSeparator;
        private ToolStripTextBox bindingNavigatorPositionItem;
        private ToolStripSeparator bindingNavigatorSeparator1;
        private ToolStripButton bindingNavigatorMoveNextItem;
        private ToolStripButton bindingNavigatorMoveLastItem;
        private ToolStripSeparator bindingNavigatorSeparator2;
        private ToolStripButton sampleModelBindingNavigatorSaveItem;
        private Extensions.Winforms.DataGridViewNumericColumn dataGridViewNumericColumn1;
        private Extensions.Winforms.DataGridViewIntegerColumn IntegerColumn1;
        private Extensions.Winforms.DataGridViewNumericColumn NumericColumn1;
        private Extensions.Winforms.DataGridViewCalendarColumn CalendarColumn2;
        private Extensions.Winforms.DataGridViewPasswordColumn PasswordColumn3;
        private Extensions.Winforms.DataGridViewRolloverCellColumn RolloverCellColumn4;
        private Button buttonLink;
        private Button buttonDelLink;
    }
}