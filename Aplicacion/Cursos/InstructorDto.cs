using System;

namespace Aplicacion.Cursos
{
    public class InstructorDto
    {
        public Guid InstructorId {get; set;}
        public string Nombres {get; set;}
        public string Apellidos {get; set;}
        public string Grados {get; set;}
        public byte[] FotoPerfil {get; set;}
    }
}