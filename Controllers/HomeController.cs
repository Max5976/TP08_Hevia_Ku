using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP08_Ku_Hevia.Models;

namespace TP08_Ku_Hevia.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
     public IActionResult ConfigurarJuego(string username, string dificultad, string categoria)  //En index poner form para entrar a juego
    {
    Juego juego = new Juego();
    juego.CargarPartida(username, dificultad, categoria);
    Objeto.ListToString<Juego>("juego", Objeto.ListToString(juego));
    List<Categoria> categorias = Juego.ObtenerCategorias();
    List<Dificultad> dificultades = Juego.ObtenerDificultades();
    Objeto.ListToString<Categoria>("categorias", Objeto.ListToString(categorias));
    Objeto.ListToString<Dificultad>("dificultades", Objeto.ListToString(dificultades));
    ViewBag.Categorias = categorias;
    ViewBag.Dificultades = dificultades;
    return View("ConfigurarJuego");
    }
    public IActionResult Comenzar(string username, int dificultad, int categoria){
        ViewBag.ListaPreguntas = Juego.CargarPartida(username, dificultad, categoria);
        ViewBag.username = username;
        return View("Jugar");
    }
    
    public IActionResult Jugar() {
        ViewBag.Pregunta = Juego.ObtenerProximaPregunta();
        if (ViewBag.Pregunta == null) {
            return View("Fin");
        }
        else {
            ViewBag.Respuestas = Juego.ObtenerProximasRespuestas(ViewBag.Pregunta.IdPregunta);
            return View("Juego");
        }
    }

    [HttpPost]
    public IActionResult VerificarRespuesta(int idRespuestaElegida) {
        ViewBag.esCorrecto = Juego.VerificarRespuesta(idRespuestaElegida);
        if (ViewBag.esCorrecto == false) {
            //foreach(idRespuesta in ObtenerRespuestas(idPregunta)) {

            //}
        }
        return View("Respuesta");
    }
}
