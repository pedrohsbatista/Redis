using Microsoft.Extensions.DependencyInjection;
using Redis.Model.Entities;
using Redis.Model.Services;
using System.Data;
using System.Data.OleDb;
using System.Text.RegularExpressions;

namespace Redis.Test
{
    [TestFixture]
    public class ImportDefaultData : TestSetup
    {
        [Test]
        public async Task ImportCbo()
        {
            var path = Regex.Replace(Environment.CurrentDirectory, @"(?!(.+\\){3}).+", "");

            var connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={path}\\Data\\cbos.xlsx;Extended Properties='Excel 12.0;HDR=YES'";

            using var connection = new OleDbConnection(connectionString);

            using var adapter = new OleDbDataAdapter("SELECT * FROM [Cbos$]", connection);

            using var dataTable = new DataTable();

            adapter.Fill(dataTable);

            var cbos = new List<Cbo>();

            foreach (DataRow row in dataTable.Rows)
            {
                var cbo = new Cbo
                {
                    Codigo = row["CODIGO"].ToString(),
                    Descricao = row["DESCRICAO"].ToString()
                };

                cbos.Add(cbo);
            }

            var cboService = TestSetup.ServiceProvider.GetService<CboService>();

            foreach (var cbo in cbos)            
                await cboService.Insert(cbo);            
        }
    }
}
