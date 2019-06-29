namespace Kapta.Herramientas.Services
{

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kapta.Common.Models;
using Kapta.Herramientas.Interfaces;
using SQLite;
using Xamarin.Forms;

public class DataService
{
    private SQLiteAsyncConnection connection;

    public DataService()
    {
        this.OpenOrCreateDB();
    }

    private async Task OpenOrCreateDB()
    {
            //obtenemos path de base de datos
        var databasePath = DependencyService.Get<IPathService>().GetDatabasePath();
            //conexión y se crea vacía
        this.connection = new SQLiteAsyncConnection(databasePath);
        await connection.CreateTableAsync<Exercise>().ConfigureAwait(false);
    }

    public async Task Insert<T>(T model)
    {
        await this.connection.InsertAsync(model);
    }

    public async Task Insert<T>(List<T> models)
    {
        await this.connection.InsertAllAsync(models);
    }

    public async Task Update<T>(T model)
    {
        await this.connection.UpdateAsync(model);
    }

    public async Task Update<T>(List<T> models)
    {
        await this.connection.UpdateAllAsync(models);
    }

    public async Task Delete<T>(T model)
    {
        await this.connection.DeleteAsync(model);
    }

    public async Task<List<Exercise>> GetAllExercises()
    {
        var query = await this.connection.QueryAsync<Exercise>("select * from [Exercise]");
        var array = query.ToArray();
        var list = array.Select(p => new Exercise
        {
            Name = p.Name,
            Description = p.Description,
            ImagePath = p.ImagePath,
            //IsAvailable = p.IsAvailable,
            //Price = p.Price,
            ExerciseId = p.ExerciseId,
            PublishOn = p.PublishOn,
            //Remarks = p.Remarks,
        }).ToList();
        return list;
    }

    public async Task DeleteAllExercises()
    {
        var query = await this.connection.QueryAsync<Exercise>("delete from [Exercise]");
    }
}

}


