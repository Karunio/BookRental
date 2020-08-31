using System;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using MetroFramework.Forms;
using MetroFramework;
using System.Data;
using System.Collections.Generic;

namespace BookRental.SubItems
{
    public partial class BooksMngForm : MetroForm
    {
        private static readonly string tableName = "bookstbl";
        private static readonly string comboQuery = @"SELECT Division, Names
                                                      From divtbl";
        private static readonly string selectQuery = @"SELECT 
                                                           Idx,
                                                           Author,
                                                           bookstbl.Division AS Division,
                                                           divtbl.Names AS Genre,
                                                           bookstbl.Names AS Names,
                                                           ReleaseDate,
                                                           ISBN,
                                                           Price
                                                       FROM
                                                           bookstbl
                                                               INNER JOIN
                                                           divtbl ON bookstbl.Division = divtbl.Division
                                                       ORDER BY Idx";
        private static readonly string updateQuery = @"UPDATE bookstbl 
                                                       SET 
                                                           Author = @Author,
                                                           Division = @Division,
                                                           Names = @Names,
                                                           ReleaseDate = @ReleaseDate,
                                                           ISBN = @ISBN,
                                                           Price = @Price
                                                       WHERE
                                                           Idx = @Idx;";
        private static readonly string insertQuery = @"INSERT INTO bookstbl
                                                           (Author,
                                                           Division,
                                                           Names,
                                                           ReleaseDate,
                                                           ISBN,
                                                           Price)
                                                       VALUES
                                                           (@Author,
                                                           @Division,
                                                           @Names,
                                                           @ReleaseDate,
                                                           @ISBN,
                                                           @Price)";
        private BaseMode baseMode = BaseMode.NONE;

        public BooksMngForm()
        {
            InitializeComponent();
        }

        private void DivMngForm_Load(object sender, EventArgs e)
        {
            RefreshGridData();
            UpdateComboDivision();
            InitControls();
        }

        private void UpdateComboDivision()
        {
            using (MySqlConnection connection = new MySqlConnection(Commons.CONNSTR))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand()
                {
                    Connection = connection,
                    CommandText = comboQuery
                };

                MySqlDataReader reader = command.ExecuteReader();

                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("선택", "");

                while (reader.Read())
                {
                    dic.Add(reader[1].ToString(), reader[0].ToString());
                }

                CboDivision.DataSource = new BindingSource(dic, null);
                CboDivision.DisplayMember = "Key";
                CboDivision.ValueMember = "Value";
            }
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
            DataGridViewColumn column;

            SetColumnProperty(GrdBooksTbl.Columns["Idx"], 100, "번호");
            SetColumnProperty(GrdBooksTbl.Columns["Author"], 100, "저자");
            SetColumnProperty(GrdBooksTbl.Columns["Division"], 100, "구분코드");
            SetColumnProperty(GrdBooksTbl.Columns["Genre"], 100, "장르");
            SetColumnProperty(GrdBooksTbl.Columns["Names"], 100, "이름");
            SetColumnProperty(GrdBooksTbl.Columns["ReleaseDate"], 100, "출간일");
            SetColumnProperty(GrdBooksTbl.Columns["ISBN"], 100, "ISBN");
            SetColumnProperty(GrdBooksTbl.Columns["Price"], 150, "가격");
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
            // Author
            TxtAuthor.Text = data.Cells["Author"].Value.ToString();
            // Genre
            //CboDivision.SelectedIndex = CboDivision.FindString(data.Cells["Genre"].Value.ToString());
            CboDivision.SelectedValue = data.Cells["Division"].Value;
            // Names
            TxtNames.Text = data.Cells["Names"].Value.ToString();
            // ReleaseDate
            DtReleaseDate.Value = DateTime.Parse(data.Cells["ReleaseDate"].Value.ToString());
            // ISBN
            TxtIsbn.Text = data.Cells["ISBN"].Value.ToString();
            // Price
            TxtPrice.Text = data.Cells["Price"].Value.ToString();
        }


        private void InitControls()
        {
            TxtIdx.Clear();
            TxtAuthor.Clear();
            TxtNames.Clear();
            TxtIsbn.Clear();
            TxtPrice.Clear();
            CboDivision.SelectedIndex = 0;
            DtReleaseDate.Value = DateTime.Today;

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
            if (string.IsNullOrEmpty(TxtAuthor.Text) ||
                CboDivision.SelectedIndex <= 0 ||
                string.IsNullOrEmpty(TxtNames.Text) ||
                string.IsNullOrEmpty(TxtIsbn.Text))
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

                    List<MySqlParameter> paramList = new List<MySqlParameter>();
                    paramList.Add(new MySqlParameter("@Author", MySqlDbType.VarChar, 45)
                    {
                        Value = TxtAuthor.Text
                    });
                    paramList.Add(new MySqlParameter("@Division", MySqlDbType.VarChar, 4)
                    {
                        Value = CboDivision.SelectedValue
                    });
                    paramList.Add(new MySqlParameter("@Names", MySqlDbType.VarChar, 45)
                    {
                        Value = TxtAuthor.Text
                    });
                    paramList.Add(new MySqlParameter("@ReleaseDate", MySqlDbType.Date)
                    {
                        Value = DtReleaseDate.Text
                    });
                    paramList.Add(new MySqlParameter("@ISBN", MySqlDbType.VarChar, 13)
                    {
                        Value = TxtAuthor.Text
                    });
                    paramList.Add(new MySqlParameter("@Price", MySqlDbType.Decimal)
                    {
                        Value = TxtPrice.Text
                    });


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
            if (CboDivision.SelectedIndex <= 0)
            {
                return;
            }
        }
    }
}
