public class Juego{
    string username{get; set;}
    int PuntajeActual {get; set;}
    int CantidadPreguntasCorrectas{get; set;}
    int ContadorNroPreguntaActual{get; set;}
    Pregunta PreguntaActual{get; set;}
    List<Pregunta> ListaPreguntas{get; set;}
    List<Respuesta> ListaRespuestas{get; set;}
 
    private void InicializarJuego(){
        username = null;
        PuntajeActual = 0;
        CantidadPreguntasCorrectas = 0;
        ContadorNroPreguntaActual = 0;
        PreguntaActual = null;
        ListaPreguntas = null;
        ListaRespuestas = null;
    }

public List<Categoria> ObtenerCategorias(){
            return BD.ObtenerCategorias();
        }
public List<Dificultad> ObtenerDificultades(){
            return BD.ObtenerDificultades();
}
public List<Pregunta> CargarPartida(string username, int dificultad, int categoria){
    InicializarJuego();
    List<Pregunta> listaPreguntas = BD.ObtenerPreguntas(dificultad,categoria);
    return listaPreguntas;
}
}
