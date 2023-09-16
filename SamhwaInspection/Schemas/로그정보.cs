using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SamhwaInspection.Schemas
{
    public enum 로그구분
    {
        정보 = 0,
        경고 = 5,
        오류 = 9,
    }

    [Table("logs")]
    public class 로그정보
    {
        [Column("ltime"), Required, Key]
        public DateTime 시간 { get; set; } = DateTime.Now;
        [Column("ltype")]
        public 로그구분 구분 { get; set; } = 로그구분.정보;
        [Column("larea")]
        public string 영역 { get; set; } = String.Empty;
        [Column("lsubj")]
        public string 제목 { get; set; } = String.Empty;
        [Column("lcont")]
        public string 내용 { get; set; } = String.Empty;
        [Column("luser")]
        public string 작업자 { get; set; } = String.Empty;

        public 로그정보() { }
        public 로그정보(string 영역, 로그구분 구분, string 제목, string 내용)
        {
            this.시간 = DateTime.Now;
            this.구분 = 구분;
            this.영역 = 영역;
            this.제목 = 제목;
            this.내용 = 내용;
            this.작업자 = Global.환경설정.사용자명;
        }
    }

    public class 로그자료 : BindingList<로그정보>
    {
        public const string 로그영역 = nameof(로그자료);

        public 로그자료()
        {
            this.AllowEdit = true;
            this.AllowRemove = true;
        }

        public void Init()
        {
            this.Load();
        }

        public void Load()
        {
            this.Load(DateTime.Today, DateTime.Today);
        }

        public async void Load(DateTime 시작, DateTime 종료)
        {
            this.Clear();
            this.RaiseListChangedEvents = false;
            List<로그정보> 자료 = null;
            using (로그테이블 Table = new 로그테이블())
                자료 = await Table.Select(시작, 종료);
            if (자료 == null) return;

            자료.ForEach(e => this.Add(e));
            this.RaiseListChangedEvents = true;
            this.ResetBindings();
        }

        public void Close()
        {
            using (로그테이블 Table = new 로그테이블())
                Table.자료정리(Global.환경설정.로그보관);
        }

        // 최종 입력시간 체크
        private DateTime LastInsert = DateTime.Today;
        private void InsertTimeCheck(로그정보 로그)
        {
            if (로그.시간 <= this.LastInsert)
            {
                로그.시간 = DateTime.Now.AddMilliseconds(1);
                while (로그.시간 <= this.LastInsert)
                    로그.시간 = 로그.시간.AddMilliseconds(1);
            }
            this.LastInsert = 로그.시간;
        }
        public 로그정보 Add(string 영역, 로그구분 구분, string 제목, string 내용)
        {
            로그정보 로그 = new 로그정보(영역, 구분, 제목, 내용);
            InsertTimeCheck(로그);
            this.Insert(0, 로그);
            Task.Run(async () => {
                using (로그테이블 Table = new 로그테이블())
                    await Table.InsertAsync(로그);
            });
            return 로그;
        }
    }

    public class 로그테이블 : BaseTable
    {
        public DbSet<로그정보> DbSet { get; set; }
        protected override String 로그영역 { get { return nameof(로그테이블); } }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<로그정보>().Property(e => e.구분).HasConversion(new EnumToNumberConverter<로그구분, Int32>());
            base.OnModelCreating(modelBuilder);
        }

        public async Task<int> InsertAsync(로그정보 정보)
        {
            this.DbSet.Add(정보);
            if (this.DbConn == null) return 0;
            try { return await this.SaveChangesAsync(); }
            catch (Exception ex) { IvmUtils.Utils.DebugException(ex, 3, "로그저장"); }
            return 0;
        }

        public async Task<List<로그정보>> Select()
        {
            return await this.Select(DateTime.Today);
        }
        public async Task<List<로그정보>> Select(DateTime 날짜)
        {
            DateTime 시작 = new DateTime(날짜.Year, 날짜.Month, 날짜.Day);
            return await this.Select(시작, 시작);
        }
        public async Task<List<로그정보>> Select(DateTime 시작, DateTime 종료)
        {
            IOrderedQueryable<로그정보> query = (
                from n in DbSet
                where n.시간 >= 시작 && n.시간 < 종료.AddDays(1)
                orderby n.시간 descending
                select n);
            return await query.AsNoTracking().ToListAsync();
        }

        public int 자료정리(int 일수)
        {
            DateTime 일자 = DateTime.Today.AddDays(-일수);
            String Sql = $@"DELETE FROM logs WHERE ltime < DATE('{IvmUtils.Utils.FormatDate(일자, "{0:yyyy-MM-dd}")}')";
            try
            {
                int AffectedRows = 0;
                using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand(Sql, this.DbConn))
                {
                    if (cmd.Connection.State != ConnectionState.Open)
                        cmd.Connection.Open();
                    AffectedRows = cmd.ExecuteNonQuery();
                }
                return AffectedRows;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Global.오류로그(로그영역, "로그정리 오류", ex.Message, true);
            }
            return -1;
        }
    }
}
