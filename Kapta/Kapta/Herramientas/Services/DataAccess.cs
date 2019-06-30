
using Kapta.Deportistas;
using Kapta.Ejercicios;
using Kapta.Herramientas.Interfaces;
using Kapta.Lesiones;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Kapta.Herramientas.Services
{

    //Clase que implementa la base de datos
    //se implementa la interfaz disponsable para conectarse y desconectarse de la base de datos
    public class DataAccess : IDisposable
    {
        private SQLiteConnection connection;
        public DataAccess()
        {
            //obtenemos la configuracion obteniendo lo que hay en la interfaz iconfig, para que coja la configuracion segun la plataforma
            var config = DependencyService.Get<IConfig>();
            connection = new SQLiteConnection(config.Plataforma,
            //como llamamos a la base de datos
            System.IO.Path.Combine(config.DirectorioDB, "Deportistas.db3"));

            //creamos la tabla de los deportistas
            connection.CreateTable<Deportista>();

            //creamos la tabla de las lesiones
            connection.CreateTable<Lesion>();

            //creamos la tabla de los ejercicios
            connection.CreateTable<TablaEjercicios>();
        }

        //DEPORTISTA BASE DE DATOS////////////////
        //insertar deportista
        public void InsertDeportista(Deportista deportista)
        {
            connection.Insert(deportista);
        }
        //actualizar
        public void UpdateDeportista(Deportista deportista)
        {
            connection.Update(deportista);
        }
        //eliminar
        public void DeleteEmpleado(Deportista deportista)
        {
            connection.Delete(deportista);
        }
        //obtener deportista
        public Deportista GetDeportista(int IDDeportista)
        {
            return connection.Table<Deportista>().FirstOrDefault(c => c.IDDeportista == IDDeportista);
        }
        //obtener lista ordenada por apellidos
        public List<Deportista> GetDeportistas()
        {
            return connection.Table<Deportista>().OrderBy(c => c.Apellidos).ToList();
        }
        //deportistas de un email
        public List<Deportista> GetDeportistass(string clavedep)
        {
            return connection.Table<Deportista>().OrderBy(c => c.Apellidos).Where(c => c.IdUser == clavedep).ToList();

        }

        //ENTRENADOR BASE DE DATOS////////////////

        /*
        //insertar ENTRENADOR
        public void InsertEntrenador(Entrenador entrenador)
        {
            connection.Insert(entrenador);
        }
        //actualizar
        public void UpdateEntrenador(Entrenador entrenador)
        {
            connection.Update(entrenador);
        }
        //eliminar
        public void DeleteEntrenador(Entrenador entrenador)
        {
            connection.Delete(entrenador);
        }
        //obtener deportista
        public Entrenador GetEntrenador(int IDEntrenador)
        {
            return connection.Table<Entrenador>().FirstOrDefault(c => c.IDEntrenador == IDEntrenador);
        }
        //obtener lista ordenada por apellidos
        public List<Entrenador> GetEntrenadores()
        {
            return connection.Table<Entrenador>().OrderBy(c => c.Apellidos).ToList();
        }
        */

        //insertar LESION
        public void InsertLesion(Lesion lesion)
        {
            connection.Insert(lesion);
        }
        //actualizar
        public void UpdateLesion(Lesion lesion)
        {
            connection.Update(lesion);
        }
        //eliminar
        public void DeleteLesion(Lesion lesion)
        {
            connection.Delete(lesion);
        }
        //obtener lesión
        public Lesion GetLesion(int IDLesion)
        {
            return connection.Table<Lesion>().FirstOrDefault(c => c.IDLesion == IDLesion);

        }
        //obtener lesión del deportista
        public List<Lesion> GetLesionDeportista(int clavedep)
        {
            return connection.Table<Lesion>().Where(c => c.clavedeportista == clavedep).ToList();

        }
        //obtener lesiones
        public List<Lesion> GetLesiones()
        {
            return connection.Table<Lesion>().OrderBy(c => c.Lugar).ToList();
        }
        //Tabla de ejercicios

        //insertar ejercicio
        public void InsertEjercicio(TablaEjercicios ejercicio)
        {
            connection.Insert(ejercicio);
        }
        //actualizar
        public void UpdateEjercicio(TablaEjercicios ejercicio)
        {
            connection.Update(ejercicio);
        }
        //eliminar
        public void DeleteEjercicio(TablaEjercicios ejercicio)
        {
            connection.Delete(ejercicio);
        }
        //obtener ejercicio
        public TablaEjercicios GetEjercicio(int IDEjercicio)
        {
            return connection.Table<TablaEjercicios>().FirstOrDefault(c => c.IDEjercicio == IDEjercicio);

        }
        public List<TablaEjercicios> GetEjercicioDeportista(int clavedep)
        {
            return connection.Table<TablaEjercicios>().Where(c => c.clavedeportista == clavedep).ToList();

        }
        public List<TablaEjercicios> GetEjercicios()
        {
            return connection.Table<TablaEjercicios>().OrderBy(c => c.IDEjercicio).ToList();
        }
        public void Dispose()
        {
            connection.Dispose();
        }
    }
}

