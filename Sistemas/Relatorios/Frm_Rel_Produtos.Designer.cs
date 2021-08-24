
namespace SistemaLoja.Relatorios
{
    partial class Frm_Rel_Produtos
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.tbprodutosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sistemalojaDataSet = new SistemaLoja.sistemalojaDataSet();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.tbprodutosTableAdapter = new SistemaLoja.sistemalojaDataSetTableAdapters.tbprodutosTableAdapter();
            this.tbprodutosBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.tbprodutosBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sistemalojaDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbprodutosBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // tbprodutosBindingSource
            // 
            this.tbprodutosBindingSource.DataMember = "tbprodutos";
            this.tbprodutosBindingSource.DataSource = this.sistemalojaDataSet;
            // 
            // sistemalojaDataSet
            // 
            this.sistemalojaDataSet.DataSetName = "sistemalojaDataSet";
            this.sistemalojaDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSetProdutos";
            reportDataSource1.Value = this.tbprodutosBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "SistemaLoja.Relatorios.Rel_Produtos.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(646, 450);
            this.reportViewer1.TabIndex = 0;
            // 
            // tbprodutosTableAdapter
            // 
            this.tbprodutosTableAdapter.ClearBeforeFill = true;
            // 
            // tbprodutosBindingSource1
            // 
            this.tbprodutosBindingSource1.DataMember = "tbprodutos";
            this.tbprodutosBindingSource1.DataSource = this.sistemalojaDataSet;
            // 
            // Frm_Rel_Produtos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 450);
            this.Controls.Add(this.reportViewer1);
            this.Name = "Frm_Rel_Produtos";
            this.Text = "Frm_Rel_Produtos";
            this.Load += new System.EventHandler(this.Frm_Rel_Produtos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tbprodutosBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sistemalojaDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbprodutosBindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private sistemalojaDataSet sistemalojaDataSet;
        private System.Windows.Forms.BindingSource tbprodutosBindingSource;
        private sistemalojaDataSetTableAdapters.tbprodutosTableAdapter tbprodutosTableAdapter;
        private System.Windows.Forms.BindingSource tbprodutosBindingSource1;
    }
}