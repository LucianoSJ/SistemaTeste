
namespace SistemaLoja.Servicos
{
    partial class FrmParcelas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmParcelas));
            this.txt_Cliente = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grid_Parcelas = new System.Windows.Forms.DataGridView();
            this.grid = new System.Windows.Forms.DataGridView();
            this.txt_N_Venda = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Parcela = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_Vencimento = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_Valor = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_Paga = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_DataPagamento = new System.Windows.Forms.TextBox();
            this.lbl_Dia = new System.Windows.Forms.Label();
            this.ck_Pagar = new System.Windows.Forms.CheckBox();
            this.txt_TotalEAberto = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_ID_Cliente = new System.Windows.Forms.TextBox();
            this.txt_ValorVendaSel = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lblidVenda = new System.Windows.Forms.Label();
            this.txt_Limite = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_Saldo = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.rbtn_Sim = new System.Windows.Forms.RadioButton();
            this.rbtn_Nao = new System.Windows.Forms.RadioButton();
            this.rbtn_Todas = new System.Windows.Forms.RadioButton();
            this.gp_ParcelasPagas = new System.Windows.Forms.GroupBox();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grid_Parcelas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.gp_ParcelasPagas.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_Cliente
            // 
            this.txt_Cliente.Location = new System.Drawing.Point(159, 12);
            this.txt_Cliente.Name = "txt_Cliente";
            this.txt_Cliente.Size = new System.Drawing.Size(364, 20);
            this.txt_Cliente.TabIndex = 150;
            this.txt_Cliente.TextChanged += new System.EventHandler(this.txt_Cliente_TextChanged);
            this.txt_Cliente.Enter += new System.EventHandler(this.txt_Cliente_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 103;
            this.label1.Text = "Cliente:";
            // 
            // grid_Parcelas
            // 
            this.grid_Parcelas.AllowUserToAddRows = false;
            this.grid_Parcelas.AllowUserToDeleteRows = false;
            this.grid_Parcelas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid_Parcelas.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.grid_Parcelas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid_Parcelas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.grid_Parcelas.GridColor = System.Drawing.SystemColors.ControlLightLight;
            this.grid_Parcelas.Location = new System.Drawing.Point(741, 116);
            this.grid_Parcelas.Name = "grid_Parcelas";
            this.grid_Parcelas.ReadOnly = true;
            this.grid_Parcelas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid_Parcelas.Size = new System.Drawing.Size(399, 320);
            this.grid_Parcelas.TabIndex = 126;
            this.grid_Parcelas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_Parcelas_CellClick);
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Cursor = System.Windows.Forms.Cursors.Hand;
            this.grid.GridColor = System.Drawing.SystemColors.ControlLightLight;
            this.grid.Location = new System.Drawing.Point(12, 116);
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(723, 320);
            this.grid.TabIndex = 125;
            this.grid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellClick);
            // 
            // txt_N_Venda
            // 
            this.txt_N_Venda.Location = new System.Drawing.Point(431, 39);
            this.txt_N_Venda.Name = "txt_N_Venda";
            this.txt_N_Venda.Size = new System.Drawing.Size(92, 20);
            this.txt_N_Venda.TabIndex = 128;
            this.txt_N_Venda.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(356, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 127;
            this.label2.Text = "Nº da Venda:";
            // 
            // txt_Parcela
            // 
            this.txt_Parcela.Enabled = false;
            this.txt_Parcela.Location = new System.Drawing.Point(85, 43);
            this.txt_Parcela.Name = "txt_Parcela";
            this.txt_Parcela.Size = new System.Drawing.Size(86, 20);
            this.txt_Parcela.TabIndex = 130;
            this.txt_Parcela.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 129;
            this.label3.Text = "Parcela:";
            // 
            // txt_Vencimento
            // 
            this.txt_Vencimento.Enabled = false;
            this.txt_Vencimento.Location = new System.Drawing.Point(262, 42);
            this.txt_Vencimento.Name = "txt_Vencimento";
            this.txt_Vencimento.Size = new System.Drawing.Size(86, 20);
            this.txt_Vencimento.TabIndex = 132;
            this.txt_Vencimento.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(193, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 131;
            this.label4.Text = "Vencimento:";
            // 
            // txt_Valor
            // 
            this.txt_Valor.Enabled = false;
            this.txt_Valor.Location = new System.Drawing.Point(85, 69);
            this.txt_Valor.Name = "txt_Valor";
            this.txt_Valor.Size = new System.Drawing.Size(86, 20);
            this.txt_Valor.TabIndex = 134;
            this.txt_Valor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(48, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 133;
            this.label5.Text = "Valor:";
            // 
            // txt_Paga
            // 
            this.txt_Paga.Enabled = false;
            this.txt_Paga.Location = new System.Drawing.Point(261, 68);
            this.txt_Paga.Name = "txt_Paga";
            this.txt_Paga.Size = new System.Drawing.Size(37, 20);
            this.txt_Paga.TabIndex = 136;
            this.txt_Paga.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(222, 72);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 135;
            this.label6.Text = "Paga:";
            // 
            // txt_DataPagamento
            // 
            this.txt_DataPagamento.Enabled = false;
            this.txt_DataPagamento.Location = new System.Drawing.Point(329, 68);
            this.txt_DataPagamento.Name = "txt_DataPagamento";
            this.txt_DataPagamento.Size = new System.Drawing.Size(117, 20);
            this.txt_DataPagamento.TabIndex = 137;
            this.txt_DataPagamento.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_DataPagamento.Visible = false;
            // 
            // lbl_Dia
            // 
            this.lbl_Dia.AutoSize = true;
            this.lbl_Dia.Enabled = false;
            this.lbl_Dia.Location = new System.Drawing.Point(302, 72);
            this.lbl_Dia.Name = "lbl_Dia";
            this.lbl_Dia.Size = new System.Drawing.Size(26, 13);
            this.lbl_Dia.TabIndex = 138;
            this.lbl_Dia.Text = "Dia:";
            this.lbl_Dia.Visible = false;
            // 
            // ck_Pagar
            // 
            this.ck_Pagar.AutoSize = true;
            this.ck_Pagar.Location = new System.Drawing.Point(304, 70);
            this.ck_Pagar.Name = "ck_Pagar";
            this.ck_Pagar.Size = new System.Drawing.Size(54, 17);
            this.ck_Pagar.TabIndex = 139;
            this.ck_Pagar.Text = "Pagar";
            this.ck_Pagar.UseVisualStyleBackColor = true;
            this.ck_Pagar.Visible = false;
            this.ck_Pagar.Click += new System.EventHandler(this.ck_Pagar_Click);
            // 
            // txt_TotalEAberto
            // 
            this.txt_TotalEAberto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_TotalEAberto.Location = new System.Drawing.Point(1054, 53);
            this.txt_TotalEAberto.Name = "txt_TotalEAberto";
            this.txt_TotalEAberto.ReadOnly = true;
            this.txt_TotalEAberto.Size = new System.Drawing.Size(86, 20);
            this.txt_TotalEAberto.TabIndex = 141;
            this.txt_TotalEAberto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(808, 57);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(243, 13);
            this.label7.TabIndex = 140;
            this.label7.Text = "Valor Total Parcelas em Aberto Todas As Vendas:";
            // 
            // txt_ID_Cliente
            // 
            this.txt_ID_Cliente.Enabled = false;
            this.txt_ID_Cliente.Location = new System.Drawing.Point(85, 12);
            this.txt_ID_Cliente.Name = "txt_ID_Cliente";
            this.txt_ID_Cliente.Size = new System.Drawing.Size(64, 20);
            this.txt_ID_Cliente.TabIndex = 142;
            this.txt_ID_Cliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_ValorVendaSel
            // 
            this.txt_ValorVendaSel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_ValorVendaSel.Location = new System.Drawing.Point(1054, 6);
            this.txt_ValorVendaSel.Name = "txt_ValorVendaSel";
            this.txt_ValorVendaSel.ReadOnly = true;
            this.txt_ValorVendaSel.Size = new System.Drawing.Size(86, 20);
            this.txt_ValorVendaSel.TabIndex = 144;
            this.txt_ValorVendaSel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(834, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(190, 13);
            this.label8.TabIndex = 143;
            this.label8.Text = "Valor Total Parcelas em Aberto Venda:";
            // 
            // lblidVenda
            // 
            this.lblidVenda.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblidVenda.AutoSize = true;
            this.lblidVenda.Location = new System.Drawing.Point(1025, 10);
            this.lblidVenda.Name = "lblidVenda";
            this.lblidVenda.Size = new System.Drawing.Size(25, 13);
            this.lblidVenda.TabIndex = 145;
            this.lblidVenda.Text = "0   :";
            // 
            // txt_Limite
            // 
            this.txt_Limite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Limite.Location = new System.Drawing.Point(1054, 29);
            this.txt_Limite.Name = "txt_Limite";
            this.txt_Limite.ReadOnly = true;
            this.txt_Limite.Size = new System.Drawing.Size(86, 20);
            this.txt_Limite.TabIndex = 147;
            this.txt_Limite.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1013, 32);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 13);
            this.label9.TabIndex = 146;
            this.label9.Text = "Limite:";
            // 
            // txt_Saldo
            // 
            this.txt_Saldo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Saldo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Saldo.Location = new System.Drawing.Point(1054, 77);
            this.txt_Saldo.Name = "txt_Saldo";
            this.txt_Saldo.ReadOnly = true;
            this.txt_Saldo.Size = new System.Drawing.Size(86, 20);
            this.txt_Saldo.TabIndex = 149;
            this.txt_Saldo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(966, 80);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(82, 13);
            this.label10.TabIndex = 148;
            this.label10.Text = "Saldo do Limite:";
            // 
            // rbtn_Sim
            // 
            this.rbtn_Sim.AutoSize = true;
            this.rbtn_Sim.Location = new System.Drawing.Point(15, 22);
            this.rbtn_Sim.Name = "rbtn_Sim";
            this.rbtn_Sim.Size = new System.Drawing.Size(42, 17);
            this.rbtn_Sim.TabIndex = 150;
            this.rbtn_Sim.TabStop = true;
            this.rbtn_Sim.Text = "Sim";
            this.rbtn_Sim.UseVisualStyleBackColor = true;
            this.rbtn_Sim.Click += new System.EventHandler(this.rbtn_Sim_Click);
            // 
            // rbtn_Nao
            // 
            this.rbtn_Nao.AutoSize = true;
            this.rbtn_Nao.Location = new System.Drawing.Point(15, 47);
            this.rbtn_Nao.Name = "rbtn_Nao";
            this.rbtn_Nao.Size = new System.Drawing.Size(45, 17);
            this.rbtn_Nao.TabIndex = 151;
            this.rbtn_Nao.TabStop = true;
            this.rbtn_Nao.Text = "Não";
            this.rbtn_Nao.UseVisualStyleBackColor = true;
            this.rbtn_Nao.Click += new System.EventHandler(this.rbtn_ParcelasNaoPagas);
            // 
            // rbtn_Todas
            // 
            this.rbtn_Todas.AutoSize = true;
            this.rbtn_Todas.Location = new System.Drawing.Point(15, 70);
            this.rbtn_Todas.Name = "rbtn_Todas";
            this.rbtn_Todas.Size = new System.Drawing.Size(55, 17);
            this.rbtn_Todas.TabIndex = 152;
            this.rbtn_Todas.TabStop = true;
            this.rbtn_Todas.Text = "Todas";
            this.rbtn_Todas.UseVisualStyleBackColor = true;
            this.rbtn_Todas.Click += new System.EventHandler(this.rbtn_Todas_Click);
            // 
            // gp_ParcelasPagas
            // 
            this.gp_ParcelasPagas.Controls.Add(this.rbtn_Sim);
            this.gp_ParcelasPagas.Controls.Add(this.rbtn_Todas);
            this.gp_ParcelasPagas.Controls.Add(this.rbtn_Nao);
            this.gp_ParcelasPagas.Location = new System.Drawing.Point(542, 6);
            this.gp_ParcelasPagas.Name = "gp_ParcelasPagas";
            this.gp_ParcelasPagas.Size = new System.Drawing.Size(101, 91);
            this.gp_ParcelasPagas.TabIndex = 153;
            this.gp_ParcelasPagas.TabStop = false;
            this.gp_ParcelasPagas.Text = "Parcelas Pagas";
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.BackColor = System.Drawing.Color.Transparent;
            this.btnConfirmar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirmar.FlatAppearance.BorderSize = 0;
            this.btnConfirmar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.btnConfirmar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmar.ForeColor = System.Drawing.Color.DimGray;
            this.btnConfirmar.Image = ((System.Drawing.Image)(resources.GetObject("btnConfirmar.Image")));
            this.btnConfirmar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConfirmar.Location = new System.Drawing.Point(356, 61);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(113, 35);
            this.btnConfirmar.TabIndex = 154;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnConfirmar.UseVisualStyleBackColor = false;
            this.btnConfirmar.Visible = false;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 100);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(129, 13);
            this.label11.TabIndex = 155;
            this.label11.Text = "Informações das Compras";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(738, 100);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(129, 13);
            this.label12.TabIndex = 156;
            this.label12.Text = "Informações das Parcelas";
            // 
            // FrmParcelas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1152, 443);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.gp_ParcelasPagas);
            this.Controls.Add(this.txt_Saldo);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txt_Limite);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblidVenda);
            this.Controls.Add(this.txt_ValorVendaSel);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txt_ID_Cliente);
            this.Controls.Add(this.txt_TotalEAberto);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.ck_Pagar);
            this.Controls.Add(this.lbl_Dia);
            this.Controls.Add(this.txt_DataPagamento);
            this.Controls.Add(this.txt_Paga);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt_Valor);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_Vencimento);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_Parcela);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_N_Venda);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.grid_Parcelas);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.txt_Cliente);
            this.Controls.Add(this.label1);
            this.Name = "FrmParcelas";
            this.Text = "Vendas";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmParcelas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid_Parcelas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.gp_ParcelasPagas.ResumeLayout(false);
            this.gp_ParcelasPagas.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_Cliente;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView grid_Parcelas;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.TextBox txt_N_Venda;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_Parcela;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_Vencimento;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_Valor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_Paga;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_DataPagamento;
        private System.Windows.Forms.Label lbl_Dia;
        private System.Windows.Forms.CheckBox ck_Pagar;
        private System.Windows.Forms.TextBox txt_TotalEAberto;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_ID_Cliente;
        private System.Windows.Forms.TextBox txt_ValorVendaSel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblidVenda;
        private System.Windows.Forms.TextBox txt_Limite;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_Saldo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.RadioButton rbtn_Sim;
        private System.Windows.Forms.RadioButton rbtn_Nao;
        private System.Windows.Forms.RadioButton rbtn_Todas;
        private System.Windows.Forms.GroupBox gp_ParcelasPagas;
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
    }
}