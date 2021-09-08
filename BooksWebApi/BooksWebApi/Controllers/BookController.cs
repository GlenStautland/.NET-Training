using BooksWebApi.Models;
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

namespace BooksWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;


        public BookController(IConfiguration configuration, IWebHostEnvironment env)
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
                            select BookId, BookName, Category,
                            convert(varchar(10),DateOfSelling,120) as DateOfSelling,
                            BookPhoto,price
                            from dbo.Books
                            ";
            DataTable table = new DataTable();
            // BookAppCon is created in the sql application and refered to in the appsettings.json
            string sqlDataSource = _configuration.GetConnectionString("BookAppCon");
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
        public JsonResult Post(Book book)
        {
            // Docs: https://docs.microsoft.com/en-us/dotnet/api/system.data.datatable?view=net-5.0
            string query = @"insert into dbo.Books
                           (BookName,Category,DateOfSelling,BookPhoto) 
                           values
                            (
                            '" + book.BookName + @"'
                            ,'" + book.Category + @"'
                            ,'" + book.DateOfSelling + @"'
                            ,'" + book.BookPhoto + @"'
                            )";
            DataTable table = new DataTable();
            // EmplyeAppCon is created in the sql application and refered to in the appsettings.json
            string sqlDataSource = _configuration.GetConnectionString("BookAppCon");
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
        public JsonResult Put(Book book)
        {
            // Docs: https://docs.microsoft.com/en-us/dotnet/api/system.data.datatable?view=net-5.0
            string query = @"
            update dbo.Books set
            BookName = '" + book.BookName + @"'
            ,Category = '" + book.Category + @"'
            ,DateOfSelling = '" + book.DateOfSelling + @"'
            ,BookPhoto = '" + book.BookPhoto + @"'
            where BookId = " + book.BookId + @"
            ";
            DataTable table = new DataTable();
            // BookAppCon is created in the sql application and refered to in the appsettings.json
            string sqlDataSource = _configuration.GetConnectionString("BookAppCon");
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
    }

    

}
