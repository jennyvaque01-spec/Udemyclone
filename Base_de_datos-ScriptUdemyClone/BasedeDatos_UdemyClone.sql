CREATE DATABASE UdemyClone
GO
 
USE UdemyClone
GO

--------- Tabla Independiente-----------
-----DDL-----
CREATE TABLE Categoria (
    CategoriaId INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    CategoriaPadreId INT NULL,                           --Para la autoreferencia para las subcategorias
    CONSTRAINT FK_Categoria_Padre FOREIGN KEY (CategoriaPadreId) REFERENCES Categoria(CategoriaId)
);

CREATE TABLE Instructores (
    InstructorId INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Email VARCHAR(100) UNIQUE NOT NULL,
    FechaRegistro DATETIME NOT NULL DEFAULT SYSUTCDATETIME()
);

CREATE TABLE Estudiantes (
    EstudianteId INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Email VARCHAR(100) UNIQUE NOT NULL,
    FechaRegistro DATETIME NOT NULL DEFAULT SYSUTCDATETIME()
);

CREATE TABLE TipoLeccion (
    TipoLeccionId INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL
);

-----Tabla dependiente------
CREATE TABLE Cursos (
    CursoId INT PRIMARY KEY IDENTITY(1,1),
    Titulo NVARCHAR(250) NOT NULL,
    Descripcion TEXT,
	Precio DECIMAL(10,2) NOT NULL,
	Idioma NVARCHAR(50),
	Nivel NVARCHAR(50),                                     ---Nivel el usuario principiante, intermedio, avanazado
    CategoriaId INT NOT NULL,
	CONSTRAINT FK_Cursos_Categoria FOREIGN KEY (CategoriaId) REFERENCES Categoria(CategoriaId)
);

------Relacion entre N:M (Curso y Instructores)------
CREATE TABLE CursoInstructor(
    CursoId INT NOT NULL,
    InstructorId INT NOT NULL,
    PRIMARY KEY (CursoId, InstructorId),
    CONSTRAINT FK_CI_Curso FOREIGN KEY (CursoId) REFERENCES Cursos(CursoId),
    CONSTRAINT FK_CI_Instructor FOREIGN KEY (InstructorId) REFERENCES Instructores(InstructorId)
)

CREATE TABLE Secciones (
    SeccionId INT PRIMARY KEY IDENTITY(1,1),
    CursoId INT NOT NULL,
    Titulo NVARCHAR(255) NOT NULL,
    Orden INT NOT NULL, -- Para el orden de cada seccion
    CONSTRAINT FK_Secciones_Curso FOREIGN KEY (CursoId) REFERENCES Cursos(CursoId)
);

CREATE TABLE Lecciones (
    LeccionId INT PRIMARY KEY IDENTITY(1,1),
    SeccionId INT NOT NULL,
    TipoLeccionId INT NOT NULL,
    Titulo NVARCHAR(255) NOT NULL,
    DuracionSegundos INT NOT NULL,
    Orden INT NOT NULL,
    CONSTRAINT FK_Lecciones_Seccion FOREIGN KEY (SeccionId) REFERENCES Secciones(SeccionId),
    CONSTRAINT FK_Lecciones_Tipo FOREIGN KEY (TipoLeccionId) REFERENCES TipoLeccion(TipoLeccionId)
);

CREATE TABLE Inscripciones (
    InscripcionId INT PRIMARY KEY IDENTITY(1,1),
    EstudianteId INT NOT NULL,
    CursoId INT NOT NULL,
    FechaInscripcion DATETIME DEFAULT SYSUTCDATETIME(),
    PrecioPagado DECIMAL(10,2) NOT NULL,                                  --Recibe el precio al momento de obtener el curso 
    CuponCodigo NVARCHAR(20) NULL,
    CONSTRAINT UC_Estudiante_Curso UNIQUE (EstudianteId, CursoId),        -- No se permite una inscripcion doble
    CONSTRAINT FK_Inscrip_Estudiante FOREIGN KEY (EstudianteId) REFERENCES Estudiantes(EstudianteId),
    CONSTRAINT FK_Inscrip_Curso FOREIGN KEY (CursoId) REFERENCES Cursos(CursoId)
);

