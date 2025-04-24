using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsSqlSugarDemo
{
    public static class Helper
    {
        // 创建SqlSugarClient实例
        public static SqlSugarScope db = new SqlSugarScope(new ConnectionConfig()
        {
            ConnectionString = "server=localhost;Database=Testdb;Uid=root;Pwd=123456;",
            DbType = SqlSugar.DbType.MySql, // 数据库类型
            IsAutoCloseConnection = true // 自动释放
        },
        db =>
        {
            // 配置AOP
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql); // 输出SQL
            };
        });

        public static void InitializeDatabase()
        {
            try
            {
                db.DbMaintenance.CreateDatabase(); // 创建数据库(如果使用SQLite，这个方法是检查数据库文件是否存在)

                // 创建Student表(如果不存在)
                db.CodeFirst.InitTables(typeof(Student));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"数据库初始化失败: {ex.Message}");
            }
        }

    }
}
