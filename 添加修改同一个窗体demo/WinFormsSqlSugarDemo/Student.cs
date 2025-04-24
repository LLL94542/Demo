using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsSqlSugarDemo
{
    // 学生实体类
    [SugarTable("Student")] // 指定表名
    public class Student
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)] // 主键且自增
        public int Id { get; set; }

        [SugarColumn(Length = 50) ] // 设置长度
        [DisplayName("姓名")]
        public string Name { get; set; }

        [DisplayName("年龄")]
        public int Age { get; set; }
        [DisplayName("性别")]
        [SugarColumn(Length = 10)]
        public string Gender { get; set; }

        [SugarColumn(IsNullable = true)]
        [DisplayName("创建时间")]
        public DateTime CreateTime { get; set; }
    }
}