CREATE TABLE Resenas (
    EstudianteId INT NOT NULL,
    CursoId INT NOT NULL,
    Calificacion INT CHECK (Calificacion BETWEEN 1 AND 5),            --califiacion de cada curso de 1 al 5
    Resena TEXT,
    Fecha DATETIME DEFAULT SYSUTCDATETIME(),
    PRIMARY KEY (EstudianteId, CursoId),                              --solo permite una reseña por curso
    CONSTRAINT FK_Resena_Estudiante FOREIGN KEY (EstudianteId) REFERENCES Estudiantes(EstudianteId),
    CONSTRAINT FK_Resena_Curso FOREIGN KEY (CursoId) REFERENCES Cursos(CursoId)
);

CREATE TABLE ListaDeseos (
    CursoId INT NOT NULL, 
    EstudianteId INT NOT NULL,
    PRIMARY KEY (CursoId, EstudianteId),
    CONSTRAINT FK_Wish_Curso FOREIGN KEY (CursoId) REFERENCES Cursos(CursoId),
    CONSTRAINT FK_Wish_Estudiante FOREIGN KEY (EstudianteId) REFERENCES Estudiantes(EstudianteId)
);

CREATE TABLE Progreso (
    EstudianteId INT NOT NULL,
    LeccionId INT NOT NULL,
    FechaTerminada DATETIME DEFAULT GETDATE(),
    PRIMARY KEY (EstudianteId, LeccionId),
    CONSTRAINT FK_Prog_Estudiante FOREIGN KEY (EstudianteId) REFERENCES Estudiantes(EstudianteId),
    CONSTRAINT FK_Prog_Leccion FOREIGN KEY (LeccionId) REFERENCES Lecciones(LeccionId)
);

---------DML-----------
USE UdemyClone
GO

----------- Tabla Categoria -----------
INSERT INTO Categoria (Nombre, CategoriaPadreId)
VALUES
    ('Programación', NULL),
    ('Desarrollo Web', 1),
    ('Bases de Datos', 1),
    ('Seguridad Informática', 1),
    ('Inteligencia Artificial', 1),
    ('Frontend', 2),  -- Subcategoría de Desarrollo Web
    ('Backend', 2);   -- Subcategoría de Desarrollo Web

----------- Tabla Instructores -----------
INSERT INTO Instructores (Nombre, Email)
VALUES
    ('Carlos Pérez', 'carlos.perez@gmail.com'),
    ('Ana Sánchez', 'ana.sanchez@hotmail.com'),
    ('Javier Gómez', 'javier.gomez@outlook.com'),
    ('Laura Rodríguez', 'laura.rodriguez@yahoo.com'),
    ('Eduardo Díaz', 'eduardo.diaz@gmail.com');

----------- Tabla Estudiantes -----------
INSERT INTO Estudiantes (Nombre, Email)
VALUES
    ('Juan Martínez', 'juan.martinez@outlook.com'),
    ('Marta López', 'marta.lopez@gmail.com'),
    ('Andrés González', 'andres.gonzalez@hotmail.com'),
    ('Elena Ruiz', 'elena.ruiz@outlook.com'),
    ('Luis García', 'luis.garcia@gmail.com'),
    ('Paola Pérez', 'paola.perez@gmail.com'),
    ('Felipe Fernández', 'felipe.fernandez@hotmail.com'),
    ('María Rodríguez', 'maria.rodriguez@gmail.com'),
    ('Ricardo Torres', 'ricardo.torres@yahoo.com'),
    ('Valeria Martínez', 'valeria.martinez@outlook.com');

----------- Tabla TipoLeccion -----------
INSERT INTO TipoLeccion (Nombre)
VALUES
    ('Video'),
    ('Lectura'),
    ('Examen'),
    ('Práctica'),
    ('Foro');

