using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;
static class BD{
private static string _connectionString = @"Server=localhost; Database=PreguntadOrt;Integrated Security=True;TrustServerCertificate=;";

public static List<Categoria> ObtenerCategorias()
{
     List<Categoria> listaCategorias = new List<Categoria>();
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        string storedProcedure = "ObtenerCategorias";
        listaCategorias = connection.Query<Categoria>(
            storedProcedure,
            commandType: CommandType.StoredProcedure
        ).ToList();
    }
    return listaCategorias;
}
public static List<Dificultad> ObtenerDificultades()
{
    List<Dificultad> listaDificultades = new List<Dificultad>();
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        string storedProcedure = "ObtenerDificultades";
        listaDificultades = connection.Query<Dificultad>(
            storedProcedure,
            commandType: CommandType.StoredProcedure
        ).ToList();
    }
    return listaDificultades;
}
public static List<Pregunta> ObtenerPreguntas(int dificultad, int categoria)
{
    List<Pregunta> listaPreguntas = new List<Pregunta>();

    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        string storedProcedure = "ObtenerPreguntas";
        listaPreguntas = connection.Query<Pregunta>(
            storedProcedure,
            new { IdDificultad = dificultad, IdCategoria = categoria },
            commandType: CommandType.StoredProcedure
        ).ToList();
    }

    return listaPreguntas;
}
public static List<Respuesta> ObtenerRespuestas(int idPregunta)
{
    List<Respuesta> listaRespuestas = new List<Respuesta>();

    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        string storedProcedure = "ObtenerRespuestas";
        listaRespuestas = connection.Query<Respuesta>(
            storedProcedure,
            new { IdPregunta = idPregunta },
            commandType: CommandType.StoredProcedure
        ).ToList();
    }

    return listaRespuestas;
}

}