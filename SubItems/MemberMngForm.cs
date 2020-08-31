using System;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using MetroFramework.Forms;
using MetroFramework;
using System.Data;
using System.Collections.Generic;

namespace BookRental.SubItems
{
    public partial class MemberMngForm : MetroForm
    {
        enum Levels
        {
            A,
            B,
            C,
            D,
            F,
            LEVELS_END
        }
        private static readonly string tableName = "membertbl";
        private static readonly string selectQuery = @"SELECT Idx,
                                                           Names,
                                                           Levels,
                                                           Addr,
                                                           Mobile,
                                                           Email
                                                       FROM membertbl
                                                       ORDER BY Idx";
        private static readonly string updateQuery = @"UPDATE membertbl 
                                                       SET 
                                                           Names = @Names,
                                                           Levels = @Levels,
                                                           Addr = @Addr,
                                                           Mobile = @Mobile,
                                                           Email = @Email
                                                       WHERE
                                                           Idx = @Idx";
        private static readonly string insertQuery = @"INSERT INTO membertbl
                                                           (Names,
                                                           Levels,
                                                           Addr,
                                                           Mobile,
                                                           Email)
                                                       VALUES
                                                           (@Names,
                                                           @Levels,
                                                           @Addr,
                                                           @Mobile,
                                                           @Email)";
        private BaseMode baseMode = BaseMode.NONE;

        public MemberMngForm()
        {
            InitializeComponent();
        }

        private void DivMngForm_Load(object sender, EventArgs e)
        {
            RefreshGridData();
            UpdateComboLevels();
            InitControls();
        }

        private void UpdateComboLevels()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("선택", "");

            for (int i = 0; i < (int)Levels.LEVELS_END; i++)
            {
                Levels level = (Levels)i;
                dic.Add(level.ToString(), level.ToString());
            }

            CboLevels.DataSource = new BindingSource(dic, null);
            CboLevels.DisplayMember = "Key";
            CboLevels.ValueMember = "Value";
        }

        private void RefreshGridData()
        {
            using (MySqlConnection connection = new MySqlConnection(Commons.CONNSTR))
            {
                connection.Open();

                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(selectQuery, connection);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, tableName);

                GrdBooksTbl.DataSource = dataSet;
                GrdBooksTbl.DataMember = tableName;
            }

            SetColumnHeaders();
        }

        private void SetColumnHeaders()
        {
            // Idx
            SetColumnProperty(GrdBooksTbl.Columns["Idx"], 100, "번호");
            // Names
            SetColumnProperty(GrdBooksTbl.Columns["Names"], 100, "이름");
            // Levels
            SetColumnProperty(GrdBooksTbl.Columns["Levels"], 100, "성적");
            // Addr
            SetColumnProperty(GrdBooksTbl.Columns["Addr"], 100, "주소");
            // Mobile
            SetColumnProperty(GrdBooksTbl.Columns["Mobile"], 100, "연락처");
            // Email
            SetColumnProperty(GrdBooksTbl.Columns["Email"], 100, "이메일");
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
            baseMode = BaseMode.INSERT;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            DataProcess();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            InitControls();
        }

        private void GrdBooksTbl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            RefreshDetailData(e.RowIndex);

            baseMode = BaseMode.UPDATE;
        }

        void RefreshDetailData(int index)
        {
            DataGridViewRow data = GrdBooksTbl.Rows[index];

            // Idx
            TxtIdx.Text = data.Cells["Idx"].Value.ToString();
            // Names
            TxtNames.Text = data.Cells["Names"].Value.ToString();
            // Levels
            CboLevels.SelectedValue = data.Cells["Levels"].Value;
            // Addr
            TxtAddr.Text = data.Cells["Addr"].Value.ToString();
            // Mobile
            TxtMobile.Text = data.Cells["Mobile"].Value.ToString();
            // Email
            TxtEmail.Text = data.Cells["Email"].Value.ToString();
        }

        private void InitControls()
        {
            TxtIdx.Clear();
            TxtNames.Clear();
            TxtAddr.Clear();
            TxtMobile.Clear();
            TxtEmail.Clear();
            CboLevels.SelectedIndex = 0;

            TxtIdx.Focus();

            baseMode = BaseMode.NONE;

            #region comboBox Dictionary
            /*Dictionary<string, string> dic = new Dictionary<string, string>
            {
                { "선택", "00" },
                { "서울특별시", "11" },
                { "부산광역시", "21" },
                { "대구광역시", "22" },
                { "인천광역시", "23" },
                { "광주광역시", "24" },
                { "대전광역시", "25" }
            };

            CboDivision.DataSource = new BindingSource(dic, null);
            CboDivision.DisplayMember = "Key";
            CboDivision.ValueMember = "Value";*/
            #endregion


        }

        private void DataProcess()
        {
            if (string.IsNullOrEmpty(TxtNames.Text) ||
                CboLevels.SelectedIndex <= 0 ||
                string.IsNullOrEmpty(TxtAddr.Text) ||
                string.IsNullOrEmpty(TxtMobile.Text))
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
                    query = insertQuery;
                    break;
                case BaseMode.UPDATE:
                    query = updateQuery;
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
                    connection.Open();

                    MySqlCommand command = new MySqlCommand()
                    {
                        Connection = connection,
                        CommandText = query
                    };

                    List<MySqlParameter> paramList = new List<MySqlParameter>()
                    {
                        new MySqlParameter("@Names", MySqlDbType.VarChar, TxtNames.MaxLength)
                        {
                            Value = TxtNames.Text
                        },
                        new MySqlParameter("@Levels", MySqlDbType.VarChar, CboLevels.MaxLength)
                        {
                            Value = CboLevels.SelectedValue
                        },
                        new MySqlParameter("@Addr", MySqlDbType.VarChar, TxtAddr.MaxLength)
                        {
                            Value = TxtAddr.Text
                        },
                        new MySqlParameter("@Mobile", MySqlDbType.VarChar, TxtMobile.MaxLength)
                        {
                            Value = TxtMobile.Text
                        },
                        new MySqlParameter("@Email", MySqlDbType.VarChar, TxtEmail.MaxLength)
                        {
                            Value = TxtEmail.Text
                        }
                    };

                    if (baseMode != BaseMode.INSERT)
                    {
                        paramList.Add(new MySqlParameter("@Idx", MySqlDbType.Int32)
                        {
                            Value = TxtIdx.Text
                        });
                    }

                    foreach (var item in paramList)
                    {
                        command.Parameters.Add(item);
                    }

                    int result = command.ExecuteNonQuery();

                    string resultStr = string.Empty;
                    switch (baseMode)
                    {
                        case BaseMode.INSERT:
                            resultStr = $"{result} Data Inserted";
                            break;
                        case BaseMode.UPDATE:
                            resultStr = $"{result} Data Updated";
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
                RefreshGridData();
            }
        }

        private void CboDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CboLevels.SelectedIndex <= 0)
            {
                return;
            }
        }
    }
}