----------- Tabla Cursos -----------
INSERT INTO Cursos (Titulo, Descripcion, Precio, Idioma, Nivel, CategoriaId)
VALUES
    ('Introducción a la Programación en Python', 'Curso básico de programación en Python.', 50.00, 'Español', 'Principiante', 1),
    ('Desarrollo Web con HTML y CSS', 'Curso para aprender a crear páginas web estáticas.', 60.00, 'Español', 'Principiante', 2),
    ('Bases de Datos con MySQL', 'Curso completo de bases de datos relacionales utilizando MySQL.', 70.00, 'Español', 'Intermedio', 3),
    ('Seguridad Informática: Fundamentos', 'Aprende las bases de la seguridad en informática.', 80.00, 'Español', 'Avanzado', 4),
    ('Introducción a la Inteligencia Artificial con Python', 'Curso para introducirte en la inteligencia artificial usando Python.', 90.00, 'Español', 'Intermedio', 5);

----------- Tabla CursoInstructor -----------
INSERT INTO CursoInstructor (CursoId, InstructorId)
VALUES
    (1, 1),
    (2, 2),
    (3, 3),
    (4, 4),
    (5, 5);

----------- Tabla Secciones -----------
INSERT INTO Secciones (CursoId, Titulo, Orden)
VALUES
    (1, 'Introducción a la Programación', 1),
    (1, 'Variables y Tipos de Datos', 2),
    (1, 'Condicionales y Bucles', 3),
    (1, 'Funciones y Librerías', 4),
    (2, 'Introducción al Desarrollo Web', 1),
    (2, 'HTML Básico', 2),
    (2, 'CSS y Diseño Web', 3),
    (2, 'JavaScript para Principiantes', 4),
    (3, 'Conceptos Básicos de Bases de Datos', 1),
    (3, 'Modelado Relacional', 2),
    (3, 'Consultas SQL', 3),
    (3, 'Optimización de Consultas', 4),
    (4, 'Conceptos de Seguridad Informática', 1),
    (4, 'Criptografía Básica', 2),
    (4, 'Hacking Ético', 3),
    (5, 'Introducción a la Inteligencia Artificial', 1),
    (5, 'Redes Neuronales Básicas', 2),
    (5, 'Aplicaciones de IA', 3);

----------- Tabla Lecciones -----------
INSERT INTO Lecciones (SeccionId, TipoLeccionId, Titulo, DuracionSegundos, Orden)
VALUES
    (1, 1, 'Qué es la Programación', 600, 1),
    (1, 1, 'Primer Programa en Python', 900, 2),
    (2, 2, 'Tipos de Datos en Python', 600, 1),
    (3, 3, 'Estructuras Condicionales', 1200, 1),
    (3, 3, 'Bucles en Python', 1500, 2),
    (4, 4, 'Uso de Funciones en Python', 1800, 1),
    (5, 1, 'Qué es HTML', 600, 1),
    (5, 1, 'Estructura de una Página Web', 800, 2),
    (6, 2, 'CSS Básico', 1000, 1),
    (7, 3, 'Primer Examen de Seguridad', 900, 1),
    (8, 2, 'Modelo Relacional de Bases de Datos', 1200, 1),
    (9, 4, 'Prácticas de SQL', 1800, 1),
    (10, 3, 'Introducción a Criptografía', 1500, 1),
    (11, 1, 'Redes Neuronales en IA', 1000, 1),
    (12, 5, 'Prácticas en Seguridad Informática', 1200, 1);

----------- Tabla Inscripciones -----------
INSERT INTO Inscripciones (EstudianteId, CursoId, PrecioPagado, CuponCodigo)
VALUES
    (1, 1, 50.00, NULL),
    (2, 2, 60.00, 'DESCUENTO10'),
    (3, 3, 70.00, NULL),
    (4, 4, 80.00, 'OFERTA20'),
    (5, 5, 90.00, NULL),
    (6, 1, 50.00, 'DESCUENTO10'),
    (7, 2, 60.00, NULL),
    (8, 3, 70.00, NULL),
    (9, 4, 80.00, NULL),
    (10, 5, 90.00, 'OFERTA20');

