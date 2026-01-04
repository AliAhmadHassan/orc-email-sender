using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrcEMail.WFApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            #region Validações
            if (!File.Exists(txtCaminhoArquivo.Text))
            {
                MessageBox.Show("Favor selecionar um arquivo.");
                txtCaminhoArquivo.BackColor = Color.Yellow;
                return;
            }
            if (txtAssunto.Text == "")
            {
                MessageBox.Show("Favor Definir um Assunto.");
                txtAssunto.BackColor = Color.Yellow;

                return;
            }
            if (string.IsNullOrEmpty(htmlEditorControl1.InnerText))
            {
                MessageBox.Show("Favor Definir um texto para o E-Mail.");
                htmlEditorControl1.BackColor = Color.Yellow;
                return;
            }

            #endregion



            List<DTO.BaseXLS> clientes = new BLL.BaseXLS().ImportaBase(txtCaminhoArquivo.Text);
            BLL.EnviaEMail enviaEMail = new BLL.EnviaEMail();

            toolStripProgressBar1.Maximum = clientes.Count();
            toolStripProgressBar1.Value = 0;
                
            foreach (DTO.BaseXLS cliente in clientes)
            {
                Envia(enviaEMail, cliente.EMail1, htmlEditorControl1.InnerHtml, txtAssunto.Text);
                Envia(enviaEMail, cliente.EMail2, htmlEditorControl1.InnerHtml, txtAssunto.Text);
                Envia(enviaEMail, cliente.EMail3, htmlEditorControl1.InnerHtml, txtAssunto.Text);
                Envia(enviaEMail, cliente.EMail4, htmlEditorControl1.InnerHtml, txtAssunto.Text);

                toolStripProgressBar1.Value++;

                toolStripStatusLabel1.Text = string.Format("{0} de {1} - {2}%", toolStripProgressBar1.Value, toolStripProgressBar1.Maximum, Math.Round(((decimal)toolStripProgressBar1.Value / (decimal)toolStripProgressBar1.Maximum) * 100, 2));
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnLocalizar_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            txtCaminhoArquivo.Text = openFileDialog1.FileName;
            txtCaminhoArquivo.BackColor = Color.White;
        }

        private void txtAssunto_Enter(object sender, EventArgs e)
        {
            txtAssunto.BackColor = Color.White;
        }

        private void txtCaminhoArquivo_Enter(object sender, EventArgs e)
        {
            txtCaminhoArquivo.BackColor = Color.White;
        }

        private void htmlEditorControl1_Enter(object sender, EventArgs e)
        {
            htmlEditorControl1.BackColor = Color.Lavender;
        }

        private void Envia(BLL.EnviaEMail enviaEMail, string Email, string Conteudo, string Assunto)
        {
            if (!string.IsNullOrEmpty(Email))
            {
                try
                {
                    lblPara.Text = Email;
                    enviaEMail.Envia(Email, Conteudo, Assunto);
                    Application.DoEvents();
                }
                catch (Exception Erro)
                {
                    txtLog.Text += DateTime.Now.ToString("dd/MM/yy + hh:MM:ss") + " " + Email + " - " + Erro.Message;
                }

            }

        }
    }
}

