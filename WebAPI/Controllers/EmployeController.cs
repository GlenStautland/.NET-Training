using Microsoft.AspNetCore.Hosting;
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
using System.IO;
namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public EmployeController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }


        [HttpGet]
        public JsonResult Get()
        {
            // Docs: https://docs.microsoft.com/en-us/dotnet/api/system.data.datatable?view=net-5.0
            // SQl docs: https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqldatareader?view=dotnet-plat-ext-5.0
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

        
        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        //Docs https://docs.microsoft.com/en-us/dotnet/api/system.io.filestream?view=net-5.0
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception)
            {

                return new JsonResult("anonymous.png");
            }
        }
        [Route("GetAllDepartmentNames")]
        [HttpGet]
        public JsonResult GetAllDepartmentNames()
        {
            // Docs: https://docs.microsoft.com/en-us/dotnet/api/system.data.datatable?view=net-5.0
            string query = @"select DepartmentName from dbo.Department";
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

    }
}
