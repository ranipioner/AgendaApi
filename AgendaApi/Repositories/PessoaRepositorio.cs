using AgendaApi.Interface;
using AgendaApi.Models;
using System.Data.SqlClient;

namespace AgendaApi.Repositories
{
    public class PessoaRepositorio : IRepositorio<Pessoa, int>
    {
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AgendaMouts;";

        private List<Pessoa> pessoas = new List<Pessoa>();

        public void Delete(int id)
        {
            string selectQuery = "DELETE FROM tb_pessos where id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(selectQuery, connection))
            {
                try
                {
                    command.Parameters.AddWithValue("@Id", id);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public Pessoa Get(int id)
        {
            Pessoa pessoa = null;
            string selectQuery = "SELECT Id, Nome, Email, Fone FROM tb_pessoas where id = @Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(selectQuery, connection))
            {
                try
                {
                    command.Parameters.AddWithValue("@Id", id);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            pessoa = new Pessoa
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Nome = reader.GetString(reader.GetOrdinal("Nome")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),

                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.Message);
                }
            }
            return pessoa;
        }

        public IEnumerable<Pessoa> GetAll()
        {
            List<Pessoa> pessoas = new List<Pessoa>();
            string selectQuery = "SELECT Id, Nome, Email, Fone FROM tb_pessoas";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(selectQuery, connection))
            {
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            pessoas.Add(new Pessoa
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Nome = reader.GetString(reader.GetOrdinal("Nome")),
                                Email = reader.GetString(reader.GetOrdinal("Email"))

                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.Message);
                }
            }
            return pessoas;
        }

        public Pessoa Save(Pessoa entity)
        {
            string insertQuery = "insert into tb_pessoas(Nome,Email, Fone)values(@Nome, @Email, @Fone);SELECT SCOPE_IDENTITY();";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@Nome", entity.Nome);
                command.Parameters.AddWithValue("@Email", entity.Email);

                connection.Open();
                entity.Id = Convert.ToInt32(command.ExecuteScalar());
            }
            return entity;
        }

        public Pessoa Update(Pessoa entity)
        {
            string updateQuery = "update tb_pessoas set Nome = @Nome, Email = @Email, Fone = @Fone where id = @Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(updateQuery, connection))
            {
                command.Parameters.AddWithValue("@Nome", entity.Nome);
                command.Parameters.AddWithValue("@Email", entity.Email);
                command.Parameters.AddWithValue("@Fone", entity.Fone);
                command.Parameters.AddWithValue("@Id", entity.Id);
                connection.Open();
                command.ExecuteNonQuery();
            }
            return entity;
        }
    }
}
