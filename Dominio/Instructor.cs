using System;
using System.Collections.Generic;
namespace Dominio
{
    public class Instructor
    {
        public Guid InstructorId {get; set;}
        public string Nombres {get; set;}
        public string Apellidos {get; set;}
        public string Grados {get; set;}
        public byte[] FotoPerfil {get; set;}
        public ICollection<CursoInstructor> CursoLink {get; set;}
    }
}