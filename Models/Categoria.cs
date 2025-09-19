public class Categoria
    {
        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
        public string Foto { get; set; }

        public Categoria(int Idcategoria, string nombre, string foto){
            IdCategoria = Idcategoria;
            Nombre = nombre;
            Foto = foto;    
        }
    }