using System;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using MetroFramework.Forms;
using MetroFramework;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookRental.SubItems
{
    public partial class DivMngForm : MetroForm
    {
        private static readonly string tableName = "divtbl";
        private BaseMode baseMode = BaseMode.NONE;


        public DivMngForm()
        {
            InitializeComponent();
        }

        private async void DivMngForm_Load(object sender, EventArgs e)
        {
            await RefreshGridData();
        }

        private async Task RefreshGridData()
        {
            using (MySqlConnection connection = new MySqlConnection(Commons.CONNSTR))
            {
                string query = $@"SELECT Division, Names FROM {tableName}";

                await connection.OpenAsync();

                MySqlCommand command = new MySqlCommand() { Connection = connection };
                command.CommandText = query;

                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, connection);
                DataSet dataSet = new DataSet();
                await dataAdapter.FillAsync(dataSet, tableName);

                GrdDivTbl.DataSource = dataSet;
                GrdDivTbl.DataMember = tableName;
            }

            SetColumnHeaders();
        }

        private void SetColumnHeaders()
        {
            SetColumnProperty(GrdDivTbl.Columns["Division"], 100, "구분코드");
            SetColumnProperty(GrdDivTbl.Columns["Names"], 150, "이름");
        }

        private void SetColumnProperty(DataGridViewColumn column, int width, string text)
        {
            column.Width = width;
            column.HeaderText = text;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (baseMode != BaseMode.UPDATE)
            {
                MetroMessageBox.Show(this, "Plz select want to delet data", "Alarm");
                return;
            }

            baseMode = BaseMode.DELETE;
            DataProcess();
            InitControls();
        }

        #region DeleteData()
        //private void DeleteData()
        //{
        //    try
        //    {
        //        using (MySqlConnection connection = new MySqlConnection(Commons.CONNSTR))
        //        {
        //            connection.Open();

        //            string query = @"DELETE FROM divtbl
        //                             WHERE Division = @Division";

        //            MySqlCommand command = new MySqlCommand()
        //            {
        //                Connection = connection,
        //                CommandText = query
        //            };

        //            MySqlParameter paramDivision = new MySqlParameter("@Division", MySqlDbType.VarChar, 4)
        //            {
        //                Value = TxtDivision.Text
        //            };
        //            command.Parameters.Add(paramDivision);

        //            int result = command.ExecuteNonQuery();
        //            string resultStr = $"{result} Data Deleted";
        //            MetroMessageBox.Show(this, resultStr, "Result");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MetroMessageBox.Show(this, $"Error {ex.Message}",
        //                             "Error",
        //                             MessageBoxButtons.OK,
        //                             MessageBoxIcon.Error);
        //    }
        //    finally
        //    {
        //        UpdateData();
        //    }
        //}
        #endregion

        private void BtnNew_Click(object sender, EventArgs e)
        {
            InitControls();
            TxtDivision.ReadOnly = false;
            baseMode = BaseMode.INSERT;
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            await DataProcess();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            InitControls();
        }

        private void GrdDivTbl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow row = GrdDivTbl.Rows[e.RowIndex];

            TxtDivision.Text = row.Cells["Division"].Value.ToString();
            TxtDivision.ReadOnly = true;

            TxtNames.Text = row.Cells["Names"].Value.ToString();

            baseMode = BaseMode.UPDATE;
        }

        private void InitControls()
        {
            TxtDivision.Clear();
            TxtDivision.ReadOnly = true;
            TxtNames.Clear();

            TxtDivision.Focus();
            baseMode = BaseMode.NONE;
        }

        private async Task DataProcess()
        {
            if (string.IsNullOrEmpty(TxtDivision.Text) ||
                string.IsNullOrEmpty(TxtNames.Text))
            {
                MetroMessageBox.Show(this, "Can't save empty value",
                                     "Error",
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Error);
                return;
            }

            string query;
            switch (baseMode)
            {
                case BaseMode.INSERT:
                    query = @"INSERT INTO divtbl (Division, Names)
                              VALUES(@Division, @Names)";
                    break;
                case BaseMode.UPDATE:
                    query = @"UPDATE divtbl
                          SET
                          Names = @Names
                          WHERE Division = @Division";
                    break;
                case BaseMode.DELETE:
                    query = @"DELETE FROM divtbl
                              WHERE Division = @Division";
                    break;
                default:
                    MetroMessageBox.Show(this, "If you want to new regist, Press New Button",
                           "Alarm",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Information);
                    return;
            }

            try
            {
                using (MySqlConnection connection = new MySqlConnection(Commons.CONNSTR))
                {
                    await connection.OpenAsync();

                    MySqlCommand command = new MySqlCommand()
                    {
                        Connection = connection,
                        CommandText = query
                    };

                    List<MySqlParameter> paramList = new List<MySqlParameter>()
                    {
                        new MySqlParameter("@Division", MySqlDbType.VarChar, 4)
                        {
                            Value = TxtDivision.Text
                        }
                    };

                    if (baseMode != BaseMode.DELETE)
                    {
                        paramList.Add(new MySqlParameter("@Names", MySqlDbType.VarChar, 45)
                        {
                            Value = TxtNames.Text
                        });
                    }

                    foreach (var item in paramList)
                    {
                        command.Parameters.Add(item);
                    }

                    int result = await command.ExecuteNonQueryAsync();

                    string resultStr = string.Empty;
                    switch (baseMode)
                    {
                        case BaseMode.INSERT:
                            resultStr = $"{result} Data Inserted";
                            break;
                        case BaseMode.UPDATE:
                            resultStr = $"{result} Data Updated";
                            break;
                        case BaseMode.DELETE:
                            resultStr = $"{result} Data Deleted";
                            break;
                    }

                    MetroMessageBox.Show(this, resultStr, "Result");
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, $"Error {ex.Message}",
                                     "Error",
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Error);
            }
            finally
            {
                await RefreshGridData();
            }
        }
    }
}
