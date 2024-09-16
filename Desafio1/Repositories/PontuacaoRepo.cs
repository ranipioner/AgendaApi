using Desafio1.Interfaces;
using Desafio1.Models;
using System.Data.SqlClient;

namespace Desafio1.Repositories
{
    public class PontuacaoRepo : IRepositorio<Pontuacao, int>
    {
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Desafio1;";

        private List<Pontuacao> pontuacao = new List<Pontuacao>();

        public void Delete(int id)
        {
            string selectQuery = "DELETE FROM Pontuacao where Clienteid = @ClienteId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(selectQuery, connection))
            {
                try
                {
                    command.Parameters.AddWithValue("@ClienteId", id);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public Pontuacao Get(int id)
        {
            Pontuacao pontuacao = null;
            string selectQuery = "SELECT ClienteId, Pontos FROM Pontuacao where ClienteId = @Id";
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
                            pontuacao = new Pontuacao
                            {
                                ClienteId = reader.GetInt32(reader.GetOrdinal("ClienteId")),
                                Pontos = reader.GetInt32(reader.GetOrdinal("Pontos")),

                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.Message);
                }
            }
            return pontuacao;
        }

        public IEnumerable<Pontuacao> GetAll()
        {
            List<Pontuacao> pontuacao = new List<Pontuacao>();
            string selectQuery = "SELECT ClienteId, Pontos FROM Pontuacao";
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
                            pontuacao.Add(new Pontuacao
                            {
                                ClienteId = reader.GetInt32(reader.GetOrdinal("ClienteId")),
                                Pontos = reader.GetInt32(reader.GetOrdinal("Pontos")),

                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.Message);
                }
            }
            return pontuacao;
        }

        public Pontuacao Save(Pontuacao entity)
        {
            string insertQuery = "insert into Pontuacao(ClienteId, Pontos)values(@ClienteId, @Pontos);";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@ClienteId", entity.ClienteId);
                command.Parameters.AddWithValue("@Pontos", entity.Pontos);

                connection.Open();
                command.ExecuteNonQuery();
            }
            return entity;
        }

        public Pontuacao Update(Pontuacao entity)
        {
            string updateQuery = "update Pontuacao set ClienteId = @ClienteId, Pontos = @Pontos where ClienteId = @ClienteId";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(updateQuery, connection))
            {
                command.Parameters.AddWithValue("@ClienteId", entity.ClienteId);
                command.Parameters.AddWithValue("@Pontos", entity.Pontos);
               
                connection.Open();
                command.ExecuteNonQuery();
            }
            return entity;
        }
    }
}
