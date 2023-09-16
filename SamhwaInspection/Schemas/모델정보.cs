using DevExpress.Data.Utils;
using DevExpress.Utils.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace SamhwaInspection.Schemas
{
    public class 모델정보
    {
        private const String 로그영역 = "모델정보";

        [JsonProperty("Number")]
        public Int32 모델번호 { get; set; } = 0;
        [JsonProperty("ModelName")]
        public String 모델이름 { get; set; } = String.Empty;
        [JsonProperty("ModelPath")]
        public String 모델저장폴더 { get; set; } = String.Empty;
        [JsonProperty("SolutionPath")]
        public String 솔루션파일저장경로 { get; set; } = String.Empty;
        [JsonProperty("DisplayCount")]
        public Int32 디스플레이개수 { get; set; } = 0;
        [JsonIgnore]
        private String 검사목록파일 { get; set; } = String.Empty;
        [JsonIgnore]
        public String 마스터이미지1 { get; set; } = String.Empty;
        [JsonIgnore]
        public String 마스터이미지2 { get; set; } = String.Empty;
        [JsonIgnore]
        public BindingList<검사정보> 검사목록 { get; set; } = new BindingList<검사정보>();

        public void Init()
        {
            모델저장폴더 = Path.Combine(Global.환경설정.모델저장경로, 모델이름);
            검사목록파일 = Path.Combine(모델저장폴더, "Tools.json");
            마스터이미지1 = Path.Combine(모델저장폴더, "MasterImage1.bmp");
            마스터이미지2 = Path.Combine(모델저장폴더, "MasterImage2.bmp");
            솔루션파일저장경로 = Path.Combine(모델저장폴더, $"{모델이름}.sol");

            //폴더 존재 유무 체크 및 없으면 생성
            if (!Utils.Common.DirectoryExists(모델저장폴더, true))
            {
                Global.오류로그(로그영역, "저장경로", "모델저장 경로 디렉토리를 생성할 수 없습니다.", true);
                return;
            }
            this.Load();
        }

        public void Close()
        {
            //this.Save();
        }

        public void Load()
        {
            if (!File.Exists(검사목록파일))
            {
                Global.정보로그(로그영역, "검사목록로드", "검사목록이 없습니다.", false);
                return;
            }
            try
            {
                BindingList<검사정보> 목록 = JsonConvert.DeserializeObject<BindingList<검사정보>>(File.ReadAllText(검사목록파일));
                if (목록 == null)
                {
                    검사목록 = new BindingList<검사정보>();
                    return;
                }
                else
                {
                    검사목록 = 목록;
                }
            }
            catch (Exception ex)
            {
                Global.오류로그(로그영역, "검사목록로드", ex.Message, false);
            }
        }

        public void Save()
        {
            모델저장폴더 = Path.Combine(Global.환경설정.모델저장경로, 모델이름);
            검사목록파일 = Path.Combine(모델저장폴더, "Tools.json");
            
            //폴더 존재 유무 체크 및 없으면 생성
            if (!Utils.Common.DirectoryExists(모델저장폴더, true))
            {
                Global.오류로그(로그영역, "저장경로", "모델저장 경로 디렉토리를 생성할 수 없습니다.", true);
                return;
            }
            if (!IvmUtils.Utils.WriteAllText(검사목록파일, JsonConvert.SerializeObject(this.검사목록, IvmUtils.Utils.JsonSetting())))
                Global.오류로그(로그영역, "검사목록저장", "목록저장에 실패하였습니다.", false);
            else Debug.WriteLine(검사목록파일, "검사목록 저장완료");
        }

        

        public String 마스터이미지경로(CameraType camera)
        {
            return Path.Combine(모델저장폴더, $"MasterImage{(Int32)camera}.bmp");
        }

        public void 검사현황초기화()
        {
            //추가 예정
        }

        public List<검사정보> 선택카메라검사목록(CameraType type)
        {
            List<검사정보> 선택카메라검사목록 = new List<검사정보>();

            this.검사목록.ForEach(x => {
                if (x.카메라구분 == type) 선택카메라검사목록.Add(x);
            });
            return 선택카메라검사목록;
        }

        public void SelectAll(CameraType type, Boolean selected)
        {
            this.검사목록.ForEach(x => { 
                if (x.카메라구분 == type) x.rectangle.Selected = selected; 
            });
        }

        public IvLibs.Graphics.GraphicCollections GetRegions(CameraType type)
        {
            IvLibs.Graphics.GraphicCollections list = new IvLibs.Graphics.GraphicCollections();
            this.검사목록.ForEach(x =>
            {
                if (x.카메라구분 == type)
                    list.Add(x.rectangle);
            });
            return list;
        }
    }

    public class 모델자료 : BindingList<모델정보>
    {
        private const String 로그영역 = "모델자료";
        private String 모델목록파일 { get { return Path.Combine(Global.환경설정.모델저장경로, $"모델목록.json"); } }
        public 모델정보 선택모델 { get { return this.GetItem(Global.환경설정.선택모델);} }
        public void Init()
        {
            this.Load();
            this.ForEach(x => x.Init());
            if (this.선택모델 == null) Global.환경설정.선택모델 = 0;
            this.선택모델?.검사현황초기화();
        }

        public void Close()
        {
            foreach (모델정보 정보 in this) 정보.Close();
        }

        private void Load()
        {
            if (!File.Exists(모델목록파일))
            {
                Global.정보로그(로그영역, "자료로드", "모델목록이 없습니다.", false);
                return;
            }
            try
            {
                List<모델정보> 모델목록 = JsonConvert.DeserializeObject<List<모델정보>>(File.ReadAllText(모델목록파일));
                if (모델목록 == null) return;
                모델목록.ForEach(e => this.Add(e));
            }
            catch (Exception ex)
            {
                Global.오류로그(로그영역, "자료로드", ex.Message, false);
            }
        }

        public 모델정보 GetItem(Int32 모델번호)
        {
            return this.Where(e => e.모델번호 == 모델번호).FirstOrDefault();
        }

        public void Save()
        {
            foreach (모델정보 정보 in this) 정보.Save();

            if (!IvmUtils.Utils.WriteAllText(모델목록파일, JsonConvert.SerializeObject(this, IvmUtils.Utils.JsonSetting())))
                Global.오류로그(로그영역, "목록저장", "목록저장에 실패하였습니다.", false);
            else Debug.WriteLine(모델목록파일, "모델목록 저장완료");
        }

        public void 모델변경적용()
        {
            this.Save();
            //foreach (모델정보 모델 in this)
            //{
            //    Utils.Common.DirectoryExists(모델.모델저장폴더, true);
            //}
                //모델.검사현황?.Save();
            //this.선택모델?.검사현황.Load();
        }

    }



}
