using System;
using System.Collections.Generic;

namespace db_1.Models;

public partial class Nota
{
    public int Id { get; set; }

    public float Nota1 { get; set; }

    public float Ponderacion { get; set; }

    public int EstudianteId { get; set; }

    public int AsignaturaId { get; set; }

    public virtual Asignatura Asignatura { get; set; } = null!;

    public virtual Estudiante Estudiante { get; set; } = null!;
}
