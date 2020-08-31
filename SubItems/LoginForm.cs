using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;
using MySql.Data.MySqlClient;

namespace BookRental.SubItems
{
    public partial class LoginForm : MetroForm
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private async void BtnOk_Click(object sender, EventArgs e)
        {
            await LoginProcess();
        }

        private async Task LoginProcess()
        {
            // ID, Password 빈칸일경우 재입력 요청
            if (string.IsNullOrEmpty(TxtUserId.Text) || string.IsNullOrEmpty(TxtUserPwd.Text))
            {
                MetroMessageBox.Show(this, "ID, Password를 입력해주세요.",
                                     "Login",
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Information);
                TxtUserId.Focus();
                return;
            }

            // DB에 연결하여 로그인정보 확인
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Commons.CONNSTR))
                {
                    await connection.OpenAsync();
                    //MetroMessageBox.Show(this, "DB Connection OK.");

                    MySqlCommand command = new MySqlCommand() { Connection = connection };
                    command.CommandText = @"SELECT userID FROM usertbl
                                            WHERE userID = @userID
                                            AND password = @password";


                    MySqlParameter userId = new MySqlParameter("@userID", MySqlDbType.VarChar, TxtUserId.Text.Length);
                    userId.Value = TxtUserId.Text.Trim();
                    command.Parameters.Add(userId);

                    MySqlParameter userPwd = new MySqlParameter("@password", MySqlDbType.VarChar, TxtUserPwd.Text.Length);
                    userPwd.Value = TxtUserPwd.Text.Trim();
                    command.Parameters.Add(userPwd);

                    MySqlDataReader reader = await Task.Run(() => command.ExecuteReader());
                    await reader.ReadAsync();
                    string resultId = reader["userID"]?.ToString();

                    if (reader.HasRows == false || string.IsNullOrEmpty(resultId))
                    {
                        MetroMessageBox.Show(this, "ID / Password check again", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        TxtUserId.Clear();
                        TxtUserPwd.Clear();
                        TxtUserId.Focus();
                        return;
                    }
                    else
                    {
                        MetroMessageBox.Show(this, $"{resultId} Login");
                        this.Owner.Focus();
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, $"DB Connection Error: {ex.Message}",
                                     "Login Error",
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Error);
                return;
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void TxtUserId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                TxtUserPwd.Focus();
            }
        }

        private void TxtUserPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                BtnOk_Click(sender, new EventArgs());
            }
        }
    }
}
