using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Euresys.MultiCam;
using Newtonsoft.Json;
using IvmUtils;
using SamhwaInspection.Utils;
using System.Diagnostics;

namespace SamhwaInspection.Schemas
{
    #region EnumSection
    public enum 조명구분
    {
        TOP1 = 1,
        TOP2 = 2,
        TOP3 = 3,
        BACK = 4,
    }
    public enum 조명포트
    {
        None,
        COM1,
        COM2,
        COM3,
        COM4,
    }

    public enum 조명채널
    {
        CH1 = 1,
        CH2 = 2,
        CH3 = 3,
        CH4 = 4,
        CH5 = 5,
        CH6 = 6,
        CH7 = 7,
        CH8 = 8,
        CH9 = 9,
        CH10 = 10,
        CH11 = 11,
    }
    #endregion

    public class LightControl : BindingList<조명정보>
    {
        [JsonIgnore]
        private LightController Controller1;
        [JsonIgnore]
        private LightController Controller2;
        [JsonIgnore]
        private string 저장파일 { get { return Path.Combine(Global.환경설정.기본경로, "조명설정.json"); } }
        private const String 로그영역 = "조명컨트롤";
        public void Init()
        {
            
            this.Controller1 = new LightController(조명포트.COM3, 19200, $"{Convert.ToChar(2)}", $"{Convert.ToChar(3)}");
            this.Controller2 = new LightController(조명포트.COM4, 19200, $"{Convert.ToChar(2)}", $"{Convert.ToChar(3)}");
            this.Controller1.Init();
            this.Controller2.Init();

            this.Add(new 조명정보() { 구분 = 조명구분.TOP1, 채널 = 조명채널.CH1, 밝기 = 100, lightController = Controller1, 포트 = Controller1.포트 });
            this.Add(new 조명정보() { 구분 = 조명구분.TOP2, 채널 = 조명채널.CH2, 밝기 = 100, lightController = Controller1, 포트 = Controller1.포트 });
            this.Add(new 조명정보() { 구분 = 조명구분.BACK, 채널 = 조명채널.CH1, 밝기 = 100, lightController = Controller2, 포트 = Controller2.포트 });
            Debug.WriteLine("조명정보 추가 완료");
            this.Save();
            this.Open();
            foreach (조명정보 조명 in this) 조명.Set();
            this.TurnOff();
        }

        public 조명정보 GetItem(조명구분 구분, 조명포트 포트, 조명채널 채널)
        {
            foreach (조명정보 조명 in this)
                if (조명.구분 == 구분 && 조명.포트 == 포트 && 조명.채널 == 채널) return 조명;
            return null;
        }

        public void Load()
        {
            if (!File.Exists(this.저장파일)) return;
            try
            {
                List<조명정보> 자료 = JsonConvert.DeserializeObject<List<조명정보>>(File.ReadAllText(this.저장파일), Common.JsonSetting());
                foreach (조명정보 정보 in 자료)
                {
                    조명정보 조명 = this.GetItem(정보.구분, 정보.포트, 정보.채널);
                    if (조명 == null) continue;
                    조명.Set(정보);
                }
            }
            catch (Exception ex)
            {
                //Global.오류로그(로그영역, "조명 설정 로드", ex.Message, false);
            }
        }

        public void Save()
        {
            if (!IvmUtils.Utils.WriteAllText(저장파일, JsonConvert.SerializeObject(this, IvmUtils.Utils.JsonSetting())))
            {
                //Global.오류로그(로그영역, "조명설정 저장", "조명 설정 저장에 실패하였습니다.", true);
            }
        }

        public void Open()
        {
            if (!this.Controller1.Open())
            {
                this.Controller1.Close();
                Global.오류로그(로그영역, "조명장치 연결", "조명 컨트롤러1에 연결할 수 없습니다.", true);
            }
            if (!this.Controller2.Open())
            {
                this.Controller2.Close();
                Global.오류로그(로그영역, "조명장치 연결", "조명 컨트롤러2에 연결할 수 없습니다.", true);
            }
        }
        public void Close()
        {
            this.TurnOff();
            if (this.Controller1 != null)
            {
                this.Controller1.Close();
            }
            if (this.Controller2 != null)
            {
                this.Controller2.Close();
            }
        }

        public void TurnOn()
        {
            foreach (조명정보 정보 in this)
                정보.TurnOn();
        }

        public void TurnOn(조명구분 구분)
        {
            foreach (조명정보 정보 in this)
                if (정보.구분 == 구분)
                    정보.TurnOn();
        }

        public void TurnOff()
        {
            foreach (조명정보 정보 in this)
                정보.TurnOff();
        }

        public void TurnOff(조명구분 구분)
        {
            foreach (조명정보 정보 in this)
                if (정보.구분 == 구분)
                    정보.TurnOff();
        }

        public void TurnOnOff(조명구분 구분, Boolean IsOn)
        {
            if (IsOn) this.TurnOn(구분);
            else this.TurnOff(구분);
        }




    }


    public class 조명정보
    {
        [JsonProperty("LightType")]
        public 조명구분 구분 { get; set; } = 조명구분.TOP1;
        [JsonProperty("Port")]
        public 조명포트 포트 { get; set; } = 조명포트.None;
        [JsonProperty("Channel")]
        public 조명채널 채널 { get; set; } = 조명채널.CH1;
        [JsonProperty("Brightness")]
        public Int32 밝기 { get; set; } = 100;
        [JsonProperty("Note")]
        public String 설명 { get; set; } = String.Empty;
        [JsonProperty("Use")]
        public Boolean 사용유무 { get; set; } = true;
        [JsonIgnore]
        public Boolean 켜짐 { get; set; } = false;
        [JsonIgnore]
        public LightController lightController;

        public Boolean Get() { return this.lightController.Get(this); }
        public Boolean Set()
        {
            this.켜짐 = this.lightController.Set(this);
            return this.켜짐;
        }
        public Boolean TurnOn()
        {
            this.lightController.TurnOn(this);
            //if (this.켜짐) 
            //{
            //    this.켜짐 = this.lightController.TurnOn(this);
            //    return true;
            //}
            this.켜짐 = true;
            return this.켜짐;
        }
        public Boolean TurnOff()
        {
            this.lightController.TurnOff(this);
            //if (!this.켜짐) return true;
            this.켜짐 = false;
            return this.켜짐;
        }
        public Boolean OnOff()
        {
            if (this.켜짐) return this.TurnOn();
            else return this.TurnOff();
        }

        public void Set(조명정보 정보)
        {
            this.밝기 = 정보.밝기;
            this.설명 = 정보.설명;
        }
    }
}
