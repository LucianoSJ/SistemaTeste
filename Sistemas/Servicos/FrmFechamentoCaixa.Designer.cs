
namespace SistemaLoja.Servicos
{
    partial class FrmFechamentoCaixa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFechamentoCaixa));
            this.grid = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_IDFechamento = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_ValorInicial = new System.Windows.Forms.TextBox();
            this.txt_Entrada = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_Retirada = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_Saldo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_TotalVendido = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_NovoValor = new System.Windows.Forms.TextBox();
            this.lbl_DgValor = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lbl_Retirado = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lbl_Saldo = new System.Windows.Forms.Label();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.txt_DataHora = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lbl_ValorAcres = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lbl_CxFechado = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Cursor = System.Windows.Forms.Cursors.Hand;
            this.grid.GridColor = System.Drawing.SystemColors.ControlLightLight;
            this.grid.Location = new System.Drawing.Point(12, 150);
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(941, 368);
            this.grid.TabIndex = 71;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(780, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 70;
            this.label2.Text = "Fechamento Nº:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txt_IDFechamento
            // 
            this.txt_IDFechamento.Location = new System.Drawing.Point(864, 54);
            this.txt_IDFechamento.Name = "txt_IDFechamento";
            this.txt_IDFechamento.Size = new System.Drawing.Size(76, 20);
            this.txt_IDFechamento.TabIndex = 69;
            this.txt_IDFechamento.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 72;
            this.label1.Text = "Valor Inicial:";
            // 
            // txt_ValorInicial
            // 
            this.txt_ValorInicial.Location = new System.Drawing.Point(126, 9);
            this.txt_ValorInicial.Name = "txt_ValorInicial";
            this.txt_ValorInicial.Size = new System.Drawing.Size(100, 20);
            this.txt_ValorInicial.TabIndex = 73;
            this.txt_ValorInicial.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_Entrada
            // 
            this.txt_Entrada.Location = new System.Drawing.Point(126, 35);
            this.txt_Entrada.Name = "txt_Entrada";
            this.txt_Entrada.Size = new System.Drawing.Size(100, 20);
            this.txt_Entrada.TabIndex = 75;
            this.txt_Entrada.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 74;
            this.label3.Text = "Entrada em Dinheiro:";
            // 
            // txt_Retirada
            // 
            this.txt_Retirada.Location = new System.Drawing.Point(126, 61);
            this.txt_Retirada.Name = "txt_Retirada";
            this.txt_Retirada.Size = new System.Drawing.Size(100, 20);
            this.txt_Retirada.TabIndex = 77;
            this.txt_Retirada.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(51, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 76;
            this.label4.Text = "Valor Retirado:";
            // 
            // txt_Saldo
            // 
            this.txt_Saldo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txt_Saldo.Location = new System.Drawing.Point(126, 87);
            this.txt_Saldo.Name = "txt_Saldo";
            this.txt_Saldo.Size = new System.Drawing.Size(100, 20);
            this.txt_Saldo.TabIndex = 79;
            this.txt_Saldo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 13);
            this.label5.TabIndex = 78;
            this.label5.Text = "Saldo em Dinheiro:";
            // 
            // txt_TotalVendido
            // 
            this.txt_TotalVendido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txt_TotalVendido.Location = new System.Drawing.Point(126, 113);
            this.txt_TotalVendido.Name = "txt_TotalVendido";
            this.txt_TotalVendido.Size = new System.Drawing.Size(100, 20);
            this.txt_TotalVendido.TabIndex = 81;
            this.txt_TotalVendido.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 120);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 13);
            this.label6.TabIndex = 80;
            this.label6.Text = "Valor Total Vendido:";
            // 
            // txt_NovoValor
            // 
            this.txt_NovoValor.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_NovoValor.Location = new System.Drawing.Point(340, 61);
            this.txt_NovoValor.Name = "txt_NovoValor";
            this.txt_NovoValor.Size = new System.Drawing.Size(238, 30);
            this.txt_NovoValor.TabIndex = 1;
            this.txt_NovoValor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_NovoValor.TextChanged += new System.EventHandler(this.txt_NovoValor_TextChanged);
            this.txt_NovoValor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_NovoValor_KeyPress);
            // 
            // lbl_DgValor
            // 
            this.lbl_DgValor.AutoSize = true;
            this.lbl_DgValor.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_DgValor.Location = new System.Drawing.Point(336, 33);
            this.lbl_DgValor.Name = "lbl_DgValor";
            this.lbl_DgValor.Size = new System.Drawing.Size(243, 25);
            this.lbl_DgValor.TabIndex = 82;
            this.lbl_DgValor.Text = "Digite novo Valor em caixa";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(787, 85);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 13);
            this.label8.TabIndex = 84;
            this.label8.Text = "Valor Retirado:";
            // 
            // lbl_Retirado
            // 
            this.lbl_Retirado.AutoSize = true;
            this.lbl_Retirado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Retirado.ForeColor = System.Drawing.Color.Red;
            this.lbl_Retirado.Location = new System.Drawing.Point(861, 85);
            this.lbl_Retirado.Name = "lbl_Retirado";
            this.lbl_Retirado.Size = new System.Drawing.Size(14, 13);
            this.lbl_Retirado.TabIndex = 85;
            this.lbl_Retirado.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(827, 124);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(37, 13);
            this.label10.TabIndex = 86;
            this.label10.Text = "Saldo:";
            // 
            // lbl_Saldo
            // 
            this.lbl_Saldo.AutoSize = true;
            this.lbl_Saldo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Saldo.ForeColor = System.Drawing.Color.Green;
            this.lbl_Saldo.Location = new System.Drawing.Point(861, 125);
            this.lbl_Saldo.Name = "lbl_Saldo";
            this.lbl_Saldo.Size = new System.Drawing.Size(14, 13);
            this.lbl_Saldo.TabIndex = 87;
            this.lbl_Saldo.Text = "0";
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
            this.btnSalvar.Location = new System.Drawing.Point(584, 55);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(94, 45);
            this.btnSalvar.TabIndex = 2;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // txt_DataHora
            // 
            this.txt_DataHora.Location = new System.Drawing.Point(820, 28);
            this.txt_DataHora.Name = "txt_DataHora";
            this.txt_DataHora.Size = new System.Drawing.Size(120, 20);
            this.txt_DataHora.TabIndex = 88;
            this.txt_DataHora.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(817, 12);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(123, 13);
            this.label9.TabIndex = 89;
            this.label9.Text = "Data e Hora da Abertura";
            // 
            // lbl_ValorAcres
            // 
            this.lbl_ValorAcres.AutoSize = true;
            this.lbl_ValorAcres.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ValorAcres.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lbl_ValorAcres.Location = new System.Drawing.Point(861, 104);
            this.lbl_ValorAcres.Name = "lbl_ValorAcres";
            this.lbl_ValorAcres.Size = new System.Drawing.Size(14, 13);
            this.lbl_ValorAcres.TabIndex = 91;
            this.lbl_ValorAcres.Text = "0";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(761, 104);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(103, 13);
            this.label12.TabIndex = 90;
            this.label12.Text = "Valor Acrescentado:";
            // 
            // lbl_CxFechado
            // 
            this.lbl_CxFechado.AutoSize = true;
            this.lbl_CxFechado.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_CxFechado.ForeColor = System.Drawing.Color.Red;
            this.lbl_CxFechado.Location = new System.Drawing.Point(363, 33);
            this.lbl_CxFechado.Name = "lbl_CxFechado";
            this.lbl_CxFechado.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_CxFechado.Size = new System.Drawing.Size(197, 25);
            this.lbl_CxFechado.TabIndex = 92;
            this.lbl_CxFechado.Text = "CAIXA FECHADO!";
            this.lbl_CxFechado.Visible = false;
            // 
            // FrmFechamentoCaixa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(965, 530);
            this.Controls.Add(this.lbl_CxFechado);
            this.Controls.Add(this.lbl_ValorAcres);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txt_DataHora);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.lbl_Saldo);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lbl_Retirado);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txt_NovoValor);
            this.Controls.Add(this.lbl_DgValor);
            this.Controls.Add(this.txt_TotalVendido);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt_Saldo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_Retirada);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_Entrada);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_ValorInicial);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_IDFechamento);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmFechamentoCaixa";
            this.Text = "Fechamento de Caixa";
            this.Load += new System.EventHandler(this.FrmFechamentoCaixa_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_IDFechamento;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_ValorInicial;
        private System.Windows.Forms.TextBox txt_Entrada;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_Retirada;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_Saldo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_TotalVendido;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_NovoValor;
        private System.Windows.Forms.Label lbl_DgValor;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lbl_Retirado;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbl_Saldo;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.TextBox txt_DataHora;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lbl_ValorAcres;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lbl_CxFechado;
    }
}