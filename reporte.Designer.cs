namespace doggys_system
{
    partial class reporte
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Datagrid = new Guna.UI2.WinForms.Guna2DataGridView();
            this.DateTimeInicio = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.DateTimeFin = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.CargarBtn = new Guna.UI2.WinForms.Guna2Button();
            this.RangoCmbBox = new Guna.UI2.WinForms.Guna2ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.Datagrid)).BeginInit();
            this.SuspendLayout();
            // 
            // Datagrid
            // 
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            this.Datagrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.Datagrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Satoshi", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Datagrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.Datagrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Satoshi", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Datagrid.DefaultCellStyle = dataGridViewCellStyle6;
            this.Datagrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.Datagrid.Location = new System.Drawing.Point(72, 155);
            this.Datagrid.MultiSelect = false;
            this.Datagrid.Name = "Datagrid";
            this.Datagrid.ReadOnly = true;
            this.Datagrid.RowHeadersVisible = false;
            this.Datagrid.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Datagrid.Size = new System.Drawing.Size(657, 230);
            this.Datagrid.TabIndex = 0;
            this.Datagrid.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.Datagrid.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.Datagrid.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.Datagrid.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.Datagrid.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.Datagrid.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.Datagrid.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.Datagrid.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.Datagrid.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.Datagrid.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Satoshi", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Datagrid.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.Datagrid.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.Datagrid.ThemeStyle.HeaderStyle.Height = 23;
            this.Datagrid.ThemeStyle.ReadOnly = true;
            this.Datagrid.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.Datagrid.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.Datagrid.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Satoshi", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Datagrid.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.Datagrid.ThemeStyle.RowsStyle.Height = 22;
            this.Datagrid.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.Datagrid.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.Datagrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Datagrid_CellContentClick);
            // 
            // DateTimeInicio
            // 
            this.DateTimeInicio.Checked = true;
            this.DateTimeInicio.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.DateTimeInicio.Font = new System.Drawing.Font("Satoshi", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateTimeInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DateTimeInicio.Location = new System.Drawing.Point(90, 48);
            this.DateTimeInicio.MaxDate = new System.DateTime(2024, 11, 10, 0, 0, 0, 0);
            this.DateTimeInicio.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.DateTimeInicio.Name = "DateTimeInicio";
            this.DateTimeInicio.Size = new System.Drawing.Size(119, 36);
            this.DateTimeInicio.TabIndex = 2;
            this.DateTimeInicio.Value = new System.DateTime(2024, 11, 10, 0, 0, 0, 0);
            // 
            // DateTimeFin
            // 
            this.DateTimeFin.Checked = true;
            this.DateTimeFin.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.DateTimeFin.Font = new System.Drawing.Font("Satoshi", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateTimeFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DateTimeFin.Location = new System.Drawing.Point(215, 48);
            this.DateTimeFin.MaxDate = new System.DateTime(2024, 11, 10, 0, 0, 0, 0);
            this.DateTimeFin.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.DateTimeFin.Name = "DateTimeFin";
            this.DateTimeFin.Size = new System.Drawing.Size(119, 36);
            this.DateTimeFin.TabIndex = 3;
            this.DateTimeFin.Value = new System.DateTime(2024, 11, 10, 0, 0, 0, 0);
            // 
            // CargarBtn
            // 
            this.CargarBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.CargarBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.CargarBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.CargarBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.CargarBtn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CargarBtn.ForeColor = System.Drawing.Color.White;
            this.CargarBtn.Location = new System.Drawing.Point(354, 48);
            this.CargarBtn.Name = "CargarBtn";
            this.CargarBtn.Size = new System.Drawing.Size(180, 36);
            this.CargarBtn.TabIndex = 4;
            this.CargarBtn.Text = "Cargar";
            this.CargarBtn.Click += new System.EventHandler(this.CargarBtn_Click);
            // 
            // RangoCmbBox
            // 
            this.RangoCmbBox.BackColor = System.Drawing.Color.Transparent;
            this.RangoCmbBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.RangoCmbBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RangoCmbBox.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.RangoCmbBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.RangoCmbBox.Font = new System.Drawing.Font("Satoshi", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RangoCmbBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.RangoCmbBox.ItemHeight = 30;
            this.RangoCmbBox.Items.AddRange(new object[] {
            "7 DIAS",
            "30 DIAS ",
            "90 DIAS"});
            this.RangoCmbBox.Location = new System.Drawing.Point(589, 48);
            this.RangoCmbBox.Name = "RangoCmbBox";
            this.RangoCmbBox.Size = new System.Drawing.Size(140, 36);
            this.RangoCmbBox.TabIndex = 5;
            this.RangoCmbBox.SelectedIndexChanged += new System.EventHandler(this.RangoCmbBox_SelectedIndexChanged);
            // 
            // reporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.RangoCmbBox);
            this.Controls.Add(this.CargarBtn);
            this.Controls.Add(this.DateTimeFin);
            this.Controls.Add(this.DateTimeInicio);
            this.Controls.Add(this.Datagrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "reporte";
            this.Text = "reporte";
            this.Load += new System.EventHandler(this.reporte_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Datagrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2DataGridView Datagrid;
        private Guna.UI2.WinForms.Guna2DateTimePicker DateTimeInicio;
        private Guna.UI2.WinForms.Guna2DateTimePicker DateTimeFin;
        private Guna.UI2.WinForms.Guna2Button CargarBtn;
        private Guna.UI2.WinForms.Guna2ComboBox RangoCmbBox;
    }
}