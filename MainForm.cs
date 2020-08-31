using System;
using System.Windows.Forms;
using BookRental.SubItems;
using MetroFramework;
using MetroFramework.Forms;

namespace BookRental
{
    public partial class MainForm : MetroForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog(this);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MetroMessageBox.Show(this, "Are you sure exit?",
                                                       "Exit",
                                                       MessageBoxButtons.YesNo,
                                                       MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                e.Cancel = false;
                Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void MnuItemExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void MnuItemCodeMng_Click(object sender, EventArgs e)
        {
            ShowMnuForm(new DivMngForm(), "구분 코드 관리");
        }

        private void MnuItemBooksMng_Click(object sender, EventArgs e)
        {
            ShowMnuForm(new BooksMngForm(), "도서 관리");
        }

        private void ShowMnuForm(Form form, string title)
        {
            form.MdiParent = this;
            form.Text = title;
            form.Dock = DockStyle.Fill;
            form.Show();
            form.WindowState = FormWindowState.Maximized;
        }

        private void MnuItemMemberMng_Click(object sender, EventArgs e)
        {
            ShowMnuForm(new MemberMngForm(), "회원 관리");
        }
    }
}
