using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RESTDiceExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiceController : ControllerBase
    {
        private string connectionString = ConnectionString.connectionString;

        // GET: api/Dice
        [HttpGet]
        public IEnumerable<DiceRoll> Get()
        {
            string selectString = "select * from DiceRoll;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(selectString, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<DiceRoll> result = new List<DiceRoll>();
                        while (reader.Read())
                        {
                            DiceRoll dice = ReadDice(reader);
                            result.Add(dice);
                        }
                        return result;
                    }
                }
            }
        }

        private static DiceRoll ReadDice(SqlDataReader reader)
        {
            int id = reader.GetInt32(0);
            string name = reader.GetString(1);
            int number = reader.GetInt32(2);
            int guess = reader.GetInt32(3);
            int result = reader.GetInt32(4);

            DiceRoll dice = new DiceRoll()
            {
                Id = id,
                Name = name,
                Number = number,
                Guess = guess,
                Result = result
            };

            return dice;
        }

        // GET: api/Dice/5
        [Route("{id}")]
        public DiceRoll Get(int id)
        {
            string selectString = "select * from DiceRoll where id = @id";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(selectString, conn))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            return ReadDice(reader);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        [Route("byPerson/{name}")]
        public IEnumerable<DiceRoll> GetName(string name)
        {
            string selectString = "select * from DiceRoll where Name = @name";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(selectString, conn))
                {
                    command.Parameters.AddWithValue("@name", name);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<DiceRoll> result = new List<DiceRoll>();
                        while (reader.Read())
                        {
                            DiceRoll dice = ReadDice(reader);
                            result.Add(dice);
                        }
                        return result;
                    }
                }
            }
        }

        // POST: api/Dice
        [HttpPost]
        public int Post([FromBody] DiceRoll value)
        {
            string insertString = "insert into DiceRoll (Name, Number, Guess, Result) values(@name, @number, @guess, @result); ";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(insertString, conn))
                {
                    command.Parameters.AddWithValue("@name", value.Name);
                    command.Parameters.AddWithValue("@number", value.Number);
                    command.Parameters.AddWithValue("@guess", value.Guess);
                    command.Parameters.AddWithValue("@result", value.Result);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
        }

        // PUT: api/Dice/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
