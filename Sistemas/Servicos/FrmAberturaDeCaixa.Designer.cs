
namespace SistemaLoja.Servicos
{
    partial class FrmAberturaDeCaixa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAberturaDeCaixa));
            this.txt_ValorInicial = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_TitValorRetirado = new System.Windows.Forms.Label();
            this.lbl_ValorRetirado = new System.Windows.Forms.Label();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.lbl_ValorAcres = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lbl_Saldo = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lbl_Retirado = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txt_ValorInicial
            // 
            this.txt_ValorInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ValorInicial.Location = new System.Drawing.Point(38, 52);
            this.txt_ValorInicial.Name = "txt_ValorInicial";
            this.txt_ValorInicial.Size = new System.Drawing.Size(182, 38);
            this.txt_ValorInicial.TabIndex = 3;
            this.txt_ValorInicial.TextChanged += new System.EventHandler(this.txt_ValorInicial_TextChanged);
            this.txt_ValorInicial.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_ValorInicial_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Valor Inicial";
            // 
            // lbl_TitValorRetirado
            // 
            this.lbl_TitValorRetirado.AutoSize = true;
            this.lbl_TitValorRetirado.Location = new System.Drawing.Point(248, 36);
            this.lbl_TitValorRetirado.Name = "lbl_TitValorRetirado";
            this.lbl_TitValorRetirado.Size = new System.Drawing.Size(74, 13);
            this.lbl_TitValorRetirado.TabIndex = 4;
            this.lbl_TitValorRetirado.Text = "Valor Retirado";
            // 
            // lbl_ValorRetirado
            // 
            this.lbl_ValorRetirado.AutoSize = true;
            this.lbl_ValorRetirado.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ValorRetirado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lbl_ValorRetirado.Location = new System.Drawing.Point(245, 59);
            this.lbl_ValorRetirado.Name = "lbl_ValorRetirado";
            this.lbl_ValorRetirado.Size = new System.Drawing.Size(187, 31);
            this.lbl_ValorRetirado.TabIndex = 5;
            this.lbl_ValorRetirado.Text = "Valor Retirado";
            // 
            // btnSalvar
            // 
            this.btnSalvar.BackColor = System.Drawing.Color.Transparent;
            this.btnSalvar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalvar.FlatAppearance.BorderSize = 0;
            this.btnSalvar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.btnSalvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalvar.ForeColor = System.Drawing.Color.DimGray;
            this.btnSalvar.Image = ((System.Drawing.Image)(resources.GetObject("btnSalvar.Image")));
            this.btnSalvar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalvar.Location = new System.Drawing.Point(266, 107);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(137, 63);
            this.btnSalvar.TabIndex = 69;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // lbl_ValorAcres
            // 
            this.lbl_ValorAcres.AutoSize = true;
            this.lbl_ValorAcres.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ValorAcres.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lbl_ValorAcres.Location = new System.Drawing.Point(151, 133);
            this.lbl_ValorAcres.Name = "lbl_ValorAcres";
            this.lbl_ValorAcres.Size = new System.Drawing.Size(14, 13);
            this.lbl_ValorAcres.TabIndex = 97;
            this.lbl_ValorAcres.Text = "0";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(51, 133);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(103, 13);
            this.label12.TabIndex = 96;
            this.label12.Text = "Valor Acrescentado:";
            // 
            // lbl_Saldo
            // 
            this.lbl_Saldo.AutoSize = true;
            this.lbl_Saldo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Saldo.ForeColor = System.Drawing.Color.Green;
            this.lbl_Saldo.Location = new System.Drawing.Point(151, 154);
            this.lbl_Saldo.Name = "lbl_Saldo";
            this.lbl_Saldo.Size = new System.Drawing.Size(14, 13);
            this.lbl_Saldo.TabIndex = 95;
            this.lbl_Saldo.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(117, 153);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(37, 13);
            this.label10.TabIndex = 94;
            this.label10.Text = "Saldo:";
            // 
            // lbl_Retirado
            // 
            this.lbl_Retirado.AutoSize = true;
            this.lbl_Retirado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Retirado.ForeColor = System.Drawing.Color.Red;
            this.lbl_Retirado.Location = new System.Drawing.Point(151, 114);
            this.lbl_Retirado.Name = "lbl_Retirado";
            this.lbl_Retirado.Size = new System.Drawing.Size(14, 13);
            this.lbl_Retirado.TabIndex = 93;
            this.lbl_Retirado.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(77, 114);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 13);
            this.label8.TabIndex = 92;
            this.label8.Text = "Valor Retirado:";
            // 
            // FrmAberturaDeCaixa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 197);
            this.Controls.Add(this.lbl_ValorAcres);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.lbl_Saldo);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lbl_Retirado);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.lbl_ValorRetirado);
            this.Controls.Add(this.lbl_TitValorRetirado);
            this.Controls.Add(this.txt_ValorInicial);
            this.Controls.Add(this.label2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAberturaDeCaixa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Abertura de Caixa";
            this.Load += new System.EventHandler(this.FrmAberturaDeCaixa_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txt_ValorInicial;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_TitValorRetirado;
        private System.Windows.Forms.Label lbl_ValorRetirado;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Label lbl_ValorAcres;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lbl_Saldo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbl_Retirado;
        private System.Windows.Forms.Label label8;
    }
}