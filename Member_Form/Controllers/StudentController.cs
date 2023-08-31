using Member_Form.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Member_Form.Controllers
{
    public class StudentController : Controller
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        //private string message;

        public Response RegisterStudent(Student Stu)
        {
            try
            {
                Student EL = new Student();
                if (ModelState.IsValid)
                {
                    RegisterResponse registerResponse = RegisterCustomer(Stu);

                    return new Response
                    {
                        Status = "Success",
                        Message = registerResponse.Message + " Id: " + registerResponse.RecordId
                    };
                }
                else
                {

                    return new Response
                    {
                        Status = "Error",
                        Message = "false"
                    };
                }

            }
            catch (Exception)
            {
                return new Response
                { Status = "Error", Message = "Invalid Data." };
            }
        }

        private RegisterResponse RegisterCustomer(Student leadRequest)
        {
            string message = "";
            RegisterResponse response = new RegisterResponse();

            if (string.IsNullOrEmpty(leadRequest.PhoneNumber))
            {
                message = "failed";
                response.Message = "phoneno is empty";
                response.RecordId = Guid.Empty.ToString();

                return response;
            }


            using (SqlConnection conn = new SqlConnection())
            {

                conn.ConnectionString = connectionString;

                Guid StudentID = Guid.NewGuid();

                string studentID = string.Empty;


                try
                {
                    studentID = GetRegisterStudent(leadRequest.PhoneNumber);

                    if (string.IsNullOrEmpty(studentID))
                    {
                        conn.Open();
                        string cmdString = "insert into Student(StudentID, Name,PhoneNumber,Gender,Address, Photo) VALUES ( @StudentID, @name, @PhoneNumber,@Gender,@Address, @Photo)";
                        using (SqlCommand comm = new SqlCommand())
                        {
                            comm.Connection = conn;
                            comm.CommandText = cmdString;

                            comm.Parameters.AddWithValue("@RegisterId", StudentID);
                            comm.Parameters.AddWithValue("@Name", leadRequest.Name);
                            comm.Parameters.AddWithValue("@PhoneNumber", leadRequest.PhoneNumber);
                            comm.Parameters.AddWithValue("@Gender", leadRequest.Gender);
                            comm.Parameters.AddWithValue("@Address", leadRequest.Address);
                            comm.Parameters.AddWithValue("@Photo", leadRequest.Photo);
                            
                            comm.ExecuteNonQuery();
                        }

                        Console.WriteLine("Connection Open ! ");
                        message = "Success";
                        response.Message = message;
                        response.RecordId = StudentID.ToString();
                        //Close the connection
                        conn.Close();
                    }
                    else
                    {
                        message = "Failed, phoneno already exists";
                        response.Message = message;
                        response.RecordId = studentID;
                    }
                }
                catch (Exception ex)
                {
                 
                    response.Message = "Failed: " + ex;
                    response.RecordId = StudentID.ToString();
                }

            }

            return response;
        }

        private string GetRegisterStudent(object phoneNumber)
        {
            throw new NotImplementedException();
        }
    }
}