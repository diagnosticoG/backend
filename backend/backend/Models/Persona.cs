namespace backend.Models
{
    public class Persona
    {
        public int Id { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime Fecha_Nacimiento { get; set; }
        public int Genero_Id { get; set; }
        public string Estado_Civil { get; set; }

    }
}
