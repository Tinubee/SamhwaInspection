﻿using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SamhwaInspection.Schemas;
using System.Diagnostics.Eventing.Reader;

namespace SamhwaInspection.UI.Control
{
    public partial class BaseConfig : DevExpress.XtraEditors.XtraUserControl
    {
        public BaseConfig()
        {
            InitializeComponent();
        }

        public void Init()
        {

            this.Bing환경설정.DataSource = Global.환경설정;
            

            this.d기본경로.SelectedPath = Global.환경설정.기본경로;
            this.d사진저장.SelectedPath = Global.환경설정.이미지저장경로;
            this.d문서저장.SelectedPath = Global.환경설정.자료저장경로;

            this.e기본경로.Text = this.d기본경로.SelectedPath;
            this.e사진저장경로.Text = this.d사진저장.SelectedPath;
            this.e문서저장경로.Text = this.d문서저장.SelectedPath;

            this.e양품이미지저장여부.Toggled += E양품이미지저장여부_Toggled;
            this.e불량이미지저장여부.Toggled += E불량이미지저장여부_Toggled;
            
            


            this.e기본경로.ButtonClick += E기본경로_ButtonClick;
            this.e사진저장경로.ButtonClick += E사진저장_ButtonClick;
            this.e문서저장경로.ButtonClick += E문서저장_ButtonClick;


            this.GridView1.Init(this.barManager1);
            this.GridView1.AddPopupMemuItem("Test", IvmUtils.Resources.인쇄, ButtonPrintClick);
            this.GridView1.AddDeleteMenuItem(ButtonDeleteClick);
            this.GridView1.OptionsBehavior.Editable = true;
            this.GridView1.OptionsView.ShowAutoFilterRow = false;
            this.GridView1.OptionsView.ShowFooter = false;
            this.GridControl1.DataSource = Global.그랩제어;
            //this.GridView1.CellValueChanged += GridView1_CellValueChanged;


            this.GridView2.Init();
            this.GridView2.OptionsBehavior.Editable = true;
            this.GridView2.OptionsView.ShowAutoFilterRow = false;
            this.GridView2.OptionsView.ShowFooter = false;
            this.GridControl2.DataSource = Global.조명제어;
            this.GridView2.CellValueChanged += GridView2_CellValueChanged;
            this.e조명켜짐.Toggled += E켜짐_Toggled;

            this.btnSaveSetting.Click += BtnSaveSetting_Click;

            this.user1.Init();
            
        }

        private void E불량이미지저장여부_Toggled(object sender, EventArgs e)
        {
            if (e불량이미지저장여부.IsOn)
            {
                Global.환경설정.사진저장NG = true;
                return;
            }
            Global.환경설정.사진저장NG = false;
        }

        private void E양품이미지저장여부_Toggled(object sender, EventArgs e)
        {
            if (e양품이미지저장여부.IsOn)
            {
                Global.환경설정.사진저장OK = true;
                return;
            }
            Global.환경설정.사진저장OK = false;
        }

        private void E문서저장_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (this.d문서저장.ShowDialog() == DialogResult.OK)
                this.e문서저장경로.Text = this.d문서저장.SelectedPath;
        }

        private void E사진저장_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (this.d사진저장.ShowDialog() == DialogResult.OK)
                this.e사진저장경로.Text = this.d사진저장.SelectedPath;
        }

        private void E기본경로_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (this.d기본경로.ShowDialog() == DialogResult.OK)
                this.e기본경로.Text = this.d기본경로.SelectedPath;
        }

        private void GridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.Column.FieldName != this.colLineRate_Hz.FieldName) return;
            {
                GridView view = sender as GridView;
                Cam 정보 = view.GetRow(e.RowHandle) as Cam;
                정보?.Set();
                view.RefreshRow(e.RowHandle);
            } 
            return;
        }

        private void BtnSaveSetting_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            Global.그랩제어.Save();
            Global.조명제어.Save();


        }

        private void E켜짐_Toggled(object sender, EventArgs e)
        {
            조명정보 정보 = this.GridView2.GetRow(this.GridView2.FocusedRowHandle) as 조명정보;
            if (정보 == null) return;
            this.GridControl2.EmbeddedNavigator.Buttons.DoClick(this.GridControl2.EmbeddedNavigator.Buttons.EndEdit);
            정보.OnOff();
        }

        private void GridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName != this.col밝기.FieldName) return;
            GridView view = sender as GridView;
            조명정보 정보 = view.GetRow(e.RowHandle) as 조명정보;
            정보?.Set();
            view.RefreshRow(e.RowHandle);
        }

        private void ButtonDeleteClick(object sender, ItemClickEventArgs e)
        {
            if (!IvmUtils.Utils.Confirm("선택한 항목을 제거 하시겠습니까?")) return;
            this.GridView1.DeleteSelectedRows();
        }

        private void ButtonPrintClick(object sender, ItemClickEventArgs e)
        {
            GridView view = sender as GridView;
            Cam cam =  view.GetFocusedRow() as Cam;
            if (cam != null) return;
            IvmUtils.Utils.DebugSerializeObject(cam);

        }
    }
}
