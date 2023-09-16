using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Windows.Forms;

namespace SamhwaInspection.Schemas
{
    public enum 유저권한구분
    {
        없음 = 0,
        작업자 = 3,
        관리자 = 5,
        시스템 = 9,
    }

    [Table("user")]
    public class 유저정보
    {
        [Column("uname"), Required, Key, DisplayName("사용자성명")]
        public string 성명 { get; set; } = string.Empty;

        [Column("upass"), DisplayName("비밀번호")]
        public string 암호 { get; set; } = string.Empty;

        [Column("unote")]
        public string 비고 { get; set; } = string.Empty;

        [Column("uperm"), DisplayName("접근권한")]
        public 유저권한구분 권한 { get; set; } = 유저권한구분.작업자;

        [Column("uallow"), DisplayName("접근허용")]
        public bool 허용 { get; set; } = true;
    }

    public class 유저자료 : BindingList<유저정보>
    {
        private String 저장파일
        { get { return Path.Combine(Global.환경설정.기본경로, "유저정보.cnf"); } }

        public Boolean Init()
        {
            return this.Load();
        }

        public void Close()
        {
        }

        public Boolean Load()
        {
            if (!File.Exists(저장파일))
            {
                this.Add(new 유저정보() { 성명 = 유저권한구분.작업자.ToString(), 암호 = "1234", 권한 = 유저권한구분.작업자 });
                this.Add(new 유저정보() { 성명 = 유저권한구분.관리자.ToString(), 암호 = "1234", 권한 = 유저권한구분.관리자 });
                this.Add(new 유저정보() { 성명 = 유저권한구분.시스템.ToString(), 암호 = "1234", 권한 = 유저권한구분.시스템 });
                this.Save();
                return true;
            }

            try
            {
                String json = Utils.Common.StringCipher.Decrypt(File.ReadAllText(저장파일), Global.GetGuid());
                List<유저정보> 자료 = JsonConvert.DeserializeObject<List<유저정보>>(json);
                자료.ForEach(e => this.Add(e));
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("정보로드 에러");
                Global.오류로그("유저", "정보로드", "사용자 정보를 불러올 수 없습니다.\n" + ex.Message, true);
            }
            return false;
        }

        public void Save()
        {
            File.WriteAllText(저장파일, Utils.Common.StringCipher.Encrypt(JsonConvert.SerializeObject(this), Global.GetGuid()));
        }
    }
}