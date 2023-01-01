namespace BingMap
{
    partial class MainForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MapStatusLevel = new System.Windows.Forms.StatusStrip();
            this.MapStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.layerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ALayerMenuBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.RLayerMenuBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.HLayerMenuBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.列印ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PrintPreviewMenuBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CbMRTLine = new System.Windows.Forms.CheckBox();
            this.CbMark = new System.Windows.Forms.CheckBox();
            this.CbTown = new System.Windows.Forms.CheckBox();
            this.TbFind = new System.Windows.Forms.TextBox();
            this.BtnZoomOut = new System.Windows.Forms.Button();
            this.BtnCentor = new System.Windows.Forms.Button();
            this.BtnZoomRect = new System.Windows.Forms.Button();
            this.BtnZoomIn = new System.Windows.Forms.Button();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.MarkToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.MapViewer = new BingMap.MapView.MapView();
            this.BtnFind = new System.Windows.Forms.Button();
            this.MapStatusLevel.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MapStatusLevel
            // 
            this.MapStatusLevel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MapStatus});
            this.MapStatusLevel.Location = new System.Drawing.Point(0, 451);
            this.MapStatusLevel.Name = "MapStatusLevel";
            this.MapStatusLevel.Size = new System.Drawing.Size(496, 22);
            this.MapStatusLevel.TabIndex = 1;
            this.MapStatusLevel.Text = "statusStrip1";
            // 
            // MapStatus
            // 
            this.MapStatus.Name = "MapStatus";
            this.MapStatus.Size = new System.Drawing.Size(128, 17);
            this.MapStatus.Text = "toolStripStatusLabel1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.layerToolStripMenuItem,
            this.列印ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(496, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // layerToolStripMenuItem
            // 
            this.layerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ALayerMenuBtn,
            this.RLayerMenuBtn,
            this.HLayerMenuBtn});
            this.layerToolStripMenuItem.Name = "layerToolStripMenuItem";
            this.layerToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.layerToolStripMenuItem.Text = "Layer";
            // 
            // ALayerMenuBtn
            // 
            this.ALayerMenuBtn.Name = "ALayerMenuBtn";
            this.ALayerMenuBtn.Size = new System.Drawing.Size(116, 22);
            this.ALayerMenuBtn.Text = "A Layer";
            this.ALayerMenuBtn.Click += new System.EventHandler(this.ALayerMenuBtn_Click);
            // 
            // RLayerMenuBtn
            // 
            this.RLayerMenuBtn.Name = "RLayerMenuBtn";
            this.RLayerMenuBtn.Size = new System.Drawing.Size(116, 22);
            this.RLayerMenuBtn.Text = "R Layer";
            this.RLayerMenuBtn.Click += new System.EventHandler(this.RLayerMenuBtn_Click);
            // 
            // HLayerMenuBtn
            // 
            this.HLayerMenuBtn.Name = "HLayerMenuBtn";
            this.HLayerMenuBtn.Size = new System.Drawing.Size(116, 22);
            this.HLayerMenuBtn.Text = "H Layer";
            this.HLayerMenuBtn.Click += new System.EventHandler(this.HLayerMenuBtn_Click);
            // 
            // 列印ToolStripMenuItem
            // 
            this.列印ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PrintPreviewMenuBtn});
            this.列印ToolStripMenuItem.Name = "列印ToolStripMenuItem";
            this.列印ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.列印ToolStripMenuItem.Text = "列印";
            // 
            // PrintPreviewMenuBtn
            // 
            this.PrintPreviewMenuBtn.Name = "PrintPreviewMenuBtn";
            this.PrintPreviewMenuBtn.Size = new System.Drawing.Size(122, 22);
            this.PrintPreviewMenuBtn.Text = "預覽列印";
            this.PrintPreviewMenuBtn.Click += new System.EventHandler(this.PrintPreviewMenuBtn_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.CbMRTLine);
            this.panel1.Controls.Add(this.CbMark);
            this.panel1.Controls.Add(this.CbTown);
            this.panel1.Location = new System.Drawing.Point(22, 99);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(103, 101);
            this.panel1.TabIndex = 4;
            // 
            // CbMRTLine
            // 
            this.CbMRTLine.AutoSize = true;
            this.CbMRTLine.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.CbMRTLine.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.CbMRTLine.Location = new System.Drawing.Point(11, 68);
            this.CbMRTLine.Name = "CbMRTLine";
            this.CbMRTLine.Size = new System.Drawing.Size(83, 21);
            this.CbMRTLine.TabIndex = 6;
            this.CbMRTLine.Text = "MRT Line";
            this.CbMRTLine.UseVisualStyleBackColor = true;
            // 
            // CbMark
            // 
            this.CbMark.AutoSize = true;
            this.CbMark.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.CbMark.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.CbMark.Location = new System.Drawing.Point(11, 41);
            this.CbMark.Name = "CbMark";
            this.CbMark.Size = new System.Drawing.Size(59, 21);
            this.CbMark.TabIndex = 6;
            this.CbMark.Text = "Mark";
            this.CbMark.UseVisualStyleBackColor = true;
            // 
            // CbTown
            // 
            this.CbTown.AutoSize = true;
            this.CbTown.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.CbTown.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.CbTown.Location = new System.Drawing.Point(11, 14);
            this.CbTown.Name = "CbTown";
            this.CbTown.Size = new System.Drawing.Size(60, 21);
            this.CbTown.TabIndex = 6;
            this.CbTown.Text = "Town";
            this.CbTown.UseVisualStyleBackColor = true;
            // 
            // TbFind
            // 
            this.TbFind.Location = new System.Drawing.Point(25, 38);
            this.TbFind.Name = "TbFind";
            this.TbFind.Size = new System.Drawing.Size(100, 22);
            this.TbFind.TabIndex = 5;
            // 
            // BtnZoomOut
            // 
            this.BtnZoomOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnZoomOut.BackColor = System.Drawing.Color.Transparent;
            this.BtnZoomOut.Location = new System.Drawing.Point(444, 69);
            this.BtnZoomOut.Name = "BtnZoomOut";
            this.BtnZoomOut.Size = new System.Drawing.Size(25, 25);
            this.BtnZoomOut.TabIndex = 3;
            this.BtnZoomOut.Text = "-";
            this.BtnZoomOut.UseVisualStyleBackColor = false;
            this.BtnZoomOut.Click += new System.EventHandler(this.BtnZoomOut_Click);
            // 
            // BtnCentor
            // 
            this.BtnCentor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCentor.BackColor = System.Drawing.Color.Transparent;
            this.BtnCentor.Location = new System.Drawing.Point(444, 100);
            this.BtnCentor.Name = "BtnCentor";
            this.BtnCentor.Size = new System.Drawing.Size(25, 25);
            this.BtnCentor.TabIndex = 3;
            this.BtnCentor.Text = "*";
            this.BtnCentor.UseVisualStyleBackColor = false;
            this.BtnCentor.Click += new System.EventHandler(this.BtnCentor_Click);
            // 
            // BtnZoomRect
            // 
            this.BtnZoomRect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnZoomRect.BackColor = System.Drawing.Color.Transparent;
            this.BtnZoomRect.Location = new System.Drawing.Point(444, 131);
            this.BtnZoomRect.Name = "BtnZoomRect";
            this.BtnZoomRect.Size = new System.Drawing.Size(25, 25);
            this.BtnZoomRect.TabIndex = 3;
            this.BtnZoomRect.Text = "[]";
            this.BtnZoomRect.UseVisualStyleBackColor = false;
            this.BtnZoomRect.Click += new System.EventHandler(this.BtnZoomRect_Click);
            // 
            // BtnZoomIn
            // 
            this.BtnZoomIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnZoomIn.BackColor = System.Drawing.SystemColors.Control;
            this.BtnZoomIn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnZoomIn.FlatAppearance.BorderSize = 0;
            this.BtnZoomIn.ForeColor = System.Drawing.SystemColors.Desktop;
            this.BtnZoomIn.Location = new System.Drawing.Point(444, 38);
            this.BtnZoomIn.Name = "BtnZoomIn";
            this.BtnZoomIn.Size = new System.Drawing.Size(25, 25);
            this.BtnZoomIn.TabIndex = 3;
            this.BtnZoomIn.Text = "+";
            this.BtnZoomIn.UseVisualStyleBackColor = false;
            this.BtnZoomIn.Click += new System.EventHandler(this.BtnZoomIn_Click);
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // MarkToolTip
            // 
            this.MarkToolTip.AutoPopDelay = 50000;
            this.MarkToolTip.InitialDelay = 500;
            this.MarkToolTip.ReshowDelay = 100;
            this.MarkToolTip.ShowAlways = true;
            // 
            // MapViewer
            // 
            this.MapViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MapViewer.LandMarkLayerRescoresPath = "";
            //this.MapViewer.LayerType = 50;
            //this.MapViewer.Level = 10;
            this.MapViewer.Location = new System.Drawing.Point(0, 0);
            this.MapViewer.MrtLayerRescoresPath = "";
            this.MapViewer.Name = "MapViewer";
            //this.MapViewer.OnFormX = 0;
            //this.MapViewer.OnFormY = 0;
            this.MapViewer.ShowMarkLayer = true;
            this.MapViewer.ShowMrtLineLayer = true;
            this.MapViewer.ShowTownLayer = true;
            //this.MapViewer.Size = new System.Drawing.Size(496, 473);
            this.MapViewer.TabIndex = 6;
            this.MapViewer.TownLayerRescoresPath = "";
            this.MapViewer.Paint += new System.Windows.Forms.PaintEventHandler(this.MapViewer_Paint);
            this.MapViewer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MapViewer_MouseDown);
            this.MapViewer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MapViewer_MouseMove);
            this.MapViewer.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MapViewer_MouseUp);
            // 
            // BtnFind
            // 
            this.BtnFind.BackColor = System.Drawing.SystemColors.Control;
            this.BtnFind.BackgroundImage = global::BingMap.Properties.Resources.magnifier;
            this.BtnFind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnFind.ForeColor = System.Drawing.Color.Transparent;
            this.BtnFind.Location = new System.Drawing.Point(141, 38);
            this.BtnFind.Name = "BtnFind";
            this.BtnFind.Size = new System.Drawing.Size(25, 25);
            this.BtnFind.TabIndex = 3;
            this.BtnFind.UseVisualStyleBackColor = false;
            this.BtnFind.Click += new System.EventHandler(this.BtnFind_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 473);
            this.Controls.Add(this.TbFind);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.BtnFind);
            this.Controls.Add(this.BtnZoomIn);
            this.Controls.Add(this.BtnZoomRect);
            this.Controls.Add(this.BtnCentor);
            this.Controls.Add(this.BtnZoomOut);
            this.Controls.Add(this.MapStatusLevel);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.MapViewer);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(512, 512);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.MapStatusLevel.ResumeLayout(false);
            this.MapStatusLevel.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip MapStatusLevel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem layerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ALayerMenuBtn;
        private System.Windows.Forms.ToolStripMenuItem RLayerMenuBtn;
        private System.Windows.Forms.ToolStripMenuItem HLayerMenuBtn;
        private System.Windows.Forms.ToolStripMenuItem 列印ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PrintPreviewMenuBtn;
        private System.Windows.Forms.Button BtnZoomIn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox TbFind;
        private System.Windows.Forms.Button BtnZoomOut;
        private System.Windows.Forms.Button BtnCentor;
        private System.Windows.Forms.Button BtnZoomRect;
        private System.Windows.Forms.Button BtnFind;
        private System.Windows.Forms.CheckBox CbMRTLine;
        private System.Windows.Forms.CheckBox CbMark;
        private System.Windows.Forms.CheckBox CbTown;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.ToolTip MarkToolTip;
        private System.Windows.Forms.ToolStripStatusLabel MapStatus;
        private MapView.MapView MapViewer;
    }
}

