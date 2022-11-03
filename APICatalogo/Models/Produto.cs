﻿namespace APICatalogo.Models
{
    public class Produto
    {
        public int ProdutoId { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public string? Preco { get; set; }
        public string? ImagemUrl { get; set; }
        public float Estoque { get; set; }
        public DateTime DataCadastroe { get; set; }
        public Categoria? Categoria { get; set; }
        public int CategoriaId { get; set; }
    }
}
