using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP08_Ku_Hevia.Models;

namespace TP08_Ku_Hevia.Controllers
{
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
            Juego juego = new Juego();

            List<Categoria> categorias = juego.ObtenerCategorias();
            List<Dificultad> dificultades = juego.ObtenerDificultades();
            HttpContext.Session.SetString("categorias", Objeto.ListToString(categorias));
            HttpContext.Session.SetString("dificultades", Objeto.ListToString(dificultades));
            ViewBag.Categorias = categorias;
            ViewBag.Dificultades = dificultades;
            return View("/ConfigurarJuego");
        }

        public IActionResult Comenzar(string username, int dificultad, int categoria)
        {

            Juego juego = new Juego();
            ViewBag.ListaPreguntas = juego.CargarPartida(username, dificultad, categoria);
            HttpContext.Session.SetString("juego", Objeto.ObjectToString(juego));
            HttpContext.Session.SetString("username", username);
            ViewBag.username = username;    
            return View("Jugar");
        }

        public IActionResult Jugar()
        {
             var jsonJuego = HttpContext.Session.GetString("juego");
            Juego juego = Objeto.StringToObject<Juego>(jsonJuego);

            Pregunta pregunta = juego.ObtenerProximaPregunta();
            HttpContext.Session.SetString("pregunta", Objeto.ObjectToString(pregunta));

            if (pregunta == null)
            {
                return View("Fin");
            }
            else
            {
                var respuestas = juego.ObtenerProximasRespuestas(pregunta.IdPregunta);


                HttpContext.Session.SetString("respuestas", Objeto.ListToString(respuestas));

                ViewBag.Pregunta = pregunta;
                ViewBag.Respuestas = respuestas;
                return View("Juego");
            }
        }

        [HttpPost]
        public IActionResult VerificarRespuesta(int idRespuestaElegida)
        {
            var jsonJuego = HttpContext.Session.GetString("juego");
            Juego? juego = Objeto.StringToObject<Juego>(jsonJuego);
            ViewBag.esCorrecto = juego.VerificarRespuesta(idRespuestaElegida);

            if (ViewBag.esCorrecto == false)
            {
                List<Respuesta> respuestas = Objeto.StringToList<Respuesta>(HttpContext.Session.GetString("respuestas"));
                foreach (Respuesta respuesta in respuestas)
                {
                    if (juego.VerificarRespuesta(respuesta.IdRespuesta))
                    {
                        ViewBag.respuestaCorrecta = respuesta;
                        break;  
                    }
                }
            }
            return View("Respuesta");
        }
    }
}
