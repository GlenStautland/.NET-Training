using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public EmployeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        public JsonResult Get()
        {
            // Docs: https://docs.microsoft.com/en-us/dotnet/api/system.data.datatable?view=net-5.0
            string query = @"
                            select EmployeId, EmployeName, Department,
                            convert(varchar(10),DateOfJoining,120) as DateOfJoining,
                            PhotoFileName
                            from dbo.Employe
                            ";
            DataTable table = new DataTable();
            // EmplyeAppCon is created in the sql application and refered to in the appsettings.json
            string sqlDataSource = _configuration.GetConnectionString("EmployeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Employe emp)
        {
            // Docs: https://docs.microsoft.com/en-us/dotnet/api/system.data.datatable?view=net-5.0
            string query = @"insert into dbo.Employe
                           (EmployeName,Department,DateOfJoining,PhotoFileName) 
                           values
                            (
                            '" + emp.EmployeName + @"'
                            ,'" + emp.Department + @"'
                            ,'" + emp.DateOfJoining + @"'
                            ,'" + emp.PhotoFileName + @"'
                            )";
            DataTable table = new DataTable();
            // EmplyeAppCon is created in the sql application and refered to in the appsettings.json
            string sqlDataSource = _configuration.GetConnectionString("EmployeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Employe emp)
        {
            // Docs: https://docs.microsoft.com/en-us/dotnet/api/system.data.datatable?view=net-5.0
            string query = @"
            update dbo.Employe set
            EmployeName = '" + emp.EmployeName + @"'
            ,Department = '" + emp.Department + @"'
            ,DateOfJoining = '" + emp.DateOfJoining + @"'
            ,PhotoFileName = '" + emp.PhotoFileName + @"'
            where EmployeId = " + emp.EmployeId + @"
            ";
            DataTable table = new DataTable();
            // EmplyeAppCon is created in the sql application and refered to in the appsettings.json
            string sqlDataSource = _configuration.GetConnectionString("EmployeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Update Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            // Docs: https://docs.microsoft.com/en-us/dotnet/api/system.data.datatable?view=net-5.0
            string query = @"
            delete from dbo.Employe
            where EmployeId = " + id + @"
            ";
            DataTable table = new DataTable();
            // EmplyeAppCon is created in the sql application and refered to in the appsettings.json
            string sqlDataSource = _configuration.GetConnectionString("EmployeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Delete Successfully");
        }
    }
}
