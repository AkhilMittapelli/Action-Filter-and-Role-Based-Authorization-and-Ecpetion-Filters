using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.IdentityModel.Tokens;
using Technical.Filters;
using Technical.Models;

namespace Technical.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public StudentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
         
        private static List<Student> students = new List<Student>();
        [HttpPost]
        [Route("StudentLogin")]
        public IActionResult StudentLogin(Login login)
        {
            var exsistingStudent = students.FirstOrDefault(a => a.StudentUserName == login.StudentUserName && a.Password == login.Password);

            if (exsistingStudent == null)
            {

                return NotFound();
            }

            var token = GenerateJwtToken(exsistingStudent);

            return Ok(new { Token = token });
        }

        private object GenerateJwtToken(Student student)
        {
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, student.StudentUserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("RollNo", student.RollNo.ToString()),
                new Claim("Name", student.Name),
                //new Claim(ClaimTypes.Role, student.Role)
                 new Claim(ClaimTypes.Role, student.Role)
            };

            var token = new JwtSecurityToken(
                issuer: "YourIssuer",
                audience: "YourAudience",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        [HttpPost]
        [Route("AddStudent")]
    
        public ActionResult AddStudent(Student student1)
        {
            if (student1 == null)
            {

                throw new Exception();
            }

            students.Add(student1);
            return Ok("Student Successfully Added");
        }

        [HttpGet]
        [Route("GetStudents")]
        [MyLogging("Controller")]
        public List<Student> GetStudents()
        {
            if (students == null)
            {
                throw new Exception();
            }
            return students;
        }

        [HttpGet]
        [AsyncLogging("Asyn-Controller")]
        [Route("GetStudent/Id")]
        [GlobalException]
        public ActionResult<Student> GetStudent(int RollNo)
        {
            var student = students.FirstOrDefault(s => s.RollNo == RollNo);

            if (student == null)
            {
                throw new Exception();
            }

            return student;
        }

        [HttpDelete]
        [Route("DeleteStudent/RollNo")]
        [CustomDeleteException]
        [Authorize(Roles= "Teacher")]

        public ActionResult DeleteStudent(int RollNo)
        {
            var existStudent = students.FirstOrDefault(a => a.RollNo == RollNo);

            if (existStudent == null)
            {
                throw new Exception();
            }

            students.Remove(existStudent);
            return Ok("Student is successfully deleted only by Teacher");
        }
        
    }
}
