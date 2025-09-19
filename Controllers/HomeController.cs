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
     public IActionResult ConfigurarJuego()
    {
    list<Categoria> categorias = Juego.ObtenerCategorias();
    list<Dificultades> dificultades = Juego.ObtenerDificultades();
    ViewBag.Categorias = categorias;
    categorias = Objeto.StringToList<Categoria> (HttpContext.Session.GetString("categorias"));
    //Falta crear StringToList
    ViewBag.Dificultades = dificultades;
    return View("ConfigurarJuego");
    }
    public IActionResult Comenzar(string username, int dificultad, int categoria){
        ViewBag.ListaPreguntas = Juego.CargarPartida(username,dificultad,categoria);
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
    public IActionResult VerificarRespuesta(int idPregunta, int idRespuesta) {
        ViewBag.esCorrecto = Juego.VerificarRespuesta(idRespuesta);
    }
}
