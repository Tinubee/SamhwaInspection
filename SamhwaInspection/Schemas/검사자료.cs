using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraPrinting.Export;
using SamhwaInspection.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace SamhwaInspection.Schemas
{
    [Table("inspl")]
    public class 검사결과
    {
        [Column("ilwdt"), Required, Key]
        public DateTime 검사일시 { get; set; } = DateTime.Now;
        [Column("ilmni")]
        public Int32 모델번호 { get; set; } = Global.모델자료.선택모델.모델번호;
        [Column("ilres")]
        public Boolean 최종결과 { get; set; } = true;
        [NotMapped]
        public List<검사정보일시> 검사내역 { get; set; } = new List<검사정보일시>();

        public 검사결과() { }
        public 검사결과(BindingList<검사정보> 검사항목)
        {
            검사일시 = DateTime.Now;
            모델번호 = Global.모델자료.선택모델.모델번호;
            foreach (검사정보 검사정보 in 검사항목)
            {
                if (검사정보.판정 != 결과구분.OK)
                {
                    최종결과 = false;
                }
                검사내역.Add(new 검사정보일시(검사정보, 검사일시));
            }
        }

        public void AddToDb()
        {
            Task.Run(async () =>
            {
                using (검사결과테이블 Table = new 검사결과테이블())
                    await Table.InsertAsync(this);
            });
        }
    }
    
    [Table("inspd")]
    public class 검사정보일시
    {
        [Column("idwdt", Order = 0), Required, Key]
        public DateTime 일시 { get; set; } = DateTime.Now;
        [Column("idnum", Order = 1), Required, Key]
        public Int32 검사번호 { get; set; } = 0;
        [Column("idcam")]
        public CameraType 카메라구분 { get; set; } = CameraType.None;
        [Column("idmin")]
        public Decimal 최소 { get; set; } = 0m;
        [Column("idstd")]
        public Decimal 기준 { get; set; } = 0m;
        [Column("idmax")]
        public Decimal 최대 { get; set; } = 0m;
        [Column("idval")]
        public Decimal 측정 { get; set; } = 0m;
        [Column("idres")]
        public 결과구분 판정 { get; set; } = 결과구분.NO;
        public 검사정보일시() { }

        public 검사정보일시(검사정보 정보, DateTime 일시)
        {
            this.일시 = 일시;
            this.카메라구분 = 정보.카메라구분;
            this.검사번호 = 정보.검사번호;
            this.최소 = Convert.ToDecimal(정보.최소);
            this.기준 = Convert.ToDecimal(정보.기준);
            this.최대 = Convert.ToDecimal(정보.최대);
            this.측정 = Convert.ToDecimal(정보.측정);
            this.판정 = 정보.판정;
        }
    }

    public class 검사결과테이블 : BaseTable
    {
        protected override String 로그영역 { get { return nameof(검사결과테이블); } }
        protected String 삭제오류 { get; set; } = "삭제 중 오류가 발생하였습니다.";
        private DbSet<검사결과> 검사결과 { get; set; }
        private DbSet<검사정보일시> 검사정보일시 { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Debug.WriteLine(nameof(this.OnModelCreating));
            
            modelBuilder.Entity<검사정보일시>().HasKey(e => new { e.일시, e.검사번호 });
            Debug.WriteLine("HasKey완료");
            modelBuilder.Entity<검사정보일시>().Property(e => e.카메라구분).HasConversion(new EnumToNumberConverter<CameraType, Int32>());
            Debug.WriteLine("HasConversion(카메라구분)완료");
            modelBuilder.Entity<검사정보일시>().Property(e => e.판정).HasConversion(new EnumToNumberConverter<결과구분, Int32>());
            Debug.WriteLine("HasConversion(판정)완료");
            base.OnModelCreating(modelBuilder);
            Debug.WriteLine("modelBuilder 완료");
        }

        public void Add(검사결과 결과)
        {
            Debug.WriteLine("Add시작");
            this.검사결과.Add(결과);
            Debug.WriteLine("Add완료 AddRange시작");
            this.검사정보일시.AddRange(결과.검사내역);
            Debug.WriteLine("AddRange완료");
        }

        public async Task<int> InsertAsync(검사결과 결과)
        {
            Debug.WriteLine("Add시작");
            this.검사결과.Add(결과);
            Debug.WriteLine("Add완료 AddRange시작");
            this.검사정보일시.AddRange(결과.검사내역);
            Debug.WriteLine("AddRange완료");
            if (this.DbConn == null) return 0;
            try
            {
                Debug.WriteLine("2");
                return await this.SaveChangesAsync();
                
            }
            catch (Exception ex)
            {

                Debug.WriteLine("3");

                IvmUtils.Utils.DebugException(ex, 3, "결과저장");

            }
            return 0;
        }

        public void Save()
        {
            try { this.SaveChanges(); }
            catch (Exception ex) { Debug.WriteLine(ex.ToString(), "자료저장"); }
        }

        public List<검사결과> Select()
        {
            return this.Select(DateTime.Today);
        }

        public List<검사결과> Select(DateTime 날짜)
        {
            DateTime 검색날짜 = new DateTime(날짜.Year, 날짜.Month, 날짜.Day);

            //query1은 투입제품 최종 판정 자료
            IOrderedQueryable<검사결과> query1 = (
               from n in 검사결과
               where n.검사일시 >= 검색날짜 && n.검사일시 < 검색날짜.AddDays(1)
               orderby n.검사일시 descending
               select n);
            List<검사결과> 자료 = query1.AsNoTracking().ToList();
            
            //query2는 제품 내 검사항목에 대한 개별 판정 자료
            IOrderedQueryable<검사정보일시> query2 = (
                from n in 검사정보일시
                where n.일시 >= 검색날짜 && n.일시 < 검색날짜.AddDays(1)
                orderby n.일시 ascending
                orderby n.카메라구분 ascending
                orderby n.검사번호 ascending
                select n);
            List<검사정보일시> 정보  = query2.AsNoTracking().ToList();

            자료.ForEach(l =>
            {
                l.검사내역.AddRange(정보.Where(d => d.일시 == l.검사일시).ToList().OrderBy(e => e.카메라구분).ThenBy(e => e.검사번호));
            });

            return 자료;
        }

        public int 자료정리(int 일수)
        {
            DateTime 일자 = DateTime.Today.AddDays(-일수);
            String Day = IvmUtils.Utils.FormatDate(일자, "{0:yyyy-MM-dd}");
            String Sql = $"DELETE FROM inspd WHERE idwdt < DATE('{Day}');\nDELETE FROM inspl WHERE ilwdt < DATE('{Day}');";
            try
            {
                int AffectedRows = 0;
                using(NpgsqlCommand cmd = new NpgsqlCommand(Sql, this.DbConn))
                {
                    if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                    AffectedRows = cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
                return AffectedRows;
            }catch(Exception ex)
            {
                Global.오류로그(로그영역, "Remove DATA", ex.Message, true);
            }
            return -1;
        }
    }

    public class 검사자료 : BindingList<검사결과>
    {
        [JsonIgnore]
        public static String 로그영역 { get { return nameof(검사자료); } }
        [JsonIgnore]
        protected String 저장오류 { get; set; } = "저장 중 오류가 발생하였습니다.";
        [JsonIgnore]
        private 검사결과테이블 테이블 = null;


        public void Init()
        {
            this.AllowEdit = true;
            this.AllowRemove = true;
            this.테이블 = new 검사결과테이블();
            this.Load();
        }

        public Boolean Close()
        {
            if(this.테이블 == null) return false;
            this.테이블.자료정리(Global.환경설정.결과보관);
            return this.Save();
        }

        public Boolean Save()
        {
            DateTime 날짜 = DateTime.Today;
            try
            {
                List<검사결과> 자료 = this.테이블.Select(날짜);
                if (자료.Count < 1) return true;
                return true;
            }
            catch(Exception ex)
            {
                Global.오류로그(로그영역, 저장오류, $"{저장오류}\r\n{ex.Message}", true);
            }
            return false;
        }

        public void Load()
        {
            this.Load(DateTime.Today);
        }

        public void Load(DateTime 날짜)
        {
            this.Clear();
            this.RaiseListChangedEvents = false;
            List<검사결과> 자료 = this.테이블.Select(날짜);
            자료.ForEach(e => this.Add(e));
            this.RaiseListChangedEvents = true;
            this.ResetBindings();
        }

        public void AddResult(검사결과 자료)
        {
            this.RaiseListChangedEvents = false;
            //this.Add(자료);
            this.Insert(0, 자료);
            this.RaiseListChangedEvents = true;
            this.ResetBindings();
            Debug.WriteLine("결과 추가 완료");
        }
    }
}

     

