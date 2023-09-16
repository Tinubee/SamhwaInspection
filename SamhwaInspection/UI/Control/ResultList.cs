using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SamhwaInspection.UI.Control
{
    public partial class ResultList : DevExpress.XtraEditors.XtraUserControl
    {
        public ResultList()
        {
            InitializeComponent();
        }

        public void Init()
        {
            this.e시작일자.DateTime = DateTime.Today;

            this.GridView1.Init(this.barManager1);
            this.GridView2.Init(this.barManager1);
            this.GridControl1.DataSource = Global.검사자료;
            this.GridView1.RowCountChanged += GridView1_RowCountChanged;
            //this.GridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            this.b자료조회.Click += B자료조회_Click;
            this.b엑셀파일.Click += B엑셀파일_Click;


            Global.환경설정.결과상태갱신알림 += 환경설정_결과상태갱신알림;
        }

        private void GridView1_RowCountChanged(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            view.MoveFirst();
            
        }

        //private void GridView1_RowCountChanged(object sender, EventArgs e)
        //{
        //    (sender as GridView).MoveFirst();
        //}

        private void 환경설정_결과상태갱신알림()
        {
            //Global.검사자료.Load(this.e시작일자.DateTime);
            //GridControl1.Refresh();
            //GridView1.RefreshData();
            //GridControl1.RefreshDataSource();
            //B자료조회_Click(null, null);
        }

        private void B엑셀파일_Click(object sender, EventArgs e)
        {
            this.GridView1.BtnXlsExportViewClick(null, null);
        }

        private void B자료조회_Click(object sender, EventArgs e)
        {
            Global.검사자료.Load(this.e시작일자.DateTime);
            
        }
    }
}
