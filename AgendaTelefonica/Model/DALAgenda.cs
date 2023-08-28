using System;
using System.Data.SQLite;
using System.IO;
using System.Data;

namespace AgendaTelefonica
{
    public class DALAgenda
    {
        public static string path = Directory.GetCurrentDirectory() + "\\banco.sqlite";
        private static SQLiteConnection sqliteConnection;

        private static SQLiteConnection DbConnection()
        {
            sqliteConnection = new SQLiteConnection("Data Source=" + path);
            sqliteConnection.Open();
            return sqliteConnection;
        }

        public static void CriarBancoSQLite()
        {
            try
            {
                if(File.Exists(path) == false)
                {
                    SQLiteConnection.CreateFile(path);
                }
            }
            catch
            {
                throw;
            }
        }

        public static void CriarTabelaSQLite()
        {
            try
            {
                using (var cmd=DbConnection().CreateCommand())
                {
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS Contatos(id INTEGER PRIMARY KEY AUTOINCREMENT, nome Varchar(90), email Varchar(90), telefone Varchar(20), tipoContato Varchar(90))";
                    cmd.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetContatos()
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();

            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Contatos";
                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetContatos(string nome)
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();

            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Contatos WHERE nome LIKE '%" + nome+"%'";
                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetContato(int id)
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();

            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Contatos Where Id=" + id;
                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Adicionar(Contato contato)
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Contatos (nome, email, telefone, tipoContato) values(@nome, @email, @telefone, @tipoContato)";
                    //cmd.Parameters.AddWithValue("@id", contato.Id);
                    cmd.Parameters.AddWithValue("@nome", contato.Nome);
                    cmd.Parameters.AddWithValue("@email", contato.Email);
                    cmd.Parameters.AddWithValue("@telefone", contato.Telefone);
                    cmd.Parameters.AddWithValue("@tipoContato", contato.TipoContato);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Alterar(Contato contato)
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "UPDATE Contatos SET Nome=@nome, Email=@email, Telefone=@telefone, TipoContato=@tipoContato WHERE Id=@id";
                    cmd.Parameters.AddWithValue("@id", contato.Id);
                    cmd.Parameters.AddWithValue("@nome", contato.Nome);
                    cmd.Parameters.AddWithValue("@email", contato.Email);
                    cmd.Parameters.AddWithValue("@telefone", contato.Telefone);
                    cmd.Parameters.AddWithValue("@tipoContato", contato.TipoContato);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Deletar(int id)
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Contatos WHERE Id=@id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
