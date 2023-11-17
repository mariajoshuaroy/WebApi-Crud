using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using System.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        string constr = "Data Source=DESKTOP-9S00RF6;Initial Catalog = mvc; Integrated Security = True";
        // GET: api/<EmployeeController>
        [HttpGet]
        public List<EmployeeModel> Get()
        {
            List<EmployeeModel> EmployeeObj = new List<EmployeeModel>();
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("sp_ShowEmployee", con);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    EmployeeObj.Add(new EmployeeModel
                    {
                        id = Convert.ToInt32(sdr["id"]),
                        EmpName = sdr["EmpName"].ToString(),
                        EmpNumber = sdr["EmpNumber"].ToString(),
                        EmpEmail = sdr["EmpEmail"].ToString(),
                        Address = sdr["Address"].ToString(),
                        BloodGroup = sdr["BloodGroup"].ToString(),

                    });
                }

                con.Close();
            }
            return EmployeeObj;
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public EmployeeModel Get(int id)
        {
            EmployeeModel empobj = new EmployeeModel();
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("sp_ShowEmployee_Id " + id, con);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    empobj = new EmployeeModel
                    {
                        id = Convert.ToInt32(sdr["id"]),
                        EmpName = sdr["EmpName"].ToString(),
                        EmpNumber = sdr["EmpNumber"].ToString(),
                        EmpEmail = sdr["EmpEmail"].ToString(),
                        Address = sdr["Address"].ToString(),
                        BloodGroup = sdr["BloodGroup"].ToString(),

                    };
                }

                con.Close();
            }
            return empobj;
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public void Post([FromBody] EmployeeModel obj)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string query = "sp_create '" + obj.EmpName + "','" + obj.EmpNumber + "','" + obj.EmpEmail + "','" + obj.Address + "','"
                        + obj.BloodGroup + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] EmployeeModel obj)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string query = "sp_Edit " + id + ",'" + obj.EmpName + "','" + obj.EmpNumber +
                        "','" + obj.EmpEmail + "','" + obj.Address + "','" + obj.BloodGroup + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    cmd.ExecuteReader();
                    con.Close();
                }
            }
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string query = "sp_Delete " + id;
                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
       
    }
}
