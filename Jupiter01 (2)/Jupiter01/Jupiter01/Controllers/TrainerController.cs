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
    public class TrainerController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TrainerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Trainer Dashboard
        [HttpGet]
        [Route("GetAllTrainer")]

        public string GetAllTrainer()
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("Select t.name,t.designation,s.skillName,s.numberOfTrainer,o.sessionContent,o.sessionStartTime,o.sessionEndTime,l.locationName,l.locationCapacity,c.trackName,b.batchName,b.batchCapacity From trainer t,skill s,session o,batch b,location l,track c Where  t.trainerId=s.trainerId And t.trainerId=o.trainerId And o.batchId=b.batchId And o.locationId=l.locationId And b.trackId=c.trackId", con);
            con.Open();
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            con.Close();
            return JsonConvert.SerializeObject(dt);
        }

        //Trainer login
        [HttpPost]
        [Route("TrainerLogin")]

        public string TrainerLogin(Trainer trainer)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("SELECT * FROM trainer WHERE username= '" + trainer.Username + "' AND password= '" + trainer.Password + "' ", con);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return "Valid User";
            }
            else
            {
                return "InValid User";
            }

        }

        //Adding new Schedule/Session
        [HttpPost]
        [Route("AddSession")]
        public string Addsession(Session session)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("Insert into session(sessionContent,trackId,trainerId,locationId,batchId,sessionStartTime,sessionEndTime) VALUES('" + session.SessionContent + "','" + session.TrackId + "','" + session.TrainerId + "','" + session.LocationId + "','" + session.BatchId + "','" + session.SessionStartTime + "','" + session.SessionEndTime + "')", con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                return "Added new Calender";
            }
            else
            {
                return "Error";
            }

        }

        [HttpPut]
        [Route("UpdateSession")]
        public string UpdateSession(int id, [FromBody] Session session)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("Update session set sessionContent='" + session.SessionContent + "',trackId='" + session.TrackId + "',trainerId='" + session.TrainerId + "',locationId='" + session.LocationId + "',batchId='" + session.BatchId + "',sessionStartTime='" + session.SessionStartTime + "',sessionEndTime='" + session.SessionEndTime + "' Where sessionId='" + id + "' ", con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                return "Updated Calender";
            }
            else
            {
                return "Error";
            }

        }

        [HttpDelete]
        [Route("DeleteSession")]
        public string DeleteSession(int id)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("Delete From session Where sessionId='" + id + "' ", con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                return "Deleted Calender";
            }
            else
            {
                return "Error";
            }
        }

    }
}
