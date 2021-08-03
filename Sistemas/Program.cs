using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaLoja
{
    static class Program
    {
        public static string forVendas;
        public static string ExecultadoVenda = "Não";

        public static string forAberto;

        public static decimal valorInicial;
        public static decimal saldoCaixa;

        public static string CodBarras = "NT";
        public static string Botao = "NT";

        public static string idVenda;
        public static string idUsuario;
        public static string nomeUsuario;

        public static string vendedorAcionado;
        public static string vendedor;
        public static string cargoUsuario;

        public static string idcliente = "0";
        public static string nomecliente = "Selecione o Cliente";

        public static string chamadaProdutos;
        public static string idProdutos;
        public static string nomeProdutos;
        public static string fornecedoorProduto;
        public static string valorProdutos;
        public static string custoProdutos;
        public static string estoqueProdutos;


        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmLogin());
        }
    }
}
