using APILivro.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APILivro.Services
{
    public class LivroService
    {
        private readonly IMongoCollection<Livro> _livros;

        public LivroService(IBibliotecaDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _livros = database.GetCollection<Livro>(settings.LivrosCollectionName);
        }

        public List<Livro> Get() =>
            _livros.Find(livro => true).ToList();

        public Livro Get(string id) =>
            _livros.Find<Livro>(livro => livro.Id == id).FirstOrDefault();

        public Livro GetTitulo(string titulo) =>
            _livros.Find<Livro>(livro => livro.Titulo == titulo).FirstOrDefault();

        public Livro GetAutor(string autor) =>
            _livros.Find<Livro>(livro => livro.Autor == autor).FirstOrDefault();

        public Livro Create(Livro livro)
        {
            _livros.InsertOne(livro);
            return livro;
        }

        public void Update(string id, Livro livroIn) =>
            _livros.ReplaceOne(livro => livro.Id == id, livroIn);

        public void Remove(Livro livroIn) =>
            _livros.DeleteOne(livro => livro.Id == livroIn.Id);

        public void Remove(string id) =>
            _livros.DeleteOne(livro => livro.Id == id);
    }
}
