namespace Clinic_CRM;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    private System.Windows.Forms.Button btnShowPatients;
    private System.Windows.Forms.DataGridView dgvPatients;

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
        this.btnShowPatients = new System.Windows.Forms.Button();
        this.dgvPatients = new System.Windows.Forms.DataGridView();
        ((System.ComponentModel.ISupportInitialize)(this.dgvPatients)).BeginInit();
        this.SuspendLayout();
        
        // 
        // btnShowPatients
        // 
        this.btnShowPatients.Location = new System.Drawing.Point(12, 12);
        this.btnShowPatients.Name = "btnShowPatients";
        this.btnShowPatients.Size = new System.Drawing.Size(150, 30);
        this.btnShowPatients.TabIndex = 0;
        this.btnShowPatients.Text = "Показать пациентов";
        this.btnShowPatients.UseVisualStyleBackColor = true;
        this.btnShowPatients.Click += new System.EventHandler(this.btnShowPatients_Click);
        
        // 
        // dgvPatients
        // 
        this.dgvPatients.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
        this.dgvPatients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dgvPatients.Location = new System.Drawing.Point(12, 48);
        this.dgvPatients.Name = "dgvPatients";
        this.dgvPatients.Size = new System.Drawing.Size(776, 390);
        this.dgvPatients.TabIndex = 1;
        
        // 
        // Form1
        // 
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Controls.Add(this.dgvPatients);
        this.Controls.Add(this.btnShowPatients);
        this.Text = "Clinic CRM - Пациенты";
        ((System.ComponentModel.ISupportInitialize)(this.dgvPatients)).EndInit();
        this.ResumeLayout(false);
    }

    #endregion
}