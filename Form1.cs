using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WC_InventoryApplication
{
    public partial class Form1 : Form
    {
        private string mode = "NOT SET";
        private DataBase db;

        public Form1()
        {
            InitializeComponent();
            enableButton();
            disableText();

            db = new DataBase();
            loadRecords();
        }

        public void enableButton()
        {
            btn_Add.Enabled = true;
            btn_Update.Enabled = true;
            btn_Delete.Enabled = true;
            btn_Save.Enabled = false;
            btn_Cancel.Enabled = false;
        }

        public void disableButton()
        {
            btn_Add.Enabled = false;
            btn_Update.Enabled = false;
            btn_Delete.Enabled = false;
            btn_Save.Enabled = true;
            btn_Cancel.Enabled = true;
        }

        public void enabletext()
        {
            txt_ItemID.Enabled = true;
            txt_ItemName.Enabled = true;
            txt_Price.Enabled = true;
            txt_Quantity.Enabled = true;
        }

        public void disableText()
        {
            txt_ItemID.Enabled = false;
            txt_ItemName.Enabled = false;
            txt_Price.Enabled = false;
            txt_Quantity.Enabled = false;
        }

        public void clearText()
        {
            txt_ItemID.Text = "";
            txt_ItemName.Text = "";
            txt_Quantity.Text = "";
            txt_Price.Text = "";
        }

        public bool fieldValidity()
        {
            bool flag = false;

            if (txt_ItemName.Text.Trim() == "")
            {
                MessageBox.Show(this, "Item Name is Empty", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txt_Quantity.Text.Trim() == "")
            {
                DialogResult res = MessageBox.Show(this, "Quantity is Empty", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (res == DialogResult.OK)
                {
                    txt_Quantity.Text = "0";
                }
            }
            else if (txt_Price.Text.Trim() == "")
            {
                DialogResult res = MessageBox.Show(this, "Price is Empty", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (res == DialogResult.OK)
                {
                    txt_Price.Text = "0";
                }
            }
            else if (Convert.ToInt32(txt_Quantity.Text.Trim()) < 0)
            {
                MessageBox.Show(this, "Quantity is Negative", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (Convert.ToDecimal(txt_Price.Text.Trim()) < 0)
            {
                MessageBox.Show(this, "Price is Negative", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                flag = true;
            }

            return flag;
        }

        public void loadRecords()
        {
            string SQL = "SELECT * FROM inventoryitems ORDER BY ItemID DESC";
            db.openConnection();
            MySqlDataReader reader = db.GETdata(SQL);
            DataTable table = new DataTable();
            table.Load(reader);
            dataGridView.DataSource = table;
            db.closeConnection();
        }

        private void txt_ItemID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (mode == "ADD")
                {
                }
                else
                {
                    string SQL = "Select * FROM inventoryitems WHERE ItemID=" + txt_ItemID.Text.Trim();
                    db.openConnection();
                    MySqlDataReader reader = db.GETdata(SQL);
                    if (reader.Read())
                    {
                        txt_ItemName.Text = reader["ItemName"].ToString();
                        txt_Quantity.Text = reader["Quantity"].ToString();
                        txt_Price.Text = reader["Price"].ToString();
                    }
                    db.closeConnection();
                }
            }
        }


        /*==========================Button Operation==========================*/
        private void btn_Add_Click(object sender, EventArgs e)
        {
            mode = "ADD";
            disableButton();
            enabletext();
            txt_ItemID.Focus();
            txt_ItemID.Enabled = false;
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            mode = "UPDATE";
            disableButton();
            enabletext();
            txt_ItemID.Focus();
            
            if(lbl_instruction.Text == "Press Update or Delete Button You Need")
            {
                lbl_instruction.Text = ""; 
            }
            else
            {
                lbl_instruction.Text = "Press Enter Button After Item ID Add";
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            mode = "DELETE";
            txt_ItemID.Enabled = true;
            txt_ItemName.Enabled = false;
            txt_Quantity.Enabled = false;
            txt_Price.Enabled = false;
            txt_ItemID.Focus();
            disableButton();
            btn_Save.Text = "OK";

            if (lbl_instruction.Text == "Press Update or Delete Button You Need")
            {
                lbl_instruction.Text = "";
            }
            else
            {
                lbl_instruction.Text = "Press Enter Button After Item ID Add";
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (fieldValidity())
            {
                if (mode == "ADD")
                {
                    string SQL = "INSERT INTO inventoryitems (ItemName,Quantity,Price) VALUES (";
                    SQL = SQL + "'" + txt_ItemName.Text.Trim() + "',";
                    SQL = SQL + txt_Quantity.Text.Trim() + ",";
                    SQL = SQL + txt_Price.Text.Trim() + ")";
                    db.openConnection();
                    int x = db.SENDdata(SQL);
                    if (x > 0)
                    {
                        MessageBox.Show(this, "New Item Add Successful", "Alert", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    else
                    {
                        MessageBox.Show(this, "New Item Add Unsuccessful", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    db.closeConnection();
                }
                else if (mode == "UPDATE")
                {
                    string SQL = "UPDATE inventoryitems SET ";
                    SQL = SQL + "ItemName='" + txt_ItemName.Text.Trim() + "',";
                    SQL = SQL + "Quantity=" + txt_Quantity.Text.Trim() + ",";
                    SQL = SQL + "Price=" + txt_Price.Text.Trim() + " WHERE ItemID=" + txt_ItemID.Text.Trim();
                    db.openConnection();
                    int x = db.SENDdata(SQL);
                    if (x > 0)
                    {
                        MessageBox.Show(this, "Item Updated", "Alert", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    else
                    {
                        MessageBox.Show(this, "Item Update Unsuccessful", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    db.closeConnection();
                }
                else
                {
                    if (btn_Save.Text == "OK")
                    {
                        DialogResult result = MessageBox.Show(this, "Are You Sure Delete That Record ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            string SQL = "DELETE FROM inventoryitems WHERE ItemID=" + txt_ItemID.Text.Trim();
                            db.openConnection();
                            int x = db.SENDdata(SQL);
                            if (x > 0)
                            {
                                MessageBox.Show(this, "Item Delete Successful", "Alert", MessageBoxButtons.OK, MessageBoxIcon.None);
                            }
                            else
                            {
                                MessageBox.Show(this, "Item Delete Unsuccessful", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            db.closeConnection();
                        }
                    }
                }
                clearText();
                disableText();
                loadRecords();
                enableButton();
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            enableButton();
            disableText();
            clearText();
            lbl_instruction.Text = "";
            btn_Save.Text = "Save";
        }

       
        /*===========================Search & Grid===========================*/
        private void btn_Search_Click(object sender, EventArgs e)
        {
            string SQL = "SELECT * FROM inventoryitems WHERE ItemName LIKE '%" + txt_Search.Text.Trim() + "%'";
            db.openConnection();
            MySqlDataReader reader = db.GETdata(SQL);
            DataTable table = new DataTable();
            table.Load(reader);
            dataGridView.DataSource = table;
            db.closeConnection();
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            enabletext();
            btn_Add.Enabled = false;
            btn_Update.Enabled = true;
            btn_Delete.Enabled = true;
            btn_Cancel.Enabled = true;

            int rowIndex = e.RowIndex;
            int columnIndex = e.ColumnIndex;

            string itemID = dataGridView.Rows[rowIndex].Cells[0].Value.ToString();
            string itemName = dataGridView.Rows[rowIndex].Cells[1].Value.ToString();
            string quantity = dataGridView.Rows[rowIndex].Cells[2].Value.ToString();
            string price = dataGridView.Rows[rowIndex].Cells[3].Value.ToString();

            txt_ItemID.Text = itemID;
            txt_ItemName.Text = itemName;
            txt_Quantity.Text = quantity;
            txt_Price.Text = price;

            lbl_instruction.Text = "Press Update or Delete Button You Need";
        }
    }
}
