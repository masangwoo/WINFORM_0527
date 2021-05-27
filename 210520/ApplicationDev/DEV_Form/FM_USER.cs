﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DEV_Form
{
    public partial class FM_USER : BaseMDIChildForm
    {
        public FM_USER()
        {
            InitializeComponent();
        }

        public override void Inquire()
        {
            base.Inquire();

            //DB HELPER 선언
            DBHelper helper = new DBHelper(false);
            try
            {
                string sUserID = txtUserID.Text;
                string sUserName = txtUserName.Text;
                DataTable dtTemp = new DataTable();
                dtTemp = helper.FillTable("SP_USER_MSW_S1", CommandType.StoredProcedure,
                         helper.CreateParameter("USERID", sUserID),
                         helper.CreateParameter("USERNAME", sUserName));

                if (dtTemp.Rows.Count==0)
                {
                    dataGridView1.DataSource = null;
                    MessageBox.Show("조회할 데이터가 없습니다.");
                }
                else
                {
                    //그리드 뷰에 데이터 삽입
                    dataGridView1.DataSource = dtTemp;
                }
            }

            catch (Exception ex){ }

            finally { helper.Close(); }
        }

        public override void DoNew()
        {
            base.DoNew();
            DataRow dr = ((DataTable)dataGridView1.DataSource).NewRow();
            ((DataTable)dataGridView1.DataSource).Rows.Add(dr);
        }

        public override void Delete()
        {
            base.Delete();
            if (dataGridView1.Rows.Count == 0) return;
            int iSelectIndex = dataGridView1.CurrentCell.RowIndex;
            DataTable dtTemp = (DataTable)dataGridView1.DataSource;
            dtTemp.Rows[iSelectIndex].Delete();

           
        }
    }
}