----------- Tabla Resenas -----------
INSERT INTO Resenas (EstudianteId, CursoId, Calificacion, Resena)
VALUES
    (1, 1, 5, 'Excelente curso para empezar en programación.'),
    (2, 2, 4, 'Buen curso, aunque el tema de CSS podría ser más profundo.'),
    (3, 3, 5, 'Muy completo, aprendí mucho sobre bases de datos.'),
    (4, 4, 4, 'Interesante, pero hay mucho contenido teórico.'),
    (5, 5, 5, 'Impresionante curso de Inteligencia Artificial, muy recomendable.');

----------- Tabla ListaDeseos -----------
INSERT INTO ListaDeseos (CursoId, EstudianteId)
VALUES
    (1, 6),
    (2, 7),
    (3, 8),
    (4, 9),
    (5, 10),
    (1, 1),
    (2, 2),
    (3, 3),
    (4, 4),
    (5, 5);

----------- Tabla Progreso -----------
INSERT INTO Progreso (EstudianteId, LeccionId)
VALUES
    (1, 1),
    (2, 2),
    (3, 3),
    (4, 4),
    (5, 5),
    (6, 6),
    (7, 7),
    (8, 8),
    (9, 9),
    (10, 10);

-------Consultas---------
USE UdemyClone;
GO

-- 1. Cursos con su calificación promedio y número de reseñas---
SELECT 
    c.Titulo, 
    AVG(CAST(r.Calificacion AS FLOAT)) AS PromedioCalificacion,
    COUNT(r.EstudianteId) AS TotalResenas
FROM Cursos c
LEFT JOIN Resenas r ON c.CursoId = r.CursoId
GROUP BY c.Titulo
ORDER BY PromedioCalificacion DESC;

---2.  Progreso de un estudiante en todos sus cursos inscritos ------
---Vamos a tomar a juan martinez como referencia -----
SELECT 
    c.Titulo,
    COUNT(DISTINCT p.LeccionId) AS LeccionesCompletadas,
    COUNT(DISTINCT l.LeccionId) AS TotalLecciones,
    (CAST(COUNT(DISTINCT p.LeccionId) AS FLOAT) / COUNT(DISTINCT l.LeccionId)) * 100 AS PorcentajeAvance
FROM Estudiantes e
JOIN Inscripciones i ON e.EstudianteId = i.EstudianteId
JOIN Cursos c ON i.CursoId = c.CursoId
JOIN Secciones s ON c.CursoId = s.CursoId
JOIN Lecciones l ON s.SeccionId = l.SeccionId
LEFT JOIN Progreso p ON e.EstudianteId = p.EstudianteId AND l.LeccionId = p.LeccionId
WHERE e.Email = 'juan.martinez@outlook.com'
GROUP BY c.Titulo;

----3. Top 5 instructores por número de estudiantes inscriptos-----
SELECT TOP 5
    ins.Nombre AS Instructor, 
    COUNT(DISTINCT i.EstudianteId) AS TotalEstudiantes
FROM Instructores ins
JOIN CursoInstructor ci ON ins.InstructorId = ci.InstructorId
JOIN Inscripciones i ON ci.CursoId = i.CursoId
GROUP BY ins.Nombre
ORDER BY TotalEstudiantes DESC;

----4.  Cursos a los que un estudiante está inscrito pero no ha iniciad0 o no se han inscriptos-------
SELECT 
    c.Titulo, 
    i.FechaInscripcion
FROM Estudiantes e
JOIN Inscripciones i ON e.EstudianteId = i.EstudianteId
JOIN Cursos c ON i.CursoId = c.CursoId
LEFT JOIN Progreso p ON e.EstudianteId = p.EstudianteId 
    AND p.LeccionId IN (SELECT LeccionId FROM Lecciones l JOIN Secciones s ON l.SeccionId = s.SeccionId WHERE s.CursoId = c.CursoId)
WHERE e.Email = 'marta.lopez@gmail.com'
AND p.LeccionId IS NULL;

------5.  Ingresos totales por curso es la suma de precios pagado---------
SELECT 
    c.Titulo, 
    SUM(i.PrecioPagado) AS IngresosTotales
