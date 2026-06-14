# Tech Academy -Clon de Udemy
Plataforma de aprendizaje en línea inspirada en Udemy, desarrollada como proyecto final del Bootcamp "Impulso tu futuro". 
Permite gestionar cursos, estudiantes, instructores, inscripciones y reseñas desde un panel administrativo seguro.

## Estructura del proyecto
UdemyClone 
-- Backend-UdemyClone/ # ApiRest con .net9
-- Frontend-UdemyClone/  # SPA con Angular 21
-- Base_de_datos/  # Script SQL de la base de datos

## Tecnologías Utilizadas
## BACKEND 
- .NET9
- Entity Framework Core 9
- SQL Server
- JWT Authentication
- BCrypt
- Serilog
- Scalar docs

## FRONTEND
- Angular 21
- SCSS
- Chart.js
- Font Awesome
- Standalone Components

## BASE DE DATOS
- SQL Server
- Tabla de entidades de estudiantes, instructores, cursos, categorias, inscripciones y reseñas

## Diagrama de Base de Datos 
 [Ver diagrama de relaciones en Excalidraw](https://excalidraw.com/#room=dd4605aba25230f90df6,30xbsPcO0cn59QCs0wY1Fw)

## Borrador de diseño con excalidraw
[Ver diagrama de tablas en Excalidraw](https://excalidraw.com/#json=zDkOVVr582WOgjIfdpgXZ,7eBOw2_9bnIobYuof4dMbg)


## Funcionalidades de Tech Academy

### Panel Público
- Página de inicio con slider y cursos en tendencia
- Catálogo de cursos con filtros
- Página de instructores
- Página de contacto

### Panel Administrativo (requiere login)
- Dashboard con estadísticas 
- Gestión de Estudiantes (CRUD)
- Gestión de Instructores 
- Gestión de Cursos 
- Gestión de Categorías 
- Gestión de Inscripciones 
- Gestión de Reseñas 

### Seguridad
- Autenticación con JWT
- Rutas protegidas con Guards
- Encriptación de contraseñas con BCrypt


