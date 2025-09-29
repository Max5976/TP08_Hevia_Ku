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
    List<Categoria> categorias = Juego.ObtenerCategorias();
    List<Dificultad> dificultades = Juego.ObtenerDificultades();
    Objeto.ListToString<Categoria>("categorias", Objeto.ListToString(categorias));
    Objeto.ListToString<Dificultad>("dificultades", Objeto.ListToString(dificultades));
    ViewBag.Categorias = categorias;
    ViewBag.Dificultades = dificultades;
    return View("ConfigurarJuego");
    }
    public IActionResult Comenzar(string username, int dificultad, int categoria){
        Juego juego = new Juego();
        ViewBag.ListaPreguntas = Juego.CargarPartida(username, dificultad, categoria);
        Objeto.ObjectToString<Juego>("juego", Objeto.ObjectToString(juego));
        ViewBag.username = username;
        return View("Jugar");
    }
    
    public IActionResult Jugar() {
        Juego juego = Objeto.StringToObject<Juego>("juego");
        ViewBag.Pregunta = Juego.ObtenerProximaPregunta();
        Objeto.ObjectToString<Pregunta>("pregunta", Objeto.ObjectToString(ViewBag.Pregunta));
        if (ViewBag.Pregunta == null) {
            return View("Fin");
        }
        else {
            ViewBag.Respuestas = Juego.ObtenerProximasRespuestas(ViewBag.Pregunta.IdPregunta);
            Objeto.ListToString<Respuesta>("respuestas", Objeto.ListToString(ViewBag.Respuestas));
            return View("Juego");
        }
    }

    [HttpPost]
    public IActionResult VerificarRespuesta(int idRespuestaElegida) {
        ViewBag.esCorrecto = Juego.VerificarRespuesta(idRespuestaElegida);
        if (ViewBag.esCorrecto == false) {
            List<Respuesta> respuestas = Objeto.StringToList<Respuesta>(Objeto.StringToList("respuestas"));
            foreach(Respuesta respuesta in respuestas) {
                idRespuesta = respuesta.IdRespuesta;
                ViewBag.esLaVerdadera = Juego.VerificarRespuesta(idRespuesta);
                if (ViewBag.esLaVerdadera == true) {
                    ViewBag.respuestaCorrecta = respuesta;
                }
                else {
                    Console.WriteLine("Se ha chequeado una respuesta de las que no es, es falsa");
                }
            }
        }
        else {
            Console.WriteLine("Se ha logrado la verificación de respuesta");
        }
        return View("Respuesta");
    }
}
