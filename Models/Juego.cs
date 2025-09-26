public class Juego {
    public string username {get; private set;}
    public int PuntajeActual {get; private set;}
    public int CantidadPreguntasCorrectas{get; private set;}
    public int ContadorNroPreguntaActual{get; private set;}
    public Pregunta PreguntaActual{get; private set;}
    public List<Pregunta> ListaPreguntas{get; private set;}
    public List<Respuesta> ListaRespuestas{get; private set;}
 
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
    public Pregunta ObtenerProximaPregunta()
    {
        if (ContadorNroPreguntaActual + 1 < ListaPreguntas.Count)
        {
            return ListaPreguntas[ContadorNroPreguntaActual + 1];
        }
        return null;
    }
    public List<Respuesta> ObtenerProximasRespuestas(int idPregunta) {
        List<Respuesta> respuestas = BD.ObtenerRespuestas(idPregunta);
        return respuestas;
    }
    public bool VerificarRespuesta(int idRespuesta) {
        bool esCorrecto;
        if (idRespuesta == ListaRespuestas[ContadorNroPreguntaActual].IdRespuesta && ListaRespuestas[ContadorNroPreguntaActual].Correcta == true) {
            esCorrecto = true;
            PuntajeActual += 1000;
            CantidadPreguntasCorrectas += 1;
        }
        else {
            esCorrecto = false;
        }
        ContadorNroPreguntaActual += 1;
        ObtenerProximaPregunta();
        return esCorrecto;
    }
}
