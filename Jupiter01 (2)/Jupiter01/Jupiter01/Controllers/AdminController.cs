using Jupiter01.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace Jupiter01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AdminController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        //Admin Dashboard
        [HttpGet]
        [Route("GetAllTrainer")]

        public string GetAllTrainer()
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("Select a.username, t.name,t.designation,s.skillName,s.numberOfTrainer,o.sessionContent,o.sessionStartTime,o.sessionEndTime,b.batchName,b.batchCapacity From admin a,trainer t,skill s,session o,batch b Where a.adminId=t.adminId And t.trainerId=s.trainerId And t.trainerId=o.trainerId And o.batchId=b.batchId", con);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return JsonConvert.SerializeObject(dt);
        }

        //Admin Login
        [HttpPost]
        [Route("AdminLogin")]
        public string AdminLogin(Admin admin)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("SELECT * FROM admin WHERE username= '" + admin.Username + "' AND password= '" + admin.Password + "' ", con);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0)
            {
                return "Valid User";
            }
            else
            {
                return "InValid User";
            }
        }

        [HttpGet]
        [Route("GetTrainerById")]
        public string GetTrainerById(int id)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("Select t.name,t.designation,s.skillName,s.numberOfTrainer,o.sessionContent,o.sessionStartTime,o.sessionEndTime,b.batchName,b.batchCapacity From admin a,trainer t,skill s,session o,batch b Where a.adminId=t.adminId And t.trainerId=s.trainerId And t.trainerId=o.trainerId And o.batchId=b.batchId And t.trainerId='" + id + "' ", con);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return JsonConvert.SerializeObject(dt);

        }

        [HttpGet]
        [Route("GetTrainersBySkill")]
        public string GetTrainersBySkill(int id)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("Select s.skillName,s.numberOfTrainer,t.name,t.designation,o.sessionContent,o.sessionStartTime,o.sessionEndTime,b.batchName,b.batchCapacity From admin a,trainer t,skill s,session o,batch b Where a.adminId=t.adminId And t.trainerId=s.trainerId And t.trainerId=o.trainerId And o.batchId=b.batchId And s.skillId='" + id + "' ", con);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return JsonConvert.SerializeObject(dt);


        }
    }
}