FROM Cursos c
JOIN Inscripciones i ON c.CursoId = i.CursoId
GROUP BY c.Titulo
ORDER BY IngresosTotales DESC;

-- 6. Lecciones de un curso con su sección y duración
SELECT 
    s.Titulo AS Seccion,
    l.Titulo AS Leccion,
    ROUND(CAST(l.DuracionSegundos AS FLOAT) / 60, 2) AS DuracionMinutos,
    lt.Nombre AS TipoLeccion
FROM Cursos c
JOIN Secciones s ON c.CursoId = s.CursoId
JOIN Lecciones l ON s.SeccionId = l.SeccionId
JOIN TipoLeccion lt ON l.TipoLeccionId = lt.TipoLeccionId
WHERE c.Titulo = 'Introducción a la Programación en Python'
ORDER BY s.Orden, l.Orden;
GO

-- 7. Estudiantes que completaron el 100% de al menos un curso
SELECT 
    e.Nombre AS Estudiante,
    c.Titulo AS Curso
FROM Estudiantes e
JOIN Inscripciones i ON e.EstudianteId = i.EstudianteId
JOIN Cursos c ON i.CursoId = c.CursoId
JOIN Secciones s ON c.CursoId = s.CursoId
JOIN Lecciones l ON s.SeccionId = l.SeccionId
LEFT JOIN Progreso p ON e.EstudianteId = p.EstudianteId 
    AND l.LeccionId = p.LeccionId
GROUP BY e.Nombre, c.Titulo
HAVING COUNT(DISTINCT l.LeccionId) = COUNT(DISTINCT p.LeccionId);
GO

-- 8. Cursos en lista de deseos de un estudiante que aún no ha comprado
SELECT 
    c.Titulo,
    c.Precio
FROM ListaDeseos ld
JOIN Estudiantes e ON ld.EstudianteId = e.EstudianteId
JOIN Cursos c ON ld.CursoId = c.CursoId
LEFT JOIN Inscripciones i 
    ON e.EstudianteId = i.EstudianteId 
    AND c.CursoId = i.CursoId
WHERE e.Email = 'paola.perez@gmail.com'
  AND i.InscripcionId IS NULL;
GO

-- 9. Duración total en horas de cada curso
SELECT 
    c.Titulo,
    ROUND(SUM(CAST(l.DuracionSegundos AS FLOAT)) / 3600, 2) AS TotalHoras
FROM Cursos c
JOIN Secciones s ON c.CursoId = s.CursoId
JOIN Lecciones l ON s.SeccionId = l.SeccionId
GROUP BY c.Titulo
ORDER BY TotalHoras DESC;
GO

-- 10. Cursos por categoría con su precio promedio
SELECT 
    cat.Nombre AS Categoria,
    COUNT(c.CursoId) AS TotalCursos,
    ROUND(AVG(CAST(c.Precio AS FLOAT)), 2) AS PrecioPromedio
FROM Categoria cat
JOIN Cursos c ON cat.CategoriaId = c.CategoriaId
GROUP BY cat.Nombre
ORDER BY TotalCursos DESC;
GO

USE UdemyClone
GO

CREATE TABLE Logs (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Message NVARCHAR(MAX) NULL,
	MessageTemplate NVARCHAR(MAX) NULL,
    Level NVARCHAR(128) NULL,
    TimeStamp DATETIME NOT NULL DEFAULT GETDATE(),
    Exception NVARCHAR(MAX) NULL,
    Properties NVARCHAR(MAX) NULL
)
SELECT TOP 10 * FROM Logs ORDER BY TimeStamp DESC



USE UdemyClone
GO

CREATE TABLE Usuario (
    UsuarioId INT PRIMARY KEY IDENTITY(1,1),
    Email VARCHAR(100) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    Rol NVARCHAR(50) NOT NULL DEFAULT 'Estudiante',
    FechaRegistro DATETIME DEFAULT SYSUTCDATETIME()
)


SELECT * FROM Cursos;


SELECT * 
FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_NAME LIKE 'Instructor%';


SELECT * FROM Inscripciones;
