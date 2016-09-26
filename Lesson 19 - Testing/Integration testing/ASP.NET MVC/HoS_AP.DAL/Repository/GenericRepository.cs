using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using HoS_AP.DAL.Dto;
using Newtonsoft.Json;

namespace HoS_AP.DAL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseDto
    {
        private readonly string basePath;
        private readonly string fileName;

        private List<T> entities;

        public GenericRepository(string fileName)
        {
            var uri = new UriBuilder(Assembly.GetExecutingAssembly().CodeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            basePath = Path.GetDirectoryName(path);
            this.fileName = fileName;
        }

        public IQueryable<T> Entities
        {
            get
            {
                if (entities == null)
                {
                    entities = Load(string.Empty);
                }

                return entities.AsQueryable();
            }
        }

        public void Save(T entity)
        {
            var itemToRemove = entities.FirstOrDefault(x => x.Id == entity.Id);
            if (itemToRemove != null)
            {
                entities.Remove(itemToRemove);
            }

            entities.Add(entity);
            File.WriteAllText(Path.Combine(basePath, fileName), JsonConvert.SerializeObject(entities));
        }

        private List<T> Load(string defaultFileContent)
        {
            var path = Path.Combine(basePath, fileName);
            if (!File.Exists(path))
            {
                File.WriteAllText(path, defaultFileContent);
            }

            return JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(path)) ?? new List<T>();
        }
    }
}